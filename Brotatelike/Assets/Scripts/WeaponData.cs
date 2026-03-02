using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatus", menuName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("ステータス")]
    public float damage;
    public float weaponMultiplier;
    public float criticalRate;
    public float criticalDamageMultiplier;
    public float range;
    public float coolTime;

    [Header("パラメータ")]
    public int bulletCnt;
    public float bulletSpeed;
    public Vector2 fireDirection;
    [Range(0, 360)] public int spreadAngle;
    [Range(0f, 1f)] public float accuray;
    public float cycleTime;
    public bool isTargetting;
}
