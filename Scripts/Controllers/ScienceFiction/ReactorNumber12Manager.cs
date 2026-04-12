using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReactorNumber12Manager : MonoBehaviour
{
    private Transform MainPanel;
    private GameObject ReactorPanelNumberPrefab;
    private TMP_FontAsset EuroStyleNormalFont;
    private int fontSize;
    TeamsService teamsService;
    UserItemsService userItemsService;
    private const int MAX_LEVEL = 10000;
    public static ReactorNumber12Manager Instance { get; private set; }
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
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ReactorPanelNumberPrefab = UIManager.Instance.Get("ReactorPanelNumberPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
        fontSize = 26;
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }
    public async Task CreateReactorPanelAsync()
    {
        GameObject currentObject = Instantiate(ReactorPanelNumberPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button homeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await HomeManager.Instance.CreateHomePanelAsync();
        });
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });

        RawImage leftSideCounduit1Image = transform.Find("DictionaryCards/Reactor/LeftSideConduit1/LeftSideConduiltImage").GetComponent<RawImage>();
        RawImage leftSideCounduit2Image = transform.Find("DictionaryCards/Reactor/LeftSideConduit2/LeftSideConduiltImage").GetComponent<RawImage>();
        RawImage rightSideCounduit1Image = transform.Find("DictionaryCards/Reactor/RightSideConduit1/RightSideConduiltImage").GetComponent<RawImage>();
        RawImage rightSideCounduit2Image = transform.Find("DictionaryCards/Reactor/RightSideConduit2/RightSideConduiltImage").GetComponent<RawImage>();
        RawImage MainReactorBackgroundImage = transform.Find("DictionaryCards/Reactor/MainReactorBackgroundCircle/MainReactorImage").GetComponent<RawImage>();
        RawImage MainReactorImage = transform.Find("DictionaryCards/Reactor/MainReactor/MainReactorImage").GetComponent<RawImage>();
        RawImage MainReactorCoreImage = transform.Find("DictionaryCards/Reactor/MainReactorBackgroundCore/MainReactorCoreImage").GetComponent<RawImage>();
        TextMeshProUGUI ReactorLevelText = transform.Find("DictionaryCards/Reactor/ReactorLevel/ReactorLevelText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI ReactorNumberText = transform.Find("DictionaryCards/Reactor/ReactorLevel/ReactorNumberText").GetComponent<TextMeshProUGUI>();
        Button upLevelButton = transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Transform material1Group = transform.Find("DictionaryCards/MaterialNumber1");
        Transform material2Group = transform.Find("DictionaryCards/MaterialNumber2");
        Transform material3Group = transform.Find("DictionaryCards/MaterialNumber3");
        Transform material4Group = transform.Find("DictionaryCards/MaterialNumber4");
        RawImage material1Image = transform.Find("DictionaryCards/MaterialNumber1/MaterialImage").GetComponent<RawImage>();
        RawImage material2Image = transform.Find("DictionaryCards/MaterialNumber2/MaterialImage").GetComponent<RawImage>();
        RawImage material3Image = transform.Find("DictionaryCards/MaterialNumber3/MaterialImage").GetComponent<RawImage>();
        RawImage material4Image = transform.Find("DictionaryCards/MaterialNumber4/MaterialImage").GetComponent<RawImage>();
        TextMeshProUGUI availableQuantity1Text = transform.Find("DictionaryCards/MaterialNumber1/AvailableQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI availableQuantity2Text = transform.Find("DictionaryCards/MaterialNumber2/AvailableQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI availableQuantity3Text = transform.Find("DictionaryCards/MaterialNumber3/AvailableQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI availableQuantity4Text = transform.Find("DictionaryCards/MaterialNumber4/AvailableQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI requiredQuantity1Text = transform.Find("DictionaryCards/MaterialNumber1/RequiredQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI requiredQuantity2Text = transform.Find("DictionaryCards/MaterialNumber2/RequiredQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI requiredQuantity3Text = transform.Find("DictionaryCards/MaterialNumber3/RequiredQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI requiredQuantity4Text = transform.Find("DictionaryCards/MaterialNumber4/RequiredQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();

        TextMeshProUGUI upLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upLevelButtonText.font = EuroStyleNormalFont;
        upLevelButtonText.fontSize = fontSize;
        upLevelButtonText.fontStyle = FontStyles.Bold;
        upLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI upMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upMaxLevelButtonText.font = EuroStyleNormalFont;
        upMaxLevelButtonText.fontSize = fontSize;
        upMaxLevelButtonText.fontStyle = FontStyles.Bold;
        upMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_MAX_LEVEL);

        upLevelButton.AddComponent<SlideBottomToTopAnimation>();
        upMaxLevelButton.AddComponent<SlideBottomToTopAnimation>();
        material1Group.AddComponent<SlideBottomToTopAnimation>();
        material2Group.AddComponent<SlideBottomToTopAnimation>();
        material3Group.AddComponent<SlideBottomToTopAnimation>();
        material4Group.AddComponent<SlideBottomToTopAnimation>();

        ReactorNumberText.text = "12";
        titleText.text = LocalizationManager.Get(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_12);

        // Texture conduitTexture = TextureHelper.LoadTextureCached("UI/Background2/Conduit_1");
        // leftSideCounduit1Image.texture = conduitTexture;
        // leftSideCounduit2Image.texture = conduitTexture;
        // rightSideCounduit1Image.texture = conduitTexture;
        // rightSideCounduit2Image.texture = conduitTexture;

        MainReactorBackgroundImage.AddComponent<RotateAnimation>();

        Texture mainReactorTexture = TextureHelper.LoadTextureCached("UI/Background2/Reactor_1");
        MainReactorImage.texture = mainReactorTexture;

        // RotateAnimation anim = MainReactorCoreImage.AddComponent<RotateAnimation>();
        // anim.direction = 1;
        leftSideCounduit1Image.AddComponent<SlideLeftToRightAnimation>();
        leftSideCounduit2Image.AddComponent<SlideLeftToRightAnimation>();
        rightSideCounduit1Image.AddComponent<SlideRightToLeftAnimation>();
        rightSideCounduit2Image.AddComponent<SlideRightToLeftAnimation>();

        var feature = (await FeaturesService.Create()
            .GetFeaturesByTypeAsync(AppConstants.MainType.SCIENCE_FICTION))
            .GetValueOrDefault(AppConstants.ScienceFiction.REACTOR_NUMBER_12);

        ScienceFiction scienceFiction = await ScienceFictionService.Create().GetScienceFictionAsync(feature.Id);
        RankService rankService = new RankService();
        scienceFiction.Id = feature.Id;

        List<Items> items = new List<Items>();
        items.Add(await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.REACTOR_MATERIAL_NUMBER_1));
        items.Add(await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.REACTOR_MATERIAL_NUMBER_3));

        ReactorLevelText.text = scienceFiction.Level.ToString();

        double oneMaterialQuantity = EvaluateItem.CalculateToMaterialRequiredForOneUpgrade(scienceFiction.Level);
        double maxMaterialQuantity = EvaluateItem.CalculateTotalMaterialRequiredForMaxUpgrade(scienceFiction.Level, MAX_LEVEL, items);

        for (int i = 0; i < items.Count; i++)
        {
            Items item = items[i];

            if (i == 0)
            {
                CreateMaterialForOneLevel(material1Image, availableQuantity1Text, requiredQuantity1Text, item.Image, scienceFiction.Level, item.Quantity, oneMaterialQuantity);
                CreateMaterialForMaxLevel(material3Image, availableQuantity3Text, requiredQuantity3Text, item.Image, scienceFiction.Level, item.Quantity, maxMaterialQuantity);
            }
            else if (i == 1)
            {
                CreateMaterialForOneLevel(material2Image, availableQuantity2Text, requiredQuantity2Text, item.Image, scienceFiction.Level, item.Quantity, oneMaterialQuantity);
                CreateMaterialForMaxLevel(material4Image, availableQuantity4Text, requiredQuantity4Text, item.Image, scienceFiction.Level, item.Quantity, maxMaterialQuantity);
            }
        }
        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();

        upLevelButton.onClick.AddListener(async () =>
        {
            double materialRequired = EvaluateItem.CalculateToMaterialRequiredForOneUpgrade(scienceFiction.Level);

            // Check nếu đã max level thì dừng
            if (scienceFiction.Level >= MAX_LEVEL) return;

            // Kiểm tra xem tất cả items đều đủ số lượng
            bool hasAllMaterials = items.All(i => i.Quantity >= materialRequired);

            if (hasAllMaterials)
            {
                foreach (var i in items)
                {
                    i.Quantity -= materialRequired;
                    await userItemsService.UpdateUserItemQuantityAsync(i);
                }

                ScienceFiction newScienceFiction = rankService.EnhanceScienceFiction(scienceFiction, 1, 10);

                // rankService.UpLevel(cardHeroes, newRank, mainType);
                await ScienceFictionService.Create().InsertOrUpdateScienceFictionAsync(User.CurrentUserId, newScienceFiction, feature.Id);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(currentObject);
                await CreateReactorPanelAsync();
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            double totalMaterialRequired = EvaluateItem.CalculateTotalMaterialRequiredForMaxUpgrade(scienceFiction.Level, MAX_LEVEL, items);

            if (totalMaterialRequired <= 0.0)
            {
                return;
            }

            // 2. TÍNH TOÁN SỐ LƯỢNG LEVEL NÂNG CẤP (upgradeAmount)
            // Vòng lặp này cần chạy để xác định số level (int) tương ứng với chi phí (double)
            int currentLevel = scienceFiction.Level;
            double tempCost = 0.0; // Sử dụng double cho chi phí tạm thời
            int upgradeAmount = 0;

            // Vòng lặp để xác định N level
            while (tempCost < totalMaterialRequired && currentLevel < MAX_LEVEL)
            {
                double cost = EvaluateItem.CalculateToMaterialRequiredForOneUpgrade(currentLevel);
                if (tempCost + cost <= totalMaterialRequired)
                {
                    tempCost += cost;
                    currentLevel++;
                    upgradeAmount++;
                }
                else
                {
                    break;
                }
            }

            // 3. TRỪ VẬT LIỆU VÀ CẬP NHẬT DATABASE
            foreach (var i in items)
            {
                // Trừ tổng chi phí (double) khỏi Quantity (double)
                i.Quantity -= totalMaterialRequired;

                // Cập nhật Database
                await userItemsService.UpdateUserItemQuantityAsync(i);
            }

            // Nâng cấp scienceFiction
            ScienceFiction newScienceFiction = rankService.EnhanceScienceFiction(scienceFiction, upgradeAmount, 10);

            await ScienceFictionService.Create().InsertOrUpdateScienceFictionAsync(User.CurrentUserId, newScienceFiction, feature.Id);
            double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
            double currentPower = User.CurrentUserPower;
            User.CurrentUserPower = newPower;
            FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

            Destroy(currentObject);
            await CreateReactorPanelAsync();
        });
    }
    public void CreateMaterialForOneLevel(RawImage image, TextMeshProUGUI availableQuantityText, TextMeshProUGUI requiredQuantityText, string itemImage, int level = 0, double userMaterialQuantity = 0, double materialQuantity = 0)
    {
        
        availableQuantityText.text = userMaterialQuantity.ToString();
        requiredQuantityText.text = materialQuantity.ToString();

        Texture texture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(itemImage)}");
        image.texture = texture;

        if (level >= MAX_LEVEL)
        {
            requiredQuantityText.text = "MAX";
            return;
        }
    }
    public void CreateMaterialForMaxLevel(RawImage image, TextMeshProUGUI availableQuantityText, TextMeshProUGUI requiredQuantityText, string itemImage, int level = 0, double userMaterialQuantity = 0, double materialQuantity = 0)
    {
        
        availableQuantityText.text = userMaterialQuantity.ToString();
        requiredQuantityText.text = materialQuantity.ToString();

        Texture texture = TextureHelper.LoadTextureCached($"{ImageExtensionHandler.RemoveImageExtension(itemImage)}");
        image.texture = texture;

        if (level >= MAX_LEVEL)
        {
            requiredQuantityText.text = "MAX";
            return;
        }
    }
}
