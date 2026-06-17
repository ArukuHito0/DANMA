using UnityEngine;

public class NormalBullet : BulletBase
{
    protected override void OnHit()
    {
        var result = DamageCalculator.CalculateDamage(weaponData.Damage, weaponData.CriticalChance, weaponData.CriticalMultiplier);
        hitCache?.TakeDamage(result.damage, result.isCritical);
        Release();
    }
}
