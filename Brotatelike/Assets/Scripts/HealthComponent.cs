using System;
using TMPro;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    private float currentHealth;
    public float CurrentHealth => currentHealth;
    [SerializeField] private float maxHealth;
    public float MaxHealth => maxHealth;

    public float healthRate
    {
        get
        {
            return (float)currentHealth / maxHealth;
        }
    }
    [SerializeField] private float defence;

    public bool IsDead => currentHealth <= 0;

    public event Action<float> OnHealthChanged;
    public event Action OnDead;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Awake()
    {
        currentHealth = maxHealth;

        OnHealthChanged?.Invoke(healthRate);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (int)((damage * (1 - (defence / 100))));

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

    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth += amount;

        OnHealthChanged?.Invoke(healthRate);
    }

    public void RemoveMaxHealth(float amount)
    {
        maxHealth -= amount;
        currentHealth -= amount;

        OnHealthChanged?.Invoke(healthRate);
    }
}
