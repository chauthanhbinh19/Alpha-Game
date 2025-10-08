using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReactorNumber1Manager : MonoBehaviour
{
    private Transform MainPanel;
    public GameObject ReactorPanelNumberPrefab;
    private TMP_FontAsset EuroStyleNormalFont;
    private int fontSize;
    TeamsService teamsService;
    UserItemsService userItemsService;
    private int maxLevel;
    public static ReactorNumber1Manager Instance { get; private set; }
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ReactorPanelNumberPrefab = UIManager.Instance.GetGameObjectScienceFiction("ReactorPanelNumberPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
        fontSize = 26;
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
        maxLevel = 10000;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateReactorPanel()
    {
        GameObject currentObject = Instantiate(ReactorPanelNumberPrefab, MainPanel);
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            ButtonEvent.Instance.Close(MainPanel);
        });
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            Destroy(currentObject);
        });

        RawImage leftSideCounduit1Image = currentObject.transform.Find("DictionaryCards/LeftSideConduit1/LeftSideConduiltImage").GetComponent<RawImage>();
        RawImage leftSideCounduit2Image = currentObject.transform.Find("DictionaryCards/LeftSideConduit2/LeftSideConduiltImage").GetComponent<RawImage>();
        RawImage rightSideCounduit1Image = currentObject.transform.Find("DictionaryCards/RightSideConduit1/RightSideConduiltImage").GetComponent<RawImage>();
        RawImage rightSideCounduit2Image = currentObject.transform.Find("DictionaryCards/RightSideConduit2/RightSideConduiltImage").GetComponent<RawImage>();
        RawImage MainReactorBackgroundImage = currentObject.transform.Find("DictionaryCards/MainReactorBackgroundCircle/MainReactorImage").GetComponent<RawImage>();
        RawImage MainReactorImage = currentObject.transform.Find("DictionaryCards/MainReactor/MainReactorImage").GetComponent<RawImage>();
        RawImage MainReactorCoreImage = currentObject.transform.Find("DictionaryCards/MainReactorBackgroundCore/MainReactorCoreImage").GetComponent<RawImage>();
        TextMeshProUGUI ReactorLevelText = currentObject.transform.Find("DictionaryCards/ReactorLevel/ReactorLevelText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI ReactorNumberText = currentObject.transform.Find("DictionaryCards/ReactorLevel/ReactorNumberText").GetComponent<TextMeshProUGUI>();
        Button UpLevelButton = currentObject.transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        Button UpMaxLevelButton = currentObject.transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Transform material1Group = currentObject.transform.Find("DictionaryCards/MaterialNumber1");
        Transform material2Group = currentObject.transform.Find("DictionaryCards/MaterialNumber2");
        RawImage material1Image = currentObject.transform.Find("DictionaryCards/MaterialNumber1/MaterialImage").GetComponent<RawImage>();
        RawImage material2Image = currentObject.transform.Find("DictionaryCards/MaterialNumber2/MaterialImage").GetComponent<RawImage>();
        TextMeshProUGUI quantity1Text = currentObject.transform.Find("DictionaryCards/MaterialNumber1/QuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI quantity2Text = currentObject.transform.Find("DictionaryCards/MaterialNumber2/QuantityText").GetComponent<TextMeshProUGUI>();

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);

        UpLevelButton.AddComponent<SlideBottomToTopAnimation>();
        UpMaxLevelButton.AddComponent<SlideBottomToTopAnimation>();
        material1Group.AddComponent<SlideBottomToTopAnimation>();
        material2Group.AddComponent<SlideBottomToTopAnimation>();

        ReactorNumberText.text = "01";

        Texture conduitTexture = Resources.Load<Texture>("UI/Background2/Conduit_1");
        leftSideCounduit1Image.texture = conduitTexture;
        leftSideCounduit2Image.texture = conduitTexture;
        rightSideCounduit1Image.texture = conduitTexture;
        rightSideCounduit2Image.texture = conduitTexture;

        MainReactorBackgroundImage.AddComponent<RotateAnimation>();

        Texture mainReactorTexture = Resources.Load<Texture>("UI/Background2/Reactor_1");
        MainReactorImage.texture = mainReactorTexture;

        // RotateAnimation anim = MainReactorCoreImage.AddComponent<RotateAnimation>();
        // anim.direction = 1;
        leftSideCounduit1Image.AddComponent<SlideLeftToRightAnimation>();
        leftSideCounduit2Image.AddComponent<SlideLeftToRightAnimation>();
        rightSideCounduit1Image.AddComponent<SlideRightToLeftAnimation>();
        rightSideCounduit2Image.AddComponent<SlideRightToLeftAnimation>();

        ScienceFiction scienceFiction = ScienceFictionService.Create().GetScienceFiction(AppConstants.ScienceFiction.ReactorNumber1);
        RankService rankService = new RankService();
        List<Items> items = new List<Items>();
        items.Add(UserItemsService.Create().GetUserItemByName(ItemConstants.ReactorMaterialNumber1));
        items.Add(UserItemsService.Create().GetUserItemByName(ItemConstants.ReactorMaterialNumber2));

        for (int i = 0; i < items.Count; i++)
        {
            Items item = items[i];

            if (i == 0)
            {
                CreateMaterialUI(material1Image, quantity1Text, item.image, scienceFiction.level,item.quantity);
            }
            else if (i == 1)
            {
                CreateMaterialUI(material2Image, quantity2Text, item.image, scienceFiction.level,item.quantity);
            }
        }
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();

        UpLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000;
            int materialQuantity = (scienceFiction.level == 0)
                ? 1
                : (scienceFiction.level % levelsPerSkill == 0 ? levelsPerSkill : scienceFiction.level % levelsPerSkill);

            // Check nếu đã max level thì dừng
            if (scienceFiction.level >= maxLevel) return;

            // Kiểm tra xem tất cả items đều đủ số lượng
            bool hasAllMaterials = items.All(i => i.quantity >= materialQuantity);

            if (hasAllMaterials)
            {
                foreach (var i in items)
                {
                    i.quantity -= materialQuantity;
                    userItemsService.UpdateUserItemsQuantity(i);
                }

                ScienceFiction newScienceFiction = rankService.EnhanceScienceFiction(scienceFiction, 1);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                // rankService.UpLevel(cardHeroes, newRank, mainType);
                ScienceFictionService.Create().InsertOrUpdateScienceFiction(newScienceFiction, AppConstants.ScienceFiction.ReactorNumber1);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);

                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(currentObject);
                CreateReactorPanel();
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            int levelsPerSkill = 1000; // giả sử 1000, hoặc load từ config

            int finalLevel = int.MaxValue;

            // Tính số level tối đa có thể nâng cho tất cả items
            foreach (var i in items)
            {
                int maxLevelForItem = EvaluateItem.CalculateMaxMaterialLevel(i.quantity, scienceFiction.level);
                finalLevel = Math.Min(finalLevel, maxLevelForItem);
            }

            // Nếu không đủ nguyên liệu để nâng thì dừng
            if (finalLevel <= 0) return;

            // Giới hạn không vượt quá 10000
            if (scienceFiction.level + finalLevel > maxLevel)
            {
                finalLevel = maxLevel - scienceFiction.level;
            }

            // Kiểm tra & trừ nguyên liệu cho finalLevel
            foreach (var i in items)
            {
                int consume = EvaluateItem.CalculateRequiredQuantityForLevel(scienceFiction.level, finalLevel, levelsPerSkill);

                if (i.quantity < consume)
                {
                    // Không đủ nguyên liệu thì dừng
                    return;
                }

                i.quantity -= consume;
                userItemsService.UpdateUserItemsQuantity(i);
            }

            // Nâng cấp scienceFiction
            ScienceFiction newScienceFiction = rankService.EnhanceScienceFiction(scienceFiction, finalLevel);

            double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
            ScienceFictionService.Create().InsertOrUpdateScienceFiction(newScienceFiction, AppConstants.ScienceFiction.ReactorNumber1);
            double newPower = teamsService.GetTeamsPower(User.CurrentUserId);

            FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

            Destroy(currentObject);
            CreateReactorPanel();
        });
    }
    public void CreateMaterialUI(RawImage image, TextMeshProUGUI quantityText, string itemImage, int level = 0, int userMaterialQuantity = 0)
    {
        int levelsPerSkill = 1000;
        int materialQuantity = (level == 0) ? 1 : (level % levelsPerSkill == 0 ? levelsPerSkill : level % levelsPerSkill);
        quantityText.text = userMaterialQuantity + "/" + materialQuantity;

        Texture texture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(itemImage)}");
        image.texture = texture;
    }
}
