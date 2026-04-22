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
    public static Color GetEmblemColor(string emblemType)
    {
        string hex = emblemType switch
        {
            AppConstants.Emblem.FACTION_A => ColorConstants.Emblem.EMBLEM_FACTION_A_COLOR,
            AppConstants.Emblem.FACTION_B => ColorConstants.Emblem.EMBLEM_FACTION_B_COLOR,
            AppConstants.Emblem.FACTION_C => ColorConstants.Emblem.EMBLEM_FACTION_C_COLOR,
            AppConstants.Emblem.FACTION_D => ColorConstants.Emblem.EMBLEM_FACTION_D_COLOR,
            AppConstants.Emblem.FACTION_E => ColorConstants.Emblem.EMBLEM_FACTION_E_COLOR,
            _ => ColorConstants.DEFAULT_COLOR
        };

        return ColorHelper.HexToColor(hex);
    }
}