

using System.Collections.Generic;
using UnityEngine;

public static class ColorExtensions
{

    public static string ToHex(this Color color)
    {
        int r = Mathf.RoundToInt(color.r * 255f);
        int g = Mathf.RoundToInt(color.g * 255f);
        int b = Mathf.RoundToInt(color.b * 255f);
        return $"#{r:X2}{g:X2}{b:X2}";
    }

    public static string ToHexRGBA(this Color color)
    {
        int r = Mathf.RoundToInt(color.r * 255f);
        int g = Mathf.RoundToInt(color.g * 255f);
        int b = Mathf.RoundToInt(color.b * 255f);
        int a = Mathf.RoundToInt(color.a * 255f);
        return $"#{r:X2}{g:X2}{b:X2}{a:X2}";
    }

    public static string[] ToHexArray(this List<Color> colors)
    {
        string[] hexColors = new string[colors.Count];
        for (int i = 0; i < colors.Count; i++)
        {
            Color color = colors[i];
            int r = Mathf.RoundToInt(color.r * 255f);
            int g = Mathf.RoundToInt(color.g * 255f);
            int b = Mathf.RoundToInt(color.b * 255f);
            hexColors[i] = $"#{r:X2}{g:X2}{b:X2}";
        }
        return hexColors;
    }
}