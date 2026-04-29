using UnityEngine;
using System.Collections.Generic;

// File: CardColorMapper.cs
public static class CardImageMapper
{
    // Dùng Dictionary để ánh xạ nhanh chóng và rõ ràng
    private static readonly Dictionary<CardType, string> TypeColorMap = new Dictionary<CardType, string>
    {
        { CardType.CardHero,    ImageConstants.Background.CARD_HERO_GRADIENT_BACKGROUND_URL },
        { CardType.CardCaptain,  ImageConstants.Background.CARD_CAPTAIN_GRADIENT_BACKGROUND_URL },
        { CardType.CardColonel,  ImageConstants.Background.CARD_COLONEL_GRADIENT_BACKGROUND_URL },
        { CardType.CardGeneral,  ImageConstants.Background.CARD_GENERAL_GRADIENT_BACKGROUND_URL },
        { CardType.CardAdmiral,  ImageConstants.Background.CARD_ADMIRAL_GRADIENT_BACKGROUND_URL },
        { CardType.CardMonster,  ImageConstants.Background.CARD_MONSTER_GRADIENT_BACKGROUND_URL },
        // Thêm các loại thẻ khác
        { CardType.CardMilitary, ImageConstants.Background.CARD_MILITARY_GRADIENT_BACKGROUND_URL },
        { CardType.CardSpell,    ImageConstants.Background.CARD_SPELL_GRADIENT_BACKGROUND_URL },
        { CardType.Book,        ImageConstants.Background.CARD_HERO_GRADIENT_BACKGROUND_URL },
        { CardType.Pet,         ImageConstants.Background.CARD_HERO_GRADIENT_BACKGROUND_URL }
    };

    /// <summary>
    /// Trả về màu tương ứng với CardType.
    /// </summary>
    /// <param name="type">CardType cần tra cứu.</param>
    /// <returns>Màu sắc tương ứng. Trả về màu trắng nếu không tìm thấy.</returns>
    public static string GetImage(CardType type)
    {
        if (TypeColorMap.TryGetValue(type, out string image))
        {
            return image;
        }
        
        // Trả về màu mặc định (ví dụ: Trắng) nếu CardType không được định nghĩa trong map
        Debug.LogWarning($"CardType {type} không có hình ảnh được định nghĩa. Trả về hình ảnh mặc định.");
        return ImageConstants.Background.CARD_HERO_GRADIENT_BACKGROUND_URL;
    }
}