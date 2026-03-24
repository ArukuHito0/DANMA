using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopCurator : MonoBehaviour
{
    [SerializeField] private AudioClip rerollSe;
    [SerializeField] private AudioClip rerollFailedSe;

    [SerializeField] private ItemLottery itemLottery;
    [SerializeField] private WeaponLottery weaponLottery;

    [SerializeField] private int rerollMoney;
    public int RerollMoney => (int)(rerollMoney + (rerollMoney * 0.3f) * EnemyGenerator.Instance.currentWaveCnt * rerollCnt);

    [SerializeField] private uint weaponSpawnRate;
    private float WeaponSpawnRate => weaponSpawnRate * 0.01f;

    [SerializeField] private ProductCard[] productCards;

    private HashSet<IProduct> lineups = new HashSet<IProduct>();

    private int rerollCnt = 0;

    private bool canFreeReroll => rerollCnt < PlayerController.Instance.playerRuntimeStatus.FreeRerollCnt;

    public event Action OnShopEnter;
    public event Action<int> OnRerolled;

    private void Awake()
    {
        itemLottery.LoadAllAssets();
        weaponLottery.LoadAllAssets();
    }

    public void Reroll()
    {
        if (PlayerController.Instance.wallet.CanBuy(RerollMoney))
        {
            rerollCnt++;

            if (!canFreeReroll)
                PlayerController.Instance.wallet.RemoveMoney(RerollMoney);

            OnRerolled?.Invoke(RerollMoney);

            SoundUtil.PlaySe(rerollSe.name);
            UpdateProducts();
        }
        else
        {
            SoundUtil.PlaySe(rerollFailedSe.name);
        }
    }

    // 全商品を抽選
    public void UpdateProducts()
    {
        lineups.Clear();

        // ロック済みの商品のデータを追加
        foreach(var product in productCards)
            if(product.isLocked)
                lineups.Add(product.product);

        if (productCards == null) return;

        foreach (var product in productCards)
        {
            if(product.isLocked) continue;

            IProduct data = null;

            // 商品が抽選されるまでループ
            while (data == null)
            {
                // 武器かアイテムを抽選
                IProduct candidate = UnityEngine.Random.value < WeaponSpawnRate ? weaponLottery.GetProduct() : itemLottery.GetProduct();
                 
                // 既に出ていない商品なら代入
                if(!lineups.Contains(candidate))
                {
                    data = candidate;

                    // ラインナップに追加し、カードに商品データをセット
                    lineups.Add(data);

                    product?.SetProduct(data);
                    product?.Initialize();

                    break;
                }
                else
                {
                    data = null;
                }
            }
        }
    }

    public void SetShop()
    {
        rerollCnt = 0;
        UpdateProducts();

        OnShopEnter?.Invoke();
        OnRerolled?.Invoke(canFreeReroll ? 0 : RerollMoney);
    }
}
