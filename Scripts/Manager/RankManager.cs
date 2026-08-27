using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public static RankManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject RankPanelPrefab;
    private GameObject RankButtonPrefab;
    private GameObject SetRankButtonPrefab;
    private int Set;
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
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        RankPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.Rank.RANK_PANEL_PREFAB);
        RankButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Rank.RANK_BUTTON_PREFAB);
        SetRankButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Rank.SET_RANK_BUTTON_PREFAB);
    }
    public void CreateRank(IStats stat)
    {
        GameObject currentObject = Instantiate(RankPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("RankContent/Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);
        TextMeshProUGUI titleText2 = transform.Find("RankContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);

        CreateRankButtonUI(1, "Set 1", TextureHelper.LoadTexture2DCached(ImageConstants.Set.SET_1_URL), contentPanel);
        CreateRankButtonUI(2, "Set 2", TextureHelper.LoadTexture2DCached(ImageConstants.Set.SET_2_URL), contentPanel);
        CreateRankButtonUI(3, "Set 3", TextureHelper.LoadTexture2DCached(ImageConstants.Set.SET_3_URL), contentPanel);
        CreateRankButtonUI(4, "Set 4", TextureHelper.LoadTexture2DCached(ImageConstants.Set.SET_4_URL), contentPanel);
        CreateRankButtonUI(5, "Set 5", TextureHelper.LoadTexture2DCached(ImageConstants.Set.SET_5_URL), contentPanel);
        CreateRankButtonUI(6, "Set 6", TextureHelper.LoadTexture2DCached(ImageConstants.Set.SET_6_URL), contentPanel);
        CreateRankButtonUI(7, "Set 7", TextureHelper.LoadTexture2DCached(ImageConstants.Set.SET_7_URL), contentPanel);

        CreateRankButtonEvent(stat, contentPanel);
    }
    private void CreateRankButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(SetRankButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = "Button_" + index;

        // Gán hình ảnh cho itemImage
        RawImage image = transform.Find("Image").GetComponent<RawImage>();
        if (image != null && _itemImage != null)
        {
            image.texture = _itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }

        RawImage borderImage = transform.Find("BorderCircleImage").GetComponent<RawImage>();
        if (borderImage != null)
        {
            borderImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_BORDER_URL);
        }

        RawImage iconImage = transform.Find("IconImage").GetComponent<RawImage>();
        if (iconImage != null)
        {
            iconImage.texture = TextureHelper.LoadTexture2DCached(ImageConstants.ScienceFiction.SCIENCE_FICTION_ICON_URL);
        }
    }
    public void CreateRankButtonEvent(IStats stat, Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => CreateButtonSet1(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => CreateButtonSet2(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => CreateButtonSet3(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => CreateButtonSet4(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => CreateButtonSet5(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => CreateButtonSet6(stat));
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => CreateButtonSet7(stat));
    }
    private void CreateButtonWithBackgroundUI(int index, string itemName, string itemBackground, Texture2D itemImage, Transform panel)
    {
        if (panel == null)
        {
            Debug.Log("Panel is null for index: " + index);
            return;
        }
        // Tạo button từ prefab
        GameObject newButton = Instantiate(RankButtonPrefab, panel);
        newButton.name = "Button_" + index;
        Transform transform = newButton.transform;

        // Gán màu cho itemBackground
        RawImage background = transform.Find("Background").GetComponent<RawImage>();
        Texture texture = TextureHelper.LoadTextureCached($"{itemBackground}");
        if (background != null && itemBackground != null)
        {
            background.texture = texture;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = transform.Find("MainImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void CreateButtonSet1(IStats stat)
    {
        GameObject currentObject = Instantiate(RankPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("RankContent/Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);
        TextMeshProUGUI titleText2 = transform.Find("RankContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);
        
        CreateButtonWithBackgroundUI(1, AppDisplayConstants.MainMenuSet1.EQUIPMENTS, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Equipments"), contentPanel);
        CreateButtonWithBackgroundUI(2, AppDisplayConstants.MainMenuSet1.REALM, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Realm"), contentPanel);
        CreateButtonWithBackgroundUI(3, AppDisplayConstants.MainMenuSet1.UPGRADE, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Upgrade"), contentPanel);
        CreateButtonWithBackgroundUI(4, AppDisplayConstants.MainMenuSet1.APTITUDE, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Aptitude"), contentPanel);
        CreateButtonWithBackgroundUI(5, AppDisplayConstants.MainMenuSet1.AFFINITY, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Affinity"), contentPanel);
        CreateButtonWithBackgroundUI(6, AppDisplayConstants.MainMenuSet1.BLESSING, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Blessing"), contentPanel);
        CreateButtonWithBackgroundUI(7, AppDisplayConstants.MainMenuSet1.CORE, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Core"), contentPanel);
        CreateButtonWithBackgroundUI(8, AppDisplayConstants.MainMenuSet1.PHYSIQUE, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Physique"), contentPanel);
        CreateButtonWithBackgroundUI(9, AppDisplayConstants.MainMenuSet1.BLOODLINE, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Bloodline"), contentPanel);

        CreateButtonWithBackgroundUI(10, AppDisplayConstants.MainMenuSet1.OMNIVISION, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Omnivision"), contentPanel);
        CreateButtonWithBackgroundUI(11, AppDisplayConstants.MainMenuSet1.OMNIPOTENCE, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Omnipotence"), contentPanel);
        CreateButtonWithBackgroundUI(12, AppDisplayConstants.MainMenuSet1.OMNIPRESENCE, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Omnipresence"), contentPanel);
        CreateButtonWithBackgroundUI(13, AppDisplayConstants.MainMenuSet1.OMNISCIENCE, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Omniscience"), contentPanel);
        CreateButtonWithBackgroundUI(14, AppDisplayConstants.MainMenuSet1.OMNIVORY, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Omnivory"), contentPanel);
        CreateButtonWithBackgroundUI(15, AppDisplayConstants.MainMenuSet1.ANGEL, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Angel"), contentPanel);
        CreateButtonWithBackgroundUI(16, AppDisplayConstants.MainMenuSet1.DEMON, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Demon"), contentPanel);

        CreateButtonWithBackgroundUI(17, AppDisplayConstants.MainMenuSet1.SWORD, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Sword"), contentPanel);
        CreateButtonWithBackgroundUI(18, AppDisplayConstants.MainMenuSet1.SPEAR, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Spear"), contentPanel);
        CreateButtonWithBackgroundUI(19, AppDisplayConstants.MainMenuSet1.SHIELD, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Shield"), contentPanel);
        CreateButtonWithBackgroundUI(20, AppDisplayConstants.MainMenuSet1.BOW, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Bow"), contentPanel);
        CreateButtonWithBackgroundUI(21, AppDisplayConstants.MainMenuSet1.GUN, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Gun"), contentPanel);
        CreateButtonWithBackgroundUI(22, AppDisplayConstants.MainMenuSet1.CYBER, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Cyber"), contentPanel);
        CreateButtonWithBackgroundUI(23, AppDisplayConstants.MainMenuSet1.FAIRY, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Fairy"), contentPanel);


        ButtonEvent.Instance.AssignButtonEvent("Button_1", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuEquipmentManager>().CreateMainMenuEquipmentManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuRealmManager>().CreateMainMenuRealmManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuUpgradeManager>().CreateMainMenuUpgradeManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAptitudeManager>().CreateMainMenuAptitudeManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", contentPanel, () =>
        {
            FindAnyObjectByType<MainMenuAffinityManager>().CreateMainMenuAffinityManager(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuBlessingManager>().CreateMainMenuBlessingManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuCoreManager>().CreateMainMenuCoreManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuPhysiqueManager>().CreateMainMenuPhysiqueManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuBloodlineManager>().CreateMainMenuBloodlineManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuOmnivisionManager>().CreateMainMenuOmnivisionManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuOmnipotenceManager>().CreateMainMenuOmnipotenceManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuOmnipresenceManager>().CreateMainMenuOmnipresenceManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuOmniscienceManager>().CreateMainMenuOmniscienceManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuOmnivoryManager>().CreateMainMenuOmnivoryManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAngelManager>().CreateMainMenuAngelManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuDemonManager>().CreateMainMenuDemonManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuSwordManager>().CreateMainMenuSwordManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuSpearManager>().CreateMainMenuSpearManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuShieldManager>().CreateMainMenuShieldManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuBowManager>().CreateMainMenuBowManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuGunManager>().CreateMainMenuGunManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuCyberManager>().CreateMainMenuCyberManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuFairyManager>().CreateMainMenuFairyManagerAsync(stat);
        });
    }
    public void CreateButtonSet2(IStats stat)
    {
        GameObject currentObject = Instantiate(RankPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("RankContent/Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);
        TextMeshProUGUI titleText2 = transform.Find("RankContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);

        CreateButtonWithBackgroundUI(1, AppDisplayConstants.MainMenuSet2.DARK, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Dark"), contentPanel);
        CreateButtonWithBackgroundUI(2, AppDisplayConstants.MainMenuSet2.LIGHT, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Light"), contentPanel);
        CreateButtonWithBackgroundUI(3, AppDisplayConstants.MainMenuSet2.FIRE, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Fire"), contentPanel);
        CreateButtonWithBackgroundUI(4, AppDisplayConstants.MainMenuSet2.ICE, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Ice"), contentPanel);
        CreateButtonWithBackgroundUI(5, AppDisplayConstants.MainMenuSet2.EARTH, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Earth"), contentPanel);
        CreateButtonWithBackgroundUI(6, AppDisplayConstants.MainMenuSet2.THUNDER, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Thunder"), contentPanel);
        CreateButtonWithBackgroundUI(7, AppDisplayConstants.MainMenuSet2.LIFE, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Life"), contentPanel);
        CreateButtonWithBackgroundUI(8, AppDisplayConstants.MainMenuSet2.SPACE, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Space"), contentPanel);
        CreateButtonWithBackgroundUI(9, AppDisplayConstants.MainMenuSet2.TIME, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Time"), contentPanel);

        CreateButtonWithBackgroundUI(10, AppDisplayConstants.MainMenuSet2.NANOTECH, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Nanotech"), contentPanel);
        CreateButtonWithBackgroundUI(11, AppDisplayConstants.MainMenuSet2.QUANTUM, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Quantum"), contentPanel);
        CreateButtonWithBackgroundUI(12, AppDisplayConstants.MainMenuSet2.HOLOGRAPHY, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Holography"), contentPanel);
        CreateButtonWithBackgroundUI(13, AppDisplayConstants.MainMenuSet2.PLASMAN, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Plasma"), contentPanel);
        CreateButtonWithBackgroundUI(14, AppDisplayConstants.MainMenuSet2.BIOMECH, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Biomech"), contentPanel);
        CreateButtonWithBackgroundUI(15, AppDisplayConstants.MainMenuSet2.CRYOTECH, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Cryotech"), contentPanel);
        CreateButtonWithBackgroundUI(16, AppDisplayConstants.MainMenuSet2.PSIONICS, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Psionics"), contentPanel);

        CreateButtonWithBackgroundUI(17, AppDisplayConstants.MainMenuSet2.NEUROTECH, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Neurotech"), contentPanel);
        CreateButtonWithBackgroundUI(18, AppDisplayConstants.MainMenuSet2.ANIMATTER, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Antimatter"), contentPanel);
        CreateButtonWithBackgroundUI(19, AppDisplayConstants.MainMenuSet2.PHANTOMWARE, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Phantomware"), contentPanel);
        CreateButtonWithBackgroundUI(20, AppDisplayConstants.MainMenuSet2.GRAVITECH, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Gravitech"), contentPanel);
        CreateButtonWithBackgroundUI(21, AppDisplayConstants.MainMenuSet2.AETHERNET, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Aethernet"), contentPanel);
        CreateButtonWithBackgroundUI(22, AppDisplayConstants.MainMenuSet2.STARFORGE, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Starforge"), contentPanel);
        CreateButtonWithBackgroundUI(23, AppDisplayConstants.MainMenuSet2.ORBITALIS, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Orbitalis"), contentPanel);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuDarkManager>().CreateMainMenuDarkManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuLightManager>().CreateMainMenuLightManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuFireManager>().CreateMainMenuFireManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuIceManager>().CreateMainMenuIceManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuEarthManager>().CreateMainMenuEarthManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuThunderManager>().CreateMainMenuThunderManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuLifeManager>().CreateMainMenuLifeManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuSpaceManager>().CreateMainMenuSpaceManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuTimeManager>().CreateMainMenuTimeManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuNanotechManager>().CreateMainMenuNanotechManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuQuantumManager>().CreateMainMenuQuantumManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuHolographyManager>().CreateMainMenuHolographyManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuPlasmaManager>().CreateMainMenuPlasmaManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuBiomechManager>().CreateMainMenuBiomechManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuCryotechManager>().CreateMainMenuCryotechManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuPsionicsManager>().CreateMainMenuPsionicsManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuNeurotechManager>().CreateMainMenuNeurotechManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAntimatterManager>().CreateMainMenuAntimatterManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuPhantomwareManager>().CreateMainMenuPhantomwareManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuGravitechManager>().CreateMainMenuGravitechManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAethernetManager>().CreateMainMenuAethernetManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuStarforgeManager>().CreateMainMenuStarforgeManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuOrbitalisManager>().CreateMainMenuOrbitalisManagerAsync(stat);
        });
    }
    public void CreateButtonSet3(IStats stat)
    {
        GameObject currentObject = Instantiate(RankPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("RankContent/Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);
        TextMeshProUGUI titleText2 = transform.Find("RankContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);

        CreateButtonWithBackgroundUI(1, AppDisplayConstants.MainMenuSet3.AZATHOTH, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Azathoth"), contentPanel);
        CreateButtonWithBackgroundUI(2, AppDisplayConstants.MainMenuSet3.YOG_SOTHOTH, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Yog-Sothoth"), contentPanel);
        CreateButtonWithBackgroundUI(3, AppDisplayConstants.MainMenuSet3.NYARLATHOTEP, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Nyarlathotep"), contentPanel);
        CreateButtonWithBackgroundUI(4, AppDisplayConstants.MainMenuSet3.SHUB_NIGGURATH, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Shub-Niggurath"), contentPanel);
        CreateButtonWithBackgroundUI(5, AppDisplayConstants.MainMenuSet3.NIHORATH, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Nihorath"), contentPanel);
        CreateButtonWithBackgroundUI(6, AppDisplayConstants.MainMenuSet3.AEONAX, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Aeonax"), contentPanel);
        CreateButtonWithBackgroundUI(7, AppDisplayConstants.MainMenuSet3.SERAPHIROS, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Seraphiros"), contentPanel);
        CreateButtonWithBackgroundUI(8, AppDisplayConstants.MainMenuSet3.THORINDAR, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Thorindar"), contentPanel);
        CreateButtonWithBackgroundUI(9, AppDisplayConstants.MainMenuSet3.ZILTHROS, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Zilthros"), contentPanel);

        CreateButtonWithBackgroundUI(10, AppDisplayConstants.MainMenuSet3.KHORAZAL, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Khorazal"), contentPanel);
        CreateButtonWithBackgroundUI(11, AppDisplayConstants.MainMenuSet3.IXITHRA, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Ixithra"), contentPanel);
        CreateButtonWithBackgroundUI(12, AppDisplayConstants.MainMenuSet3.OMNITHEUS, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Omnitheus"), contentPanel);
        CreateButtonWithBackgroundUI(13, AppDisplayConstants.MainMenuSet3.PHYRIXA, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Phyrixa"), contentPanel);
        CreateButtonWithBackgroundUI(14, AppDisplayConstants.MainMenuSet3.ATHERION, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Atherion"), contentPanel);
        CreateButtonWithBackgroundUI(15, AppDisplayConstants.MainMenuSet3.VORATHOS, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Vorathos"), contentPanel);
        CreateButtonWithBackgroundUI(16, AppDisplayConstants.MainMenuSet3.TENEBRIS, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Tenebris"), contentPanel);

        CreateButtonWithBackgroundUI(17, AppDisplayConstants.MainMenuSet3.XYLKOR, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Xylkor"), contentPanel);
        CreateButtonWithBackgroundUI(18, AppDisplayConstants.MainMenuSet3.VELTHARION, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Veltharion"), contentPanel);
        CreateButtonWithBackgroundUI(19, AppDisplayConstants.MainMenuSet3.ARCANOS, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Arcanos"), contentPanel);
        CreateButtonWithBackgroundUI(20, AppDisplayConstants.MainMenuSet3.DOLOMATH, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Dolomath"), contentPanel);
        CreateButtonWithBackgroundUI(21, AppDisplayConstants.MainMenuSet3.ARATHOR, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Arathor"), contentPanel);
        CreateButtonWithBackgroundUI(22, AppDisplayConstants.MainMenuSet3.XYPHOS, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Xyphos"), contentPanel);
        CreateButtonWithBackgroundUI(23, AppDisplayConstants.MainMenuSet3.VAELITH, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Vaelith"), contentPanel);


        ButtonEvent.Instance.AssignButtonEvent("Button_1", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAzathothManager>().CreateMainMenuAzathothManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuYogSothothManager>().CreateMainMenuYogSothothManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuNyarlathotepManager>().CreateMainMenuNyarlathotepManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuShubNiggurathManager>().CreateMainMenuShubNiggurathManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuNihorathManager>().CreateMainMenuNihorathManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAeonaxManager>().CreateMainMenuAeonaxManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuSeraphirosManager>().CreateMainMenuSeraphirosManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuThorindarManager>().CreateMainMenuThorindarManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuZilthrosManager>().CreateMainMenuZilthrosManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuKhorazalManager>().CreateMainMenuKhorazalManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuIxithraManager>().CreateMainMenuIxithraManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuOmnitheusManager>().CreateMainMenuOmnitheusManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuPhyrixaManager>().CreateMainMenuPhyrixaManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAtherionManager>().CreateMainMenuAtherionManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuVorathosManager>().CreateMainMenuVorathosManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuTenebrisManager>().CreateMainMenuTenebrisManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuXylkorManager>().CreateMainMenuXylkorManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuVeltharionManager>().CreateMainMenuVeltharionManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuArcanosManager>().CreateMainMenuArcanosManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuDolomathManager>().CreateMainMenuDolomathManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuArathorManager>().CreateMainMenuArathorManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuXyphosManager>().CreateMainMenuXyphosManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuVaelithManager>().CreateMainMenuVaelithManagerAsync(stat);
        });
    }
    public void CreateButtonSet4(IStats stat)
    {
        GameObject currentObject = Instantiate(RankPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("RankContent/Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);
        TextMeshProUGUI titleText2 = transform.Find("RankContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);

        CreateButtonWithBackgroundUI(1, AppDisplayConstants.MainMenuSet4.ZARX, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Zarx"), contentPanel);
        CreateButtonWithBackgroundUI(2, AppDisplayConstants.MainMenuSet4.RAIK, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Raik"), contentPanel);
        CreateButtonWithBackgroundUI(3, AppDisplayConstants.MainMenuSet4.DRAX, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Drax"), contentPanel);
        CreateButtonWithBackgroundUI(4, AppDisplayConstants.MainMenuSet4.KRON, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Kron"), contentPanel);
        CreateButtonWithBackgroundUI(5, AppDisplayConstants.MainMenuSet4.ZOLT, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Zolt"), contentPanel);
        CreateButtonWithBackgroundUI(6, AppDisplayConstants.MainMenuSet4.GORR, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Gorr"), contentPanel);
        CreateButtonWithBackgroundUI(7, AppDisplayConstants.MainMenuSet4.RYZE, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Ryze"), contentPanel);
        CreateButtonWithBackgroundUI(8, AppDisplayConstants.MainMenuSet4.JAXX, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Jaxx"), contentPanel);
        CreateButtonWithBackgroundUI(9, AppDisplayConstants.MainMenuSet4.THAR, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Thar"), contentPanel);

        CreateButtonWithBackgroundUI(10, AppDisplayConstants.MainMenuSet4.VORN, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Vorn"), contentPanel);
        CreateButtonWithBackgroundUI(11, AppDisplayConstants.MainMenuSet4.NYX, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Nyx"), contentPanel);
        CreateButtonWithBackgroundUI(12, AppDisplayConstants.MainMenuSet4.AROS, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Aros"), contentPanel);
        CreateButtonWithBackgroundUI(13, AppDisplayConstants.MainMenuSet4.HEX, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Hex"), contentPanel);
        CreateButtonWithBackgroundUI(14, AppDisplayConstants.MainMenuSet4.LORN, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Lorn"), contentPanel);
        CreateButtonWithBackgroundUI(15, AppDisplayConstants.MainMenuSet4.BAXX, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Baxx"), contentPanel);
        CreateButtonWithBackgroundUI(16, AppDisplayConstants.MainMenuSet4.ZEPH, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Zeph"), contentPanel);

        CreateButtonWithBackgroundUI(17, AppDisplayConstants.MainMenuSet4.KAEL, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Kael"), contentPanel);
        CreateButtonWithBackgroundUI(18, AppDisplayConstants.MainMenuSet4.DRAV, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Drav"), contentPanel);
        CreateButtonWithBackgroundUI(19, AppDisplayConstants.MainMenuSet4.TORN, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Torn"), contentPanel);
        CreateButtonWithBackgroundUI(20, AppDisplayConstants.MainMenuSet4.MYRR, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Myrr"), contentPanel);
        CreateButtonWithBackgroundUI(21, AppDisplayConstants.MainMenuSet4.VASK, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Vask"), contentPanel);
        CreateButtonWithBackgroundUI(22, AppDisplayConstants.MainMenuSet4.JORR, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Jorr"), contentPanel);
        CreateButtonWithBackgroundUI(23, AppDisplayConstants.MainMenuSet4.QUEN, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Quen"), contentPanel);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuZarxManager>().CreateMainMenuZarxManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuRaikManager>().CreateMainMenuRaikManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuDraxManager>().CreateMainMenuDraxManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuKronManager>().CreateMainMenuKronManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuZoltManager>().CreateMainMenuZoltManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuGorrManager>().CreateMainMenuGorrManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuRyzeManager>().CreateMainMenuRyzeManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuJaxxManager>().CreateMainMenuJaxxManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuTharManager>().CreateMainMenuTharManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuVornManager>().CreateMainMenuVornManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuNyxManager>().CreateMainMenuNyxManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuArosManager>().CreateMainMenuArosManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuHexManager>().CreateMainMenuHexManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuLornManager>().CreateMainMenuLornManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuBaxxManager>().CreateMainMenuBaxxManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuZephManager>().CreateMainMenuZephManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuKaelManager>().CreateMainMenuKaelManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuDravManager>().CreateMainMenuDravManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuTornManager>().CreateMainMenuTornManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuMyrrManager>().CreateMainMenuMyrrManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuVaskManager>().CreateMainMenuVaskManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuJorrManager>().CreateMainMenuJorrManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuQuenManager>().CreateMainMenuQuenManagerAsync(stat);
        });
    }
    public void CreateButtonSet5(IStats stat)
    {
        GameObject currentObject = Instantiate(RankPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("RankContent/Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);
        TextMeshProUGUI titleText2 = transform.Find("RankContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);

        CreateButtonWithBackgroundUI(1, AppDisplayConstants.MainMenuSet5.ASTRAL_VOICE, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/AstralVoice"), contentPanel);
        CreateButtonWithBackgroundUI(2, AppDisplayConstants.MainMenuSet5.BRANCH_BLADE_SONG, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/BranchBladeSong"), contentPanel);
        CreateButtonWithBackgroundUI(3, AppDisplayConstants.MainMenuSet5.CHAOS_JAZZ, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/ChaosJazz"), contentPanel);
        CreateButtonWithBackgroundUI(4, AppDisplayConstants.MainMenuSet5.CHAOTIC_METAL, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/ChaoticMetal"), contentPanel);
        CreateButtonWithBackgroundUI(5, AppDisplayConstants.MainMenuSet5.DAWN_S_BLOOM, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/DawnSBloom"), contentPanel);
        CreateButtonWithBackgroundUI(6, AppDisplayConstants.MainMenuSet5.FANGED_METAL, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/FangedMetal"), contentPanel);
        CreateButtonWithBackgroundUI(7, AppDisplayConstants.MainMenuSet5.FREEDOM_BLUES, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/FreedomBlues"), contentPanel);
        CreateButtonWithBackgroundUI(8, AppDisplayConstants.MainMenuSet5.HORMONE_PUNK, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/HormonePunk"), contentPanel);
        CreateButtonWithBackgroundUI(9, AppDisplayConstants.MainMenuSet5.INFERNO_METAL, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/InfernoMetal"), contentPanel);

        CreateButtonWithBackgroundUI(10, AppDisplayConstants.MainMenuSet5.KING_OF_THE_SUMMIT, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/KingOfTheSummit"), contentPanel);
        CreateButtonWithBackgroundUI(11, AppDisplayConstants.MainMenuSet5.MOONLIGHT_LULLABY, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/MoonlightLullaby"), contentPanel);
        CreateButtonWithBackgroundUI(12, AppDisplayConstants.MainMenuSet5.PHAETON_S_MELODY, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/PhaetonSMelody"), contentPanel);
        CreateButtonWithBackgroundUI(13, AppDisplayConstants.MainMenuSet5.POLAR_METAL, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/PolarMetal"), contentPanel);
        CreateButtonWithBackgroundUI(14, AppDisplayConstants.MainMenuSet5.PROTO_PUNK, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/ProtoPunk"), contentPanel);
        CreateButtonWithBackgroundUI(15, AppDisplayConstants.MainMenuSet5.PUFFER_ELECTRO, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/PufferElectro"), contentPanel);
        CreateButtonWithBackgroundUI(16, AppDisplayConstants.MainMenuSet5.SHADOW_HARMONY, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/ShadowHarmony"), contentPanel);

        CreateButtonWithBackgroundUI(17, AppDisplayConstants.MainMenuSet5.SHOCKSTAR_DISCO, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/ShockstarDisco"), contentPanel);
        CreateButtonWithBackgroundUI(18, AppDisplayConstants.MainMenuSet5.SOUL_ROCK, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/SoulRock"), contentPanel);
        CreateButtonWithBackgroundUI(19, AppDisplayConstants.MainMenuSet5.SWING_JAZZ, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/SwingJazz"), contentPanel);
        CreateButtonWithBackgroundUI(20, AppDisplayConstants.MainMenuSet5.THUNDER_METAL, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/ThunderMetal"), contentPanel);
        CreateButtonWithBackgroundUI(21, AppDisplayConstants.MainMenuSet5.WOODPECKER_ELECTRO, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/WoodpeckerElectro"), contentPanel);
        CreateButtonWithBackgroundUI(22, AppDisplayConstants.MainMenuSet5.YUNKUI_TALES, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/YunkuiTales"), contentPanel);
        CreateButtonWithBackgroundUI(23, AppDisplayConstants.MainMenuSet5.CHIP, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Chip_Slot"), contentPanel);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAstralVoiceManager>().CreateMainMenuAstralVoiceManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuBranchBladeSongManager>().CreateMainMenuBranchBladeSongManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuChaosJazzManager>().CreateMainMenuChaosJazzManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuChaoticMetalManager>().CreateMainMenuChaoticMetalManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuDawnSBloomManager>().CreateMainMenuDawnSBloomManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuFangedMetalManager>().CreateMainMenuFangedMetalManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuFreedomBluesManager>().CreateMainMenuFreedomBluesManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuHormonePunkManager>().CreateMainMenuHormonePunkManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuInfernoMetalManager>().CreateMainMenuInfernoMetalManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuKingOfTheSummitManager>().CreateMainMenuKingOfTheSummitManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuMoonlightLullabyManager>().CreateMainMenuMoonlightLullabyManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuPhaetonSMelodyManager>().CreateMainMenuPhaetonSMelodyManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuPolarMetalManager>().CreateMainMenuPolarMetalManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuProtoPunkManager>().CreateMainMenuProtoPunkManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuPufferElectroManager>().CreateMainMenuPufferElectroManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuShadowHarmonyManager>().CreateMainMenuShadowHarmonyManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuShockstarDiscoManager>().CreateMainMenuShockstarDiscoManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuSoulRockManager>().CreateMainMenuSoulRockManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuSwingJazzManager>().CreateMainMenuSwingJazzManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuThunderManager>().CreateMainMenuThunderManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuWoodpeckerElectroManager>().CreateMainMenuWoodpeckerElectroManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuYunkuiTalesManager>().CreateMainMenuYunkuiTalesManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuChipManager>().CreateMainMenuChipManagerAsync(stat);
        });
    }
    public void CreateButtonSet6(IStats stat)
    {
        GameObject currentObject = Instantiate(RankPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("RankContent/Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);
        TextMeshProUGUI titleText2 = transform.Find("RankContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);

        CreateButtonWithBackgroundUI(1, AppDisplayConstants.MainMenuSet6.APOTHEON, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Apotheon"), contentPanel);
        CreateButtonWithBackgroundUI(2, AppDisplayConstants.MainMenuSet6.AXIOM, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Axiom"), contentPanel);
        CreateButtonWithBackgroundUI(3, AppDisplayConstants.MainMenuSet6.CATACLYSM, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Cataclysm"), contentPanel);
        CreateButtonWithBackgroundUI(4, AppDisplayConstants.MainMenuSet6.CATALYST, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Catalyst"), contentPanel);
        CreateButtonWithBackgroundUI(5, AppDisplayConstants.MainMenuSet6.DOMINION, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Dominion"), contentPanel);
        CreateButtonWithBackgroundUI(6, AppDisplayConstants.MainMenuSet6.ECLIPSE, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Eclipse"), contentPanel);
        CreateButtonWithBackgroundUI(7, AppDisplayConstants.MainMenuSet6.ELYSIUM, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Elysium"), contentPanel);
        CreateButtonWithBackgroundUI(8, AppDisplayConstants.MainMenuSet6.EMPYREAN, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Empyrean"), contentPanel);
        CreateButtonWithBackgroundUI(9, AppDisplayConstants.MainMenuSet6.ENTROPY, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Entropy"), contentPanel);

        CreateButtonWithBackgroundUI(10, AppDisplayConstants.MainMenuSet6.FLUX, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Flux"), contentPanel);
        CreateButtonWithBackgroundUI(11, AppDisplayConstants.MainMenuSet6.GENESIS, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Genesis"), contentPanel);
        CreateButtonWithBackgroundUI(12, AppDisplayConstants.MainMenuSet6.HELIX, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Helix"), contentPanel);
        CreateButtonWithBackgroundUI(13, AppDisplayConstants.MainMenuSet6.HYPERION, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Hyperion"), contentPanel);
        CreateButtonWithBackgroundUI(14, AppDisplayConstants.MainMenuSet6.INFERNUM, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Infernum"), contentPanel);
        CreateButtonWithBackgroundUI(15, AppDisplayConstants.MainMenuSet6.NEXUS, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Nexus"), contentPanel);
        CreateButtonWithBackgroundUI(16, AppDisplayConstants.MainMenuSet6.NULLITY, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Nullity"), contentPanel);

        CreateButtonWithBackgroundUI(17, AppDisplayConstants.MainMenuSet6.OBLIVION, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Oblivion"), contentPanel);
        CreateButtonWithBackgroundUI(18, AppDisplayConstants.MainMenuSet6.OBLIVIUM, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Oblivium"), contentPanel);
        CreateButtonWithBackgroundUI(19, AppDisplayConstants.MainMenuSet6.PARAGON, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Paragon"), contentPanel);
        CreateButtonWithBackgroundUI(20, AppDisplayConstants.MainMenuSet6.PARALLAX, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Parallax"), contentPanel);
        CreateButtonWithBackgroundUI(21, AppDisplayConstants.MainMenuSet6.SINGULARITY, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Singularity"), contentPanel);
        CreateButtonWithBackgroundUI(22, AppDisplayConstants.MainMenuSet6.UMBRA, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Umbra"), contentPanel);
        CreateButtonWithBackgroundUI(23, AppDisplayConstants.MainMenuSet6.ZENITH, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Zenith"), contentPanel);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuApotheonManager>().CreateMainMenuApotheonManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAxiomManager>().CreateMainMenuAxiomManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuCataclysmManager>().CreateMainMenuCataclysmManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuCatalystManager>().CreateMainMenuCatalystManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuDominionManager>().CreateMainMenuDominionManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuEclipseManager>().CreateMainMenuEclipseManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuElysiumManager>().CreateMainMenuElysiumManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuEmpyreanManager>().CreateMainMenuEmpyreanManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuEntropyManager>().CreateMainMenuEntropyManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuFluxManager>().CreateMainMenuFluxManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuGenesisManager>().CreateMainMenuGenesisManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuHelixManager>().CreateMainMenuHelixManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuHyperionManager>().CreateMainMenuHyperionManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuInfernumManager>().CreateMainMenuInfernumManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuNexusManager>().CreateMainMenuNexusManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuNullityManager>().CreateMainMenuNullityManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuOblivionManager>().CreateMainMenuOblivionManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuObliviumManager>().CreateMainMenuObliviumManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuParagonManager>().CreateMainMenuParagonManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuParallaxManager>().CreateMainMenuParallaxManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuSingularityManager>().CreateMainMenuSingularityManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuUmbraManager>().CreateMainMenuUmbraManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuZenithManager>().CreateMainMenuZenithManagerAsync(stat);
        });
    }
    public void CreateButtonSet7(IStats stat)
    {
        GameObject currentObject = Instantiate(RankPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform contentPanel = transform.Find("RankContent/Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);
        TextMeshProUGUI titleText2 = transform.Find("RankContent/TitleText").GetComponent<TextMeshProUGUI>();
        titleText2.text = LocalizationManager.Get(AppDisplayConstants.MainType.RANK);

        CreateButtonWithBackgroundUI(1, AppDisplayConstants.MainMenuSet7.ABYSSAL, ImageConstants.Background.ADVANCED_BACKGROUND_1_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Abyssal"), contentPanel);
        CreateButtonWithBackgroundUI(2, AppDisplayConstants.MainMenuSet7.ARCANE, ImageConstants.Background.ADVANCED_BACKGROUND_2_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Arcane"), contentPanel);
        CreateButtonWithBackgroundUI(3, AppDisplayConstants.MainMenuSet7.ASHFRAME, ImageConstants.Background.ADVANCED_BACKGROUND_3_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Ashframe"), contentPanel);
        CreateButtonWithBackgroundUI(4, AppDisplayConstants.MainMenuSet7.ASTRION, ImageConstants.Background.ADVANCED_BACKGROUND_4_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Astrion"), contentPanel);
        CreateButtonWithBackgroundUI(5, AppDisplayConstants.MainMenuSet7.AXIOMATA, ImageConstants.Background.ADVANCED_BACKGROUND_5_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Axiomata"), contentPanel);
        CreateButtonWithBackgroundUI(6, AppDisplayConstants.MainMenuSet7.CHRONYX, ImageConstants.Background.ADVANCED_BACKGROUND_6_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Chronyx"), contentPanel);
        CreateButtonWithBackgroundUI(7, AppDisplayConstants.MainMenuSet7.COGNITUM, ImageConstants.Background.ADVANCED_BACKGROUND_7_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Cognitum"), contentPanel);
        CreateButtonWithBackgroundUI(8, AppDisplayConstants.MainMenuSet7.CONTINUUM, ImageConstants.Background.ADVANCED_BACKGROUND_8_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Continuum"), contentPanel);
        CreateButtonWithBackgroundUI(9, AppDisplayConstants.MainMenuSet7.COSMOS, ImageConstants.Background.ADVANCED_BACKGROUND_9_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Cosmos"), contentPanel);

        CreateButtonWithBackgroundUI(10, AppDisplayConstants.MainMenuSet7.ETERNUM, ImageConstants.Background.ADVANCED_BACKGROUND_10_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Eternum"), contentPanel);
        CreateButtonWithBackgroundUI(11, AppDisplayConstants.MainMenuSet7.FERRUMAX, ImageConstants.Background.ADVANCED_BACKGROUND_11_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Ferrumax"), contentPanel);
        CreateButtonWithBackgroundUI(12, AppDisplayConstants.MainMenuSet7.HORIZON, ImageConstants.Background.ADVANCED_BACKGROUND_12_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Horizon"), contentPanel);
        CreateButtonWithBackgroundUI(13, AppDisplayConstants.MainMenuSet7.KAELTHRA, ImageConstants.Background.ADVANCED_BACKGROUND_13_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Kaelthra"), contentPanel);
        CreateButtonWithBackgroundUI(14, AppDisplayConstants.MainMenuSet7.LUMINARY, ImageConstants.Background.ADVANCED_BACKGROUND_14_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Luminary"), contentPanel);
        CreateButtonWithBackgroundUI(15, AppDisplayConstants.MainMenuSet7.MORVANE, ImageConstants.Background.ADVANCED_BACKGROUND_15_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Morvane"), contentPanel);
        CreateButtonWithBackgroundUI(16, AppDisplayConstants.MainMenuSet7.NEOTERRA, ImageConstants.Background.ADVANCED_BACKGROUND_16_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Neoterra"), contentPanel);

        CreateButtonWithBackgroundUI(17, AppDisplayConstants.MainMenuSet7.NEXARIUM, ImageConstants.Background.ADVANCED_BACKGROUND_17_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Nexarium"), contentPanel);
        CreateButtonWithBackgroundUI(18, AppDisplayConstants.MainMenuSet7.NOVA, ImageConstants.Background.ADVANCED_BACKGROUND_18_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Nova"), contentPanel);
        CreateButtonWithBackgroundUI(19, AppDisplayConstants.MainMenuSet7.OMNIVEX, ImageConstants.Background.ADVANCED_BACKGROUND_19_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Omnivex"), contentPanel);
        CreateButtonWithBackgroundUI(20, AppDisplayConstants.MainMenuSet7.PARADOX, ImageConstants.Background.ADVANCED_BACKGROUND_20_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Paradox"), contentPanel);
        CreateButtonWithBackgroundUI(21, AppDisplayConstants.MainMenuSet7.THRENODY, ImageConstants.Background.ADVANCED_BACKGROUND_21_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Threnody"), contentPanel);
        CreateButtonWithBackgroundUI(22, AppDisplayConstants.MainMenuSet7.VELKRYN, ImageConstants.Background.ADVANCED_BACKGROUND_22_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Velkryn"), contentPanel);
        CreateButtonWithBackgroundUI(23, AppDisplayConstants.MainMenuSet7.XARPHIS, ImageConstants.Background.ADVANCED_BACKGROUND_23_URL, TextureHelper.LoadTexture2DCached($"UI/Button/Main/Xarphis"), contentPanel);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAbyssalManager>().CreateMainMenuAbyssalManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuArcaneManager>().CreateMainMenuArcaneManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAshframeManager>().CreateMainMenuAshframeManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAstrionManager>().CreateMainMenuAstrionManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuAxiomataManager>().CreateMainMenuAxiomataManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuChronyxManager>().CreateMainMenuChronyxManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuCognitumManager>().CreateMainMenuCognitumManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuContinuumManager>().CreateMainMenuContinuumManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuCosmosManager>().CreateMainMenuCosmosManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuEternumManager>().CreateMainMenuEternumManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuFerrumaxManager>().CreateMainMenuFerrumaxManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuHorizonManager>().CreateMainMenuHorizonManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuKaelthraManager>().CreateMainMenuKaelthraManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuLuminaryManager>().CreateMainMenuLuminaryManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuMorvaneManager>().CreateMainMenuMorvaneManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuNeoterraManager>().CreateMainMenuNeoterraManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuNexariumManager>().CreateMainMenuNexariumManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_18", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuNovaManager>().CreateMainMenuNovaManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_19", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuOmnivexManager>().CreateMainMenuOmnivexManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_20", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuParadoxManager>().CreateMainMenuParadoxManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_21", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuThrenodyManager>().CreateMainMenuThrenodyManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_22", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuVelkrynManager>().CreateMainMenuVelkrynManagerAsync(stat);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_23", contentPanel, async () =>
        {
            await FindAnyObjectByType<MainMenuXarphisManager>().CreateMainMenuXarphisManagerAsync(stat);
        });
    }
}