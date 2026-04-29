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
    public GameObject ItemThird;
    public GameObject StarPrefab;

    [Header("Font")]
    public TMP_FontAsset EuroStyleNormalFont;
    private Dictionary<string, GameObject> prefabDict;

    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
            LoadAllPrefabs();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    private void LoadAllPrefabs()
    {
        prefabDict = new Dictionary<string, GameObject>();

        // Load toàn bộ prefab trong thư mục Resources/UI/MainMenu/
        List<GameObject> prefabs = new List<GameObject>();

        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet1/Panel"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet1/Slot"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet2/Panel"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet2/Slot"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet3/Panel"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet3/Slot"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet4/Panel"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet4/Slot"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet5/Panel"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet5/Slot"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet6/Panel"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet6/Slot"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet7/Panel"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/MainMenuSet7/Slot"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/Market"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/Master"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/Component"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/General"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/ScienceFiction"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/Shop"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/Skill"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/Anime"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/Arena"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/Equipment"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/Research"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/Archive"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/Universe"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/HIIN"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/SSWN"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/HITN"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/HIHN"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/HIEN"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/HICA"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/HIRN"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/HIDC"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/HICB"));
        prefabs.AddRange(Resources.LoadAll<GameObject>("Main Feature/Prefabs/HISN"));

        foreach (var prefab in prefabs)
        {
            // Key = tên prefab
            prefabDict[prefab.name] = prefab;
            // Debug.Log($"Loaded: {prefab.name}");
        }
    }
    public GameObject Get(string prefabName)
    {
        if (prefabDict.TryGetValue(prefabName, out var prefab))
        {
            // Debug.Log(prefabName);
            return prefab;
        }

        Debug.LogWarning($"Prefab '{prefabName}' not found in dictionary!");
        return null;
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
        Transform transform = gameObject.transform;
        Transform backgroundImageTransform = transform.Find("Background");
        if (backgroundImageTransform != null)
        {
            RawImage backgroundImage = transform.Find("Background").GetComponent<RawImage>();
            Texture backgroundTexture = TextureHelper.LoadTextureCached($"UI/Background3/{mainType}");
            backgroundImage.texture = backgroundTexture;
            backgroundImage.rectTransform.sizeDelta = new Vector2(350, 350);
        }

        Transform backgroundTransform = transform.Find("BackgroundCircle");
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
            Transform aptitudeSkill = transform.Find($"UpgradeSkill{i}");
            if (aptitudeSkill == null) continue;

            RawImage aptitudeImage = aptitudeSkill.Find("AptitudeImage").GetComponent<RawImage>();
            TextMeshProUGUI levelText = aptitudeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

            Texture texture = TextureHelper.LoadTextureCached($"UI/Rank/{type}");
            aptitudeImage.texture = texture;

            if (aptitudeImage != null) aptitudeImage.color = Color.black;
            if (levelText != null) levelText.text = $"0/{levelsPerSkill}";
        }

        // Xác định số kỹ năng được kích hoạt
        int activeSkillsCount = Mathf.Clamp((level / levelsPerSkill), 1, totalSkills);
        for (int i = 1; i <= activeSkillsCount; i++)
        {
            Transform activeSkill = transform.Find($"UpgradeSkill{i}");
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
        TextMeshProUGUI totalLevelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        totalLevelText.text = level.ToString();
    }
    public void SetMaterialUI(GameObject gameobject, string itemImage, double itemQuantity, double currencyQuantity, int rankLevel, int maxLevel)
    {
        Transform transform = gameObject.transform;
        Transform currencyPanel = transform.Find("DictionaryCards/Currency");
        // List<Currencies> currencies = await UserCurrenciesService.Create().GetUserCurrencyAsync(User.CurrentUserId);
        ButtonEvent.Instance.Close(currencyPanel);
        GameObject itemObject = Get("ItemPrefab");
        GameObject tempObject = Instantiate(itemObject, currencyPanel);
        RawImage image = tempObject.transform.Find("Image").GetComponent<RawImage>();
        image.texture =TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(itemImage));
        TextMeshProUGUI quantityText = tempObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        quantityText.text = NumberFormatterHelper.FormatNumber(itemQuantity, true);
        // CurrenciesManager.Instance.GetMainCurrency(currencies, currencyPanel);

        var oneResult = ItemHelper.CalculateLevelUp(itemQuantity, currencyQuantity, 1, 10, rankLevel, false, maxLevel);
        var maxResult = ItemHelper.CalculateLevelUp(itemQuantity, currencyQuantity, 1, 10, rankLevel, true, maxLevel);
        RawImage oneLevelCurrencyImage = transform.Find("DictionaryCards/OneLevelCurrency/CurrencyImage").GetComponent<RawImage>();
        RawImage maxLevelCurrencyImage = transform.Find("DictionaryCards/MaxLevelCurrency/CurrencyImage").GetComponent<RawImage>();
        Texture oneLevelCurrencyTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Currency.SILVER}");
        Texture maxLevelCurrencyTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Currency.SILVER}");
        oneLevelCurrencyImage.texture = oneLevelCurrencyTexture;
        maxLevelCurrencyImage.texture = maxLevelCurrencyTexture;

        TextMeshProUGUI oneLevelCurrencyText = transform.Find("DictionaryCards/OneLevelCurrency/QuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI maxLevelCurrencyText = transform.Find("DictionaryCards/MaxLevelCurrency/QuantityText").GetComponent<TextMeshProUGUI>();
        oneLevelCurrencyText.text = oneResult.totalCurrencyUsed.ToString();
        maxLevelCurrencyText.text = maxResult.totalCurrencyUsed.ToString();

        RawImage oneLevelImage = transform.Find("DictionaryCards/OneLevelMaterial/MaterialImage").GetComponent<RawImage>();
        Texture oneLevelTexture = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(itemImage)}");
        oneLevelImage.texture = oneLevelTexture;

        TextMeshProUGUI oneLevelQuantity = transform.Find("DictionaryCards/OneLevelMaterial/QuantityText").GetComponent<TextMeshProUGUI>();
        oneLevelQuantity.text = oneResult.totalMaterialUsed.ToString();

        // RectTransform oneLevelRectTransform = oneLevelImage.GetComponent<RectTransform>();
        // oneLevelRectTransform.sizeDelta = new Vector2(40, 40);

        RawImage maxLevelImage = transform.Find("DictionaryCards/MaxLevelMaterial/MaterialImage").GetComponent<RawImage>();
        Texture maxLevelTexture = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(itemImage)}");
        maxLevelImage.texture = maxLevelTexture;

        TextMeshProUGUI maxLevelQuantity = transform.Find("DictionaryCards/MaxLevelMaterial/QuantityText").GetComponent<TextMeshProUGUI>();
        maxLevelQuantity.text = maxResult.totalMaterialUsed.ToString();

        // RectTransform maxLevelRectTransform = maxLevelImage.GetComponent<RectTransform>();
        // maxLevelRectTransform.sizeDelta = new Vector2(40, 40);
    }
    public void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = TextureHelper.LoadTextureCached($"{image}");
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
        Transform transform = currentObject.transform;
        TextMeshProUGUI currentLevelText = transform.Find("DictionaryCards/Content/LevelPanel/Level/CurrentLevelText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nextLevelText = transform.Find("DictionaryCards/Content/LevelPanel/Level/NextLevelText").GetComponent<TextMeshProUGUI>();
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
        Transform transform = currentObject.transform;
        Transform detailsContent = transform.Find("DictionaryCards/Content/DetailsPanel/Scroll View/Viewport/Content");

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
    public void CreateSinglePropertyUI(PropertyInfo property, object value, Transform generalInformationPanel, Transform statsInformationPanel, Transform descriptionInformationPanel)
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
                descriptionText.color = ColorHelper.HexToColor(ColorConstants.DESCRIPTION_TEXT_COLOR);

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
                        GameObject elementObject = Instantiate(Get("ElementDetailsPrefab"), generalInformationPanel);

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
                        GameObject elementObject = Instantiate(Get("ElementDetailsPrefab"), statsInformationPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = NumberFormatterHelper.FormatNumber(intValue, false);

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
    public void CreatePropertyRuneUI(string property, RawImage runeImage)
    {
        Texture runeTexture;
        if (property.Equals(AppConstants.StatFields.PHYSICAL_ATTACK))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.PHYSICAL_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.PHYSICAL_DEFENSE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.PHYSICAL_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.MAGICAL_ATTACK))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.MAGICAL_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.MAGICAL_DEFENSE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.MAGICAL_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.CHEMICAL_ATTACK))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.CHEMICAL_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.CHEMICAL_DEFENSE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.CHEMICAL_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.ATOMIC_ATTACK))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.ATOMIC_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.ATOMIC_DEFENSE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.ATOMIC_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.MENTAL_ATTACK))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.MENTAL_ATTACK_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.MENTAL_DEFENSE))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.MENTAL_DEFENSE_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.HEALTH))
        {
            runeTexture = TextureHelper.LoadTextureCached(ImageConstants.Rune.HEALTH_RUNE_URL);
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.SPEED))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.SPEED_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.CRITICAL_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.DAMAGE_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.CRITICAL_DAMAGE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.CRITICAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.DAMAGE_ABSORPTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.ABSORPTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.CRITICAL_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.CRITICAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.IGNORE_CRITICAL_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.CRITICAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.PENETRATION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.PENETRATION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.PENETRATION_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.PENETRATION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.EVASION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.EVASION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.IGNORE_DAMAGE_ABSORPTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.ABSORPTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.ABSORBED_DAMAGE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.ABSORPTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.VITALITY_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.VITALITY_REGENERATION_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.VITALITY_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.ACCURACY_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.ACCURACY_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.LIFE_STEAL_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.LIFESTEAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.SHIELD_STRENGTH))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.SHIELD_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.TENACITY))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.TENACITY_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.RESISTANCE_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.COMBO_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.COMBO_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.IGNORE_COMBO_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.COMBO_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.COMBO_DAMAGE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.COMBO_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.COMBO_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.COMBO_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.STUN_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.STUN_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.IGNORE_STUN_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.STUN_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.MANA))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.MANA_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.MANA_REGENERATION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.MANA_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.REFLECTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.REFLECTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.IGNORE_REFLECTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.REFLECTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.REFLECTION_DAMAGE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.REFLECTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.REFLECTION_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.REFLECTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.DAMAGE_TO_DIFFERENT_FACTION_RATE) || property.Equals(AppConstants.StatFields.RESISTANCE_TO_DIFFERENT_FACTION_RATE) ||
         property.Equals(AppConstants.StatFields.DAMAGE_TO_SAME_FACTION_RATE) || property.Equals(AppConstants.StatFields.RESISTANCE_TO_SAME_FACTION_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.FACTION_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.NORMAL_DAMAGE_RATE) || property.Equals(AppConstants.StatFields.NORMAL_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.NORMAL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        else if (property.Equals(AppConstants.StatFields.SKILL_DAMAGE_RATE) || property.Equals(AppConstants.StatFields.SKILL_RESISTANCE_RATE))
        {
            runeTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Rune.SKILL_RUNE_URL}");
            runeImage.texture = runeTexture;
        }
        runeImage.gameObject.SetActive(true);
    }
    public void CreatePropertyLevelUI(PropertyInfo[] properties, object targetObject, double increasePerLevel, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        Transform levelElementContent = transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        foreach (var property in properties)
        {
            // Lấy giá trị của thuộc tính
            object value = property.GetValue(targetObject, null);
            CreateSinglePropertyLevelUI(property, value, increasePerLevel, levelElementContent, currentObject);
        }
    }
    public void CreateSinglePropertyLevelUI(PropertyInfo property, object value, double increasePerLevel, Transform levelElementContent, GameObject currentObject)
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
                        GameObject elementObject = Instantiate(Get("ElementDetailsPrefab"), levelElementContent);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                        {
                            double newintValue = increasePerLevel * intValue;
                            elementContentText.text = "+" + NumberFormatterHelper.FormatNumber(newintValue, false);
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
        Transform upgradeElementContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
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
                        GameObject elementObject = Instantiate(Get("ElementDetailsPrefab"), upgradeElementContent);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                        {
                            double newintValue = increasePerUpgrade * intValue;
                            elementContentText.text = "+" + NumberFormatterHelper.FormatNumber(newintValue, false);
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
        Transform transform = currentObject.transform;
        Transform levelMaterialContent = transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        foreach (Items item in items)
        {
            GameObject itemObject = Instantiate(ItemThird, levelMaterialContent);

            RawImage itemImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
            Texture itemTexture = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(item.Image)}");
            itemImage.texture = itemTexture;

            TextMeshProUGUI itemQuantityText = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
            itemQuantityText.text = NumberFormatterHelper.FormatNumber(item.Quantity, false);
        }
    }
    public void CreateStarUI(int star, GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        Transform currentStar = transform.Find("DictionaryCards/Content/UpgradePanel/Level/CurrentStar");
        Transform nextStar = transform.Find("DictionaryCards/Content/UpgradePanel/Level/NextStar");
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
        Texture starTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Star.STAR_LV1_URL}");
        switch (starIndex)
        {
            case 0:
                starTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Star.STAR_LV1_URL}");
                starImage.texture = starTexture;
                break;
            case 1:
                starTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Star.STAR_LV2_URL}");
                starImage.texture = starTexture;
                break;
            case 2:
                starTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Star.STAR_LV3_URL}");
                starImage.texture = starTexture;
                break;
            case 3:
                starTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Star.STAR_LV4_URL}");
                starImage.texture = starTexture;
                break;
            case 4:
                starTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Star.STAR_LV5_URL}");
                starImage.texture = starTexture;
                break;
            case 5:
                starTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Star.STAR_LV6_URL}");
                starImage.texture = starTexture;
                break;
            case 6:
                starTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Star.STAR_LV7_URL}");
                starImage.texture = starTexture;
                break;
            case 7:
                starTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Star.STAR_LV8_URL}");
                starImage.texture = starTexture;
                break;
            case 8:
                starTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Star.STAR_LV9_URL}");
                starImage.texture = starTexture;
                break;
            case 9:
                starTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Star.STAR_LV10_URL}");
                starImage.texture = starTexture;
                break;
            default:
                starTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Star.STAR_LV1_URL}");
                starImage.texture = starTexture;
                break;
        }
    }
}
