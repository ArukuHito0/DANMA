using UnityEngine;
using System;

public enum PlayerStatus
{
    MaxHealth,
    Strength,
    AttackSpeed,
    Critical,
    AttackRange,
    MoveSpeed,
    Armor,
    CollectRange,
    DodgeChance,
    Luck,
}

public static class StatusExtensions
{
    public static void ApplyStatusUP(this PlayerStatus status, int amount) => status.GetIncreaseMethod()?.Invoke(amount);

    public static Action<int> GetIncreaseMethod(this PlayerStatus status) => status switch
    {
        PlayerStatus.MaxHealth => PlayerController.Instance.playerRuntimeStatus.AddMaxHealth,
        PlayerStatus.Strength => PlayerController.Instance.playerRuntimeStatus.AddStrength,
        PlayerStatus.AttackSpeed => PlayerController.Instance.playerRuntimeStatus.AddAttackSpeed,
        PlayerStatus.Critical => PlayerController.Instance.playerRuntimeStatus.AddCritical,
        PlayerStatus.AttackRange => PlayerController.Instance.playerRuntimeStatus.AddAttackRange,
        PlayerStatus.MoveSpeed => PlayerController.Instance.playerRuntimeStatus.AddMoveSpeed,
        PlayerStatus.Armor => PlayerController.Instance.playerRuntimeStatus.AddArmor,
        PlayerStatus.CollectRange => PlayerController.Instance.playerRuntimeStatus.AddCollectRange,
        PlayerStatus.DodgeChance => PlayerController.Instance.playerRuntimeStatus.AddDodgeChance,
        PlayerStatus.Luck => PlayerController.Instance.playerRuntimeStatus.AddLuck,
        _ => null,
    };

    public static string GetPlayerStatusName(this PlayerStatus status) => status switch
    {
        PlayerStatus.MaxHealth => "Å‘åHP",
        PlayerStatus.Strength => "UŒ‚—Í",
        PlayerStatus.AttackSpeed => "UŒ‚‘¬“x(%)",
        PlayerStatus.Critical => "¸ØÃ¨¶Ù—¦(%)",
        PlayerStatus.AttackRange => "ŽË’ö",
        PlayerStatus.MoveSpeed => "ˆÚ“®‘¬“x(%)",
        PlayerStatus.Armor => "ƒA[ƒ}[",
        PlayerStatus.CollectRange => "‰ñŽû”ÍˆÍ",
        PlayerStatus.DodgeChance => "‰ñ”ð—¦(%)",
        PlayerStatus.Luck => "‰^",
        _ => null,
    };

    public static float GetRuntimeStatus(this PlayerStatus status) => status switch
    {
        PlayerStatus.MaxHealth => PlayerController.Instance.playerRuntimeStatus.MaxHealth,
        PlayerStatus.Strength => PlayerController.Instance.playerRuntimeStatus.Strength,
        PlayerStatus.AttackSpeed => PlayerController.Instance.playerRuntimeStatus.AttackSpeed,
        PlayerStatus.Critical => PlayerController.Instance.playerRuntimeStatus.Critical,
        PlayerStatus.AttackRange => PlayerController.Instance.playerRuntimeStatus.AttackRange,
        PlayerStatus.MoveSpeed => PlayerController.Instance.playerRuntimeStatus.MoveSpeed,
        PlayerStatus.Armor => PlayerController.Instance.playerRuntimeStatus.Armor,
        PlayerStatus.CollectRange => PlayerController.Instance.playerRuntimeStatus.CollectRange,
        PlayerStatus.DodgeChance => PlayerController.Instance.playerRuntimeStatus.DodgeChance,
        PlayerStatus.Luck => PlayerController.Instance.playerRuntimeStatus.Luck,
        _ => -1,
    };
}
