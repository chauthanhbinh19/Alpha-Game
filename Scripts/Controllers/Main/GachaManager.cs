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
    private Transform MainPanel;
    public GameObject GachaPanelPrefab;
    private GameObject MainButtonPrefab;
    public Transform gachaMenuPanel;
    Texture2D itemBackground;
    Texture2D subBackground;
    private string mainType;
    private TextMeshProUGUI titleText;
    List<Items> items;
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
        GachaPanelPrefab = UIManager.Instance.Get("GachaPanelPrefab");
        // TabButtonPrefab = UIManager.Instance.Get("TabButtonPrefab");
        // AdvancedButtonPrefab = UIManager.Instance.Get("AdvancedButtonPrefab");
        // AdvancedSubButtonPrefab = UIManager.Instance.Get("AdvancedSubButtonPrefab");
        // PopupMenuPanelPrefab = UIManager.Instance.Get("PopupMenuPanelPrefab");
        // FeatureButtonPrefab = UIManager.Instance.Get("FeatureButtonPrefab");
        // MainPanel = UIManager.Instance.GetTransform("MainPanel");
    }
    public void CreateGachaButton(Transform tempGachaMenuPanel)
    {
        subBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Flag.FLAG_INVENTORY_URL);
        itemBackground = TextureHelper.LoadTexture2DCached(ImageConstants.Badge.BADGE_GALLERY_URL);
        //Gallery menu
        CreateGachaButtonUI(1, AppDisplayConstants.Gallery.CARD_HEROES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_HERO_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(2, AppDisplayConstants.Gallery.BOOKS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BOOK_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(3, AppDisplayConstants.Gallery.PETS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PET_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(4, AppDisplayConstants.Gallery.CARD_CAPTAINS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_CAPTAIN_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(5, AppDisplayConstants.Gallery.COLLABORATION_EQUIPMENTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_EQUIPMENT_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(6, AppDisplayConstants.Gallery.CARD_MILITARIES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MILITARY_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(7, AppDisplayConstants.Gallery.CARD_SPELL_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SPELL_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(8, AppDisplayConstants.Gallery.COLLABORATIONS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(9, AppDisplayConstants.Gallery.CARD_MONSTERS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MONSTER_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(10, AppDisplayConstants.Gallery.EQUIPMENTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EQUIPMENT_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(11, AppDisplayConstants.Gallery.MEDALS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MEDAL_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(12, AppDisplayConstants.Gallery.SKILLS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SKILL_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(13, AppDisplayConstants.Gallery.SYMBOLS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SYMBOL_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(14, AppDisplayConstants.Gallery.TITLES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TITLE_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(15, AppDisplayConstants.Gallery.MAGIC_FORMATION_CRICLES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MAGIC_FORMATION_CIRCLE_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(16, AppDisplayConstants.Gallery.RELICS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RELIC_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(17, AppDisplayConstants.Gallery.CARD_COLONELS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_COLONEL_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(18, AppDisplayConstants.Gallery.CARD_GENERALS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_GENERAL_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(19, AppDisplayConstants.Gallery.CARD_ADMIRALS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_ADMIRAL_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(20, AppDisplayConstants.Gallery.BORDERS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BORDER_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(21, AppDisplayConstants.Gallery.TALISMANS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TALISMAN_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(22, AppDisplayConstants.Gallery.PUPPETS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PUPPET_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(23, AppDisplayConstants.Gallery.ALCHEMIES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ALCHEMY_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(24, AppDisplayConstants.Gallery.FORGES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FORGE_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(25, AppDisplayConstants.Gallery.CARD_LIVES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.LIFE_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(26, AppDisplayConstants.Gallery.ARTWORKS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTWORK_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(27, AppDisplayConstants.Gallery.SPIRIT_BEASTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_BEAST_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(28, AppDisplayConstants.Gallery.AVATARS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.AVATAR_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(29, AppDisplayConstants.Gallery.SPIRIT_CARDS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SPIRIT_CARD_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(30, AppDisplayConstants.Gallery.ACHIEVEMENTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ACHIEVEMENT_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(31, AppDisplayConstants.Gallery.ARTIFACTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARTIFACT_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(32, AppDisplayConstants.Gallery.ARCHITECTURES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ARCHITECTURE_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(33, AppDisplayConstants.Gallery.TECHNOLOGIES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TECHNOLOGY_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(34, AppDisplayConstants.Gallery.VEHICLES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.VEHICLE_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(35, AppDisplayConstants.Gallery.CORES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CORE_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(36, AppDisplayConstants.Gallery.WEAPONS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.WEAPON_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(37, AppDisplayConstants.Gallery.ROBOTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ROBOT_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(38, AppDisplayConstants.Gallery.BADGES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BADGE_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(39, AppDisplayConstants.Gallery.MECHA_BEASTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MECHA_BEAST_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(40, AppDisplayConstants.Gallery.RUNES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RUNE_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(41, AppDisplayConstants.Gallery.FURNITURES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BADGE_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(42, AppDisplayConstants.Gallery.FOODS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MECHA_BEAST_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(43, AppDisplayConstants.Gallery.BEVERAGES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.RUNE_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(44, AppDisplayConstants.Gallery.BUILDINGS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.BUILDING_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(45, AppDisplayConstants.Gallery.PLANTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.PLANT_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(46, AppDisplayConstants.Gallery.FASHIONS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.FASHION_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(47, AppDisplayConstants.Gallery.EMOJIS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EMOJI_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(48, AppDisplayConstants.Gallery.CARD_SOLDIERS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SOLDIER_URL), tempGachaMenuPanel);

        tempGachaMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
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
    public void CreateGallery(Transform tempGachaMenuPanel)
    {
        gachaMenuPanel = tempGachaMenuPanel;
        // DictionaryPanelPrefab = UIManager.Instance.Get("DictionaryPanelPrefab");
        // UI_Blue_Gradient_Radius_Mat_MaskPercent_70 = MaterialManager.Instance.Get("UI_Blue_Gradient_Radius_Mat_MaskPercent_70");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");

        AssignButtonEvent("Button_1", () => GetType(AppConstants.MainType.CARD_HERO));
        AssignButtonEvent("Button_2", () => GetType(AppConstants.MainType.BOOK));
        AssignButtonEvent("Button_3", () => GetType(AppConstants.MainType.PET));
        AssignButtonEvent("Button_4", () => GetType(AppConstants.MainType.CARD_CAPTAIN));
        AssignButtonEvent("Button_5", () => GetType(AppConstants.MainType.COLLABORATION_EQUIPMENT));
        AssignButtonEvent("Button_6", () => GetType(AppConstants.MainType.CARD_MILITARY));
        AssignButtonEvent("Button_7", () => GetType(AppConstants.MainType.CARD_SPELL));
        AssignButtonEvent("Button_8", () => GetType(AppConstants.MainType.COLLABORATION));
        AssignButtonEvent("Button_9", () => GetType(AppConstants.MainType.CARD_MONSTER));
        AssignButtonEvent("Button_10", () => GetType(AppConstants.MainType.EQUIPMENT));
        AssignButtonEvent("Button_11", () => GetType(AppConstants.MainType.MEDAL));
        AssignButtonEvent("Button_12", () => GetType(AppConstants.MainType.SKILL));
        AssignButtonEvent("Button_13", () => GetType(AppConstants.MainType.SYMBOL));
        AssignButtonEvent("Button_14", () => GetType(AppConstants.MainType.TITLE));
        AssignButtonEvent("Button_15", () => GetType(AppConstants.MainType.MAGIC_FORMATION_CIRCLE));
        AssignButtonEvent("Button_16", () => GetType(AppConstants.MainType.RELIC));
        AssignButtonEvent("Button_17", () => GetType(AppConstants.MainType.CARD_COLONEL));
        AssignButtonEvent("Button_18", () => GetType(AppConstants.MainType.CARD_GENERAL));
        AssignButtonEvent("Button_19", () => GetType(AppConstants.MainType.CARD_ADMIRAL));
        AssignButtonEvent("Button_20", () => GetType(AppConstants.MainType.BORDER));
        AssignButtonEvent("Button_21", () => GetType(AppConstants.MainType.TALISMAN));
        AssignButtonEvent("Button_22", () => GetType(AppConstants.MainType.PUPPET));
        AssignButtonEvent("Button_23", () => GetType(AppConstants.MainType.ALCHEMY));
        AssignButtonEvent("Button_24", () => GetType(AppConstants.MainType.FORGE));
        AssignButtonEvent("Button_25", () => GetType(AppConstants.MainType.CARD_LIFE));
        AssignButtonEvent("Button_26", () => GetType(AppConstants.MainType.ARTWORK));
        AssignButtonEvent("Button_27", () => GetType(AppConstants.MainType.SPIRIT_BEAST));
        AssignButtonEvent("Button_28", () => GetType(AppConstants.MainType.AVATAR));
        AssignButtonEvent("Button_29", () => GetType(AppConstants.MainType.SPIRIT_CARD));
        AssignButtonEvent("Button_30", () => GetType(AppConstants.MainType.ACHIEVEMENT));
        AssignButtonEvent("Button_31", () => GetType(AppConstants.MainType.ARTIFACT));
        AssignButtonEvent("Button_32", () => GetType(AppConstants.MainType.ARCHITECTURE));
        AssignButtonEvent("Button_33", () => GetType(AppConstants.MainType.TECHNOLOGY));
        AssignButtonEvent("Button_34", () => GetType(AppConstants.MainType.VEHICLE));
        AssignButtonEvent("Button_35", () => GetType(AppConstants.MainType.CORE));
        AssignButtonEvent("Button_36", () => GetType(AppConstants.MainType.WEAPON));
        AssignButtonEvent("Button_37", () => GetType(AppConstants.MainType.ROBOT));
        AssignButtonEvent("Button_38", () => GetType(AppConstants.MainType.BADGE));
        AssignButtonEvent("Button_39", () => GetType(AppConstants.MainType.MECHA_BEAST));
        AssignButtonEvent("Button_40", () => GetType(AppConstants.MainType.RUNE));
        AssignButtonEvent("Button_41", () => GetType(AppConstants.MainType.FURNITURE));
        AssignButtonEvent("Button_42", () => GetType(AppConstants.MainType.FOOD));
        AssignButtonEvent("Button_43", () => GetType(AppConstants.MainType.BEVERAGE));
        AssignButtonEvent("Button_44", () => GetType(AppConstants.MainType.BUILDING));
        AssignButtonEvent("Button_45", () => GetType(AppConstants.MainType.PLANT));
        AssignButtonEvent("Button_46", () => GetType(AppConstants.MainType.FASHION));
        AssignButtonEvent("Button_47", () => GetType(AppConstants.MainType.EMOJI));
        // GetCardsType();
    }
    void AssignButtonEvent(string buttonName, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = gachaMenuPanel.Find(buttonName);
        if (buttonTransform != null)
        {
            Button button = buttonTransform.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    action();
                });
            }
        }
        else
        {
            Debug.LogWarning($"Button {buttonName} not found!");
        }
    }
    public void GetType(string type)
    {
        mainType = type;
        // _ = GetButtonTypeAsync();
        titleText.text = LocalizationManager.Get(type);
    }
    public void CreateGachaManager()
    {
        GameObject gachaObject = Instantiate(GachaPanelPrefab, MainPanel);
        Transform transform = gachaObject.transform;
        Button summonOneButton = transform.Find("SummonOneButton").GetComponent<Button>();
        Button summonTenButton = transform.Find("SummonTenButton").GetComponent<Button>();

        summonOneButton.onClick.AddListener(() =>
        {
            
        });
        summonTenButton.onClick.AddListener(() =>
        {
            
        });
    }
    public async Task LoadCurrencyAsync(Transform transform)
    {
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_HERO_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BOOK_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_CAPTAIN_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MONSTER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_COLONEL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_GENERAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_ADMIRAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MILITARY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_SPELL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.COLLABORATION_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.COLLABORATION_EQUIPMENT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.EQUIPMENT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.PET_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SKILL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SYMBOL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.MEDAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.TITLE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BORDER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.MAGIC_FORMATION_CIRCLE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.RELIC_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.TALISMAN_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.PUPPET_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ALCHEMY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FORGE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_LIFE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ARTWORK_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SPIRIT_BEAST_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.AVATAR_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SPIRIT_CARD_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        // else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        // {
        //     items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ACHIE_TICKET) };
        //     CurrenciesManager.Instance.GetTicketsCurrency(
        //         items,
        //         transform
        //     );

        //     CreateTicketUI(items, transform);
        // }
        else if (mainType.Equals(AppConstants.MainType.ARTIFACT))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ARTIFACT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ARCHITECTURE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.TECHNOLOGY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.VEHICLE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CORE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.WEAPON_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ROBOT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BADGE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.MECHA_BEAST_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.RUNE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.FURNITURE))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FURNITURE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.FOOD))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FOOD_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BEVERAGE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BUILDING))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BUILDING_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.PLANT))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.PLANT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.FASHION))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FASHION_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.EMOJI))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.EMOJI_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SOLDIER))
        {
            items = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_SOLDIER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                items,
                transform
            );

            CreateTicketUI(items, transform);
        }
    }
    public void LoadGachaEvent()
    {
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.ARTIFACT))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.FURNITURE))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.FOOD))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            
        }
        else if (mainType.Equals(AppConstants.MainType.BUILDING))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.PLANT))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.FASHION))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.EMOJI))
        {

        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SOLDIER))
        {

        }
    }
    public void CreateTicketUI(List<Items> items, Transform transform)
    {
        foreach (Items item in items)
        {
            RawImage oneTicketImage = transform.Find("DictionaryCards/OneTicketImage").GetComponent<RawImage>();
            RawImage tenTicketImage = transform.Find("DictionaryCards/TenTicketImage").GetComponent<RawImage>();
            TextMeshProUGUI oneTicketText = transform.Find("DictionaryCards/OneTicketText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI tenTicketText = transform.Find("DictionaryCards/TenTicketText").GetComponent<TextMeshProUGUI>();

            string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(item.Image);
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            oneTicketImage.texture = texture;
            tenTicketImage.texture = texture;
            oneTicketText.text = "1";
            tenTicketText.text = "10";
        }
    }
}