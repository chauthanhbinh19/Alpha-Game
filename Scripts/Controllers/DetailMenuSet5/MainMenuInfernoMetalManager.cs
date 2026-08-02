using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;

public class MainMenuInfernoMetalManager : MonoBehaviour
{
    public static MainMenuInfernoMetalManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject RankPanelPrefab;
    private GameObject RankButtonPrefab;
    private GameObject PopupRankPanelPrefab;
    private GameObject PopupRankQuantityPanelPrefab;
    private GameObject PopupRankButtonPrefab;
    private GameObject MainRankPanelPrefab;
    private GameObject RankItemPrefab;
    private Transform Content;
    private const int ITEMS_PER_PAGE = 50;
    private int CurrentPage = 0;
    private List<KeyValuePair<string, FeatureRankDTO>> FeatureList;
    private IStats Stat;
    private PaginationManager PaginationManager;
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
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        RankPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.Rank.RANK_PANEL_PREFAB);
        RankButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Rank.RANK_BUTTON_PREFAB);
        PopupRankPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.Rank.POPUP_RANK_PANEL_PREFAB);
        PopupRankQuantityPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.Rank.POPUP_RANK_QUANTITY_PANEL_PREFAB);
        PopupRankButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Rank.POPUP_RANK_BUTTON_PREFAB);
        MainRankPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.Rank.MAIN_RANK_PANEL_PREFAB);
        RankItemPrefab = UIManager.Instance.Get(AppConstants.Prefab.Rank.RANK_ITEM_PREFAB);
    }
    public async Task CreateMainMenuInfernoMetalManagerAsync(IStats stat)
    {
        GameObject currentObject = Instantiate(PopupRankPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Content = transform.Find("Scroll View/Viewport/Content");
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
        Dictionary<string, FeatureRankDTO> uniqueTypes = new Dictionary<string, FeatureRankDTO>();
        uniqueTypes = await FeaturesService.Create().GetRankFeaturesByTypeAsync(AppConstants.MainMenuSet5.INFERNO_METAL, stat);
        uniqueTypes = uniqueTypes
            .OrderBy(kvp =>
            {
                var match = Regex.Match(kvp.Value.FeatureName, @"\d+$");
                return match.Success ? int.Parse(match.Value) : 0;
            })
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        FeatureList = uniqueTypes.ToList();
        CurrentPage = 0;
        Stat = stat;
        SetupPagination(currentObject);
        RenderPage();
    }

    private void RenderPage()
    {
        // 1. Dọn dẹp các Prefab UI cũ ở trang trước
        foreach (Transform child in Content)
            Destroy(child.gameObject);

        if (FeatureList == null || FeatureList.Count == 0) return;

        // 2. Tính toán dải Index phân trang Client-side
        int start = CurrentPage * ITEMS_PER_PAGE;
        int end = Mathf.Min(start + ITEMS_PER_PAGE, FeatureList.Count);

        int userLevel = User.CurrentUserLevel; // Cache level để tránh gọi Property nhiều lần trong vòng lặp

        for (int i = start; i < end; i++)
        {
            var kvp = FeatureList[i];

            // Tạo bản copy của giá trị để tránh lỗi bộ nhớ Closure trong Lambda Listener
            string currentSubtype = kvp.Key;
            int currentRequiredLevel = kvp.Value.RequiredLevel;
            string currentFeatureId = kvp.Value.Id;
            int displayIndex = i + 1;
            bool isLocked = currentRequiredLevel > userLevel;

            // Sinh đối tượng Prefab nút bấm
            GameObject button = Instantiate(PopupRankButtonPrefab, Content);
            Transform btnTransform = button.transform; // Cache transform của button

            // 3. Tối ưu tìm kiếm và gán Text (Dùng chuỗi format thay vì Replace trùng lặp)
            string processedText = currentSubtype.Replace("_", " ");

            TextMeshProUGUI buttonText = btnTransform.Find("ContentText")?.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null) buttonText.text = processedText;

            TextMeshProUGUI buttonText2 = btnTransform.Find("MainTitleText")?.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText2 != null) buttonText2.text = processedText;

            TextMeshProUGUI quantityText = btnTransform.Find("QuantityText")?.GetComponentInChildren<TextMeshProUGUI>();
            if (quantityText != null) quantityText.text = displayIndex.ToString();

            // 4. Xử lý trạng thái khóa/mở khóa cấp độ
            Transform warningLevel = btnTransform.Find("WarningLevel");
            if (warningLevel != null)
            {
                warningLevel.gameObject.SetActive(isLocked);
                if (isLocked)
                {
                    TextMeshProUGUI levelText = warningLevel.Find("LevelText")?.GetComponent<TextMeshProUGUI>();
                    if (levelText != null) levelText.text = currentRequiredLevel.ToString();
                }
            }

            // 5. Gán sự kiện click chuột an toàn (Safe Event Binding)
            Button btn = button.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners(); // Đảm bảo sạch listener
                btn.onClick.AddListener(async () =>
                {
                    if (isLocked)
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                        return;
                    }

                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await CreateMainRankPanelAsync(currentFeatureId, currentSubtype);
                });
            }
        }
    }

    private void SetupPagination(GameObject currentObject)
    {
        PaginationManager = currentObject.transform.Find("PaginationPanelPrefab")?.GetComponent<PaginationManager>();

        if (PaginationManager != null)
        {
            PaginationManager.OnPageChanged -= OnPageSelected;
            PaginationManager.OnPageChanged += OnPageSelected;

            // Vẽ dải nút số phân trang trên UI
            PaginationManager.InitPagination(FeatureList.Count, ITEMS_PER_PAGE, CurrentPage + 1);

            // QUAN TRỌNG: Vẽ luôn dữ liệu trang hiện tại lên khung Content khi vừa setup xong
            RenderPage();
        }
        else
        {
            Debug.LogError("Không tìm thấy component PaginationManager trong 'Pagination'!");
        }
    }

    private void OnPageSelected(int pageNumber)
    {
        CurrentPage = pageNumber - 1;
        RenderPage();
    }

    private void ResetOrUpdatePagination()
    {
        if (PaginationManager != null)
        {
            PaginationManager.OnPageChanged -= OnPageSelected;

            CurrentPage = 0;

            PaginationManager.InitPagination(FeatureList.Count, ITEMS_PER_PAGE, 1);

            // Vẽ lại dữ liệu của Trang 1 sau khi thực hiện filter/search thành công
            RenderPage();

            PaginationManager.OnPageChanged += OnPageSelected;
        }
    }

    public async Task CreateMainRankPanelAsync(string featureId, string featureName)
    {
        GameObject currentObject = Instantiate(MainRankPanelPrefab, MainPanel);
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

        RawImage mapImage = transform.Find("MapImage").GetComponent<RawImage>();
        Texture mapTexture = TextureHelper.LoadTexture2DCached("UI/Background2/Chapter_14");
        mapImage.texture = mapTexture; 
        RawImage rankImage = transform.Find("GroupBackground/RankImage").GetComponent<RawImage>();
        // Texture rankTexture = TextureHelper.LoadTexture2DCached($"UI/Rank_Research/{AppConstants.Rank.MASTER_OF_ATOMIC}");
        // rankImage.texture = rankTexture;
        RawImage background = transform.Find("Background").GetComponent<RawImage>();
        background.texture = TextureHelper.LoadTexture2DCached(ImageConstants.MainMenuSet5.INFERNO_METAL);

        AnimationController.Instance.CreateRankAnimation(currentObject);
        Ranks rank = await RanksService.Create().GetRankByIdAsync(featureId);
        UserRanks userRank = await UserRanksService.Create().GetUserRanksAsync(User.CurrentUserId, featureId);
        List<RecipeItemDto> recipeItems = await RecipeService.Create().GetRecipeItemsAsync(featureName, userRank.Level, User.CurrentUserId);

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

            GameObject itemGO = Instantiate(RankItemPrefab, parent);

            SetupRankItemUI(itemGO, recipeItems[i]);
        }

        int currentLevel = userRank?.Level ?? 0;
        levelText.text = currentLevel.ToString();
        async Task RefreshPanelAsync()
        {
            userRank = await UserRanksService.Create().GetUserRanksAsync(User.CurrentUserId, featureId);
            currentLevel = userRank?.Level ?? 0;
            levelText.text = currentLevel.ToString();

            List<RecipeItemDto> refreshedRecipeItems = await RecipeService.Create().GetRecipeItemsAsync(featureName, userRank.Level, User.CurrentUserId);
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

                GameObject itemGO = Instantiate(RankItemPrefab, parent);
                SetupRankItemUI(itemGO, refreshedRecipeItems[i]);
            }
        }


        // Popup that allows upgrading multiple levels (wired to UpgradeLevelButton)
        void CreatePopupUpgradePanelAsync()
        {
            GameObject gameObject =
                Instantiate(PopupRankQuantityPanelPrefab, MainPanel);

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
            Transform currentStatsContent = panelTransform.Find("Scroll View/Viewport/Content/CurrentStats");
            Transform nextStatsContent = panelTransform.Find("Scroll View/Viewport/Content/NextStats");

            int popupCurrentLevel = currentLevel;
            int maxLevel = rank != null ? rank.MaxLevel : popupCurrentLevel;
            int maxPossible = Mathf.Max(0, maxLevel - popupCurrentLevel);

            currentLevelText.text = popupCurrentLevel.ToString();
            nextLevelText.text = (popupCurrentLevel + 1).ToString();

            if (userRank != null)
            {
                StatsManager.Instance.CreateStatsManager(userRank, currentStatsContent);
                StatsManager.Instance.CreateStatsManager(userRank, nextStatsContent);
            }

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

                    if (userRank != null)
                        StatsManager.Instance.CreateStatsManager(userRank, nextStatsContent);

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

                if (preview.UpgradedLevels > 0)
                {
                    UserRanks previewRank = userRank.CloneUserRank(userRank);
                    EnhanceHelper.EnhanceRanks(previewRank, preview.UpgradedLevels, rank.BaseMultiplier);
                    StatsManager.Instance.CreateStatsManager(previewRank, nextStatsContent);
                }
                else if (userRank != null)
                {
                    StatsManager.Instance.CreateStatsManager(userRank, nextStatsContent);
                }

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
                    userRank = EnhanceHelper.EnhanceRanks(userRank, result.UpgradedLevels, rank.BaseMultiplier);
                    await UserRanksService.Create().InsertOrUpdateUserRanksAsync(User.CurrentUserId, userRank, featureId, Stat);

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

    private void SetupRankItemUI(GameObject itemGO, RecipeItemDto data)
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
