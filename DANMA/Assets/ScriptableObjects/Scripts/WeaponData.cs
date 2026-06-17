using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatus", menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject, IProduct
{
    [Serializable]
    public struct DamageMultiplier
    {
        public PlayerStatus status;         // 倍率がのるステータス
        [Range(0, 100)] public int rate;    // ステータスの割合
    }

    #region フィールド変数
    
    [Header("基本情報のデータ")]
    public WeaponIdentityData identityData;
    [SerializeField] private TierType weaponTier;   // 武器のティア
    [SerializeField] private int weaponPrice;       // 武器の値段

    [Header("ステータス")]
    [SerializeField] private float baseDamage;      // 基本ダメージ
    public DamageMultiplier damageMultiplier;       // ダメージ補正
    [SerializeField, Range(-100, 100)] private float baseCriticalChance;    // 基本クリティカル率
    [SerializeField] private float baseCriticalDamageMultiplier;            // 基本クリティカルダメージ
    [SerializeField] private float baseRange;                               // 基本射程距離
    [SerializeField] private float baseCoolTime;                            // 基本クールタイム

    [Header("武器パラメータ")]
    public int bulletCnt;                   // 発射する弾数
    public float bulletSpeed;               // 弾速
    public Vector2 fireDirection;           // 発射方向
    [Range(0, 360)] public int spreadAngle; // 拡散角度
    [Range(0, 100)] public int dispersion;  // 射撃エラー
    public float cycleTime;                 // 射撃サイクルの時間
    public bool isTargetting;               // 敵をねらって打つか

    [Header("弾丸パラメータ")]
    public int penetrationCnt;  // 貫通する敵の数
    #endregion

    #region 参照用プロパティ

    // 計算・表示に使用するプロパティ
    public float Damage => baseDamage + (damageMultiplier.status.GetRuntimeStatus() * (damageMultiplier.rate * 0.01f));
    public float CriticalChance => (baseCriticalChance + PlayerStatus.Critical.GetRuntimeStatus());
    public float CriticalMultiplier => baseCriticalDamageMultiplier;
    public float Range => Mathf.Max(0, baseRange + PlayerStatus.AttackRange.GetRuntimeStatus());
    public float CoolTime => baseCoolTime / (1 + PlayerStatus.AttackSpeed.GetRuntimeStatus() / 100f);
    public float baseAngle => Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;    // 攻撃を発射するデフォルトの向き
    public float fireRate => cycleTime / bulletCnt;                                             // 連続して弾を発射するときの間隔
    #endregion

    #region IProductのプロパティ
    public TierType Tier => weaponTier;
    public Sprite Icon => identityData.weaponIcon;
    public string Name => identityData.weaponName;
    public int Price => weaponPrice;

    public void PayProduct()
    {
        PlayerController.weaponInventory.AddWeapon(this);
    }

    public string GetDescriptionText()
    {
        string text = string.Empty;
        text = $"ダメージ: {GetStatText(baseDamage, Damage)}(+{damageMultiplier.status.GetPlayerStatusName()}の{damageMultiplier.rate}%)\n" +
               $"クリティカル率: {CriticalChance.ToColorText()}%\n" +
               $"クリティカルダメージ: x{baseCriticalDamageMultiplier}\n" +
               $"クールタイム: {GetStatText(baseCoolTime, CoolTime, true)}s\n" +
               $"射程距離: {GetStatText(baseRange, Range)}m\n";

        return text;
    }
    #endregion

    private string GetStatText(float baseStat, float scaleStat, bool reverse = false)
    {
        if (scaleStat < 0) return $"<color=red>{scaleStat.ToString("F1")}</color>";

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
