using System;
using System.Collections;
using UnityEngine;

public class CoinItem : PickableItem
{
    [SerializeField] private int gold;

    protected override void OnPickUpItem()
    {
        SoundUtil.PlaySe("GetCoin");

        PlayerController.Instance.wallet.AddMoney(gold);
    }
}
