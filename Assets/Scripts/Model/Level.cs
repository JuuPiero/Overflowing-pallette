
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int rows;
    public int columns;
    public int maxChange;
    public string[] colors;
    public int target;
    public int[] data;


    public Color GetTarget()
    {
        Color colorTarger;
        ColorUtility.TryParseHtmlString(colors[target], out colorTarger);
        return colorTarger;
    }

    public List<Color> GetColors()
    {
        List<Color> newColors = new();
        foreach (var colorHex in colors)
        {
            Color color;
            ColorUtility.TryParseHtmlString(colorHex, out color);
            newColors.Add(color);
        }
        return newColors;
    }

}