using System;
using UnityEngine;

public static class DamageCalculator
{
    public static event Action<float> OnCalculateDamage;

    public static (float damage, bool isCritical) CalculateDamage(float baseDamage, float criticalChance, float criticalMultiplier)
    {
        float damage = Mathf.Max(0, baseDamage);
        bool isCritical = false;

        if (criticalChance > 0)
        {
            if (UnityEngine.Random.value <= criticalChance * 0.01f)
            {
                damage *= criticalMultiplier;
                isCritical = true;

                SoundUtil.PlaySe("CriticalDamage");
            }
        }

        SoundUtil.PlaySe("Damage");

        OnCalculateDamage?.Invoke(damage);

        return (damage, isCritical);
    }
}
