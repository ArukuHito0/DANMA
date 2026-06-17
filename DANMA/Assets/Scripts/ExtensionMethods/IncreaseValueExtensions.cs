using UnityEngine;

public static class IncreaseValueExtensions
{
    public static void Increase(this ref int value, int amount)
    {
        value += amount;
    }

    public static void Increase(this ref float value, float amount)
    {
        value += amount;
    }
}