
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

    public int GetValueAt(int x, int y)
    {
        return data[y * columns + x]; 
    }

    public Color GetColorAt(int x, int y)
    {
        Color cellColor;


        int cellValue = GetValueAt(x, y);
        if (cellValue != -1)
        {
            ColorUtility.TryParseHtmlString(colors[cellValue], out cellColor);
            // cell.SetColor(cellColor);
        }
        else
        {
            cellColor = Color.gray;
            // cell.CanChange = false;
            // cell.SetColor(Color.gray);
        }
        return cellColor;
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