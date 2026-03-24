using UnityEngine;
using System;

public enum PlayerStatus
{
    // メインステータス
    MaxHealth,
    Strength,
    AttackSpeed,
    Critical,
    AttackRange,
    MoveSpeed,
    Armor,
    DodgeChance,
    Luck,

    // サブステータス
    CollectRange,
    GetGoldRate,
    GetExpRate,
    FreeRerollCnt,
    WaveHeal,
    WaveGetGold,
    AttackHeal,
    EnemySpawnRate,
    SaleSpawnChance,
    ItemPriceRate,
}

public static class StatusExtensions
{
    public static void ApplyStatusUP(this PlayerStatus status, int amount) => status.GetIncreaseMethod()?.Invoke(amount);

    public static Action<int> GetIncreaseMethod(this PlayerStatus status) => status switch
    {
        // メインステータス
        PlayerStatus.MaxHealth => PlayerController.Instance.playerRuntimeStatus.AddMaxHealth,
        PlayerStatus.Strength => PlayerController.Instance.playerRuntimeStatus.AddStrength,
        PlayerStatus.AttackSpeed => PlayerController.Instance.playerRuntimeStatus.AddAttackSpeed,
        PlayerStatus.Critical => PlayerController.Instance.playerRuntimeStatus.AddCritical,
        PlayerStatus.AttackRange => PlayerController.Instance.playerRuntimeStatus.AddAttackRange,
        PlayerStatus.MoveSpeed => PlayerController.Instance.playerRuntimeStatus.AddMoveSpeed,
        PlayerStatus.Armor => PlayerController.Instance.playerRuntimeStatus.AddArmor,
        PlayerStatus.DodgeChance => PlayerController.Instance.playerRuntimeStatus.AddDodgeChance,
        PlayerStatus.Luck => PlayerController.Instance.playerRuntimeStatus.AddLuck,

        // サブステータス
        PlayerStatus.CollectRange => PlayerController.Instance.playerRuntimeStatus.AddCollectRange,
        PlayerStatus.GetGoldRate => PlayerController.Instance.playerRuntimeStatus.AddGetGoldRate,
        PlayerStatus.GetExpRate => PlayerController.Instance.playerRuntimeStatus.AddGetExpRate,
        PlayerStatus.FreeRerollCnt => PlayerController.Instance.playerRuntimeStatus.AddFreeRerollCnt,
        PlayerStatus.WaveHeal => PlayerController.Instance.playerRuntimeStatus.AddWaveHeal,
        PlayerStatus.WaveGetGold => PlayerController.Instance.playerRuntimeStatus.AddWaveGetGold,
        PlayerStatus.AttackHeal => PlayerController.Instance.playerRuntimeStatus.AddAttackHeal,
        PlayerStatus.EnemySpawnRate => PlayerController.Instance.playerRuntimeStatus.AddEnemySpawnRate,
        PlayerStatus.SaleSpawnChance => PlayerController.Instance.playerRuntimeStatus.AddSaleSpawnChance,
        PlayerStatus.ItemPriceRate => PlayerController.Instance.playerRuntimeStatus.AddItemPriceRate,
        _ => null,
    };

    public static string GetPlayerStatusName(this PlayerStatus status) => status switch
    {
        // メインステータス
        PlayerStatus.MaxHealth => "最大HP",
        PlayerStatus.Strength => "攻撃力",
        PlayerStatus.AttackSpeed => "攻撃速度(%)",
        PlayerStatus.Critical => "ｸﾘﾃｨｶﾙ率(%)",
        PlayerStatus.AttackRange => "射程(m)",
        PlayerStatus.MoveSpeed => "移動速度(%)",
        PlayerStatus.Armor => "アーマー",
        PlayerStatus.DodgeChance => "回避率(%)",
        PlayerStatus.Luck => "運",
        
        // サブステータス
        PlayerStatus.CollectRange => "回収範囲増加(%)",
        PlayerStatus.GetGoldRate => "取得ゴールド増加(%)",
        PlayerStatus.GetExpRate => "取得EXP増加(%)",
        PlayerStatus.FreeRerollCnt => "無料リロール(回)",
        PlayerStatus.WaveHeal => "WAVE終了時回復(%)",
        PlayerStatus.WaveGetGold => "WAVE終了時獲得ゴールド",
        PlayerStatus.AttackHeal => "ライフスティール発生率(%)",
        PlayerStatus.EnemySpawnRate => "敵の出現量(%)",
        PlayerStatus.SaleSpawnChance => "セール出現率(%)",
        PlayerStatus.ItemPriceRate => "アイテム割引価格(%)",
        _ => null,
    };

