using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SSWNIManager : MonoBehaviour
{
    public static SSWNIManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject SSWNPanelPrefab;
    private GameObject SSWNButtonPrefab;
    private GameObject PopupSSWNPanelPrefab;
    private GameObject PopupSSWNQuantityPanelPrefab;
    private GameObject PopupSSWNButtonPrefab;
    private GameObject MainSSWNPanelPrefab;
    private GameObject SSWNItemPrefab;
    private Transform content;
    private const int ITEMS_PER_PAGE = 50;
    private int _currentPage = 0;
    private List<KeyValuePair<string, FeatureSSWNDTO>> _featureList;
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
        SSWNPanelPrefab = UIManager.Instance.Get("SSWNPanelPrefab");
        SSWNButtonPrefab = UIManager.Instance.Get("SSWNButtonPrefab");
        PopupSSWNPanelPrefab = UIManager.Instance.Get("PopupSSWNPanelPrefab");
        PopupSSWNQuantityPanelPrefab = UIManager.Instance.Get("PopupSSWNQuantityPanelPrefab");
        PopupSSWNButtonPrefab = UIManager.Instance.Get("PopupSSWNButtonPrefab");
        MainSSWNPanelPrefab = UIManager.Instance.Get("MainSSWNPanelPrefab");
        SSWNItemPrefab = UIManager.Instance.Get("SSWNItemPrefab");
    }
    public async Task CreateSSWNIManagerAsync()
    {
        GameObject currentObject = Instantiate(PopupSSWNPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        content = transform.Find("Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener( () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            
        });
        Dictionary<string, FeatureSSWNDTO> uniqueTypes = new Dictionary<string, FeatureSSWNDTO>();
        uniqueTypes = await FeaturesService.Create().GetSSWNFeaturesByTypeAsync(AppConstants.SSWN.SSWN_I);
        uniqueTypes = uniqueTypes
            .OrderBy(kvp =>
            {
                var match = Regex.Match(kvp.Value.FeatureName, @"\d+$");
                return match.Success ? int.Parse(match.Value) : 0;
            })
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        _featureList = uniqueTypes.ToList();
        _currentPage = 0;
        SetupPagination(currentObject);
        RenderPage();
    }
    
    private void RenderPage()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        int start = _currentPage * ITEMS_PER_PAGE;
        int end = Mathf.Min(start + ITEMS_PER_PAGE, _featureList.Count);

        for (int i = start; i < end; i++)
        {
            var kvp = _featureList[i];

            string subtype = kvp.Key;
            int requiredLevel = kvp.Value.RequiredLevel;
            string featureId = kvp.Value.Id;

            GameObject button = Instantiate(PopupSSWNButtonPrefab, content);

            TextMeshProUGUI buttonText =
                button.transform.Find("ContentText")
                .GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = subtype.Replace("_", " ");

            TextMeshProUGUI buttonText2 =
                button.transform.Find("MainTitleText")
                .GetComponentInChildren<TextMeshProUGUI>();

            buttonText2.text = subtype.Replace("_", " ");

            TextMeshProUGUI quantityText =
                button.transform.Find("QuantityText")
                .GetComponentInChildren<TextMeshProUGUI>();

            quantityText.text = (i + 1).ToString();

            bool isLocked = requiredLevel > User.CurrentUserLevel;

            Transform warningLevel = button.transform.Find("WarningLevel");
            if (warningLevel != null)
            {
                warningLevel.gameObject.SetActive(isLocked);
                TextMeshProUGUI levelText = button.transform.Find("WarningLevel/LevelText").GetComponent<TextMeshProUGUI>();
                levelText.text = requiredLevel.ToString();
            }

            Button btn = button.GetComponent<Button>();
            btn.onClick.AddListener(async () =>
            {
                if (isLocked)
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                    return;
                }

                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await CreateMainSSWNPanelAsync(featureId, subtype);
            });
        }
    }
    
    private void SetupPagination(GameObject currentObject)
    {
        Transform transform = currentObject.transform;
        nextButton = transform
            .Find("Pagination/Next")
            .GetComponent<Button>();

        previousButton = transform
            .Find("Pagination/Previous")
            .GetComponent<Button>();

        pageText = transform
            .Find("Pagination/Page")
            .GetComponent<TextMeshProUGUI>();

        nextButton.onClick.RemoveAllListeners();
        previousButton.onClick.RemoveAllListeners();

        nextButton.onClick.AddListener(OnNextPage);
        previousButton.onClick.AddListener(OnPreviousPage);

        UpdatePageUI();
    }

    private int GetTotalPages()
    {
        if (_featureList == null || _featureList.Count == 0)
            return 1;

        return Mathf.CeilToInt((float)_featureList.Count / ITEMS_PER_PAGE);
    }

    private void UpdatePageUI()
    {
        int totalPages = GetTotalPages();

        pageText.text = $"{_currentPage + 1} / {totalPages}";

        previousButton.interactable = _currentPage > 0;
        nextButton.interactable = _currentPage < totalPages - 1;
    }

    private void OnNextPage()
    {
        int totalPages = GetTotalPages();

        if (_currentPage >= totalPages - 1)
            return;

        _currentPage++;
        RenderPage();
        UpdatePageUI();
    }

    private void OnPreviousPage()
    {
        if (_currentPage <= 0)
            return;

        _currentPage--;
        RenderPage();
        UpdatePageUI();
    }

    public async Task CreateMainSSWNPanelAsync(string featureId, string featureName)
    {
        GameObject currentObject = Instantiate(MainSSWNPanelPrefab, MainPanel);
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
        homeButton.onClick.AddListener( () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            
        });
        RawImage mapImage = transform.Find("MapImage").GetComponent<RawImage>();
        Texture mapTexture = TextureHelper.LoadTexture2DCached("UI/Background2/Chapter_14");
        mapImage.texture = mapTexture; 
        RawImage rankImage = transform.Find("GroupBackground/RankImage").GetComponent<RawImage>();
        Texture rankTexture = TextureHelper.LoadTexture2DCached($"UI/Rank_Research/{AppConstants.SSWN.SSWN_I}");
        rankImage.texture = rankTexture;
        RawImage background = transform.Find("Background").GetComponent<RawImage>();
        background.texture = TextureHelper.LoadTexture2DCached(ImageConstants.SSWN.SSWN_I_BACKGROUND_URL);

        SSWNs sswn = await SSWNsService.Create().GetSSWNByIdAsync(featureId);
        List<RecipeItemDto> recipeItems = await RecipeService.Create().GetRecipeItemsAsync(featureName, User.CurrentUserLevel, User.CurrentUserId);
        UserSSWNs userSSWN = await UserSSWNsService.Create().GetUserSSWNsAsync(featureId);

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

            GameObject itemGO = Instantiate(SSWNItemPrefab, parent);

            SetupSSWNItemUI(itemGO, recipeItems[i]);
        }

        int currentLevel = userSSWN?.Level ?? 0;
        levelText.text = currentLevel.ToString();

        async Task RefreshPanelAsync()
        {
            userSSWN = await UserSSWNsService.Create().GetUserSSWNsAsync(featureId);
            currentLevel = userSSWN?.Level ?? 0;
            levelText.text = currentLevel.ToString();

            List<RecipeItemDto> refreshedRecipeItems = await RecipeService.Create().GetRecipeItemsAsync(featureName, User.CurrentUserLevel, User.CurrentUserId);
            if (refreshedRecipeItems == null)
                return;

            foreach (Transform child in leftSideContent)
                Destroy(child.gameObject);
            foreach (Transform child in rightSideContent)
                Destroy(child.gameObject);

            int refreshedTotal = refreshedRecipeItems.Count;
            int refreshedLeftCount = Mathf.CeilToInt(refreshedTotal / 2f);

            for (int i = 0; i < refreshedTotal; i++)
            {
                Transform parent = (i < refreshedLeftCount)
                    ? leftSideContent
                    : rightSideContent;

                GameObject itemGO = Instantiate(SSWNItemPrefab, parent);
                SetupSSWNItemUI(itemGO, refreshedRecipeItems[i]);
            }
        }


        // Popup that allows upgrading multiple levels (wired to UpgradeLevelButton)
        void CreatePopupUpgradePanelAsync()
        {
            GameObject gameObject =
                Instantiate(PopupSSWNQuantityPanelPrefab, MainPanel);

            Transform panelTransform = gameObject.transform;

            TextMeshProUGUI currentLevelText = panelTransform.Find("CurrentLevel").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nextLevelText = panelTransform.Find("NextLevel").GetComponent<TextMeshProUGUI>();
            Slider quantitySlider = panelTransform.Find("QuantitySlider").GetComponent<Slider>();
            // TextMeshProUGUI userItemQuantityText = panelTransform.Find("UserItemQuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemUsedQuantityText = panelTransform.Find("ItemUsedQuantityText").GetComponent<TextMeshProUGUI>();
            // RawImage userItemImage = panelTransform.Find("UserItemImage").GetComponent<RawImage>();
            // RawImage itemUsedImage = panelTransform.Find("ItemUsedImage").GetComponent<RawImage>();
            TextMeshProUGUI notificationText = panelTransform.Find("Notification/ContentText").GetComponent<TextMeshProUGUI>();
            Button increaseOneButton = panelTransform.Find("IncreaseOneButton").GetComponent<Button>();
            Button increaseTenButton = panelTransform.Find("IncreaseTenButton").GetComponent<Button>();
            Button increaseMaxButton = panelTransform.Find("IncreaseMaxButton").GetComponent<Button>();
            Button decreaseOneButton = panelTransform.Find("DecreaseOneButton").GetComponent<Button>();
            Button decreaseTenButton = panelTransform.Find("DecreaseTenButton").GetComponent<Button>();
            Button decreaseMaxButton = panelTransform.Find("DecreaseMaxButton").GetComponent<Button>();
            Button confirmButton = panelTransform.Find("ConfirmButton").GetComponent<Button>();
            Button closeButton = panelTransform.Find("CloseButton").GetComponent<Button>();

            int popupCurrentLevel = currentLevel;
            int maxLevel = sswn != null ? sswn.MaxLevel : popupCurrentLevel;
            int maxPossible = Mathf.Max(0, maxLevel - popupCurrentLevel);

            currentLevelText.text = popupCurrentLevel.ToString();
            nextLevelText.text = (popupCurrentLevel + 1).ToString();

            quantitySlider.minValue = 1;
            quantitySlider.maxValue = Mathf.Max(1, maxPossible);
            quantitySlider.wholeNumbers = true;
            quantitySlider.value = 1;

            void SetPreviewNotification(string value, Color color)
            {
                notificationText.text = LocalizationManager.Get(value);
                notificationText.color = color;
            }

            async void UpdatePreview()
            {
                await UpdatePreviewAsync();
            }

            async Task UpdatePreviewAsync()
            {
                int requested = (int)quantitySlider.value;

                if (maxPossible <= 0)
                {
                    var backgroundImage = confirmButton.transform.Find("Background2")?.GetComponent<RawImage>();
                    if (backgroundImage != null)
                        backgroundImage.color = Color.gray;

                    SetPreviewNotification(MessageConstants.UPGRADE_ALREADY_MAX, Color.red);
                    nextLevelText.text = "MAX";
                    confirmButton.interactable = true;
                    itemUsedQuantityText.text = "0";
                    return;
                }

                var preview = await UpgradeFunctionHelper.PreviewUpgradeAsync(
                    featureName,
                    popupCurrentLevel,
                    maxLevel,
                    requested,
                    User.CurrentUserId);

                if (!preview.Success)
                {
                    SetPreviewNotification(preview.Message, Color.red);
                    confirmButton.interactable = false;
                    nextLevelText.text = preview.TargetLevel.ToString();
                    itemUsedQuantityText.text = "0";
                    // userItemQuantityText.text = "0";
                    return;
                }

                nextLevelText.text = preview.TargetLevel.ToString();
                confirmButton.interactable = preview.UpgradedLevels > 0;

                bool hasEnough = true;
                if (preview.RequiredItems != null && preview.RequiredItems.Count > 0)
                {
                    var first = preview.RequiredItems.First();
                    string firstItemId = first.Key;
                    double requiredQty = first.Value;

                    var recipeLevelItems = await RecipeService.Create()
                        .GetRecipeItemsAsync(featureName, popupCurrentLevel + 1, User.CurrentUserId);

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
                    // userItemQuantityText.text = owned.ToString();

                    if (owned < requiredQty)
                    {
                        hasEnough = false;
                    }

                    Texture tex = null;
                    if (!string.IsNullOrEmpty(imagePath))
                        tex = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(imagePath));

                    if (tex != null)
                    {
                        // itemUsedImage.texture = tex;
                        // userItemImage.texture = tex;
                    }
                }
                else
                {
                    itemUsedQuantityText.text = "0";
                    // userItemQuantityText.text = "0";
                }

                if (preview.UpgradedLevels > 0 && hasEnough)
                {
                    SetPreviewNotification(MessageConstants.READY_TO_UPGRADE, Color.green);
                }
                else
                {
                    SetPreviewNotification(MessageConstants.NOT_ENOUGH_MATERIALS, Color.red);
                    confirmButton.interactable = false;
                }
            }

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
                quantitySlider.SetValueWithoutNotify(quantitySlider.maxValue);
                await UpdatePreviewAsync();
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

            UpdatePreview();

            confirmButton.onClick.AddListener(async () =>
            {
                if (popupCurrentLevel >= maxLevel)
                {
                    notificationText.text = MessageConstants.UPGRADE_ALREADY_MAX;
                    notificationText.color = Color.red;
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                    return;
                }

                AudioManager.Instance.PlaySFX(AudioConstants.SFX.LEVEL_UP_SOUND);

                int requested = (int)quantitySlider.value;
                var result = await UpgradeFunctionHelper.UpgradeLevelAsync(
                    featureName,
                    popupCurrentLevel,
                    maxLevel,
                    requested,
                    User.CurrentUserId);

                if (result.Success)
                {
                    userSSWN = EnhanceHelper.EnhanceSSWNs(userSSWN, result.UpgradedLevels, sswn.BaseMultiplier);
                    await UserSSWNsService.Create().InsertOrUpdateUserSSWNsAsync(User.CurrentUserId, userSSWN, featureId);

                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                    Destroy(gameObject);
                    await RefreshPanelAsync();
                }
                else
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.ALERT_SOUND);
                    notificationText.text = result.Message;
                }
            });

            closeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(gameObject);
            });
        }

        upgradeLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            CreatePopupUpgradePanelAsync();
        });
    }
    
    private void SetupSSWNItemUI(GameObject itemGO,RecipeItemDto data)
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