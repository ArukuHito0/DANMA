using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ObjectPoolSystem
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField]
        private uint poolSize;
        [SerializeField]
        private PooledObject objectToPool;
        [SerializeField]
        private Stack<PooledObject> pool;

        private void Awake()
        {
            SetupPool();
        }

        private void SetupPool()
        {
            pool = new Stack<PooledObject>();
            PooledObject instance = null;

            for (int i = 0; i < poolSize; i++)
            {
                instance = Instantiate(objectToPool);
                instance.SetPool(this);
                instance.gameObject.SetActive(false);
                pool.Push(instance);
            }
        }

        public PooledObject GetPooledObject()
        {
            if (pool.Count == 0)
            {
                PooledObject instance = Instantiate(objectToPool);
                instance.SetPool(this);
                return instance;
            }

            PooledObject nextinstance = pool.Pop();
            nextinstance.gameObject.SetActive(true);
            return nextinstance;
        }

        public PooledObject GetPooledObject(Vector3 pos)
        {
            if (pool.Count == 0)
            {
                PooledObject instance = Instantiate(objectToPool, pos, Quaternion.identity);
                instance.SetPool(this);
                return instance;
            }

            PooledObject nextinstance = pool.Pop();
            nextinstance.transform.position = pos;
            nextinstance.gameObject.SetActive(true);
            return nextinstance;
        }

        public void ReturnToPool(PooledObject pooledObject)
        {
            if (pool.Contains(pooledObject))
            {
                Debug.LogError($"{pooledObject.name} が二重に返却されました！呼び出し元を確認してください。");
                return;
            }
            pooledObject.gameObject.SetActive(false);
            pool.Push(pooledObject);
        }
    }
}