    public static float GetBaseStatus(this PlayerStatus status) => status switch
    {
        // メインステータス
        PlayerStatus.MaxHealth => PlayerController.Instance.PlayerStatus.BaseMaxHealth,
        PlayerStatus.Strength => PlayerController.Instance.PlayerStatus.BaseStrength,
        PlayerStatus.AttackSpeed => PlayerController.Instance.PlayerStatus.BaseAttackSpeed,
        PlayerStatus.Critical => PlayerController.Instance.PlayerStatus.BaseCritical,
        PlayerStatus.AttackRange => PlayerController.Instance.PlayerStatus.BaseAttackRange,
        PlayerStatus.MoveSpeed => PlayerController.Instance.PlayerStatus.BaseMoveSpeed,
        PlayerStatus.Armor => PlayerController.Instance.PlayerStatus.BaseArmor,
        PlayerStatus.DodgeChance => PlayerController.Instance.PlayerStatus.BaseDodgeChance,
        PlayerStatus.Luck => PlayerController.Instance.PlayerStatus.BaseLuck,

        // サブステータス
        PlayerStatus.CollectRange => PlayerController.Instance.PlayerStatus.BaseCollectRange,
        PlayerStatus.GetGoldRate => PlayerController.Instance.PlayerStatus.BaseGetGoldRate,
        PlayerStatus.GetExpRate => PlayerController.Instance.PlayerStatus.BaseGetExpRate,
        PlayerStatus.FreeRerollCnt => PlayerController.Instance.PlayerStatus.BaseFreeRerollCnt,
        PlayerStatus.WaveHeal => PlayerController.Instance.PlayerStatus.BaseWaveHeal,
        PlayerStatus.WaveGetGold => PlayerController.Instance.PlayerStatus.BaseWaveGetGold,
        PlayerStatus.AttackHeal => PlayerController.Instance.PlayerStatus.BaseAttackHeal,
        PlayerStatus.EnemySpawnRate => PlayerController.Instance.PlayerStatus.BaseEnemySpawnRate,
        PlayerStatus.SaleSpawnChance => PlayerController.Instance.PlayerStatus.BaseSaleSpawnChance,
        PlayerStatus.ItemPriceRate => PlayerController.Instance.PlayerStatus.BaseItemPriceRate,
        _ => -1,
    };

