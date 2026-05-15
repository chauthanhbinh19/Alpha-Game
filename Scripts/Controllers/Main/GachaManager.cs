using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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
    List<Items> tickets;
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
        CreateGachaButtonUI(7, AppDisplayConstants.Gallery.CARD_SPELLS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_SPELL_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(8, AppDisplayConstants.Gallery.COLLABORATIONS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.COLLABORATION_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(9, AppDisplayConstants.Gallery.CARD_MONSTERS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.CARD_MONSTER_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(10, AppDisplayConstants.Gallery.EQUIPMENTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.EQUIPMENT_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(11, AppDisplayConstants.Gallery.MEDALS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MEDAL_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(12, AppDisplayConstants.Gallery.SKILLS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SKILL_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(13, AppDisplayConstants.Gallery.SYMBOLS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.SYMBOL_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(14, AppDisplayConstants.Gallery.TITLES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.TITLE_URL), tempGachaMenuPanel);
        CreateGachaButtonUI(15, AppDisplayConstants.Gallery.MAGIC_FORMATION_CIRCLES_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.MAGIC_FORMATION_CIRCLE_URL), tempGachaMenuPanel);
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
        // CreateGachaButtonUI(30, AppDisplayConstants.Gallery.ACHIEVEMENTS_GALLERY, subBackground, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Gallery.ACHIEVEMENT_URL), tempGachaMenuPanel);
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
        // AssignButtonEvent("Button_30", () => GetType(AppConstants.MainType.ACHIEVEMENT));
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
        titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        Button summonOneButton = transform.Find("DictionaryCards/SummonOneButton").GetComponent<Button>();
        Button summonTenButton = transform.Find("DictionaryCards/SummonTenButton").GetComponent<Button>();
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(gachaObject);
        });
        Button homeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        summonOneButton.onClick.AddListener(async () =>
        {
            await LoadGachaEventAsync(1);
        });
        summonTenButton.onClick.AddListener(async () =>
        {
            await LoadGachaEventAsync(10);
        });
    }
    public async Task LoadTicketAsync(Transform transform)
    {
        tickets.Clear();
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_HERO_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BOOK_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_CAPTAIN_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MONSTER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_COLONEL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_GENERAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_ADMIRAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_MILITARY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_SPELL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.COLLABORATION_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.COLLABORATION_EQUIPMENT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.EQUIPMENT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.PET_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SKILL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SYMBOL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.MEDAL))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.MEDAL_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.TITLE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.TITLE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BORDER))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BORDER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.MAGIC_FORMATION_CIRCLE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.RELIC_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.TALISMAN_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.PUPPET_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ALCHEMY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FORGE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_LIFE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ARTWORK_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SPIRIT_BEAST_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.AVATAR))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.AVATAR_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_CARD))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.SPIRIT_CARD_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
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
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ARTIFACT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ARCHITECTURE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.TECHNOLOGY_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.VEHICLE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CORE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.WEAPON_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.ROBOT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BADGE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.MECHA_BEAST_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.RUNE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.FURNITURE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FURNITURE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.FOOD))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FOOD_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BEVERAGE))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BEVERAGE_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.BUILDING))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.BUILDING_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.PLANT))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.PLANT_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.FASHION))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.FASHION_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.EMOJI))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.EMOJI_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SOLDIER))
        {
            tickets = new List<Items> { await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.Ticket.CARD_SOLDIER_TICKET) };
            CurrenciesManager.Instance.GetTicketsCurrency(
                tickets,
                transform
            );

            CreateTicketUI(tickets, transform);
        }
    }
    public async Task LoadGachaEventAsync(int rollNumber)
    {
        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            await GachaCardHeroesAsync(rollNumber);
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
    public async Task GachaCardHeroesAsync(int rollNumber)
    {
        // Load data 1 lần
        var allCardHeroes = await CardHeroesService.Create()
            .GetCardHeroesWithoutLimitAsync();

        var allItems = await ItemsService.Create()
            .GetItemsAsync();

        // Load rates từ config mới
        var mainRates = await GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.CARD_HERO);

        var cardTypeRates = await GachaRatesConfig
            .GetCardTypeRatesAsync(AppConstants.MainType.CARD_HERO);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserCurrenciesService.Create()
                .UpdateUserCurrencyAsync(item.Id, rollNumber);
        }

        // Group theo type
        var cardByType = allCardHeroes
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        var itemByType = allItems
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        List<CardHeroes> rewardedCards = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.CARD_HERO)
            {
                // Tier 2
                string cardType = RollHelper.RollByRate(
                    cardTypeRates
                );

                if (cardByType.TryGetValue(
                    cardType,
                    out var filteredCards
                ) && filteredCards.Any())
                {
                    var selectedCard = filteredCards[
                        UnityEngine.Random.Range(
                            0,
                            filteredCards.Count
                        )
                    ];

                    rewardedCards.Add(selectedCard);
                }
            }
            else if (mainReward == AppConstants.MainType.ITEM)
            {
                string itemType = RollHelper.RollByRate(
                    itemTypeRates
                );

                if (itemByType.TryGetValue(
                    itemType,
                    out var filteredItems
                ) && filteredItems.Any())
                {
                    var selectedItem = filteredItems[
                        UnityEngine.Random.Range(
                            0,
                            filteredItems.Count
                        )
                    ];

                    if (rewardedItems.ContainsKey(
                        selectedItem.Id
                    ))
                    {
                        var current =
                            rewardedItems[selectedItem.Id];

                        rewardedItems[selectedItem.Id] = (
                            current.item,
                            current.quantity + 1
                        );
                    }
                    else
                    {
                        rewardedItems[selectedItem.Id] = (
                            selectedItem,
                            1
                        );
                    }
                }
            }
        }

        // giữ nguyên insert card
        if (rewardedCards.Any())
        {
            await UserCardHeroesService.Create()
                .InsertOrUpdateUserCardHeroesBatchAsync(
                    rewardedCards
                );
        }

        if (rewardedItems.Any())
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemsBatchAsync(
                    rewardedItems.Values.ToList()
                );
        }
    }
    public async Task GachaRunesAsync(int rollNumber)
    {
        // Load data 1 lần
        var allRunes = await RunesService.Create()
            .GetRunesWithoutLimitAsync();

        var allItems = await ItemsService.Create()
            .GetItemsAsync();

        // Load rates
        var mainRates = await GachaRatesConfig
            .GetMainRatesAsync(AppConstants.MainType.RUNE);

        var itemTypeRates = await GachaRatesConfig
            .GetItemTypeRatesAsync();

        // Trừ ticket
        foreach (Items item in tickets)
        {
            await UserCurrenciesService.Create()
                .UpdateUserCurrencyAsync(item.Id, rollNumber);
        }

        // Item group theo type
        var itemByType = allItems
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        List<Runes> rewardedRunes = new();

        Dictionary<string, (Items item, double quantity)>
            rewardedItems = new();

        for (int i = 0; i < rollNumber; i++)
        {
            // Tier 1
            string mainReward = RollHelper.RollByRate(
                mainRates
            );

            if (mainReward == AppConstants.MainType.RUNE)
            {
                // random trực tiếp rune
                if (allRunes.Any())
                {
                    var selectedRune = allRunes[
                        UnityEngine.Random.Range(
                            0,
                            allRunes.Count
                        )
                    ];

                    rewardedRunes.Add(selectedRune);
                }
            }
            else if (mainReward == AppConstants.MainType.ITEM)
            {
                string itemType = RollHelper.RollByRate(
                    itemTypeRates
                );

                if (itemByType.TryGetValue(
                    itemType,
                    out var filteredItems
                ) && filteredItems.Any())
                {
                    var selectedItem = filteredItems[
                        UnityEngine.Random.Range(
                            0,
                            filteredItems.Count
                        )
                    ];

                    if (rewardedItems.ContainsKey(
                        selectedItem.Id
                    ))
                    {
                        var current =
                            rewardedItems[selectedItem.Id];

                        rewardedItems[selectedItem.Id] = (
                            current.item,
                            current.quantity + 1
                        );
                    }
                    else
                    {
                        rewardedItems[selectedItem.Id] = (
                            selectedItem,
                            1
                        );
                    }
                }
            }
        }

        // Insert rune
        if (rewardedRunes.Any())
        {
            await UserRunesService.Create()
                .InsertOrUpdateUserRunesBatchAsync(
                    rewardedRunes
                );
        }

        // Insert item
        if (rewardedItems.Any())
        {
            await UserItemsService.Create()
                .InsertOrUpdateUserItemsBatchAsync(
                    rewardedItems.Values.ToList()
                );
        }
    }
}