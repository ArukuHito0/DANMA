using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class WeaponInventory
{
    public List<Weapon> weaponList { get; private set; } = new List<Weapon>();

    private const int WEAPON_MAX_CNT = 6;

    public event Action<WeaponData, int> OnWeaponSlotChanged;

    /// <summary>
    /// インベントリに武器を追加する関数
    /// <br>空きが無く、同じ武器で同ティアのものが有れば自動でアップグレード</br>
    /// </summary>
    /// <param name="data"></param>
    public void AddWeapon(WeaponData data)
    {
        // 空きスロットを探す
        int emptyIdx = weaponList.FindIndex(w => w.GetWeaponData() == null);

        if (emptyIdx != -1)
        {
            // 空きスロットがあればそこにデータをセット
            weaponList[emptyIdx].SetWeaponData(data);

            OnWeaponSlotChanged?.Invoke(data, emptyIdx);
        }
        else if (weaponList.Count < WEAPON_MAX_CNT)
        {
            // リスト自体に空きがあれば新規作成して追加
            Weapon instance = new Weapon(PlayerController.Instance.gameObject);
            instance.SetWeaponData(data);
            weaponList.Add(instance);

            OnWeaponSlotChanged?.Invoke(data, weaponList.Count - 1);
        }
        else
        {
            // すでに同じ武器かつ同ティアがあるか確認
            int existingIdx = GetSameWeaponIdx(data);

            if (existingIdx != -1)
            {
                // 同じ武器があるならアップグレード
                AddtoUpgradeWeapon(data);
                return;
            }
        }
    }

    public void AddtoUpgradeWeapon(WeaponData data)
    {
        // 同じティアの同じ武器がスロット内に２つある時に合成
        // 同じ武器を探す
        int idx = GetSameWeaponIdx(data);

        if (idx == -1) return;

        // 次のティアのデータをロード
        WeaponData nextTierData = GetNextTierWeaponData(weaponList[idx].GetWeaponData());
        if (nextTierData == null) return;
        
        // 購入時のアップグレード処理
        weaponList[idx].SetWeaponData(nextTierData);

        OnWeaponSlotChanged?.Invoke(nextTierData, idx);
    }

    /// <summary>
    /// 武器をアップグレードさせる関数
    /// </summary>
    /// <param name="data"></param>
    public void UpgradeWeapon(WeaponData data)
    {
        // 同じティアの同じ武器がスロット内に２つある時に合成
        // まず1つ目を探す
        int firstIdx = GetSameWeaponIdx(data);

        if (firstIdx == -1) return;

        // 2つ目があるか確認
        int secondIdx = weaponList.FindIndex(firstIdx + 1, w =>
            w.GetWeaponData() != null &&
            w.GetWeaponData().Name == data.Name &&
            w.GetWeaponData().Tier == data.Tier);

        if(secondIdx == -1) return;

        // 次のティアのデータをロード
        WeaponData nextTierData = GetNextTierWeaponData(weaponList[firstIdx].GetWeaponData());

        if (nextTierData == null) return;

        // 1つ目をランクアップし、2つ目を空にする
        weaponList[firstIdx].SetWeaponData(nextTierData);
        RemoveWeapon(weaponList[secondIdx].GetWeaponData());

        OnWeaponSlotChanged?.Invoke(nextTierData, firstIdx);
        OnWeaponSlotChanged?.Invoke(null, secondIdx);
    }

    public void RemoveWeapon(WeaponData data)
    {
        int idx = weaponList.FindIndex(w => w.GetWeaponData() == data);
        if (idx != -1)
        {
            weaponList[idx].SetWeaponData(null);

            OnWeaponSlotChanged?.Invoke(null, idx);
        }
    }

    public void RemoveWeapon(int idx)
    {
        if (idx != -1)
        {
            weaponList[idx].SetWeaponData(null);

            OnWeaponSlotChanged?.Invoke(null, idx);
        }
    }

    public bool CanAddWeapon()
    {
        return GetWeaponCnt() < WEAPON_MAX_CNT;
    }

    public bool CanUpgradeWeapon(WeaponData data)
    {
        if (data.Tier == TierType.Legend) return false;
        else return GetSameWeaponIdx(data) != -1 ? true : false;
    }

    public bool HasAnyWeapon()
    {
        return weaponList.Exists(w => w.GetWeaponData() != null);
    }

    public int GetWeaponCnt()
    {
        return weaponList.Where(w => w.GetWeaponData() != null).Count();
    }

    private int GetSameWeaponIdx(WeaponData data)
    {
        return weaponList.FindIndex(w =>
                w.GetWeaponData() != null &&
                w.GetWeaponData().Name == data.Name &&
                w.GetWeaponData().Tier == data.Tier);
    }

    private WeaponData GetNextTierWeaponData(WeaponData data)
    {
        if (data.Tier >= TierType.Legend) return null;
        return Resources.Load<WeaponData>($"Weapons/{data.Tier.TierUp().ToString()}/{data.name}");
    }
}