    public static float GetBonusStatus(this PlayerStatus status) => status switch
    {
        // メインステータス
        PlayerStatus.MaxHealth => PlayerController.Instance.playerRuntimeStatus.BonusMaxHealth,
        PlayerStatus.Strength => PlayerController.Instance.playerRuntimeStatus.BonusStrength,
        PlayerStatus.AttackSpeed => PlayerController.Instance.playerRuntimeStatus.BonusAttackSpeed,
        PlayerStatus.Critical => PlayerController.Instance.playerRuntimeStatus.BonusCritical,
        PlayerStatus.AttackRange => PlayerController.Instance.playerRuntimeStatus.BonusAttackRange,
        PlayerStatus.MoveSpeed => PlayerController.Instance.playerRuntimeStatus.BonusMoveSpeed,
        PlayerStatus.Armor => PlayerController.Instance.playerRuntimeStatus.BonusArmor,
        PlayerStatus.DodgeChance => PlayerController.Instance.playerRuntimeStatus.BonusDodgeChance,
        PlayerStatus.Luck => PlayerController.Instance.playerRuntimeStatus.BonusLuck,

        // サブステータス
        PlayerStatus.CollectRange => PlayerController.Instance.playerRuntimeStatus.BonusCollectRange,
        PlayerStatus.GetGoldRate => PlayerController.Instance.playerRuntimeStatus.BonusGetGoldRate,
        PlayerStatus.GetExpRate => PlayerController.Instance.playerRuntimeStatus.BonusGetExpRate,
        PlayerStatus.FreeRerollCnt => PlayerController.Instance.playerRuntimeStatus.BonusFreeRerollCnt,
        PlayerStatus.WaveHeal => PlayerController.Instance.playerRuntimeStatus.BonusWaveHeal,
        PlayerStatus.WaveGetGold => PlayerController.Instance.playerRuntimeStatus.BonusWaveGetGold,
        PlayerStatus.AttackHeal => PlayerController.Instance.playerRuntimeStatus.BonusAttackHeal,
        PlayerStatus.EnemySpawnRate => PlayerController.Instance.playerRuntimeStatus.BonusEnemySpawnRate,
        PlayerStatus.SaleSpawnChance => PlayerController.Instance.playerRuntimeStatus.BonusSaleSpawnChance,
        PlayerStatus.ItemPriceRate => PlayerController.Instance.playerRuntimeStatus.BonusItemPriceRate,
        _ => -1,
    };

    public static float GetRuntimeStatus(this PlayerStatus status) => status switch
    {
        // メインステータス
        PlayerStatus.MaxHealth => PlayerController.Instance.playerRuntimeStatus.MaxHealth,
        PlayerStatus.Strength => PlayerController.Instance.playerRuntimeStatus.Strength,
        PlayerStatus.AttackSpeed => PlayerController.Instance.playerRuntimeStatus.AttackSpeed,
        PlayerStatus.Critical => PlayerController.Instance.playerRuntimeStatus.Critical,
        PlayerStatus.AttackRange => PlayerController.Instance.playerRuntimeStatus.AttackRange,
        PlayerStatus.MoveSpeed => PlayerController.Instance.playerRuntimeStatus.MoveSpeed,
        PlayerStatus.Armor => PlayerController.Instance.playerRuntimeStatus.Armor,
        PlayerStatus.DodgeChance => PlayerController.Instance.playerRuntimeStatus.DodgeChance,
        PlayerStatus.Luck => PlayerController.Instance.playerRuntimeStatus.Luck,

        // サブステータス
        PlayerStatus.CollectRange => PlayerController.Instance.playerRuntimeStatus.CollectRange,
        PlayerStatus.GetGoldRate => PlayerController.Instance.playerRuntimeStatus.GetGoldRate,
        PlayerStatus.GetExpRate => PlayerController.Instance.playerRuntimeStatus.GetExpRate,
        PlayerStatus.FreeRerollCnt => PlayerController.Instance.playerRuntimeStatus.FreeRerollCnt,
        PlayerStatus.WaveHeal => PlayerController.Instance.playerRuntimeStatus.WaveHeal,
        PlayerStatus.WaveGetGold => PlayerController.Instance.playerRuntimeStatus.WaveGetGold,
        PlayerStatus.AttackHeal => PlayerController.Instance.playerRuntimeStatus.AttackHeal,
        PlayerStatus.EnemySpawnRate => PlayerController.Instance.playerRuntimeStatus.EnemySpawnRate,
        PlayerStatus.SaleSpawnChance => PlayerController.Instance.playerRuntimeStatus.SaleSpawnChance,
        PlayerStatus.ItemPriceRate => PlayerController.Instance.playerRuntimeStatus.ItemPriceRate,
        _ => -1,
    };
}
