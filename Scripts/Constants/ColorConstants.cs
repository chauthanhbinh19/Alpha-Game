using TMPro;
using UnityEngine;

public static class ColorConstanst
{
    public const string DefaultText = "#F9EED9";
    public const string RareText = "#FFD700"; // Vàng
    public const string EpicText = "#800080"; // Tím
    public const string LegendaryText = "#FF4500"; // Cam đỏ
    public const string DisabledText = "#AAAAAA";
    public static VertexGradient PhysicalGradient = new VertexGradient(
        new Color32(255, 99, 71, 255),   // Top Left - Tomato
        new Color32(255, 69, 0, 255),    // Top Right - OrangeRed
        new Color32(139, 0, 0, 255),     // Bottom Left - DarkRed
        new Color32(178, 34, 34, 255)    // Bottom Right - FireBrick
    );

    public static VertexGradient MagicGradient = new VertexGradient(
        new Color32(135, 206, 250, 255), // Top Left - LightSkyBlue
        new Color32(30, 144, 255, 255),  // Top Right - DodgerBlue
        new Color32(65, 105, 225, 255),  // Bottom Left - RoyalBlue
        new Color32(138, 43, 226, 255)   // Bottom Right - BlueViolet
    );

    public static VertexGradient ChemicalGradient = new VertexGradient(
        new Color32(255, 215, 0, 255),   // Top Left - Gold
        new Color32(255, 165, 0, 255),   // Top Right - Orange
        new Color32(255, 140, 0, 255),   // Bottom Left - DarkOrange
        new Color32(255, 127, 80, 255)   // Bottom Right - Coral
    );

    public static VertexGradient AtomicGradient = new VertexGradient(
        new Color32(218, 112, 214, 255), // Top Left - Orchid
        new Color32(186, 85, 211, 255),  // Top Right - MediumOrchid
        new Color32(139, 0, 139, 255),   // Bottom Left - DarkMagenta
        new Color32(148, 0, 211, 255)    // Bottom Right - DarkViolet
    );

    public static VertexGradient MentalGradient = new VertexGradient(
        new Color32(211, 211, 211, 255), // Top Left - LightGray
        new Color32(169, 169, 169, 255), // Top Right - DarkGray
        new Color32(128, 128, 128, 255), // Bottom Left - Gray
        new Color32(80, 80, 80, 255)     // Bottom Right - Charcoal
    );
    public static class HexColor
    {
        public const string descriptionColor = "#F9EED9";
    }

    // Thêm các màu khác nếu cần

    public static Color ToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color color))
            return color;
        return Color.white;
    }
}
