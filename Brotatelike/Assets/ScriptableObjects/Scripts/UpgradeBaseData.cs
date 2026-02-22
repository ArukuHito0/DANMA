using System;
using UnityEngine;

public enum UpgradeRank
{
    Common,
    Rare,
    Super,
    Legend,
    Unique,
}

[CreateAssetMenu(fileName = "UpgradeData", menuName = "UpgradeData/")]
public abstract class UpgradeBaseData : ScriptableObject, IUpgrade
{
    public UpgradeRank rank;
    public string upgradeName;

    public virtual void Upgrade(PlayerController player) { }
    public virtual string GetEffectName() { return null; }
    public virtual string GetEffectValue() { return null; }

    public Color GetUpgradeColor()
    {
        switch (rank)
        {
            case UpgradeRank.Common:
                return Color.white;
            case UpgradeRank.Rare:
                return Color.skyBlue;
            case UpgradeRank.Super:
                return Color.mediumOrchid;
            case UpgradeRank.Legend:
                return Color.gold;
            case UpgradeRank.Unique:
                return Color.softRed;
            default:
                return Color.white;
        }
    }

    protected string GetEffectValueColor(float value)
    {
        return value > 0 ? "<color=green>+" : "<color=red>-";
    }

    protected string ValueToString(float value)
    {
        return GetEffectValueColor(value) + value.ToString();
    }

    protected string ValueToStringPercent(float value)
    {
        return ValueToString(value) + "%";
    }
}
