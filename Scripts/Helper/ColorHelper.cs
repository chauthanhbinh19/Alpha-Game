using UnityEngine;
using System.Collections.Generic;
public static class ColorHelper
{
    // --- TỪ HEX SANG COLOR ---
    public static Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
            return color;
        return Color.white;
    }
    // --- TỪ COLOR SANG HEX (CHỈ LẤY RGB) ---
    public static string ColorToHexRGB(Color color)
    {
        // Trả về định dạng: #RRGGBB
        return "#" + ColorUtility.ToHtmlStringRGB(color);
    }

    // --- TỪ COLOR SANG HEX (CÓ CẢ ALPHA) ---
    public static string ColorToHexRGBA(Color color)
    {
        // Trả về định dạng: #RRGGBBAA
        return "#" + ColorUtility.ToHtmlStringRGBA(color);
    }
}