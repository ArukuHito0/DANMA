using UnityEngine;

[System.Serializable]
public class Wallet
{
    [SerializeField]
    // ŠŽ‹à
    private long currentMoney;
    public long CurrentMoney => currentMoney;

    // ŠŽ‹àÅ‘å’l
    public static readonly long MAX_HOLD_MONEY = 99999999;

    public void AddMoney(long amount)
    {
        currentMoney += amount;
        if (currentMoney > MAX_HOLD_MONEY)
        {
            currentMoney = MAX_HOLD_MONEY;
        }
    }

    public void RemoveMoney(long amount)
    {
        currentMoney -= amount;
        if (currentMoney < 0)
        {
            currentMoney = 0;
        }
    }

    public bool CanBuy(long amount)
    {
        return currentMoney >= amount;
    }
}