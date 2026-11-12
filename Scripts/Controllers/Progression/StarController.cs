using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarController : MonoBehaviour
{
    private const string UpgradeStarFeatureType = "Upgrade Star";
    private const string StarObjectColumn = "STAR";
    private const string StarGrowthType = "STAR";

    private static readonly Dictionary<Type, (string ObjectType, string FeatureCodeName)> StarUpSettingsByStatType =
        new Dictionary<Type, (string ObjectType, string FeatureCodeName)>
    {
        { typeof(Achievements), (AppConstants.ObjectType.ACHIEVEMENTS, AppConstants.Star.CodeName.UPGRADE_ACHIEVEMENTS_STAR) },
        { typeof(Alchemies), (AppConstants.ObjectType.ALCHEMIES, AppConstants.Star.CodeName.UPGRADE_ALCHEMIES_STAR) },
        { typeof(Architectures), (AppConstants.ObjectType.ARCHITECTURES, AppConstants.Star.CodeName.UPGRADE_ARCHITECTURES_STAR) },
        { typeof(Artifacts), (AppConstants.ObjectType.ARTIFACTS, AppConstants.Star.CodeName.UPGRADE_ARTIFACTS_STAR) },
        { typeof(Artworks), (AppConstants.ObjectType.ARTWORKS, AppConstants.Star.CodeName.UPGRADE_ARTWORKS_STAR) },
        { typeof(Avatars), (AppConstants.ObjectType.AVATARS, AppConstants.Star.CodeName.UPGRADE_AVATARS_STAR) },
        { typeof(Badges), (AppConstants.ObjectType.BADGES, AppConstants.Star.CodeName.UPGRADE_BADGES_STAR) },
        { typeof(Beverages), (AppConstants.ObjectType.BEVERAGES, AppConstants.Star.CodeName.UPGRADE_BEVERAGES_STAR) },
        { typeof(Books), (AppConstants.ObjectType.BOOKS, AppConstants.Star.CodeName.UPGRADE_BOOKS_STAR) },
        { typeof(Borders), (AppConstants.ObjectType.BORDERS, AppConstants.Star.CodeName.UPGRADE_BORDERS_STAR) },
        { typeof(Buildings), (AppConstants.ObjectType.BUILDINGS, AppConstants.Star.CodeName.UPGRADE_BUILDINGS_STAR) },
        { typeof(CardAdmirals), (AppConstants.ObjectType.CARD_ADMIRALS, AppConstants.Star.CodeName.UPGRADE_CARD_ADMIRALS_STAR) },
        { typeof(CardCaptains), (AppConstants.ObjectType.CARD_CAPTAINS, AppConstants.Star.CodeName.UPGRADE_CARD_CAPTAINS_STAR) },
        { typeof(CardColonels), (AppConstants.ObjectType.CARD_COLONELS, AppConstants.Star.CodeName.UPGRADE_CARD_COLONELS_STAR) },
        { typeof(CardGenerals), (AppConstants.ObjectType.CARD_GENERALS, AppConstants.Star.CodeName.UPGRADE_CARD_GENERALS_STAR) },
        { typeof(CardHeroes), (AppConstants.ObjectType.CARD_HEROES, AppConstants.Star.CodeName.UPGRADE_CARD_HEROES_STAR) },
        { typeof(CardLives), (AppConstants.ObjectType.CARD_LIVES, AppConstants.Star.CodeName.UPGRADE_CARD_LIVES_STAR) },
        { typeof(CardMilitaries), (AppConstants.ObjectType.CARD_MILITARIES, AppConstants.Star.CodeName.UPGRADE_CARD_MILITARIES_STAR) },
        { typeof(CardMonsters), (AppConstants.ObjectType.CARD_MONSTERS, AppConstants.Star.CodeName.UPGRADE_CARD_MONSTERS_STAR) },
        { typeof(CardSoldiers), (AppConstants.ObjectType.CARD_SOLDIERS, AppConstants.Star.CodeName.UPGRADE_CARD_SOLDIERS_STAR) },
        { typeof(CardSpells), (AppConstants.ObjectType.CARD_SPELLS, AppConstants.Star.CodeName.UPGRADE_CARD_SPELLS_STAR) },
        { typeof(CollaborationEquipments), (AppConstants.ObjectType.COLLABORATION_EQUIPMENTS, AppConstants.Star.CodeName.UPGRADE_COLLABORATION_EQUIPMENTS_STAR) },
        { typeof(Collaborations), (AppConstants.ObjectType.COLLABORATIONS, AppConstants.Star.CodeName.UPGRADE_COLLABORATIONS_STAR) },
        { typeof(Cores), (AppConstants.ObjectType.CORES, AppConstants.Star.CodeName.UPGRADE_CORES_STAR) },
        { typeof(Emojis), (AppConstants.ObjectType.EMOJIS, AppConstants.Star.CodeName.UPGRADE_EMOJIS_STAR) },
        { typeof(Equipments), (AppConstants.ObjectType.EQUIPMENTS, AppConstants.Star.CodeName.UPGRADE_EQUIPMENTS_STAR) },
        { typeof(Fashions), (AppConstants.ObjectType.FASHIONS, AppConstants.Star.CodeName.UPGRADE_FASHIONS_STAR) },
        { typeof(Foods), (AppConstants.ObjectType.FOODS, AppConstants.Star.CodeName.UPGRADE_FOODS_STAR) },
        { typeof(Forges), (AppConstants.ObjectType.FORGES, AppConstants.Star.CodeName.UPGRADE_FORGES_STAR) },
        { typeof(Furnitures), (AppConstants.ObjectType.FURNITURES, AppConstants.Star.CodeName.UPGRADE_FURNITURES_STAR) },
        { typeof(MagicFormationCircles), (AppConstants.ObjectType.MAGIC_FORMATION_CIRCLES, AppConstants.Star.CodeName.UPGRADE_MAGIC_FORMATION_CIRCLES_STAR) },
        { typeof(MechaBeasts), (AppConstants.ObjectType.MECHA_BEASTS, AppConstants.Star.CodeName.UPGRADE_MECHA_BEASTS_STAR) },
        { typeof(Medals), (AppConstants.ObjectType.MEDALS, AppConstants.Star.CodeName.UPGRADE_MEDALS_STAR) },
        { typeof(Pets), (AppConstants.ObjectType.PETS, AppConstants.Star.CodeName.UPGRADE_PETS_STAR) },
        { typeof(Plants), (AppConstants.ObjectType.PLANTS, AppConstants.Star.CodeName.UPGRADE_PLANTS_STAR) },
        { typeof(Puppets), (AppConstants.ObjectType.PUPPETS, AppConstants.Star.CodeName.UPGRADE_PUPPETS_STAR) },
        { typeof(Relics), (AppConstants.ObjectType.RELICS, AppConstants.Star.CodeName.UPGRADE_RELICS_STAR) },
        { typeof(Robots), (AppConstants.ObjectType.ROBOTS, AppConstants.Star.CodeName.UPGRADE_ROBOTS_STAR) },
        { typeof(Runes), (AppConstants.ObjectType.RUNES, AppConstants.Star.CodeName.UPGRADE_RUNES_STAR) },
        { typeof(Skills), (AppConstants.ObjectType.SKILLS, AppConstants.Star.CodeName.UPGRADE_SKILLS_STAR) },
        { typeof(SpiritBeasts), (AppConstants.ObjectType.SPIRIT_BEASTS, AppConstants.Star.CodeName.UPGRADE_SPIRIT_BEASTS_STAR) },
        { typeof(SpiritCards), (AppConstants.ObjectType.SPIRIT_CARDS, AppConstants.Star.CodeName.UPGRADE_SPIRIT_CARDS_STAR) },
        { typeof(Symbols), (AppConstants.ObjectType.SYMBOLS, AppConstants.Star.CodeName.UPGRADE_SYMBOLS_STAR) },
        { typeof(Talismans), (AppConstants.ObjectType.TALISMANS, AppConstants.Star.CodeName.UPGRADE_TALISMANS_STAR) },
        { typeof(Technologies), (AppConstants.ObjectType.TECHNOLOGIES, AppConstants.Star.CodeName.UPGRADE_TECHNOLOGIES_STAR) },
        { typeof(Titles), (AppConstants.ObjectType.TITLES, AppConstants.Star.CodeName.UPGRADE_TITLES_STAR) },
        { typeof(Vehicles), (AppConstants.ObjectType.VEHICLES, AppConstants.Star.CodeName.UPGRADE_VEHICLES_STAR) },
        { typeof(Weapons), (AppConstants.ObjectType.WEAPONS, AppConstants.Star.CodeName.UPGRADE_WEAPONS_STAR) },
        { typeof(Outfits), (AppConstants.ObjectType.OUTFITS, AppConstants.Star.CodeName.UPGRADE_OUTFITS_STAR) }
    };

    public static StarController Instance { get; private set; }
    private Transform MainPanel;
    public GameObject StarPanelPrefab;
    private GameObject StarItemPopupPrefab;
    private GameObject TradingPanelPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        StarPanelPrefab = UIManager.Instance.Get(PrefabConstants.Progression.STAR_PANEL_PREFAB);
        StarItemPopupPrefab = UIManager.Instance.Get(PrefabConstants.Universe.UNIVERSE_ITEM_POPUP_PREFAB);
        TradingPanelPrefab = UIManager.Instance.Get(PrefabConstants.Progression.TRADING_PANEL_PREFAB);
    }

    // ==========================================
    // 1. POPUP CHÍNH: STAR (Dựa theo quantity/star, tính toán theo công thức cấu hình)
    // ==========================================
    public async Task CreateStarPanelAsync<T>(T stat) where T : IStats
    {
        GameObject gameObject = Instantiate(StarPanelPrefab, MainPanel);
        Transform panelTransform = gameObject.transform;

        TextMeshProUGUI currentLevelText = panelTransform.Find("CurrentLevel").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nextLevelText = panelTransform.Find("NextLevel").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI maxLevelText = panelTransform.Find("MaxLevel").GetComponent<TextMeshProUGUI>();
        Slider quantitySlider = panelTransform.Find("QuantitySlider").GetComponent<Slider>();
        TextMeshProUGUI experienceText = panelTransform.Find("ExperienceText")?.GetComponent<TextMeshProUGUI>();

        TMP_InputField levelInputField = panelTransform.Find("ItemUsedQuantityText")?.GetComponent<TMP_InputField>();
        TextMeshProUGUI itemUsedQuantityText = panelTransform.Find("ItemUsedQuantityText")?.GetComponent<TextMeshProUGUI>();

        TextMeshProUGUI notificationText = panelTransform.Find("Notification/ContentText").GetComponent<TextMeshProUGUI>();
        RawImage notificationIconImage = panelTransform.Find("Notification/IconImage")?.GetComponent<RawImage>();

        Button increaseOneButton = panelTransform.Find("IncreaseOneButton").GetComponent<Button>();
        Button increaseTenButton = panelTransform.Find("IncreaseTenButton").GetComponent<Button>();
        Button increaseMaxButton = panelTransform.Find("IncreaseMaxButton").GetComponent<Button>();
        Button decreaseOneButton = panelTransform.Find("DecreaseOneButton").GetComponent<Button>();
        Button decreaseTenButton = panelTransform.Find("DecreaseTenButton").GetComponent<Button>();
        Button decreaseMaxButton = panelTransform.Find("DecreaseMaxButton").GetComponent<Button>();
        Button confirmButton = panelTransform.Find("ConfirmButton").GetComponent<Button>();
        Button closeButton = panelTransform.Find("CloseButton").GetComponent<Button>();
        
        Button openTradingButton = panelTransform.Find("OpenTradingButton")?.GetComponent<Button>();

        Button previewButton = panelTransform.Find("PreviewButton").GetComponent<Button>();
        Transform previewDefaultObj = previewButton.transform.Find("Default");
        Transform previewSelectedObj = previewButton.transform.Find("Selected");

        Transform currentStatsContent = panelTransform.Find("Scroll View/Viewport/Content/CurrentStats");
        Transform nextStatsContent = panelTransform.Find("Scroll View/Viewport/Content/NextStats");
        Transform rightTransform = panelTransform.Find("Right Scroll View/Viewport/Content");

        int popupCurrentStar = stat.Star; // Hoặc stat.Quantity tùy thuộc vào thuộc tính của stat

        string objectType = null;
        string featureCodeName = null;
        Type statType = stat.GetType();
        if (StarUpSettingsByStatType.TryGetValue(statType, out var setting))
        {
            objectType = setting.ObjectType;
            featureCodeName = setting.FeatureCodeName;
        }

        GameConfig gameConfig = await GameConfigService.Create().GetGameConfigAsync(objectType, StarObjectColumn);
        List<GameGrowthConfig> growthConfig = await GameGrowthConfigService.Create().GetConfigAsync(objectType, StarGrowthType, popupCurrentStar);

        RecipeResponseDTO popupRecipeData = await RecipeService.Create().GetRecipeItemsByFeatureCodeNameAsync(featureCodeName, popupCurrentStar + 1, User.CurrentUserId);

        double recipeMaxStar = popupRecipeData?.Recipes != null && popupRecipeData.Recipes.Count > 0
            ? popupRecipeData.Recipes.Max(r => r.MaxLevel)
            : popupCurrentStar;

        double maxStarConfig = double.Parse(gameConfig.MaxValue);
        double maxSliderValue = Math.Max(0.0, recipeMaxStar - popupCurrentStar);

        LevelDTO starDto = new LevelDTO
        {
            Level = popupCurrentStar,
            Configs = growthConfig,
        };

        currentLevelText.text = popupCurrentStar.ToString();
        nextLevelText.text = popupCurrentStar.ToString();
        if (maxLevelText != null) maxLevelText.text = maxStarConfig.ToString();

        quantitySlider.minValue = 0;
        quantitySlider.maxValue = (float)maxSliderValue;
        quantitySlider.wholeNumbers = true;
        quantitySlider.value = 0;

        bool isPreviewSelected = false;

        void UpdatePreviewButtonState(bool isSelected)
        {
            if (previewDefaultObj != null) previewDefaultObj.gameObject.SetActive(!isSelected);
            if (previewSelectedObj != null) previewSelectedObj.gameObject.SetActive(isSelected);
        }

        void SetPreviewNotification(string value, bool isSuccess)
        {
            notificationText.text = LocalizationManager.Get(value);
            string colorHex = isSuccess ? ColorConstants.GREEN_COLOR : ColorConstants.RED_COLOR;
            Color statusColor = ColorHelper.HexToColor(colorHex);
            notificationText.color = statusColor;
        }

        async void UpdatePreview()
        {
            int requested = (int)quantitySlider.value;

            if (popupCurrentStar >= maxStarConfig)
            {
                SetPreviewNotification(MessageConstants.UPGRADE_ALREADY_MAX, false);
                nextLevelText.text = "MAX";
                confirmButton.interactable = false;
                if (experienceText != null) experienceText.text = "MAX";
                return;
            }

            if (requested <= 0)
            {
                nextLevelText.text = popupCurrentStar.ToString();
                confirmButton.interactable = false;
                if (experienceText != null) experienceText.text = "0";
                foreach (Transform child in rightTransform) Destroy(child.gameObject);
                SetPreviewNotification(MessageConstants.READY_TO_UPGRADE, true);
                return;
            }

            // Gọi hàm tính toán thông thường không cần truyền tham số kinh nghiệm
            RecipeCalculateDTO preview;
            if (isPreviewSelected)
            {
                preview = await RecipeCalculator.CalculateUpgradePreviewByLevelsAsync(
                    popupRecipeData, popupCurrentStar, requested, (int)maxStarConfig, starDto, 0
                );
            }
            else
            {
                preview = await RecipeCalculator.CalculateUpgradeByLevelsValueBasedAsync(
                    popupRecipeData, popupCurrentStar, requested, (int)maxStarConfig, starDto, 0
                );
            }

            int targetStarDisplay = Math.Min(popupCurrentStar + requested, (int)maxStarConfig);
            nextLevelText.text = isPreviewSelected ? targetStarDisplay.ToString() : preview.TargetLevel.ToString();

            // Nếu không dùng exp, có thể ẩn hoặc hiển thị số star tăng thêm tuỳ ý
            if (experienceText != null)
            {
                experienceText.text = $"+{preview.GainedLevels} Star";
            }

            confirmButton.interactable = !isPreviewSelected && (preview.GainedLevels > 0);

            var materials = preview.TotalDeductions ?? new List<RecipeItemDetailDTO>();
            int existingChildCount = rightTransform.childCount;
            for (int i = existingChildCount - 1; i >= materials.Count; i--)
            {
                Destroy(rightTransform.GetChild(i).gameObject);
            }

            for (int i = 0; i < materials.Count; i++)
            {
                GameObject itemGO = (i < existingChildCount) ? rightTransform.GetChild(i).gameObject : Instantiate(StarItemPopupPrefab, rightTransform);
                double ownedQty = materials[i].UserQuantity;
                SetupItemUI(itemGO, materials[i].ObjectId, materials[i].ObjectType, materials[i].ObjectImage, materials[i].RequiredQuantity, ownedQty);
            }

            if (levelInputField != null) levelInputField.text = requested.ToString();
            else if (itemUsedQuantityText != null) itemUsedQuantityText.text = $"+{requested}";
        }

        previewButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            isPreviewSelected = !isPreviewSelected;
            UpdatePreviewButtonState(isPreviewSelected);

            quantitySlider.maxValue = isPreviewSelected ? (float)Math.Max(0.0, maxStarConfig - popupCurrentStar) : (float)Math.Max(0.0, recipeMaxStar - popupCurrentStar);
            quantitySlider.SetValueWithoutNotify(0);
            UpdatePreview();
        });

        quantitySlider.onValueChanged.AddListener(_ => UpdatePreview());

        increaseOneButton.onClick.AddListener(() => quantitySlider.value = Mathf.Min(quantitySlider.maxValue, quantitySlider.value + 1));
        increaseTenButton.onClick.AddListener(() => quantitySlider.value = Mathf.Min(quantitySlider.maxValue, quantitySlider.value + 10));
        increaseMaxButton.onClick.AddListener(() => quantitySlider.value = quantitySlider.maxValue);

        decreaseOneButton.onClick.AddListener(() => quantitySlider.value = Mathf.Max(0, quantitySlider.value - 1));
        decreaseTenButton.onClick.AddListener(() => quantitySlider.value = Mathf.Max(0, quantitySlider.value - 10));
        decreaseMaxButton.onClick.AddListener(() => quantitySlider.value = 0);

        if (openTradingButton != null)
        {
            openTradingButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(gameObject);
                await CreateTradingPopupPanelAsync(stat);
            });
        }

        confirmButton.onClick.AddListener(async () =>
        {
            if (isPreviewSelected) return;

            int requested = (int)quantitySlider.value;
            if (requested <= 0) return;

            AudioManager.Instance.PlaySFX(AudioConstants.SFX.LEVEL_UP_SOUND);
            confirmButton.interactable = false;
            closeButton.interactable = false;

            RecipeCalculateDTO calcResult = await RecipeCalculator.CalculateUpgradeByLevelsValueBasedAsync(
                popupRecipeData, popupCurrentStar, requested, (int)maxStarConfig, starDto, 0
            );

            if (calcResult.GainedLevels <= 0)
            {
                confirmButton.interactable = true;
                closeButton.interactable = true;
                return;
            }

            stat.Star = calcResult.TargetLevel; // Cập nhật lại số Star cho stat

            // Gọi Service thực thi trừ nguyên liệu tương ứng
            // await ExecuteServiceInsertAsync(stat, calcResult.TotalDeductions);

            Destroy(gameObject);
        });

        closeButton.onClick.AddListener(() => Destroy(gameObject));
        UpdatePreview();
    }


    // ==========================================
    // 2. POPUP PHỤ: TRADING (Dựa theo objectValue của items đổi sang quantity, KHÔNG dùng CalculateRequiredExp)
    // ==========================================
    public async Task CreateTradingPopupPanelAsync<T>(T stat) where T : IStats
    {
        GameObject gameObject = Instantiate(TradingPanelPrefab, MainPanel);
        Transform panelTransform = gameObject.transform;

        Slider quantitySlider = panelTransform.Find("QuantitySlider").GetComponent<Slider>();
        Button confirmButton = panelTransform.Find("ConfirmButton").GetComponent<Button>();
        Button closeButton = panelTransform.Find("CloseButton").GetComponent<Button>();
        Transform rightTransform = panelTransform.Find("Right Scroll View/Viewport/Content");

        string featureCodeName = "TRADING_FEATURE_CODE"; 
        RecipeResponseDTO tradingRecipeData = await RecipeService.Create().GetRecipeItemsByFeatureCodeNameAsync(featureCodeName, 1, User.CurrentUserId);

        quantitySlider.minValue = 0;
        quantitySlider.maxValue = 100;
        quantitySlider.wholeNumbers = true;
        quantitySlider.value = 0;

        void UpdateTradingPreview()
        {
            int requestedQuantity = (int)quantitySlider.value;

            if (requestedQuantity <= 0)
            {
                confirmButton.interactable = false;
                foreach (Transform child in rightTransform) Destroy(child.gameObject);
                return;
            }

            // Quy đổi trực tiếp dựa theo objectValue của items để ra quantity tiêu thụ
            var deductions = new List<RecipeItemDetailDTO>();
            if (tradingRecipeData?.Recipes != null)
            {
                foreach (var recipe in tradingRecipeData.Recipes)
                {
                    foreach (var detail in recipe.RecipeItemDetails)
                    {
                        double unitValue = detail.ObjectValue > 0 ? detail.ObjectValue : 1;
                        double totalRequired = requestedQuantity * unitValue; 

                        double ownedQty = 0;
                        if (tradingRecipeData.UserInventory != null && tradingRecipeData.UserInventory.TryGetValue(detail.ObjectId, out var q))
                            ownedQty = q;

                        deductions.Add(new RecipeItemDetailDTO
                        {
                            ObjectId = detail.ObjectId,
                            ObjectType = detail.ObjectType,
                            ObjectImage = detail.ObjectImage,
                            ObjectValue = detail.ObjectValue,
                            RequiredQuantity = totalRequired,
                            UserQuantity = ownedQty
                        });
                    }
                }
            }

            int existingChildCount = rightTransform.childCount;
            for (int i = existingChildCount - 1; i >= deductions.Count; i--)
            {
                Destroy(rightTransform.GetChild(i).gameObject);
            }

            bool canAffordAll = true;
            for (int i = 0; i < deductions.Count; i++)
            {
                GameObject itemGO = (i < existingChildCount) ? rightTransform.GetChild(i).gameObject : Instantiate(StarItemPopupPrefab, rightTransform);
                SetupItemUI(itemGO, deductions[i].ObjectId, deductions[i].ObjectType, deductions[i].ObjectImage, deductions[i].RequiredQuantity, deductions[i].UserQuantity);

                if (deductions[i].UserQuantity < deductions[i].RequiredQuantity)
                {
                    canAffordAll = false;
                }
            }

            confirmButton.interactable = canAffordAll;
        }

        quantitySlider.onValueChanged.AddListener(_ => UpdateTradingPreview());

        confirmButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.LEVEL_UP_SOUND);
            // Thực hiện gọi API Trading tương ứng tại đây
            Destroy(gameObject);
        });

        closeButton.onClick.AddListener(() => Destroy(gameObject));
        UpdateTradingPreview();
    }

    private void SetupItemUI(GameObject itemGO, string objectId, string objectType, string objectImage, double requiredQty, double ownedQty)
    {
        TextMeshProUGUI requiredText = itemGO.transform.Find("RequiredText")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI ownedText = itemGO.transform.Find("AvailableText")?.GetComponent<TextMeshProUGUI>();
        RawImage image = itemGO.transform.Find("Image")?.GetComponent<RawImage>();

        bool isEnough = ownedQty >= requiredQty;
        Color statusColor = ColorHelper.HexToColor(isEnough ? ColorConstants.GREEN_COLOR : ColorConstants.RED_COLOR);

        if (requiredText != null) { requiredText.text = requiredQty.ToString(); requiredText.color = statusColor; }
        if (ownedText != null) { ownedText.text = ownedQty.ToString(); ownedText.color = statusColor; }

        if (image != null && !string.IsNullOrEmpty(objectImage))
        {
            Texture texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(objectImage));
            if (texture != null) image.texture = texture;
        }
    }
}