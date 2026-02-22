using UnityEngine;

[CreateAssetMenu(fileName = "MaxHealthUpgrade", menuName = "UpgradeData/MaxHealth")]
public class MaxHealthUpgrade : UpgradeBaseData
{
    [Header("Å‘åHP‚Ì‘‰Á—Ê")]
    public int addMaxHealth;

    public override void Upgrade(PlayerController player)
    {
        player.HealthComponent.AddMaxHealth(addMaxHealth);
    }

    public override string GetEffectName()
    {
        return "HP‚ÌÅ‘å’l";
    }

    public override string GetEffectValue()
    {
        return ValueToString(addMaxHealth);
    }
}
