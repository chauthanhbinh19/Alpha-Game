using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchController : MonoBehaviour
{
    public static ResearchController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ResearchPanelPrefab;
    private GameObject ResearchButtonPrefab;
    private GameObject PopupResearchPanelPrefab;
    private GameObject PopupResearchQuantityPanelPrefab;
    private GameObject PopupResearchButtonPrefab;
    private GameObject MainResearchPanelPrefab;
    private GameObject ResearchItemPrefab;
    private Transform Content;
    private const int ITEMS_PER_PAGE = 50;
    private int CurrentPage = 0;
    private List<KeyValuePair<string, FeatureResearchDTO>> FeatureList;
    private PaginationManager PaginationManager;
    private string Type = "";
    private string Image = "";
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
        ResearchPanelPrefab = UIManager.Instance.Get(PrefabConstants.Research.RESEARCH_PANEL_PREFAB);
        ResearchButtonPrefab = UIManager.Instance.Get(PrefabConstants.Research.RESEARCH_BUTTON_PREFAB);
        PopupResearchPanelPrefab = UIManager.Instance.Get(PrefabConstants.Research.POPUP_RESEARCH_PANEL_PREFAB);
        PopupResearchQuantityPanelPrefab = UIManager.Instance.Get(PrefabConstants.Research.POPUP_RESEARCH_QUANTITY_PANEL_PREFAB);
        PopupResearchButtonPrefab = UIManager.Instance.Get(PrefabConstants.Research.POPUP_RESEARCH_BUTTON_PREFAB);
        MainResearchPanelPrefab = UIManager.Instance.Get(PrefabConstants.Research.MAIN_RESEARCH_PANEL_PREFAB);
        ResearchItemPrefab = UIManager.Instance.Get(PrefabConstants.Research.RESEARCH_ITEM_PREFAB);
    }
    public async Task GetResearchAsync(string type)
    {
        switch (type)
        {
            case AppConstants.Research.FACILITIES:
                Type = type;
                Image = ImageConstants.Research.FACILITIES_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.HOUSING:
                Type = type;
                Image = ImageConstants.Research.HOUSING_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.INFRASTRUCTURE:
                Type = type;
                Image = ImageConstants.Research.INFRASTRUCTURE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.LOGISTICS:
                Type = type;
                Image = ImageConstants.Research.LOGISTICS_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.SANITATION:
                Type = type;
                Image = ImageConstants.Research.SANITATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.TRANSPORTATION:
                Type = type;
                Image = ImageConstants.Research.TRANSPORTATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.URBANIZATION:
                Type = type;
                Image = ImageConstants.Research.URBANIZATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.UTILITIES:
                Type = type;
                Image = ImageConstants.Research.UTILITIES_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.WASTE:
                Type = type;
                Image = ImageConstants.Research.WASTE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.WATER:
                Type = type;
                Image = ImageConstants.Research.WATER_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.CONSTRUCTION:
                Type = type;
                Image = ImageConstants.Research.CONSTRUCTION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.ENERGY:
                Type = type;
                Image = ImageConstants.Research.ENERGY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.ENGINEERING:
                Type = type;
                Image = ImageConstants.Research.ENGINEERING_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.INDUSTRY:
                Type = type;
                Image = ImageConstants.Research.INDUSTRY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.MANUFACTURING:
                Type = type;
                Image = ImageConstants.Research.MANUFACTURING_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.MATERIALS:
                Type = type;
                Image = ImageConstants.Research.MATERIALS_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.MECHANICS:
                Type = type;
                Image = ImageConstants.Research.MECHANICS_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.POWER:
                Type = type;
                Image = ImageConstants.Research.POWER_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.RESOURCE:
                Type = type;
                Image = ImageConstants.Research.RESOURCE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.SYSTEM:
                Type = type;
                Image = ImageConstants.Research.SYSTEM_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.ARMOR:
                Type = type;
                Image = ImageConstants.Research.ARMOR_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.DEFENSE:
                Type = type;
                Image = ImageConstants.Research.DEFENSE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.DISASTER:
                Type = type;
                Image = ImageConstants.Research.DISASTER_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.EMERGENCY:
                Type = type;
                Image = ImageConstants.Research.EMERGENCY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.FORTIFICATION:
                Type = type;
                Image = ImageConstants.Research.FORTIFICATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.MILITARY:
                Type = type;
                Image = ImageConstants.Research.MILITARY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.SAFETY:
                Type = type;
                Image = ImageConstants.Research.SAFETY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.SHIELDING:
                Type = type;
                Image = ImageConstants.Research.SHIELDING_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.TACTICS:
                Type = type;
                Image = ImageConstants.Research.TACTICS_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.WEAPONS:
                Type = type;
                Image = ImageConstants.Research.WEAPONS_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.COMMERCE:
                Type = type;
                Image = ImageConstants.Research.COMMERCE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.DISTRIBUTION:
                Type = type;
                Image = ImageConstants.Research.DISTRIBUTION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.ECONOMY:
                Type = type;
                Image = ImageConstants.Research.ECONOMY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.ENTERPRISE:
                Type = type;
                Image = ImageConstants.Research.ENTERPRISE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.FINANCE:
                Type = type;
                Image = ImageConstants.Research.FINANCE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.INVESTMENT:
                Type = type;
                Image = ImageConstants.Research.INVESTMENT_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.MARKET:
                Type = type;
                Image = ImageConstants.Research.MARKET_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.PRODUCTIVITY:
                Type = type;
                Image = ImageConstants.Research.PRODUCTIVITY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.SUPPLY:
                Type = type;
                Image = ImageConstants.Research.SUPPLY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.TRADE:
                Type = type;
               Image = ImageConstants.Research.TRADE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.CLIMATE:
                Type = type;
                Image = ImageConstants.Research.CLIMATE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.CONSERVATION:
                Type = type;
                Image = ImageConstants.Research.CONSERVATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.ECOLOGY:
                Type = type;
                Image = ImageConstants.Research.ECOLOGY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.ENVIRONMENT:
                Type = type;
                Image = ImageConstants.Research.ENVIRONMENT_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.POLLUTION:
                Type = type;
                Image = ImageConstants.Research.POLLUTION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.PRESERVATION:
                Type = type;
                Image = ImageConstants.Research.PRESERVATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.RECYCLING:
                Type = type;
                Image = ImageConstants.Research.RECYCLING_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.RENEWABLES:
                Type = type;
                Image = ImageConstants.Research.RENEWABLES_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.RESTORATION:
                Type = type;
                Image = ImageConstants.Research.RESTORATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.SUSTAINABILITY:
                Type = type;
                Image = ImageConstants.Research.SUSTAINABILITY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.ASCENSION:
                Type = type;
                Image = ImageConstants.Research.ASCENSION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.COLONIZATION:
                Type = type;
                Image = ImageConstants.Research.COLONIZATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.COSMOLOGY:
                Type = type;
                Image = ImageConstants.Research.COSMOLOGY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.DIMENSIONAL:
                Type = type;
                Image = ImageConstants.Research.DIMENSIONAL_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.EXPANSION:
                Type = type;
                Image = ImageConstants.Research.EXPANSION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.EXPLORATION:
                Type = type;
                Image = ImageConstants.Research.EXPLORATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.MEGASTRUCTURE:
                Type = type;
                Image = ImageConstants.Research.MEGASTRUCTURE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.SINGULARITY:
                Type = type;
                Image = ImageConstants.Research.SINGULARITY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.TERRAFORMING:
                Type = type;
                Image = ImageConstants.Research.TERRAFORMING_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.TIME:
                Type = type;
                Image = ImageConstants.Research.TIME_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.BIOTECH:
                Type = type;
                Image = ImageConstants.Research.BIOTECH_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.EPIDEMIOLOGY:
                Type = type;
                Image = ImageConstants.Research.EPIDEMIOLOGY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.GENETICS:
                Type = type;
                Image = ImageConstants.Research.GENETICS_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.HEALTH:
                Type = type;
                Image = ImageConstants.Research.HEALTH_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.IMMUNOLOGY:
                Type = type;
                Image = ImageConstants.Research.IMMUNOLOGY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.LONGEVITY:
                Type = type;
                Image = ImageConstants.Research.LONGEVITY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.MEDICINE:
                Type = type;
                Image = ImageConstants.Research.MEDICINE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.NUTRITION:
                Type = type;
                Image = ImageConstants.Research.NUTRITION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.PHARMACEUTICALS:
                Type = type;
                Image = ImageConstants.Research.PHARMACEUTICALS_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.REGENERATION:
                Type = type;
                Image = ImageConstants.Research.REGENERATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.AI:
                Type = type;
                Image = ImageConstants.Research.AI_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.ANALYTICS:
                Type = type;
                Image = ImageConstants.Research.ANALYTICS_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.COMMUNICATION:
                Type = type;
                Image = ImageConstants.Research.COMMUNICATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.CONTROL:
                Type = type;
                Image = ImageConstants.Research.CONTROL_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.CYBERSECURITY:
                Type = type;
                Image = ImageConstants.Research.CYBERSECURITY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.DATA:
                Type = type;
                Image = ImageConstants.Research.DATA_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.INFORMATION:
                Type = type;
                Image = ImageConstants.Research.INFORMATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.NETWORKING:
                Type = type;
                Image = ImageConstants.Research.NETWORKING_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.SECURITY:
                Type = type;
                Image = ImageConstants.Research.SECURITY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.SURVEILLANCE:
                Type = type;
               Image = ImageConstants.Research.SURVEILLANCE_URL;
                await CreateResearchControllerAsync();
                break;

            case AppConstants.Research.AUTOMATION:
                Type = type;
                Image = ImageConstants.Research.AUTOMATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.BIOLOGY:
                Type = type;
                Image = ImageConstants.Research.BIOLOGY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.CHEMISTRY:
                Type = type;
                Image = ImageConstants.Research.CHEMISTRY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.COMPUTING:
                Type = type;
                Image = ImageConstants.Research.COMPUTING_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.INNOVATION:
                Type = type;
                Image = ImageConstants.Research.INNOVATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.NANOTECHNOLOGY:
                Type = type;
                Image = ImageConstants.Research.NANOTECHNOLOGY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.PHYSICS:
                Type = type;
                Image = ImageConstants.Research.PHYSICS_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.QUANTUM:
                Type = type;
                Image = ImageConstants.Research.QUANTUM_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.ROBOTICS:
                Type = type;
                Image = ImageConstants.Research.ROBOTICS_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.SCIENCE:
                Type = type;
                Image = ImageConstants.Research.SCIENCE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.CIVICS:
                Type = type;
                Image = ImageConstants.Research.CIVICS_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.CULTURE:
                Type = type;
                Image = ImageConstants.Research.CULTURE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.DEMOGRAPHY:
                Type = type;
                Image = ImageConstants.Research.DEMOGRAPHY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.EDUCATION:
                Type = type;
                Image = ImageConstants.Research.EDUCATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.GOVERNANCE:
                Type = type;
                Image = ImageConstants.Research.GOVERNANCE_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.HAPPINESS:
                Type = type;
                Image = ImageConstants.Research.HAPPINESS_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.LAW:
                Type = type;
                Image = ImageConstants.Research.LAW_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.POLICY:
                Type = type;
                Image = ImageConstants.Research.POLICY_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.POPULATION:
                Type = type;
                Image = ImageConstants.Research.POPULATION_URL;
                await CreateResearchControllerAsync();
                break;
            case AppConstants.Research.SOCIETY:
                Type = type;
                Image = ImageConstants.Research.SOCIETY_URL;
                await CreateResearchControllerAsync();
                break;
            default:
                break;
        }
    }
    public async Task CreateResearchControllerAsync()
    {
        GameObject currentObject = Instantiate(PopupResearchPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Content = transform.Find("Scroll View/Viewport/Content");
        Button CloseButton = transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button HomeButton = transform.Find("HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener( () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            
        });
        Dictionary<string, FeatureResearchDTO> uniqueTypes = new Dictionary<string, FeatureResearchDTO>();
        uniqueTypes = await FeaturesService.Create().GetResearchFeaturesByTypeAsync(Type);
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
            GameObject button = Instantiate(PopupResearchButtonPrefab, Content);
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
                    await CreateMainResearchPanelAsync(currentFeatureId, currentSubtype);
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

    public async Task CreateMainResearchPanelAsync(string featureId, string featureName)
    {
        GameObject currentObject = Instantiate(MainResearchPanelPrefab, MainPanel);
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
        Texture mapTexture = Resources.Load<Texture2D>("UI/Background2/Chapter_16");
        mapImage.texture = mapTexture;
        RawImage rankImage = transform.Find("GroupBackground/RankImage").GetComponent<RawImage>();
        Texture rankTexture = Resources.Load<Texture2D>($"UI/Rank_Research/{Type}");
        rankImage.texture = rankTexture;
        RawImage background = transform.Find("Background").GetComponent<RawImage>();
        background.texture = TextureHelper.LoadTexture2DCached(Image);

        AnimationController.Instance.CreateResearchAnimation(currentObject);
        Researchs research = await ResearchsService.Create().GetResearchByIdAsync(featureId);
        UserResearchs userResearch = await UserResearchsService.Create().GetUserResearchsAsync(User.CurrentUserId, featureId);
        List<RecipeItemDto> recipeItems = await RecipeService.Create().GetRecipeItemsAsync(featureName, userResearch.Level, User.CurrentUserId);

        if (recipeItems == null || recipeItems.Count == 0)
            return;

        // Xoá item cũ nếu có
        if (leftSideContent != null)
        {
            for (int i = leftSideContent.childCount - 1; i >= 0; i--)
                Destroy(leftSideContent.GetChild(i).gameObject);
        }

        if (rightSideContent != null)
        {
            for (int i = rightSideContent.childCount - 1; i >= 0; i--)
                Destroy(rightSideContent.GetChild(i).gameObject);
        }

        int total = recipeItems.Count;
        int leftCount = Mathf.CeilToInt(total / 2f);

        for (int i = 0; i < total; i++)
        {
            Transform parent = (i < leftCount)
                ? leftSideContent
                : rightSideContent;

            GameObject itemGO = Instantiate(ResearchItemPrefab, parent);

            SetupResearchItemUI(itemGO, recipeItems[i]);
        }

        int currentLevel = userResearch?.Level ?? 0;
        levelText.text = currentLevel.ToString();

        async Task RefreshPanelAsync()
        {
            userResearch = await UserResearchsService.Create().GetUserResearchsAsync(User.CurrentUserId,featureId);
            currentLevel = userResearch?.Level ?? 0;
            levelText.text = currentLevel.ToString();

            List<RecipeItemDto> refreshedRecipeItems = await RecipeService.Create().GetRecipeItemsAsync(featureName, userResearch.Level, User.CurrentUserId);
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

                GameObject itemGO = Instantiate(ResearchItemPrefab, parent);
                SetupResearchItemUI(itemGO, refreshedRecipeItems[i]);
            }
        }

        // Popup that allows upgrading multiple levels (wired to UpgradeLevelButton)
        void CreatePopupUpgradePanelAsync()
        {
            GameObject gameObject =
                Instantiate(PopupResearchQuantityPanelPrefab, MainPanel);

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
            int maxLevel = research != null ? research.MaxLevel : popupCurrentLevel;
            int maxPossible = Mathf.Max(0, maxLevel - popupCurrentLevel);

            currentLevelText.text = popupCurrentLevel.ToString();
            nextLevelText.text = (popupCurrentLevel + 1).ToString();

            if (userResearch != null)
            {
                StatsManager.Instance.CreateStatsManager(userResearch, currentStatsContent);
                StatsManager.Instance.CreateStatsManager(userResearch, nextStatsContent);
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

                    if (userResearch != null)
                        StatsManager.Instance.CreateStatsManager(userResearch, nextStatsContent);

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
                    UserResearchs previewResearch = userResearch.CloneUserResearch(userResearch);
                    EnhanceHelper.EnhanceResearchs(previewResearch, preview.UpgradedLevels, research.BaseMultiplier);
                    StatsManager.Instance.CreateStatsManager(previewResearch, nextStatsContent);
                }
                else if (userResearch != null)
                {
                    StatsManager.Instance.CreateStatsManager(userResearch, nextStatsContent);
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
                    userResearch = EnhanceHelper.EnhanceResearchs(userResearch, result.UpgradedLevels, research.BaseMultiplier);
                    var insertOrUpdateResult = await UserResearchsService.Create().InsertOrUpdateUserResearchsAsync(User.CurrentUserId, userResearch, featureId);

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
    
    private void SetupResearchItemUI(GameObject itemGO,RecipeItemDto data)
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