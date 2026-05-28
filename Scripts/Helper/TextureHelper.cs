using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class TextureHelper
{
    private static readonly Dictionary<string, Texture> cache = new Dictionary<string, Texture>();
    private static readonly Dictionary<string, Texture2D> cache2D = new Dictionary<string, Texture2D>();
    private static string GetStarPath(int starLevel)
    {
        return starLevel switch
        {
            1 => ImageConstants.Star.STAR_LV1_URL,//0 - 10
            2 => ImageConstants.Star.STAR_LV2_URL,//11 - 20
            3 => ImageConstants.Star.STAR_LV3_URL,//21 - 30
            4 => ImageConstants.Star.STAR_LV4_URL,//31 - 40
            5 => ImageConstants.Star.STAR_LV5_URL,//41 - 50
            6 => ImageConstants.Star.STAR_LV6_URL,//51 - 60
            7 => ImageConstants.Star.STAR_LV7_URL,//61 - 70
            8 => ImageConstants.Star.STAR_LV8_URL,//71 - 80
            9 => ImageConstants.Star.STAR_LV9_URL,//81 - 90
            _ => ImageConstants.Star.STAR_LV10_URL//91 - 100
        };
    }

    public static Texture LoadTextureCached(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        if (cache.TryGetValue(path, out var texture))
            return texture;

        texture = Resources.Load<Texture>(path);
        cache[path] = texture;
        return texture;
    }

    public static Texture2D LoadTexture2DCached(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        if (cache2D.TryGetValue(path, out var texture2D))
            return texture2D;

        texture2D = Resources.Load<Texture2D>(path);
        cache2D[path] = texture2D;
        return texture2D;
    }

    public static void ClearCache()
    {
        cache.Clear();
        cache2D.Clear();
    }

    public static void CreatePropertyRuneUI(string property, RawImage runeImage)
    {
        Texture runeTexture;
        if (property.Equals(AppConstants.StatFields.PHYSICAL_ATTACK))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.PHYSICAL_ATTACK_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.PHYSICAL_DEFENSE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.PHYSICAL_DEFENSE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.MAGICAL_ATTACK))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.MAGICAL_ATTACK_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.MAGICAL_DEFENSE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.MAGICAL_DEFENSE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.CHEMICAL_ATTACK))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.CHEMICAL_ATTACK_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.CHEMICAL_DEFENSE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.CHEMICAL_DEFENSE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.ATOMIC_ATTACK))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.ATOMIC_ATTACK_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.ATOMIC_DEFENSE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.ATOMIC_DEFENSE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.MENTAL_ATTACK))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.MENTAL_ATTACK_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.MENTAL_DEFENSE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.MENTAL_DEFENSE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.HEALTH))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.HEALTH_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.SPEED))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.SPEED_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.CRITICAL_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.CRITICAL_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.CRITICAL_DAMAGE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.CRITICAL_DAMAGE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.DAMAGE_ABSORPTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.DAMAGE_ABSORPTION_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.CRITICAL_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.CRITICAL_RESISTANCE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.IGNORE_CRITICAL_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.IGNORE_CRITICAL_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.PENETRATION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.PENETRATION_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.PENETRATION_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.PENETRATION_RESISTANCE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.EVASION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.EVASION_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.IGNORE_DAMAGE_ABSORPTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.IGNORE_DAMAGE_ABSORPTION_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.ABSORBED_DAMAGE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.ABSORBED_DAMAGE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.VITALITY_REGENERATION_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.VITALITY_REGENERATION_RESISTANCE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.ACCURACY_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.ACCURACY_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.LIFE_STEAL_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.LIFE_STEAL_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.SHIELD_STRENGTH))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.SHIELD_STRENGTH_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.TENACITY))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.TENACITY_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.RESISTANCE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.COMBO_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.COMBO_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.IGNORE_COMBO_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.IGNORE_COMBO_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.COMBO_DAMAGE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.COMBO_DAMAGE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.COMBO_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.COMBO_RESISTANCE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.STUN_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.STUN_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.IGNORE_STUN_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.IGNORE_STUN_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.MANA))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.MANA_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.MANA_REGENERATION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.MANA_REGENERATION_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.REFLECTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.REFLECTION_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.IGNORE_REFLECTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.IGNORE_REFLECTION_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.REFLECTION_DAMAGE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.REFLECTION_DAMAGE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.REFLECTION_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.REFLECTION_RESISTANCE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.DAMAGE_TO_DIFFERENT_FACTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.DAMAGE_TO_DIFFERENT_FACTION_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.RESISTANCE_TO_DIFFERENT_FACTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.RESISTANCE_TO_DIFFERENT_FACTION_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.DAMAGE_TO_SAME_FACTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.DAMAGE_TO_SAME_FACTION_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.RESISTANCE_TO_SAME_FACTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.RESISTANCE_TO_SAME_FACTION_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.NORMAL_DAMAGE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.NORMAL_DAMAGE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.NORMAL_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.NORMAL_RESISTANCE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.SKILL_DAMAGE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.SKILL_DAMAGE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.SKILL_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.SKILL_RESISTANCE_RATE_URL);
            runeImage.texture = runeTexture;
        }
        runeImage.gameObject.SetActive(true);
    }
    public static void SetupStars(Transform gridTransform, int star)
    {
        Texture lv1Texture =
            Resources.Load<Texture>(ImageConstants.Star.STAR_LV1_URL);

        if (star <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                RawImage image = gridTransform.GetChild(i).GetComponent<RawImage>();

                image.texture = lv1Texture;
                image.color = Color.black;
            }

            return;
        }

        int starLevel = ((star - 1) / 10) + 1;

        int currentLevelStarCount = star % 10;

        if (currentLevelStarCount == 0)
            currentLevelStarCount = 10;

        Texture currentLevelTexture =
            Resources.Load<Texture>(GetStarPath(starLevel));

        Texture previousLevelTexture =
            starLevel > 1
                ? Resources.Load<Texture>(GetStarPath(starLevel - 1))
                : lv1Texture;

        bool useBlackForRemaining = star < 10;

        for (int i = 0; i < 10; i++)
        {
            RawImage image = gridTransform.GetChild(i).GetComponent<RawImage>();

            if (i < currentLevelStarCount)
            {
                image.texture = currentLevelTexture;
                image.color = Color.white;
            }
            else
            {
                image.texture = previousLevelTexture;

                image.color = useBlackForRemaining
                    ? Color.black
                    : Color.white;
            }
        }
    }
}
