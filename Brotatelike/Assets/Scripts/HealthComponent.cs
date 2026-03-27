using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour, IDamageable
{
    public float currentHealth { get; private set; }
    public float maxHealth { get; private set; }
    public float healthRate
    {
        get
        {
            return currentHealth / maxHealth;
        }
    }

    public float armor {  get; private set; } = 0;
    public float dodgeChance { get; private set; } = 0;

    public bool IsDead => currentHealth <= 0;

    public event Action<float> OnHealthChanged;
    public static event Action<Vector3, int> OnDamaged;
    public static event Action<Vector3, int> OnCriticalDamaged;
    public static event Action<Vector3> OnDodgeSuccess;
    public UnityEvent OnDead;

    public void SetHealthStats(float health)
    {
        currentHealth = health;
        maxHealth = health;

        OnHealthChanged?.Invoke(healthRate);
    }

    public void SetArmor(float armor) => this.armor = armor;
    public void SetDodgeChance(float dodgeChance) => this.dodgeChance = dodgeChance * 0.01f;

    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
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
        currentHealth = (int)Mathf.Clamp(currentHealth + amount, 0, maxHealth);

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
