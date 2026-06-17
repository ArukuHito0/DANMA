using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "WeaponLottery", menuName = "Lottery/Weapon")]
public class WeaponLottery : ProductLotteryBase<WeaponData>
{
    protected override WeaponData RandomizeData()
    {
        return base.RandomizeData();
    }

    // 特定のティアの武器を抽選
    public List<WeaponData> GetRandomWeaponDatasByTier(TierType tier, int cnt)
    {
        if (dataDict == null || dataDict.Count == 0) Debug.Log("抽選対象のデータがありません");

        if (dataDict.TryGetValue(tier, out var list) && list.Any())
        {
            list.Shuffle();

            return list.GetRange(0, cnt);
        }

        return null;
    }
}