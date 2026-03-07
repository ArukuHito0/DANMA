using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatus", menuName = "WeaponData")]
public class WeaponData : ProductBaseData
{
    [Serializable]
    public struct DamageMultiplier
    {
        public PlayerStatus status;
        [Range(0, 100)] public int rate;
    }

    [Header("ステータス")]
    [SerializeField] private Sprite weaponIcon;
    [SerializeField] private string weaponName;
    [SerializeField] private TierType weaponTier;
    [SerializeField] private uint weaponPrice;

    [SerializeField] private float damage;
    public DamageMultiplier damageMultiplier;
    [SerializeField] private float criticalRate;
    [SerializeField] private float criticalDamageMultiplier;
    [SerializeField] private float range;
    [SerializeField] private float coolTime;
    
    public int Damage
    {
        get
        {
            var result = damage + (damageMultiplier.status.GetRuntimeStatus() * ((float)damageMultiplier.rate / 100f));
            return result <= 0 ? 1 : Mathf.RoundToInt(result);
        }
    }

    public float CriticalRate
    {
        get
        {
            return criticalRate + PlayerStatus.Critical.GetRuntimeStatus();
        }
    }

    public float Range
    {
        get
        {
            return range * (1 + PlayerStatus.AttackRange.GetRuntimeStatus() / 100f);
        }
    }

    public float CoolTime
    {
        get
        {
            return coolTime / (1 + PlayerStatus.AttackSpeed.GetRuntimeStatus() / 100f);
        }
    }

    
    [Header("パラメータ")]
    public int bulletCnt;
    public float bulletSpeed;
    public Vector2 fireDirection;
    [Range(0, 360)] public int spreadAngle;
    [Range(0, 100)] public int dispersion;
    public float cycleTime;
    public bool isTargetting;

    public float baseAngle => Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

    public float fireRate => cycleTime / bulletCnt;

    #region ProductBaseData

    public override TierType Tier => weaponTier;
    public override Sprite Icon => weaponIcon;
    public override string Name => weaponName;
    public override uint Price => weaponPrice;

    public override void PayProduct()
    {

    }

    public override string GetDescriptionText()
    {
        string text = string.Empty;

        text = $"ダメージ: {ResultToColorText(damage, Damage)}({damageMultiplier.status.GetPlayerStatusName()}{damageMultiplier.rate}%)\n" +
               $"クリティカル率: {ResultToColorText(criticalRate, CriticalRate)}%\n" +
               $"クリティカルダメージ: x{criticalDamageMultiplier}\n" +
               $"弾数: {bulletCnt} 発\n" +
               $"発射方向: \n" +
               $"拡散角度: {spreadAngle}°\n" +
               $"射撃誤差: {dispersion}%\n" +
               $"クールダウン: {ResultToColorText(coolTime, CoolTime, true)}s\n" +
               $"射程距離: {ResultToColorText(range, Range)}m\n";

        return text;
    }

    #endregion

    private string ResultToColorText(float baseValue, float result, bool reverse = false)
    {
        if (result == baseValue)
            return $"{baseValue}";
        else if (result > baseValue)
            return reverse ? $"<color=red>{result}</color>" : $"<color=green>{result}</color>";
        else
            return reverse ? $"<color=green>{result}</color>" : $"<color=red>{result}</color>";
    }

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
