using ObjectPoolSystem;
using System;
using TMPro;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    private static ObjectPool damageTextPool;
    private CharacterRuntimeStatusBase status;

    private float currentHealth;
    public float CurrentHealth => currentHealth;
    public float healthRate
    {
        get
        {
            return (float)currentHealth / status.MaxHealth;
        }
    }

    public bool IsDead => currentHealth <= 0;

    public event Action<float> OnHealthChanged;
    public event Action OnDead;

    public void SetHealth()
    {
        if (status != null) currentHealth = status.MaxHealth;

        OnHealthChanged?.Invoke(healthRate);
    }

    private void Awake()
    {
        if (status == null) status = GetComponent<CharacterRuntimeStatusBase>();

        if (damageTextPool == null)
        {
            damageTextPool = GameObject.Find("DamageTextPool").GetComponent<ObjectPool>();
        }
    }

    public void TakeDamage(float damage)
    {
        if(IsDead) return;

        var resultDamage = damage;
        currentHealth -= resultDamage;

        damageTextPool?.GetPooledObject(transform.position)?.GetComponent<DamageText>()?.SetDamageText((int)resultDamage);

        if (IsDead)
        {
            Dead();
        }

        OnHealthChanged?.Invoke(healthRate);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, status.MaxHealth);

        OnHealthChanged?.Invoke(healthRate);
    }

    private void Dead()
    {
        OnHealthChanged?.Invoke(0);
        OnDead?.Invoke();
    }
}
