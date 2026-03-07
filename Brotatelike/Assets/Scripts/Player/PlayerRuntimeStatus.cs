using System;
using UnityEngine;

[Serializable]
public class PlayerRuntimeStatus
{
    #region 計算に使用するパラメータ

    public float MaxHealth => PlayerController.Instance.PlayerStatus.BaseMaxHealth + bonusMaxHealth;
    public float Strength => PlayerController.Instance.PlayerStatus.BaseStrength + bonusStrength;
    public float AttackSpeed => PlayerController.Instance.PlayerStatus.BaseAttackSpeed + bonusAttackSpeed;
    public float Critical => PlayerController.Instance.PlayerStatus.BaseCritical + bonusCritical;
    public float AttackRange => PlayerController.Instance.PlayerStatus.BaseAttackRange + bonusAttackRange;
    public float MoveSpeed => PlayerController.Instance.PlayerStatus.BaseMoveSpeed * (1 + (float)bonusMoveSpeed / 100f);
    public float Armor => PlayerController.Instance.PlayerStatus.BaseArmor + bonusArmor;
    public float CollectRange => PlayerController.Instance.PlayerStatus.BaseCollectRange * (1 + (float)bonusCollectRange / 100f);
    public float DodgeChance => PlayerController.Instance.PlayerStatus.BaseDodgeChance + bonusDodgeChance;
    public int Luck => PlayerController.Instance.PlayerStatus.BaseLuck + bonusLuck;

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

    public event Action OnStatusChanged;

    #region 各ステータス強化値の増加関数

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

    public void AddCollectRange(int amount)
    {
        bonusCollectRange.Increase(amount);

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

    #endregion
}
