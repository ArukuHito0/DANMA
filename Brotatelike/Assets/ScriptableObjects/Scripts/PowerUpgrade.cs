using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpgrade", menuName = "UpgradeData/Power")]
public class PowerUpgrade : UpgradeBaseData
{
    [Header("UŒ‚—Í‚Ì‘‰Á—Ê")]
    public int addPower;

    public override void Upgrade(PlayerController player)
    {
        player.AddPower(addPower);
    }

    public override string GetEffectName()
    {
        return "UŒ‚—Í";
    }

    public override string GetEffectValue()
    {
        return ValueToString(addPower);
    }
}
