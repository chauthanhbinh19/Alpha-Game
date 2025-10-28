using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserArtworkController : MonoBehaviour
{
    public static UserArtworkController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ArtworkFirstPrefab;
    private GameObject ElementDetails2Prefab;
    private double increasePerLevel = 0.01;
    private double increasePerUpgrade = 1.1;
    private TeamsService teamsService;
    private UserItemsService userItemsService;
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

    // Update is called once per frame
    void Update()
    {

    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ArtworkFirstPrefab = UIManager.Instance.GetGameObject("ArtworkFirstPrefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }
    public void CreateUserArtwork(List<Artworks> alchemies, Transform DictionaryContentPanel)
    {
        foreach (var Artwork in alchemies)
        {
            GameObject ArtworkObject = Instantiate(ArtworkFirstPrefab, DictionaryContentPanel);

            Text Title = ArtworkObject.transform.Find("Title").GetComponent<Text>();
            Title.text = Artwork.Name.Replace("_", " ");

            RawImage Image = ArtworkObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Artwork.Image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = ArtworkObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                MainMenuDetailsManager.Instance.PopupDetails(Artwork, MainPanel);
            });

            RawImage frameImage = ArtworkObject.transform.Find("FrameImage").GetComponent<RawImage>();
            frameImage.gameObject.SetActive(true);

            RawImage rareImage = ArtworkObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{Artwork.Rare}");
            rareImage.texture = rareTexture;

        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(270, 220);
        }
        DictionaryContentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void ShowArtworkDetails(Artworks Artwork, GameObject currentObject, int buttonType = 1)
    {
        Transform RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        ButtonLoader.Instance.CreateButton(1, "Details", RightButtonContent);
        ButtonLoader.Instance.CreateButton(2, "Level", RightButtonContent);
        ButtonLoader.Instance.CreateButton(4, "Upgrade", RightButtonContent);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(Artwork, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(Artwork, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(Artwork, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
        });

        switch (buttonType)
        {
            case 1:
                GetDetails(Artwork, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
            case 2:
                GetLevel(Artwork, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
                break;
            case 3:
                GetSkills(Artwork, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
                break;
            case 4:
                GetUpgrade(Artwork, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
                break;
            default:
                GetDetails(Artwork, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
        }
        RightButtonContent.gameObject.AddComponent<SlideRightToLeftAnimation>();
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        if (obj is Artworks Artwork)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(Artwork.Image); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ImageManager.Instance.ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = Artwork.Name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = NumberFormatter.FormatNumber(Artwork.Power, false);

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{Artwork.Rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = Artwork.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, Artwork, currentObject);
        }
    }
    public void GetLevel(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonLevelPanels();
        Button up1LevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        Transform LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        Transform LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        if (obj is Artworks Artwork)
        {
            PropertyInfo[] properties = Artwork.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, Artwork, increasePerLevel, currentObject);

            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForLevel(AppConstants.MainType.ARTWORK);
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Artworks currentCard = new Artworks();
                currentCard = UserArtworkService.Create().GetUserArtworkById(User.CurrentUserId, Artwork.Id);
                double totalExperiment = currentCard.Experiment;
                int currentLevel = currentCard.Level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Artworks newCard = new Artworks();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newCard = UserArtworkService.Create().GetNewLevelPower(Artwork, increasePerLevel);
                    UserArtworkService.Create().UpdateArtworkLevel(newCard, currentLevel + 1);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    ButtonEvent.Instance.Close(LevelElementContent);
                    ButtonEvent.Instance.Close(LevelMaterialContent);
                    GetLevel(obj, currentObject);
                    UIManager.Instance.CreateLevelUI(currentLevel, currentObject);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                Artworks currentCard = UserArtworkService.Create().GetUserArtworkById(User.CurrentUserId, Artwork.Id);
                double totalExperiment = currentCard.Experiment;
                int currentLevel = currentCard.Level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = MainMenuDetailsManager.Instance.UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    Artworks newCard = UserArtworkService.Create().GetNewLevelPower(Artwork, levelsGained * increasePerLevel);
                    UserArtworkService.Create().UpdateArtworkLevel(newCard, currentLevel);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(LevelElementContent);
                    ButtonEvent.Instance.Close(LevelMaterialContent);
                    GetLevel(obj, currentObject);
                    UIManager.Instance.CreateLevelUI(currentLevel, currentObject);
                }
            });
        }
    }
    public void GetSkills(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonSkillsPanels();
    }
    public void GetUpgrade(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonUpgradePanels();
        Button breakthroughButton = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/BreakthroughButton").GetComponent<Button>();
        Transform UpgradeElementContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        Transform UpgradeMaterialContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewMaterial/Viewport/Content");
        if (obj is Artworks Artwork)
        {
            PropertyInfo[] properties = Artwork.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(Artwork, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, increasePerUpgrade, currentObject);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForBreakthourgh(AppConstants.MainType.ARTWORK);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.Image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.Quantity.ToString() + "/" + (Artwork.Star + 1).ToString();
            }
            GameObject magicFormationObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage magicFormationImage = magicFormationObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = Artwork.Image.Replace(".png", "");
            Texture magicFormationTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            magicFormationImage.texture = magicFormationTexture;

            TextMeshProUGUI magicFormationQuantity = magicFormationObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            magicFormationQuantity.text = Artwork.Quantity.ToString() + "/" + (Artwork.Star + 1).ToString();

            UIManager.Instance.CreateStarUI(Artwork.Star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                double requiredQuantity = Artwork.Star + 1;
                double totalItemQuantity = 0;

                // Kiểm tra số lượng vòng phép
                bool hasEnoughMagicFormation = Artwork.Quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.Quantity;
                }
                bool hasEnoughItems = totalItemQuantity + Artwork.Quantity >= requiredQuantity;

                if (hasEnoughMagicFormation || hasEnoughItems)
                {
                    // Giảm số lượng vòng phép trước
                    if (Artwork.Quantity >= requiredQuantity)
                    {
                        Artwork.Quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu vòng phép không đủ, dùng cả vòng phép + vật phẩm để bù vào
                        double remainingRequired = requiredQuantity - Artwork.Quantity;
                        Artwork.Quantity = 0; // Dùng hết vòng phép

                        foreach (Items items1 in items)
                        {
                            if (remainingRequired <= 0) break; // Đã đủ vật phẩm để nâng cấp

                            if (items1.Quantity >= remainingRequired)
                            {
                                items1.Quantity -= remainingRequired;
                                remainingRequired = 0;
                            }
                            else
                            {
                                remainingRequired -= items1.Quantity;
                                items1.Quantity = 0; // Dùng hết vật phẩm này
                            }
                        }
                    }

                    foreach (Items items1 in items)
                    {
                        userItemsService.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Artworks newArtwork = new Artworks();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newArtwork = UserArtworkService.Create().GetNewBreakthroughPower(Artwork, increasePerUpgrade);
                    UserArtworkService.Create().UpdateArtworkBreakthrough(newArtwork, Artwork.Star + 1, Artwork.Quantity);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    ArtworkGalleryService.Create().UpdateStarArtworkGallery(Artwork.Id, Artwork.Star + 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    GetUpgrade(obj, currentObject);
                    UIManager.Instance.CreateStarUI(Artwork.Star, currentObject);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp vòng phép!");
                }
            });
        }
    }
}
