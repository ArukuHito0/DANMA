using System;
using UnityEngine;

public static class DamageCalculator
{
    public static event Action<float> OnCalculateDamage;

    public static (float damage, bool isCritical) CalculateDamage(float baseDamage, float criticalChance, float criticalMultiplier)
    {
        float damage = baseDamage;
        bool isCritical = false;

        if (criticalChance > 0)
        {
            if (UnityEngine.Random.value <= criticalChance)
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
