using ObjectPoolSystem;
using UnityEngine;

// プール対象のオブジェクト
public abstract class PooledObject : MonoBehaviour
{
    private ObjectPool pool;

    protected bool isReleased = false;

    protected void OnEnable()
    {
        isReleased = false;

        OnSpawn();
    }

    // OnEnableと同じタイミングで呼ばれる
    protected virtual void OnSpawn() { }

    public void SetPool(ObjectPool pool)
    {
        this.pool = pool;
    }

    // プールにオブジェクトを返却する
    protected void Release()
    {
        if(isReleased) return;

        isReleased = true;
        pool.ReturnToPool(this);
    }
}
