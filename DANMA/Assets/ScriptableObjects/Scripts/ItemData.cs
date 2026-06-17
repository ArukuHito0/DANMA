using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject, IProduct
{
    [Serializable]
    public struct UpgradeStats
    {
        public PlayerStatus status;
        public int value;
    }

    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string itemName;
    [SerializeField] private TierType itemTier;
    [SerializeField] private int itemPrice;
    [SerializeField] private UpgradeStats[] stats;

    public UpgradeStats[] Stats => stats;

    #region IProductのプロパティ
    public TierType Tier => itemTier;
    public  Sprite Icon => itemIcon;
    public  string Name => itemName;
    public  int Price => itemPrice;

    public void PayProduct()
    {
        foreach (var item in Stats)
        {
            item.status.ApplyStatusUP(item.value);
        }

        PlayerController.itemInventory.AddItem(this);
    }

    public string GetDescriptionText()
    {
        var s = string.Empty;

        for (int i = 0; i < Stats.Length; i++)
        {
            s += Stats[i].status.GetPlayerStatusName();
            s += "  " + Stats[i].value.ToColorText();

            if (i != Stats.Length - 1) s += "\n";
        }

        return s;
    }
    #endregion
}
