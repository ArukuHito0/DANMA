using System;
using System.Collections.Generic;

[Serializable]
public class ItemInventory
{
    private Dictionary<ItemData, int> items = new Dictionary<ItemData, int>();

    public event Action<ItemData, int> OnItemAdded;

    // アイテムを追加する
    public void AddItem(ItemData data)
    {
        if(items.ContainsKey(data))
            items[data]++;
        else
            items.Add(data, 1);

        OnItemAdded?.Invoke(data, items[data]);
    }

    // アイテムを返却
    public void RemoveItem(ItemData data)
    {
        if(!items.ContainsKey(data)) return;

        items[data]--;

        if(items[data] <= 0) items.Remove(data);

        foreach (var item in data.Stats)
        {
            item.status.ApplyStatusUP(-item.value);
        }

        OnItemAdded?.Invoke(data, items[data]);
    }

    // インベントリをリセット
    public void ResetInventory()
    {
        items.Clear();
    }

    // 指定のアイテムの所持数を返す
    public int GetItemCnt(ItemData item) => items.TryGetValue(item, out var cnt) ? cnt : 0;

    // 所持しているアイテムの種類を全て返す
    public Dictionary<ItemData, int>.KeyCollection GetAllItems() => items.Keys;
}
