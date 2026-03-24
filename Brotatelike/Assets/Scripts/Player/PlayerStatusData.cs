using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "PlayerStatus")]
public class PlayerStatusData : ScriptableObject
{
    public long firstGold;

    [Header("メインステータス")]
    [SerializeField] private float baseMaxHealth;
    [SerializeField] private float baseStrength;
    [SerializeField] private float baseAttackSpeed;
    [SerializeField] private float baseCritical;
    [SerializeField] private float baseAttackRange;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float baseArmor;
    [SerializeField] private float baseDodgeChance;
    [SerializeField] private int baseLuck;

    [Header("サブステータス")]
    [SerializeField] private float baseCollectRange;
    [SerializeField] private float baseGetGoldRate;
    [SerializeField] private float baseGetExpRate;
    [SerializeField] private float baseFreeRerollCnt;
    [SerializeField] private float baseWaveHeal;
    [SerializeField] private float baseWaveGetGold;
    [SerializeField] private float baseAttackHeal;
    [SerializeField] private float baseEnemySpawnRate;
    [SerializeField] private float baseSaleSpawnChance;
    [SerializeField] private float baseItemPriceRate;

    [Header("各ステータス上限値")]
    public float maxArmor;
    public float maxDodgeChance;
    public float maxSaleSpawnChance;
    public float maxItemPriceRate;

    #region メインステータス参照用プロパティ
    public float BaseMaxHealth => baseMaxHealth;
    public float BaseStrength => baseStrength;
    public float BaseAttackSpeed => baseAttackSpeed;
    public float BaseCritical => baseCritical;
    public float BaseAttackRange => baseAttackRange;
    public float BaseMoveSpeed => baseMoveSpeed;
    public float BaseArmor => baseArmor;
    public float BaseDodgeChance => baseDodgeChance;
    public int BaseLuck => baseLuck;
    #endregion

    #region サブステータス参照用プロパティ
    public float BaseCollectRange => baseCollectRange;
    public float BaseGetGoldRate => baseGetGoldRate;
    public float BaseGetExpRate => baseGetExpRate;
    public float BaseFreeRerollCnt => baseFreeRerollCnt;
    public float BaseWaveHeal => baseWaveHeal;
    public float BaseWaveGetGold => baseWaveGetGold;
    public float BaseAttackHeal => baseAttackHeal;
    public float BaseEnemySpawnRate => baseEnemySpawnRate;
    public float BaseSaleSpawnChance => baseSaleSpawnChance;
    public float BaseItemPriceRate => baseItemPriceRate;
    #endregion
}