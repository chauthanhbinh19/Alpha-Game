using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private const string UpgradeLevelFeatureType = "Upgrade Level";
    private const string LevelObjectColumn = "LEVEL";
    private const string LevelGrowthType = "LEVEL";

    private static readonly Dictionary<Type, (string ObjectType, string FeatureCodeName)> LevelUpSettingsByStatType =
        new Dictionary<Type, (string ObjectType, string FeatureCodeName)>
    {
        { typeof(Achievements), (AppConstants.ObjectType.ACHIEVEMENTS, AppConstants.Level.CodeName.UPGRADE_ACHIEVEMENTS_LEVEL) },
        { typeof(Alchemies), (AppConstants.ObjectType.ALCHEMIES, AppConstants.Level.CodeName.UPGRADE_ALCHEMIES_LEVEL) },
        { typeof(Architectures), (AppConstants.ObjectType.ARCHITECTURES, AppConstants.Level.CodeName.UPGRADE_ARCHITECTURES_LEVEL) },
        { typeof(Artifacts), (AppConstants.ObjectType.ARTIFACTS, AppConstants.Level.CodeName.UPGRADE_ARTIFACTS_LEVEL) },
        { typeof(Artworks), (AppConstants.ObjectType.ARTWORKS, AppConstants.Level.CodeName.UPGRADE_ARTWORKS_LEVEL) },
        { typeof(Avatars), (AppConstants.ObjectType.AVATARS, AppConstants.Level.CodeName.UPGRADE_AVATARS_LEVEL) },
        { typeof(Badges), (AppConstants.ObjectType.BADGES, AppConstants.Level.CodeName.UPGRADE_BADGES_LEVEL) },
        { typeof(Beverages), (AppConstants.ObjectType.BEVERAGES, AppConstants.Level.CodeName.UPGRADE_BEVERAGES_LEVEL) },
        { typeof(Books), (AppConstants.ObjectType.BOOKS, AppConstants.Level.CodeName.UPGRADE_BOOKS_LEVEL) },
        { typeof(Borders), (AppConstants.ObjectType.BORDERS, AppConstants.Level.CodeName.UPGRADE_BORDERS_LEVEL) },
        { typeof(Buildings), (AppConstants.ObjectType.BUILDINGS, AppConstants.Level.CodeName.UPGRADE_BUILDINGS_LEVEL) },
        { typeof(CardAdmirals), (AppConstants.ObjectType.CARD_ADMIRALS, AppConstants.Level.CodeName.UPGRADE_CARD_ADMIRALS_LEVEL) },
        { typeof(CardCaptains), (AppConstants.ObjectType.CARD_CAPTAINS, AppConstants.Level.CodeName.UPGRADE_CARD_CAPTAINS_LEVEL) },
        { typeof(CardColonels), (AppConstants.ObjectType.CARD_COLONELS, AppConstants.Level.CodeName.UPGRADE_CARD_COLONELS_LEVEL) },
        { typeof(CardGenerals), (AppConstants.ObjectType.CARD_GENERALS, AppConstants.Level.CodeName.UPGRADE_CARD_GENERALS_LEVEL) },
        { typeof(CardHeroes), (AppConstants.ObjectType.CARD_HEROES, AppConstants.Level.CodeName.UPGRADE_CARD_HEROES_LEVEL) },
        { typeof(CardLives), (AppConstants.ObjectType.CARD_LIVES, AppConstants.Level.CodeName.UPGRADE_CARD_LIVES_LEVEL) },
        { typeof(CardMilitaries), (AppConstants.ObjectType.CARD_MILITARIES, AppConstants.Level.CodeName.UPGRADE_CARD_MILITARIES_LEVEL) },
        { typeof(CardMonsters), (AppConstants.ObjectType.CARD_MONSTERS, AppConstants.Level.CodeName.UPGRADE_CARD_MONSTERS_LEVEL) },
        { typeof(CardSoldiers), (AppConstants.ObjectType.CARD_SOLDIERS, AppConstants.Level.CodeName.UPGRADE_CARD_SOLDIERS_LEVEL) },
        { typeof(CardSpells), (AppConstants.ObjectType.CARD_SPELLS, AppConstants.Level.CodeName.UPGRADE_CARD_SPELLS_LEVEL) },
        { typeof(CollaborationEquipments), (AppConstants.ObjectType.COLLABORATION_EQUIPMENTS, AppConstants.Level.CodeName.UPGRADE_COLLABORATION_EQUIPMENTS_LEVEL) },
        { typeof(Collaborations), (AppConstants.ObjectType.COLLABORATIONS, AppConstants.Level.CodeName.UPGRADE_COLLABORATIONS_LEVEL) },
        { typeof(Cores), (AppConstants.ObjectType.CORES, AppConstants.Level.CodeName.UPGRADE_CORES_LEVEL) },
        { typeof(Emojis), (AppConstants.ObjectType.EMOJIS, AppConstants.Level.CodeName.UPGRADE_EMOJIS_LEVEL) },
        { typeof(Equipments), (AppConstants.ObjectType.EQUIPMENTS, AppConstants.Level.CodeName.UPGRADE_EQUIPMENTS_LEVEL) },
        { typeof(Fashions), (AppConstants.ObjectType.FASHIONS, AppConstants.Level.CodeName.UPGRADE_FASHIONS_LEVEL) },
        { typeof(Foods), (AppConstants.ObjectType.FOODS, AppConstants.Level.CodeName.UPGRADE_FOODS_LEVEL) },
        { typeof(Forges), (AppConstants.ObjectType.FORGES, AppConstants.Level.CodeName.UPGRADE_FORGES_LEVEL) },
        { typeof(Furnitures), (AppConstants.ObjectType.FURNITURES, AppConstants.Level.CodeName.UPGRADE_FURNITURES_LEVEL) },
        { typeof(MagicFormationCircles), (AppConstants.ObjectType.MAGIC_FORMATION_CIRCLES, AppConstants.Level.CodeName.UPGRADE_MAGIC_FORMATION_CIRCLES_LEVEL) },
        { typeof(MechaBeasts), (AppConstants.ObjectType.MECHA_BEASTS, AppConstants.Level.CodeName.UPGRADE_MECHA_BEASTS_LEVEL) },
        { typeof(Medals), (AppConstants.ObjectType.MEDALS, AppConstants.Level.CodeName.UPGRADE_MEDALS_LEVEL) },
        { typeof(Pets), (AppConstants.ObjectType.PETS, AppConstants.Level.CodeName.UPGRADE_PETS_LEVEL) },
        { typeof(Plants), (AppConstants.ObjectType.PLANTS, AppConstants.Level.CodeName.UPGRADE_PLANTS_LEVEL) },
        { typeof(Puppets), (AppConstants.ObjectType.PUPPETS, AppConstants.Level.CodeName.UPGRADE_PUPPETS_LEVEL) },
        { typeof(Relics), (AppConstants.ObjectType.RELICS, AppConstants.Level.CodeName.UPGRADE_RELICS_LEVEL) },
        { typeof(Robots), (AppConstants.ObjectType.ROBOTS, AppConstants.Level.CodeName.UPGRADE_ROBOTS_LEVEL) },
        { typeof(Runes), (AppConstants.ObjectType.RUNES, AppConstants.Level.CodeName.UPGRADE_RUNES_LEVEL) },
        { typeof(Skills), (AppConstants.ObjectType.SKILLS, AppConstants.Level.CodeName.UPGRADE_SKILLS_LEVEL) },
        { typeof(SpiritBeasts), (AppConstants.ObjectType.SPIRIT_BEASTS, AppConstants.Level.CodeName.UPGRADE_SPIRIT_BEASTS_LEVEL) },
        { typeof(SpiritCards), (AppConstants.ObjectType.SPIRIT_CARDS, AppConstants.Level.CodeName.UPGRADE_SPIRIT_CARDS_LEVEL) },
        { typeof(Symbols), (AppConstants.ObjectType.SYMBOLS, AppConstants.Level.CodeName.UPGRADE_SYMBOLS_LEVEL) },
        { typeof(Talismans), (AppConstants.ObjectType.TALISMANS, AppConstants.Level.CodeName.UPGRADE_TALISMANS_LEVEL) },
        { typeof(Technologies), (AppConstants.ObjectType.TECHNOLOGIES, AppConstants.Level.CodeName.UPGRADE_TECHNOLOGIES_LEVEL) },
        { typeof(Titles), (AppConstants.ObjectType.TITLES, AppConstants.Level.CodeName.UPGRADE_TITLES_LEVEL) },
        { typeof(Vehicles), (AppConstants.ObjectType.VEHICLES, AppConstants.Level.CodeName.UPGRADE_VEHICLES_LEVEL) },
        { typeof(Weapons), (AppConstants.ObjectType.WEAPONS, AppConstants.Level.CodeName.UPGRADE_WEAPONS_LEVEL) },
        { typeof(Outfits), (AppConstants.ObjectType.OUTFITS, AppConstants.Level.CodeName.UPGRADE_OUTFITS_LEVEL) }
    };

    public static LevelController Instance { get; private set; }
    private Transform MainPanel;
    public GameObject LevelPanelPrefab;
    private GameObject CurrentPanel;
    private GameObject LevelItemPopupPrefab;

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
        LevelPanelPrefab = UIManager.Instance.Get(PrefabConstants.Progression.LEVEL_PANEL_PREFAB);
        LevelItemPopupPrefab = UIManager.Instance.Get(PrefabConstants.Universe.UNIVERSE_ITEM_POPUP_PREFAB);
    }

    // Popup Nâng Cấp Số Lượng
    // Popup Nâng Cấp Số Lượng
    // Popup Nâng Cấp Số Lượng
    public async Task CreateLevelPanelAsync<T>(T stat) where T : IStats
    {
        GameObject gameObject = Instantiate(LevelPanelPrefab, MainPanel);
        Transform panelTransform = gameObject.transform;

        TextMeshProUGUI currentLevelText = panelTransform.Find("CurrentLevel").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nextLevelText = panelTransform.Find("NextLevel").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI maxLevelText = panelTransform.Find("MaxLevel").GetComponent<TextMeshProUGUI>();
        Slider quantitySlider = panelTransform.Find("QuantitySlider").GetComponent<Slider>();

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
        
        Button previewButton = panelTransform.Find("PreviewButton").GetComponent<Button>();
        Transform previewDefaultObj = previewButton.transform.Find("Default");
        Transform previewSelectedObj = previewButton.transform.Find("Selected");

        Transform currentStatsContent = panelTransform.Find("Scroll View/Viewport/Content/CurrentStats");
        Transform nextStatsContent = panelTransform.Find("Scroll View/Viewport/Content/NextStats");
        Transform rightTransform = panelTransform.Find("Right Scroll View/Viewport/Content");

        Transform leftScrollViewTransform = panelTransform.Find("Scroll View");
        Transform rightScrollViewTransform = panelTransform.Find("Right Scroll View");

        Image leftScrollViewBg = leftScrollViewTransform.GetComponent<Image>() ?? leftScrollViewTransform.Find("Viewport")?.GetComponent<Image>();
        Image rightScrollViewBg = rightScrollViewTransform.GetComponent<Image>() ?? rightScrollViewTransform.Find("Viewport")?.GetComponent<Image>();

        int popupCurrentLevel = stat.Level;

        string objectType = null;
        string featureCodeName = null;
        Type statType = stat.GetType();
        if (LevelUpSettingsByStatType.TryGetValue(statType, out var setting))
        {
            objectType = setting.ObjectType;
            featureCodeName = setting.FeatureCodeName;
        }

        GameConfig gameConfig = await GameConfigService.Create().GetGameConfigAsync(objectType, LevelObjectColumn);
        List<GameGrowthConfig> growthConfig = await GameGrowthConfigService.Create().GetConfigAsync(objectType, LevelGrowthType, popupCurrentLevel);

        RecipeResponseDTO popupRecipeData = await RecipeService.Create().GetRecipeItemsByFeatureCodeNameAsync(featureCodeName, popupCurrentLevel + 1, User.CurrentUserId);

        double recipeMaxLevel = popupRecipeData?.Recipes != null && popupRecipeData.Recipes.Count > 0
            ? popupRecipeData.Recipes.Max(r => r.MaxLevel)
            : popupCurrentLevel;

        double maxLevel = double.Parse(gameConfig.MaxValue);
        double maxSliderValue = Math.Max(0.0, recipeMaxLevel - popupCurrentLevel);

        LevelDTO levelDto = new LevelDTO
        {
            Level = popupCurrentLevel,
            Configs = growthConfig,
        };

        currentLevelText.text = popupCurrentLevel.ToString();
        nextLevelText.text = popupCurrentLevel.ToString();
        if (maxLevelText != null) maxLevelText.text = maxLevel.ToString();

        if (stat != null)
        {
            StatsManager.Instance.CreateStatsManager(stat, currentStatsContent);
            StatsManager.Instance.CreateStatsManager(stat, nextStatsContent);
        }

        quantitySlider.minValue = 0;
        quantitySlider.maxValue = (float)maxSliderValue;
        quantitySlider.wholeNumbers = true;
        quantitySlider.value = 0;

        bool isPreviewSelected = false;

        Button[] adjustmentButtons = new Button[]
        {
            increaseOneButton, increaseTenButton, increaseMaxButton,
            decreaseOneButton, decreaseTenButton, decreaseMaxButton
        };

        void UpdatePreviewButtonState(bool isSelected)
        {
            if (previewDefaultObj != null) previewDefaultObj.gameObject.SetActive(!isSelected);
            if (previewSelectedObj != null) previewSelectedObj.gameObject.SetActive(isSelected);
        }

        void UpdateSliderButtonsUI(bool isSelected)
        {
            Color targetBgColor = ColorHelper.HexToColor(isSelected ? ColorConstants.YELLOW_COLOR : ColorConstants.RED_COLOR);
            Color targetTextColor = ColorHelper.HexToColor(isSelected ? ColorConstants.BLACK_COLOR : ColorConstants.WHITE_COLOR);

            Image previewBtnImg = previewButton.GetComponent<Image>();
            if (previewBtnImg != null) previewBtnImg.color = targetBgColor;

            Color sidebarColor = new Color(targetBgColor.r, targetBgColor.g, targetBgColor.b, 0.35f);
            if (leftScrollViewBg != null) leftScrollViewBg.color = sidebarColor;
            if (rightScrollViewBg != null) rightScrollViewBg.color = sidebarColor;

            UpdateScrollbarHandleColor(leftScrollViewTransform, targetBgColor);
            UpdateScrollbarHandleColor(rightScrollViewTransform, targetBgColor);

            if (quantitySlider.fillRect != null)
            {
                Image fillImage = quantitySlider.fillRect.GetComponent<Image>();
                if (fillImage != null) fillImage.color = targetBgColor;
            }
            if (quantitySlider.handleRect != null)
            {
                Image handleImage = quantitySlider.handleRect.GetComponent<Image>();
                if (handleImage != null) handleImage.color = targetBgColor;
            }

            foreach (var btn in adjustmentButtons)
            {
                if (btn == null) continue;

                Image btnImage = btn.GetComponent<Image>();
                if (btnImage != null) btnImage.color = targetBgColor;

                TextMeshProUGUI btnText = btn.GetComponentInChildren<TextMeshProUGUI>();
                if (btnText != null) btnText.color = targetTextColor;
            }
        }

        void SetPreviewNotification(string value, bool isSuccess)
        {
            notificationText.text = LocalizationManager.Get(value);

            string colorHex = isSuccess ? ColorConstants.GREEN_COLOR : ColorConstants.RED_COLOR;
            string iconPath = isSuccess ? ImageConstants.status.CONFIRM_URL : ImageConstants.status.CANCEL_URL;

            Color statusColor = ColorHelper.HexToColor(colorHex);
            notificationText.color = statusColor;

            if (notificationIconImage != null)
            {
                Texture iconTex = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(iconPath));
                if (iconTex != null)
                {
                    notificationIconImage.texture = iconTex;
                    notificationIconImage.color = statusColor;
                    notificationIconImage.gameObject.SetActive(true);
                }
            }
        }

        void UpdateLevelDisplay(int levelValue)
        {
            string formattedText = $"+{levelValue}";
            if (levelInputField != null)
            {
                levelInputField.text = levelValue.ToString();
            }
            else if (itemUsedQuantityText != null)
            {
                itemUsedQuantityText.text = formattedText;
            }
        }

        async void UpdatePreview()
        {
            int requested = (int)quantitySlider.value;

            if (popupCurrentLevel >= maxLevel)
            {
                var backgroundImage = confirmButton.transform.Find("Background2")?.GetComponent<RawImage>();
                if (backgroundImage != null)
                    backgroundImage.color = ColorHelper.HexToColor(ColorConstants.GRAY_COLOR);

                SetPreviewNotification(MessageConstants.UPGRADE_ALREADY_MAX, false);
                nextLevelText.text = "MAX";
                confirmButton.interactable = false;
                UpdateLevelDisplay(0);

                if (stat != null)
                    StatsManager.Instance.CreateStatsManager(stat, nextStatsContent);

                return;
            }

            if (requested <= 0)
            {
                nextLevelText.text = popupCurrentLevel.ToString();
                confirmButton.interactable = false;
                UpdateLevelDisplay(0);

                if (stat != null)
                    StatsManager.Instance.CreateStatsManager(stat, nextStatsContent);

                foreach (Transform child in rightTransform) Destroy(child.gameObject);
                SetPreviewNotification(MessageConstants.READY_TO_UPGRADE, true);
                return;
            }

            // --- ĐOẠN ĐƯỢC CHỈ ĐỊNH NẰM TẠI ĐÂY ---
            RecipeCalculateDTO preview;

            if (isPreviewSelected)
            {
                // Ở chế độ Preview: Gọi hàm tính toán lý thuyết không kẹt kho đồ (nguyên liệu tăng theo số level muốn xem)
                preview = await RecipeCalculator.CalculateUpgradePreviewByLevelsAsync(
                    popupRecipeData,
                    popupCurrentLevel,
                    requested,
                    (int)maxLevel,
                    levelDto
                );
            }
            else
            {
                // Ở chế độ Default: Gọi hàm tính toán thực tế có kiểm tra kho đồ
                preview = await RecipeCalculator.CalculateUpgradeByLevelsValueBasedAsync(
                    popupRecipeData,
                    popupCurrentLevel,
                    requested,
                    (int)maxLevel,
                    levelDto
                );
            }
            // ------------------------------------

            int targetLevelDisplay = Math.Min(popupCurrentLevel + requested, (int)maxLevel);
            nextLevelText.text = isPreviewSelected ? targetLevelDisplay.ToString() : preview.TargetLevel.ToString();

            if (isPreviewSelected)
            {
                confirmButton.interactable = false;
            }
            else
            {
                confirmButton.interactable = preview.GainedLevels > 0;
            }

            int displayGained = isPreviewSelected ? requested : (preview.GainedLevels > 0 ? preview.GainedLevels : requested);

            if (displayGained > 0 && stat != null)
            {
                StatsManager.Instance.CreateStatsManager(stat, nextStatsContent);
            }
            else if (stat != null)
            {
                StatsManager.Instance.CreateStatsManager(stat, nextStatsContent);
            }

            var materials = preview.TotalDeductions ?? new List<RecipeItemDetailDTO>();
            int existingChildCount = rightTransform.childCount;
            int neededChildCount = materials.Count;

            for (int i = existingChildCount - 1; i >= neededChildCount; i--)
            {
                Destroy(rightTransform.GetChild(i).gameObject);
            }

            for (int i = 0; i < neededChildCount; i++)
            {
                GameObject itemGO = (i < existingChildCount)
                    ? rightTransform.GetChild(i).gameObject
                    : Instantiate(LevelItemPopupPrefab, rightTransform);

                double ownedQty = materials[i].UserQuantity;
                if (ownedQty <= 0 && popupRecipeData != null && popupRecipeData.UserInventory != null)
                {
                    string objectId = materials[i].ObjectId;
                    string prefixedKey = $"{materials[i].ObjectType}_{objectId}";

                    if (popupRecipeData.UserInventory.TryGetValue(objectId, out var q1))
                        ownedQty = q1;
                    else if (popupRecipeData.UserInventory.TryGetValue(prefixedKey, out var q2))
                        ownedQty = q2;
                }

                SetupLevelItemUI2(itemGO, new RecipePreviewDetailDTO
                {
                    ObjectId = materials[i].ObjectId,
                    ObjectType = materials[i].ObjectType,
                    ObjectImage = materials[i].ObjectImage,
                    TotalRequiredQuantity = materials[i].RequiredQuantity,
                    CurrentUserQuantity = ownedQty,
                    IsEnough = ownedQty >= materials[i].RequiredQuantity
                });
            }

            UpdateLevelDisplay(displayGained);

            if (isPreviewSelected)
            {
                SetPreviewNotification(MessageConstants.READY_TO_UPGRADE, true);
            }
            else
            {
                if (preview.GainedLevels >= requested)
                {
                    SetPreviewNotification(MessageConstants.READY_TO_UPGRADE, true);
                }
                else
                {
                    SetPreviewNotification(MessageConstants.NOT_ENOUGH_MATERIALS, false);
                    confirmButton.interactable = false;
                }
            }
        }

        if (levelInputField != null)
        {
            levelInputField.onEndEdit.AddListener(inputValue =>
            {
                ValidateAndApplyInputValue(inputValue, maxSliderValue, levelInputField, quantitySlider, UpdatePreview);
            });
        }

        UpdatePreviewButtonState(false);

        previewButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            isPreviewSelected = !isPreviewSelected;

            UpdatePreviewButtonState(isPreviewSelected);
            UpdateSliderButtonsUI(isPreviewSelected);

            if (isPreviewSelected)
            {
                quantitySlider.maxValue = (float)Math.Max(0.0, maxLevel - popupCurrentLevel);
            }
            else
            {
                quantitySlider.maxValue = (float)Math.Max(0.0, recipeMaxLevel - popupCurrentLevel);
                if (quantitySlider.value > quantitySlider.maxValue)
                    quantitySlider.value = quantitySlider.maxValue;
            }

            quantitySlider.SetValueWithoutNotify(0);
            UpdatePreview();
        });

        quantitySlider.onValueChanged.AddListener(_ => UpdatePreview());

        increaseOneButton.onClick.AddListener(() =>
        {
            quantitySlider.value = Mathf.Min(quantitySlider.maxValue, quantitySlider.value + 1);
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        increaseTenButton.onClick.AddListener(() =>
        {
            quantitySlider.value = Mathf.Min(quantitySlider.maxValue, quantitySlider.value + 10);
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        increaseMaxButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            if (isPreviewSelected)
            {
                quantitySlider.value = quantitySlider.maxValue;
            }
            else
            {
                RecipeCalculateDTO maxAffordable = await RecipeCalculator.CalculateMaxUpgradeValueBasedAsync(
                    popupRecipeData,
                    popupCurrentLevel,
                    (int)maxLevel,
                    levelDto
                );
                quantitySlider.value = Mathf.Clamp(maxAffordable.GainedLevels, 0, quantitySlider.maxValue);
            }
        });

        decreaseOneButton.onClick.AddListener(() =>
        {
            quantitySlider.value = Mathf.Max(0, quantitySlider.value - 1);
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        decreaseTenButton.onClick.AddListener(() =>
        {
            quantitySlider.value = Mathf.Max(0, quantitySlider.value - 10);
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        decreaseMaxButton.onClick.AddListener(() =>
        {
            quantitySlider.value = 0;
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });

        UpdateSliderButtonsUI(false);
        UpdatePreview();

        confirmButton.onClick.AddListener(async () =>
        {
            if (isPreviewSelected)
            {
                SetPreviewNotification(MessageConstants.CANNOT_CONFIRM_IN_PREVIEW_MODE, false);
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                return;
            }

            if (popupCurrentLevel >= maxLevel)
            {
                SetPreviewNotification(MessageConstants.UPGRADE_ALREADY_MAX, false);
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                return;
            }

            int requested = (int)quantitySlider.value;
            if (requested <= 0)
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                return;
            }

            AudioManager.Instance.PlaySFX(AudioConstants.SFX.LEVEL_UP_SOUND);
            confirmButton.interactable = false;
            closeButton.interactable = false;

            RecipeCalculateDTO calcResult = await RecipeCalculator.CalculateUpgradeByLevelsValueBasedAsync(
                popupRecipeData,
                popupCurrentLevel,
                requested,
                (int)maxLevel,
                levelDto
            );

            if (calcResult.GainedLevels <= 0)
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.ALERT_SOUND);
                confirmButton.interactable = true;
                closeButton.interactable = true;
                return;
            }

            stat.Level = calcResult.TargetLevel;

            await ExecuteServiceInsertAsync(stat, calcResult.TotalDeductions);

            Destroy(gameObject);
            await RefreshPanelAsync(stat);
        });

        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(gameObject);
        });
    }

    private async Task RefreshPanelAsync<T>(T stat) where T : IStats
    {
        await Task.CompletedTask;
    }

    private void SetupLevelItemUI2(GameObject itemGO, RecipePreviewDetailDTO data)
    {
        TextMeshProUGUI requiredText = itemGO.transform.Find("RequiredText")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI ownedText = itemGO.transform.Find("AvailableText")?.GetComponent<TextMeshProUGUI>();
        RawImage image = itemGO.transform.Find("Image")?.GetComponent<RawImage>();

        bool isEnough = data.CurrentUserQuantity >= data.TotalRequiredQuantity;
        Color statusColor = ColorHelper.HexToColor(isEnough ? ColorConstants.GREEN_COLOR : ColorConstants.RED_COLOR);

        if (requiredText != null)
        {
            requiredText.text = data.TotalRequiredQuantity.ToString();
            requiredText.color = statusColor;
        }

        if (ownedText != null)
        {
            ownedText.text = data.CurrentUserQuantity.ToString();
            ownedText.color = statusColor;
        }

        if (image != null && !string.IsNullOrEmpty(data.ObjectImage))
        {
            Texture texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(data.ObjectImage));
            if (texture != null)
                image.texture = texture;
        }
    }

    private void UpdateScrollbarHandleColor(Transform scrollViewTransform, Color color)
    {
        if (scrollViewTransform == null) return;

        Scrollbar scrollbar = scrollViewTransform.GetComponentInChildren<Scrollbar>();
        if (scrollbar == null) return;

        if (scrollbar.handleRect != null)
        {
            Image handleImage = scrollbar.handleRect.GetComponent<Image>();
            if (handleImage != null)
            {
                handleImage.color = color;
            }
        }
        else if (scrollbar.targetGraphic != null)
        {
            scrollbar.targetGraphic.color = color;
        }
    }

    public async Task ExecuteServiceInsertAsync<T>(T stat, List<RecipeItemDetailDTO> totalDeductions) where T : IStats
    {
        try
        {
            if (stat is Achievements achievement)
            {
                // await UserAchievementsService.Create().UpdateUserAchievementLevelAsync(User.CurrentUserId, achievement, totalDeductions);
                // UserAchievementsController.Instance.RefreshCurrentDetailsUI(achievement);
            }
            else if (stat is Alchemies alchemy)
            {
                await UserAlchemiesService.Create().UpdateUserAlchemyLevelAsync(User.CurrentUserId, alchemy, totalDeductions);
                UserAlchemiesController.Instance.RefreshCurrentDetailsUI(alchemy);
            }
            else if (stat is Architectures architecture)
            {
                await UserArchitecturesService.Create().UpdateUserArchitectureLevelAsync(User.CurrentUserId, architecture, totalDeductions);
                UserArchitecturesController.Instance.RefreshCurrentDetailsUI(architecture);
            }
            else if (stat is Artifacts artifact)
            {
                await UserArtifactsService.Create().UpdateUserArtifactLevelAsync(User.CurrentUserId, artifact, totalDeductions);
                UserArtifactsController.Instance.RefreshCurrentDetailsUI(artifact);
            }
            else if (stat is Artworks artwork)
            {
                await UserArtworksService.Create().UpdateUserArtworkLevelAsync(User.CurrentUserId, artwork, totalDeductions);
                UserArtworksController.Instance.RefreshCurrentDetailsUI(artwork);
            }
            else if (stat is Avatars avatar)
            {
                await UserAvatarsService.Create().UpdateUserAvatarLevelAsync(User.CurrentUserId, avatar, totalDeductions);
                UserAvatarsController.Instance.RefreshCurrentDetailsUI(avatar);
            }
            else if (stat is Badges badge)
            {
                await UserBadgesService.Create().UpdateUserBadgeLevelAsync(User.CurrentUserId, badge, totalDeductions);
                UserBadgesController.Instance.RefreshCurrentDetailsUI(badge);
            }
            else if (stat is Beverages beverage)
            {
                await UserBeveragesService.Create().UpdateUserBeverageLevelAsync(User.CurrentUserId, beverage, totalDeductions);
                UserBeveragesController.Instance.RefreshCurrentDetailsUI(beverage);
            }
            else if (stat is Books book)
            {
                await UserBooksService.Create().UpdateUserBookLevelAsync(User.CurrentUserId, book, totalDeductions);
                UserBooksController.Instance.RefreshCurrentDetailsUI(book);
            }
            else if (stat is Borders border)
            {
                await UserBordersService.Create().UpdateUserBorderLevelAsync(User.CurrentUserId, border, totalDeductions);
                UserBordersController.Instance.RefreshCurrentDetailsUI(border);
            }
            else if (stat is Buildings building)
            {
                await UserBuildingsService.Create().UpdateUserBuildingLevelAsync(User.CurrentUserId, building, totalDeductions);
                UserBuildingsController.Instance.RefreshCurrentDetailsUI(building);
            }
            else if (stat is CardAdmirals admiral)
            {
                await UserCardAdmiralsService.Create().UpdateUserCardAdmiralLevelAsync(User.CurrentUserId, admiral, totalDeductions);
                UserCardAdmiralsController.Instance.RefreshCurrentDetailsUI(admiral);
            }
            else if (stat is CardCaptains captain)
            {
                await UserCardCaptainsService.Create().UpdateUserCardCaptainLevelAsync(User.CurrentUserId, captain, totalDeductions);
                UserCardCaptainsController.Instance.RefreshCurrentDetailsUI(captain);
            }
            else if (stat is CardColonels colonel)
            {
                await UserCardColonelsService.Create().UpdateUserCardColonelLevelAsync(User.CurrentUserId, colonel, totalDeductions);
                UserCardColonelsController.Instance.RefreshCurrentDetailsUI(colonel);
            }
            else if (stat is CardGenerals general)
            {
                await UserCardGeneralsService.Create().UpdateUserCardGeneralLevelAsync(User.CurrentUserId, general, totalDeductions);
                UserCardGeneralsController.Instance.RefreshCurrentDetailsUI(general);
            }
            else if (stat is CardHeroes hero)
            {
                await UserCardHeroesService.Create().UpdateUserCardHeroLevelAsync(User.CurrentUserId, hero, totalDeductions);
                UserCardHeroesController.Instance.RefreshCurrentDetailsUI(hero);
            }
            else if (stat is CardLives cardLife)
            {
                await UserCardLivesService.Create().UpdateUserCardLifeLevelAsync(User.CurrentUserId, cardLife, totalDeductions);
                UserCardLivesController.Instance.RefreshCurrentDetailsUI(cardLife);
            }
            else if (stat is CardMilitaries military)
            {
                await UserCardMilitariesService.Create().UpdateUserCardMilitaryLevelAsync(User.CurrentUserId, military, totalDeductions);
                UserCardMilitariesController.Instance.RefreshCurrentDetailsUI(military);
            }
            else if (stat is CardMonsters monster)
            {
                await UserCardMonstersService.Create().UpdateUserCardMonsterLevelAsync(User.CurrentUserId, monster, totalDeductions);
                UserCardMonstersController.Instance.RefreshCurrentDetailsUI(monster);
            }
            else if (stat is CardSoldiers soldier)
            {
                await UserCardSoldiersService.Create().UpdateUserCardSoldierLevelAsync(User.CurrentUserId, soldier, totalDeductions);
                UserCardSoldiersController.Instance.RefreshCurrentDetailsUI(soldier);
            }
            else if (stat is CardSpells spell)
            {
                await UserCardSpellsService.Create().UpdateUserCardSpellLevelAsync(User.CurrentUserId, spell, totalDeductions);
                UserCardSpellsController.Instance.RefreshCurrentDetailsUI(spell);
            }
            else if (stat is CollaborationEquipments collabEquip)
            {
                await UserCollaborationEquipmentsService.Create().UpdateUserCollaborationEquipmentLevelAsync(User.CurrentUserId, collabEquip, totalDeductions);
                UserCollaborationEquipmentsController.Instance.RefreshCurrentDetailsUI(collabEquip);
            }
            else if (stat is Collaborations collab)
            {
                await UserCollaborationsService.Create().UpdateUserCollaborationLevelAsync(User.CurrentUserId, collab, totalDeductions);
                UserCollaborationsController.Instance.RefreshCurrentDetailsUI(collab);
            }
            else if (stat is Cores core)
            {
                await UserCoresService.Create().UpdateUserCoreLevelAsync(User.CurrentUserId, core, totalDeductions);
                UserCoresController.Instance.RefreshCurrentDetailsUI(core);
            }
            else if (stat is Emojis emoji)
            {
                await UserEmojisService.Create().UpdateUserEmojiLevelAsync(User.CurrentUserId, emoji, totalDeductions);
                UserEmojisController.Instance.RefreshCurrentDetailsUI(emoji);
            }
            else if (stat is Equipments equipment)
            {
                await UserEquipmentsService.Create().UpdateUserEquipmentLevelAsync(User.CurrentUserId, equipment, totalDeductions);
                UserEquipmentsController.Instance.RefreshCurrentDetailsUI(equipment);
            }
            else if (stat is Fashions fashion)
            {
                await UserFashionsService.Create().UpdateUserFashionLevelAsync(User.CurrentUserId, fashion, totalDeductions);
                UserFashionsController.Instance.RefreshCurrentDetailsUI(fashion);
            }
            else if (stat is Foods food)
            {
                await UserFoodsService.Create().UpdateUserFoodLevelAsync(User.CurrentUserId, food, totalDeductions);
                UserFoodsController.Instance.RefreshCurrentDetailsUI(food);
            }
            else if (stat is Forges forge)
            {
                await UserForgesService.Create().UpdateUserForgeLevelAsync(User.CurrentUserId, forge, totalDeductions);
                UserForgesController.Instance.RefreshCurrentDetailsUI(forge);
            }
            else if (stat is Furnitures furniture)
            {
                await UserFurnituresService.Create().UpdateUserFurnitureLevelAsync(User.CurrentUserId, furniture, totalDeductions);
                UserFurnituresController.Instance.RefreshCurrentDetailsUI(furniture);
            }
            else if (stat is MagicFormationCircles circle)
            {
                await UserMagicFormationCirclesService.Create().UpdateUserMagicFormationCircleLevelAsync(User.CurrentUserId, circle, totalDeductions);
                UserMagicFormationCirclesController.Instance.RefreshCurrentDetailsUI(circle);
            }
            else if (stat is MechaBeasts mechaBeast)
            {
                await UserMechaBeastsService.Create().UpdateUserMechaBeastLevelAsync(User.CurrentUserId, mechaBeast, totalDeductions);
                UserMechaBeastsController.Instance.RefreshCurrentDetailsUI(mechaBeast);
            }
            else if (stat is Medals medal)
            {
                await UserMedalsService.Create().UpdateUserMedalLevelAsync(User.CurrentUserId, medal, totalDeductions);
                UserMedalsController.Instance.RefreshCurrentDetailsUI(medal);
            }
            else if (stat is Pets pet)
            {
                await UserPetsService.Create().UpdateUserPetLevelAsync(User.CurrentUserId, pet, totalDeductions);
                UserPetsController.Instance.RefreshCurrentDetailsUI(pet);
            }
            else if (stat is Plants plant)
            {
                await UserPlantsService.Create().UpdateUserPlantLevelAsync(User.CurrentUserId, plant, totalDeductions);
                UserPlantsController.Instance.RefreshCurrentDetailsUI(plant);
            }
            else if (stat is Puppets puppet)
            {
                await UserPuppetsService.Create().UpdateUserPuppetLevelAsync(User.CurrentUserId, puppet, totalDeductions);
                UserPuppetsController.Instance.RefreshCurrentDetailsUI(puppet);
            }
            else if (stat is Relics relic)
            {
                await UserRelicsService.Create().UpdateUserRelicLevelAsync(User.CurrentUserId, relic, totalDeductions);
                UserRelicsController.Instance.RefreshCurrentDetailsUI(relic);
            }
            else if (stat is Robots robot)
            {
                await UserRobotsService.Create().UpdateUserRobotLevelAsync(User.CurrentUserId, robot, totalDeductions);
                UserRobotsController.Instance.RefreshCurrentDetailsUI(robot);
            }
            else if (stat is Runes rune)
            {
                await UserRunesService.Create().UpdateUserRuneLevelAsync(User.CurrentUserId, rune, totalDeductions);
                UserRunesController.Instance.RefreshCurrentDetailsUI(rune);
            }
            else if (stat is Skills skill)
            {
                await UserSkillsService.Create().UpdateUserSkillLevelAsync(User.CurrentUserId, skill, totalDeductions);
                UserSkillsController.Instance.RefreshCurrentDetailsUI(skill);
            }
            else if (stat is SpiritBeasts spiritBeast)
            {
                await UserSpiritBeastsService.Create().UpdateUserSpiritBeastLevelAsync(User.CurrentUserId, spiritBeast, totalDeductions);
                UserSpiritBeastsController.Instance.RefreshCurrentDetailsUI(spiritBeast);
            }
            else if (stat is SpiritCards spiritCard)
            {
                await UserSpiritCardsService.Create().UpdateUserSpiritCardLevelAsync(User.CurrentUserId, spiritCard, totalDeductions);
                UserSpiritCardsController.Instance.RefreshCurrentDetailsUI(spiritCard);
            }
            else if (stat is Symbols symbol)
            {
                await UserSymbolsService.Create().UpdateUserSymbolLevelAsync(User.CurrentUserId, symbol, totalDeductions);
                UserSymbolsController.Instance.RefreshCurrentDetailsUI(symbol);
            }
            else if (stat is Talismans talisman)
            {
                await UserTalismansService.Create().UpdateUserTalismanLevelAsync(User.CurrentUserId, talisman, totalDeductions);
                UserTalismansController.Instance.RefreshCurrentDetailsUI(talisman);
            }
            else if (stat is Technologies technology)
            {
                await UserTechnologiesService.Create().UpdateUserTechnologyLevelAsync(User.CurrentUserId, technology, totalDeductions);
                UserTechnologiesController.Instance.RefreshCurrentDetailsUI(technology);
            }
            else if (stat is Titles title)
            {
                await UserTitlesService.Create().UpdateUserTitleLevelAsync(User.CurrentUserId, title, totalDeductions);
                UserTitlesController.Instance.RefreshCurrentDetailsUI(title);
            }
            else if (stat is Vehicles vehicle)
            {
                await UserVehiclesService.Create().UpdateUserVehicleLevelAsync(User.CurrentUserId, vehicle, totalDeductions);
                UserVehiclesController.Instance.RefreshCurrentDetailsUI(vehicle);
            }
            else if (stat is Weapons weapon)
            {
                await UserWeaponsService.Create().UpdateUserWeaponLevelAsync(User.CurrentUserId, weapon, totalDeductions);
                UserWeaponsController.Instance.RefreshCurrentDetailsUI(weapon);
            }
            else if (stat is Outfits outfit)
            {
                await UserOutfitsService.Create().UpdateUserOutfitLevelAsync(User.CurrentUserId, outfit, totalDeductions);
                UserOutfitsController.Instance.RefreshCurrentDetailsUI(outfit);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error executing service insert for stat type {stat.GetType().Name}: {ex.Message}");
            throw;
        }
    }
    /// <summary>
    /// Hàm xử lý và chuẩn hóa giá trị nhập tay từ InputField, lọc bỏ chữ, ký tự đặc biệt, số âm và kẹp trong khoảng [0, maxSliderValue].
    /// </summary>
    private void ValidateAndApplyInputValue(string inputValue, double maxSliderValue, TMP_InputField levelInputField, Slider quantitySlider, Action onValidValueApplied)
    {
        double clampedLevel = double.TryParse(inputValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double val)
            ? Math.Max(0.0, Math.Min(val, maxSliderValue))
            : 0.0;

        if (levelInputField != null)
        {
            levelInputField.text = clampedLevel.ToString("0", CultureInfo.InvariantCulture);
        }

        if (quantitySlider != null)
        {
            quantitySlider.SetValueWithoutNotify((float)clampedLevel);
        }

        onValidValueApplied?.Invoke();
    }
}