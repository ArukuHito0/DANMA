using UnityEngine;
using ObjectPoolSystem;
using System.Collections;

public class Weapon : MonoBehaviour
{
    private ObjectPool pool;

    [SerializeField] private WeaponData weaponData;

    private Coroutine activeCycle;

    private void Awake()
    {
        pool = GameObject.Find("BulletPool").GetComponent<ObjectPool>();
    }

    private void Start()
    {
        StartCycle();
    }

    public void SetWeaponData(WeaponData weaponData) => this.weaponData = weaponData;

    private void StartCycle()
    {
        if (activeCycle != null)
        {
            StopCoroutine(activeCycle);
            activeCycle = null;
        }

        activeCycle = StartCoroutine(WeaponAttackCycle());

    }

    private IEnumerator WeaponAttackCycle()
    {
        while (true)
        {
            if(weaponData.isTargetting)
                yield return new WaitUntil(() => GetTarget.GetTargetInRange(EnemyBase.enemyList, transform.position, weaponData.Range) != null);
            
            yield return Shooting();
            yield return new WaitForSeconds(weaponData.CoolTime);
        }
    }

    private IEnumerator Shooting()
    {
        if (weaponData.bulletCnt <= 0) yield break;

        var targetAngle = AngleOfBase();

        for (int i = 0; i < weaponData.bulletCnt; i++)
        {
            float angle = AngleOfBullet(i);

            float rad = RadOfBullet(targetAngle, angle);

            ShotBullet(rad);

            if (weaponData.cycleTime != 0)
                yield return new WaitForSeconds(weaponData.fireRate);
        }
    }

    private float AngleOfBase()
    {
        var target = GetTarget.GetTargetInRange(EnemyBase.enemyList, transform.position, weaponData.Range);

        if(target == null || !weaponData.isTargetting) return weaponData.baseAngle;

        return Mathf.Atan2(
            target.transform.position.y - transform.position.y,
            target.transform.position.x - transform.position.x
            ) * Mathf.Rad2Deg;
    }

    private float AngleOfBullet(int num)
    {
        if(weaponData.spreadAngle == 0 || weaponData.bulletCnt <= 1) return 0;

        if (weaponData.spreadAngle == 360.0f)
            return (weaponData.spreadAngle / (float)weaponData.bulletCnt) * num;
        else
            return -(weaponData.spreadAngle / 2f) + (weaponData.spreadAngle / (float)(weaponData.bulletCnt - 1)) * num;
    }

    private float AngleOfShot(float shotAngle, float angle)
    {
        return shotAngle + angle;
    }

    private float AngleOfError(float angle)
    {
        float rndError = Random.Range(-weaponData.dispersion, weaponData.dispersion) / 100f * Mathf.Rad2Deg;

        if(weaponData.spreadAngle != 360)
            return Mathf.Clamp(angle + rndError, -(weaponData.spreadAngle / 2f), (weaponData.spreadAngle / 2));
        else
            return angle + rndError;
    }

    private float RadOfBullet(float targetAngle, float angle)
    {
        if (weaponData.dispersion != 0)
            return AngleOfShot(targetAngle, AngleOfError(angle)) * Mathf.Deg2Rad;
        else
            return AngleOfShot(targetAngle, angle) * Mathf.Deg2Rad;
    }

    private Vector3 BulletVelocity(float rad)
    {
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * weaponData.bulletSpeed;
    }

    private void ShotBullet(float rad)
    {
        BulletController bullet = pool.GetPooledObject(transform.position).GetComponent<BulletController>();
        bullet.Initialize(weaponData, BulletVelocity(rad));
    }
}
