using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ColorProvider
{
    private static Dictionary<string, Color> _colors = new Dictionary<string, Color>
    { 
        {"red", Color.red }, 
        {"green", Color.green }, 
        {"yellow", Color.yellow } 
    };

    public static Color GetRandomColor()
    {
        return _colors.ElementAt(Random.Range(0, _colors.Count - 1)).Value;
    }

    public static string GetColorName(Color color)
    {
        return _colors.First(x => x.Value == color).Key;
    }
}
