using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton instance
    public static UIManager Instance { get; private set; }

    [Header("Panels")]
    public Transform RootPanel;
    public Transform notificationPanel;
    public Transform WaitingPanel;
    public Transform MainPanel;
    public Transform popupPanel;
    public Transform LoadingPanel;


    // Prefab references
    [Header("General")]
    public GameObject SignInPanel;
    public GameObject SignUpPanel;
    public GameObject CreateNamePanel;
    public GameObject buttonPrefab;
    public GameObject itemSecondPrefab;
    public GameObject notificationPrefab;
    public GameObject DictionaryPanel;
    public GameObject TabButton;
    public GameObject ItemPrefab;
    public GameObject MainButtonPrefab;
    public GameObject MainPanelPrefab;
    public GameObject MainMenuPanel;
    public GameObject MainMenuShopPanel;
    public GameObject MainMenuEnhancementPanel;
    public GameObject MainMenuCampaignPanel;
    [Header("Profile")]
    public GameObject ProfilePanelPrefab;
    [Header("Setting")]
    public GameObject SettingPanelPrefab;
    public GameObject SettingButtonPrefab;
    [Header("News")]
    public GameObject NewsPanelPrefab;
    public GameObject NewsButtonPrefab;
    [Header("Language")]
    public GameObject LanguageButtonPrefab;
    [Header("Edit Name")]
    public GameObject EditNamePanelPrefab;
    [Header("Currency")]
    public GameObject CurrencyPanelPrefab;
    [Header("Feature")]
    public GameObject FeaturePanelPrefab;
    public GameObject FeatureButtonPrefab;
    [Header("General")]
    public GameObject quantityPopupPrefab;
    public GameObject equipmentsPrefab;
    public GameObject equipmentsShopPrefab;
    public GameObject currencyPrefab;
    public GameObject EquipmentsPanelPrefab;
    public GameObject CardsPrefab;
    public GameObject CardsSecondPrefab;
    public GameObject EquipmentSecondPrefab;
    public GameObject EquipmentFirstPrefab;
    public GameObject EquipmentFourthPrefab;
    public GameObject ArtworkFirstPrefab;
    public GameObject ArtworkSecondPrefab;
    public GameObject ElementDetailsPrefab;
    public GameObject ElementDetails2Prefab;
    public GameObject MainMenuDetailPanelPrefab;
    public GameObject MainMenuDetailPanel2Prefab;
    public GameObject SummonPanelPrefab;
    public GameObject PositionPrefab;
    public GameObject CampaignPrefab;
    public GameObject CampaignDetailPrefab;
    public GameObject ShopManagerPrefab;
    public GameObject ShopButtonPrefab;
    public GameObject ShopPrefab;
    public GameObject NumberDetailPrefab;
    public GameObject NumberDetail2Prefab;
    public GameObject NumberDetail3Prefab;
    public GameObject TabButton2;
    public GameObject TabButton4;
    public GameObject ReceivedNotification;
    public GameObject ItemThird;
    public GameObject TabButton5;
    public GameObject TabButton6;
    public GameObject AdvancedButtonFirst;
    public GameObject PopupTeamFirstPrefab;
    public GameObject PopupTeamSecondPrefab;
    public GameObject TeamsPanelPrefab;
    public GameObject TeamsPositionPrefab;
    public GameObject TeamTypePrefab;
    public GameObject TeamSlotPrefab;
    public GameObject CardsThirdPrefab;
    public GameObject StarPrefab;
    public GameObject PowerPrefab;
    public GameObject LoadingPanelPrefab;
    public GameObject SkillPanelPrefab;
    public GameObject SkillGroupPrefab;
    public GameObject Skill1Prefab;
    public GameObject Skill2Prefab;
    public GameObject PopupSkillDetailPrefab;
    public GameObject PopupButtonPrefab;
    public GameObject CardContractPrefab;
    public GameObject CardPenaltyPrefab;
    [Header("General Panel")]
    public GameObject WorldPanelPrefab;
    public GameObject CityPanelPrefab;
    public GameObject BasePanelPrefab;
    public GameObject TrainPanelPrefab;
    public GameObject ResearchPanelPrefab;
    public GameObject EmployeePanelPrefab;
    [Header("General Button")]
    public GameObject WorldButtonPrefab;
    public GameObject CityButtonPrefab;
    public GameObject BaseButtonPrefab;
    public GameObject TrainButtonPrefab;
    public GameObject ResearchButtonPrefab;
    public GameObject EmployeeButtonPrefab;
    public GameObject RareButtonPrefab;
    [Header("Science Fiction")]
    public GameObject ReactorPanelPrefab;
    public GameObject ReactorButtonPrefab;
    public GameObject ReactorPanelNumberPrefab;
    
    [Header("Other")]
    public GameObject MainMenuAnimePanelPrefab;
    public GameObject AnimePanelPrefab;
    public GameObject MasterBoardPanelPrefab;
    public GameObject DailyCheckinPanelPrefab;
    public GameObject ArenaPanelPrefab;
    public GameObject ArenaDetailsPanelPrefab;
    public GameObject TowerDetailsPanelPrefab;
    public GameObject PopupEquipmentsPanelPrefab;
    public GameObject PopupSpiritBeastPanelPrefab;
    public GameObject PopupSkillsPanelPrefab;
    public GameObject PopupMenuPanelPrefab;
    public GameObject EquipmentsWearingPrefab;
    public GameObject Slot1Prefab;
    public GameObject Slot4Prefab;
    public GameObject Slot6Prefab;
    public GameObject Slot8Prefab;
    public GameObject Slot10Prefab;
    public GameObject Slot12Prefab;
    public GameObject Slot14Prefab;
    public GameObject Slot16Prefab;
    public GameObject ArenaButtonPrefab;
    public GameObject AnimeButtonPrefab;
    public GameObject DailyCheckinComponentPrefab;
    
    [Header("Other")]
    public GameObject AnimeSlotPrefab;
    public GameObject MasterBoardNodePrefab;
    public GameObject MasterBoardPopupPrefab;
    public GameObject ArenaSlotPrefab;
    [Header("Font")]
    public TMP_FontAsset EuroStyleNormalFont;

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
    public Transform GetTransform(string panelName)
    {
        switch (panelName)
        {
            case "RootPanel":
                return RootPanel;
            case "MainPanel":
                return MainPanel;
            case "WaitingPanel":
                return WaitingPanel;
            case "notificationPanel":
                return notificationPanel;
            case "popupPanel":
                return popupPanel;
            case "LoadingPanel":
                return LoadingPanel;
            default:
                Debug.LogWarning($"Panel {panelName} not found.");
                return null;
        }
    }
    public GameObject GetGameObject(string prefabName)
    {
        switch (prefabName)
        {
            case "SignInPanel":
                return SignInPanel;
            case "SignUpPanel":
                return SignUpPanel;
            case "CreateNamePanel":
                return CreateNamePanel;
            case "buttonPrefab":
                return buttonPrefab;
            case "itemSecondPrefab":
                return itemSecondPrefab;
            case "notificationPrefab":
                return notificationPrefab;
            case "DictionaryPanel":
                return DictionaryPanel;
            case "TabButton":
                return TabButton;
            case "ItemPrefab":
                return ItemPrefab;
            case "MainButtonPrefab":
                return MainButtonPrefab;
            case "MainMenuPanel":
                return MainMenuPanel;
            case "MainPanelPrefab":
                return MainPanelPrefab;
            case "MainMenuShopPanel":
                return MainMenuShopPanel;
            case "MainMenuEnhancementPanel":
                return MainMenuEnhancementPanel;
            case "MainMenuCampaignPanel":
                return MainMenuCampaignPanel;
            case "quantityPopupPrefab":
                return quantityPopupPrefab;
            case "equipmentsPrefab":
                return equipmentsPrefab;
            case "equipmentsShopPrefab":
                return equipmentsShopPrefab;
            case "currencyPrefab":
                return currencyPrefab;
            case "EquipmentsPanelPrefab":
                return EquipmentsPanelPrefab;
            case "CardsPrefab":
                return CardsPrefab;
            case "CardsSecondPrefab":
                return CardsSecondPrefab;
            case "CardsThirdPrefab":
                return CardsThirdPrefab;
            case "EquipmentSecondPrefab":
                return EquipmentSecondPrefab;
            case "EquipmentFirstPrefab":
                return EquipmentFirstPrefab;
            case "EquipmentFourthPrefab":
                return EquipmentFourthPrefab;
            case "ArtworkSecondPrefab":
                return ArtworkSecondPrefab;
            case "ArtworkFirstPrefab":
                return ArtworkFirstPrefab;
            case "ElementDetailsPrefab":
                return ElementDetailsPrefab;
            case "MainMenuDetailPanelPrefab":
                return MainMenuDetailPanelPrefab;
            case "MainMenuDetailPanel2Prefab":
                return MainMenuDetailPanel2Prefab;
            case "SummonPanelPrefab":
                return SummonPanelPrefab;
            case "PositionPrefab":
                return PositionPrefab;
            case "CampaignPrefab":
                return CampaignPrefab;
            case "CampaignDetailPrefab":
                return CampaignDetailPrefab;
            case "ShopButtonPrefab":
                return ShopButtonPrefab;
            case "ShopManagerPrefab":
                return ShopManagerPrefab;
            case "ShopPrefab":
                return ShopPrefab;
            case "NumberDetailPrefab":
                return NumberDetailPrefab;
            case "NumberDetail2Prefab":
                return NumberDetail2Prefab;
            case "NumberDetail3Prefab":
                return NumberDetail3Prefab;
            case "TabButton2":
                return TabButton2;
            case "TabButton4":
                return TabButton4;
            case "ReceivedNotification":
                return ReceivedNotification;
            case "ItemThird":
                return ItemThird;
            case "PopupTeamFirstPrefab":
                return PopupTeamFirstPrefab;
            case "PopupTeamSecondPrefab":
                return PopupTeamSecondPrefab;
            case "TeamsPanelPrefab":
                return TeamsPanelPrefab;
            case "TeamsPositionPrefab":
                return TeamsPositionPrefab;
            case "TeamTypePrefab":
                return TeamTypePrefab;
            case "TeamSlotPrefab":
                return TeamSlotPrefab;
            case "StarPrefab":
                return StarPrefab;
            case "ElementDetails2Prefab":
                return ElementDetails2Prefab;
            case "PowerPrefab":
                return PowerPrefab;
            case "LoadingPanelPrefab":
                return LoadingPanelPrefab;
            case "SkillGroupPrefab":
                return SkillGroupPrefab;
            case "SkillPanelPrefab":
                return SkillPanelPrefab;
            case "Skill1Prefab":
                return Skill1Prefab;
            case "Skill2Prefab":
                return Skill2Prefab;
            case "PopupSkillDetailPrefab":
                return PopupSkillDetailPrefab;
            case "CardPenaltyPrefab":
                return CardPenaltyPrefab;
            case "CardContractPrefab":
                return CardContractPrefab;

            case "MainMenuAnimePanelPrefab":
                return MainMenuAnimePanelPrefab;
            case "ArenaPanelPrefab":
                return ArenaPanelPrefab;
            case "AnimePanelPrefab":
                return AnimePanelPrefab;
            case "MasterBoardPanelPrefab":
                return MasterBoardPanelPrefab;
            case "DailyCheckinPanelPrefab":
                return DailyCheckinPanelPrefab;
            case "ArenaDetailsPanelPrefab":
                return ArenaDetailsPanelPrefab;
            case "TowerDetailsPanelPrefab":
                return TowerDetailsPanelPrefab;
            case "PopupEquipmentsPanelPrefab":
                return PopupEquipmentsPanelPrefab;
            case "PopupSpiritBeastPanelPrefab":
                return PopupSpiritBeastPanelPrefab;
            case "PopupMenuPanelPrefab":
                return PopupMenuPanelPrefab;
            case "PopupSkillsPanelPrefab":
                return PopupSkillsPanelPrefab;
            case "EquipmentsWearingPrefab":
                return EquipmentsWearingPrefab;
            case "Slot1Prefab":
                return Slot1Prefab;
            case "Slot4Prefab":
                return Slot4Prefab;
            case "Slot6Prefab":
                return Slot6Prefab;
            case "Slot8Prefab":
                return Slot8Prefab;
            case "Slot10Prefab":
                return Slot10Prefab;
            case "Slot12Prefab":
                return Slot12Prefab;
            case "Slot14Prefab":
                return Slot14Prefab;
            case "Slot16Prefab":
                return Slot16Prefab;
            case "ArenaButtonPrefab":
                return ArenaButtonPrefab;
            case "AnimeButtonPrefab":
                return AnimeButtonPrefab;
            case "DailyCheckinComponentPrefab":
                return DailyCheckinComponentPrefab;
            case "AnimeSlotPrefab":
                return AnimeSlotPrefab;
            case "MasterBoardNodePrefab":
                return MasterBoardNodePrefab;
            case "MasterBoardPopupPrefab":
                return MasterBoardPopupPrefab;
            case "ArenaSlotPrefab":
                return ArenaSlotPrefab;
            case "TabButton5":
                return TabButton5;
            case "TabButton6":
                return TabButton6;
            case "PopupButtonPrefab":
                return PopupButtonPrefab;
            case "AdvancedButtonFirst":
                return AdvancedButtonFirst;
            
            default:
                Debug.LogWarning($"Prefab {prefabName} not found.");
                return null;
        }
    }
    public GameObject GetFeaturePanel(string prefabName)
    {
        switch (prefabName)
        {
            case "FeaturePanelPrefab":
                return FeaturePanelPrefab;
            case "FeatureButtonPrefab":
                return FeatureButtonPrefab;
            default:
                return FeaturePanelPrefab;
        }
    }
    public GameObject GetProfilePanel(string prefabName)
    {
        switch (prefabName)
        {
            case "ProfilePanelPrefab":
                return ProfilePanelPrefab;
            default:
                return ProfilePanelPrefab;
        }
    }
    public GameObject GetCurrencyPanel(string prefabName)
    {
        switch (prefabName)
        {
            case "CurrencyPanelPrefab":
                return CurrencyPanelPrefab;
            default:
                return CurrencyPanelPrefab;
        }
    }
    public GameObject GetEditNamePanel(string prefabName)
    {
        switch (prefabName)
        {
            case "EditNamePanelPrefab":
                return EditNamePanelPrefab;
            default:
                return EditNamePanelPrefab;
        }
    }
    public GameObject GetLanguagePanel(string prefabName)
    {
        switch (prefabName)
        {
            case "LanguageButtonPrefab":
                return LanguageButtonPrefab;
            default:
                return LanguageButtonPrefab;
        }
    }
    public GameObject GetSettingPanel(string prefabName)
    {
        switch (prefabName)
        {
            case "SettingPanelPrefab":
                return SettingPanelPrefab;
            case "SettingButtonPrefab":
                return SettingButtonPrefab;
            default:
                return SettingPanelPrefab;
        }
    }
    public GameObject GetNewsPanel(string prefabName)
    {
        switch (prefabName)
        {
            case "NewsPanelPrefab":
                return NewsPanelPrefab;
            case "NewsButtonPrefab":
                return NewsButtonPrefab;
            default:
                return NewsPanelPrefab;
        }
    }
    public GameObject GetGameObjectScienceFiction(string prefabName)
    {
        switch (prefabName)
        {
            case "ReactorPanelPrefab":
                return ReactorPanelPrefab;
            case "ReactorButtonPrefab":
                return ReactorButtonPrefab;

            case "ReactorPanelNumberPrefab":
                return ReactorPanelNumberPrefab;

            default:
                Debug.LogWarning($"Prefab name '{prefabName}' not found!");
                return null;
        }
    }
    public GameObject GetGeneralPanel(string prefabName)
    {
        switch (prefabName)
        {
            case "WorldPanelPrefab":
                return WorldPanelPrefab;
            case "CityPanelPrefab":
                return CityPanelPrefab;
            case "BasePanelPrefab":
                return BasePanelPrefab;
            case "TrainPanelPrefab":
                return TrainPanelPrefab;
            case "ResearchPanelPrefab":
                return ResearchPanelPrefab;
            case "EmployeePanelPrefab":
                return EmployeePanelPrefab;
            default:
                return WorldButtonPrefab;
        }
    }
    public GameObject GetGeneralButton(string prefabName)
    {
        switch (prefabName)
        {
            case "WorldButtonPrefab":
                return WorldButtonPrefab;
            case "CityButtonPrefab":
                return CityButtonPrefab;
            case "BaseButtonPrefab":
                return BaseButtonPrefab;
            case "TrainButtonPrefab":
                return TrainButtonPrefab;
            case "ResearchButtonPrefab":
                return ResearchButtonPrefab;
            case "EmployeeButtonPrefab":
                return EmployeeButtonPrefab;
            case "RareButtonPrefab":
                return RareButtonPrefab;
            default:
                return WorldButtonPrefab;
        }
    }
    public TMP_FontAsset GetTMPFontAsset(string fontName)
    {
        switch (fontName)
        {
            case "EuroStyleNormalFont":
                return EuroStyleNormalFont;
            default:
                return EuroStyleNormalFont;
        }
    }
    public void SetUI(GameObject gameObject, string type, int level = 0, string mainType = "")
    {
        if (mainType.Equals(AppConstants.MainMenuSet1.AFFINITY) || mainType.Equals(AppConstants.MainMenuSet1.BLESSING))
        {
            return;
        }
        Transform BackgroundImageTransform = gameObject.transform.Find("Background");
        if (BackgroundImageTransform != null)
        {
            RawImage BackgroundImage = gameObject.transform.Find("Background").GetComponent<RawImage>();
            Texture backgroundTexture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            BackgroundImage.texture = backgroundTexture;
            BackgroundImage.rectTransform.sizeDelta = new Vector2(350, 350);
        }

        Transform backgroundTransform = gameObject.transform.Find("BackgroundCircle");
        if (backgroundTransform != null)
        {
            RawImage backgroundImageCircle = backgroundTransform.GetComponent<RawImage>();
            if (backgroundImageCircle != null)
            {
                backgroundImageCircle.gameObject.AddComponent<RotateAnimation>();
            }
        }

        int totalSkills = 10;
        int levelsPerSkill = 1000;

        // Đặt tất cả kỹ năng về trạng thái mặc định (đen + text "0/1000")
        for (int i = 1; i <= totalSkills; i++)
        {
            Transform aptitudeSkill = gameObject.transform.Find($"UpgradeSkill{i}");
            if (aptitudeSkill == null) continue;

            RawImage aptitudeImage = aptitudeSkill.Find("AptitudeImage").GetComponent<RawImage>();
            TextMeshProUGUI levelText = aptitudeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

            Texture texture = Resources.Load<Texture>($"UI/Rank/{type}");
            aptitudeImage.texture = texture;

            if (aptitudeImage != null) aptitudeImage.color = Color.black;
            if (levelText != null) levelText.text = $"0/{levelsPerSkill}";
        }

        // Xác định số kỹ năng được kích hoạt
        int activeSkillsCount = Mathf.Clamp((level / levelsPerSkill), 1, totalSkills);
        for (int i = 1; i <= activeSkillsCount; i++)
        {
            Transform activeSkill = gameObject.transform.Find($"UpgradeSkill{i}");
            if (activeSkill != null)
            {
                RawImage activeImage = activeSkill.Find("AptitudeImage").GetComponent<RawImage>();
                TextMeshProUGUI activeLevelText = activeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

                if (activeImage != null && level != 0) activeImage.color = Color.white;

                if (activeLevelText != null)
                {
                    // Kiểm tra nếu level là bội số của levelsPerSkill (1000, 2000, ..., 10000)
                    int displayedLevel = (level % levelsPerSkill == 0) ? levelsPerSkill : level % levelsPerSkill;
                    activeLevelText.text = $"{displayedLevel}/{levelsPerSkill}";
                }
            }
        }
        TextMeshProUGUI LevelText = gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        LevelText.text = level.ToString();
    }
    public async Task SetMaterialUIAsync(GameObject gameobject, string itemImage, double itemQuantity, double currencyQuantity, int rankLevel, int maxLevel)
    {
        Transform currencyPanel = gameobject.transform.Find("DictionaryCards/Currency");
        List<Currencies> currencies = await UserCurrenciesService.Create().GetUserCurrencyAsync(User.CurrentUserId);
        ButtonEvent.Instance.Close(currencyPanel);
        CurrenciesManager.Instance.GetMainCurrency(currencies, currencyPanel);

        var oneResult = EvaluateItem.CalculateLevelUp(itemQuantity, currencyQuantity, 1, 10, rankLevel, false, maxLevel);
        var maxResult = EvaluateItem.CalculateLevelUp(itemQuantity, currencyQuantity, 1, 10, rankLevel, true, maxLevel);
        RawImage OneLevelCurrencyImage = gameobject.transform.Find("DictionaryCards/OneLevelCurrency/CurrencyImage").GetComponent<RawImage>();
        RawImage MaxLevelCurrencyImage = gameobject.transform.Find("DictionaryCards/MaxLevelCurrency/CurrencyImage").GetComponent<RawImage>();
        Texture OneLevelCurrencyTexture = Resources.Load<Texture>($"{ImageConstants.Currency.SILVER}");
        Texture MaxLevelCurrencyTexture = Resources.Load<Texture>($"{ImageConstants.Currency.SILVER}");
        OneLevelCurrencyImage.texture = OneLevelCurrencyTexture;
        MaxLevelCurrencyImage.texture = MaxLevelCurrencyTexture;

        TextMeshProUGUI OneLevelCurrencyText = gameobject.transform.Find("DictionaryCards/OneLevelCurrency/QuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI MaxLevelCurrencyText = gameobject.transform.Find("DictionaryCards/MaxLevelCurrency/QuantityText").GetComponent<TextMeshProUGUI>();
        OneLevelCurrencyText.text = oneResult.totalCurrencyUsed.ToString();
        MaxLevelCurrencyText.text = maxResult.totalCurrencyUsed.ToString();

        Transform OneLevelMaterial = gameobject.transform.Find("DictionaryCards/OneLevelMaterial");
        Transform MaxLevelMaterial = gameobject.transform.Find("DictionaryCards/MaxLevelMaterial");
        ButtonEvent.Instance.Close(OneLevelMaterial);
        ButtonEvent.Instance.Close(MaxLevelMaterial);
        GameObject oneLevelMaterialObject = Instantiate(ElementDetails2Prefab, OneLevelMaterial);
        GameObject maxLevelMaterialObject = Instantiate(ElementDetails2Prefab, MaxLevelMaterial);

        RawImage oneLevelImage = oneLevelMaterialObject.transform.Find("MaterialImage").GetComponent<RawImage>();
        Texture oneLevelTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(itemImage)}");
        oneLevelImage.texture = oneLevelTexture;

        // RectTransform oneLevelRectTransform = oneLevelImage.GetComponent<RectTransform>();
        // oneLevelRectTransform.sizeDelta = new Vector2(40, 40);

        TextMeshProUGUI oneLevelQuantity = oneLevelMaterialObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        oneLevelQuantity.text = itemQuantity + "/" + oneResult.totalMaterialUsed;

        RawImage maxLevelImage = maxLevelMaterialObject.transform.Find("MaterialImage").GetComponent<RawImage>();
        Texture maxLevelTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(itemImage)}");
        maxLevelImage.texture = maxLevelTexture;

        TextMeshProUGUI maxLevelQuantity = maxLevelMaterialObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        maxLevelQuantity.text = itemQuantity + "/" + maxResult.totalMaterialUsed;

        // RectTransform maxLevelRectTransform = maxLevelImage.GetComponent<RectTransform>();
        // maxLevelRectTransform.sizeDelta = new Vector2(40, 40);
    }
    public void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = Resources.Load<Texture>($"{image}");
            if (texture != null)
            {
                buttonImage.texture = texture;
            }
            else
            {
                Debug.LogError($"Texture '{image}' not found in Resources.");
            }
        }
        else
        {
            Debug.LogError("Button does not have a RawImage component.");
        }
    }
    public void CreateLevelUI(int level, GameObject currentObject)
    {
        TextMeshProUGUI currentLevelText = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/Level/CurrentLevelText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nextLevelText = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/Level/NextLevelText").GetComponent<TextMeshProUGUI>();
        currentLevelText.text = level.ToString();
        int nextLevel = level + 1;
        if ((int)level == 100000)
        {
            nextLevelText.text = "Max";
        }
        else
        {
            nextLevelText.text = nextLevel.ToString();
        }
    }
    public void CreatePropertyUI(int status, PropertyInfo[] properties, object targetObject, GameObject currentObject)
    {
        Transform detailsContent = currentObject.transform.Find("DictionaryCards/Content/DetailsPanel/Scroll View/Viewport/Content");

        Transform generalInformationPanel = detailsContent.transform.Find("GeneralInformation");
        Transform statsInformationPanel = detailsContent.transform.Find("StatInformation");
        Transform descriptionInformationPanel = detailsContent.transform.Find("DescriptionInformation");

        foreach (var property in properties)
        {
            object value = property.GetValue(targetObject, null);

            CreateSinglePropertyUI(property, value,
                generalInformationPanel, statsInformationPanel, descriptionInformationPanel);
        }
    }
    public void CreateSinglePropertyUI(PropertyInfo property, object value,
    Transform generalInformationPanel, Transform statsInformationPanel, Transform descriptionInformationPanel)
    {
        // Transform DetailsContent = currentObject.transform.Find("DictionaryCards/Content/DetailsPanel/Scroll View/Viewport/Content");
        // GameObject firstDetailsObject = Instantiate(NumberDetail2Prefab, DetailsContent);
        // GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, DetailsContent);
        // GameObject elementDetails2Object = Instantiate(NumberDetail3Prefab, DetailsContent);
        // GameObject descriptionDetailsObject = Instantiate(NumberDetail3Prefab, DetailsContent);
        // Transform firstPopupPanel = firstDetailsObject.transform.Find("ElementDetails");
        // Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        // Transform element2PopupPanel = elementDetails2Object.transform.Find("ElementDetails");
        // Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");
        if (!property.Name.Equals(AppConstants.StatFields.ID) && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals(AppConstants.StatFields.EXPERIMENT) && !property.Name.Equals(AppConstants.StatFields.QUANTITY) 
                && !property.Name.Equals(AppConstants.StatFields.BLOCK)
                && !property.Name.Equals(AppConstants.StatFields.STATUS) && !property.Name.Equals(AppConstants.StatFields.NAME)
                && !property.Name.Equals(AppConstants.StatFields.IMAGE))
        {
            if (property.Name.Equals(AppConstants.StatFields.DESCRIPTION))
            {
                // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                GameObject descriptionTextObject = new GameObject("DescriptionText");
                descriptionTextObject.transform.SetParent(descriptionInformationPanel, false); // Thêm vào panel với vị trí chính xác

                // Thêm component TextMeshProUGUI vào đối tượng mới
                TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                // Đổi màu chữ bằng mã hex #844000
                Color color;
                if (ColorUtility.TryParseHtmlString(ColorConstants.HexColor.descriptionColor, out color)) // Chuyển mã hex thành Color
                {
                    descriptionText.color = color; // Gán màu cho text
                }

                // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(600, 500);
                // rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                // GridLayoutGroup gridLayout = descriptionPopupPanel.GetComponent<GridLayoutGroup>();
                // if (gridLayout != null)
                // {
                //     gridLayout.cellSize = new Vector2(670, 800);
                // }
            }
            else if (property.Name.Equals(AppConstants.StatFields.POWER) 
            || property.Name.Equals(AppConstants.StatFields.RARE) || property.Name.Equals(AppConstants.StatFields.TYPE)
            || property.Name.Equals(AppConstants.StatFields.STAR) || property.Name.Equals(AppConstants.StatFields.LEVEL))
            {
                if (value != null)
                {
                    bool shouldDisplay = false;

                    if (value is int intValue)
                    {
                        shouldDisplay = intValue != -1;
                    }
                    else if (value is double doubleValue)
                    {
                        shouldDisplay = doubleValue != -1;
                    }
                    else if (value is string)
                    {
                        shouldDisplay = true;
                    }

                    if (shouldDisplay)
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, generalInformationPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = value.ToString();
                    }
                }

            }
            else if (property.Name.Equals(AppConstants.StatFields.HEALTH)
            || property.Name.Equals(AppConstants.StatFields.PHYSICAL_ATTACK)
            || property.Name.Equals(AppConstants.StatFields.PHYSICAL_DEFENSE)
            || property.Name.Equals(AppConstants.StatFields.MAGICAL_ATTACK)
            || property.Name.Equals(AppConstants.StatFields.MAGICAL_DEFENSE)
            || property.Name.Equals(AppConstants.StatFields.CHEMICAL_ATTACK)
            || property.Name.Equals(AppConstants.StatFields.CHEMICAL_DEFENSE)
            || property.Name.Equals(AppConstants.StatFields.ATOMIC_ATTACK)
            || property.Name.Equals(AppConstants.StatFields.ATOMIC_DEFENSE)
            || property.Name.Equals(AppConstants.StatFields.MENTAL_ATTACK)
            || property.Name.Equals(AppConstants.StatFields.MENTAL_DEFENSE))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, statsInformationPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = NumberFormatter.FormatNumber(intValue, false);

                        RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                        CreatePropertyRuneUI(property.Name, runeImage);
                    }
                }
            }
            // else if (property.Name.Equals(AppConstants.StatFields.SPEED)
            // || property.Name.Equals(AppConstants.StatFields.CRITICAL_DAMAGE_RATE)
            // || property.Name.Equals(AppConstants.StatFields.CRITICAL_RATE)
            // || property.Name.Equals(AppConstants.StatFields.CRITICAL_RESISTANCE_RATE)
            // || property.Name.Equals(AppConstants.StatFields.IGNORE_CRITICAL_RATE)
            // || property.Name.Equals(AppConstants.StatFields.PENETRATION_RATE)
            // || property.Name.Equals(AppConstants.StatFields.PENETRATION_RESISTANCE_RATE)
            // || property.Name.Equals(AppConstants.StatFields.EVASION_RATE)
            // || property.Name.Equals(AppConstants.StatFields.DAMAGE_ABSORPTION_RATE)
            // || property.Name.Equals(AppConstants.StatFields.IGNORE_DAMAGE_ABSORPTION_RATE)
            // || property.Name.Equals(AppConstants.StatFields.ABSORBED_DAMAGE_RATE)
            // || property.Name.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RATE)
            // || property.Name.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RESISTANCE_RATE))
            // {
            //     // Kiểm tra nếu value không phải null
            //     if (value != null)
            //     {
            //         if (value is double intValue && intValue != -1)
            //         {
            //             // Tạo một element mới từ prefab
            //             GameObject elementObject = Instantiate(ElementDetailsPrefab, element2PopupPanel);

            //             // Gán tên thuộc tính vào TitleText
            //             TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            //             if (elementTitleText != null)
            //                 elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

            //             // Gán giá trị thuộc tính vào ContentText
            //             TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
            //             if (elementContentText != null)
            //                 elementContentText.text = intValue.ToString();

            //             RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
            //             CreatePropertyRuneUI(property.Name, runeImage);
            //         }
            //     }
            // }
            // else if (property.Name.Equals(AppConstants.StatFields.ACCURACY_RATE)
            // || property.Name.Equals(AppConstants.StatFields.LIFE_STEAL_RATE)
            // || property.Name.Equals(AppConstants.StatFields.SHIELD_STRENGTH)
            // || property.Name.Equals(AppConstants.StatFields.TENACITY)
            // || property.Name.Equals(AppConstants.StatFields.RESISTANCE_RATE)
            // || property.Name.Equals(AppConstants.StatFields.COMBO_RATE)
            // || property.Name.Equals(AppConstants.StatFields.IGNORE_COMBO_RATE)
            // || property.Name.Equals(AppConstants.StatFields.COMBO_DAMAGE_RATE)
            // || property.Name.Equals(AppConstants.StatFields.COMBO_RESISTANCE_RATE)
            // || property.Name.Equals(AppConstants.StatFields.STUN_RATE)
            // || property.Name.Equals(AppConstants.StatFields.IGNORE_STUN_RATE)
            // || property.Name.Equals(AppConstants.StatFields.MANA)
            // || property.Name.Equals(AppConstants.StatFields.MANA_REGENERATION_RATE)
            // || property.Name.Equals(AppConstants.StatFields.REFLECTION_RATE)
            // || property.Name.Equals(AppConstants.StatFields.IGNORE_REFLECTION_RATE)
            // || property.Name.Equals(AppConstants.StatFields.REFLECTION_DAMAGE_RATE)
            // || property.Name.Equals(AppConstants.StatFields.REFLECTION_RESISTANCE_RATE))
            // {
            //     // Kiểm tra nếu value không phải null
            //     if (value != null)
            //     {
            //         if (value is double intValue && intValue != -1)
            //         {
            //             // Tạo một element mới từ prefab
            //             GameObject elementObject = Instantiate(ElementDetailsPrefab, element3PopupPanel);

            //             // Gán tên thuộc tính vào TitleText
            //             TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            //             if (elementTitleText != null)
            //                 elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

            //             // Gán giá trị thuộc tính vào ContentText
            //             TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
            //             if (elementContentText != null)
            //                 elementContentText.text = intValue.ToString();

            //             RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
            //             CreatePropertyRuneUI(property.Name, runeImage);
            //         }
            //     }
            // }
            // else if (property.Name.Equals(AppConstants.StatFields.DAMAGE_TO_DIFFERENT_FACTION_RATE)
            // || property.Name.Equals(AppConstants.StatFields.RESISTANCE_TO_DIFFERENT_FACTION_RATE)
            // || property.Name.Equals(AppConstants.StatFields.DAMAGE_TO_SAME_FACTION_RATE)
            // || property.Name.Equals(AppConstants.StatFields.RESISTANCE_TO_SAME_FACTION_RATE)
            // || property.Name.Equals(AppConstants.StatFields.NORMAL_DAMAGE_RATE)
            // || property.Name.Equals(AppConstants.StatFields.NORMAL_RESISTANCE_RATE)
            // || property.Name.Equals(AppConstants.StatFields.SKILL_DAMAGE_RATE)
            // || property.Name.Equals(AppConstants.StatFields.SKILL_RESISTANCE_RATE))
            // {
            //     // Kiểm tra nếu value không phải null
            //     if (value != null)
            //     {
            //         if (value is double intValue && intValue != -1)
            //         {
            //             // Tạo một element mới từ prefab
            //             GameObject elementObject = Instantiate(ElementDetailsPrefab, element4PopupPanel);

            //             // Gán tên thuộc tính vào TitleText
            //             TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            //             if (elementTitleText != null)
            //                 elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

            //             // Gán giá trị thuộc tính vào ContentText
            //             TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
            //             if (elementContentText != null)
            //                 elementContentText.text = intValue.ToString();

            //             RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
            //             CreatePropertyRuneUI(property.Name, runeImage);
            //         }
            //     }
            // }
        }
    }
    public void CreatePropertyRuneUI(string title, RawImage runeImage)
    {
        Texture runeTexture;
        if (title.Equals(AppConstants.StatFields.PHYSICAL_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.PHYSICAL_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.PHYSICAL_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.PHYSICAL_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MAGICAL_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MAGICAL_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MAGICAL_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MAGICAL_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CHEMICAL_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.CHEMICAL_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CHEMICAL_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.CHEMICAL_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ATOMIC_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.ATOMIC_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ATOMIC_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.ATOMIC_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MENTAL_ATTACK))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MENTAL_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MENTAL_DEFENSE))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.MENTAL_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.HEALTH))
        {
            runeTexture = Resources.Load<Texture>(ImageConstants.Rune.HEALTH_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.SPEED)) {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.SPEED_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CRITICAL_RATE)) {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.DAMAGE_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CRITICAL_DAMAGE_RATE)) {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.CRITICAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.DAMAGE_ABSORPTION_RATE)) {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ABSORPTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.CRITICAL_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.CRITICAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_CRITICAL_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.CRITICAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.PENETRATION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.PENETRATION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.PENETRATION_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.PENETRATION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.EVASION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.EVASION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_DAMAGE_ABSORPTION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ABSORPTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ABSORBED_DAMAGE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ABSORPTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.VITALITY_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.VITALITY_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.ACCURACY_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.ACCURACY_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.LIFE_STEAL_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.LIFESTEAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.SHIELD_STRENGTH))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.SHIELD_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.TENACITY))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.TENACITY_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.RESISTANCE_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.COMBO_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.COMBO_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_COMBO_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.COMBO_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.COMBO_DAMAGE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.COMBO_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.COMBO_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.COMBO_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.STUN_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.STUN_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_STUN_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.STUN_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MANA))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.MANA_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.MANA_REGENERATION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.MANA_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.REFLECTION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.REFLECTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.IGNORE_REFLECTION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.REFLECTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.REFLECTION_DAMAGE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.REFLECTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.REFLECTION_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.REFLECTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.DAMAGE_TO_DIFFERENT_FACTION_RATE) || title.Equals(AppConstants.StatFields.RESISTANCE_TO_DIFFERENT_FACTION_RATE) ||
         title.Equals(AppConstants.StatFields.DAMAGE_TO_SAME_FACTION_RATE) || title.Equals(AppConstants.StatFields.RESISTANCE_TO_SAME_FACTION_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.FACTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.NORMAL_DAMAGE_RATE) || title.Equals(AppConstants.StatFields.NORMAL_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.NORMAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals(AppConstants.StatFields.SKILL_DAMAGE_RATE) || title.Equals(AppConstants.StatFields.SKILL_RESISTANCE_RATE))
        {
            runeTexture = Resources.Load<Texture>($"{ImageConstants.Rune.SKILL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        runeImage.gameObject.SetActive(true);
    }
    public void CreatePropertyLevelUI(PropertyInfo[] properties, object targetObject, double increasePerLevel, GameObject currentObject)
    {
        Transform LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(targetObject, null);
            CreateSinglePropertyLevelUI(property, value, increasePerLevel, LevelElementContent, currentObject);
        }
    }
    public void CreateSinglePropertyLevelUI(PropertyInfo property, object value, double increasePerLevel,
    Transform LevelElementContent, GameObject currentObject)
    {

        if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
                && !property.Name.Equals("status") && !property.Name.Equals("name")
                && !property.Name.Equals("image") && !property.Name.Equals("description") && !property.Name.Equals("power")
                && !property.Name.Equals("rare") && !property.Name.Equals("type")
                && !property.Name.Equals("star") && !property.Name.Equals("level"))
        {
            // Kiểm tra nếu value không phải null
            if (value != null)
            {
                if (value is double intValue && intValue != -1)
                {
                    if (!property.Name.Contains("all"))
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, LevelElementContent);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                        {
                            double newintValue = increasePerLevel * intValue;
                            elementContentText.text = "+" + NumberFormatter.FormatNumber(newintValue, false);
                            Color greenColor;
                            if (ColorUtility.TryParseHtmlString(ColorConstants.GREEN_COLOR, out greenColor)) // Màu xanh lá LimeGreen
                            {
                                elementContentText.color = greenColor;
                                elementContentText.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, Color.green); // Màu phát sáng
                                elementContentText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.5f); // Độ mạnh phát sáng (giảm giá trị
                            }
                        }
                    }
                }
            }
        }
        else if (property.Name.Equals("level"))
        {
            CreateLevelUI((int)value, currentObject);
        }
    }
    public void CreatePropertyUpgradeUI(PropertyInfo property, object value, double increasePerUpgrade, GameObject currentObject)
    {
        Transform UpgradeElementContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
                && !property.Name.Equals("status") && !property.Name.Equals("name")
                && !property.Name.Equals("image") && !property.Name.Equals("description") && !property.Name.Equals("power")
                && !property.Name.Equals("rare") && !property.Name.Equals("type")
                && !property.Name.Equals("star") && !property.Name.Equals("level"))
        {
            // Kiểm tra nếu value không phải null
            if (value != null)
            {
                if (value is double intValue && intValue != -1)
                {
                    if (!property.Name.Contains("all"))
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, UpgradeElementContent);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                        {
                            double newintValue = increasePerUpgrade * intValue;
                            elementContentText.text = "+" + NumberFormatter.FormatNumber(newintValue, false);
                            Color greenColor;
                            if (ColorUtility.TryParseHtmlString(ColorConstants.GREEN_COLOR, out greenColor)) // Màu xanh lá LimeGreen
                            {
                                elementContentText.color = greenColor;
                                elementContentText.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, Color.green); // Màu phát sáng
                                elementContentText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.5f); // Độ mạnh phát sáng (giảm giá trị
                            }
                        }
                    }
                }
            }
        }
        else if (property.Name.Equals("star"))
        {
            TextMeshProUGUI currentLevelText = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/CurrentLevelText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nextLevelText = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/NextLevelText").GetComponent<TextMeshProUGUI>();
            currentLevelText.text = value.ToString();
            int nextLevel = (int)value + 1;
            if ((int)value == 100000)
            {
                nextLevelText.text = "Max";
            }
            else
            {
                nextLevelText.text = nextLevel.ToString();
            }
        }
    }
    public void CreateMaterialUI(List<Items> items, GameObject currentObject)
    {
        Transform LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        foreach (Items item in items)
        {
            GameObject itemObject = Instantiate(ItemThird, LevelMaterialContent);

            RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
            Texture equipmentTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(item.Image)}");
            eImage.texture = equipmentTexture;

            TextMeshProUGUI eQuantity = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
            eQuantity.text = NumberFormatter.FormatNumber(item.Quantity, false);
        }
    }
    public void CreateStarUI(int star, GameObject currentObject)
    {
        Transform currentStar = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/CurrentStar");
        Transform nextStar = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/NextStar");
        int currentimageIndex = (star == 0) ? 0 : ((star - 1) % 10) + 1;
        int currentstarIndex = (star == 0) ? 0 : (star - 1) / 10;
        int newStar = (star + 1 > 100000) ? 0 : star + 1;
        int nextimageIndex = (newStar == 0) ? 0 : ((newStar - 1) % 10) + 1;
        int nextstarIndex = (newStar == 0) ? 0 : (newStar - 1) / 10;
        for (int i = 0; i < currentimageIndex; i++)
        {
            GameObject starObject = Instantiate(StarPrefab, currentStar);

            RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
            GetStarImage(starImage, currentstarIndex);
        }
        for (int i = 0; i < nextimageIndex; i++)
        {
            GameObject starObject = Instantiate(StarPrefab, nextStar);

            RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
            GetStarImage(starImage, nextstarIndex);
        }
        GridLayoutGroup currentGridLayout = currentStar.GetComponent<GridLayoutGroup>();
        if (currentGridLayout != null)
        {
            currentGridLayout.cellSize = new Vector2(20, 20);
        }
        GridLayoutGroup nextGridLayout = nextStar.GetComponent<GridLayoutGroup>();
        if (nextGridLayout != null)
        {
            nextGridLayout.cellSize = new Vector2(20, 20);
        }
    }
    public void GetStarImage(RawImage starImage, int starIndex)
    {
        Texture starTexture = Resources.Load<Texture>($"{ImageConstants.Star.STAR_LV1_URL}");
        switch (starIndex)
        {
            case 0:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.STAR_LV1_URL}");
                starImage.texture = starTexture;
                break;
            case 1:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.STAR_LV2_URL}");
                starImage.texture = starTexture;
                break;
            case 2:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.STAR_LV3_URL}");
                starImage.texture = starTexture;
                break;
            case 3:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.STAR_LV4_URL}");
                starImage.texture = starTexture;
                break;
            case 4:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.STAR_LV5_URL}");
                starImage.texture = starTexture;
                break;
            case 5:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.STAR_LV6_URL}");
                starImage.texture = starTexture;
                break;
            case 6:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.STAR_LV7_URL}");
                starImage.texture = starTexture;
                break;
            case 7:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.STAR_LV8_URL}");
                starImage.texture = starTexture;
                break;
            case 8:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.STAR_LV9_URL}");
                starImage.texture = starTexture;
                break;
            case 9:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.STAR_LV10_URL}");
                starImage.texture = starTexture;
                break;
            default:
                starTexture = Resources.Load<Texture>($"{ImageConstants.Star.STAR_LV1_URL}");
                starImage.texture = starTexture;
                break;
        }
    }
}
