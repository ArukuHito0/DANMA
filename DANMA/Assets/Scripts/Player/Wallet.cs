using System;
using UnityEngine;

[System.Serializable]
public class Wallet
{
    [SerializeField]
    // Š‹à
    private long currentMoney;
    public long CurrentMoney => currentMoney;

    // Š‹àÅ‘å’l
    public static readonly long MAX_HOLD_MONEY = 999999999999;

    public static event Action<int> OnMoneyAdded;
    public static event Action<int> OnMoneyRemoved;
    public static event Action OnMoneyChanged;

    public void SetMoney(long money)
    {
        currentMoney = money;
        OnMoneyChanged?.Invoke();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        
        if (currentMoney > MAX_HOLD_MONEY)
        {
            currentMoney = MAX_HOLD_MONEY;
        }

        OnMoneyAdded?.Invoke(amount);
        OnMoneyChanged?.Invoke();
    }

    public void RemoveMoney(int amount)
    {
        currentMoney -= amount;
        
        if (currentMoney < 0)
        {
            currentMoney = 0;
        }

        OnMoneyRemoved?.Invoke(amount);
        OnMoneyChanged?.Invoke();
    }

    public bool CanBuy(int amount)
    {
        return currentMoney >= amount;
    }
}