using UnityEngine;

[CreateAssetMenu(fileName = "WeaponIdentityData", menuName = "Weapon/WeaponIdentityData")]
public class WeaponIdentityData : ScriptableObject
{
    [Header("Šî–{î•ñ")]
    public Sprite weaponIcon;
    public string weaponName;
    public BulletBase bulletPrefab;
}
