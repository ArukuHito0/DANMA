using System.Collections.Generic;
using UnityEngine;

public class ItemInventoryVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject iteminventoryContent;
    [SerializeField] private ItemIcon productIconPrefab;

    private Dictionary<ItemData, ItemIcon> productIconDict = new Dictionary<ItemData, ItemIcon>();

    private void Start()
    {
        PlayerController.itemInventory.OnItemAdded += VisualizeInventory;
    }

    private void OnDisable()
    {
        PlayerController.itemInventory.OnItemAdded -= VisualizeInventory;
    }

    private void VisualizeInventory(ItemData data, int cnt)
    {
        if (PlayerController.itemInventory.GetItemCnt(data) == 1)
        {
            ItemIcon icon = Instantiate(productIconPrefab, iteminventoryContent.transform);

            if (!productIconDict.ContainsKey(data))
            {
                productIconDict[data] = icon;
            }

            icon.Initialize(data, cnt);
        }
        else
        {
            productIconDict[data].Initialize(data, cnt);
        }
    }
}
