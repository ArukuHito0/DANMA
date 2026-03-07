using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData")]
public class ItemData : ProductBaseData
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
    [SerializeField] private uint itemPrice;
    public UpgradeStats[] stats;

    #region ProductBaseData

    public override TierType Tier => itemTier;
    public override Sprite Icon => itemIcon;
    public override string Name => itemName;
    public override uint Price => itemPrice;

    public override void PayProduct()
    {
        foreach (var item in stats)
        {
            item.status.ApplyStatusUP(item.value);
        }
    }

    public override string GetDescriptionText()
    {
        var s = string.Empty;

        for (int i = 0; i < stats.Length; i++)
        {
            s += stats[i].status.GetPlayerStatusName();
            s += "  " + ValueToString(stats[i].value);

            if (i != stats.Length - 1) s += "\n";
        }

        return s;
    }

    #endregion

    public string ValueToString(float value)
    {
        return value.GetValueColorText() + "</color>";
    }
}
