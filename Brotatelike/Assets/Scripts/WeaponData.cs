using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatus", menuName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("ステータス")]
    public TierType tier;
    public float damage;
    public float weaponMultiplier;
    public float criticalRate;
    public float criticalDamageMultiplier;
    public float range;
    public float coolTime;

    [Header("パラメータ")]
    public float bulletSize;
    public int bulletCnt;
    public float bulletSpeed;
    public Vector2 fireDirection;
    [Range(0, 360)] public int spreadAngle;
    [Range(0, 100)] public int dispersion;
    public float cycleTime;
    public bool isTargetting;

    public float baseAngle => Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

    public float fireRate => cycleTime / bulletCnt;

    [ContextMenu("バースト射撃")]
    public void SetBurstShot()
    {
        spreadAngle = 0;
        cycleTime = 1;
        coolTime = 1;
    }

    [ContextMenu("拡散射撃")]
    public void SetWideShot()
    {
        spreadAngle = 120;
        cycleTime = 0;
    }

    [ContextMenu("サークル射撃")]
    public void SetCircularShot()
    {
        spreadAngle = 360;
        cycleTime = 0;
    }
}
