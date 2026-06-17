using UnityEngine;
using System.Collections.Generic;

public class PickableItemManager : MonoBehaviour
{
    private List<PickableItem> nearItemList = new List<PickableItem>();

    private void LateUpdate()
    {
        if(PlayerController.Instance == null || PickableItem.itemList == null || PickableItem.itemList.Count <= 0) return;

        GetTarget.GetAllTargetinRange(PickableItem.itemList, nearItemList, PlayerController.Instance.transform.position, PlayerStatus.CollectRange.GetRuntimeStatus());
        if (nearItemList != null)
        {
            for (int i = 0; i < nearItemList.Count; i++)
            {
                nearItemList[i].PickUp();
            }
        }
    }

    public void PickAllPickableItem()
    {
        if (PickableItem.itemList != null)
        {
            for (int i = PickableItem.itemList.Count - 1; i >= 0; i--)
                PickableItem.itemList[i].PickUp();
        }

        PickableItem.itemList.Clear();
    }

    public void ReleaseAllPickableItem()
    {
        if (PickableItem.itemList != null)
        {
            for (int i = PickableItem.itemList.Count - 1; i >= 0; i--)
                PickableItem.itemList[i].Release();
        }

        PickableItem.itemList.Clear();
    }
}
