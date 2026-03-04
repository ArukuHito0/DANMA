using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<ItemData> itemDatas;
    [SerializeField] private ItemCard[] items;

    private HashSet<ItemData> lineup = new HashSet<ItemData>();

    public event Action OnEndShopping;

    private void OnEnable()
    {
        EnemyGenerator eg = FindObjectOfType<EnemyGenerator>();
        if(eg != null)
            eg.OnEndWave += UpdateItems;
    }

    private void OnDisable()
    {
        EnemyGenerator eg = FindObjectOfType<EnemyGenerator>();
        if(eg != null)
            eg.OnEndWave -= UpdateItems;
    }

    public void UpdateItems()
    {
        foreach (ItemCard item in items)
        {
            if (item.isLocked) continue;
            else lineup.Remove(item.itemData);
        }

        List<ItemData> list = itemDatas.Where(k => !lineup.Contains(k)).OrderBy(x => Guid.NewGuid()).ToList();
        
        int idx = 0;

        foreach (ItemCard item in items)
        {
            if(item.isLocked) continue;

            lineup.Add(list[idx]);
            item.SetItemData(list[idx]);
            item.Initialize();

            idx++;
        }
    }

    public void GoToNextWave()
    {
        OnEndShopping?.Invoke();
    }
}
