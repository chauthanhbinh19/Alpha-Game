using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
using System;

public class MainMenuDetailsManager : MonoBehaviour
{
    private GameObject MainMenuDetailPanel2Prefab;
    private GameObject ElementDetailsPrefab;
    private GameObject NumberDetailPrefab;
    private GameObject NumberDetail2Prefab;
    private GameObject NumberDetail3Prefab;
    private Transform MainPanel;
    // private Transform RightButtonContent;
    private Transform DetailsPanel;
    private Transform LevelPanel;
    private Transform SkillsPanel;
    private Transform UpgradePanel;
    private Transform DetailsContent;
    private Transform LevelElementContent;
    private Transform LevelMaterialContent;
    private Transform SkillsContent;
    private Transform UpgradeElementContent;
    private Transform UpgradeMaterialContent;
    private GameObject currentObject;
    private GameObject ElementDetails2Prefab;
    // private GameObject firstDetailsObject;
    // private GameObject elementDetailsObject;
    // private GameObject elementDetails2Object;
    // private GameObject descriptionDetailsObject;
    private GameObject TabButton5;
    // private Transform firstPopupPanel;
    // private Transform elementPopupPanel;
    // private Transform element2PopupPanel;
    // private Transform descriptionPopupPanel;
    private Transform buttonGroupPanel1;
    private Transform buttonGroupPanel2;
    private Transform buttonGroupPanel3;
    private Transform setButtonGroupPanel;
    private RawImage CardBackground;
    private string mainType;
    // private double increasePerLevel = 0.01;
    // private double increasePerUpgrade = 1.1;
    // private TeamsService teamsService;
    // private UserItemsService userItemsService;
    public static MainMenuDetailsManager Instance { get; private set; }
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
        MainMenuDetailPanel2Prefab = UIManager.Instance.GetGameObject("MainMenuDetailPanel2Prefab");

        ElementDetailsPrefab = UIManager.Instance.GetGameObject("ElementDetailsPrefab");
        NumberDetailPrefab = UIManager.Instance.GetGameObject("NumberDetailPrefab");
        NumberDetail2Prefab = UIManager.Instance.GetGameObject("NumberDetail2Prefab");
        NumberDetail3Prefab = UIManager.Instance.GetGameObject("NumberDetail3Prefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");
        TabButton5 = UIManager.Instance.GetGameObject("TabButton5");

