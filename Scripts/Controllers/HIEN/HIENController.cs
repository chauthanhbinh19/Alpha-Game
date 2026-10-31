using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HIENController : MonoBehaviour
{
    public static HIENController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject HIENPanelPrefab;
    private GameObject HIENButtonPrefab;
    private GameObject PopupHIENPanelPrefab;
    private GameObject PopupHIENQuantityPanelPrefab;
    private GameObject PopupHIENButtonPrefab;
    private GameObject MainHIENPanelPrefab;
    private GameObject HIENItemPrefab;
    private GameObject HIENItemPopupPrefab;
    private Transform Content;
    private const int ITEMS_PER_PAGE = 50;
    private int CurrentPage = 0;
    private List<KeyValuePair<string, FeatureHIENDTO>> FeatureList;
    private PaginationManager PaginationManager;
    private string Type = "";
    private string BackgroundImage = "";
    private string MainImage = "";
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
        HIENPanelPrefab = UIManager.Instance.Get(PrefabConstants.HIEN.HIEN_PANEL_PREFAB);
        HIENButtonPrefab = UIManager.Instance.Get(PrefabConstants.HIEN.HIEN_BUTTON_PREFAB);
        PopupHIENPanelPrefab = UIManager.Instance.Get(PrefabConstants.HIEN.POPUP_HIEN_PANEL_PREFAB);
        PopupHIENQuantityPanelPrefab = UIManager.Instance.Get(PrefabConstants.HIEN.POPUP_HIEN_QUANTITY_PANEL_PREFAB);
        PopupHIENButtonPrefab = UIManager.Instance.Get(PrefabConstants.HIEN.POPUP_HIEN_BUTTON_PREFAB);
        MainHIENPanelPrefab = UIManager.Instance.Get(PrefabConstants.HIEN.MAIN_HIEN_PANEL_PREFAB);
        HIENItemPrefab = UIManager.Instance.Get(PrefabConstants.HIEN.HIEN_ITEM_PREFAB);
        HIENItemPopupPrefab = UIManager.Instance.Get(PrefabConstants.HIEN.HIEN_ITEM_POPUP_PREFAB);
    }
    public async Task GetHIENAsync(string type)
    {
        switch (type)
        {
            case AppConstants.HIEN.HIEN_I:
                Type = type;
                BackgroundImage = ImageConstants.HIEN.HIEN_I_BACKGROUND_URL;
                MainImage = ImageConstants.HIEN.HIEN_I_MAIN_IMAGE_URL;
                await CreateHIENControllerAsync();
                break;
            case AppConstants.HIEN.HIEN_II:
                Type = type;
                BackgroundImage = ImageConstants.HIEN.HIEN_II_BACKGROUND_URL;
                MainImage = ImageConstants.HIEN.HIEN_II_MAIN_IMAGE_URL;
                await CreateHIENControllerAsync();
                break;
            case AppConstants.HIEN.HIEN_III:
                Type = type;
                BackgroundImage = ImageConstants.HIEN.HIEN_III_BACKGROUND_URL;
                MainImage = ImageConstants.HIEN.HIEN_III_MAIN_IMAGE_URL;
                await CreateHIENControllerAsync();
                break;
            case AppConstants.HIEN.HIEN_IV:
                Type = type;
                BackgroundImage = ImageConstants.HIEN.HIEN_IV_BACKGROUND_URL;
                MainImage = ImageConstants.HIEN.HIEN_IV_MAIN_IMAGE_URL;
                await CreateHIENControllerAsync();
                break;
            case AppConstants.HIEN.HIEN_V:
                Type = type;
                BackgroundImage = ImageConstants.HIEN.HIEN_V_BACKGROUND_URL;
                MainImage = ImageConstants.HIEN.HIEN_V_MAIN_IMAGE_URL;
                await CreateHIENControllerAsync();
                break;
            case AppConstants.HIEN.HIEN_VI:
                Type = type;
                BackgroundImage = ImageConstants.HIEN.HIEN_VI_BACKGROUND_URL;
                MainImage = ImageConstants.HIEN.HIEN_VI_MAIN_IMAGE_URL;
                await CreateHIENControllerAsync();
                break;
            case AppConstants.HIEN.HIEN_VII:
                Type = type;
                BackgroundImage = ImageConstants.HIEN.HIEN_VII_BACKGROUND_URL;
                MainImage = ImageConstants.HIEN.HIEN_VII_MAIN_IMAGE_URL;
                await CreateHIENControllerAsync();
                break;
            case AppConstants.HIEN.HIEN_VIII:
                Type = type;
                BackgroundImage = ImageConstants.HIEN.HIEN_VIII_BACKGROUND_URL;
                MainImage = ImageConstants.HIEN.HIEN_VIII_MAIN_IMAGE_URL;
                await CreateHIENControllerAsync();
                break;
            case AppConstants.HIEN.HIEN_IX:
                Type = type;
                BackgroundImage = ImageConstants.HIEN.HIEN_IX_BACKGROUND_URL;
                MainImage = ImageConstants.HIEN.HIEN_IX_MAIN_IMAGE_URL;
                await CreateHIENControllerAsync();
                break;
            case AppConstants.HIEN.HIEN_X:
                Type = type;
                BackgroundImage = ImageConstants.HIEN.HIEN_X_BACKGROUND_URL;
                MainImage = ImageConstants.HIEN.HIEN_X_MAIN_IMAGE_URL;
                await CreateHIENControllerAsync();
                break;
            default:
                break;
        }
    }
    public async Task CreateHIENControllerAsync()
    {
        GameObject currentObject = Instantiate(PopupHIENPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Content = transform.Find("Scroll View/Viewport/Content");
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
        Dictionary<string, FeatureHIENDTO> uniqueTypes = new Dictionary<string, FeatureHIENDTO>();
        uniqueTypes = await FeaturesService.Create().GetHIENFeaturesByTypeAsync(Type);
        uniqueTypes = uniqueTypes
            .OrderBy(kvp =>
            {
                var match = Regex.Match(kvp.Value.FeatureName, @"\d+$");
                return match.Success ? int.Parse(match.Value) : 0;
            })
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        FeatureList = uniqueTypes.ToList();
        CurrentPage = 0;
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
            GameObject button = Instantiate(PopupHIENButtonPrefab, Content);
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
                    await CreateMainHIENPanelAsync(currentFeatureId, currentSubtype);
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

    public async Task CreateMainHIENPanelAsync(string featureId, string featureName)
    {
        GameObject currentObject = Instantiate(MainHIENPanelPrefab, MainPanel);
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
        Texture rankTexture = TextureHelper.LoadTexture2DCached($"UI/Rank_Research/{Type}");
        rankImage.texture = rankTexture;

        RawImage background = transform.Find("Background").GetComponent<RawImage>();
        background.texture = TextureHelper.LoadTexture2DCached(BackgroundImage);

        RawImage mainImage = transform.Find("MainImage").GetComponent<RawImage>();
        mainImage.texture = TextureHelper.LoadTexture2DCached(MainImage);

        AnimationController.Instance.CreateHIENAnimation(currentObject);

        HIENs hien = await HIENsService.Create().GetHIENByIdAsync(featureId);
        UserHIENs userHIEN = await UserHIENsService.Create().GetUserHIENsAsync(User.CurrentUserId, featureId);
        
        int currentLevel = userHIEN?.Level ?? 0;
        RecipeResponseDTO recipeData = await RecipeService.Create().GetRecipeItemsAsync(featureName, currentLevel + 1, User.CurrentUserId);

        if (recipeData == null || recipeData.Recipes == null || recipeData.Recipes.Count == 0)
            return;

        void RenderMainRecipes(List<RecipeItemDTO> recipes)
        {
            foreach (Transform child in leftSideContent) Destroy(child.gameObject);
            foreach (Transform child in rightSideContent) Destroy(child.gameObject);

            var allDetails = recipes.SelectMany(r => r.RecipeItemDetails).ToList();
            int total = allDetails.Count;
            int leftCount = Mathf.CeilToInt(total / 2f);

            for (int i = 0; i < total; i++)
            {
                Transform parent = (i < leftCount) ? leftSideContent : rightSideContent;
                GameObject itemGO = Instantiate(HIENItemPrefab, parent);
                SetupHIENItemUI(itemGO, allDetails[i], recipeData.UserInventory);
            }
        }

        RenderMainRecipes(recipeData.Recipes);
        levelText.text = currentLevel.ToString();

        async Task RefreshPanelAsync()
        {
            userHIEN = await UserHIENsService.Create().GetUserHIENsAsync(User.CurrentUserId, featureId);
            currentLevel = userHIEN?.Level ?? 0;
            levelText.text = currentLevel.ToString();

            recipeData = await RecipeService.Create().GetRecipeItemsAsync(featureName, currentLevel + 1, User.CurrentUserId);
            if (recipeData != null && recipeData.Recipes != null)
            {
                RenderMainRecipes(recipeData.Recipes);
            }
        }

        // Popup Nâng Cấp Số Lượng
        async void CreatePopupUpgradePanelAsync()
        {
            GameObject gameObject = Instantiate(PopupHIENQuantityPanelPrefab, MainPanel);
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

            Transform currentStatsContent = panelTransform.Find("Scroll View/Viewport/Content/CurrentStats");
            Transform nextStatsContent = panelTransform.Find("Scroll View/Viewport/Content/NextStats");
            Transform rightTransform = panelTransform.Find("Right Scroll View/Viewport/Content");

            Transform leftScrollViewTransform = panelTransform.Find("Scroll View");
            Transform rightScrollViewTransform = panelTransform.Find("Right Scroll View");

            Image leftScrollViewBg = leftScrollViewTransform.GetComponent<Image>() ?? leftScrollViewTransform.Find("Viewport")?.GetComponent<Image>();
            Image rightScrollViewBg = rightScrollViewTransform.GetComponent<Image>() ?? rightScrollViewTransform.Find("Viewport")?.GetComponent<Image>();

            int popupCurrentLevel = currentLevel;

            // Fetch DTO Recipe duy nhất 1 lần
            RecipeResponseDTO popupRecipeData = await RecipeService.Create().GetRecipeItemsAsync(featureName, popupCurrentLevel + 1, User.CurrentUserId);

            // Max level của Recipe dùng làm maxValue cho Slider
            int recipeMaxLevel = popupRecipeData?.Recipes != null && popupRecipeData.Recipes.Count > 0 
                ? popupRecipeData.Recipes.Max(r => r.MaxLevel) 
                : popupCurrentLevel;

            // Max level của HIEN hiển thị lên UI
            int hienMaxLevel = hien != null ? hien.MaxLevel : recipeMaxLevel;

            int maxSliderValue = Mathf.Max(0, recipeMaxLevel - popupCurrentLevel);

            currentLevelText.text = popupCurrentLevel.ToString();
            nextLevelText.text = popupCurrentLevel.ToString();
            if (maxLevelText != null) maxLevelText.text = hienMaxLevel.ToString(); // YÊU CẦU 1: Lấy từ hien.MaxLevel

            if (userHIEN != null)
            {
                StatsManager.Instance.CreateStatsManager(userHIEN, currentStatsContent);
                StatsManager.Instance.CreateStatsManager(userHIEN, nextStatsContent);
            }

            // Slider maxValue cấu hình theo Recipe
            quantitySlider.minValue = 0;
            quantitySlider.maxValue = maxSliderValue; // YÊU CẦU 1: maxValue = recipe.MaxLevel - currentLevel
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

            void UpdatePreview()
            {
                int requested = (int)quantitySlider.value;

                if (popupCurrentLevel >= hienMaxLevel)
                {
                    var backgroundImage = confirmButton.transform.Find("Background2")?.GetComponent<RawImage>();
                    if (backgroundImage != null)
                        backgroundImage.color = ColorHelper.HexToColor(ColorConstants.GRAY_COLOR);

                    SetPreviewNotification(MessageConstants.UPGRADE_ALREADY_MAX, false);
                    nextLevelText.text = "MAX";
                    confirmButton.interactable = false;
                    UpdateLevelDisplay(0);

                    if (userHIEN != null)
                        StatsManager.Instance.CreateStatsManager(userHIEN, nextStatsContent);

                    return;
                }

                if (requested <= 0)
                {
                    nextLevelText.text = popupCurrentLevel.ToString();
                    confirmButton.interactable = false;
                    UpdateLevelDisplay(0);

                    if (userHIEN != null)
                        StatsManager.Instance.CreateStatsManager(userHIEN, nextStatsContent);

                    foreach (Transform child in rightTransform) Destroy(child.gameObject);
                    SetPreviewNotification(MessageConstants.READY_TO_UPGRADE, true);
                    return;
                }

                // YÊU CẦU 2: Tính toán bằng PreviewUpgrade (Tăng đúng 'requested' level không bị khống chế)
                RecipePreviewResultDTO preview = RecipeCalculator.PreviewUpgrade(
                    popupRecipeData, 
                    popupCurrentLevel, 
                    numberOfLevel: requested
                );

                nextLevelText.text = preview.TargetLevel.ToString();

                if (isPreviewSelected)
                {
                    confirmButton.interactable = false;
                }
                else
                {
                    confirmButton.interactable = preview.CanAffordAll && preview.GainedLevels > 0;
                }

                int displayGained = preview.GainedLevels > 0 ? preview.GainedLevels : requested;

                if (displayGained > 0 && userHIEN != null)
                {
                    UserHIENs previewHIEN = userHIEN.CloneUserHIEN(userHIEN);
                    EnhanceHelper.EnhanceHIENs(previewHIEN, displayGained, hien.BaseMultiplier);
                    StatsManager.Instance.CreateStatsManager(previewHIEN, nextStatsContent);
                }
                else if (userHIEN != null)
                {
                    StatsManager.Instance.CreateStatsManager(userHIEN, nextStatsContent);
                }

                // Cập nhật Slots Nguyên Liệu
                var materials = preview.RequiredMaterials ?? new List<RecipePreviewDetailDTO>();
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
                        : Instantiate(HIENItemPopupPrefab, rightTransform);

                    SetupHIENItemUI2(itemGO, materials[i]);
                }

                UpdateLevelDisplay(displayGained);

                if (preview.CanAffordAll)
                {
                    SetPreviewNotification(MessageConstants.READY_TO_UPGRADE, true);
                }
                else
                {
                    SetPreviewNotification(MessageConstants.NOT_ENOUGH_MATERIALS, false);
                    confirmButton.interactable = false;
                }
            }

            if (levelInputField != null)
            {
                levelInputField.onEndEdit.AddListener((inputValue) =>
                {
                    if (int.TryParse(inputValue, out int parsedLevel))
                    {
                        int clampedLevel = Mathf.Clamp(parsedLevel, 0, maxSliderValue);
                        levelInputField.text = clampedLevel.ToString();
                        quantitySlider.SetValueWithoutNotify(clampedLevel);
                    }
                    else
                    {
                        levelInputField.text = "0";
                        quantitySlider.SetValueWithoutNotify(0);
                    }

                    UpdatePreview();
                });
            }

            // Click Preview Toggle -> Reset Slider về 0
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
            
            // YÊU CẦU 2: Bấm Max Button
            increaseMaxButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

                if (isPreviewSelected)
                {
                    // Ở Preview Mode, bấm MAX sẽ đẩy Slider lên max của Recipe
                    quantitySlider.value = quantitySlider.maxValue;
                }
                else
                {
                    // Ở Default Mode, bấm MAX sẽ nhảy đúng cấp tối đa mà kho đồ chi trả được
                    RecipePreviewResultDTO maxAffordable = RecipeCalculator.PreviewMaxUpgrade(popupRecipeData, popupCurrentLevel);
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

            // Click Confirm
            confirmButton.onClick.AddListener(async () =>
            {
                if (isPreviewSelected)
                {
                    SetPreviewNotification(MessageConstants.CANNOT_CONFIRM_IN_PREVIEW_MODE, false);
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                    return;
                }

                if (popupCurrentLevel >= hienMaxLevel)
                {
                    SetPreviewNotification(MessageConstants.UPGRADE_ALREADY_MAX, false);
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                    return;
                }

                if ((int)quantitySlider.value <= 0)
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                    return;
                }

                AudioManager.Instance.PlaySFX(AudioConstants.SFX.LEVEL_UP_SOUND);
                confirmButton.interactable = false;
                closeButton.interactable = false;

                int requested = (int)quantitySlider.value;
                RecipeCalculateDTO calcResult = RecipeCalculator.CalculateUpgradeByLevels(popupRecipeData, popupCurrentLevel, requested);

                if (calcResult.GainedLevels <= 0)
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.ALERT_SOUND);
                    confirmButton.interactable = true;
                    closeButton.interactable = true;
                    return;
                }

                userHIEN = EnhanceHelper.EnhanceHIENs(userHIEN, calcResult.GainedLevels, hien.BaseMultiplier);

                var insertOrUpdateResult = await UserHIENsService.Create().InsertOrUpdateUserHIENsAsync(
                    User.CurrentUserId, 
                    userHIEN, 
                    featureId, 
                    calcResult.TotalDeductions
                );

                if (insertOrUpdateResult.Data && insertOrUpdateResult.IsChangePower
                    && (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated || insertOrUpdateResult.OperationType == DatabaseOperationType.Inserted))
                {
                    PowerResultDTO powerResult = await UserService.Create().UpdateUserPowerAsync();

                    if (powerResult.HasChanged)
                    {
                        PowerController.Instance.ShowPower(
                            powerResult.CurrentPower,
                            powerResult.Difference,
                            1
                        );
                    }
                }

                Destroy(gameObject);
                await RefreshPanelAsync();
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

    private void SetupHIENItemUI(GameObject itemGO, RecipeItemDetailDTO detail, Dictionary<string, double> userInventory)
    {
        TextMeshProUGUI requiredText = itemGO.transform.Find("RequiredText")?.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI ownedText = itemGO.transform.Find("AvailableText")?.GetComponent<TextMeshProUGUI>();
        RawImage image = itemGO.transform.Find("Image")?.GetComponent<RawImage>();

        double ownedQty = 0;
        if (userInventory != null)
        {
            if (userInventory.TryGetValue(detail.ObjectId, out var qty) || 
                userInventory.TryGetValue($"{detail.ObjectType}_{detail.ObjectId}", out qty))
            {
                ownedQty = qty;
            }
        }

        bool isEnough = ownedQty >= detail.RequiredQuantity;
        Color statusColor = ColorHelper.HexToColor(isEnough ? ColorConstants.GREEN_COLOR : ColorConstants.RED_COLOR);

        if (requiredText != null)
        {
            requiredText.text = detail.RequiredQuantity.ToString();
            requiredText.color = statusColor;
        }

        if (ownedText != null)
        {
            ownedText.text = ownedQty.ToString();
            ownedText.color = statusColor;
        }

        if (image != null && !string.IsNullOrEmpty(detail.ObjectImage))
        {
            Texture texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(detail.ObjectImage));
            if (texture != null)
                image.texture = texture;
        }
    }

    private void SetupHIENItemUI2(GameObject itemGO, RecipePreviewDetailDTO data)
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

}