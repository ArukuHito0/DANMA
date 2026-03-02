using UnityEngine;
using ObjectPoolSystem;
using System.Collections;

public class Weapon : MonoBehaviour
{
    private ObjectPool pool;

    [SerializeField] private WeaponData weaponData;

    private void Awake()
    {
        pool = GameObject.Find("BulletPool").GetComponent<ObjectPool>();
    }

    private void Start()
    {
        StartCoroutine(WeaponAttackCycle());
    }

    private IEnumerator WeaponAttackCycle()
    {
        while (true)
        {
            yield return FireBullet();

            yield return new WaitForSeconds(weaponData.coolTime);
        }
    }

    private IEnumerator FireBullet()
    {
        if (weaponData.bulletCnt <= 0) yield break;

        var baseAngle = Mathf.Atan2(weaponData.fireDirection.y, weaponData.fireDirection.x) * Mathf.Rad2Deg;

        float fireRate = weaponData.cycleTime / weaponData.bulletCnt;

        for (int i = 0; i < weaponData.bulletCnt; i++)
        {
            var angle = (weaponData.spreadAngle / weaponData.bulletCnt) * i;

            var offset = weaponData.spreadAngle < 360.0f ? (weaponData.spreadAngle / weaponData.bulletCnt) * (weaponData.bulletCnt - 1) / 2f : 0;

            var finalAngle = baseAngle + angle - offset;

            var rad = finalAngle * Mathf.Deg2Rad;

            Vector2 velocity = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * weaponData.bulletSpeed;

            BulletController bullet = pool.GetPooledObject(transform.position).GetComponent<BulletController>();
            bullet.Initialize(weaponData, velocity);

            if(weaponData.cycleTime != 0)
                yield return new WaitForSeconds(fireRate);
        }
    }
}
