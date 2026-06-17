using UnityEngine;

public class CoinItem : PickableItem
{
    [SerializeField] private int gold;

    protected override void OnPickUpItem()
    {
        SoundUtil.PlaySe("GetCoin");

        var result = gold * (1 + PlayerStatus.GetGoldRate.GetRuntimeStatus() * 0.01f);

        PlayerController.Instance.wallet.AddMoney((int)result);
    }
}
