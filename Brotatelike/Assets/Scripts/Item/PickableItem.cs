using UnityEngine;
using System.Collections.Generic;

public abstract class PickableItem : PooledObject, IPickable
{
    public static List<PickableItem> itemList = new List<PickableItem>();

    protected override void OnSpawn()
    {
        itemList.Add(this);
    }

    public virtual void PickUp(PlayerController player) { itemList.Remove(this); }
}
