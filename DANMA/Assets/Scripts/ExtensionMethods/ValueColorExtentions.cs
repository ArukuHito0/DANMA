using UnityEngine;

public static class ValueColorExtentions
{
    public static string ToColorText(this int value)
    {
        if(value == 0) return value.ToString();

        return value > 0 ? $"<color=green>+{value}</color>" : $"<color=red>{value}</color>";
    }

    public static string ToColorText(this float value)
    {
        if (value == 0) return value.ToString();

        return value > 0 ? $"<color=green>+{value}</color>" : $"<color=red>{value}</color>";
    }
}
