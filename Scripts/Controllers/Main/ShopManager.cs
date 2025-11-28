using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using System.Reflection;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform currentContent;
    private Transform currencyPanel;
    private GameObject ShopButtonPrefab;
    private GameObject ShopManagerPrefab;
    private GameObject currentObject;
    private GameObject ShopPrefab;
    private GameObject buttonPrefab;
    private GameObject equipmentsShopPrefab;
    private Transform popupPanel;
    private RawImage firstDecorationImage;
    private RawImage secondDecorationImage;
    private Button CloseButton;
    private Button HomeButton;
    private int offset;
    private int currentPage;
    private int totalPage;
    private int pageSize;
    private TextMeshProUGUI PageText;
    private Button NextButton;
    private Button PreviousButton;
    private string mainType;
    private string type;
    private Text titleText;
    private string rare;
    public static ShopManager Instance { get; private set; }
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
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        rare = "All";
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ShopButtonPrefab = UIManager.Instance.GetGameObject("ShopButtonPrefab");
        ShopManagerPrefab = UIManager.Instance.GetGameObject("ShopManagerPrefab");
        ShopPrefab = UIManager.Instance.GetGameObject("ShopPrefab");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        equipmentsShopPrefab = UIManager.Instance.GetGameObject("equipmentsShopPrefab");
        popupPanel = UIManager.Instance.GetTransform("popupPanel");
    }
    void AssignButtonEvent(string buttonName, Transform panel, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = panel.Find(buttonName);
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
    public void CreateShopButton()
    {
        currentObject = Instantiate(ShopManagerPrefab, MainPanel);
        titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        titleText.text = LocalizationManager.Get("shop");
        CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");

        List<Currencies> currencies = new List<Currencies>();
        currencies = UserCurrencyService.Create().GetUserCurrency(User.CurrentUserId);
        FindObjectOfType<CurrenciesManager>().GetMainCurrency(currencies, CurrencyPanel);

        Transform tempContent = currentObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        CreateButton(1, AppDisplayConstants.MainType.CARD_HEROES, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_HERO_URL), tempContent);
        CreateButton(2, AppDisplayConstants.MainType.BOOKS, Resources.Load<Texture2D>(ImageConstants.Gallery.BOOK_URL), tempContent);
        CreateButton(3, AppDisplayConstants.MainType.PETS, Resources.Load<Texture2D>(ImageConstants.Gallery.PET_URL), tempContent);
        CreateButton(4, AppDisplayConstants.MainType.CARD_CAPTAINS, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_CAPTAIN_URL), tempContent);
        CreateButton(5, AppDisplayConstants.MainType.COLLABORATION_EQUIPMENTS, Resources.Load<Texture2D>(ImageConstants.Gallery.COLLABORATION_EQUIPMENT_URL), tempContent);
        CreateButton(6, AppDisplayConstants.MainType.CARD_MILITARIES, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_MILITARY_URL), tempContent);
        CreateButton(7, AppDisplayConstants.MainType.CARD_SPELLS, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_SPELL_URL), tempContent);
        CreateButton(8, AppDisplayConstants.MainType.COLLABORATIONS, Resources.Load<Texture2D>(ImageConstants.Gallery.COLLABORATION_URL), tempContent);
        CreateButton(9, AppDisplayConstants.MainType.CARD_MONSTERS, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_MONSTER_URL), tempContent);
        CreateButton(10, AppDisplayConstants.MainType.BORDERS, Resources.Load<Texture2D>(ImageConstants.Gallery.BORDER_URL), tempContent);
        CreateButton(11, AppDisplayConstants.MainType.MEDALS, Resources.Load<Texture2D>(ImageConstants.Gallery.MEDAL_URL), tempContent);
        CreateButton(12, AppDisplayConstants.MainType.SKILLS, Resources.Load<Texture2D>(ImageConstants.Gallery.SKILL_URL), tempContent);
        CreateButton(13, AppDisplayConstants.MainType.SYMBOLS, Resources.Load<Texture2D>(ImageConstants.Gallery.SYMBOL_URL), tempContent);
        CreateButton(14, AppDisplayConstants.MainType.TITLES, Resources.Load<Texture2D>(ImageConstants.Gallery.TITLE_URL), tempContent);
        CreateButton(15, AppDisplayConstants.MainType.MAGIC_FORMATION_CIRCLES, Resources.Load<Texture2D>(ImageConstants.Gallery.MAGIC_FORMATION_CIRCLE_URL), tempContent);
        CreateButton(16, AppDisplayConstants.MainType.RELICS, Resources.Load<Texture2D>(ImageConstants.Gallery.RELIC_URL), tempContent);
        CreateButton(17, AppDisplayConstants.MainType.ARTWORKS, Resources.Load<Texture2D>(ImageConstants.Gallery.ARTWORK_URL), tempContent);
        CreateButton(18, AppDisplayConstants.MainType.ACHIEVEMENTS, Resources.Load<Texture2D>(ImageConstants.Gallery.ACHIEVEMENT_URL), tempContent);
        CreateButton(19, AppDisplayConstants.MainType.CARD_COLONELS, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_COLONEL_URL), tempContent);
        CreateButton(20, AppDisplayConstants.MainType.CARD_GENERALS, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_GENERAL_URL), tempContent);
        CreateButton(21, AppDisplayConstants.MainType.CARD_ADMIRALS, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_ADMIRAL_URL), tempContent);
        CreateButton(22, AppDisplayConstants.MainType.TALISMANS, Resources.Load<Texture2D>(ImageConstants.Gallery.TALISMAN_URL), tempContent);
        CreateButton(23, AppDisplayConstants.MainType.PUPPETS, Resources.Load<Texture2D>(ImageConstants.Gallery.PUPPET_URL), tempContent);
        CreateButton(24, AppDisplayConstants.MainType.ALCHEMIES, Resources.Load<Texture2D>(ImageConstants.Gallery.ALCHEMY_URL), tempContent);
        CreateButton(25, AppDisplayConstants.MainType.FORGES, Resources.Load<Texture2D>(ImageConstants.Gallery.FORGE_URL), tempContent);
        CreateButton(26, AppDisplayConstants.MainType.CARD_LIVES, Resources.Load<Texture2D>(ImageConstants.Gallery.LIFE_URL), tempContent);
        CreateButton(27, AppDisplayConstants.MainType.SPIRIT_BEASTS, Resources.Load<Texture2D>(ImageConstants.Gallery.SPIRIT_BEAST_URL), tempContent);
        CreateButton(28, AppDisplayConstants.MainType.SPIRIT_CARDS, Resources.Load<Texture2D>(ImageConstants.Gallery.SPIRIT_CARD_URL), tempContent);
        CreateButton(29, AppDisplayConstants.MainType.CARDS, Resources.Load<Texture2D>(ImageConstants.Gallery.CARD_URL), tempContent);
        CreateButton(30, AppDisplayConstants.MainType.ARCHITECTURES, Resources.Load<Texture2D>(ImageConstants.Gallery.ARCHITECTURE_URL), tempContent);
        CreateButton(31, AppDisplayConstants.MainType.TECHONOLOGIES, Resources.Load<Texture2D>(ImageConstants.Gallery.TECHNOLOGY_URL), tempContent);
        CreateButton(32, AppDisplayConstants.MainType.VEHICLES, Resources.Load<Texture2D>(ImageConstants.Gallery.VEHICLE_URL), tempContent);
        CreateButton(33, AppDisplayConstants.MainType.CORES, Resources.Load<Texture2D>(ImageConstants.Gallery.CORE_URL), tempContent);
        CreateButton(34, AppDisplayConstants.MainType.WEAPONS, Resources.Load<Texture2D>(ImageConstants.Gallery.WEAPON_URL), tempContent);
        CreateButton(35, AppDisplayConstants.MainType.ROBOTS, Resources.Load<Texture2D>(ImageConstants.Gallery.ROBOT_URL), tempContent);

        AssignButtonEvent("Button_1", tempContent, () => GetType(AppConstants.MainType.CARD_HERO));
        AssignButtonEvent("Button_2", tempContent, () => GetType(AppConstants.MainType.BOOK));
        AssignButtonEvent("Button_3", tempContent, () => GetType(AppConstants.MainType.PET));
        AssignButtonEvent("Button_4", tempContent, () => GetType(AppConstants.MainType.CARD_CAPTAIN));
        AssignButtonEvent("Button_5", tempContent, () => GetType(AppConstants.MainType.COLLABORATION_EQUIPMENT));
        AssignButtonEvent("Button_6", tempContent, () => GetType(AppConstants.MainType.CARD_MILITARY));
        AssignButtonEvent("Button_7", tempContent, () => GetType(AppConstants.MainType.CARD_SPELL));
        AssignButtonEvent("Button_8", tempContent, () => GetType(AppConstants.MainType.COLLABORATION));
        AssignButtonEvent("Button_9", tempContent, () => GetType(AppConstants.MainType.CARD_MONSTER));
        AssignButtonEvent("Button_10", tempContent, () => GetType(AppConstants.MainType.BORDER));
        AssignButtonEvent("Button_11", tempContent, () => GetType(AppConstants.MainType.MEDAL));
        AssignButtonEvent("Button_12", tempContent, () => GetType(AppConstants.MainType.SKILL));
        AssignButtonEvent("Button_13", tempContent, () => GetType(AppConstants.MainType.SYMBOL));
        AssignButtonEvent("Button_14", tempContent, () => GetType(AppConstants.MainType.TITLE));
        AssignButtonEvent("Button_15", tempContent, () => GetType(AppConstants.MainType.MAGIC_FORMATION_CIRCLE));
        AssignButtonEvent("Button_16", tempContent, () => GetType(AppConstants.MainType.RELIC));
        AssignButtonEvent("Button_17", tempContent, () => GetType(AppConstants.MainType.ARTWORK));
        AssignButtonEvent("Button_18", tempContent, () => GetType(AppConstants.MainType.ACHIEVEMENT));
        AssignButtonEvent("Button_19", tempContent, () => GetType(AppConstants.MainType.CARD_COLONEL));
        AssignButtonEvent("Button_20", tempContent, () => GetType(AppConstants.MainType.CARD_GENERAL));
        AssignButtonEvent("Button_21", tempContent, () => GetType(AppConstants.MainType.CARD_ADMIRAL));
        AssignButtonEvent("Button_22", tempContent, () => GetType(AppConstants.MainType.TALISMAN));
        AssignButtonEvent("Button_23", tempContent, () => GetType(AppConstants.MainType.PUPPET));
        AssignButtonEvent("Button_24", tempContent, () => GetType(AppConstants.MainType.ALCHEMY));
        AssignButtonEvent("Button_25", tempContent, () => GetType(AppConstants.MainType.FORGE));
        AssignButtonEvent("Button_26", tempContent, () => GetType(AppConstants.MainType.CARD_LIFE));
        AssignButtonEvent("Button_27", tempContent, () => GetType(AppConstants.MainType.SPIRIT_BEAST));
        AssignButtonEvent("Button_28", tempContent, () => GetType(AppConstants.MainType.SPIRIT_CARD));
        AssignButtonEvent("Button_29", tempContent, () => GetType(AppConstants.MainType.CARD));
        AssignButtonEvent("Button_30", tempContent, () => GetType(AppConstants.MainType.ARCHITECTURE));
        AssignButtonEvent("Button_31", tempContent, () => GetType(AppConstants.MainType.TECHNOLOGY));
        AssignButtonEvent("Button_32", tempContent, () => GetType(AppConstants.MainType.VEHICLE));
        AssignButtonEvent("Button_33", tempContent, () => GetType(AppConstants.MainType.CORE));
        AssignButtonEvent("Button_34", tempContent, () => GetType(AppConstants.MainType.WEAPON));
        AssignButtonEvent("Button_35", tempContent, () => GetType(AppConstants.MainType.ROBOT));

        tempContent.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateButton(int index, string itemName, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ShopButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void GetType(string type)
    {
        mainType = type; // Gán giá trị cho mainType
        GetButtonType(); // Gọi hàm xử lý
        titleText.text = LocalizationManager.Get(type); // Cập nhật tiêu đề
    }
    public void GetButtonType()
    {
        // DictionaryPanel.SetActive(true);
        GameObject equipmentObject = Instantiate(ShopPrefab, MainPanel);
        currentContent = equipmentObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        TabButtonPanel = equipmentObject.transform.Find("Scroll View/Viewport/Content");
        currencyPanel = equipmentObject.transform.Find("DictionaryCards/Currency");
        firstDecorationImage = equipmentObject.transform.Find("DictionaryCards/FirstDecorationImage").GetComponent<RawImage>();
        secondDecorationImage = equipmentObject.transform.Find("DictionaryCards/SecondDecorationImage").GetComponent<RawImage>();
        PageText = equipmentObject.transform.Find("Pagination/Page").GetComponent<TextMeshProUGUI>();
        NextButton = equipmentObject.transform.Find("Pagination/Next").GetComponent<Button>();
        PreviousButton = equipmentObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        titleText = equipmentObject.transform.Find("DictionaryCards/Title").GetComponent<Text>();
        CloseButton = equipmentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(equipmentObject);
        });
        HomeButton = equipmentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Close(MainPanel);
        });
        NextButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            ChangeNextPage();
        });
        PreviousButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            ChangePreviousPage();
        });

        // Transform CurrencyPanel = equipmentObject.transform.Find("DictionaryCards/Currency");
        // 
        // List<Currency> currencies = new List<Currency>();
        // currencies = currency.GetUserCurrency();
        // FindObjectOfType<CurrencyManager>().GetMainCurrency(currencies, CurrencyPanel);

        List<string> uniqueTypes = TypeManager.GetUniqueTypes(mainType);
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                // Tạo một nút mới từ prefab
                string subtype = uniqueTypes[i];
                GameObject button = Instantiate(buttonPrefab, TabButtonPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    OnButtonClick(button, subtype);
                });

                if (i == 0)
                {
                    type = subtype;
                    ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    LoadCurrentPage();

                }
                else
                {
                    ButtonLoader.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                }
            }
        }
        else
        {
            int totalRecord = 0;
            if (mainType.Equals(AppConstants.MainType.COLLABORATION))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_47_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_48_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Collaborations> collaborations = CollaborationService.Create().GetCollaborationWithPrice(pageSize, offset);
                CollaborationsController.Instance.CreateCollaborationTrade(collaborations, type, currentContent, currencyPanel, popupPanel);

                totalRecord = CollaborationService.Create().GetCollaborationWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.MEDAL))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_49_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_50_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Medals> medals = MedalsService.Create().GetMedalsWithPrice(pageSize, offset);
                MedalsController.Instance.CreateMedalsTrade(medals, type, currentContent, currencyPanel, popupPanel);

                totalRecord = MedalsService.Create().GetMedalsWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.TITLE))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_51_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_52_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Titles> titles = TitlesService.Create().GetTitlesWithPrice(pageSize, offset);
                TitlesController.Instance.CreateTitlesTrade(titles, type, currentContent, currencyPanel, popupPanel);

                totalRecord = TitlesService.Create().GetTitlesWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.BORDER))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_53_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_54_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Borders> borders = BordersService.Create().GetBordersWithPrice(pageSize, offset);
                BordersController.Instance.CreateBordersTrade(borders, type, currentContent, currencyPanel, popupPanel);

                totalRecord = BordersService.Create().GetBordersWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.ACHIEVEMENT))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_55_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_56_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Achievements> achievements = AchievementsService.Create().GetAchievementsWithPrice(pageSize, offset);
                AchievementsController.Instance.CreateAchievementsTrade(achievements, type, currentContent, currencyPanel, popupPanel);

                totalRecord = AchievementsService.Create().GetAchievementsWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_45_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_46_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<SpiritBeasts> spiritBeasts = SpiritBeastService.Create().GetSpiritBeastWithPrice(pageSize, offset);
                SpiritBeastsController.Instance.CreateSpiritBeastTrade(spiritBeasts, type, currentContent, currencyPanel, popupPanel);

                totalRecord = SpiritBeastService.Create().GetSpiritBeastWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.CARD))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_57_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_58_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Cards> cards = CardsService.Create().GetCardsWithPrice(pageSize, offset);
                CardsController.Instance.CreateCardsTrade(cards, type, currentContent, currencyPanel, popupPanel);

                totalRecord = CardsService.Create().GetCardsWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_59_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_60_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Architectures> architectures = ArchitecturesService.Create().GetArchitecturesWithPrice(pageSize, offset);
                ArchitecturesController.Instance.CreateArchitecturesTrade(architectures, type, currentContent, currencyPanel, popupPanel);

                totalRecord = ArchitecturesService.Create().GetArchitecturesWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_61_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_62_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Technologies> technologies = TechnologiesService.Create().GetTechnologiesWithPrice(pageSize, offset);
                TechnologiesController.Instance.CreateTechnologiesTrade(technologies, type, currentContent, currencyPanel, popupPanel);

                totalRecord = TechnologiesService.Create().GetTechnologiesWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.CORE))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_63_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_64_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Cores> cores = CoresService.Create().GetCoresWithPrice(pageSize, offset);
                CoresController.Instance.CreateCoresTrade(cores, type, currentContent, currencyPanel, popupPanel);

                totalRecord = CoresService.Create().GetCoresWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.WEAPON))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_65_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_66_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Weapons> weapons = WeaponsService.Create().GetWeaponsWithPrice(pageSize, offset);
                WeaponsController.Instance.CreateWeaponsTrade(weapons, type, currentContent, currencyPanel, popupPanel);

                totalRecord = WeaponsService.Create().GetWeaponsWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.ROBOT))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_67_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_68_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Robots> robots = RobotsService.Create().GetRobotsWithPrice(pageSize, offset);
                RobotsController.Instance.CreateRobotsTrade(robots, type, currentContent, currencyPanel, popupPanel);

                totalRecord = RobotsService.Create().GetRobotsWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.BADGE))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_69_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_70_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Badges> badges = BadgesService.Create().GetBadgesWithPrice(pageSize, offset);
                BadgesController.Instance.CreateBadgesTrade(badges, type, currentContent, currencyPanel, popupPanel);

                totalRecord = BadgesService.Create().GetBadgesWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_71_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_72_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<MechaBeasts> mechaBeasts = MechaBeastsService.Create().GetMechaBeastsWithPrice(pageSize, offset);
                MechaBeastsController.Instance.CreateMechaBeastsTrade(mechaBeasts, type, currentContent, currencyPanel, popupPanel);

                totalRecord = MechaBeastsService.Create().GetMechaBeastsWithPriceCount();
            }
            else if (mainType.Equals(AppConstants.MainType.RUNE))
            {
                Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_73_URL);
                Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_74_URL);
                firstDecorationImage.texture = firstDecorationTexture;
                secondDecorationImage.texture = secondDecorationTexture;
                List<Runes> runes = RunesService.Create().GetRunesWithPrice(pageSize, offset);
                RunesController.Instance.CreateRunesTrade(runes, type, currentContent, currencyPanel, popupPanel);

                totalRecord = RunesService.Create().GetRunesWithPriceCount();
            }

            totalPage = CalculateTotalPages(totalRecord, pageSize);
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        }

    }
    void OnButtonClick(GameObject clickedButton, string type)
    {
        foreach (Transform child in TabButtonPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ButtonLoader.Instance.ChangeButtonBackground(button.gameObject, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL); // Giả sử bạn có texture trắng
            }
        }

        this.type = type;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        ButtonLoader.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
        LoadCurrentPage();
    }
    public void LoadCurrentPage()
    {
        int totalRecord = 0;

        if (mainType.Equals(AppConstants.MainType.CARD_HERO))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_1_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_2_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<CardHeroes> cardHeroes = CardHeroesService.Create().GetCardHeroesWithPrice(type, pageSize, offset);
            CardHeroesController.Instance.CreateCardHeroesTrade(cardHeroes, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardHeroesService.Create().GetCardHeroesWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.BOOK))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_3_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_4_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Books> books = BooksService.Create().GetBooksWithPrice(type, pageSize, offset);
            BooksController.Instance.CreateBooksTrade(books, type, currentContent, currencyPanel, popupPanel);

            totalRecord = BooksService.Create().GetBookssWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_CAPTAIN))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_5_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_6_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<CardCaptains> cardCaptains = CardCaptainsService.Create().GetCardCaptainsWithPrice(type, pageSize, offset);
            CardCaptainsController.Instance.CreateCardCaptainsTrade(cardCaptains, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardCaptainsService.Create().GetCardCaptainsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.COLLABORATION_EQUIPMENT))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_7_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_8_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<CollaborationEquipments> collaborationEquipments = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPrice(type, pageSize, offset);
            CollaborationEquipmentsController.Instance.CreateCollaborationEquipmentsTrade(collaborationEquipments, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CollaborationEquipmentService.Create().GetCollaborationEquipmentsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.EQUIPMENT))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_9_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_10_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Equipments> equipments = EquipmentsService.Create().GetEquipments(type, pageSize, offset, rare);
            createEquipments(equipments);

            totalRecord = EquipmentsService.Create().GetEquipmentsCount(type, rare);
        }
        else if (mainType.Equals(AppConstants.MainType.PET))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_11_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_12_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Pets> pets = PetsService.Create().GetPetsWithPrice(type, pageSize, offset);
            PetsController.Instance.CreatePetsTrade(pets, type, currentContent, currencyPanel, popupPanel);

            totalRecord = PetsService.Create().GetPetsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.SKILL))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_13_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_14_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Skills> skills = SkillsService.Create().GetSkillsWithPrice(type, pageSize, offset);
            SkillsController.Instance.CreateSkillsTrade(skills, type, currentContent, currencyPanel, popupPanel);

            totalRecord = SkillsService.Create().GetSkillsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.SYMBOL))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_15_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_16_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Symbols> symbols = SymbolsService.Create().GetSymbolsWithPrice(type, pageSize, offset);
            SymbolsController.Instance.CreateSymbolsTrade(symbols, type, currentContent, currencyPanel, popupPanel);

            totalRecord = SymbolsService.Create().GetSkillsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MILITARY))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_17_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_18_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<CardMilitaries> cardMilitaries = CardMilitaryService.Create().GetCardMilitaryWithPrice(type, pageSize, offset);
            CardMilitariesController.Instance.CreateCardMilitaryTrade(cardMilitaries, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardMilitaryService.Create().GetCardMilitaryWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_SPELL))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_19_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_20_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<CardSpells> cardSpells = CardSpellService.Create().GetCardSpellWithPrice(type, pageSize, offset);
            CardSpellsController.Instance.CreateCardSpellTrade(cardSpells, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardSpellService.Create().GetCardSpellWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.MAGIC_FORMATION_CIRCLE))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_21_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_22_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<MagicFormationCircles> magicFormationCircles = MagicFormationCircleService.Create().GetMagicFormationCircleWithPrice(type, pageSize, offset);
            MagicFormationCirclesController.Instance.CreateMagicFormationCircleTrade(magicFormationCircles, type, currentContent, currencyPanel, popupPanel);

            totalRecord = MagicFormationCircleService.Create().GetMagicFormationCircleWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.RELIC))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_23_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_24_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Relics> relics = RelicsService.Create().GetRelicsWithPrice(type, pageSize, offset);
            RelicsController.Instance.CreateRelicsTrade(relics, type, currentContent, currencyPanel, popupPanel);

            totalRecord = RelicsService.Create().GetRelicsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_MONSTER))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_25_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_26_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<CardMonsters> cardMonsters = CardMonstersService.Create().GetCardMonstersWithPrice(type, pageSize, offset);
            CardMonstersController.Instance.CreateCardMonstersTrade(cardMonsters, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardMonstersService.Create().GetCardMonstersWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_COLONEL))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_27_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_28_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<CardColonels> cardColonels = CardColonelsService.Create().GetCardColonelsWithPrice(type, pageSize, offset);
            CardColonelsController.Instance.CreateCardColonelsTrade(cardColonels, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardColonelsService.Create().GetCardColonelsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_GENERAL))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_29_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_30_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<CardGenerals> cardGenerals = CardGeneralsService.Create().GetCardGeneralsWithPrice(type, pageSize, offset);
            CardGeneralsController.Instance.CreateCardGeneralsTrade(cardGenerals, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardGeneralsService.Create().GetCardGeneralsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_ADMIRAL))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_31_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_32_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<CardAdmirals> cardAdmirals = CardAdmiralsService.Create().GetCardAdmiralsWithPrice(type, pageSize, offset);
            CardAdmiralsController.Instance.CreateCardAdmiralsTrade(cardAdmirals, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardAdmiralsService.Create().GetCardAdmiralsWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.TALISMAN))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_33_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_34_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Talismans> talismans = TalismanService.Create().GetTalismanWithPrice(type, pageSize, offset);
            TalismansController.Instance.CreateTalismanTrade(talismans, type, currentContent, currencyPanel, popupPanel);

            totalRecord = TalismanService.Create().GetTalismanWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.PUPPET))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_35_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_36_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Puppets> puppets = PuppetService.Create().GetPuppetWithPrice(type, pageSize, offset);
            PuppetsController.Instance.CreatePuppetTrade(puppets, type, currentContent, currencyPanel, popupPanel);

            totalRecord = PuppetService.Create().GetPuppetWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.ALCHEMY))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_37_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_38_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Alchemies> alchemies = AlchemyService.Create().GetAlchemyWithPrice(type, pageSize, offset);
            AlchemiesController.Instance.CreateAlchemyTrade(alchemies, type, currentContent, currencyPanel, popupPanel);

            totalRecord = AlchemyService.Create().GetAlchemyWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.FORGE))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_39_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_40_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Forges> forges = ForgeService.Create().GetForgeWithPrice(type, pageSize, offset);
            ForgesController.Instance.CreateForgeTrade(forges, type, currentContent, currencyPanel, popupPanel);

            totalRecord = ForgeService.Create().GetForgeWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.CARD_LIFE))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_41_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_42_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<CardLives> cardLives = CardLifeService.Create().GetCardLifeWithPrice(type, pageSize, offset);
            CardLivesController.Instance.CreateCardLifeTrade(cardLives, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardLifeService.Create().GetCardLifeWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.ARTWORK))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_43_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_44_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Artworks> artworks = ArtworkService.Create().GetArtworkWithPrice(type, pageSize, offset);
            ArtworksController.Instance.CreateArtworkTrade(artworks, type, currentContent, currencyPanel, popupPanel);

            totalRecord = ArtworkService.Create().GetArtworkWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.SPIRIT_BEAST))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_45_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_46_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<SpiritBeasts> spiritBeasts = SpiritBeastService.Create().GetSpiritBeastWithPrice(pageSize, offset);
            SpiritBeastsController.Instance.CreateSpiritBeastTrade(spiritBeasts, type, currentContent, currencyPanel, popupPanel);

            totalRecord = SpiritBeastService.Create().GetSpiritBeastWithPriceCount();
        }
        else if (mainType.Equals(AppConstants.MainType.CARD))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_57_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_58_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Cards> cards = CardsService.Create().GetCardsWithPrice(pageSize, offset);
            CardsController.Instance.CreateCardsTrade(cards, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CardsService.Create().GetCardsWithPriceCount();
        }
        else if (mainType.Equals(AppConstants.MainType.ARCHITECTURE))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_59_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_60_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Architectures> architectures = ArchitecturesService.Create().GetArchitecturesWithPrice(pageSize, offset);
            ArchitecturesController.Instance.CreateArchitecturesTrade(architectures, type, currentContent, currencyPanel, popupPanel);

            totalRecord = ArchitecturesService.Create().GetArchitecturesWithPriceCount();
        }
        else if (mainType.Equals(AppConstants.MainType.TECHNOLOGY))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_61_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_62_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Technologies> technologies = TechnologiesService.Create().GetTechnologiesWithPrice(pageSize, offset);
            TechnologiesController.Instance.CreateTechnologiesTrade(technologies, type, currentContent, currencyPanel, popupPanel);

            totalRecord = TechnologiesService.Create().GetTechnologiesWithPriceCount();
        }
        else if (mainType.Equals(AppConstants.MainType.VEHICLE))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_63_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_64_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Vehicles> vehicles = VehiclesService.Create().GetVehiclesWithPrice(type, pageSize, offset);
            VehiclesController.Instance.CreateVehicleTrade(vehicles, type, currentContent, currencyPanel, popupPanel);

            totalRecord = VehiclesService.Create().GetVehicleWithPriceCount(type);
        }
        else if (mainType.Equals(AppConstants.MainType.CORE))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_63_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_64_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Cores> cores = CoresService.Create().GetCoresWithPrice(pageSize, offset);
            CoresController.Instance.CreateCoresTrade(cores, type, currentContent, currencyPanel, popupPanel);

            totalRecord = CoresService.Create().GetCoresWithPriceCount();
        }
        else if (mainType.Equals(AppConstants.MainType.WEAPON))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_65_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_66_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Weapons> weapons = WeaponsService.Create().GetWeaponsWithPrice(pageSize, offset);
            WeaponsController.Instance.CreateWeaponsTrade(weapons, type, currentContent, currencyPanel, popupPanel);

            totalRecord = WeaponsService.Create().GetWeaponsWithPriceCount();
        }
        else if (mainType.Equals(AppConstants.MainType.ROBOT))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_67_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_68_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Robots> robots = RobotsService.Create().GetRobotsWithPrice(pageSize, offset);
            RobotsController.Instance.CreateRobotsTrade(robots, type, currentContent, currencyPanel, popupPanel);

            totalRecord = RobotsService.Create().GetRobotsWithPriceCount();
        }
        else if (mainType.Equals(AppConstants.MainType.BADGE))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_69_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_70_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Badges> badges = BadgesService.Create().GetBadgesWithPrice(pageSize, offset);
            BadgesController.Instance.CreateBadgesTrade(badges, type, currentContent, currencyPanel, popupPanel);

            totalRecord = BadgesService.Create().GetBadgesWithPriceCount();
        }
        else if (mainType.Equals(AppConstants.MainType.MECHA_BEAST))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_71_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_72_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<MechaBeasts> mechaBeasts = MechaBeastsService.Create().GetMechaBeastsWithPrice(pageSize, offset);
            MechaBeastsController.Instance.CreateMechaBeastsTrade(mechaBeasts, type, currentContent, currencyPanel, popupPanel);

            totalRecord = MechaBeastsService.Create().GetMechaBeastsWithPriceCount();
        }
        else if (mainType.Equals(AppConstants.MainType.RUNE))
        {
            Texture firstDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_73_URL);
            Texture secondDecorationTexture = Resources.Load<Texture>(ImageConstants.Artifact.ARTIFACT_74_URL);
            firstDecorationImage.texture = firstDecorationTexture;
            secondDecorationImage.texture = secondDecorationTexture;
            List<Runes> runes = RunesService.Create().GetRunesWithPrice(pageSize, offset);
            RunesController.Instance.CreateRunesTrade(runes, type, currentContent, currencyPanel, popupPanel);

            totalRecord = RunesService.Create().GetRunesWithPriceCount();
        }

        totalPage = CalculateTotalPages(totalRecord, pageSize);
        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
    }
    private void createEquipments(List<Equipments> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            GameObject equipmentObject = Instantiate(equipmentsShopPrefab, currentContent);

            Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
            Title.text = equipment.Name.Replace("_", " ");

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(equipment.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            RawImage FrameImage = equipmentObject.transform.Find("Frame").GetComponent<RawImage>();
            EventTrigger eventTrigger = FrameImage.gameObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = FrameImage.gameObject.AddComponent<EventTrigger>(); // Nếu chưa có thì thêm EventTrigger
            }

            // Gán sự kiện click
            ButtonEvent.Instance.AddClickListener(eventTrigger, () => FindObjectOfType<PopupDetailsManager>().PopupDetails(equipment, MainPanel));
            // Thêm sự kiện Scroll để chuyển tiếp sự kiện cuộn
            EventTrigger.Entry scrollEntry = new EventTrigger.Entry { eventID = EventTriggerType.Scroll };
            scrollEntry.callback.AddListener((eventData) =>
            {
                var scrollRect = currentContent.GetComponentInParent<ScrollRect>();
                if (scrollRect != null)
                {
                    scrollRect.OnScroll((PointerEventData)eventData);
                }
            });
            eventTrigger.triggers.Add(scrollEntry);
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.Rare}");
            rareImage.texture = rareTexture;

            // Button buy = equipmentObject.transform.Find("Buy").GetComponent<Button>();
            // buy.onClick.AddListener(() =>
            // {
            //     GetQuantity(equipment.currency.quantity, e);
            // });
        }
        // GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        // if (gridLayout != null)
        // {
        //     gridLayout.cellSize = new Vector2(200, 230);
        // }
    }
    public void ClearAllPrefabs()
    {
        // Duyệt qua tất cả các con cái của cardsContent
        foreach (Transform child in currentContent)
        {
            Destroy(child.gameObject);
        }
    }
    public void ClearAllButton()
    {
        // Duyệt qua tất cả các con cái của cardsContent
        foreach (Transform child in TabButtonPanel)
        {
            Destroy(child.gameObject);
        }
    }
    public int CalculateTotalPages(int totalRecords, int pageSize)
    {
        if (pageSize <= 0) return 0; // Đảm bảo pageSize không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / pageSize);
    }
    public void ChangeNextPage()
    {
        if (currentPage < totalPage)
        {
            ClearAllPrefabs();
            currentPage = currentPage + 1;
            offset = offset + pageSize;
            LoadCurrentPage();

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ChangePreviousPage()
    {
        if (currentPage > 1)
        {
            ClearAllPrefabs();
            currentPage = currentPage - 1;
            offset = offset - pageSize;
            LoadCurrentPage();

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ClosePanel()
    {
        ClearAllButton();
        ClearAllPrefabs();
        offset = 0;
        currentPage = 1;
        foreach (Transform child in MainPanel)
        {
            Destroy(child.gameObject);
        }
    }
    public void Close(Transform content)
    {
        offset = 0;
        currentPage = 1;
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    // public void GetQuantity(int price, object obj)
    // {
    //     GameObject quantityObject = Instantiate(quantityPopupPrefab, popupPanel);

    //     Button increaseButton = quantityObject.transform.Find("IncreaseButton").GetComponent<Button>();
    //     Button decreaseButton = quantityObject.transform.Find("DecreaseButton").GetComponent<Button>();
    //     Button increase10Button = quantityObject.transform.Find("Increase10Button").GetComponent<Button>();
    //     Button decrease10Button = quantityObject.transform.Find("Decrease10Button").GetComponent<Button>();
    //     Button maxButton = quantityObject.transform.Find("MaxButton").GetComponent<Button>();
    //     Button minButton = quantityObject.transform.Find("MinButton").GetComponent<Button>();
    //     Button closeButton = quantityObject.transform.Find("CloseButton").GetComponent<Button>();
    //     Button confirmButton = quantityObject.transform.Find("Buy").GetComponent<Button>();
    //     TextMeshProUGUI quantityText = quantityObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
    //     RawImage currencyImage = quantityObject.transform.Find("Price/CurrencyImage").GetComponent<RawImage>();
    //     TextMeshProUGUI priceText = quantityObject.transform.Find("Price/PriceText").GetComponent<TextMeshProUGUI>();
    //     RawImage equipmentImage = quantityObject.transform.Find("Image").GetComponent<RawImage>();

    //     // Lấy thuộc tính `Id` và `Image` từ object
    //     var idProperty = obj.GetType().GetProperty("id");
    //     var imageProperty = obj.GetType().GetProperty("image");
    //     var currencyProperty = obj.GetType().GetProperty("currency");


    //     if (idProperty != null && imageProperty != null && currencyProperty != null)
    //     {
    //         int id = (int)idProperty.GetValue(obj);
    //         string image = (string)imageProperty.GetValue(obj);

    //         // Lấy đối tượng currency từ obj
    //         var currencyObject = currencyProperty.GetValue(obj);

    //         if (currencyObject != null)
    //         {
    //             // Lấy thuộc tính "image" từ currencyObject
    //             var currencyImageProperty = currencyObject.GetType().GetProperty("image");
    //             if (currencyImageProperty != null)
    //             {
    //                 string currencyImageValue = (string)currencyImageProperty.GetValue(currencyObject);

    //                 if (!string.IsNullOrEmpty(currencyImageValue))
    //                 {
    //                     string currencyFileNameWithoutExtension = currencyImageValue.Replace(".png", "");
    //                     Texture currencyTexture = Resources.Load<Texture>($"{currencyFileNameWithoutExtension}");
    //                     currencyImage.texture = currencyTexture;
    //                 }
    //             }
    //         }

    //         // Xử lý image của obj
    //         if (!string.IsNullOrEmpty(image))
    //         {
    //             string fileNameWithoutExtension = image.Replace(".png", "");
    //             Texture entityTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
    //             equipmentImage.texture = entityTexture;
    //         }

    //         priceText.text = price.ToString();
    //     }

    //     else
    //     {
    //         Debug.LogError("Object không có thuộc tính Id hoặc Image");
    //     }

    //     priceText.text = price.ToString();
    //     double originPrice = price;
    //     increaseButton.onClick.AddListener(() =>
    //     {
    //         int currentQuantity = int.Parse(quantityText.text);
    //         double price = double.Parse(priceText.text);
    //         currentQuantity++;
    //         price = originPrice * currentQuantity;
    //         quantityText.text = currentQuantity.ToString();
    //         priceText.text = price.ToString();
    //     });
    //     decreaseButton.onClick.AddListener(() =>
    //     {
    //         int currentQuantity = int.Parse(quantityText.text);
    //         double price = double.Parse(priceText.text);
    //         if (currentQuantity > 1)
    //         {
    //             currentQuantity--;
    //             price = originPrice * currentQuantity;
    //             quantityText.text = currentQuantity.ToString();
    //             priceText.text = price.ToString();
    //         }
    //     });
    //     increase10Button.onClick.AddListener(() =>
    //     {
    //         int currentQuantity = int.Parse(quantityText.text);
    //         double price = double.Parse(priceText.text);
    //         currentQuantity = currentQuantity + 10;
    //         price = originPrice * currentQuantity;
    //         quantityText.text = currentQuantity.ToString();
    //         priceText.text = price.ToString();
    //     });
    //     decrease10Button.onClick.AddListener(() =>
    //     {
    //         int currentQuantity = int.Parse(quantityText.text);
    //         double price = double.Parse(priceText.text);
    //         if (currentQuantity > 10)
    //         {
    //             currentQuantity = currentQuantity - 10;
    //             price = originPrice * currentQuantity;
    //             quantityText.text = currentQuantity.ToString();
    //             priceText.text = price.ToString();
    //         }
    //     });
    //     maxButton.onClick.AddListener(() =>
    //     {
    //         Currency userCurrency = new Currency();
    //         if (obj is Equipments equipment)
    //         {
    //             // userCurrency = currency.GetUserCurrencyById(equipment.c);
    //         }
    //         // double price = double.Parse(priceText.text);

    //         int max = (int)(userCurrency.quantity / price);
    //         double newprice = originPrice * max;
    //         quantityText.text = max.ToString();
    //         priceText.text = newprice.ToString();
    //     });
    //     minButton.onClick.AddListener(() =>
    //     {
    //         quantityText.text = "1";
    //         double price = double.Parse(priceText.text);
    //         price = originPrice * 1;
    //         priceText.text = price.ToString();
    //     });
    //     closeButton.onClick.AddListener(() => Close(popupPanel));
    //     confirmButton.onClick.AddListener(() =>
    //     {
    //         int quantity = int.Parse(quantityText.text); // Chuyển đổi giá trị từ quantityText thành số nguyên
    //         bool allSuccess = true; // Biến kiểm tra toàn bộ các giao dịch có thành công hay không

    //         for (int i = 1; i <= quantity; i++) // Duyệt từ 1 đến giá trị trong quantityText
    //         {
    //             if (obj is Equipments equipment)
    //             {
    //                 UserEquipmentsService.Create().UpdateUserCurrency(equipment.id);
    //                 bool success = UserEquipmentsService.Create().BuyEquipment(equipment.id);
    //                 if (!success)
    //                 {
    //                     allSuccess = false;
    //                     break;
    //                 }
    //             }

    //         }

    //         // Hiển thị thông báo dựa trên kết quả
    //         if (allSuccess)
    //         {
    //             String fileNameWithoutExtension = "";
    //             Transform CurrencyPanel = currentObject.transform.Find("DictionaryCards/Currency");
    //             List<Currency> currencies = new List<Currency>();
    //             string objType = "";
    //             if (obj is Equipments equipment)
    //             {
    //                 EquipmentsGalleryService.Create().InsertEquipmentsGallery(equipment.id);
    //                 FindObjectOfType<CurrencyManager>().GetEquipmentsCurrency(subType, CurrencyPanel);
    //                 fileNameWithoutExtension = equipment.image.Replace(".png", "");
    //             }
    //             Close(CurrencyPanel);
    //             FindObjectOfType<CurrencyManager>().createCurrency(currencies, currencyPanel);
    //             Close(popupPanel);
    //             // FindObjectOfType<NotificationManager>().ShowNotification("Purchase Successful!");
    //             GameObject receivedNotificationObject = Instantiate(ReceivedNotification, popupPanel);

    //             ButtonEvent.Instance.AddCloseEvent(receivedNotificationObject);
    //             Transform itemContent = receivedNotificationObject.transform.Find("Scroll View/Viewport/Content");
    //             GameObject itemObject = Instantiate(ItemThird, itemContent);

    //             RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
    //             Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
    //             eImage.texture = equipmentTexture;

    //             TextMeshProUGUI eQuantity = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
    //             eQuantity.text = quantity.ToString();

    //             if (objType.Equals("Achievements") || objType.Equals("Borders")
    //             || objType.Equals("Collaboration") || objType.Equals("CollaborationEquipment")
    //             || objType.Equals("Titles") || objType.Equals("Symbols") || objType.Equals("Medals")
    //             || objType.Equals("MagicFormationCircle") || objType.Equals("Talisman") || objType.Equals("Puppet")
    //             || objType.Equals("Alchemy") || objType.Equals("Forge") || objType.Equals("CardLife"))
    //             {
    //                 double currentPower = TeamsService.Create().GetTeamsPower(User.CurrentUserId);
    //                 PowerManagerService.Create().UpdateUserStats(User.CurrentUserId);
    //                 double newPower = TeamsService.Create().GetTeamsPower(User.CurrentUserId);
    //                 FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
    //             }
    //         }
    //         else
    //         {
    //             FindObjectOfType<NotificationManager>().ShowNotification("Purchase Failed!");
    //         }
    //     });
    // }
}
