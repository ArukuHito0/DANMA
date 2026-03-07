using ObjectPoolSystem;
using System;
using TMPro;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    public float currentHealth { get; private set; }
    public float maxHealth { get; private set; }
    public float healthRate
    {
        get
        {
            return (float)currentHealth / maxHealth;
        }
    }

    public bool IsDead => currentHealth <= 0;

    public event Action<float> OnHealthChanged;
    public static event Action<Vector3, int> OnDamaged;
    public event Action OnDead;

    public void SetHealth(float health)
    {
        currentHealth = health;
        maxHealth = health;

        OnHealthChanged?.Invoke(healthRate);
    }

    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
        Heal(amount);
    }

    public void TakeDamage(float damage)
    {
        if(IsDead) return;

        var resultDamage = damage;
        currentHealth -= resultDamage;

        OnDamaged?.Invoke(transform.position, (int)damage);

        if (IsDead)
        {
            Dead();
        }

        OnHealthChanged?.Invoke(healthRate);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        OnHealthChanged?.Invoke(healthRate);
    }

    private void Dead()
    {
        OnHealthChanged?.Invoke(0);
        OnDead?.Invoke();
    }
}
