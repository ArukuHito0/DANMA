using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatus", menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject, IProduct
{
    [Serializable]
    public struct DamageMultiplier
    {
        public PlayerStatus status;
        [Range(0, 100)] public int rate;
    }

    [Header("基本情報のデータ")]
    public WeaponIdentityData identityData;
    [SerializeField] private TierType weaponTier;

    [Header("ステータス")]
    [SerializeField] private float baseDamage;
    public DamageMultiplier damageMultiplier;
    [SerializeField, Range(0, 100)] private float baseCriticalChance;
    [SerializeField] private float baseCriticalDamageMultiplier;
    [SerializeField] private float baseRange;
    [SerializeField] private float baseCoolTime;

    [Header("武器パラメータ")]
    public int bulletCnt;
    public float bulletSpeed;
    public Vector2 fireDirection;
    [Range(0, 360)] public int spreadAngle;
    [Range(0, 100)] public int dispersion;
    public float cycleTime;
    public bool isTargetting;

    [Header("弾丸パラメータ")]
    public int penetrationCnt;
    public int bounceCnt;

    /// <summary>
    /// 参照用プロパティ
    /// </summary>

    // 計算・表示に使用するプロパティ
    public float Damage => baseDamage + (damageMultiplier.status.GetRuntimeStatus() * (damageMultiplier.rate * 0.01f));
    public float CriticalChance => (baseCriticalChance + PlayerController.Instance.playerRuntimeStatus.Critical) * 0.01f;
    public float CriticalMultiplier => baseCriticalDamageMultiplier;
    public float Range => Mathf.Max(0, baseRange + (PlayerController.Instance.playerRuntimeStatus.AttackRange * 0.1f));
    public float CoolTime => baseCoolTime / (1 + PlayerController.Instance.playerRuntimeStatus.AttackSpeed / 100f);
    public float baseAngle => Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;    // 攻撃を発射するデフォルトの向き
    public float fireRate => cycleTime / bulletCnt;                                             // 連続して弾を発射するときの間隔

    #region IProductのプロパティ
    public TierType Tier => weaponTier;
    public Sprite Icon => identityData.weaponIcon;
    public string Name => identityData.weaponName;
    public int Price => identityData.weaponPrice;

    public void PayProduct()
    {
        PlayerController.weaponInventory.AddWeapon(this);
    }

    public string GetDescriptionText()
    {
        string text = string.Empty;
        text = $"ダメージ: {GetStatText(baseDamage, Damage)}(+{damageMultiplier.status.GetPlayerStatusName()}の{damageMultiplier.rate}%)\n" +
               $"クリティカル率: {CriticalChance}%\n" +
               $"クリティカルダメージ: x{baseCriticalDamageMultiplier}\n" +
               $"クールタイム: {GetStatText(baseCoolTime, CoolTime, true)}s\n" +
               $"射程距離: {GetStatText(baseRange, Range)}m\n";

        return text;
    }

    public bool CanBuy()
    {
        if (!PlayerController.Instance.wallet.CanBuy(Price)) return false;

        if (PlayerController.weaponInventory.CanAddWeapon())
            return true;
        else
            return PlayerController.weaponInventory.CanUpgradeWeapon(this);
    }
    #endregion

    public string GetStatText(float baseStat, float scaleStat, bool reverse = false)
    {
        if (scaleStat > baseStat)
            if(!reverse)
                return $"<color=green>{scaleStat.ToString("F1")}</color>";
            else
                return $"<color=red>{scaleStat.ToString("F1")}</color>";
        else if (scaleStat < baseStat)
            if(!reverse)
                return $"<color=red>{scaleStat.ToString("F1")}</color>";
            else
                return $"<color=green>{scaleStat.ToString("F1")}</color>";
        else
            return $"{scaleStat.ToString("F1")}";
    }
}
