using TMPro;
using UnityEngine;

public static class ColorConstants
{
    public const string DEFAULT_TEXT_COLOR = "#F9EED9";
    public const string RARE_TEXT_COLOR = "#FFD700"; // Vàng
    public const string EPIC_TEXT_COLOR = "#800080"; // Tím
    public const string LEGENDARY_TEXT_COLOR = "#FF4500"; // Cam đỏ
    public const string DISABLED_TEXT_COLOR = "#AAAAAA";
    public const string RED_COLOR       = "#9E2F3C";
    public const string GREEN_COLOR     = "#064E3B";
    public const string BLUE_COLOR      = "#3E79F6";
    public const string YELLOW_COLOR    = "#B45309";
    public const string CYAN_COLOR      = "#00FFFF";
    public const string MAGENTA_COLOR   = "#FF00FF";
    public const string ORANGE_COLOR    = "#C2410C";
    public const string PURPLE_COLOR    = "#9A60A2";
    public const string PINK_COLOR      = "#BE185D";
    public const string BROWN_COLOR     = "#A52A2A";
    public const string GRAY_COLOR      = "#646464";
    public const string LIGHT_GRAY_COLOR = "#D3D3D3";
    public const string DARK_GRAY_COLOR  = "#A9A9A9";
    public const string BLACK_COLOR     = "#000000";
    public const string WHITE_COLOR = "#FFFFFF";
    
    public const string RED_TRANSPARENT_COLOR       = "#c51313cc"; 
    public const string GREEN_TRANSPARENT_COLOR     = "#27b327cc";
    public const string BLUE_TRANSPARENT_COLOR      = "#1616becc";
    public const string YELLOW_TRANSPARENT_COLOR    = "#c8c81bcc";
    public const string CYAN_TRANSPARENT_COLOR      = "#19d1d1cc";
    public const string MAGENTA_TRANSPARENT_COLOR   = "#b016b0cc";
    public const string ORANGE_TRANSPARENT_COLOR    = "#be841acc";
    public const string PURPLE_TRANSPARENT_COLOR    = "#b51ab5cc";
    public const string PINK_TRANSPARENT_COLOR      = "#eb2c9fcc";
    public const string BROWN_TRANSPARENT_COLOR     = "#771a1acc";
    public const string GRAY_TRANSPARENT_COLOR      = "#808080CC";
    public const string LIGHT_GRAY_TRANSPARENT_COLOR= "#D3D3D3CC";
    public const string DARK_GRAY_TRANSPARENT_COLOR = "#A9A9A9CC";
    public const string BLACK_TRANSPARENT_COLOR     = "#000000CC";
    public const string WHITE_TRANSPARENT_COLOR     = "#FFFFFFCC";
    
    public const string SR_COLOR = "#3C52FF";
    public const string SSR_COLOR = "#FFD33C";
    public const string UR_COLOR = "#FF7D3C";
    public const string LG_COLOR = "#FF423C";
    public const string LGPlus_COLOR = "#FF3C55";
    public const string MR_COLOR = "#BDFF3C";
    public const string SLG_COLOR = "#82FF3C";
    public const string SLGPlus_COLOR = "#3CFF98";
    public const string SP_COLOR = "#3CE9FF";
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
}
