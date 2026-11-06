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

    // Popup Nâng Cấp Sao (Star) theo cơ chế Value-based
    public async Task CreateStarPanelAsync<T>(T stat) where T : IStats
    {
        GameObject gameObject = Instantiate(StarPanelPrefab, MainPanel);
        Transform panelTransform = gameObject.transform;

        TextMeshProUGUI currentStarText = panelTransform.Find("CurrentLevel").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nextStarText = panelTransform.Find("NextLevel").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI maxStarText = panelTransform.Find("MaxLevel").GetComponent<TextMeshProUGUI>();
        Slider quantitySlider = panelTransform.Find("QuantitySlider").GetComponent<Slider>();

        TMP_InputField starInputField = panelTransform.Find("ItemUsedQuantityText")?.GetComponent<TMP_InputField>();
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

        Transform currentStatsContent = panelTransform.Find("Scroll View/Viewport/Content/CurrentStats");
        Transform nextStatsContent = panelTransform.Find("Scroll View/Viewport/Content/NextStats");
        Transform rightTransform = panelTransform.Find("Right Scroll View/Viewport/Content");

        Transform leftScrollViewTransform = panelTransform.Find("Scroll View");
        Transform rightScrollViewTransform = panelTransform.Find("Right Scroll View");

        Image leftScrollViewBg = leftScrollViewTransform.GetComponent<Image>() ?? leftScrollViewTransform.Find("Viewport")?.GetComponent<Image>();
        Image rightScrollViewBg = rightScrollViewTransform.GetComponent<Image>() ?? rightScrollViewTransform.Find("Viewport")?.GetComponent<Image>();

        int popupCurrentStar = stat.Star;

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

        int maxStar = int.Parse(gameConfig.MaxValue);
        float maxSliderValue = Mathf.Max(0, (float)(recipeMaxStar - popupCurrentStar));

        LevelDTO levelDto = new LevelDTO
        {
            Level = popupCurrentStar,
            Configs = growthConfig,
        };

        currentStarText.text = popupCurrentStar.ToString();
        nextStarText.text = popupCurrentStar.ToString();
        if (maxStarText != null) maxStarText.text = maxStar.ToString();

        if (stat != null)
        {
            StatsManager.Instance.CreateStatsManager(stat, currentStatsContent);
            StatsManager.Instance.CreateStatsManager(stat, nextStatsContent);
        }

        quantitySlider.minValue = 0;
        quantitySlider.maxValue = maxSliderValue;
        quantitySlider.wholeNumbers = true;
        quantitySlider.value = 0;

        bool isPreviewSelected = false;

        Button[] adjustmentButtons = new Button[]
        {
            increaseOneButton, increaseTenButton, increaseMaxButton,
            decreaseOneButton, decreaseTenButton, decreaseMaxButton
        };

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

        void UpdateStarDisplay(int starValue)
        {
            string formattedText = $"+{starValue}";
            if (starInputField != null)
            {
                starInputField.text = starValue.ToString();
            }
            else if (itemUsedQuantityText != null)
            {
                itemUsedQuantityText.text = formattedText;
            }
        }

        async void UpdatePreview()
        {
            int requested = (int)quantitySlider.value;

            if (popupCurrentStar >= maxStar)
            {
                var backgroundImage = confirmButton.transform.Find("Background2")?.GetComponent<RawImage>();
                if (backgroundImage != null)
                    backgroundImage.color = ColorHelper.HexToColor(ColorConstants.GRAY_COLOR);

                SetPreviewNotification(MessageConstants.UPGRADE_ALREADY_MAX, false);
                nextStarText.text = "MAX";
                confirmButton.interactable = false;
                UpdateStarDisplay(0);

                if (stat != null)
                    StatsManager.Instance.CreateStatsManager(stat, nextStatsContent);

                return;
            }

            if (requested <= 0)
            {
                nextStarText.text = popupCurrentStar.ToString();
                confirmButton.interactable = false;
                UpdateStarDisplay(0);

                if (stat != null)
                    StatsManager.Instance.CreateStatsManager(stat, nextStatsContent);

                foreach (Transform child in rightTransform) Destroy(child.gameObject);
                SetPreviewNotification(MessageConstants.READY_TO_UPGRADE, true);
                return;
            }

            RecipeCalculateDTO preview = await RecipeCalculator.CalculateUpgradeByLevelsValueBasedAsync(
                popupRecipeData,
                popupCurrentStar,
                requested,
                maxStar,
                levelDto
            );

            nextStarText.text = preview.TargetLevel.ToString();

            if (isPreviewSelected)
            {
                confirmButton.interactable = false;
            }
            else
            {
                confirmButton.interactable = preview.GainedLevels > 0;
            }

            int displayGained = preview.GainedLevels > 0 ? preview.GainedLevels : requested;

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
                    : Instantiate(StarItemPopupPrefab, rightTransform);

                SetupStarItemUI2(itemGO, new RecipePreviewDetailDTO
                {
                    ObjectId = materials[i].ObjectId,
                    ObjectType = materials[i].ObjectType,
                    ObjectImage = materials[i].ObjectImage,
                    TotalRequiredQuantity = materials[i].RequiredQuantity,
                    CurrentUserQuantity = materials[i].UserQuantity,
                    IsEnough = true
                });
            }

            UpdateStarDisplay(displayGained);
            SetPreviewNotification(MessageConstants.READY_TO_UPGRADE, true);
        }

        if (starInputField != null)
        {
            starInputField.onEndEdit.AddListener((inputValue) =>
            {
                if (int.TryParse(inputValue, out int parsedStar))
                {
                    float clampedStar = Mathf.Clamp(parsedStar, 0, maxSliderValue);
                    starInputField.text = clampedStar.ToString();
                    quantitySlider.SetValueWithoutNotify(clampedStar);
                }
                else
                {
                    starInputField.text = "0";
                    quantitySlider.SetValueWithoutNotify(0);
                }

                UpdatePreview();
            });
        }

        previewButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            isPreviewSelected = !isPreviewSelected;

            UpdateSliderButtonsUI(isPreviewSelected);

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
                    popupCurrentStar,
                    maxStar,
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

        // Click Confirm: Thực hiện tăng Star và gọi hàm service tương ứng
        confirmButton.onClick.AddListener(async () =>
        {
            if (isPreviewSelected)
            {
                SetPreviewNotification(MessageConstants.CANNOT_CONFIRM_IN_PREVIEW_MODE, false);
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                return;
            }

            if (popupCurrentStar >= maxStar)
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
                popupCurrentStar,
                requested,
                maxStar,
                levelDto
            );

            if (calcResult.GainedLevels <= 0)
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.ALERT_SOUND);
                confirmButton.interactable = true;
                closeButton.interactable = true;
                return;
            }

            // Gán giá trị Star mới cho stat
            stat.Star = calcResult.TargetLevel;

            // Gọi hàm thực thi service update Star tương ứng
            await ExecuteServiceStarInsertAsync(stat);

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

    private void SetupStarItemUI2(GameObject itemGO, RecipePreviewDetailDTO data)
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

    public async Task ExecuteServiceStarInsertAsync(IStats stat)
    {
        try
        {
            if (stat is Achievements achievement)
            {
                // Gọi service update Star của Achievement nếu có
            }
            else if (stat is CardHeroes hero)
            {
                await UserCardHeroesService.Create().UpdateUserCardHeroStarAsync(User.CurrentUserId, hero);
                UserCardHeroesController.Instance.RefreshCurrentDetailsUI(hero);
            }
            // Thêm các nhánh điều kiện cho các stat khác tương tự như LevelController nhưng gọi hàm Update Star tương ứng
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error updating star for {stat.GetType().Name}: {ex.Message}");
            throw;
        }
    }

    internal void CreateStarPanel(Titles title, int mAX_LEVEL, Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}