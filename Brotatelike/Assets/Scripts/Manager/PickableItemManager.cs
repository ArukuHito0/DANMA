using UnityEngine;
using System.Collections.Generic;

public class PickableItemManager : MonoBehaviour
{
    private List<PickableItem> nearItemList = new List<PickableItem>();

    private void LateUpdate()
    {
        if(PlayerController.Instance == null || PickableItem.itemList == null || PickableItem.itemList.Count <= 0) return;

        GetTarget.GetAllTargetinRange(PickableItem.itemList, nearItemList, PlayerController.Instance.transform.position, PlayerController.Instance.playerRuntimeStatus.CollectRange);
        if (nearItemList != null)
        {
            for (int i = 0; i < nearItemList.Count; i++)
            {
                nearItemList[i].PickUp(PlayerController.Instance);
            }
        }
    }
}