        // teamsService = TeamsService.Create();
        // userItemsService = UserItemsService.Create();

    }

    public void PopupDetails(object data, Transform panel)
    {
        MainPanel = panel;
        currentObject = Instantiate(MainMenuDetailPanel2Prefab, MainPanel);
        // RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        DetailsPanel = currentObject.transform.Find("DictionaryCards/Content/DetailsPanel");
        LevelPanel = currentObject.transform.Find("DictionaryCards/Content/LevelPanel");
        SkillsPanel = currentObject.transform.Find("DictionaryCards/Content/SkillsPanel");
        UpgradePanel = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel");
        DetailsContent = currentObject.transform.Find("DictionaryCards/Content/DetailsPanel/Scroll View/Viewport/Content");
        // LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        // LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        SkillsContent = currentObject.transform.Find("DictionaryCards/Content/SkillsPanel");
        UpgradeElementContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        UpgradeMaterialContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewMaterial/Viewport/Content");
        TextMeshProUGUI titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        buttonGroupPanel1 = currentObject.transform.Find("DictionaryCards/ButtonGroup1");
        buttonGroupPanel2 = currentObject.transform.Find("DictionaryCards/ButtonGroup2");
        buttonGroupPanel3 = currentObject.transform.Find("DictionaryCards/ButtonGroup3");
        setButtonGroupPanel = currentObject.transform.Find("DictionaryCards/SetButtonGroup");
        CardBackground = currentObject.transform.Find("DictionaryCards/Background").GetComponent<RawImage>();
        HomeButton.onClick.AddListener(() => ButtonEvent.Instance.Close(MainPanel));

        ButtonLoader.Instance.buttonGroupPanel1 = buttonGroupPanel1;
        ButtonLoader.Instance.buttonGroupPanel2 = buttonGroupPanel2;
        ButtonLoader.Instance.buttonGroupPanel3 = buttonGroupPanel3;
        ButtonLoader.Instance.CreateSetButtonGroup(data, TabButton5, setButtonGroupPanel);

        // Kiểm tra kiểu của data và ép kiểu phù hợp
        if (data is CardHeroes cardHeroes)
        {
            // Xử lý đối tượng Card
            mainType = "CardHeroes";
            titleText.text = "Card Hero";
            UserCardHeroesController.Instance.ShowCardHeroesDetails(cardHeroes, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardHeroes.type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Books book)
        {
            // Xử lý đối tượng Book
            mainType = "Books";
            titleText.text = "Book";
            UserBooksController.Instance.ShowBooksDetails(book, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardCaptains cardCaptains)
        {
            // Xử lý đối tượng Captain
            mainType = "CardCaptains";
            titleText.text = "Card Captain";
            UserCardCaptainsController.Instance.ShowCardCaptainsDetails(cardCaptains, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardCaptains.type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Pets pet)
        {
            // Xử lý đối tượng Pet
            mainType = "Pets";
            titleText.text = "Pet";
            UserPetsController.Instance.ShowPetsDetails(pet, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CollaborationEquipment collaborationEquipmentsequipment)
        {
            // Xử lý đối tượng CollaborationEquipment
            mainType = "CollaborationEquipments";
            titleText.text = "Collaboration Equipment";
            UserCollaborationEquipmentController.Instance.ShowCollaborationEquipmentsDetails(collaborationEquipmentsequipment, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardMilitary cardMilitary)
        {
            // Xử lý đối tượng Military
            mainType = "CardMilitary";
            titleText.text = "Card Military";
            UserCardMilitaryController.Instance.ShowCardMilitaryDetails(cardMilitary, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardMilitary.type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardSpell cardSpell)
        {
            // Xử lý đối tượng Spell
            mainType = "CardSpell";
            titleText.text = "Card Spell";
            UserCardSpellController.Instance.ShowCardSpellDetails(cardSpell, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Collaboration collaboration)
        {
            // Xử lý đối tượng Collaboration
            mainType = "Collaborations";
            titleText.text = "Collaboration";
            UserCollaborationController.Instance.ShowCollaborationsDetails(collaboration, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardMonsters cardMonsters)
        {
            // Xử lý đối tượng Monster
            mainType = "CardMonsters";
            titleText.text = "Card Monster";
            UserCardMonstersController.Instance.ShowCardMonstersDetails(cardMonsters, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Equipments equipment)
        {
            // Xử lý đối tượng Equipment
            mainType = "Equipments";
            titleText.text = "Equipment";
            UserEquipmentsController.Instance.ShowEquipmentsDetails(equipment, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Medals medal)
        {
            // Xử lý đối tượng Medal
            mainType = "Medals";
            titleText.text = "Medal";
            UserMedalsController.Instance.ShowMedalsDetails(medal, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Skills skill)
        {
            // Xử lý đối tượng Skill
            mainType = "Skills";
            titleText.text = "Skill";
            UserSkillsController.Instance.ShowSkillsDetails(skill, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Symbols symbol)
        {
            // Xử lý đối tượng Symbol
            mainType = "Symbols";
            titleText.text = "Symbol";
            UserSymbolsController.Instance.ShowSymbolsDetails(symbol, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Titles title)
        {
            // Xử lý đối tượng Title
            mainType = "Titles";
            titleText.text = "Title";
            UserTitlesController.Instance.ShowTitlesDetails(title, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is MagicFormationCircle magicFormationCircle)
        {
            // Xử lý đối tượng Title
            mainType = "MagicFormationCircle";
            titleText.text = "Magic Formation Cirlce";
            UserMagicFormationCircleController.Instance.ShowMagicFormationCircleDetails(magicFormationCircle, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Relics relics)
        {
            // Xử lý đối tượng Title
            mainType = "Relics";
            titleText.text = "Relic";
            UserRelicsController.Instance.ShowRelicsDetails(relics, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardColonels cardColonels)
        {
            // Xử lý đối tượng colonels
            mainType = "CardColonels";
            titleText.text = "Card Colonel";
            UserCardColonelsController.Instance.ShowCardColonelsDetails(cardColonels, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardColonels.type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardGenerals cardGenerals)
        {
            // Xử lý đối tượng Generals
            mainType = "CardGenerals";
            titleText.text = "Card General";
            UserCardGeneralsController.Instance.ShowCardGeneralsDetails(cardGenerals, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardGenerals.type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            // Xử lý đối tượng admirals
            mainType = "CardAdmirals";
            titleText.text = "Card Admiral";
            UserCardAdmiralsController.Instance.ShowCardAdmiralsDetails(cardAdmirals, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{cardAdmirals.type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        // else if (data is Borders borders)
        // {
        //     // Xử lý đối tượng borders
        //     ShowBordersDetails(borders);
        // }
        else if (data is Achievements achievements)
        {
            // Xử lý đối tượng achievements
            mainType = "Achievements";
            titleText.text = "Achievement";
            UserAchievementsController.Instance.ShowAchievementsDetails(achievements, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Talisman talisman)
        {
            // Xử lý đối tượng achievements
            mainType = "Talisman";
            titleText.text = "Talisman";
            UserTalismanController.Instance.ShowTalismanDetails(talisman, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Puppet puppet)
        {
            // Xử lý đối tượng achievements
            mainType = "Puppet";
            titleText.text = "Puppet";
            UserPuppetController.Instance.ShowPuppetDetails(puppet, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Alchemy alchemy)
        {
            // Xử lý đối tượng achievements
            mainType = "Alchemy";
            titleText.text = "Alchemy";
            UserAlchemyController.Instance.ShowAlchemyDetails(alchemy, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Forge forge)
        {
            // Xử lý đối tượng achievements
            mainType = "Forge";
            titleText.text = "Forge";
            UserForgeController.Instance.ShowForgeDetails(forge, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardLife cardLife)
        {
            // Xử lý đối tượng achievements
            mainType = "CardLife";
            titleText.text = "Card Life";
            UserCardLifeController.Instance.ShowCardLifeDetails(cardLife, currentObject);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                ButtonEvent.Instance.Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else
        {
            Debug.LogError("Không hỗ trợ loại dữ liệu này!");
        }

    }
    public bool UpOneLevelCondition(List<Items> items, int currentLevel, int userMaxLevel, int maxLevel, int experimentCondition, int totalExperiment)
    {
        bool status = false;
        if (currentLevel < userMaxLevel && currentLevel < maxLevel)
        {
            int requiredExp = experimentCondition - totalExperiment; // EXP cần để lên cấp
            bool canLevelUp = false;

            foreach (Items items1 in items)
            {
                int expPerBottle = EvaluateExperiment.GetItemExp(items1.name);

                if (expPerBottle > 0 && items1.quantity > 0) // Điều kiện 2: Phải có item hợp lệ
                {
                    int totalExpFromThisItem = expPerBottle * items1.quantity;

                    if (requiredExp <= totalExpFromThisItem)
                    {
                        totalExperiment += requiredExp;
                        items1.quantity -= (int)Math.Ceiling((double)requiredExp / expPerBottle);
                        canLevelUp = true;
                        break;
                    }
                    else
                    {
                        totalExperiment += totalExpFromThisItem;
                        items1.quantity = 0;
                    }
                }
            }

            if (canLevelUp)
            {
                totalExperiment -= experimentCondition;
                currentLevel++;
                experimentCondition = currentLevel * 100;
                status = true;
            }
        }
        if (status == true)
        {
            foreach (Items items1 in items)
            {
                UserItemsService.Create().UpdateUserItemsQuantity(items1);
            }
        }
        return status;
    }
    public bool UpMaxLevelCondition(List<Items> items, ref int currentLevel, int userMaxLevel, int maxLevel, int experimentCondition, int totalExperiment)
    {
        bool status = false;
        while (currentLevel < userMaxLevel && currentLevel < maxLevel)
        {
            int requiredExp = experimentCondition - totalExperiment; // EXP cần để lên cấp
            bool canLevelUp = false;

            foreach (Items items1 in items)
            {
                int expPerBottle = EvaluateExperiment.GetItemExp(items1.name);

                if (expPerBottle > 0 && items1.quantity > 0) // Điều kiện 2: Phải có item hợp lệ
                {
                    int totalExpFromThisItem = expPerBottle * items1.quantity;

                    if (requiredExp <= totalExpFromThisItem)
                    {
                        totalExperiment += requiredExp;
                        items1.quantity -= (int)Math.Ceiling((double)requiredExp / expPerBottle);
                        canLevelUp = true;
                        break;
                    }
                    else
                    {
                        totalExperiment += totalExpFromThisItem;
                        items1.quantity = 0;
                    }
                }
            }

            if (canLevelUp)
            {
                totalExperiment -= experimentCondition;
                currentLevel++;
                experimentCondition = currentLevel * 100;
                status = true;
            }
            else
            {
                break; // Không đủ EXP để lên cấp tiếp
            }
        }
        if (status == true)
        {
            // Cập nhật số lượng item còn lại trong cơ sở dữ liệu
            foreach (Items items1 in items)
            {
                UserItemsService.Create().UpdateUserItemsQuantity(items1);
            }
        }
        return status;
    }
    public bool BreakthroughCondition()
    {
        bool status = false;

        return status;
    }
    public void HideNonDetailsPanels()
    {
        DetailsPanel.gameObject.SetActive(true);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        ButtonEvent.Instance.Close(LevelElementContent);
        ButtonEvent.Instance.Close(LevelMaterialContent);
        ButtonEvent.Instance.Close(SkillsPanel);
        ButtonEvent.Instance.Close(UpgradeElementContent);
        ButtonEvent.Instance.Close(UpgradeMaterialContent);
    }
    public void HideNonLevelPanels()
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(true);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        ButtonEvent.Instance.Close(DetailsContent);
        ButtonEvent.Instance.Close(SkillsPanel);
        ButtonEvent.Instance.Close(UpgradeElementContent);
        ButtonEvent.Instance.Close(UpgradeMaterialContent);
        ButtonEvent.Instance.Close(LevelElementContent);
        ButtonEvent.Instance.Close(LevelMaterialContent);
    }
    public void HideNonSkillsPanels()
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(true);
        UpgradePanel.gameObject.SetActive(false);
        ButtonEvent.Instance.Close(DetailsContent);
        ButtonEvent.Instance.Close(LevelElementContent);
        ButtonEvent.Instance.Close(LevelMaterialContent);
        ButtonEvent.Instance.Close(UpgradeElementContent);
        ButtonEvent.Instance.Close(UpgradeMaterialContent);
    }
    public void HideNonUpgradePanels()
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(true);
        ButtonEvent.Instance.Close(DetailsContent);
        ButtonEvent.Instance.Close(LevelElementContent);
        ButtonEvent.Instance.Close(LevelMaterialContent);
        ButtonEvent.Instance.Close(SkillsPanel);
        ButtonEvent.Instance.Close(UpgradeElementContent);
        ButtonEvent.Instance.Close(UpgradeMaterialContent);
    }
}
