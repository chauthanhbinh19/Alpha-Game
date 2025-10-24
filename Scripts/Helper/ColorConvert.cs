using UnityEngine;
using System.Collections.Generic;
public static class ColorHelper
{
    public static Color ToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
            return color;
        return Color.white;
    }
}