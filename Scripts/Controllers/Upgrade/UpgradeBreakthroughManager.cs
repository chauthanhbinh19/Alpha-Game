using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBreakthroughManager : MonoBehaviour
{
    public static UpgradeBreakthroughManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject UpgradePanelPrefab;
    private GameObject UpgradeButtonPrefab;
    private GameObject PopupUpgradePanelPrefab;
    private GameObject PopupUpgradeQuantityPanelPrefab;
    private GameObject PopupUpgradeButtonPrefab;
    private GameObject MainUpgradePanelPrefab;
    private GameObject UpgradeItemPrefab;
    private Transform Content;
    private FeatureUpgradeDTO FeatureUpgradeDTO;

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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        UpgradePanelPrefab = UIManager.Instance.Get("UpgradePanelPrefab");
        UpgradeButtonPrefab = UIManager.Instance.Get("UpgradeButtonPrefab");
        PopupUpgradePanelPrefab = UIManager.Instance.Get("PopupUpgradePanelPrefab");
        PopupUpgradeQuantityPanelPrefab = UIManager.Instance.Get("PopupUpgradeQuantityPanelPrefab");
        PopupUpgradeButtonPrefab = UIManager.Instance.Get("PopupUpgradeButtonPrefab");
        MainUpgradePanelPrefab = UIManager.Instance.Get("MainUpgradePanelPrefab");
        UpgradeItemPrefab = UIManager.Instance.Get("UpgradeItemPrefab");
    }

    public async Task CreateUpgradeBreakthroughManagerAsync(IStats stat)
    {
        FeatureUpgradeDTO = (await FeaturesService.Create().GetUpgradeFeaturesByTypeAsync(AppConstants.Upgrade.UPGRADE_BREAKTHROUGH, stat))
                .Values
                .FirstOrDefault();

        await CreateMainUpgradePanelAsync(FeatureUpgradeDTO.Id, FeatureUpgradeDTO.FeatureName, stat);
    }

    public async Task CreateMainUpgradePanelAsync(string featureId, string featureName, IStats stat)
    {
        GameObject currentObject = Instantiate(MainUpgradePanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Button upgradeLevelButton = transform.Find("UpgradeLevelButton").GetComponent<Button>();
        Transform leftSideContent = transform.Find("LeftSideContent");
        Transform rightSideContent = transform.Find("RightSideContent");
        TextMeshProUGUI levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();

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

        AnimationController.Instance.CreateUpgradeAnimation(currentObject);

        // --- LẤY DỮ LIỆU BAN ĐẦU ---
        Upgrades upgrade = await UpgradesService.Create().GetUpgradeByIdAsync(featureId);
        UserUpgrades userUpgrade = await UserUpgradesService.Create().GetUserUpgradesAsync(User.CurrentUserId, featureId, stat);

        int currentLevel = userUpgrade?.CurrentLevel ?? 0;

        // --- HÀM REFRESH MAIN PANEL ---
        async Task RefreshMainPanelAsync()
        {
            if (currentObject == null) return;

            // 1. Cập nhật Text Level
            levelText.text = currentLevel.ToString();

            // 2. Cập nhật Nguyên liệu ở Main Panel dựa theo Level mới (+1 level kế tiếp)
            // SỬA LỖI: Thay User.CurrentUserLevel bằng (currentLevel + 1)
            List<RecipeItemDto> refreshedRecipeItems = await RecipeService.Create()
                .GetRecipeItemsAsync(featureName, currentLevel + 1, User.CurrentUserId);

            if (refreshedRecipeItems == null || currentObject == null) return;

            foreach (Transform child in leftSideContent) Destroy(child.gameObject);
            foreach (Transform child in rightSideContent) Destroy(child.gameObject);

            int refreshedTotal = refreshedRecipeItems.Count;
            int refreshedLeftCount = Mathf.CeilToInt(refreshedTotal / 2f);

            for (int i = 0; i < refreshedTotal; i++)
            {
                Transform parent = (i < refreshedLeftCount) ? leftSideContent : rightSideContent;
                GameObject itemGO = Instantiate(UpgradeItemPrefab, parent);
                SetupUpgradeItemUI(itemGO, refreshedRecipeItems[i]);
            }
        }

        // Render Main Panel lần đầu
        await RefreshMainPanelAsync();

        // --- POPUP NÂNG CẤP ---
        void CreatePopupUpgradePanel()
        {
            GameObject popupGO = Instantiate(PopupUpgradeQuantityPanelPrefab, MainPanel);
            Transform panelTransform = popupGO.transform;

            TextMeshProUGUI currentLevelText = panelTransform.Find("CurrentLevel").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nextLevelText = panelTransform.Find("NextLevel").GetComponent<TextMeshProUGUI>();
            Slider quantitySlider = panelTransform.Find("QuantitySlider").GetComponent<Slider>();
            TextMeshProUGUI itemUsedQuantityText = panelTransform.Find("ItemUsedQuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI notificationText = panelTransform.Find("Notification/ContentText").GetComponent<TextMeshProUGUI>();

            Button increaseOneButton = panelTransform.Find("IncreaseOneButton").GetComponent<Button>();
            Button increaseTenButton = panelTransform.Find("IncreaseTenButton").GetComponent<Button>();
            Button increaseMaxButton = panelTransform.Find("IncreaseMaxButton").GetComponent<Button>();
            Button decreaseOneButton = panelTransform.Find("DecreaseOneButton").GetComponent<Button>();
            Button decreaseTenButton = panelTransform.Find("DecreaseTenButton").GetComponent<Button>();
            Button decreaseMaxButton = panelTransform.Find("DecreaseMaxButton").GetComponent<Button>();
            Button confirmButton = panelTransform.Find("ConfirmButton").GetComponent<Button>();
            Button popupCloseButton = panelTransform.Find("CloseButton").GetComponent<Button>();
            Transform currentStatsContent = panelTransform.Find("Scroll View/Viewport/Content/CurrentStats");
            Transform nextStatsContent = panelTransform.Find("Scroll View/Viewport/Content/NextStats");

            if (userUpgrade != null && currentStatsContent != null)
            {
                StatsManager.Instance.CreateStatsManager(userUpgrade, currentStatsContent);
            }

            if (userUpgrade != null && nextStatsContent != null)
            {
                StatsManager.Instance.CreateStatsManager(userUpgrade, nextStatsContent);
            }

            // Reset Trạng Thái Slider & Text
            void ResetPopupState()
            {
                if (popupGO == null) return;

                int maxLevel = upgrade != null ? upgrade.MaxLevel : currentLevel;
                int maxPossible = Mathf.Max(0, maxLevel - currentLevel);

                currentLevelText.text = currentLevel.ToString();

                quantitySlider.onValueChanged.RemoveAllListeners();

                if (maxPossible <= 0)
                {
                    quantitySlider.minValue = 0;
                    quantitySlider.maxValue = 0;
                    quantitySlider.value = 0;
                    quantitySlider.interactable = false;
                }
                else
                {
                    quantitySlider.interactable = true;
                    quantitySlider.minValue = 1;
                    quantitySlider.maxValue = maxPossible;
                    quantitySlider.wholeNumbers = true;
                    quantitySlider.value = 1;
                }

                quantitySlider.onValueChanged.AddListener(_ => UpdatePreview());
            }

            void SetPreviewNotification(string value, Color color)
            {
                if (notificationText == null) return;
                notificationText.text = LocalizationManager.Get(value);
                notificationText.color = color;
            }

            async Task UpdatePreviewAsync()
            {
                if (popupGO == null) return;

                int maxLevel = upgrade != null ? upgrade.MaxLevel : currentLevel;
                int maxPossible = Mathf.Max(0, maxLevel - currentLevel);
                int requested = (int)quantitySlider.value;

                if (maxPossible <= 0 || currentLevel >= maxLevel)
                {
                    var backgroundImage = confirmButton.transform.Find("Background2")?.GetComponent<RawImage>();
                    if (backgroundImage != null) backgroundImage.color = Color.gray;

                    SetPreviewNotification(MessageConstants.UPGRADE_ALREADY_MAX, Color.red);
                    nextLevelText.text = "MAX";
                    confirmButton.interactable = false;
                    itemUsedQuantityText.text = "0";

                    if (userUpgrade != null && nextStatsContent != null)
                    {
                        StatsManager.Instance.CreateStatsManager(userUpgrade, nextStatsContent);
                    }

                    return;
                }

                var preview = await UpgradeFunctionHelper.PreviewUpgradeAsync(
                    featureName,
                    currentLevel,
                    maxLevel,
                    requested,
                    User.CurrentUserId);

                if (popupGO == null) return;

                if (!preview.Success)
                {
                    SetPreviewNotification(preview.Message, Color.red);
                    confirmButton.interactable = false;
                    nextLevelText.text = preview.TargetLevel.ToString();
                    itemUsedQuantityText.text = "0";
                    return;
                }

                nextLevelText.text = preview.TargetLevel.ToString();

                bool hasEnough = true;
                if (preview.RequiredItems != null && preview.RequiredItems.Count > 0)
                {
                    var first = preview.RequiredItems.First();
                    string firstItemId = first.Key;
                    double requiredQty = first.Value;

                    var recipeLevelItems = await RecipeService.Create()
                        .GetRecipeItemsAsync(featureName, currentLevel + 1, User.CurrentUserId);

                    double owned = 0;
                    if (recipeLevelItems != null)
                    {
                        var match = recipeLevelItems.FirstOrDefault(x => x.ItemId == firstItemId);
                        if (match != null) owned = match.UserQuantity;
                    }

                    itemUsedQuantityText.text = requiredQty.ToString();

                    if (owned < requiredQty) hasEnough = false;
                }
                else
                {
                    itemUsedQuantityText.text = "0";
                }

                if (preview.UpgradedLevels > 0)
                {
                    UserUpgrades previewUpgrade = userUpgrade.CloneUserUpgrade(userUpgrade);
                    if (previewUpgrade != null)
                    {
                        EnhanceHelper.EnhanceUpgrades(previewUpgrade, preview.UpgradedLevels, upgrade?.BaseMultiplier ?? 1);
                        if (nextStatsContent != null)
                        {
                            StatsManager.Instance.CreateStatsManager(previewUpgrade, nextStatsContent);
                        }
                    }
                }
                else if (userUpgrade != null && nextStatsContent != null)
                {
                    StatsManager.Instance.CreateStatsManager(userUpgrade, nextStatsContent);
                }

                if (preview.UpgradedLevels > 0 && hasEnough)
                {
                    SetPreviewNotification(MessageConstants.READY_TO_UPGRADE, Color.green);
                    confirmButton.interactable = true;
                }
                else
                {
                    SetPreviewNotification(MessageConstants.NOT_ENOUGH_MATERIALS, Color.red);
                    confirmButton.interactable = false;
                }
            }

            async void UpdatePreview() => await UpdatePreviewAsync();

            increaseOneButton.onClick.AddListener(() => { quantitySlider.value += 1; AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND); });
            increaseTenButton.onClick.AddListener(() => { quantitySlider.value += 10; AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND); });
            increaseMaxButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                quantitySlider.SetValueWithoutNotify(quantitySlider.maxValue);
                await UpdatePreviewAsync();
            });

            decreaseOneButton.onClick.AddListener(() => { quantitySlider.value -= 1; AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND); });
            decreaseTenButton.onClick.AddListener(() => { quantitySlider.value -= 10; AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND); });
            decreaseMaxButton.onClick.AddListener(() => { quantitySlider.value = quantitySlider.minValue; AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND); });

            ResetPopupState();
            UpdatePreview();

            // --- XỬ LÝ NÚT CONFIRM ---
            confirmButton.onClick.AddListener(async () =>
            {
                int maxLevel = upgrade != null ? upgrade.MaxLevel : currentLevel;

                if (currentLevel >= maxLevel)
                {
                    SetPreviewNotification(MessageConstants.UPGRADE_ALREADY_MAX, Color.red);
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                    return;
                }

                confirmButton.interactable = false;

                int requested = (int)quantitySlider.value;
                var result = await UpgradeFunctionHelper.UpgradeLevelAsync(
                    featureName,
                    currentLevel,
                    maxLevel,
                    requested,
                    User.CurrentUserId);

                if (result.Success)
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.LEVEL_UP_SOUND);

                    // 1. TÍNH TOÁN VÀ CẬP NHẬT DATABASE
                    userUpgrade = EnhanceHelper.EnhanceUpgrades(userUpgrade, result.UpgradedLevels, upgrade.BaseMultiplier);
                    await UserUpgradesService.Create().InsertOrUpdateUserUpgradesAsync(User.CurrentUserId, userUpgrade, stat);

                    // 2. QUERY LẠI DATABASE ĐỂ LẤY DỮ LIỆU CHUẨN NHẤT CỦA USER UPGRADE
                    userUpgrade = await UserUpgradesService.Create().GetUserUpgradesAsync(User.CurrentUserId, featureId, stat);

                    // Cập nhật lại currentLevel từ data mới nhất vừa fetch
                    currentLevel = userUpgrade?.CurrentLevel ?? 0;

                    // 3. Cập nhật Lực chiến
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                    // 4. Làm mới lại Main Panel ở dưới (sẽ query lại nguyên liệu theo level + 1 mới)
                    await RefreshMainPanelAsync();

                    // 5. Làm mới lại trạng thái Slider & Preview trên Popup
                    ResetPopupState();
                    await UpdatePreviewAsync();
                }
                else
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.ALERT_SOUND);
                    SetPreviewNotification(result.Message, Color.red);
                    confirmButton.interactable = true;
                }
            });

            popupCloseButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(popupGO);
            });
        }

        upgradeLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            CreatePopupUpgradePanel();
        });
    }

    private void SetupUpgradeItemUI(GameObject itemGO, RecipeItemDto data)
    {
        TextMeshProUGUI requiredText = itemGO.transform.Find("RequiredText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI ownedText = itemGO.transform.Find("AvailableText").GetComponent<TextMeshProUGUI>();
        RawImage image = itemGO.transform.Find("Image").GetComponent<RawImage>();

        requiredText.text = data.RequiredQuantity.ToString();
        ownedText.text = data.UserQuantity.ToString();

        if (data.UserQuantity < data.RequiredQuantity)
            ownedText.color = Color.red;
        else
            ownedText.color = Color.green;

        Texture texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(data.ItemImage));
        if (texture != null)
            image.texture = texture;
    }
}