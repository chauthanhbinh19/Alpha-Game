using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HIDCIXManager : MonoBehaviour
{
    public static HIDCIXManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject HIDCPanelPrefab;
    private GameObject HIDCButtonPrefab;
    private GameObject PopupHIDCPanelPrefab;
    private GameObject PopupHIDCButtonPrefab;
    private GameObject MainHIDCPanelPrefab;
    private GameObject HIDCItemPrefab;
    private Transform content;
    private const int ITEMS_PER_PAGE = 50;
    private int _currentPage = 0;
    private List<KeyValuePair<string, FeatureHIDCDTO>> _featureList;
    private Button nextButton;
    private Button previousButton;
    private TextMeshProUGUI pageText;
    private const int MAX_LEVEL = 10000;
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
        HIDCPanelPrefab = UIManager.Instance.Get("HIDCPanelPrefab");
        HIDCButtonPrefab = UIManager.Instance.Get("HIDCButtonPrefab");
        PopupHIDCPanelPrefab = UIManager.Instance.Get("PopupHIDCPanelPrefab");
        PopupHIDCButtonPrefab = UIManager.Instance.Get("PopupHIDCButtonPrefab");
        MainHIDCPanelPrefab = UIManager.Instance.Get("MainHIDCPanelPrefab");
        HIDCItemPrefab = UIManager.Instance.Get("HIDCItemPrefab");
    }
    public async Task CreateHIDCIXManagerAsync()
    {
        GameObject currentObject = Instantiate(PopupHIDCPanelPrefab, MainPanel);
        content = currentObject.transform.Find("Scroll View/Viewport/Content");
        Button CloseButton = currentObject.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button HomeButton = currentObject.transform.Find("HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await HomeManager.Instance.CreateHomePanelAsync();
        });
        Dictionary<string, FeatureHIDCDTO> uniqueTypes = new Dictionary<string, FeatureHIDCDTO>();
        uniqueTypes = await FeaturesService.Create().GetHIDCFeaturesByTypeAsync(AppConstants.HIDC.HIDC_IX);
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

            GameObject button = Instantiate(PopupHIDCButtonPrefab, content);

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

            Button btn = button.GetComponent<Button>();
            btn.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await CreateMainHIDCPanelAsync(featureId, subtype);
            });
        }
    }
    
    private void SetupPagination(GameObject currentObject)
    {
        nextButton = currentObject.transform
            .Find("Pagination/Next")
            .GetComponent<Button>();

        previousButton = currentObject.transform
            .Find("Pagination/Previous")
            .GetComponent<Button>();

        pageText = currentObject.transform
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

    public async Task CreateMainHIDCPanelAsync(string featureId, string featureName)
    {
        GameObject currentObject = Instantiate(MainHIDCPanelPrefab, MainPanel);
        Button upgradeOneLevelButton = currentObject.transform.Find("UpgradeOneLevelButton").GetComponent<Button>();
        Button upgradeMaxLevelButton = currentObject.transform.Find("UpgradeMaxLevelButton").GetComponent<Button>();
        Transform leftSideContent = currentObject.transform.Find("LeftSideContent");
        Transform rightSideContent = currentObject.transform.Find("RightSideContent");
        TextMeshProUGUI levelText = currentObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        Button CloseButton = currentObject.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button HomeButton = currentObject.transform.Find("HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await HomeManager.Instance.CreateHomePanelAsync();
        });
        RawImage mapImage = currentObject.transform.Find("MapImage").GetComponent<RawImage>();
        Texture mapTexture = Resources.Load<Texture2D>("UI/Background2/Chapter_18");
        mapImage.texture = mapTexture; 
        RawImage rankImage = currentObject.transform.Find("GroupBackground/RankImage").GetComponent<RawImage>();
        Texture rankTexture = Resources.Load<Texture2D>($"UI/Rank_Research/{AppConstants.HIDC.HIDC_IX}");
        rankImage.texture = rankTexture;

        List<RecipeItemDto> recipeItems = await RecipeService.Create().GetRecipeItemsAsync(featureName, User.CurrentUserLevel, User.CurrentUserId);
        HIDCs researchs = await HIDCsService.Create().GetHIDCsAsync(featureId);

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

            GameObject itemGO = Instantiate(HIDCItemPrefab, parent);

            SetupHIDCItemUI(itemGO, recipeItems[i]);
        }

        int currentLevel = researchs?.Level ?? 0;
        levelText.text = currentLevel.ToString();

        upgradeOneLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            UpgradeResultDTO result = await UpgradeService.Create().UpgradeOneLevelAsync(featureName, currentLevel, MAX_LEVEL, User.CurrentUserId);
            if (result.Success)
            {
                researchs = HIDCsService.Create().EnhanceHIDCs(researchs, result.UpgradedLevels, 1000);
                await HIDCsService.Create().InsertOrUpdateHIDCsAsync(User.CurrentUserId, researchs, featureId);
                Destroy(currentObject);

                double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                await CreateMainHIDCPanelAsync(featureId, featureName);
            }
            else
            {
                Debug.Log(result.Message);
            }
        });
        upgradeMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            UpgradeResultDTO result = await UpgradeService.Create().UpgradeMaxLevelAsync(featureName, currentLevel, MAX_LEVEL, User.CurrentUserId);
            if (result.Success)
            {
                researchs = HIDCsService.Create().EnhanceHIDCs(researchs, result.UpgradedLevels, 1000);
                await HIDCsService.Create().InsertOrUpdateHIDCsAsync(User.CurrentUserId, researchs, featureId);
                Destroy(currentObject);

                double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                
                await CreateMainHIDCPanelAsync(featureId, featureName);
            }
            else
            {
                Debug.Log(result.Message);
            }
        });
    }
    
    private void SetupHIDCItemUI(GameObject itemGO,RecipeItemDto data)
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
        Texture texture = Resources.Load<Texture2D>(ImageExtensionHandler.RemoveImageExtension(data.ItemImage));
        if (texture != null)
            image.texture = texture;
    }

}