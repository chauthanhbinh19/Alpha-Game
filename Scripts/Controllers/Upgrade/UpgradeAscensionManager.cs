using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAscensionManager : MonoBehaviour
{
    public static UpgradeAscensionManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject MainUpgradePanelPrefab;
    private GameObject UniverseButtonPrefab;
    private GameObject PopupUpgradeQuantityPanelPrefab;
    private GameObject PopupUniverseButtonPrefab;
    private GameObject MainUniversePanelPrefab;
    private GameObject UniverseItemPrefab;
    private Transform content;
    private const int ITEMS_PER_PAGE = 50;
    private int _currentPage = 0;
    private FeatureUpgradeDTO feature;
    private Button nextButton;
    private Button previousButton;
    private TextMeshProUGUI pageText;
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
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MainUpgradePanelPrefab = UIManager.Instance.Get("MainUpgradePanelPrefab");
        UniverseButtonPrefab = UIManager.Instance.Get("UniverseButtonPrefab");
        PopupUpgradeQuantityPanelPrefab = UIManager.Instance.Get("PopupUpgradeQuantityPanelPrefab");
        PopupUniverseButtonPrefab = UIManager.Instance.Get("PopupUniverseButtonPrefab");
        MainUniversePanelPrefab = UIManager.Instance.Get("MainUniversePanelPrefab");
        UniverseItemPrefab = UIManager.Instance.Get("UniverseItemPrefab");
    }
    public async Task CreateUpgradeAscensionManagerAsync(IStats stat)
    {
        GameObject currentObject = Instantiate(MainUpgradePanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        content = transform.Find("Scroll View/Viewport/Content");
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

        feature = (await FeaturesService.Create().GetUpgradeFeaturesByTypeAsync(AppConstants.Upgrade.UPGRADE_ASCENSION, stat))
                .Values
                .FirstOrDefault();
        _currentPage = 0;

        // Universes universe = await UniversesService.Create().GetUniverseByIdAsync(featureId);
        List<RecipeItemDto> recipeItems = await RecipeService.Create().GetRecipeItemsAsync(feature.FeatureName, User.CurrentUserLevel, User.CurrentUserId);

        if (recipeItems == null || recipeItems.Count == 0)
            return;

        // Xoá item cũ nếu có
        foreach (Transform child in leftSideContent)
            Destroy(child.gameObject);

        foreach (Transform child in rightSideContent)
            Destroy(child.gameObject);

        int total = recipeItems.Count;
        int leftCount = Mathf.CeilToInt(total / 2f);

        for (int i = 0; i < total; i++)
        {
            Transform parent = (i < leftCount)
                ? leftSideContent
                : rightSideContent;

            GameObject itemGO = Instantiate(UniverseItemPrefab, parent);

            SetupUniverseItemUI(itemGO, recipeItems[i]);
        }

        Button upgradeLevelButton = transform.Find("UpgradeLevelButton").GetComponent<Button>();
        upgradeLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            CreatePopupUpgradePanelAsync();
        });
    }
    public void CreatePopupUpgradePanelAsync()
    {
        GameObject gameObject =
            Instantiate(PopupUpgradeQuantityPanelPrefab, MainPanel);

        Transform panelTransform = gameObject.transform;

        // --- UI ---
        TextMeshProUGUI currentLevelText = panelTransform.Find("CurrentLevel").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nextLevelText = panelTransform.Find("NextLevel").GetComponent<TextMeshProUGUI>();
        Slider quantitySlider = panelTransform.Find("QuantitySlider").GetComponent<Slider>();
        TextMeshProUGUI userItemQuantityText = panelTransform.Find("UserItemQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemUsedQuantityText = panelTransform.Find("ItemUsedQuantityText").GetComponent<TextMeshProUGUI>();
        RawImage userItemImage = panelTransform.Find("UserItemImage").GetComponent<RawImage>();
        RawImage itemUsedImage = panelTransform.Find("ItemUsedImage").GetComponent<RawImage>();
        TextMeshProUGUI notificationText = panelTransform.Find("Notification/ContentText").GetComponent<TextMeshProUGUI>();
        Button increaseOneButton = panelTransform.Find("IncreaseOneButton").GetComponent<Button>();
        Button increaseTenButton = panelTransform.Find("IncreaseTenButton").GetComponent<Button>();
        Button increaseMaxButton = panelTransform.Find("IncreaseMaxButton").GetComponent<Button>();
        Button decreaseOneButton = panelTransform.Find("DecreaseOneButton").GetComponent<Button>();
        Button decreaseTenButton = panelTransform.Find("DecreaseTenButton").GetComponent<Button>();
        Button decreaseMaxButton = panelTransform.Find("DecreaseMaxButton").GetComponent<Button>();
        Button confirmButton = panelTransform.Find("ConfirmButton").GetComponent<Button>();
        Button closeButton = panelTransform.Find("CloseButton").GetComponent<Button>();

        int currentLevel = User.CurrentUserLevel;
        int maxLevel = feature != null ? feature.MaxLevel : currentLevel;
        int maxPossible = Mathf.Max(0, maxLevel - currentLevel);

        currentLevelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();

        quantitySlider.minValue = 1;
        quantitySlider.maxValue = Mathf.Max(1, maxPossible);
        quantitySlider.wholeNumbers = true;
        quantitySlider.value = 1;

        // Helper to update preview UI
        async void UpdatePreview()
        {
            int requested = (int)quantitySlider.value;

            if (maxPossible <= 0)
            {
                notificationText.text = MessageConstants.MAX_LEVEL_REACHED;
                confirmButton.interactable = false;
                itemUsedQuantityText.text = "0";
                userItemQuantityText.text = "0";
                return;
            }

            var preview = await UpgradeFunctionHelper.PreviewUpgradeAsync(
                feature.FeatureName,
                currentLevel,
                maxLevel,
                requested,
                User.CurrentUserId);

            if (!preview.Success)
            {
                notificationText.text = preview.Message;
                confirmButton.interactable = false;
                nextLevelText.text = preview.TargetLevel.ToString();
                itemUsedQuantityText.text = "0";
                userItemQuantityText.text = "0";
                return;
            }

            notificationText.text = string.Empty;
            nextLevelText.text = preview.TargetLevel.ToString();
            confirmButton.interactable = preview.UpgradedLevels > 0;

            // Show summed required quantity and owned quantity for the first required item if available
            if (preview.RequiredItems != null && preview.RequiredItems.Count > 0)
            {
                var first = preview.RequiredItems.First();
                string firstItemId = first.Key;
                double requiredQty = first.Value;

                // Try to get user's current quantity for that item from recipe level +1
                var recipeLevelItems = await RecipeService.Create()
                    .GetRecipeItemsAsync(feature.FeatureName, currentLevel + 1, User.CurrentUserId);

                double owned = 0;
                string imagePath = null;
                if (recipeLevelItems != null)
                {
                    var match = recipeLevelItems.FirstOrDefault(x => x.ItemId == firstItemId);
                    if (match != null)
                    {
                        owned = match.UserQuantity;
                        imagePath = match.ItemImage;
                    }
                }

                itemUsedQuantityText.text = requiredQty.ToString();
                userItemQuantityText.text = owned.ToString();

                Texture tex = null;
                if (!string.IsNullOrEmpty(imagePath))
                    tex = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(imagePath));

                if (tex != null)
                {
                    itemUsedImage.texture = tex;
                    userItemImage.texture = tex;
                }
            }
            else
            {
                itemUsedQuantityText.text = "0";
                userItemQuantityText.text = "0";
            }
        }

        // Wire slider and buttons
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
        increaseMaxButton.onClick.AddListener(() =>
        {
            quantitySlider.value = quantitySlider.maxValue;
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });

        decreaseOneButton.onClick.AddListener(() =>
        {
            quantitySlider.value = Mathf.Max(quantitySlider.minValue, quantitySlider.value - 1);
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        decreaseTenButton.onClick.AddListener(() =>
        {
            quantitySlider.value = Mathf.Max(quantitySlider.minValue, quantitySlider.value - 10);
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });
        decreaseMaxButton.onClick.AddListener(() =>
        {
            quantitySlider.value = quantitySlider.minValue;
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
        });

        // Initial preview
        UpdatePreview();

        confirmButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            int requested = (int)quantitySlider.value;
            var result = await UpgradeFunctionHelper.UpgradeLevelAsync(
                feature.FeatureName,
                currentLevel,
                maxLevel,
                requested,
                User.CurrentUserId);

            if (result.Success)
            {
                Destroy(gameObject);
                // Optionally refresh parent UI elsewhere
            }
            else
            {
                notificationText.text = result.Message;
            }
        });

        closeButton.onClick.AddListener(() => Destroy(gameObject));
    }

    public async Task CreateMainUniversePanelAsync(string featureId, string featureName)
    {
        GameObject currentObject = Instantiate(MainUniversePanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Button upgradeOneLevelButton = transform.Find("UpgradeOneLevelButton").GetComponent<Button>();
        Button upgradeMaxLevelButton = transform.Find("UpgradeMaxLevelButton").GetComponent<Button>();
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

        Universes universe = await UniversesService.Create().GetUniverseByIdAsync(featureId);
        List<RecipeItemDto> recipeItems = await RecipeService.Create().GetRecipeItemsAsync(featureName, User.CurrentUserLevel, User.CurrentUserId);
        UserUniverses userUniverse = await UserUniversesService.Create().GetUserUniversesAsync(featureId);

        if (recipeItems == null || recipeItems.Count == 0)
            return;

        // Xoá item cũ nếu có
        foreach (Transform child in leftSideContent)
            Destroy(child.gameObject);

        foreach (Transform child in rightSideContent)
            Destroy(child.gameObject);

        int total = recipeItems.Count;
        int leftCount = Mathf.CeilToInt(total / 2f);

        for (int i = 0; i < total; i++)
        {
            Transform parent = (i < leftCount)
                ? leftSideContent
                : rightSideContent;

            GameObject itemGO = Instantiate(UniverseItemPrefab, parent);

            SetupUniverseItemUI(itemGO, recipeItems[i]);
        }

        int currentLevel = userUniverse?.Level ?? 0;
        levelText.text = currentLevel.ToString();

        upgradeOneLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            UpgradeResultDTO result = await UpgradeService.Create().UpgradeOneLevelAsync(featureName, currentLevel, universe.MaxLevel, User.CurrentUserId);
            if (result.Success)
            {
                userUniverse = EnhanceHelper.EnhanceUniverses(userUniverse, result.UpgradedLevels, universe.BaseMultiplier);
                await UserUniversesService.Create().InsertOrUpdateUserUniversesAsync(User.CurrentUserId, userUniverse, featureId);
                Destroy(currentObject);

                double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                await CreateMainUniversePanelAsync(featureId, featureName);
            }
            else
            {
                Debug.Log(result.Message);
            }
        });
        upgradeMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            UpgradeResultDTO result = await UpgradeService.Create().UpgradeMaxLevelAsync(featureName, currentLevel, universe.MaxLevel, User.CurrentUserId);
            if (result.Success)
            {
                userUniverse = EnhanceHelper.EnhanceUniverses(userUniverse, result.UpgradedLevels, universe.BaseMultiplier);
                await UserUniversesService.Create().InsertOrUpdateUserUniversesAsync(User.CurrentUserId, userUniverse, featureId);
                Destroy(currentObject);

                double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                await CreateMainUniversePanelAsync(featureId, featureName);
            }
            else
            {
                Debug.Log(result.Message);
            }
        });
    }

    private void SetupUniverseItemUI(GameObject itemGO, RecipeItemDto data)
    {
        // TextMeshProUGUI nameText =
        //     itemGO.transform.Find("ItemName")
        //     .GetComponent<TextMeshProUGUI>();

        TextMeshProUGUI requiredText =
            itemGO.transform.Find("RequiredText")
            .GetComponent<TextMeshProUGUI>();

        TextMeshProUGUI ownedText =
            itemGO.transform.Find("AvailableText")
            .GetComponent<TextMeshProUGUI>();

        RawImage image =
            itemGO.transform.Find("Image")
            .GetComponent<RawImage>();

        // nameText.text = data.ItemId;

        requiredText.text = data.RequiredQuantity.ToString();
        ownedText.text = data.UserQuantity.ToString();

        // Nếu thiếu nguyên liệu -> đổi màu
        if (data.UserQuantity < data.RequiredQuantity)
            ownedText.color = Color.red;
        else
            ownedText.color = Color.green;

        // Load icon nếu có
        Texture texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(data.ItemImage));
        if (texture != null)
            image.texture = texture;
    }

}