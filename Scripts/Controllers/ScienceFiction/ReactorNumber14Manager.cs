using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReactorNumber14Manager : MonoBehaviour
{
    private Transform MainPanel;
    private GameObject ReactorPanelNumberPrefab;
    private TMP_FontAsset EuroStyleNormalFont;
    private int fontSize;
    TeamsService teamsService;
    UserItemsService userItemsService;
    private int maxLevel;
    public static ReactorNumber14Manager Instance { get; private set; }
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
        ReactorPanelNumberPrefab = UIManager.Instance.GetGameObjectScienceFiction("ReactorPanelNumberPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
        fontSize = 26;
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
        maxLevel = 10000;
    }
    public async Task CreateReactorPanelAsync()
    {
        GameObject currentObject = Instantiate(ReactorPanelNumberPrefab, MainPanel);
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });

        RawImage leftSideCounduit1Image = currentObject.transform.Find("DictionaryCards/Reactor/LeftSideConduit1/LeftSideConduiltImage").GetComponent<RawImage>();
        RawImage leftSideCounduit2Image = currentObject.transform.Find("DictionaryCards/Reactor/LeftSideConduit2/LeftSideConduiltImage").GetComponent<RawImage>();
        RawImage rightSideCounduit1Image = currentObject.transform.Find("DictionaryCards/Reactor/RightSideConduit1/RightSideConduiltImage").GetComponent<RawImage>();
        RawImage rightSideCounduit2Image = currentObject.transform.Find("DictionaryCards/Reactor/RightSideConduit2/RightSideConduiltImage").GetComponent<RawImage>();
        RawImage MainReactorBackgroundImage = currentObject.transform.Find("DictionaryCards/Reactor/MainReactorBackgroundCircle/MainReactorImage").GetComponent<RawImage>();
        RawImage MainReactorImage = currentObject.transform.Find("DictionaryCards/Reactor/MainReactor/MainReactorImage").GetComponent<RawImage>();
        RawImage MainReactorCoreImage = currentObject.transform.Find("DictionaryCards/Reactor/MainReactorBackgroundCore/MainReactorCoreImage").GetComponent<RawImage>();
        TextMeshProUGUI ReactorLevelText = currentObject.transform.Find("DictionaryCards/Reactor/ReactorLevel/ReactorLevelText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI ReactorNumberText = currentObject.transform.Find("DictionaryCards/Reactor/ReactorLevel/ReactorNumberText").GetComponent<TextMeshProUGUI>();
        Button UpLevelButton = currentObject.transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        Button UpMaxLevelButton = currentObject.transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Transform material1Group = currentObject.transform.Find("DictionaryCards/MaterialNumber1");
        Transform material2Group = currentObject.transform.Find("DictionaryCards/MaterialNumber2");
        Transform material3Group = currentObject.transform.Find("DictionaryCards/MaterialNumber3");
        Transform material4Group = currentObject.transform.Find("DictionaryCards/MaterialNumber4");
        RawImage material1Image = currentObject.transform.Find("DictionaryCards/MaterialNumber1/MaterialImage").GetComponent<RawImage>();
        RawImage material2Image = currentObject.transform.Find("DictionaryCards/MaterialNumber2/MaterialImage").GetComponent<RawImage>();
        RawImage material3Image = currentObject.transform.Find("DictionaryCards/MaterialNumber3/MaterialImage").GetComponent<RawImage>();
        RawImage material4Image = currentObject.transform.Find("DictionaryCards/MaterialNumber4/MaterialImage").GetComponent<RawImage>();
        TextMeshProUGUI availableQuantity1Text = currentObject.transform.Find("DictionaryCards/MaterialNumber1/AvailableQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI availableQuantity2Text = currentObject.transform.Find("DictionaryCards/MaterialNumber2/AvailableQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI availableQuantity3Text = currentObject.transform.Find("DictionaryCards/MaterialNumber3/AvailableQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI availableQuantity4Text = currentObject.transform.Find("DictionaryCards/MaterialNumber4/AvailableQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI requiredQuantity1Text = currentObject.transform.Find("DictionaryCards/MaterialNumber1/RequiredQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI requiredQuantity2Text = currentObject.transform.Find("DictionaryCards/MaterialNumber2/RequiredQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI requiredQuantity3Text = currentObject.transform.Find("DictionaryCards/MaterialNumber3/RequiredQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI requiredQuantity4Text = currentObject.transform.Find("DictionaryCards/MaterialNumber4/RequiredQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_MAX_LEVEL);

        UpLevelButton.AddComponent<SlideBottomToTopAnimation>();
        UpMaxLevelButton.AddComponent<SlideBottomToTopAnimation>();
        material1Group.AddComponent<SlideBottomToTopAnimation>();
        material2Group.AddComponent<SlideBottomToTopAnimation>();
        material3Group.AddComponent<SlideBottomToTopAnimation>();
        material4Group.AddComponent<SlideBottomToTopAnimation>();

        ReactorNumberText.text = "14";
        titleText.text = LocalizationManager.Get(AppDisplayConstants.ScienceFiction.REACTOR_NUMBER_14);

        // Texture conduitTexture = Resources.Load<Texture>("UI/Background2/Conduit_1");
        // leftSideCounduit1Image.texture = conduitTexture;
        // leftSideCounduit2Image.texture = conduitTexture;
        // rightSideCounduit1Image.texture = conduitTexture;
        // rightSideCounduit2Image.texture = conduitTexture;

        MainReactorBackgroundImage.AddComponent<RotateAnimation>();

        Texture mainReactorTexture = Resources.Load<Texture>("UI/Background2/Reactor_1");
        MainReactorImage.texture = mainReactorTexture;

        // RotateAnimation anim = MainReactorCoreImage.AddComponent<RotateAnimation>();
        // anim.direction = 1;
        leftSideCounduit1Image.AddComponent<SlideLeftToRightAnimation>();
        leftSideCounduit2Image.AddComponent<SlideLeftToRightAnimation>();
        rightSideCounduit1Image.AddComponent<SlideRightToLeftAnimation>();
        rightSideCounduit2Image.AddComponent<SlideRightToLeftAnimation>();

        ScienceFiction scienceFiction = await ScienceFictionService.Create().GetScienceFictionAsync(AppConstants.ScienceFiction.REACTOR_NUMBER_14);
        RankService rankService = new RankService();
        List<Items> items = new List<Items>();
        items.Add(await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.REACTOR_MATERIAL_NUMBER_1));
        items.Add(await UserItemsService.Create().GetUserItemByNameAsync(ItemConstants.REACTOR_MATERIAL_NUMBER_5));

        ReactorLevelText.text = scienceFiction.Level.ToString();

        double oneMaterialQuantity = EvaluateItem.CalculateToMaterialRequiredForOneUpgrade(scienceFiction.Level);
        double maxMaterialQuantity = EvaluateItem.CalculateTotalMaterialRequiredForMaxUpgrade(scienceFiction.Level, maxLevel, items);

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
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();

        UpLevelButton.onClick.AddListener(async () =>
        {
            double materialRequired = EvaluateItem.CalculateToMaterialRequiredForOneUpgrade(scienceFiction.Level);

            // Check nếu đã max level thì dừng
            if (scienceFiction.Level >= maxLevel) return;

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
                await ScienceFictionService.Create().InsertOrUpdateScienceFictionAsync(User.CurrentUserId, newScienceFiction, AppConstants.ScienceFiction.REACTOR_NUMBER_10);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(currentObject);
                await CreateReactorPanelAsync();
            }
        });
        UpMaxLevelButton.onClick.AddListener(async () =>
        {
            double totalMaterialRequired = EvaluateItem.CalculateTotalMaterialRequiredForMaxUpgrade(scienceFiction.Level, maxLevel, items);

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
            while (tempCost < totalMaterialRequired && currentLevel < maxLevel)
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

            await ScienceFictionService.Create().InsertOrUpdateScienceFictionAsync(User.CurrentUserId, newScienceFiction, AppConstants.ScienceFiction.REACTOR_NUMBER_10);
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

        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(itemImage)}");
        image.texture = texture;

        if (level >= maxLevel)
        {
            requiredQuantityText.text = "MAX";
            return;
        }
    }
    public void CreateMaterialForMaxLevel(RawImage image, TextMeshProUGUI availableQuantityText, TextMeshProUGUI requiredQuantityText, string itemImage, int level = 0, double userMaterialQuantity = 0, double materialQuantity = 0)
    {
        
        availableQuantityText.text = userMaterialQuantity.ToString();
        requiredQuantityText.text = materialQuantity.ToString();

        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(itemImage)}");
        image.texture = texture;

        if (level >= maxLevel)
        {
            requiredQuantityText.text = "MAX";
            return;
        }
    }
}
