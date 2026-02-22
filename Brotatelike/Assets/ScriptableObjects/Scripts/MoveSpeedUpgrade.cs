using UnityEngine;

[CreateAssetMenu(fileName = "MoveSpeedUpgrade", menuName = "UpgradeData/MoveSpeed")]
public class MoveSpeedUpgrade : UpgradeBaseData
{
    [Header("ˆÚ“®‘¬“x‚Ìã¸—Ê(%)")]
    [Range(0, 100)]
    public int addSpeed;

    public override void Upgrade(PlayerController player)
    {
        player.AddMoveSpeed(addSpeed);
    }

    public override string GetEffectName()
    {
        return "ˆÚ“®‘¬“x";
    }

    public override string GetEffectValue()
    {
        return ValueToStringPercent(addSpeed);
    }
}
