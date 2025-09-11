using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserCardAdmiralsController : MonoBehaviour
{
    public static UserCardAdmiralsController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject cardsPrefab;
    private GameObject PositionPrefab;
    private GameObject ElementDetails2Prefab;
    private double increasePerLevel = 0.01;
    private double increasePerUpgrade = 1.1;
    private TeamsService teamsService;
    private UserItemsService userItemsService;
    private GameObject PopupSpiritBeastPanelPrefab;
    private GameObject EquipmentsWearingPrefab;
    private GameObject popupSpiritBeastObject;
    private GameObject tempCurrentObject;
    private int pageSize;
    private int offset;
    private int currentPage;
    private int totalPage;
    private string statusToggle;
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
        cardsPrefab = UIManager.Instance.GetGameObject("CardsPrefab");
        PositionPrefab = UIManager.Instance.GetGameObject("PositionPrefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");
        PopupSpiritBeastPanelPrefab = UIManager.Instance.GetGameObject("PopupSpiritBeastPanelPrefab");
        EquipmentsWearingPrefab = UIManager.Instance.GetGameObject("EquipmentsWearingPrefab");
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateUserCardAdmirals(List<CardAdmirals> cardAdmirals, Transform DictionaryContentPanel)
    {
        foreach (var admirals in cardAdmirals)
        {
            GameObject spellObject = Instantiate(cardsPrefab, DictionaryContentPanel);

            Text Title = spellObject.transform.Find("Title").GetComponent<Text>();
            Title.text = admirals.name.Replace("_", " ");

            RawImage Image = spellObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(admirals.image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            Button button = spellObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                MainMenuDetailsManager.Instance.PopupDetails(admirals, MainPanel);
            });

            RawImage rareImage = spellObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{admirals.rare}");
            rareImage.texture = rareTexture;
        }
        GridLayoutGroup gridLayout = DictionaryContentPanel.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(200, 250);
        }
        DictionaryContentPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    public void CreateUserCardAdmiralsForSummon(List<CardAdmirals> admirals, Transform PositionPanel)
    {
        foreach (var admiral in admirals)
        {
            GameObject captainObject = Instantiate(PositionPrefab, PositionPanel);

            RawImage Image = captainObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(admiral.image);
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            // Chỉnh width và height
            RectTransform rectTransform = Image.rectTransform;
            rectTransform.sizeDelta = new Vector2(300f, 375f);

            // Chỉnh vị trí cao lên 40px
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = new Vector2(currentPosition.x, currentPosition.y + 50f);
        }
    }
    public void ShowCardAdmiralsDetails(CardAdmirals cardAdmirals, GameObject currentObject, int buttonType = 1)
    {
        tempCurrentObject = currentObject;
        Transform RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        ButtonLoader.Instance.CreateButton(1, "Details", RightButtonContent);
        ButtonLoader.Instance.CreateButton(2, "Level", RightButtonContent);
        ButtonLoader.Instance.CreateButton(3, "Skills", RightButtonContent);
        ButtonLoader.Instance.CreateButton(4, "Upgrade", RightButtonContent);
        ButtonLoader.Instance.CreateButton(5, "Spirit Beast", RightButtonContent);
        ButtonLoader.Instance.CreateButton(6, "Rank", RightButtonContent);
        ButtonLoader.Instance.CreateButton(7, AppConstants.Master.MasterOfBeast, RightButtonContent);
        ButtonLoader.Instance.CreateButton(8, AppConstants.Master.MasterOfDragon, RightButtonContent);
        ButtonLoader.Instance.CreateButton(9, AppConstants.Master.MasterOfMagic, RightButtonContent);
        ButtonLoader.Instance.CreateButton(10, AppConstants.Master.MasterOfMusic, RightButtonContent);
        ButtonLoader.Instance.CreateButton(11, AppConstants.Master.MasterOfScience, RightButtonContent);
        ButtonLoader.Instance.CreateButton(12, AppConstants.Master.MasterOfSpirit, RightButtonContent);
        ButtonLoader.Instance.CreateButton(13, AppConstants.Master.MasterOfWeapon, RightButtonContent);
        ButtonLoader.Instance.CreateButton(14, AppConstants.Master.MasterOfChemical, RightButtonContent);
        ButtonLoader.Instance.CreateButton(15, AppConstants.Master.MasterOfPhysical, RightButtonContent);
        ButtonLoader.Instance.CreateButton(16, AppConstants.Master.MasterOfAtomic, RightButtonContent);
        ButtonLoader.Instance.CreateButton(17, AppConstants.Master.MasterOfMental, RightButtonContent);

        ButtonEvent.Instance.AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_5", RightButtonContent, () =>
        {
            GetSpiritBeast(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_5", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_6", RightButtonContent, () =>
        {
            GetRank(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_6", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_7", RightButtonContent, () =>
        {
            GetMasterOfBeast(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_7", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_8", RightButtonContent, () =>
        {
            GetMasterOfDragon(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_8", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_9", RightButtonContent, () =>
        {
            GetMasterOfMagic(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_9", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_10", RightButtonContent, () =>
        {
            GetMasterOfMusic(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_10", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_11", RightButtonContent, () =>
        {
            GetMasterOfScience(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_11", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_12", RightButtonContent, () =>
        {
            GetMasterOfSpirit(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_12", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_13", RightButtonContent, () =>
        {
            GetMasterOfWeapon(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_13", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_14", RightButtonContent, () =>
        {
            GetMasterOfChemical(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_14", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_15", RightButtonContent, () =>
        {
            GetMasterOfPhysical(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_15", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_16", RightButtonContent, () =>
        {
            GetMasterOfAtomic(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_16", RightButtonContent);
        });
        ButtonEvent.Instance.AssignButtonEvent("Button_17", RightButtonContent, () =>
        {
            GetMasterOfMental(cardAdmirals, currentObject);
            ButtonLoader.Instance.OnButtonClicked("Button_17", RightButtonContent);
        });

        switch (buttonType)
        {
            case 1:
                GetDetails(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
            case 2:
                GetLevel(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_2", RightButtonContent);
                break;
            case 3:
                GetSkills(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_3", RightButtonContent);
                break;
            case 4:
                GetUpgrade(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_4", RightButtonContent);
                break;
            case 5:
                GetSpiritBeast(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_5", RightButtonContent);
                break;
            case 6:
                GetRank(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_6", RightButtonContent);
                break;
            case 7:
                GetMasterOfBeast(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_7", RightButtonContent);
                break;
            case 8:
                GetMasterOfDragon(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_8", RightButtonContent);
                break;
            case 9:
                GetMasterOfMagic(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_9", RightButtonContent);
                break;
            case 10:
                GetMasterOfMusic(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_10", RightButtonContent);
                break;
            case 11:
                GetMasterOfScience(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_11", RightButtonContent);
                break;
            case 12:
                GetMasterOfSpirit(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_12", RightButtonContent);
                break;
            case 13:
                GetMasterOfWeapon(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_13", RightButtonContent);
                break;
            case 14:
                GetMasterOfChemical(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_14", RightButtonContent);
                break;
            case 15:
                GetMasterOfPhysical(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_15", RightButtonContent);
                break;
            case 16:
                GetMasterOfAtomic(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_16", RightButtonContent);
                break;
            case 17:
                GetMasterOfMental(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_17", RightButtonContent);
                break;
            default:
                GetDetails(cardAdmirals, currentObject);
                ButtonLoader.Instance.OnButtonClicked("Button_1", RightButtonContent);
                break;
        }
        RightButtonContent.gameObject.AddComponent<SlideRightToLeftAnimation>();
    }
    public void CreateDetailsUI(CardAdmirals cardAdmirals, GameObject currentObject)
    {
        RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardAdmirals.image); // Lấy giá trị của image từ đối tượng Card
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        Image.texture = texture;

        TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
        name.text = cardAdmirals.name;

        TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
        power.text = NumberFormatter.FormatNumber(cardAdmirals.all_power, false);

        // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
        // level.text = cardAdmirals.level.ToString();

        RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
        Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardAdmirals.rare}");
        rareImage.texture = rareTexture;
    }
    public void GetDetails(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonDetailsPanels();
        if (obj is CardAdmirals cardAdmirals)
        {
            CreateDetailsUI(cardAdmirals, currentObject);
            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardAdmirals.GetType().GetProperties();
            UIManager.Instance.CreatePropertyUI(1, properties, cardAdmirals, currentObject);
        }
    }
    public void GetLevel(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonLevelPanels();
        Button up1LevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        Transform LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        Transform LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        if (obj is CardAdmirals cardAdmirals)
        {
            PropertyInfo[] properties = cardAdmirals.GetType().GetProperties();
            UIManager.Instance.CreatePropertyLevelUI(properties, cardAdmirals, increasePerLevel, currentObject);

            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForLevel("CardAdmirals");
            UIManager.Instance.CreateMaterialUI(items, currentObject);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CardAdmirals currentCard = new CardAdmirals();
                currentCard = UserCardAdmiralsService.Create().GetUserCardAdmiralsById(User.CurrentUserId, cardAdmirals.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = MainMenuDetailsManager.Instance.UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardAdmirals newCard = new CardAdmirals();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newCard = UserCardAdmiralsService.Create().GetNewLevelPower(cardAdmirals, increasePerLevel);
                    UserCardAdmiralsService.Create().UpdateCardAdmiralsLevel(newCard, currentLevel + 1);
                    UserCardAdmiralsService.Create().UpdateFactCardAdmirals(newCard);
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
                CardAdmirals currentCard = UserCardAdmiralsService.Create().GetUserCardAdmiralsById(User.CurrentUserId, cardAdmirals.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
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
                    CardAdmirals newCard = UserCardAdmiralsService.Create().GetNewLevelPower(cardAdmirals, levelsGained * increasePerLevel);
                    UserCardAdmiralsService.Create().UpdateCardAdmiralsLevel(newCard, currentLevel);
                    UserCardAdmiralsService.Create().UpdateFactCardAdmirals(newCard);
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
        if (obj is CardAdmirals cardAdmirals)
        {
            PropertyInfo[] properties = cardAdmirals.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardAdmirals, null);
                UIManager.Instance.CreatePropertyUpgradeUI(property, value, increasePerUpgrade, currentObject);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = userItemsService.GetItemForBreakthourgh("CardAdmirals");
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(items1.image);
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardAdmirals.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardAdmirals.image);
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardAdmirals.quantity.ToString() + "/" + (cardAdmirals.star + 1).ToString();

            UIManager.Instance.CreateStarUI(cardAdmirals.star, currentObject);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = cardAdmirals.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = cardAdmirals.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + cardAdmirals.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (cardAdmirals.quantity >= requiredQuantity)
                    {
                        cardAdmirals.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - cardAdmirals.quantity;
                        cardAdmirals.quantity = 0; // Dùng hết thẻ bài

                        foreach (Items items1 in items)
                        {
                            if (remainingRequired <= 0) break; // Đã đủ vật phẩm để nâng cấp

                            if (items1.quantity >= remainingRequired)
                            {
                                items1.quantity -= remainingRequired;
                                remainingRequired = 0;
                            }
                            else
                            {
                                remainingRequired -= items1.quantity;
                                items1.quantity = 0; // Dùng hết vật phẩm này
                            }
                        }
                    }

                    foreach (Items items1 in items)
                    {
                        userItemsService.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardAdmirals newCard = new CardAdmirals();

                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    newCard = UserCardAdmiralsService.Create().GetNewBreakthroughPower(cardAdmirals, increasePerUpgrade);
                    UserCardAdmiralsService.Create().UpdateCardAdmiralsBreakthrough(newCard, cardAdmirals.star + 1, cardAdmirals.quantity);
                    UserCardAdmiralsService.Create().UpdateFactCardAdmirals(newCard);
                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    CardAdmiralsGalleryService.Create().UpdateStarCardAdmiralsGallery(cardAdmirals.id, cardAdmirals.star + 1);

                    // Cập nhật giao diện
                    ButtonEvent.Instance.Close(UpgradeElementContent);
                    ButtonEvent.Instance.Close(UpgradeMaterialContent);
                    GetUpgrade(obj, currentObject);
                    UIManager.Instance.CreateStarUI(cardAdmirals.star, currentObject);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
    }
    public void GetSpiritBeast(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonSpiritBeastPanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/Background1").GetComponent<RawImage>();
        Button addButton = currentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/AddButton").GetComponent<Button>();
        Button removeButton = currentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/RemoveButton").GetComponent<Button>();

        pageSize = 100;
        offset = 0;
        currentPage = 1;

        background1Image.gameObject.AddComponent<RotateAnimation>();

        if (obj is CardAdmirals cardAdmirals)
        {
            var userCardSpiritBeast = UserSpiritBeastService.Create().GetUserCardAdmiralsSpiritBeast(User.CurrentUserId, cardAdmirals);
            RawImage spiritBeastImage = currentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = userCardSpiritBeast.image != null
                ? ImageExtensionHandler.RemoveImageExtension(userCardSpiritBeast.image)
                : "UI/Background4/Background_V4_352";
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            spiritBeastImage.texture = texture;

            CreateDetailsUI(cardAdmirals, currentObject);
            addButton.onClick.AddListener(() =>
            {
                CreatePopupEquipments(obj, currentObject);
            });
            removeButton.onClick.AddListener(() =>
            {
                UserSpiritBeastService.Create().DeleteUserCardAdmiralsSpiritBeast(User.CurrentUserId, cardAdmirals, userCardSpiritBeast);
                string fileNameWithoutExtension = "UI/Background4/Background_V4_352";
                Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                spiritBeastImage.texture = texture;

                var card = UserCardAdmiralsService.Create().GetUserCardAdmiralsById(User.CurrentUserId, cardAdmirals.id);
                ShowCardAdmiralsDetails(card, currentObject, 5);
            });
        }
    }
    public void CreatePopupEquipments(object data, GameObject currentObject, string statusToggle = "NOT EQUIP")
    {
        popupSpiritBeastObject = Instantiate(PopupSpiritBeastPanelPrefab, MainPanel);
        Transform contentPanel = popupSpiritBeastObject.transform.Find("Scroll View/Viewport/Content");
        Text PageText = popupSpiritBeastObject.transform.Find("Pagination/Page").GetComponent<Text>();
        Toggle toggle = popupSpiritBeastObject.transform.Find("Toggle").GetComponent<Toggle>();
        toggle.isOn = (statusToggle == "ALL");
        toggle.onValueChanged.AddListener((bool isOn) =>
        {
            string newStatusToggle = isOn ? "ALL" : "NOT EQUIP";
            Destroy(popupSpiritBeastObject);
            CreatePopupEquipments(data, currentObject, newStatusToggle); // Gọi lại nhưng giữ statusToggle mới
        });
        Button NextButton = popupSpiritBeastObject.transform.Find("Pagination/Next").GetComponent<Button>();
        Button PreviousButton = popupSpiritBeastObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        Button CloseButton = popupSpiritBeastObject.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.RemoveAllListeners();
        CloseButton.onClick.AddListener(() => Destroy(popupSpiritBeastObject));
        Equipments equipments = new Equipments();
        List<SpiritBeast> spiritBeasts = new List<SpiritBeast>();
        spiritBeasts = UserSpiritBeastService.Create().GetAllUserCardAdmiralsSpiritBeast(User.CurrentUserId, pageSize, offset, statusToggle);

        int totalRecord = UserSpiritBeastService.Create().GetUserSpiritBeastCount(User.CurrentUserId, AppConstants.Rare.All);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        CreatePopupEquipmentsUI(data, spiritBeasts, contentPanel, currentObject);
        NextButton.onClick.RemoveAllListeners();
        PreviousButton.onClick.RemoveAllListeners();
        NextButton.onClick.AddListener(() => { ChangeNextPage(data, PageText, contentPanel, currentObject); });
        PreviousButton.onClick.AddListener(() => { ChangePreviousPage(data, PageText, contentPanel, currentObject); });
    }
    public int CalculateTotalPages(int totalRecords, int pageSize)
    {
        if (pageSize <= 0) return 0; // Đảm bảo pageSize không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / pageSize);
    }
    public void CreatePopupEquipmentsUI(object data, List<SpiritBeast> spiritBeasts, Transform content, GameObject currentObject)
    {
        foreach (var spiritBeast in spiritBeasts)
        {
            GameObject equipmentObject = Instantiate(EquipmentsWearingPrefab, content);

            TextMeshProUGUI Title = equipmentObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = spiritBeast.name.Replace("_", " ");

            TextMeshProUGUI Power = equipmentObject.transform.Find("PowerText").GetComponent<TextMeshProUGUI>();
            Power.text = spiritBeast.power.ToString();

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = spiritBeast.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spiritBeast.rare}");
            rareImage.texture = rareTexture;

            Button EquipButton = equipmentObject.transform.Find("EquipButton").GetComponent<Button>();
            EquipButton.onClick.AddListener(() =>
            {
                Destroy(popupSpiritBeastObject);
                if (data is CardAdmirals cardAdmirals)
                {
                    double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    UserSpiritBeastService.Create().InsertOrUpdateUserCardAdmiralsSpiritBeast(User.CurrentUserId, cardAdmirals, spiritBeast);

                    RawImage spiritBeastImage = tempCurrentObject.transform.Find("DictionaryCards/Content/SpiritBeastPanel/Image").GetComponent<RawImage>();
                    var userCardSpiritBeast = UserSpiritBeastService.Create().GetUserCardAdmiralsSpiritBeast(User.CurrentUserId, cardAdmirals);
                    string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(userCardSpiritBeast.image);
                    Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                    spiritBeastImage.texture = texture;

                    double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    var card = UserCardAdmiralsService.Create().GetUserCardAdmiralsById(User.CurrentUserId, cardAdmirals.id);
                    ShowCardAdmiralsDetails(card, currentObject, 5);
                }

                Destroy(popupSpiritBeastObject);
            });
        }
        GridLayoutGroup gridLayout = content.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(340, 130);
        }
    }
    public void ChangeNextPage(object data, Text PageText, Transform content, GameObject currentObject)
    {
        if (currentPage < totalPage)
        {
            ButtonEvent.Instance.Close(content);
            int totalRecord = 0;

            totalRecord = UserSpiritBeastService.Create().GetUserSpiritBeastCount(User.CurrentUserId, AppConstants.Rare.All);
            totalPage = CalculateTotalPages(totalRecord, pageSize);
            currentPage = currentPage + 1;
            offset = offset + pageSize;
            List<SpiritBeast> spiritBeasts = UserSpiritBeastService.Create().GetAllUserCardAdmiralsSpiritBeast(User.CurrentUserId, pageSize, offset, statusToggle);
            CreatePopupEquipmentsUI(data, spiritBeasts, content, currentObject);

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ChangePreviousPage(object data, Text PageText, Transform content, GameObject currentObject)
    {
        if (currentPage > 1)
        {
            ButtonEvent.Instance.Close(content);
            int totalRecord = 0;

            totalRecord = UserSpiritBeastService.Create().GetUserSpiritBeastCount(User.CurrentUserId, AppConstants.Rare.All);
            totalPage = CalculateTotalPages(totalRecord, pageSize);
            currentPage = currentPage - 1;
            offset = offset - pageSize;
            List<SpiritBeast> spiritBeasts = UserSpiritBeastService.Create().GetAllUserCardAdmiralsSpiritBeast(User.CurrentUserId, pageSize, offset, statusToggle);
            CreatePopupEquipmentsUI(data, spiritBeasts, content, currentObject);

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void GetRank(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonRankPanels();
    }
    public void GetMasterOfBeast(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonMasterOfBeastPanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/MasterOfBeastPanel/Background1").GetComponent<RawImage>();
        RawImage image = currentObject.transform.Find("DictionaryCards/Content/MasterOfBeastPanel/Image").GetComponent<RawImage>();
        Button upgradeButton = currentObject.transform.Find("DictionaryCards/Content/MasterOfBeastPanel/UpgradeButton").GetComponent<Button>();

        background1Image.gameObject.AddComponent<RotateAnimation>();
        Texture texture = Resources.Load<Texture>("UI/Card/Master Of Beast");
        image.texture = texture;

        upgradeButton.onClick.AddListener(() =>
        {
            MasterOfBeastManager.Instance.CreateMasterOfBeastManager(obj);
        });
    }
    public void GetMasterOfDragon(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonMasterOfDragonPanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/MasterOfDragonPanel/Background1").GetComponent<RawImage>();
        RawImage image = currentObject.transform.Find("DictionaryCards/Content/MasterOfDragonPanel/Image").GetComponent<RawImage>();
        Button upgradeButton = currentObject.transform.Find("DictionaryCards/Content/MasterOfDragonPanel/UpgradeButton").GetComponent<Button>();

        background1Image.gameObject.AddComponent<RotateAnimation>();
        Texture texture = Resources.Load<Texture>("UI/Card/Master Of Dragon");
        image.texture = texture;

        upgradeButton.onClick.AddListener(() =>
        {
            MasterOfDragonManager.Instance.CreateMasterOfDragonManager(obj);
        });
    }
    public void GetMasterOfMagic(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonMasterOfMagicPanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/MasterOfMagicPanel/Background1").GetComponent<RawImage>();
        RawImage image = currentObject.transform.Find("DictionaryCards/Content/MasterOfMagicPanel/Image").GetComponent<RawImage>();
        Button upgradeButton = currentObject.transform.Find("DictionaryCards/Content/MasterOfMagicPanel/UpgradeButton").GetComponent<Button>();

        background1Image.gameObject.AddComponent<RotateAnimation>();
        Texture texture = Resources.Load<Texture>("UI/Card/Master Of Magic");
        image.texture = texture;

        upgradeButton.onClick.AddListener(() =>
        {
            MasterOfMagicManager.Instance.CreateMasterOfMagicManager(obj);
        });
    }
    public void GetMasterOfMusic(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonMasterOfMusicPanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/MasterOfMusicPanel/Background1").GetComponent<RawImage>();
        RawImage image = currentObject.transform.Find("DictionaryCards/Content/MasterOfMusicPanel/Image").GetComponent<RawImage>();
        Button upgradeButton = currentObject.transform.Find("DictionaryCards/Content/MasterOfMusicPanel/UpgradeButton").GetComponent<Button>();

        background1Image.gameObject.AddComponent<RotateAnimation>();
        Texture texture = Resources.Load<Texture>("UI/Card/Master Of Music");
        image.texture = texture;

        upgradeButton.onClick.AddListener(() =>
        {
            MasterOfMusicManager.Instance.CreateMasterOfMusicManager(obj);
        });
    }
    public void GetMasterOfScience(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonMasterOfSciencePanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/MasterOfSciencePanel/Background1").GetComponent<RawImage>();
        RawImage image = currentObject.transform.Find("DictionaryCards/Content/MasterOfSciencePanel/Image").GetComponent<RawImage>();
        Button upgradeButton = currentObject.transform.Find("DictionaryCards/Content/MasterOfSciencePanel/UpgradeButton").GetComponent<Button>();

        background1Image.gameObject.AddComponent<RotateAnimation>();
        Texture texture = Resources.Load<Texture>("UI/Card/Master Of Science");
        image.texture = texture;

        upgradeButton.onClick.AddListener(() =>
        {
            MasterOfScienceManager.Instance.CreateMasterOfScienceManager(obj);
        });
    }
    public void GetMasterOfSpirit(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonMasterOfSpiritPanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/MasterOfSpiritPanel/Background1").GetComponent<RawImage>();
        RawImage image = currentObject.transform.Find("DictionaryCards/Content/MasterOfSpiritPanel/Image").GetComponent<RawImage>();
        Button upgradeButton = currentObject.transform.Find("DictionaryCards/Content/MasterOfSpiritPanel/UpgradeButton").GetComponent<Button>();

        background1Image.gameObject.AddComponent<RotateAnimation>();
        Texture texture = Resources.Load<Texture>("UI/Card/Master Of Spirit");
        image.texture = texture;

        upgradeButton.onClick.AddListener(() =>
        {
            MasterOfSpiritManager.Instance.CreateMasterOfSpiritManager(obj);
        });
    }
    public void GetMasterOfWeapon(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonMasterOfWeaponPanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/MasterOfWeaponPanel/Background1").GetComponent<RawImage>();
        RawImage image = currentObject.transform.Find("DictionaryCards/Content/MasterOfWeaponPanel/Image").GetComponent<RawImage>();
        Button upgradeButton = currentObject.transform.Find("DictionaryCards/Content/MasterOfWeaponPanel/UpgradeButton").GetComponent<Button>();

        background1Image.gameObject.AddComponent<RotateAnimation>();
        Texture texture = Resources.Load<Texture>("UI/Card/Master Of Weapon");
        image.texture = texture;

        upgradeButton.onClick.AddListener(() =>
        {
            MasterOfWeaponManager.Instance.CreateMasterOfWeaponManager(obj);
        });
    }
    public void GetMasterOfChemical(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonMasterOfChemicalPanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/MasterOfChemicalPanel/Background1").GetComponent<RawImage>();
        RawImage image = currentObject.transform.Find("DictionaryCards/Content/MasterOfChemicalPanel/Image").GetComponent<RawImage>();
        Button upgradeButton = currentObject.transform.Find("DictionaryCards/Content/MasterOfChemicalPanel/UpgradeButton").GetComponent<Button>();

        background1Image.gameObject.AddComponent<RotateAnimation>();
        Texture texture = Resources.Load<Texture>("UI/Card/Master Of Chemical");
        image.texture = texture;

        upgradeButton.onClick.AddListener(() =>
        {
            MasterOfChemicalManager.Instance.CreateMasterOfChemicalManager(obj);
        });
    }
    public void GetMasterOfPhysical(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonMasterOfPhysicalPanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/MasterOfPhysicalPanel/Background1").GetComponent<RawImage>();
        RawImage image = currentObject.transform.Find("DictionaryCards/Content/MasterOfPhysicalPanel/Image").GetComponent<RawImage>();
        Button upgradeButton = currentObject.transform.Find("DictionaryCards/Content/MasterOfPhysicalPanel/UpgradeButton").GetComponent<Button>();

        background1Image.gameObject.AddComponent<RotateAnimation>();
        Texture texture = Resources.Load<Texture>("UI/Card/Master Of Physical");
        image.texture = texture;

        upgradeButton.onClick.AddListener(() =>
        {
            MasterOfPhysicalManager.Instance.CreateMasterOfPhysicalManager(obj);
        });
    }
    public void GetMasterOfAtomic(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonMasterOfAtomicPanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/MasterOfAtomicPanel/Background1").GetComponent<RawImage>();
        RawImage image = currentObject.transform.Find("DictionaryCards/Content/MasterOfAtomicPanel/Image").GetComponent<RawImage>();
        Button upgradeButton = currentObject.transform.Find("DictionaryCards/Content/MasterOfAtomicPanel/UpgradeButton").GetComponent<Button>();

        background1Image.gameObject.AddComponent<RotateAnimation>();
        Texture texture = Resources.Load<Texture>("UI/Card/Master Of Atomic");
        image.texture = texture;

        upgradeButton.onClick.AddListener(() =>
        {
            MasterOfAtomicManager.Instance.CreateMasterOfAtomicManager(obj);
        });
    }
    public void GetMasterOfMental(object obj, GameObject currentObject)
    {
        MainMenuDetailsManager.Instance.HideNonMasterOfMentalPanels();
        RawImage background1Image = currentObject.transform.Find("DictionaryCards/Content/MasterOfMentalPanel/Background1").GetComponent<RawImage>();
        RawImage image = currentObject.transform.Find("DictionaryCards/Content/MasterOfMentalPanel/Image").GetComponent<RawImage>();
        Button upgradeButton = currentObject.transform.Find("DictionaryCards/Content/MasterOfMentalPanel/UpgradeButton").GetComponent<Button>();

        background1Image.gameObject.AddComponent<RotateAnimation>();
        Texture texture = Resources.Load<Texture>("UI/Card/Master Of Mental");
        image.texture = texture;

        upgradeButton.onClick.AddListener(() =>
        {
            MasterOfMentalManager.Instance.CreateMasterOfMentalManager(obj);
        });
    }
}
