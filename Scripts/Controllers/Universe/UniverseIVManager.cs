using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UniverseIVManager : MonoBehaviour
{
    public static UniverseIVManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject UniversePanelPrefab;
    private GameObject UniverseButtonPrefab;
    private GameObject PopupUniversePanelPrefab;
    private GameObject PopupUniverseButtonPrefab;
    private GameObject MainUniversePanelPrefab;
    private GameObject UniverseItemPrefab;
    private Transform content;
    private const int ITEMS_PER_PAGE = 50;
    private int _currentPage = 0;
    private List<KeyValuePair<string, FeatureUniverseDTO>> _featureList;
    private Button nextButton;
    private Button previousButton;
    private TextMeshProUGUI pageText;
    private int MAX_LEVEL = 10000;
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
        UniversePanelPrefab = UIManager.Instance.Get("UniversePanelPrefab");
        UniverseButtonPrefab = UIManager.Instance.Get("UniverseButtonPrefab");
        PopupUniversePanelPrefab = UIManager.Instance.Get("PopupUniversePanelPrefab");
        PopupUniverseButtonPrefab = UIManager.Instance.Get("PopupUniverseButtonPrefab");
        MainUniversePanelPrefab = UIManager.Instance.Get("MainUniversePanelPrefab");
        UniverseItemPrefab = UIManager.Instance.Get("UniverseItemPrefab");
    }
    public async Task CreateUniverseIVManagerAsync()
    {
        GameObject currentObject = Instantiate(PopupUniversePanelPrefab, MainPanel);
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
        Dictionary<string, FeatureUniverseDTO> uniqueTypes = new Dictionary<string, FeatureUniverseDTO>();
        uniqueTypes = await FeaturesService.Create().GetUniverseFeaturesByTypeAsync(AppConstants.Universe.UNIVERSE_IV);
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

            GameObject button = Instantiate(PopupUniverseButtonPrefab, content);

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
                await CreateMainUniversePanelAsync(featureId, subtype);
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

    public async Task CreateMainUniversePanelAsync(string featureId, string featureName)
    {
        GameObject currentObject = Instantiate(MainUniversePanelPrefab, MainPanel);
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

        List<RecipeItemDto> recipeItems = await RecipeService.Create().GetRecipeItemsAsync(featureName, User.CurrentUserLevel, User.CurrentUserId);
        Universes researchs = await UniversesService.Create().GetUniversesAsync(featureId);

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

        int currentLevel = researchs?.Level ?? 0;
        levelText.text = currentLevel.ToString();

        upgradeOneLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.SWITCH_CLICK_SOUND);
            UpgradeResultDTO result = await UpgradeService.Create().UpgradeOneLevelAsync(featureName, currentLevel, MAX_LEVEL, User.CurrentUserId);
            if (result.Success)
            {
                researchs = UniversesService.Create().EnhanceUniverses(researchs, result.UpgradedLevels, 1000);
                await UniversesService.Create().InsertOrUpdateUniversesAsync(User.CurrentUserId, researchs, featureId);
                Destroy(currentObject);
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
            UpgradeResultDTO result = await UpgradeService.Create().UpgradeMaxLevelAsync(featureName, currentLevel, MAX_LEVEL, User.CurrentUserId);
            if (result.Success)
            {
                researchs = UniversesService.Create().EnhanceUniverses(researchs, result.UpgradedLevels, 1000);
                await UniversesService.Create().InsertOrUpdateUniversesAsync(User.CurrentUserId, researchs, featureId);
                Destroy(currentObject);
                await CreateMainUniversePanelAsync(featureId, featureName);
            }
            else
            {
                Debug.Log(result.Message);
            }
        });
    }
    
    private void SetupUniverseItemUI(GameObject itemGO,RecipeItemDto data)
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