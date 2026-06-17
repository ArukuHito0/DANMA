using System.Data;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage, bool isCritical = false);
}
