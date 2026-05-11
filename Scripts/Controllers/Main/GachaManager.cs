using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using System;
using System.Threading.Tasks;
using TMPro;

public class GachaManager : MonoBehaviour
{
    public static GachaManager Instance { get; private set; }
    private GameObject MainButtonPrefab;
    Texture2D itemBackground;
    Texture2D subBackground;
    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    public void Initialize()
    {
        MainButtonPrefab = UIManager.Instance.Get("MainButtonPrefab");
        // TabButtonPrefab = UIManager.Instance.Get("TabButtonPrefab");
        // AdvancedButtonPrefab = UIManager.Instance.Get("AdvancedButtonPrefab");
        // AdvancedSubButtonPrefab = UIManager.Instance.Get("AdvancedSubButtonPrefab");
        // PopupMenuPanelPrefab = UIManager.Instance.Get("PopupMenuPanelPrefab");
        // FeatureButtonPrefab = UIManager.Instance.Get("FeatureButtonPrefab");
        // MainPanel = UIManager.Instance.GetTransform("MainPanel");
    }
    public void CreateGachaButton(Transform galleryMenuPanel)
    {
        subBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Flag.FLAG_INVENTORY_URL);
        itemBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Badge.BADGE_GALLERY_URL);
        //Gallery menu
        CreateGachaButtonUI(1, AppDisplayConstants.Gallery.CARD_HEROES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_HERO_URL), galleryMenuPanel);
        CreateGachaButtonUI(2, AppDisplayConstants.Gallery.BOOKS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BOOK_URL), galleryMenuPanel);
        CreateGachaButtonUI(3, AppDisplayConstants.Gallery.PETS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PET_URL), galleryMenuPanel);
        CreateGachaButtonUI(4, AppDisplayConstants.Gallery.CARD_CAPTAINS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_CAPTAIN_URL), galleryMenuPanel);
        CreateGachaButtonUI(5, AppDisplayConstants.Gallery.COLLABORATION_EQUIPMENTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_EQUIPMENT_URL), galleryMenuPanel);
        CreateGachaButtonUI(6, AppDisplayConstants.Gallery.CARD_MILITARIES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MILITARY_URL), galleryMenuPanel);
        CreateGachaButtonUI(7, AppDisplayConstants.Gallery.CARD_SPELL_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SPELL_URL), galleryMenuPanel);
        CreateGachaButtonUI(8, AppDisplayConstants.Gallery.COLLABORATIONS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_URL), galleryMenuPanel);
        CreateGachaButtonUI(9, AppDisplayConstants.Gallery.CARD_MONSTERS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MONSTER_URL), galleryMenuPanel);
        CreateGachaButtonUI(10, AppDisplayConstants.Gallery.EQUIPMENTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EQUIPMENT_URL), galleryMenuPanel);
        CreateGachaButtonUI(11, AppDisplayConstants.Gallery.MEDALS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MEDAL_URL), galleryMenuPanel);
        CreateGachaButtonUI(12, AppDisplayConstants.Gallery.SKILLS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SKILL_URL), galleryMenuPanel);
        CreateGachaButtonUI(13, AppDisplayConstants.Gallery.SYMBOLS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SYMBOL_URL), galleryMenuPanel);
        CreateGachaButtonUI(14, AppDisplayConstants.Gallery.TITLES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TITLE_URL), galleryMenuPanel);
        CreateGachaButtonUI(15, AppDisplayConstants.Gallery.MAGIC_FORMATION_CRICLES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MAGIC_FORMATION_CIRCLE_URL), galleryMenuPanel);
        CreateGachaButtonUI(16, AppDisplayConstants.Gallery.RELICS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RELIC_URL), galleryMenuPanel);
        CreateGachaButtonUI(17, AppDisplayConstants.Gallery.CARD_COLONELS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_COLONEL_URL), galleryMenuPanel);
        CreateGachaButtonUI(18, AppDisplayConstants.Gallery.CARD_GENERALS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_GENERAL_URL), galleryMenuPanel);
        CreateGachaButtonUI(19, AppDisplayConstants.Gallery.CARD_ADMIRALS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_ADMIRAL_URL), galleryMenuPanel);
        CreateGachaButtonUI(20, AppDisplayConstants.Gallery.BORDERS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BORDER_URL), galleryMenuPanel);
        CreateGachaButtonUI(21, AppDisplayConstants.Gallery.TALISMANS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TALISMAN_URL), galleryMenuPanel);
        CreateGachaButtonUI(22, AppDisplayConstants.Gallery.PUPPETS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PUPPET_URL), galleryMenuPanel);
        CreateGachaButtonUI(23, AppDisplayConstants.Gallery.ALCHEMIES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ALCHEMY_URL), galleryMenuPanel);
        CreateGachaButtonUI(24, AppDisplayConstants.Gallery.FORGES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FORGE_URL), galleryMenuPanel);
        CreateGachaButtonUI(25, AppDisplayConstants.Gallery.CARD_LIVES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.LIFE_URL), galleryMenuPanel);
        CreateGachaButtonUI(26, AppDisplayConstants.Gallery.ARTWORKS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTWORK_URL), galleryMenuPanel);
        CreateGachaButtonUI(27, AppDisplayConstants.Gallery.SPIRIT_BEASTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_BEAST_URL), galleryMenuPanel);
        CreateGachaButtonUI(28, AppDisplayConstants.Gallery.AVATARS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.AVATAR_URL), galleryMenuPanel);
        CreateGachaButtonUI(29, AppDisplayConstants.Gallery.SPIRIT_CARDS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_CARD_URL), galleryMenuPanel);
        CreateGachaButtonUI(30, AppDisplayConstants.Gallery.ACHIEVEMENTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ACHIEVEMENT_URL), galleryMenuPanel);
        CreateGachaButtonUI(31, AppDisplayConstants.Gallery.ARTIFACTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTIFACT_URL), galleryMenuPanel);
        CreateGachaButtonUI(32, AppDisplayConstants.Gallery.ARCHITECTURES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARCHITECTURE_URL), galleryMenuPanel);
        CreateGachaButtonUI(33, AppDisplayConstants.Gallery.TECHNOLOGIES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TECHNOLOGY_URL), galleryMenuPanel);
        CreateGachaButtonUI(34, AppDisplayConstants.Gallery.VEHICLES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.VEHICLE_URL), galleryMenuPanel);
        CreateGachaButtonUI(35, AppDisplayConstants.Gallery.CORES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CORE_URL), galleryMenuPanel);
        CreateGachaButtonUI(36, AppDisplayConstants.Gallery.WEAPONS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.WEAPON_URL), galleryMenuPanel);
        CreateGachaButtonUI(37, AppDisplayConstants.Gallery.ROBOTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ROBOT_URL), galleryMenuPanel);
        CreateGachaButtonUI(38, AppDisplayConstants.Gallery.BADGES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BADGE_URL), galleryMenuPanel);
        CreateGachaButtonUI(39, AppDisplayConstants.Gallery.MECHA_BEASTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MECHA_BEAST_URL), galleryMenuPanel);
        CreateGachaButtonUI(40, AppDisplayConstants.Gallery.RUNES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RUNE_URL), galleryMenuPanel);
        CreateGachaButtonUI(41, AppDisplayConstants.Gallery.FURNITURES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BADGE_URL), galleryMenuPanel);
        CreateGachaButtonUI(42, AppDisplayConstants.Gallery.FOODS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MECHA_BEAST_URL), galleryMenuPanel);
        CreateGachaButtonUI(43, AppDisplayConstants.Gallery.BEVERAGES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RUNE_URL), galleryMenuPanel);
        CreateGachaButtonUI(44, AppDisplayConstants.Gallery.BUILDINGS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BUILDING_URL), galleryMenuPanel);
        CreateGachaButtonUI(45, AppDisplayConstants.Gallery.PLANTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PLANT_URL), galleryMenuPanel);
        CreateGachaButtonUI(46, AppDisplayConstants.Gallery.FASHIONS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FASHION_URL), galleryMenuPanel);
        CreateGachaButtonUI(47, AppDisplayConstants.Gallery.EMOJIS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EMOJI_URL), galleryMenuPanel);

        galleryMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateGachaButtonUI(int index, string itemName, Texture2D _itemBackground, Texture2D _subBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(MainButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage itemBackground = transform.Find("ItemBackground").GetComponent<RawImage>();
        if (itemBackground != null && _itemBackground != null)
        {
            itemBackground.texture = _itemBackground;
        }

        RawImage subBackground = transform.Find("SubBackground").GetComponent<RawImage>();
        if (subBackground != null && _subBackground != null)
        {
            subBackground.texture = _subBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    // public void CreateGallery(Transform GalleryMenuPanel)
    // {
    //     galleryMenuPanel = GalleryMenuPanel;
    //     DictionaryPanelPrefab = UIManager.Instance.Get("DictionaryPanelPrefab");
    //     UI_Blue_Gradient_Radius_Mat_MaskPercent_70 = MaterialManager.Instance.Get("UI_Blue_Gradient_Radius_Mat_MaskPercent_70");
    //     MainPanel = UIManager.Instance.GetTransform("MainPanel");

    //     AssignButtonEvent("Button_1", () => GetType(AppConstants.MainType.CARD_HERO));
    //     AssignButtonEvent("Button_2", () => GetType(AppConstants.MainType.BOOK));
    //     AssignButtonEvent("Button_3", () => GetType(AppConstants.MainType.PET));
    //     AssignButtonEvent("Button_4", () => GetType(AppConstants.MainType.CARD_CAPTAIN));
    //     AssignButtonEvent("Button_5", () => GetType(AppConstants.MainType.COLLABORATION_EQUIPMENT));
    //     AssignButtonEvent("Button_6", () => GetType(AppConstants.MainType.CARD_MILITARY));
    //     AssignButtonEvent("Button_7", () => GetType(AppConstants.MainType.CARD_SPELL));
    //     AssignButtonEvent("Button_8", () => GetType(AppConstants.MainType.COLLABORATION));
    //     AssignButtonEvent("Button_9", () => GetType(AppConstants.MainType.CARD_MONSTER));
    //     AssignButtonEvent("Button_10", () => GetType(AppConstants.MainType.EQUIPMENT));
    //     AssignButtonEvent("Button_11", () => GetType(AppConstants.MainType.MEDAL));
    //     AssignButtonEvent("Button_12", () => GetType(AppConstants.MainType.SKILL));
    //     AssignButtonEvent("Button_13", () => GetType(AppConstants.MainType.SYMBOL));
    //     AssignButtonEvent("Button_14", () => GetType(AppConstants.MainType.TITLE));
    //     AssignButtonEvent("Button_15", () => GetType(AppConstants.MainType.MAGIC_FORMATION_CIRCLE));
    //     AssignButtonEvent("Button_16", () => GetType(AppConstants.MainType.RELIC));
    //     AssignButtonEvent("Button_17", () => GetType(AppConstants.MainType.CARD_COLONEL));
    //     AssignButtonEvent("Button_18", () => GetType(AppConstants.MainType.CARD_GENERAL));
    //     AssignButtonEvent("Button_19", () => GetType(AppConstants.MainType.CARD_ADMIRAL));
    //     AssignButtonEvent("Button_20", () => GetType(AppConstants.MainType.BORDER));
    //     AssignButtonEvent("Button_21", () => GetType(AppConstants.MainType.TALISMAN));
    //     AssignButtonEvent("Button_22", () => GetType(AppConstants.MainType.PUPPET));
    //     AssignButtonEvent("Button_23", () => GetType(AppConstants.MainType.ALCHEMY));
    //     AssignButtonEvent("Button_24", () => GetType(AppConstants.MainType.FORGE));
    //     AssignButtonEvent("Button_25", () => GetType(AppConstants.MainType.CARD_LIFE));
    //     AssignButtonEvent("Button_26", () => GetType(AppConstants.MainType.ARTWORK));
    //     AssignButtonEvent("Button_27", () => GetType(AppConstants.MainType.SPIRIT_BEAST));
    //     AssignButtonEvent("Button_28", () => GetType(AppConstants.MainType.AVATAR));
    //     AssignButtonEvent("Button_29", () => GetType(AppConstants.MainType.SPIRIT_CARD));
    //     AssignButtonEvent("Button_30", () => GetType(AppConstants.MainType.ACHIEVEMENT));
    //     AssignButtonEvent("Button_31", () => GetType(AppConstants.MainType.ARTIFACT));
    //     AssignButtonEvent("Button_32", () => GetType(AppConstants.MainType.ARCHITECTURE));
    //     AssignButtonEvent("Button_33", () => GetType(AppConstants.MainType.TECHNOLOGY));
    //     AssignButtonEvent("Button_34", () => GetType(AppConstants.MainType.VEHICLE));
    //     AssignButtonEvent("Button_35", () => GetType(AppConstants.MainType.CORE));
    //     AssignButtonEvent("Button_36", () => GetType(AppConstants.MainType.WEAPON));
    //     AssignButtonEvent("Button_37", () => GetType(AppConstants.MainType.ROBOT));
    //     AssignButtonEvent("Button_38", () => GetType(AppConstants.MainType.BADGE));
    //     AssignButtonEvent("Button_39", () => GetType(AppConstants.MainType.MECHA_BEAST));
    //     AssignButtonEvent("Button_40", () => GetType(AppConstants.MainType.RUNE));
    //     AssignButtonEvent("Button_41", () => GetType(AppConstants.MainType.FURNITURE));
    //     AssignButtonEvent("Button_42", () => GetType(AppConstants.MainType.FOOD));
    //     AssignButtonEvent("Button_43", () => GetType(AppConstants.MainType.BEVERAGE));
    //     AssignButtonEvent("Button_44", () => GetType(AppConstants.MainType.BUILDING));
    //     AssignButtonEvent("Button_45", () => GetType(AppConstants.MainType.PLANT));
    //     AssignButtonEvent("Button_46", () => GetType(AppConstants.MainType.FASHION));
    //     AssignButtonEvent("Button_47", () => GetType(AppConstants.MainType.EMOJI));
    //     // GetCardsType();
    // }
    // void AssignButtonEvent(string buttonName, UnityEngine.Events.UnityAction action)
    // {
    //     Transform buttonTransform = galleryMenuPanel.Find(buttonName);
    //     if (buttonTransform != null)
    //     {
    //         Button button = buttonTransform.GetComponent<Button>();
    //         if (button != null)
    //         {
    //             button.onClick.AddListener(() =>
    //             {
    //                 AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
    //                 action();
    //             });
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogWarning($"Button {buttonName} not found!");
    //     }
    // }
}