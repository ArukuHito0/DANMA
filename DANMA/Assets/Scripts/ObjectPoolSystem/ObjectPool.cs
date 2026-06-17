using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public ObjectPool(PooledObject prefab, int size, Transform container)
    {
        objectToPool = prefab;
        poolSize = (uint)size;
        this.container = container;

        SetupPool();
    }

    private uint poolSize;
    private PooledObject objectToPool;
    private Stack<PooledObject> stack;
    private Transform container;

    private void SetupPool()
    {
        stack = new Stack<PooledObject>();

        for (int i = 0; i < poolSize; i++)
        {
            stack.Push(CreateInstance());
        }
    }

    // プール対象のオブジェクトを取得
    public PooledObject GetPooledObject()
    {
        PooledObject instance = stack.Count > 0 ? stack.Pop() : CreateInstance();
        instance.gameObject.SetActive(true);
        return instance;
    }

    // プール対象のオブジェクトを取得(座標指定)
    public PooledObject GetPooledObject(Vector3 pos)
    {
        PooledObject instance = stack.Count > 0 ? stack.Pop() : CreateInstance();
        instance.transform.position = pos;
        instance.gameObject.SetActive(true);
        return instance;
    }

    // プールへの返却処理
    public void ReturnToPool(PooledObject pooledObject)
    {
        if (stack.Contains(pooledObject))
        {
            Debug.LogError($"{pooledObject.name} が二重に返却されました！呼び出し元を確認してください。");
            return;
        }
        pooledObject.gameObject.SetActive(false);
        stack.Push(pooledObject);
    }

    // プール対象のオブジェクトのインスタンスを生成
    private PooledObject CreateInstance()
    {
        PooledObject instance = Object.Instantiate(objectToPool, container);
        instance.SetPool(this);
        instance.gameObject.SetActive(false);
        return instance;
    }
}
