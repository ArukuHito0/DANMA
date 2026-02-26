using System;
using UnityEngine;

public class PlayerRuntimeStatus : CharacterRuntimeStatusBase
{
    public static PlayerRuntimeStatus Instance { get; private set; }

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        if (Instance != null) Instance = null;
    }

    public event Action<float> OnMaxHealthUpdate;

    #region 計算 / 表示 に使用するパラメータ

    public override float MaxHealth => PlayerStatusData.Instance.BaseMaxHealth + bonusMaxHealth;
    public override float Strength => PlayerStatusData.Instance.BaseStrength + bonusStrength;
    public override float AttackSpeed => PlayerStatusData.Instance.BaseAttackSpeed / (1 + (float)bonusAttackSpeed / 100f);
    public float Critical => PlayerStatusData.Instance.BaseCritical + bonusCritical;
    public override float AttackRange => PlayerStatusData.Instance.BaseAttackRange * (1 + (float)bonusAttackRange / 100f);
    public override float MoveSpeed => PlayerStatusData.Instance.BaseMoveSpeed * (1 + (float)bonusMoveSpeed / 100f);
    public float Armor => PlayerStatusData.Instance.BaseArmor + bonusArmor;
    public float CollectRange => PlayerStatusData.Instance.BaseCollectRange * (1 + (float)bonusCollectRange / 100f);
    public float DodgeChance => PlayerStatusData.Instance.BaseDodgeChance + bonusDodgeChance;
    public int Luck => PlayerStatusData.Instance.BaseLuck + bonusLuck;

    #endregion

    #region ラン中のステータス強化値

    private int bonusMaxHealth;
    private int bonusStrength;
    private int bonusAttackSpeed;
    private int bonusCritical;
    private int bonusAttackRange;
    private int bonusMoveSpeed;
    private int bonusArmor;
    private int bonusCollectRange;
    private int bonusDodgeChance;
    private int bonusLuck;

    #endregion

    #region ラン中のステータス強化値参照用プロパティ

    public int BonusMaxHealth => bonusMaxHealth;
    public int BonusStrength => bonusStrength;
    public int BonusAttackSpeed => bonusAttackSpeed;
    public int BonusCritical => bonusCritical;
    public int BonusAttackRange => bonusAttackRange;
    public int BonusMoveSpeed => bonusMoveSpeed;
    public int BonusArmor => bonusArmor;
    public int BonusCollectRange => bonusCollectRange;
    public int BonusDodgeChance => bonusDodgeChance;
    public int BonusLuck => bonusLuck;

    #endregion

    #region 各ステータス強化値の増加関数

    public void AddMaxHealth(int amount)
    {
        bonusMaxHealth.Increase(amount);
        GetComponent<HealthComponent>().Heal(amount);

        OnMaxHealthUpdate?.Invoke(GetComponent<HealthComponent>().healthRate);
    }

    public void AddStrength(int amount)
    {
       bonusStrength.Increase(amount);
    }

    public void AddAttackSpeed(int amount)
    {
        bonusAttackSpeed.Increase(amount);
    }

    public void AddCritical(int amount)
    {
        bonusCritical.Increase(amount);
    }

    public void AddAttackRange(int amount)
    {
        bonusAttackRange.Increase(amount);
    }

    public void AddMoveSpeed(int amount)
    {
        bonusMoveSpeed.Increase(amount);
    }

    public void AddArmor(int amount)
    {
        bonusArmor.Increase(amount);
    }

    public void AddCollectRange(int amount)
    {
        bonusCollectRange.Increase(amount);
    }

    public void AddDodgeChance(int amount)
    {
        bonusDodgeChance.Increase(amount);
    }

    public void AddLuck(int amount)
    {
        bonusLuck.Increase(amount);
    }

    #endregion
}
