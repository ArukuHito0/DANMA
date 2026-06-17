using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour, IDamageable
{
    public int currentHealth { get; private set; }
    public int maxHealth { get; private set; }
    public float healthRate
    {
        get
        {
            return (float)currentHealth / (float)maxHealth;
        }
    }

    public float armor {  get; private set; } = 0;
    public float dodgeChance { get; private set; } = 0;

    public bool IsDead => currentHealth < 1;

    public event Action<float> OnHealthChanged;
    public static event Action<Vector3, int> OnDamaged;
    public static event Action<Vector3, int> OnCriticalDamaged;
    public static event Action<Vector3, int> OnHealed;
    public static event Action<Vector3> OnDodgeSuccess;
    public UnityEvent OnDead;

    private void OnEnable()
    {
        PlayerRuntimeStatus.OnDodgeChanceChanged += SetDodgeChance;
        PlayerRuntimeStatus.OnArmorChanged += SetArmor;
    }

    private void OnDisable()
    {
        PlayerRuntimeStatus.OnDodgeChanceChanged -= SetDodgeChance;
        PlayerRuntimeStatus.OnArmorChanged -= SetArmor;
    }

    public void SetHealthStats(float health)
    {
        currentHealth = (int)health;
        maxHealth = (int)health;

        OnHealthChanged?.Invoke(healthRate);
    }

    public void SetArmor(float armor) => this.armor = armor;
    public void SetDodgeChance(float dodgeChance) => this.dodgeChance = dodgeChance * 0.01f;

    public void AddMaxHealth(float amount)
    {
        maxHealth += (int)amount;

        if (maxHealth < 1) maxHealth = 1;

        Heal(amount);
    }

    public void TakeDamage(float damage, bool isCritical = false)
    {
        if (IsDead) return;

        // 回避成功でノーダメージ
        if (DodgeCheck()) return;

        RemoveHealth(damage);

        if (isCritical)
            OnCriticalDamaged?.Invoke(transform.position, (int)damage);
        else
            OnDamaged?.Invoke(transform.position, (int)damage);
    }

    public void Heal(float amount)
    {
        currentHealth += (int)amount;

        if (currentHealth > maxHealth) currentHealth = maxHealth;

        OnHealed?.Invoke(transform.position, (int)amount);
        OnHealthChanged?.Invoke(healthRate);
    }

    private void Dead()
    {
        SoundUtil.PlaySe("Defeat");

        OnHealthChanged?.Invoke(0);
        OnDead?.Invoke();
    }

    private void RemoveHealth(float amount)
    {
        currentHealth -= (int)amount;

        if (IsDead)
        {
            Dead();
        }

        OnHealthChanged?.Invoke(healthRate);
    }

    private bool DodgeCheck()
    {
        if (UnityEngine.Random.value <= dodgeChance)
        {
            OnDodgeSuccess?.Invoke(transform.position);
            return true;
        }
        else
        {
            return false;
        }
    }
}
