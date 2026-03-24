using System;
using UnityEngine;

[Serializable]
public class PlayerRuntimeStatus
{
    #region 計算に使用するパラメータ

    // メインステータス
    public float MaxHealth => PlayerController.Instance.PlayerStatus.BaseMaxHealth + bonusMaxHealth;
    public float Strength => PlayerController.Instance.PlayerStatus.BaseStrength + bonusStrength;
    public float AttackSpeed => PlayerController.Instance.PlayerStatus.BaseAttackSpeed + bonusAttackSpeed;
    public float Critical => PlayerController.Instance.PlayerStatus.BaseCritical + bonusCritical;
    public float AttackRange => PlayerController.Instance.PlayerStatus.BaseAttackRange + bonusAttackRange;
    public float MoveSpeed => PlayerController.Instance.PlayerStatus.BaseMoveSpeed * (1 + (float)bonusMoveSpeed / 100f);
    public float Armor => Mathf.Min(PlayerController.Instance.PlayerStatus.maxArmor, PlayerController.Instance.PlayerStatus.BaseArmor + bonusArmor);
    public float DodgeChance => Mathf.Min(PlayerController.Instance.PlayerStatus.maxDodgeChance, PlayerController.Instance.PlayerStatus.BaseDodgeChance + bonusDodgeChance);
    public int Luck => PlayerController.Instance.PlayerStatus.BaseLuck + bonusLuck;

    // サブステータス
    public float CollectRange => PlayerController.Instance.PlayerStatus.BaseCollectRange * (1 + (float)bonusCollectRange / 100f);
    public float GetGoldRate => PlayerController.Instance.PlayerStatus.BaseGetGoldRate + bonusGetGoldRate;
    public float GetExpRate => PlayerController.Instance.PlayerStatus.BaseGetExpRate + bonusGetExpRate;
    public float FreeRerollCnt => PlayerController.Instance.PlayerStatus.BaseFreeRerollCnt + bonusFreeRerollCnt;
    public float WaveHeal => PlayerController.Instance.PlayerStatus.BaseWaveHeal + bonusWaveHeal;
    public float WaveGetGold => PlayerController.Instance.PlayerStatus.BaseWaveGetGold + bonusWaveGetGold;
    public float AttackHeal => PlayerController.Instance.PlayerStatus.BaseAttackHeal + bonusAttackHeal;
    public float EnemySpawnRate => PlayerController.Instance.PlayerStatus.BaseEnemySpawnRate + bonusEnemySpawnRate;
    public float SaleSpawnChance => Mathf.Min(PlayerController.Instance.PlayerStatus.maxSaleSpawnChance, PlayerController.Instance.PlayerStatus.BaseSaleSpawnChance + bonusSaleSpawnChance);
    public float ItemPriceRate => Mathf.Min(PlayerController.Instance.PlayerStatus.maxItemPriceRate, PlayerController.Instance.PlayerStatus.BaseItemPriceRate + bonusItemPriceRate);
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

    public event Action OnStatusChanged;

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

        OnStatusChanged?.Invoke();
    }


    public void AddDodgeChance(int amount)
    {
        bonusDodgeChance.Increase(amount);

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

        OnStatusChanged?.Invoke();
    }
    #endregion
}
