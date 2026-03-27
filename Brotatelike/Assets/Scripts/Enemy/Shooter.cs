using System.Collections;
using UnityEngine;

public class Shooter : EnemyBase
{
    [SerializeField] private WeaponData weaponData;

    private Weapon weapon;

    public void StopAttack()
    {
        weapon.StopAttack(this);
    }

    protected override void OnStart()
    {
        weapon = new Weapon(gameObject);
        weapon.SetWeaponData(weaponData);

        weapon.StartAttack(this);
    }

    protected override void Attack()
    {
        if (Time.time - beforeAttacktime > enemyStatus.AttackSpeed)
        {
            PlayerController.Instance.HealthComponent.TakeDamage(enemyStatus.Strength);
            beforeAttacktime = Time.time;
        }
    }

    protected override void Move()
    {
        rb.linearVelocity = (PlayerController.Instance.transform.position - transform.position).normalized * enemyStatus.MoveSpeed;
    }
}
