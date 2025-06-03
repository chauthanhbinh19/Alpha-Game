using UnityEngine;

public static class UIColors
{
    public const string DefaultText = "#F9EED9";
    public const string RareText = "#FFD700"; // Vàng
    public const string EpicText = "#800080"; // Tím
    public const string LegendaryText = "#FF4500"; // Cam đỏ
    public const string DisabledText = "#AAAAAA";

    // Thêm các màu khác nếu cần

    public static Color ToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
            return color;
        return Color.white;
    }
}
