using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class ProductLotteryBase<T> : ScriptableObject, IProductLottery
    where T : ScriptableObject, IProduct
{
    [Serializable]
    public class TierChance
    {
        public TierType tier;
        public float baseChance;
        public float waveAddChance;
        public float maxChance;
        public uint unlockWaveNum;
    }

    public TierChance[] tierChances;

    protected Dictionary<TierType, List<T>> dataDict = new Dictionary<TierType, List<T>>();

    [SerializeField] private string _resourcesPath;

    // ロードするResoutcesのパス
    protected string ResourcesPath
    {
        get
        {
            return _resourcesPath;
        }
        set
        {
            _resourcesPath = value;
        }
    }

    // 各ティアの確率計算
    private Dictionary<TierType, float> GetTierChances()
    {
        var activeChances = new Dictionary<TierType, float>();
        float totalAddedBonus = 0f;

        // 各ティアの出現確率を計算
        foreach (var config in tierChances)
        {
            // コモンティアの場合は無視
            if (config.tier == TierType.Common) continue;

            // 現在のウェーブ数がレアリティが出現するウェーブ数に達しているか
            if (EnemyGenerator.Instance.currentWaveCnt >= config.unlockWaveNum)
            {
                // 出現するようになってからの経過ウェーブ数分で計算
                float chance = (config.waveAddChance * (EnemyGenerator.Instance.currentWaveCnt - ((int)config.unlockWaveNum - 1)) + config.baseChance) * (1 + (PlayerController.Instance.playerRuntimeStatus.Luck * 0.01f));

                //Debug.Log(chance);
                //Debug.Log($"運補正; {1 + (PlayerController.Instance.playerRuntimeStatus.Luck * 0.01f)}");
                
                chance = Mathf.Clamp(chance, 0, config.maxChance);
                //Debug.Log($"{config.tier.ToString()}の出現確率: {chance}");

                // 計算結果を格納
                activeChances[config.tier] = chance;
                totalAddedBonus += chance;
            }
            else
            {
                // 出現するウェーブ数に達していない場合、確率は0%
                activeChances[config.tier] = 0;
            }
        }

        // コモンの出現率を計算(デフォルトは100%)
        activeChances[TierType.Common] = Mathf.Clamp(100 - totalAddedBonus, 0, 100);
        return activeChances;
    }

    // アイテム抽選
    protected virtual T RandomizeData()
    {
        if (dataDict == null || dataDict.Count == 0) Debug.Log("抽選対象のデータがありません");

        var tierChances = GetTierChances();

        float total = tierChances.Values.Sum();
        float randomValue = UnityEngine.Random.Range(0, total);

        float sum = 0;

        foreach (var pair in tierChances)
        {
            sum += pair.Value;

            if (randomValue > sum) continue;

            // 抽選中のティアのキーが登録されていて、そのキーのリストが空でないか確認
            if (dataDict.TryGetValue(pair.Key, out var list) && list.Any())
            {
                T product = list[UnityEngine.Random.Range(0, list.Count)];

                return product;
            }
        }

        return null;
    }

    #region IProductLotteryのプロパティ
    public IProduct GetProduct()
    {
        T selected = RandomizeData();

        return selected != null ? selected : null;
    }

    public void LoadAllAssets()
    {
        T[] datas = Resources.LoadAll<T>(ResourcesPath);

        dataDict = datas
            .GroupBy(data => data.Tier)
            .ToDictionary(
            group => group.Key,
            group => group.ToList()
            );

        // --- デバッグログの追加 ---
        Debug.Log($"<color=cyan>【{typeof(T).Name}】ロード完了: 合計 {datas.Length} 件</color>");

        foreach (var pair in dataDict)
        {
            // そのティアに属するアイテム名のリストを作成（最初の3つだけ表示するなど）
            string itemNames = string.Join(", ", pair.Value.Select(d => d.name).Take(3));
            string suffix = pair.Value.Count > 3 ? "..." : "";

            Debug.Log($"ティア: <b>{pair.Key}</b> | 個数: {pair.Value.Count}件 ({itemNames}{suffix})");
        }
        // -----------------------
    }
    #endregion
}
