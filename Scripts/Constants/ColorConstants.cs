using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class ColorConstants
{
    public const string DEFAULT_COLOR = "#F9EED9";
    public const string DISABLED_COLOR = "#AAAAAA";
    public const string DESCRIPTION_TEXT_COLOR = "#F9EED9";
    public const string RED_COLOR = "#FD1F41";
    public const string GREEN_COLOR = "#1ffd4b";
    public const string BLUE_COLOR = "#1f35fd";
    public const string YELLOW_COLOR = "#FDEE1F";
    public const string CYAN_COLOR = "#00FFFF";
    public const string MAGENTA_COLOR = "#ea00ff";
    public const string ORANGE_COLOR = "#FD7F1F";
    public const string PURPLE_COLOR = "#A61FFD";
    public const string PINK_COLOR = "#FD1F9B";
    public const string BROWN_COLOR = "#A52A2A";
    public const string GRAY_COLOR = "#646464";
    public const string LIGHT_GRAY_COLOR = "#D3D3D3";
    public const string DARK_GRAY_COLOR = "#A9A9A9";
    public const string BLACK_COLOR = "#000000";
    public const string WHITE_COLOR = "#FFFFFF";

    public const string RED_TRANSPARENT_COLOR = "#c51313cc";
    public const string GREEN_TRANSPARENT_COLOR = "#27b327cc";
    public const string BLUE_TRANSPARENT_COLOR = "#1616becc";
    public const string YELLOW_TRANSPARENT_COLOR = "#c8c81bcc";
    public const string CYAN_TRANSPARENT_COLOR = "#19d1d1cc";
    public const string MAGENTA_TRANSPARENT_COLOR = "#b016b0cc";
    public const string ORANGE_TRANSPARENT_COLOR = "#be841acc";
    public const string PURPLE_TRANSPARENT_COLOR = "#b51ab5cc";
    public const string PINK_TRANSPARENT_COLOR = "#eb2c9fcc";
    public const string BROWN_TRANSPARENT_COLOR = "#771a1acc";
    public const string GRAY_TRANSPARENT_COLOR = "#808080CC";
    public const string LIGHT_GRAY_TRANSPARENT_COLOR = "#D3D3D3CC";
    public const string DARK_GRAY_TRANSPARENT_COLOR = "#A9A9A9CC";
    public const string BLACK_TRANSPARENT_COLOR = "#000000CC";
    public const string WHITE_TRANSPARENT_COLOR = "#FFFFFFCC";

    public static class Rare
    {
        public const string SR_COLOR = "#3C52FF";
        public const string SSR_COLOR = "#FFD33C";
        public const string UR_COLOR = "#FF7D3C";
        public const string LG_COLOR = "#FF423C";
        public const string LGPlus_COLOR = "#FF3C55";
        public const string MR_COLOR = "#BDFF3C";
        public const string SLG_COLOR = "#82FF3C";
        public const string SLGPlus_COLOR = "#3CFF98";
        public const string SP_COLOR = "#3CE9FF";
    }

    public static class Card
    {
        public const string CARD_HERO_COLOR = "#FF0B06";
        public const string CARD_CAPTAIN_COLOR = "#FF9406";
        public const string CARD_COLONEL_COLOR = "#E1FF06";
        public const string CARD_GENERAL_COLOR = "#4BFF06";
        public const string CARD_ADMIRAL_COLOR = "#06FFC0";
        public const string CARD_MONSTER_COLOR = "#0681FF";
        public const string CARD_MILITARY_COLOR = "#6706FF";
        public const string CARD_SPELL_COLOR = "#FF06CB";
    }

    public static class Emblem
    {
        public const string EMBLEM_FACTION_A_COLOR = "#CA1F1F";
        public const string EMBLEM_FACTION_B_COLOR = "#CAB61F";
        public const string EMBLEM_FACTION_C_COLOR = "#3ECA1F";
        public const string EMBLEM_FACTION_D_COLOR = "#1FCAB0";
        public const string EMBLEM_FACTION_E_COLOR = "#1F4ECA";
    }

    public static class Class
    {
        public const string CASTER = "#CA1F1F";
        public const string DEFENDER = "#b6ca1f";
        public const string GUARD = "#22ca1f";
        public const string MEDIC = "#1fcab6";
        public const string SNIPER = "#1f25ca";
        public const string SPECIALIST = "#9c1fca";
        public const string SUPPORTER = "#ca1f97";
        public const string VANGUARD = "#ca601f";
    }
    public static VertexGradient PHYSICAL_GRADIENT_COLOR = new VertexGradient(
        new Color32(255, 99, 71, 255),   // Top Left - Tomato
        new Color32(255, 69, 0, 255),    // Top Right - OrangeRed
        new Color32(139, 0, 0, 255),     // Bottom Left - DarkRed
        new Color32(178, 34, 34, 255)    // Bottom Right - FireBrick
    );

    public static VertexGradient MAGICAL_GRADIENT_COLOR = new VertexGradient(
        new Color32(135, 206, 250, 255), // Top Left - LightSkyBlue
        new Color32(30, 144, 255, 255),  // Top Right - DodgerBlue
        new Color32(65, 105, 225, 255),  // Bottom Left - RoyalBlue
        new Color32(138, 43, 226, 255)   // Bottom Right - BlueViolet
    );

    public static VertexGradient CHEMICAL_GRADIENT_COLOR = new VertexGradient(
        new Color32(255, 215, 0, 255),   // Top Left - Gold
        new Color32(255, 165, 0, 255),   // Top Right - Orange
        new Color32(255, 140, 0, 255),   // Bottom Left - DarkOrange
        new Color32(255, 127, 80, 255)   // Bottom Right - Coral
    );

    public static VertexGradient ATOMIC_GRADIENT_COLOR = new VertexGradient(
        new Color32(218, 112, 214, 255), // Top Left - Orchid
        new Color32(186, 85, 211, 255),  // Top Right - MediumOrchid
        new Color32(139, 0, 139, 255),   // Bottom Left - DarkMagenta
        new Color32(148, 0, 211, 255)    // Bottom Right - DarkViolet
    );

    public static VertexGradient MENTAL_GRADIENT_COLOR = new VertexGradient(
        new Color32(211, 211, 211, 255), // Top Left - LightGray
        new Color32(169, 169, 169, 255), // Top Right - DarkGray
        new Color32(128, 128, 128, 255), // Bottom Left - Gray
        new Color32(80, 80, 80, 255)     // Bottom Right - Charcoal
    );

    public static VertexGradient INCREASE_POWER_COLOR = new VertexGradient(
            ColorHelper.HexToColor("#C5FFBFFF"),
            ColorHelper.HexToColor("#6DFA2EFF"),
            ColorHelper.HexToColor("#2BA400FF"),
            ColorHelper.HexToColor("#0CCA00FF")
        );
    public static VertexGradient DECREASE_POWER_COLOR = new VertexGradient(
            ColorHelper.HexToColor("#FF7547FF"),
            ColorHelper.HexToColor("#FA5B2EFF"),
            ColorHelper.HexToColor("#A41200FF"),
            ColorHelper.HexToColor("#CA1000FF")
        );

}
