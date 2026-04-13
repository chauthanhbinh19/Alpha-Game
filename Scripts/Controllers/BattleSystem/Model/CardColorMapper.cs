using UnityEngine;
using System.Collections.Generic;

// File: CardColorMapper.cs
public static class CardColorMapper
{
    // Dùng Dictionary để ánh xạ nhanh chóng và rõ ràng
    private static readonly Dictionary<CardType, Color> TypeColorMap = new Dictionary<CardType, Color>
    {
        { CardType.CardHero,    ColorHelper.HexToColor(ColorConstants.RED_TRANSPARENT_COLOR) },
        { CardType.CardCaptain,  ColorHelper.HexToColor(ColorConstants.BLUE_TRANSPARENT_COLOR) },
        { CardType.CardColonel,  ColorHelper.HexToColor(ColorConstants.GREEN_TRANSPARENT_COLOR) },
        { CardType.CardGeneral,  ColorHelper.HexToColor(ColorConstants.YELLOW_TRANSPARENT_COLOR) },
        { CardType.CardAdmiral,  ColorHelper.HexToColor(ColorConstants.PURPLE_TRANSPARENT_COLOR) },
        { CardType.CardMonster,  ColorHelper.HexToColor(ColorConstants.ORANGE_TRANSPARENT_COLOR) },
        // Thêm các loại thẻ khác
        { CardType.CardMilitary, ColorHelper.HexToColor(ColorConstants.CYAN_TRANSPARENT_COLOR) },
        { CardType.CardSpell,    ColorHelper.HexToColor(ColorConstants.MAGENTA_TRANSPARENT_COLOR) },
        { CardType.Books,        ColorHelper.HexToColor(ColorConstants.BLACK_TRANSPARENT_COLOR) },
        { CardType.Pets,         ColorHelper.HexToColor(ColorConstants.WHITE_TRANSPARENT_COLOR) }
    };

    /// <summary>
    /// Trả về màu tương ứng với CardType.
    /// </summary>
    /// <param name="type">CardType cần tra cứu.</param>
    /// <returns>Màu sắc tương ứng. Trả về màu trắng nếu không tìm thấy.</returns>
    public static Color GetColor(CardType type)
    {
        if (TypeColorMap.TryGetValue(type, out Color color))
        {
            return color;
        }
        
        // Trả về màu mặc định (ví dụ: Trắng) nếu CardType không được định nghĩa trong map
        Debug.LogWarning($"CardType {type} không có màu được định nghĩa. Trả về màu Trắng.");
        return ColorHelper.HexToColor(ColorConstants.WHITE_TRANSPARENT_COLOR);
    }
}