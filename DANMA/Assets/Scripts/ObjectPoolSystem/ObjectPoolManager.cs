using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    private Dictionary<PooledObject, ObjectPool> poolDict = new Dictionary<PooledObject, ObjectPool>();

    private void OnEnable()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    private void OnDisable()
    {
        Instance = null;
    }

    // 指定したプレハブのインスタンスをそのプレハブのプールから取得する
    public T GetPooledObject<T>(T target) where T : PooledObject
    {
        if (!poolDict.ContainsKey(target))
        {
            ObjectPool pool = new ObjectPool(target, 10, this.transform);
            poolDict.Add(target, pool);
        }

        return (T)poolDict[target].GetPooledObject();
    }

    // 指定したプレハブのインスタンスをそのプレハブのプールから取得する(座標指定)
    public T GetPooledObject<T>(T target, Vector3 pos) where T : PooledObject
    {
        if (!poolDict.ContainsKey(target))
        {
            ObjectPool pool = new ObjectPool(target, 10, this.transform);
            poolDict.Add(target, pool);
        }

        return (T)poolDict[target].GetPooledObject(pos);
    }
}
