using System;
using UnityEngine;

[Serializable]
public class PlayerRuntimeStatus
{
    #region 計算に使用するパラメータ

    // メインステータス
    public float MaxHealth => PlayerStatus.MaxHealth.GetBaseStatus() + bonusMaxHealth;
    public float Strength =>PlayerStatus.Strength.GetBaseStatus() + bonusStrength;
    public float AttackSpeed => PlayerStatus.AttackSpeed.GetBaseStatus() + bonusAttackSpeed;
    public float Critical => PlayerStatus.Critical.GetBaseStatus() + bonusCritical;
    public float AttackRange => PlayerStatus.AttackRange.GetBaseStatus() + bonusAttackRange;
    public float MoveSpeed => PlayerStatus.MoveSpeed.GetBaseStatus() * (1 + (float)bonusMoveSpeed / 100f);
    public float Armor => Mathf.Min(PlayerController.Instance.PlayerStatus.maxArmor, PlayerStatus.Armor.GetBaseStatus() + bonusArmor);
    public float DodgeChance => Mathf.Min(PlayerController.Instance.PlayerStatus.maxDodgeChance, PlayerStatus.DodgeChance.GetBaseStatus() + bonusDodgeChance);
    public int Luck => (int)PlayerStatus.Luck.GetBaseStatus() + bonusLuck;

    // サブステータス
    public float CollectRange => PlayerStatus.CollectRange.GetBaseStatus() * (1 + (float)bonusCollectRange / 100f);
    public float GetGoldRate => PlayerStatus.GetGoldRate.GetBaseStatus() + bonusGetGoldRate;
    public float GetExpRate => PlayerStatus.GetExpRate.GetBaseStatus() + bonusGetExpRate;
    public float FreeRerollCnt => PlayerStatus.FreeRerollCnt.GetBaseStatus() + bonusFreeRerollCnt;
    public float WaveHeal => PlayerStatus.WaveHeal.GetBaseStatus() + bonusWaveHeal;
    public float WaveGetGold => PlayerStatus.WaveGetGold.GetBaseStatus() + bonusWaveGetGold;
    public float AttackHeal => PlayerStatus.AttackHeal.GetBaseStatus() + bonusAttackHeal;
    public float EnemySpawnRate => PlayerStatus.EnemySpawnRate.GetBaseStatus() + bonusEnemySpawnRate;
    public float SaleSpawnChance => Mathf.Min(PlayerController.Instance.PlayerStatus.maxSaleSpawnChance, PlayerStatus.SaleSpawnChance.GetBaseStatus() + bonusSaleSpawnChance);
    public float ItemPriceRate => Mathf.Min(PlayerController.Instance.PlayerStatus.maxItemPriceRate, PlayerStatus.ItemPriceRate.GetBaseStatus() + bonusItemPriceRate);
    #endregion

    #region ラン中のステータス強化値

    // メインステータス
    private int bonusMaxHealth;
    private int bonusStrength;
    private int bonusAttackSpeed;
    private int bonusCritical;
    private int bonusAttackRange;
    private int bonusMoveSpeed;
    private int bonusArmor;
    private int bonusDodgeChance;
    private int bonusLuck;

    // サブステータス
    private int bonusCollectRange;
    private int bonusGetGoldRate;
    private int bonusGetExpRate;
    private int bonusFreeRerollCnt;
    private int bonusWaveHeal;
    private int bonusWaveGetGold;
    private int bonusAttackHeal;
    private int bonusEnemySpawnRate;
    private int bonusSaleSpawnChance;
    private int bonusItemPriceRate;

    #endregion

    #region ラン中のステータス強化値参照用プロパティ

    // メインステータス
    public int BonusMaxHealth => bonusMaxHealth;
    public int BonusStrength => bonusStrength;
    public int BonusAttackSpeed => bonusAttackSpeed;
    public int BonusCritical => bonusCritical;
    public int BonusAttackRange => bonusAttackRange;
    public int BonusMoveSpeed => bonusMoveSpeed;
    public int BonusArmor => bonusArmor;
    public int BonusDodgeChance => bonusDodgeChance;
    public int BonusLuck => bonusLuck;

    // サブステータス
    public int BonusCollectRange => bonusCollectRange;
    public int BonusGetGoldRate => bonusGetGoldRate;
    public int BonusGetExpRate => bonusGetExpRate;
    public int BonusFreeRerollCnt => bonusFreeRerollCnt;
    public int BonusWaveHeal => bonusWaveHeal;
    public int BonusWaveGetGold => bonusWaveGetGold;
    public int BonusAttackHeal => bonusAttackHeal;
    public int BonusEnemySpawnRate => bonusEnemySpawnRate;
    public int BonusSaleSpawnChance => bonusSaleSpawnChance;
    public int BonusItemPriceRate => bonusItemPriceRate;

    #endregion

    public static event Action OnStatusChanged;
    public static event Action<float> OnDodgeChanceChanged;
    public static event Action<float> OnArmorChanged;
    public static event Action OnItemPriceRateChanged;

    #region 各ステータス強化値の増加関数

    // メインステータス
    public void AddMaxHealth(int amount)
    {
        bonusMaxHealth.Increase(amount);
        PlayerController.Instance.HealthComponent.AddMaxHealth(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddStrength(int amount)
    {
        bonusStrength.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddAttackSpeed(int amount)
    {
        bonusAttackSpeed.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddCritical(int amount)
    {
        bonusCritical.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddAttackRange(int amount)
    {
        bonusAttackRange.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddMoveSpeed(int amount)
    {
        bonusMoveSpeed.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddArmor(int amount)
    {
        bonusArmor.Increase(amount);

        OnArmorChanged?.Invoke(Armor);
        OnStatusChanged?.Invoke();
    }

    public void AddDodgeChance(int amount)
    {
        bonusDodgeChance.Increase(amount);

        OnDodgeChanceChanged?.Invoke(DodgeChance);
        OnStatusChanged?.Invoke();
    }

    public void AddLuck(int amount)
    {
        bonusLuck.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    // サブステータス
    public void AddCollectRange(int amount)
    {
        bonusCollectRange.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddGetGoldRate(int amount)
    {
        bonusGetGoldRate.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddGetExpRate(int amount)
    {
        bonusGetExpRate.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddFreeRerollCnt(int amount)
    {
        bonusFreeRerollCnt.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddWaveHeal(int amount)
    {
        bonusWaveHeal.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddWaveGetGold(int amount)
    {
        bonusWaveGetGold.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddAttackHeal(int amount)
    {
        bonusAttackHeal.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddEnemySpawnRate(int amount)
    {
        bonusEnemySpawnRate.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddSaleSpawnChance(int amount)
    {
        bonusSaleSpawnChance.Increase(amount);

        OnStatusChanged?.Invoke();
    }

    public void AddItemPriceRate(int amount)
    {
        bonusItemPriceRate.Increase(amount);

        OnItemPriceRateChanged?.Invoke();
        OnStatusChanged?.Invoke();
    }
    #endregion
}
