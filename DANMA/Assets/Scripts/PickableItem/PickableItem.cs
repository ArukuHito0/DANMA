using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class PickableItem : PooledObject, IPickable
{
    public static List<PickableItem> itemList = new List<PickableItem>();

    [SerializeField] protected float animTime;

    protected float time = 0f;
    protected bool isAnimation = false;
    protected Vector3 startPos;

    protected override void OnSpawn()
    {
        time = 0;
        startPos = transform.position;
        isAnimation = false;

        itemList.Add(this);
    }

    public virtual void PickUp()
    {
        itemList.Remove(this);

        if (gameObject.activeSelf)
            isAnimation = true;
    }

    private void Update()
    {
        if(isAnimation) PickupAnim();
    }

    private void PickupAnim()
    {
        time += Time.deltaTime;
        float t = time / animTime;
        t *= t;

        transform.position = Vector3.Lerp(startPos, PlayerController.Instance.transform.position, t);

        if (time > animTime || (PlayerController.Instance.transform.position - transform.position).sqrMagnitude < 0.1f)
        {
            OnPickUpItem();
            Release();
        }
    }

    protected abstract void OnPickUpItem();
}
