using UnityEngine;

public static class ValueColorExtentions
{
    public static string GetValueColorText(this float value)
    {
        return value > 0 ? $"<color=green>+{value}" : $"<color=red>{value}";
    }
}
