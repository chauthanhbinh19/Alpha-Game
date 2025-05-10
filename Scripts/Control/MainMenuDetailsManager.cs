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
    private GameObject TabButton4;
    private GameObject ElementDetailsPrefab;
    private GameObject NumberDetailPrefab;
    private GameObject NumberDetail2Prefab;
    private GameObject NumberDetail3Prefab;
    private Transform MainPanel;
    private Transform RightButtonContent;
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
    private GameObject ItemThird;
    private GameObject StarPrefab;
    private GameObject ElementDetails2Prefab;
    private Button clickedButton;
    private GameObject firstDetailsObject;
    private GameObject elementDetailsObject;
    private GameObject elementDetails2Object;
    private GameObject descriptionDetailsObject;
    private GameObject buttonPrefab;
    private GameObject TabButton5;
    private Transform firstPopupPanel;
    private Transform elementPopupPanel;
    private Transform element2PopupPanel;
    private Transform descriptionPopupPanel;
    private Transform buttonGroupPanel1;
    private Transform buttonGroupPanel2;
    private Transform buttonGroupPanel3;
    private Transform setButtonGroupPanel;
    private RawImage CardBackground;
    private string mainType;
    private int set;
    private string descriptionColor = "#F9EED9";
    private double increasePerLevel = 0.01;
    private double increasePerUpgrade = 1.1;
    // Start is called before the first frame update
    void Start()
    {
        MainMenuDetailPanel2Prefab = UIManager.Instance.GetGameObject("MainMenuDetailPanel2Prefab");
        TabButton4 = UIManager.Instance.GetGameObject("TabButton4");
        ElementDetailsPrefab = UIManager.Instance.GetGameObject("ElementDetailsPrefab");
        NumberDetailPrefab = UIManager.Instance.GetGameObject("NumberDetailPrefab");
        NumberDetail2Prefab = UIManager.Instance.GetGameObject("NumberDetail2Prefab");
        NumberDetail3Prefab = UIManager.Instance.GetGameObject("NumberDetail3Prefab");
        ItemThird = UIManager.Instance.GetGameObject("ItemThird");
        StarPrefab = UIManager.Instance.GetGameObject("StarPrefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");
        buttonPrefab = UIManager.Instance.GetGameObject("buttonPrefab");
        TabButton5 = UIManager.Instance.GetGameObject("TabButton5");

    }

    public void PopupDetails(object data, Transform panel)
    {
        MainPanel = panel;
        currentObject = Instantiate(MainMenuDetailPanel2Prefab, MainPanel);
        RightButtonContent = currentObject.transform.Find("ScrollViewRightButton/Viewport/ButtonContent");
        DetailsPanel = currentObject.transform.Find("DictionaryCards/Content/DetailsPanel");
        LevelPanel = currentObject.transform.Find("DictionaryCards/Content/LevelPanel");
        SkillsPanel = currentObject.transform.Find("DictionaryCards/Content/SkillsPanel");
        UpgradePanel = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel");
        DetailsContent = currentObject.transform.Find("DictionaryCards/Content/DetailsPanel/Scroll View/Viewport/Content");
        LevelElementContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewElement/Viewport/Content");
        LevelMaterialContent = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/ScrollViewMaterial/Viewport/Content");
        SkillsContent = currentObject.transform.Find("DictionaryCards/Content/SkillsPanel");
        UpgradeElementContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewElement/Viewport/Content");
        UpgradeMaterialContent = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/ScrollViewMaterial/Viewport/Content");
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        buttonGroupPanel1 = currentObject.transform.Find("DictionaryCards/ButtonGroup1");
        buttonGroupPanel2 = currentObject.transform.Find("DictionaryCards/ButtonGroup2");
        buttonGroupPanel3 = currentObject.transform.Find("DictionaryCards/ButtonGroup3");
        setButtonGroupPanel = currentObject.transform.Find("DictionaryCards/SetButtonGroup");
        CardBackground = currentObject.transform.Find("DictionaryCards/Background").GetComponent<RawImage>();
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        CreateSetButtonGroup(data);

        // Kiểm tra kiểu của data và ép kiểu phù hợp
        if (data is CardHeroes card)
        {
            // Xử lý đối tượng Card
            mainType = "CardHeroes";
            ShowCardHeroesDetails(card);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{card.type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Books book)
        {
            // Xử lý đối tượng Book
            mainType = "Books";
            ShowBooksDetails(book);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardCaptains captain)
        {
            // Xử lý đối tượng Captain
            mainType = "CardCaptains";
            ShowCardCaptainsDetails(captain);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{captain.type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Pets pet)
        {
            // Xử lý đối tượng Pet
            mainType = "Pets";
            ShowPetsDetails(pet);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CollaborationEquipment collaborationEquipmentsequipment)
        {
            // Xử lý đối tượng CollaborationEquipment
            mainType = "CollaborationEquipments";
            ShowCollaborationEquipmentsDetails(collaborationEquipmentsequipment);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardMilitary military)
        {
            // Xử lý đối tượng Military
            mainType = "CardMilitary";
            ShowCardMilitaryDetails(military);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{military.type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardSpell spell)
        {
            // Xử lý đối tượng Spell
            mainType = "CardSpell";
            ShowCardSpellDetails(spell);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Collaboration collaboration)
        {
            // Xử lý đối tượng Collaboration
            mainType = "Collaborations";
            ShowCollaborationsDetails(collaboration);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardMonsters monster)
        {
            // Xử lý đối tượng Monster
            mainType = "CardMonsters";
            ShowCardMonstersDetails(monster);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Equipments equipment)
        {
            // Xử lý đối tượng Equipment
            mainType = "Equipments";
            ShowEquipmentsDetails(equipment);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Medals medal)
        {
            // Xử lý đối tượng Medal
            mainType = "Medals";
            ShowMedalsDetails(medal);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Skills skill)
        {
            // Xử lý đối tượng Skill
            mainType = "Skills";
            ShowSkillsDetails(skill);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Symbols symbol)
        {
            // Xử lý đối tượng Symbol
            mainType = "Symbols";
            ShowSymbolsDetails(symbol);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Titles title)
        {
            // Xử lý đối tượng Title
            mainType = "Titles";
            ShowTitlesDetails(title);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is MagicFormationCircle magicFormationCircle)
        {
            // Xử lý đối tượng Title
            mainType = "MagicFormationCircle";
            ShowMagicFormationCircleDetails(magicFormationCircle);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Relics relics)
        {
            // Xử lý đối tượng Title
            mainType = "Relics";
            ShowRelicsDetails(relics);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardColonels colonels)
        {
            // Xử lý đối tượng colonels
            mainType = "CardColonels";
            ShowCardColonelsDetails(colonels);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{colonels.type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardGenerals generals)
        {
            // Xử lý đối tượng Generals
            mainType = "CardGenerals";
            ShowCardGeneralsDetails(generals);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{generals.type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardAdmirals admirals)
        {
            // Xử lý đối tượng admirals
            mainType = "CardAdmirals";
            ShowCardAdmiralsDetails(admirals);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{admirals.type}");
            CardBackground.texture = texture;
            CardBackground.gameObject.SetActive(true);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
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
            ShowAchievementsDetails(achievements);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Talisman talisman)
        {
            // Xử lý đối tượng achievements
            mainType = "Talisman";
            ShowTalismanDetails(talisman);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Puppet puppet)
        {
            // Xử lý đối tượng achievements
            mainType = "Puppet";
            ShowPuppetDetails(puppet);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Alchemy alchemy)
        {
            // Xử lý đối tượng achievements
            mainType = "Alchemy";
            ShowAlchemyDetails(alchemy);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is Forge forge)
        {
            // Xử lý đối tượng achievements
            mainType = "Forge";
            ShowForgeDetails(forge);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else if (data is CardLife cardLife)
        {
            // Xử lý đối tượng achievements
            mainType = "CardLife";
            ShowCardLifeDetails(cardLife);
            Texture texture = Resources.Load<Texture>($"UI/Background3/{mainType}");
            CardBackground.texture = texture;
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType(mainType);
            });
        }
        else
        {
            Debug.LogError("Không hỗ trợ loại dữ liệu này!");
        }

    }
    private void CreateButtonWithBackground(int index, string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(buttonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán màu cho itemBackground
        RawImage background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        if (background != null && itemBackground != null)
        {
            background.texture = itemBackground;
        }

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("ItemImage").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = itemName;
        }
    }
    public void CreateSetButtonGroup(object data)
    {
        if (data is CardHeroes cardHeroes || data is CardCaptains cardCaptains ||
        data is CardColonels cardColonels || data is CardGenerals cardGenerals ||
        data is CardAdmirals cardAdmirals || data is CardMonsters cardMonsters ||
        data is CardMilitary cardMilitary || data is CardSpell cardSpell ||
        data is Books books || data is Pets pets || data is Equipments equipments
        )
        {
            int setButtonNumber = 3;
            for (int i = 0; i < setButtonNumber; i++)
            {
                int index = i; // <- tạo biến tạm
                GameObject button = Instantiate(TabButton5, setButtonGroupPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = (index + 1).ToString();

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    set = index + 1;
                    foreach (Transform child in setButtonGroupPanel)
                    {
                        // Lấy component Button từ con cái
                        Button button = child.GetComponent<Button>();
                        if (button != null)
                        {
                            // Gọi hàm ChangeButtonBackground với màu trắng
                            ChangeButtonBackground(button.gameObject, "Background_V4_259"); // Giả sử bạn có texture trắng
                        }
                    }
                    ChangeButtonBackground(btn.gameObject, "Background_V4_332");
                    CreateButtonGroup(data);
                });

                if (index == 0)
                {
                    set = index + 1;
                    ChangeButtonBackground(button, "Background_V4_332");
                    CreateButtonGroup(data);
                }
                else
                {
                    ChangeButtonBackground(button, "Background_V4_259");
                }
            }
        }
    }
    public void CreateButtonGroup(object data)
    {
        if (data is CardHeroes cardHeroes)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardCaptains cardCaptains)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardColonels cardColonels)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardGenerals cardGenerals)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardMonsters cardMonsters)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardMilitary cardMilitary)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is CardSpell cardSpell)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is Books books)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is Pets pets)
        {
            CreateButtonGroupDetails(data);
        }
        else if (data is Equipments equipments)
        {
            CreateButtonGroupDetails(data);
        }
    }
    public void CreateButtonGroupDetails(object data)
    {
        Close(buttonGroupPanel1);
        Close(buttonGroupPanel2);
        Close(buttonGroupPanel3);
        if (data is CardHeroes cardHeroes || data is CardCaptains cardCaptains ||
        data is CardColonels cardColonels || data is CardGenerals cardGenerals ||
        data is CardAdmirals cardAdmirals || data is CardMonsters cardMonsters ||
        data is CardMilitary cardMilitary || data is CardSpell cardSpell ||
        data is Books books || data is Pets pets
        )
        {
            if (set == 1)
            {
                CreateButtonWithBackground(1, "Equipments", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Equipments"), buttonGroupPanel1);
                CreateButtonWithBackground(2, "Realm", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Realm"), buttonGroupPanel1);
                CreateButtonWithBackground(3, "Upgrade", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Upgrade"), buttonGroupPanel1);
                CreateButtonWithBackground(4, "Aptitude", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Aptitude"), buttonGroupPanel1);
                CreateButtonWithBackground(5, "Affinity", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Affinity"), buttonGroupPanel1);
                CreateButtonWithBackground(6, "Blessing", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Blessing"), buttonGroupPanel1);
                CreateButtonWithBackground(7, "Core", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Core"), buttonGroupPanel1);
                CreateButtonWithBackground(8, "Physique", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Physique"), buttonGroupPanel1);
                CreateButtonWithBackground(9, "Bloodline", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Bloodline"), buttonGroupPanel1);
                CreateButtonWithBackground(10, "Omnivision", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnivision"), buttonGroupPanel2);
                CreateButtonWithBackground(11, "Omnipotence", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnipotence"), buttonGroupPanel2);
                CreateButtonWithBackground(12, "Omnipresence", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnipresence"), buttonGroupPanel2);
                CreateButtonWithBackground(13, "Omniscience", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omniscience"), buttonGroupPanel2);
                CreateButtonWithBackground(14, "Omnivory", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnivory"), buttonGroupPanel2);
                CreateButtonWithBackground(15, "Angel", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Angel"), buttonGroupPanel2);
                CreateButtonWithBackground(16, "Demon", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Demon"), buttonGroupPanel2);
                CreateButtonWithBackground(17, "Sword", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Sword"), buttonGroupPanel3);
                CreateButtonWithBackground(18, "Spear", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Spear"), buttonGroupPanel3);
                CreateButtonWithBackground(19, "Shield", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Shield"), buttonGroupPanel3);
                CreateButtonWithBackground(20, "Bow", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Bow"), buttonGroupPanel3);
                CreateButtonWithBackground(21, "Gun", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Gun"), buttonGroupPanel3);
                CreateButtonWithBackground(22, "Cyber", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Cyber"), buttonGroupPanel3);
                CreateButtonWithBackground(23, "Fairy", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Fairy"), buttonGroupPanel3);

                AssignButtonEvent("Button_1", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuEquipmentManager>().CreateMainMenuEquipmentManager(data);
                });
                AssignButtonEvent("Button_2", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuRealmManager>().CreateMainMenuRealmManager(data);
                });
                AssignButtonEvent("Button_3", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuUpgradeManager>().CreateMainMenuUpgradeManager(data);
                });
                AssignButtonEvent("Button_4", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuAptitudeManager>().CreateMainMenuAptitudeManager(data);
                });
                AssignButtonEvent("Button_5", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuAffinityManager>().CreateMainMenuAffinityManager(data);
                });
                AssignButtonEvent("Button_6", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuBlessingManager>().CreateMainMenuBlessingManager(data);
                });
                AssignButtonEvent("Button_7", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuCoreManager>().CreateMainMenuCoreManager(data);
                });
                AssignButtonEvent("Button_8", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuPhysiqueManager>().CreateMainMenuPhysiqueManager(data);
                });
                AssignButtonEvent("Button_9", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuBloodlineManager>().CreateMainMenuBloodlineManager(data);
                });
                AssignButtonEvent("Button_10", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuOmnivisionManager>().CreateMainMenuOmnivisionManager(data);
                });
                AssignButtonEvent("Button_11", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuOmnipotenceManager>().CreateMainMenuOmnipotenceManager(data);
                });
                AssignButtonEvent("Button_12", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuOmnipresenceManager>().CreateMainMenuOmnipresenceManager(data);
                });
                AssignButtonEvent("Button_13", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuOmniscienceManager>().CreateMainMenuOmniscienceManager(data);
                });
                AssignButtonEvent("Button_14", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuOmnivoryManager>().CreateMainMenuOmnivoryManager(data);
                });
                AssignButtonEvent("Button_15", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuAngelManager>().CreateMainMenuAngelManager(data);
                });
                AssignButtonEvent("Button_16", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuDemonManager>().CreateMainMenuDemonManager(data);
                });
                AssignButtonEvent("Button_17", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuSwordManager>().CreateMainMenuSwordManager(data);
                });
                AssignButtonEvent("Button_18", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuSpearManager>().CreateMainMenuSpearManager(data);
                });
                AssignButtonEvent("Button_19", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuShieldManager>().CreateMainMenuShieldManager(data);
                });
                AssignButtonEvent("Button_20", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuBowManager>().CreateMainMenuBowManager(data);
                });
                AssignButtonEvent("Button_21", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuGunManager>().CreateMainMenuGunManager(data);
                });
                AssignButtonEvent("Button_22", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuCyberManager>().CreateMainMenuCyberManager(data);
                });
                AssignButtonEvent("Button_23", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuFairyManager>().CreateMainMenuFairyManager(data);
                });
            }
            else if(set == 2)
            {
                CreateButtonWithBackground(24, "Dark", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Dark"), buttonGroupPanel1);
                CreateButtonWithBackground(25, "Light", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Light"), buttonGroupPanel1);
                CreateButtonWithBackground(26, "Fire", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Fire"), buttonGroupPanel1);
                CreateButtonWithBackground(27, "Ice", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Ice"), buttonGroupPanel1);
                CreateButtonWithBackground(28, "Earth", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Earth"), buttonGroupPanel1);
                CreateButtonWithBackground(29, "Thunder", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Thunder"), buttonGroupPanel1);
                CreateButtonWithBackground(30, "Life", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Life"), buttonGroupPanel1);
                CreateButtonWithBackground(31, "Space", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Space"), buttonGroupPanel1);
                CreateButtonWithBackground(32, "Time", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Time"), buttonGroupPanel1);
                CreateButtonWithBackground(33, "Nanotech", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Nanotech"), buttonGroupPanel2);
                CreateButtonWithBackground(34, "Quantum", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Quantum"), buttonGroupPanel2);
                CreateButtonWithBackground(35, "Holography", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Holography"), buttonGroupPanel2);
                CreateButtonWithBackground(36, "Plasma", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Plasma"), buttonGroupPanel2);
                CreateButtonWithBackground(37, "Biomech", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Biomech"), buttonGroupPanel2);
                CreateButtonWithBackground(38, "Cryotech", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Cryotech"), buttonGroupPanel2);
                CreateButtonWithBackground(39, "Psionics", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Psionics"), buttonGroupPanel2);
                CreateButtonWithBackground(40, "Neurotech", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Neurotech"), buttonGroupPanel3);
                CreateButtonWithBackground(41, "Antimatter", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Antimatter"), buttonGroupPanel3);
                CreateButtonWithBackground(42, "Phantomware", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Phantomware"), buttonGroupPanel3);
                CreateButtonWithBackground(43, "Gravitech", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Gravitech"), buttonGroupPanel3);
                CreateButtonWithBackground(44, "Aethernet", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Aethernet"), buttonGroupPanel3);
                CreateButtonWithBackground(45, "Starforge", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Starforge"), buttonGroupPanel3);
                CreateButtonWithBackground(46, "Orbitalis", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Orbitalis"), buttonGroupPanel3);
                AssignButtonEvent("Button_24", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuDarkManager>().CreateMainMenuDarkManager(data);
                });
                AssignButtonEvent("Button_25", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuLightManager>().CreateMainMenuLightManager(data);
                });
                AssignButtonEvent("Button_26", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuFireManager>().CreateMainMenuFireManager(data);
                });
                AssignButtonEvent("Button_27", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuIceManager>().CreateMainMenuIceManager(data);
                });
                AssignButtonEvent("Button_28", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuEarthManager>().CreateMainMenuEarthManager(data);
                });
                AssignButtonEvent("Button_29", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuThunderManager>().CreateMainMenuThunderManager(data);
                });
                AssignButtonEvent("Button_30", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuLifeManager>().CreateMainMenuLifeManager(data);
                });
                AssignButtonEvent("Button_31", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuSpaceManager>().CreateMainMenuSpaceManager(data);
                });
                AssignButtonEvent("Button_32", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuTimeManager>().CreateMainMenuTimeManager(data);
                });
                AssignButtonEvent("Button_33", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuNanotechManager>().CreateMainMenuNanotechManager(data);
                });
                AssignButtonEvent("Button_34", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuQuantumManager>().CreateMainMenuQuantumManager(data);
                });
                AssignButtonEvent("Button_35", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuHolographyManager>().CreateMainMenuHolographyManager(data);
                });
                AssignButtonEvent("Button_36", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuPlasmaManager>().CreateMainMenuPlasmaManager(data);
                });
                AssignButtonEvent("Button_37", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuBiomechManager>().CreateMainMenuBiomechManager(data);
                });
                AssignButtonEvent("Button_38", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuCryotechManager>().CreateMainMenuCryotechManager(data);
                });
                AssignButtonEvent("Button_39", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuPsionicsManager>().CreateMainMenuPsionicsManager(data);
                });
                AssignButtonEvent("Button_40", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuNeurotechManager>().CreateMainMenuNeurotechManager(data);
                });
                AssignButtonEvent("Button_41", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuAntimatterManager>().CreateMainMenuAntimatterManager(data);
                });
                AssignButtonEvent("Button_42", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuPhantomwareManager>().CreateMainMenuPhantomwareManager(data);
                });
                AssignButtonEvent("Button_43", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuGravitechManager>().CreateMainMenuGravitechManager(data);
                });
                AssignButtonEvent("Button_44", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuAethernetManager>().CreateMainMenuAethernetManager(data);
                });
                AssignButtonEvent("Button_45", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuStarforgeManager>().CreateMainMenuStarforgeManager(data);
                });
                AssignButtonEvent("Button_46", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuOrbitalisManager>().CreateMainMenuOrbitalisManager(data);
                });
            }else if(set == 3){
                CreateButtonWithBackground(47, "Azathoth", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Azathoth"), buttonGroupPanel1);
                CreateButtonWithBackground(48, "Yog-Sothoth", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Yog-Sothoth"), buttonGroupPanel1);
                CreateButtonWithBackground(49, "Nyarlathotep", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Nyarlathotep"), buttonGroupPanel1);
                CreateButtonWithBackground(50, "Shub-Niggurath", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Shub-Niggurath"), buttonGroupPanel1);
                CreateButtonWithBackground(51, "Nihorath", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Nihorath"), buttonGroupPanel1);
                CreateButtonWithBackground(52, "Aeonax", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Aeonax"), buttonGroupPanel1);
                CreateButtonWithBackground(53, "Seraphiros", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Seraphiros"), buttonGroupPanel1);
                CreateButtonWithBackground(54, "Thorindar", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Thorindar"), buttonGroupPanel1);
                CreateButtonWithBackground(55, "Zilthros", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Zilthros"), buttonGroupPanel1);
                CreateButtonWithBackground(56, "Khorazal", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Khorazal"), buttonGroupPanel2);
                CreateButtonWithBackground(57, "Ixithra", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Ixithra"), buttonGroupPanel2);
                CreateButtonWithBackground(58, "Omnitheus", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnitheus"), buttonGroupPanel2);
                CreateButtonWithBackground(59, "Phyrixa", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Phyrixa"), buttonGroupPanel2);
                CreateButtonWithBackground(60, "Atherion", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Atherion"), buttonGroupPanel2);
                CreateButtonWithBackground(61, "Vorathos", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Vorathos"), buttonGroupPanel2);
                CreateButtonWithBackground(62, "Tenebris", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Tenebris"), buttonGroupPanel2);
                CreateButtonWithBackground(63, "Xylkor", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Xylkor"), buttonGroupPanel3);
                CreateButtonWithBackground(64, "Veltharion", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Veltharion"), buttonGroupPanel3);
                CreateButtonWithBackground(65, "Arcanos", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Arcanos"), buttonGroupPanel3);
                CreateButtonWithBackground(66, "Dolomath", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Dolomath"), buttonGroupPanel3);
                CreateButtonWithBackground(67, "Arathor", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Arathor"), buttonGroupPanel3);
                CreateButtonWithBackground(68, "Xyphos", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Xyphos"), buttonGroupPanel3);
                CreateButtonWithBackground(69, "Vaelith", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Vaelith"), buttonGroupPanel3);
                AssignButtonEvent("Button_47", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuAzathothManager>().CreateMainMenuAzathothManager(data);
                });
                AssignButtonEvent("Button_48", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuYogSothothManager>().CreateMainMenuYogSothothManager(data);
                });
                AssignButtonEvent("Button_49", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuNyarlathotepManager>().CreateMainMenuNyarlathotepManager(data);
                });
                AssignButtonEvent("Button_50", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuShubNiggurathManager>().CreateMainMenuShubNiggurathManager(data);
                });
                AssignButtonEvent("Button_51", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuNihorathManager>().CreateMainMenuNihorathManager(data);
                });
                AssignButtonEvent("Button_52", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuAeonaxManager>().CreateMainMenuAeonaxManager(data);
                });
                AssignButtonEvent("Button_53", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuSeraphirosManager>().CreateMainMenuSeraphirosManager(data);
                });
                AssignButtonEvent("Button_54", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuThorindarManager>().CreateMainMenuThorindarManager(data);
                });
                AssignButtonEvent("Button_55", buttonGroupPanel1, () =>
                {
                    FindAnyObjectByType<MainMenuZilthrosManager>().CreateMainMenuZilthrosManager(data);
                });
                AssignButtonEvent("Button_56", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuKhorazalManager>().CreateMainMenuKhorazalManager(data);
                });
                AssignButtonEvent("Button_57", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuIxithraManager>().CreateMainMenuIxithraManager(data);
                });
                AssignButtonEvent("Button_58", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuOmnitheusManager>().CreateMainMenuOmnitheusManager(data);
                });
                AssignButtonEvent("Button_59", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuPhyrixaManager>().CreateMainMenuPhyrixaManager(data);
                });
                AssignButtonEvent("Button_60", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuAtherionManager>().CreateMainMenuAtherionManager(data);
                });
                AssignButtonEvent("Button_61", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuVorathosManager>().CreateMainMenuVorathosManager(data);
                });
                AssignButtonEvent("Button_62", buttonGroupPanel2, () =>
                {
                    FindAnyObjectByType<MainMenuTenebrisManager>().CreateMainMenuTenebrisManager(data);
                });
                AssignButtonEvent("Button_63", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuXylkorManager>().CreateMainMenuXylkorManager(data);
                });
                AssignButtonEvent("Button_64", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuVeltharionManager>().CreateMainMenuVeltharionManager(data);
                });
                AssignButtonEvent("Button_65", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuArcanosManager>().CreateMainMenuArcanosManager(data);
                });
                AssignButtonEvent("Button_66", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuDolomathManager>().CreateMainMenuDolomathManager(data);
                });
                AssignButtonEvent("Button_67", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuArathorManager>().CreateMainMenuArathorManager(data);
                });
                AssignButtonEvent("Button_68", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuXyphosManager>().CreateMainMenuXyphosManager(data);
                });
                AssignButtonEvent("Button_69", buttonGroupPanel3, () =>
                {
                    FindAnyObjectByType<MainMenuVaelithManager>().CreateMainMenuVaelithManager(data);
                });
            }
        }
        else if (data is Equipments equipments)
        {
            // CreateButtonWithBackground(1, "Equipments", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Equipments"), buttonGroupPanel1);
            CreateButtonWithBackground(2, "Realm", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Realm"), buttonGroupPanel1);
            CreateButtonWithBackground(3, "Upgrade", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Upgrade"), buttonGroupPanel1);
            CreateButtonWithBackground(4, "Aptitude", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Aptitude"), buttonGroupPanel1);
            // CreateButtonWithBackground(5, "Affinity", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Affinity"), buttonGroupPanel1);
            CreateButtonWithBackground(6, "Blessing", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Blessing"), buttonGroupPanel1);
            CreateButtonWithBackground(7, "Core", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Core"), buttonGroupPanel1);
            CreateButtonWithBackground(8, "Physique", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Physique"), buttonGroupPanel1);
            CreateButtonWithBackground(9, "Bloodline", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Bloodline"), buttonGroupPanel1);
            CreateButtonWithBackground(10, "Omnivision", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnivision"), buttonGroupPanel2);
            CreateButtonWithBackground(11, "Omnipotence", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnipotence"), buttonGroupPanel2);
            CreateButtonWithBackground(12, "Omnipresence", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnipresence"), buttonGroupPanel2);
            CreateButtonWithBackground(13, "Omniscience", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omniscience"), buttonGroupPanel2);
            CreateButtonWithBackground(14, "Omnivory", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Omnivory"), buttonGroupPanel2);
            CreateButtonWithBackground(15, "Angel", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Angel"), buttonGroupPanel2);
            CreateButtonWithBackground(16, "Demon", Resources.Load<Texture2D>($"UI/Background2/bg2_prossorder"), Resources.Load<Texture2D>($"UI/Button/Demon"), buttonGroupPanel2);

            // AssignButtonEvent("Button_1", buttonGroupPanel1, () =>
            // {
            //     FindAnyObjectByType<MainMenuEquipmentManager>().CreateMainMenuEquipmentManager(data);
            // });
            AssignButtonEvent("Button_2", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuRealmManager>().CreateMainMenuRealmManager(data);
            });
            AssignButtonEvent("Button_3", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuUpgradeManager>().CreateMainMenuUpgradeManager(data);
            });
            AssignButtonEvent("Button_4", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuAptitudeManager>().CreateMainMenuAptitudeManager(data);
            });
            // AssignButtonEvent("Button_5", buttonGroupPanel1, () =>
            // {
            //     FindAnyObjectByType<MainMenuAffinityManager>().CreateMainMenuAffinityManager(data);
            // });
            AssignButtonEvent("Button_6", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuBlessingManager>().CreateMainMenuBlessingManager(data);
            });
            AssignButtonEvent("Button_7", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuCoreManager>().CreateMainMenuCoreManager(data);
            });
            AssignButtonEvent("Button_8", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuPhysiqueManager>().CreateMainMenuPhysiqueManager(data);
            });
            AssignButtonEvent("Button_9", buttonGroupPanel1, () =>
            {
                FindAnyObjectByType<MainMenuBloodlineManager>().CreateMainMenuBloodlineManager(data);
            });
            AssignButtonEvent("Button_10", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuOmnivisionManager>().CreateMainMenuOmnivisionManager(data);
            });
            AssignButtonEvent("Button_11", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuOmnipotenceManager>().CreateMainMenuOmnipotenceManager(data);
            });
            AssignButtonEvent("Button_12", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuOmnipresenceManager>().CreateMainMenuOmnipresenceManager(data);
            });
            AssignButtonEvent("Button_13", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuOmniscienceManager>().CreateMainMenuOmniscienceManager(data);
            });
            AssignButtonEvent("Button_14", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuOmnivoryManager>().CreateMainMenuOmnivoryManager(data);
            });
            AssignButtonEvent("Button_15", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuAngelManager>().CreateMainMenuAngelManager(data);
            });
            AssignButtonEvent("Button_16", buttonGroupPanel2, () =>
            {
                FindAnyObjectByType<MainMenuDemonManager>().CreateMainMenuDemonManager(data);
            });
        }
    }
    void AssignButtonEvent(string buttonName, Transform panel, UnityEngine.Events.UnityAction action)
    {
        Transform buttonTransform = panel.Find(buttonName);
        if (buttonTransform != null)
        {
            Button button = buttonTransform.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(action);
            }
        }
        else
        {
            Debug.LogWarning($"Button {buttonName} not found!");
        }
    }
    private void CreateButton(int index, string itemName, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(TabButton4, panel);
        newButton.name = "Button_" + index;

        // Gán tên cho itemName
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = itemName;
        }
    }
    public void ShowCardHeroesDetails(CardHeroes cardHeroes)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(cardHeroes);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardHeroes);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(cardHeroes);
            OnButtonClicked("Button_3");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(cardHeroes);
            OnButtonClicked("Button_4");
        });

        GetDetails(cardHeroes);
        OnButtonClicked("Button_1");
    }
    public void ShowCardCaptainsDetails(CardCaptains cardCaptains)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(cardCaptains);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardCaptains);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(cardCaptains);
            OnButtonClicked("Button_3");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(cardCaptains);
            OnButtonClicked("Button_4");
        });

        GetDetails(cardCaptains);
        OnButtonClicked("Button_1");
    }
    public void ShowCardColonelsDetails(CardColonels cardColonels)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(cardColonels);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardColonels);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(cardColonels);
            OnButtonClicked("Button_3");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(cardColonels);
            OnButtonClicked("Button_4");
        });

        GetDetails(cardColonels);
        OnButtonClicked("Button_1");
    }
    public void ShowCardGeneralsDetails(CardGenerals cardGenerals)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(cardGenerals);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardGenerals);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(cardGenerals);
            OnButtonClicked("Button_3");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(cardGenerals);
            OnButtonClicked("Button_4");
        });

        GetDetails(cardGenerals);
        OnButtonClicked("Button_1");
    }
    public void ShowCardAdmiralsDetails(CardAdmirals cardAdmirals)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(cardAdmirals);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardAdmirals);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(cardAdmirals);
            OnButtonClicked("Button_3");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(cardAdmirals);
            OnButtonClicked("Button_4");
        });

        GetDetails(cardAdmirals);
        OnButtonClicked("Button_1");
    }
    public void ShowCardMonstersDetails(CardMonsters cardMonsters)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(cardMonsters);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardMonsters);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(cardMonsters);
            OnButtonClicked("Button_3");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(cardMonsters);
            OnButtonClicked("Button_4");
        });

        GetDetails(cardMonsters);
        OnButtonClicked("Button_1");
    }
    public void ShowCardMilitaryDetails(CardMilitary cardMilitary)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(cardMilitary);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardMilitary);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(cardMilitary);
            OnButtonClicked("Button_3");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(cardMilitary);
            OnButtonClicked("Button_4");
        });

        GetDetails(cardMilitary);
        OnButtonClicked("Button_1");
    }
    public void ShowCardSpellDetails(CardSpell cardSpell)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(cardSpell);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardSpell);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetUpgrade(cardSpell);
            OnButtonClicked("Button_3");
        });

        GetDetails(cardSpell);
        OnButtonClicked("Button_1");
    }
    public void ShowBooksDetails(Books books)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(books);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(books);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(books);
            OnButtonClicked("Button_3");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(books);
            OnButtonClicked("Button_4");
        });

        GetDetails(books);
        OnButtonClicked("Button_1");
    }
    public void ShowPetsDetails(Pets pets)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Skills", RightButtonContent);
        CreateButton(4, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(pets);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(pets);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(pets);
            OnButtonClicked("Button_3");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(pets);
            OnButtonClicked("Button_4");
        });

        GetDetails(pets);
        OnButtonClicked("Button_1");
    }
    public void ShowCollaborationEquipmentsDetails(CollaborationEquipment collaborationEquipment)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(collaborationEquipment);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(collaborationEquipment);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetUpgrade(collaborationEquipment);
            OnButtonClicked("Button_3");
        });

        GetDetails(collaborationEquipment);
        OnButtonClicked("Button_1");
    }
    public void ShowCollaborationsDetails(Collaboration collaboration)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(collaboration);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(collaboration);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(collaboration);
            OnButtonClicked("Button_3");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetUpgrade(collaboration);
            OnButtonClicked("Button_3");
        });

        GetDetails(collaboration);
        OnButtonClicked("Button_1");
    }
    public void ShowMedalsDetails(Medals medals)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(medals);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(medals);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetUpgrade(medals);
            OnButtonClicked("Button_3");
        });

        GetDetails(medals);
        OnButtonClicked("Button_1");
    }
    public void ShowEquipmentsDetails(Equipments equipments)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(equipments);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(equipments);
            OnButtonClicked("Button_2");
        });

        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetUpgrade(equipments);
            OnButtonClicked("Button_3");
        });

        GetDetails(equipments);
        OnButtonClicked("Button_1");
    }
    public void ShowSymbolsDetails(Symbols symbols)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(symbols);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(symbols);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_3", RightButtonContent, () =>
        {
            GetSkills(symbols);
            OnButtonClicked("Button_3");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(symbols);
            OnButtonClicked("Button_4");
        });

        GetDetails(symbols);
        OnButtonClicked("Button_1");
    }
    public void ShowTitlesDetails(Titles titles)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(titles);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(titles);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(titles);
            OnButtonClicked("Button_4");
        });

        GetDetails(titles);
        OnButtonClicked("Button_1");
    }
    public void ShowSkillsDetails(Skills skills)
    {
        CreateButton(1, "Details", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(skills);
            OnButtonClicked("Button_1");
        });

        GetDetails(skills);
        OnButtonClicked("Button_1");
    }
    public void ShowMagicFormationCircleDetails(MagicFormationCircle magicFormationCircle)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(magicFormationCircle);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(magicFormationCircle);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(magicFormationCircle);
            OnButtonClicked("Button_4");
        });

        GetDetails(magicFormationCircle);
        OnButtonClicked("Button_1");
    }
    public void ShowRelicsDetails(Relics relics)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(relics);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(relics);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(relics);
            OnButtonClicked("Button_4");
        });

        GetDetails(relics);
        OnButtonClicked("Button_1");
    }
    public void ShowAchievementsDetails(Achievements achievements)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(achievements);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(achievements);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(achievements);
            OnButtonClicked("Button_4");
        });

        GetDetails(achievements);
        OnButtonClicked("Button_1");
    }
    public void ShowTalismanDetails(Talisman talisman)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(talisman);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(talisman);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(talisman);
            OnButtonClicked("Button_4");
        });

        GetDetails(talisman);
        OnButtonClicked("Button_1");
    }
    public void ShowPuppetDetails(Puppet puppet)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(puppet);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(puppet);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(puppet);
            OnButtonClicked("Button_4");
        });

        GetDetails(puppet);
        OnButtonClicked("Button_1");
    }
    public void ShowAlchemyDetails(Alchemy alchemy)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(alchemy);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(alchemy);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(alchemy);
            OnButtonClicked("Button_4");
        });

        GetDetails(alchemy);
        OnButtonClicked("Button_1");
    }
    public void ShowForgeDetails(Forge forge)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(forge);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(forge);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(forge);
            OnButtonClicked("Button_4");
        });

        GetDetails(forge);
        OnButtonClicked("Button_1");
    }
    public void ShowCardLifeDetails(CardLife cardLife)
    {
        CreateButton(1, "Details", RightButtonContent);
        CreateButton(2, "Level", RightButtonContent);
        CreateButton(3, "Upgrade", RightButtonContent);

        AssignButtonEvent("Button_1", RightButtonContent, () =>
        {
            GetDetails(cardLife);
            OnButtonClicked("Button_1");
        });
        AssignButtonEvent("Button_2", RightButtonContent, () =>
        {
            GetLevel(cardLife);
            OnButtonClicked("Button_2");
        });
        AssignButtonEvent("Button_4", RightButtonContent, () =>
        {
            GetUpgrade(cardLife);
            OnButtonClicked("Button_4");
        });

        GetDetails(cardLife);
        OnButtonClicked("Button_1");
    }
    public void Close(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public void GetDetails(object obj)
    {
        DetailsPanel.gameObject.SetActive(true);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        Close(LevelElementContent);
        Close(LevelMaterialContent);
        Close(SkillsPanel);
        Close(UpgradeElementContent);
        Close(UpgradeMaterialContent);

        firstDetailsObject = Instantiate(NumberDetail2Prefab, DetailsContent);
        elementDetailsObject = Instantiate(NumberDetailPrefab, DetailsContent);
        elementDetails2Object = Instantiate(NumberDetail3Prefab, DetailsContent);
        descriptionDetailsObject = Instantiate(NumberDetail3Prefab, DetailsContent);
        firstPopupPanel = firstDetailsObject.transform.Find("ElementDetails");
        elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
        element2PopupPanel = elementDetails2Object.transform.Find("ElementDetails");
        descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");
        if (obj is CardHeroes cardHeroes)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = cardHeroes.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = cardHeroes.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = cardHeroes.all_power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardHeroes.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardHeroes.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardHeroes, null);
                CreatePropertyUI(1, property, value);
            }
        }
        else if (obj is Books book)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = book.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = book.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = book.all_power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{book.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = book.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(book, null);
                CreatePropertyUI(1, property, value);
            }
        }
        else if (obj is CardCaptains cardCaptains)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = cardCaptains.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = cardCaptains.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = cardCaptains.all_power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardCaptains.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardCaptains.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardCaptains, null);
                CreatePropertyUI(1, property, value);
            }
        }
        else if (obj is Pets pet)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = pet.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = pet.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = pet.all_power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{pet.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = pet.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(pet, null);
                CreatePropertyUI(1, property, value);
            }
        }
        else if (obj is CollaborationEquipment collaborationEquipment)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = collaborationEquipment.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = collaborationEquipment.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = collaborationEquipment.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaborationEquipment.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = collaborationEquipment.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(collaborationEquipment, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is CardMilitary cardMilitary)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = cardMilitary.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = cardMilitary.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = cardMilitary.all_power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMilitary.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardMilitary.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardMilitary, null);
                CreatePropertyUI(1, property, value);
            }
        }
        else if (obj is CardSpell cardSpell)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = cardSpell.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = cardSpell.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = cardSpell.all_power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardSpell.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardSpell.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardSpell, null);
                CreatePropertyUI(1, property, value);
            }
        }
        else if (obj is Collaboration collaboration)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = collaboration.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = collaboration.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = collaboration.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaboration.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = collaboration.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(collaboration, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is CardMonsters cardMonsters)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = cardMonsters.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = cardMonsters.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = cardMonsters.all_power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardMonsters.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardMonsters.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardMonsters, null);
                CreatePropertyUI(1, property, value);
            }
        }
        else if (obj is Equipments equipment)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = equipment.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = equipment.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = equipment.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = equipment.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(equipment, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is Medals medal)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = medal.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = medal.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = medal.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{medal.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = medal.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(medal, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is Skills skill)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = skill.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = skill.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = skill.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{skill.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = skill.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(skill, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is Symbols symbol)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = symbol.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = symbol.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = symbol.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{symbol.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = symbol.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(symbol, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is Titles title)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = title.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = title.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = title.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{title.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = title.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(title, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is MagicFormationCircle magicFormationCircle)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = magicFormationCircle.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = magicFormationCircle.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = magicFormationCircle.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{magicFormationCircle.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = magicFormationCircle.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(magicFormationCircle, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is Relics relics)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = relics.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = relics.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = relics.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{relics.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = relics.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(relics, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is CardColonels cardColonels)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = cardColonels.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = cardColonels.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = cardColonels.all_power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardColonels.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardColonels.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardColonels, null);
                CreatePropertyUI(1, property, value);
            }
        }
        else if (obj is CardGenerals cardGenerals)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = cardGenerals.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = cardGenerals.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = cardGenerals.all_power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardGenerals.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardGenerals.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardGenerals, null);
                CreatePropertyUI(1, property, value);
            }
        }
        else if (obj is CardAdmirals cardAdmirals)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = cardAdmirals.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = cardAdmirals.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = cardAdmirals.all_power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardAdmirals.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardAdmirals.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardAdmirals, null);
                CreatePropertyUI(1, property, value);
            }
        }
        else if (obj is Achievements achievements)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = achievements.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = achievements.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = achievements.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{achievements.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = achievements.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(achievements, null);
                if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
                && !property.Name.Equals("status") && !property.Name.Equals("name")
                && !property.Name.Equals("image"))
                {
                    if (property.Name.Equals("description"))
                    {
                        // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                        GameObject descriptionTextObject = new GameObject("DescriptionText");
                        descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                        // Thêm component TextMeshProUGUI vào đối tượng mới
                        TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                        // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                        descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                        descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                        descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                        // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                        // Đổi màu chữ bằng mã hex #844000
                        Color color;
                        if (ColorUtility.TryParseHtmlString(descriptionColor, out color)) // Chuyển mã hex thành Color
                        {
                            descriptionText.color = color; // Gán màu cho text
                        }

                        // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                        RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                        rectTransform.sizeDelta = new Vector2(600, 100);
                        rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                        GridLayoutGroup gridLayout = descriptionPopupPanel.GetComponent<GridLayoutGroup>();
                        if (gridLayout != null)
                        {
                            gridLayout.cellSize = new Vector2(670, 800);
                        }
                    }
                    else if (property.Name.Equals("power") || property.Name.Equals("rare") || property.Name.Equals("type")
                    || property.Name.Equals("star") || property.Name.Equals("level"))
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, firstPopupPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name);

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = value != null ? value.ToString() : "";
                    }
                    else
                    {
                        // Kiểm tra nếu value không phải null
                        if (value != null)
                        {
                            if (value is double intValue && intValue != -1)
                            {
                                if (property.Name.Contains("all"))
                                {
                                    // Tạo một element mới từ prefab
                                    GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);

                                    // Gán tên thuộc tính vào TitleText
                                    TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                    if (elementTitleText != null)
                                        elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                    // Gán giá trị thuộc tính vào ContentText
                                    TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                    if (elementContentText != null)
                                        elementContentText.text = intValue.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
        else if (obj is Talisman talisman)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = talisman.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = talisman.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = talisman.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{talisman.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = talisman.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(talisman, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is Puppet puppet)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = puppet.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = puppet.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = puppet.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{puppet.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = puppet.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(puppet, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is Alchemy alchemy)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = alchemy.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = alchemy.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = alchemy.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{alchemy.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = alchemy.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(alchemy, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is Forge forge)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = forge.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = forge.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = forge.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{forge.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = forge.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(forge, null);
                CreatePropertyUI(0, property, value);
            }
        }
        else if (obj is CardLife cardLife)
        {
            RawImage Image = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = cardLife.image.Replace(".png", ""); // Lấy giá trị của image từ đối tượng Card
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            ChangeSizeImage(Image, texture);

            TextMeshProUGUI name = currentObject.transform.Find("DictionaryCards/NameText").GetComponent<TextMeshProUGUI>();
            name.text = cardLife.name;

            TextMeshProUGUI power = currentObject.transform.Find("DictionaryCards/PowerText").GetComponent<TextMeshProUGUI>();
            power.text = cardLife.power.ToString();

            // TextMeshProUGUI level = popupObject.transform.Find("DictionaryCards/LevelText").GetComponent<TextMeshProUGUI>();
            // level.text = cardHeroes.level.ToString();

            RawImage rareImage = currentObject.transform.Find("DictionaryCards/RareImage").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{cardLife.rare}");
            rareImage.texture = rareTexture;

            // Button closeButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
            // closeButton.onClick.AddListener(() => ClosePopup(popupObject));

            // Dùng Reflection để lấy tất cả thuộc tính và giá trị
            PropertyInfo[] properties = cardLife.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardLife, null);
                CreatePropertyUI(0, property, value);
            }
        }
    }
    public void GetLevel(object obj)
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(true);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(false);
        Close(DetailsContent);
        Close(SkillsPanel);
        Close(UpgradeElementContent);
        Close(UpgradeMaterialContent);
        Close(LevelElementContent);
        Close(LevelMaterialContent);

        Button up1LevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        if (obj is CardHeroes cardHeroes)
        {
            PropertyInfo[] properties = cardHeroes.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardHeroes, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);
            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CardHeroes currentCard = new CardHeroes();
                currentCard = cardHeroes.GetUserCardHeroesById(User.CurrentUserId, cardHeroes.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;

                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardHeroes newCard = new CardHeroes();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardHeroes.GetNewLevelPower(cardHeroes, increasePerLevel);
                    cardHeroes.UpdateCardHeroesLevel(newCard, currentLevel + 1);
                    cardHeroes.UpdateFactCardHeroes(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                CardHeroes currentCard = cardHeroes.GetUserCardHeroesById(User.CurrentUserId, cardHeroes.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    CardHeroes newCard = cardHeroes.GetNewLevelPower(cardHeroes, levelsGained * increasePerLevel);
                    cardHeroes.UpdateCardHeroesLevel(newCard, currentLevel);
                    cardHeroes.UpdateFactCardHeroes(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Books book)
        {
            PropertyInfo[] properties = book.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(book, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Books currentCard = new Books();
                currentCard = book.GetUserBooksById(User.CurrentUserId, book.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Books newCard = new Books();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = book.GetNewLevelPower(book, increasePerLevel);
                    book.UpdateBooksLevel(newCard, currentLevel + 1);
                    book.UpdateFactBooks(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Books currentCard = book.GetUserBooksById(User.CurrentUserId, book.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Books newCard = book.GetNewLevelPower(book, levelsGained * increasePerLevel);
                    book.UpdateBooksLevel(newCard, currentLevel);
                    book.UpdateFactBooks(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is CardCaptains cardCaptains)
        {
            PropertyInfo[] properties = cardCaptains.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardCaptains, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CardCaptains currentCard = new CardCaptains();
                currentCard = cardCaptains.GetUserCardCaptainsById(User.CurrentUserId, cardCaptains.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardCaptains newCard = new CardCaptains();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardCaptains.GetNewLevelPower(cardCaptains, increasePerLevel);
                    cardCaptains.UpdateCardCaptainsLevel(newCard, currentLevel + 1);
                    cardCaptains.UpdateFactCardCaptains(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                CardCaptains currentCard = cardCaptains.GetUserCardCaptainsById(User.CurrentUserId, cardCaptains.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    CardCaptains newCard = cardCaptains.GetNewLevelPower(cardCaptains, levelsGained * increasePerLevel);
                    cardCaptains.UpdateCardCaptainsLevel(newCard, currentLevel);
                    cardCaptains.UpdateFactCardCaptains(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Pets pet)
        {
            PropertyInfo[] properties = pet.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(pet, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Pets currentCard = new Pets();
                currentCard = pet.GetUserPetsById(User.CurrentUserId, pet.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Pets newCard = new Pets();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = pet.GetNewLevelPower(pet, increasePerLevel);
                    pet.UpdatePetsLevel(newCard, currentLevel + 1);
                    pet.UpdateFactPets(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Pets currentCard = pet.GetUserPetsById(User.CurrentUserId, pet.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Pets newCard = pet.GetNewLevelPower(pet, levelsGained * increasePerLevel);
                    pet.UpdatePetsLevel(newCard, currentLevel);
                    pet.UpdateFactPets(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is CollaborationEquipment collaborationEquipment)
        {
            PropertyInfo[] properties = collaborationEquipment.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(collaborationEquipment, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CollaborationEquipment currentCard = new CollaborationEquipment();
                currentCard = collaborationEquipment.GetUserCollaborationEquipmentsById(User.CurrentUserId, collaborationEquipment.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CollaborationEquipment newCard = new CollaborationEquipment();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = collaborationEquipment.GetNewLevelPower(collaborationEquipment, increasePerLevel);
                    collaborationEquipment.UpdateCollaborationEquipmentsLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                CollaborationEquipment currentCard = collaborationEquipment.GetUserCollaborationEquipmentsById(User.CurrentUserId, collaborationEquipment.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    CollaborationEquipment newCard = collaborationEquipment.GetNewLevelPower(collaborationEquipment, levelsGained * increasePerLevel);
                    collaborationEquipment.UpdateCollaborationEquipmentsLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is CardMilitary cardMilitary)
        {
            PropertyInfo[] properties = cardMilitary.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardMilitary, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CardMilitary currentCard = new CardMilitary();
                currentCard = cardMilitary.GetUserCardMilitaryById(User.CurrentUserId, cardMilitary.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardMilitary newCard = new CardMilitary();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardMilitary.GetNewLevelPower(cardMilitary, increasePerLevel);
                    cardMilitary.UpdateCardMilitaryLevel(newCard, currentLevel + 1);
                    cardMilitary.UpdateFactCardMilitary(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                CardMilitary currentCard = cardMilitary.GetUserCardMilitaryById(User.CurrentUserId, cardMilitary.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    CardMilitary newCard = cardMilitary.GetNewLevelPower(cardMilitary, levelsGained * increasePerLevel);
                    cardMilitary.UpdateCardMilitaryLevel(newCard, currentLevel);
                    cardMilitary.UpdateFactCardMilitary(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is CardSpell cardSpell)
        {
            PropertyInfo[] properties = cardSpell.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardSpell, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CardSpell currentCard = new CardSpell();
                currentCard = cardSpell.GetUserCardSpellById(User.CurrentUserId, cardSpell.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardSpell newCard = new CardSpell();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardSpell.GetNewLevelPower(cardSpell, increasePerLevel);
                    cardSpell.UpdateCardSpellLevel(newCard, currentLevel + 1);
                    cardSpell.UpdateFactCardSpell(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                CardSpell currentCard = cardSpell.GetUserCardSpellById(User.CurrentUserId, cardSpell.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    CardSpell newCard = cardSpell.GetNewLevelPower(cardSpell, levelsGained * increasePerLevel);
                    cardSpell.UpdateCardSpellLevel(newCard, currentLevel);
                    cardSpell.UpdateFactCardSpell(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Collaboration collaboration)
        {
            PropertyInfo[] properties = collaboration.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(collaboration, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Collaboration currentCard = new Collaboration();
                currentCard = collaboration.GetUserCollaborationsById(User.CurrentUserId, collaboration.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Collaboration newCard = new Collaboration();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = collaboration.GetNewLevelPower(collaboration, increasePerLevel);
                    collaboration.UpdateCollaborationsLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Collaboration currentCard = collaboration.GetUserCollaborationsById(User.CurrentUserId, collaboration.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Collaboration newCard = collaboration.GetNewLevelPower(collaboration, levelsGained * increasePerLevel);
                    collaboration.UpdateCollaborationsLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is CardMonsters cardMonsters)
        {
            PropertyInfo[] properties = cardMonsters.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardMonsters, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CardMonsters currentCard = new CardMonsters();
                currentCard = cardMonsters.GetUserCardMonstersById(User.CurrentUserId, cardMonsters.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardMonsters newCard = new CardMonsters();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardMonsters.GetNewLevelPower(cardMonsters, increasePerLevel);
                    cardMonsters.UpdateCardMonstersLevel(newCard, currentLevel + 1);
                    cardMonsters.UpdateFactCardMonsters(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                CardMonsters currentCard = cardMonsters.GetUserCardMonstersById(User.CurrentUserId, cardMonsters.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    CardMonsters newCard = cardMonsters.GetNewLevelPower(cardMonsters, levelsGained * increasePerLevel);
                    cardMonsters.UpdateCardMonstersLevel(newCard, currentLevel);
                    cardMonsters.UpdateFactCardMonsters(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Equipments equipment)
        {
            PropertyInfo[] properties = equipment.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(equipment, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Equipments currentCard = new Equipments();
                currentCard = equipment.GetUserEquipmentsById(User.CurrentUserId, equipment.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Equipments newCard = new Equipments();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = equipment.GetNewLevelPower(equipment, increasePerLevel);
                    equipment.UpdateEquipmentsLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Equipments currentCard = equipment.GetUserEquipmentsById(User.CurrentUserId, equipment.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Equipments newCard = equipment.GetNewLevelPower(equipment, levelsGained * increasePerLevel);
                    equipment.UpdateEquipmentsLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Medals medal)
        {
            PropertyInfo[] properties = medal.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(medal, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Medals currentCard = new Medals();
                currentCard = medal.GetUserMedalsById(User.CurrentUserId, medal.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Medals newCard = new Medals();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = medal.GetNewLevelPower(medal, increasePerLevel);
                    medal.UpdateMedalsLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Medals currentCard = medal.GetUserMedalsById(User.CurrentUserId, medal.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Medals newCard = medal.GetNewLevelPower(medal, levelsGained * increasePerLevel);
                    medal.UpdateMedalsLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Skills skill)
        {
            PropertyInfo[] properties = skill.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(skill, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Skills currentCard = new Skills();
                currentCard = skill.GetUserSkillsById(User.CurrentUserId, skill.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Skills newCard = new Skills();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = skill.GetNewLevelPower(skill, increasePerLevel);
                    skill.UpdateSkillsLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Skills currentCard = skill.GetUserSkillsById(User.CurrentUserId, skill.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Skills newCard = skill.GetNewLevelPower(skill, levelsGained * increasePerLevel);
                    skill.UpdateSkillsLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Symbols symbol)
        {
            PropertyInfo[] properties = symbol.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(symbol, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Symbols currentCard = new Symbols();
                currentCard = symbol.GetUserSymbolsById(User.CurrentUserId, symbol.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Symbols newCard = new Symbols();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = symbol.GetNewLevelPower(symbol, increasePerLevel);
                    symbol.UpdateSymbolsLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Symbols currentCard = symbol.GetUserSymbolsById(User.CurrentUserId, symbol.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Symbols newCard = symbol.GetNewLevelPower(symbol, levelsGained * increasePerLevel);
                    symbol.UpdateSymbolsLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Titles title)
        {
            PropertyInfo[] properties = title.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(title, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Titles currentCard = new Titles();
                currentCard = title.GetUserTitlesById(User.CurrentUserId, title.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Titles newCard = new Titles();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = title.GetNewLevelPower(title, increasePerLevel);
                    title.UpdateTitlesLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Titles currentCard = title.GetUserTitlesById(User.CurrentUserId, title.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Titles newCard = title.GetNewLevelPower(title, levelsGained * increasePerLevel);
                    title.UpdateTitlesLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is MagicFormationCircle magicFormationCircle)
        {
            PropertyInfo[] properties = magicFormationCircle.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(magicFormationCircle, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                MagicFormationCircle currentCard = new MagicFormationCircle();
                currentCard = magicFormationCircle.GetUserMagicFormationCirlceById(User.CurrentUserId, magicFormationCircle.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    MagicFormationCircle newCard = new MagicFormationCircle();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = magicFormationCircle.GetNewLevelPower(magicFormationCircle, increasePerLevel);
                    magicFormationCircle.UpdateMagicFormationCircleLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                MagicFormationCircle currentCard = magicFormationCircle.GetUserMagicFormationCirlceById(User.CurrentUserId, magicFormationCircle.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    MagicFormationCircle newCard = magicFormationCircle.GetNewLevelPower(magicFormationCircle, levelsGained * increasePerLevel);
                    magicFormationCircle.UpdateMagicFormationCircleLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Relics relics)
        {
            PropertyInfo[] properties = relics.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(relics, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Relics currentCard = new Relics();
                currentCard = relics.GetUserRelicsById(User.CurrentUserId, relics.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Relics newCard = new Relics();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = relics.GetNewLevelPower(relics, increasePerLevel);
                    relics.UpdateRelicsLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Relics currentCard = relics.GetUserRelicsById(User.CurrentUserId, relics.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Relics newCard = relics.GetNewLevelPower(relics, levelsGained * increasePerLevel);
                    relics.UpdateRelicsLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is CardColonels cardColonels)
        {
            PropertyInfo[] properties = cardColonels.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardColonels, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CardColonels currentCard = new CardColonels();
                currentCard = cardColonels.GetUserCardColonelsById(User.CurrentUserId, cardColonels.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardColonels newCard = new CardColonels();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardColonels.GetNewLevelPower(cardColonels, increasePerLevel);
                    cardColonels.UpdateCardColonelsLevel(newCard, currentLevel + 1);
                    cardColonels.UpdateFactCardColonels(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                CardColonels currentCard = cardColonels.GetUserCardColonelsById(User.CurrentUserId, cardColonels.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    CardColonels newCard = cardColonels.GetNewLevelPower(cardColonels, levelsGained * increasePerLevel);
                    cardColonels.UpdateCardColonelsLevel(newCard, currentLevel);
                    cardColonels.UpdateFactCardColonels(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is CardGenerals cardGenerals)
        {
            PropertyInfo[] properties = cardGenerals.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardGenerals, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CardGenerals currentCard = new CardGenerals();
                currentCard = cardGenerals.GetUserCardGeneralsById(User.CurrentUserId, cardGenerals.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardGenerals newCard = new CardGenerals();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardGenerals.GetNewLevelPower(cardGenerals, increasePerLevel);
                    cardGenerals.UpdateCardGeneralsLevel(newCard, currentLevel + 1);
                    cardGenerals.UpdateFactCardGenerals(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                CardGenerals currentCard = cardGenerals.GetUserCardGeneralsById(User.CurrentUserId, cardGenerals.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    CardGenerals newCard = cardGenerals.GetNewLevelPower(cardGenerals, levelsGained * increasePerLevel);
                    cardGenerals.UpdateCardGeneralsLevel(newCard, currentLevel);
                    cardGenerals.UpdateFactCardGenerals(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is CardAdmirals cardAdmirals)
        {
            PropertyInfo[] properties = cardAdmirals.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardAdmirals, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CardAdmirals currentCard = new CardAdmirals();
                currentCard = cardAdmirals.GetUserCardAdmiralsById(User.CurrentUserId, cardAdmirals.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardAdmirals newCard = new CardAdmirals();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardAdmirals.GetNewLevelPower(cardAdmirals, increasePerLevel);
                    cardAdmirals.UpdateCardAdmiralsLevel(newCard, currentLevel + 1);
                    cardAdmirals.UpdateFactCardAdmirals(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                CardAdmirals currentCard = cardAdmirals.GetUserCardAdmiralsById(User.CurrentUserId, cardAdmirals.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    CardAdmirals newCard = cardAdmirals.GetNewLevelPower(cardAdmirals, levelsGained * increasePerLevel);
                    cardAdmirals.UpdateCardAdmiralsLevel(newCard, currentLevel);
                    cardAdmirals.UpdateFactCardAdmirals(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Achievements achievements)
        {
            PropertyInfo[] properties = achievements.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(achievements, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Achievements currentCard = new Achievements();
                currentCard = achievements.GetUserAchievementsById(User.CurrentUserId, achievements.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Achievements newCard = new Achievements();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = achievements.GetNewLevelPower(achievements, increasePerLevel);
                    achievements.UpdateAchievementLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Achievements currentCard = achievements.GetUserAchievementsById(User.CurrentUserId, achievements.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Achievements newCard = achievements.GetNewLevelPower(achievements, levelsGained * increasePerLevel);
                    achievements.UpdateAchievementLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Talisman talisman)
        {
            PropertyInfo[] properties = talisman.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(talisman, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Talisman currentCard = new Talisman();
                currentCard = talisman.GetUserTalismanById(User.CurrentUserId, talisman.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Talisman newCard = new Talisman();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = talisman.GetNewLevelPower(talisman, increasePerLevel);
                    talisman.UpdateTalismanLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Talisman currentCard = talisman.GetUserTalismanById(User.CurrentUserId, talisman.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Talisman newCard = talisman.GetNewLevelPower(talisman, levelsGained * increasePerLevel);
                    talisman.UpdateTalismanLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Puppet puppet)
        {
            PropertyInfo[] properties = puppet.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(puppet, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Puppet currentCard = new Puppet();
                currentCard = puppet.GetUserPuppetById(User.CurrentUserId, puppet.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Puppet newCard = new Puppet();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = puppet.GetNewLevelPower(puppet, increasePerLevel);
                    puppet.UpdatePuppetLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Puppet currentCard = puppet.GetUserPuppetById(User.CurrentUserId, puppet.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Puppet newCard = puppet.GetNewLevelPower(puppet, levelsGained * increasePerLevel);
                    puppet.UpdatePuppetLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Alchemy alchemy)
        {
            PropertyInfo[] properties = alchemy.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(alchemy, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Alchemy currentCard = new Alchemy();
                currentCard = alchemy.GetUserAlchemyById(User.CurrentUserId, alchemy.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Alchemy newCard = new Alchemy();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = alchemy.GetNewLevelPower(alchemy, increasePerLevel);
                    alchemy.UpdateAlchemyLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Alchemy currentCard = alchemy.GetUserAlchemyById(User.CurrentUserId, alchemy.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Alchemy newCard = alchemy.GetNewLevelPower(alchemy, levelsGained * increasePerLevel);
                    alchemy.UpdateAlchemyLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is Forge forge)
        {
            PropertyInfo[] properties = forge.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(forge, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                Forge currentCard = new Forge();
                currentCard = forge.GetUserForgeById(User.CurrentUserId, forge.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    Forge newCard = new Forge();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = forge.GetNewLevelPower(forge, increasePerLevel);
                    forge.UpdateForgeLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                Forge currentCard = forge.GetUserForgeById(User.CurrentUserId, forge.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    Forge newCard = forge.GetNewLevelPower(forge, levelsGained * increasePerLevel);
                    forge.UpdateForgeLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
        else if (obj is CardLife cardLife)
        {
            PropertyInfo[] properties = cardLife.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardLife, null);
                CreatePropertyLevelUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForLevel(mainType);
            CreateMaterialUI(items);

            up1LevelButton.onClick.RemoveAllListeners();
            upMaxLevelButton.onClick.RemoveAllListeners();
            up1LevelButton.onClick.AddListener(() =>
            {
                CardLife currentCard = new CardLife();
                currentCard = cardLife.GetUserCardLifeById(User.CurrentUserId, cardLife.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel;
                int maxLevel = 100000;
                bool canLevel = UpOneLevelCondition(items, currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    CardLife newCard = new CardLife();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardLife.GetNewLevelPower(cardLife, increasePerLevel);
                    cardLife.UpdateCardLifeLevel(newCard, currentLevel + 1);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                CardLife currentCard = cardLife.GetUserCardLifeById(User.CurrentUserId, cardLife.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int originalLevel = currentLevel;
                int experimentCondition = currentLevel == 0 ? 100 : currentLevel * 100;
                int userMaxLevel = User.CurrentUserLevel; // Điều kiện 1: Không vượt quá cấp độ của User
                int maxLevel = 100000; // Điều kiện 3: Không vượt quá 100000

                bool canLevel = UpMaxLevelCondition(items, ref currentLevel, userMaxLevel, maxLevel, experimentCondition, totalExperiment);
                if (canLevel)
                {
                    // Tính số cấp đã tăng
                    int levelsGained = currentLevel - originalLevel;

                    // Cập nhật cấp độ và trạng thái của thẻ bài
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    CardLife newCard = cardLife.GetNewLevelPower(cardLife, levelsGained * increasePerLevel);
                    cardLife.UpdateCardLifeLevel(newCard, currentLevel);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(LevelElementContent);
                    Close(LevelMaterialContent);
                    GetLevel(obj);
                    CreateLevelUI(currentLevel);
                }
            });
        }
    }
    public void GetSkills(object obj)
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(true);
        UpgradePanel.gameObject.SetActive(false);
        Close(DetailsContent);
        Close(LevelElementContent);
        Close(LevelMaterialContent);
        Close(UpgradeElementContent);
        Close(UpgradeMaterialContent);
        if (obj is CardHeroes cardHeroes)
        {

        }
        else if (obj is Books book)
        {

        }
        else if (obj is CardCaptains captain)
        {

        }
        else if (obj is Pets pet)
        {

        }
        else if (obj is CollaborationEquipment collaborationEquipmentsequipment)
        {

        }
        else if (obj is CardMilitary military)
        {

        }
        else if (obj is CardSpell spell)
        {

        }
        else if (obj is Collaboration collaboration)
        {

        }
        else if (obj is CardMonsters monster)
        {

        }
        else if (obj is Equipments equipment)
        {

        }
        else if (obj is Medals medal)
        {

        }
        else if (obj is Skills skill)
        {

        }
        else if (obj is Symbols symbol)
        {

        }
        else if (obj is Titles title)
        {

        }
        else if (obj is MagicFormationCircle magicFormationCircle)
        {

        }
        else if (obj is Relics relics)
        {

        }
        else if (obj is CardColonels colonels)
        {

        }
        else if (obj is CardGenerals generals)
        {

        }
        else if (obj is CardAdmirals admirals)
        {

        }
        else if (obj is Achievements achievements)
        {

        }
    }
    public void GetUpgrade(object obj)
    {
        DetailsPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(false);
        SkillsPanel.gameObject.SetActive(false);
        UpgradePanel.gameObject.SetActive(true);
        Close(DetailsContent);
        Close(LevelElementContent);
        Close(LevelMaterialContent);
        Close(SkillsPanel);
        Close(UpgradeElementContent);
        Close(UpgradeMaterialContent);

        Button breakthroughButton = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/BreakthroughButton").GetComponent<Button>();
        if (obj is CardHeroes cardHeroes)
        {
            PropertyInfo[] properties = cardHeroes.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardHeroes, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardHeroes.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = cardHeroes.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardHeroes.quantity.ToString() + "/" + (cardHeroes.star + 1).ToString();

            CreateStarUI(cardHeroes.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = cardHeroes.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = cardHeroes.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + cardHeroes.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (cardHeroes.quantity >= requiredQuantity)
                    {
                        cardHeroes.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - cardHeroes.quantity;
                        cardHeroes.quantity = 0; // Dùng hết thẻ bài

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardHeroes newCard = new CardHeroes();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardHeroes.GetNewBreakthroughPower(cardHeroes, increasePerUpgrade);
                    cardHeroes.UpdateCardHeroesBreakthrough(newCard, cardHeroes.star + 1, cardHeroes.quantity);
                    cardHeroes.UpdateFactCardHeroes(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(cardHeroes.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
        else if (obj is Books book)
        {
            PropertyInfo[] properties = book.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(book, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (book.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = book.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = book.quantity.ToString() + "/" + (book.star + 1).ToString();

            CreateStarUI(book.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = book.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = book.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + book.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (book.quantity >= requiredQuantity)
                    {
                        book.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - book.quantity;
                        book.quantity = 0; // Dùng hết thẻ bài

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Books newCard = new Books();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = book.GetNewBreakthroughPower(book, increasePerUpgrade);
                    book.UpdateBooksBreakthrough(newCard, book.star + 1, book.quantity);
                    book.UpdateFactBooks(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(book.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
        else if (obj is CardCaptains cardCaptains)
        {
            PropertyInfo[] properties = cardCaptains.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardCaptains, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardCaptains.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = cardCaptains.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardCaptains.quantity.ToString() + "/" + (cardCaptains.star + 1).ToString();

            CreateStarUI(cardCaptains.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = cardCaptains.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = cardCaptains.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + cardCaptains.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (cardCaptains.quantity >= requiredQuantity)
                    {
                        cardCaptains.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - cardCaptains.quantity;
                        cardCaptains.quantity = 0; // Dùng hết thẻ bài

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardCaptains newCard = new CardCaptains();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardCaptains.GetNewBreakthroughPower(cardCaptains, increasePerUpgrade);
                    cardCaptains.UpdateCardCaptainsBreakthrough(newCard, cardCaptains.star + 1, cardCaptains.quantity);
                    cardCaptains.UpdateFactCardCaptains(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(cardCaptains.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
        else if (obj is Pets pet)
        {
            PropertyInfo[] properties = pet.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(pet, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (pet.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = pet.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = pet.quantity.ToString() + "/" + (pet.star + 1).ToString();

            CreateStarUI(pet.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = pet.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = pet.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + pet.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (pet.quantity >= requiredQuantity)
                    {
                        pet.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - pet.quantity;
                        pet.quantity = 0; // Dùng hết thẻ bài

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Pets newCard = new Pets();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = pet.GetNewBreakthroughPower(pet, increasePerUpgrade);
                    pet.UpdatePetsBreakthrough(newCard, pet.star + 1, pet.quantity);
                    pet.UpdateFactPets(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(pet.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
        else if (obj is CollaborationEquipment collaborationEquipment)
        {
            PropertyInfo[] properties = collaborationEquipment.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(collaborationEquipment, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (collaborationEquipment.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = collaborationEquipment.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = collaborationEquipment.quantity.ToString() + "/" + (collaborationEquipment.star + 1).ToString();

            CreateStarUI(collaborationEquipment.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = collaborationEquipment.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = collaborationEquipment.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + collaborationEquipment.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (collaborationEquipment.quantity >= requiredQuantity)
                    {
                        collaborationEquipment.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - collaborationEquipment.quantity;
                        collaborationEquipment.quantity = 0; // Dùng hết thẻ bài

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CollaborationEquipment newCard = new CollaborationEquipment();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = collaborationEquipment.GetNewBreakthroughPower(collaborationEquipment, increasePerUpgrade);
                    collaborationEquipment.UpdateCollaborationEquipmentsBreakthrough(newCard, collaborationEquipment.star + 1, collaborationEquipment.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(collaborationEquipment.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
        else if (obj is CardMilitary cardMilitary)
        {
            PropertyInfo[] properties = cardMilitary.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardMilitary, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardMilitary.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = cardMilitary.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardMilitary.quantity.ToString() + "/" + (cardMilitary.star + 1).ToString();

            CreateStarUI(cardMilitary.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = cardMilitary.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = cardMilitary.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + cardMilitary.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (cardMilitary.quantity >= requiredQuantity)
                    {
                        cardMilitary.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - cardMilitary.quantity;
                        cardMilitary.quantity = 0; // Dùng hết thẻ bài

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardMilitary newCard = new CardMilitary();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardMilitary.GetNewBreakthroughPower(cardMilitary, increasePerUpgrade);
                    cardMilitary.UpdateCardMilitaryBreakthrough(newCard, cardMilitary.star + 1, cardMilitary.quantity);
                    cardMilitary.UpdateFactCardMilitary(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(cardMilitary.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
        else if (obj is CardSpell cardSpell)
        {
            PropertyInfo[] properties = cardSpell.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardSpell, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardSpell.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = cardSpell.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardSpell.quantity.ToString() + "/" + (cardSpell.star + 1).ToString();

            CreateStarUI(cardSpell.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = cardSpell.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = cardSpell.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + cardSpell.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (cardSpell.quantity >= requiredQuantity)
                    {
                        cardSpell.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - cardSpell.quantity;
                        cardSpell.quantity = 0; // Dùng hết thẻ bài

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardSpell newCard = new CardSpell();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardSpell.GetNewBreakthroughPower(cardSpell, increasePerUpgrade);
                    cardSpell.UpdateCardSpellBreakthrough(newCard, cardSpell.star + 1, cardSpell.quantity);
                    cardSpell.UpdateFactCardSpell(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(cardSpell.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
        else if (obj is Collaboration collaboration)
        {
            PropertyInfo[] properties = collaboration.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(collaboration, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (collaboration.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = collaboration.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = collaboration.quantity.ToString() + "/" + (collaboration.star + 1).ToString();

            CreateStarUI(collaboration.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = collaboration.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = collaboration.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + collaboration.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (collaboration.quantity >= requiredQuantity)
                    {
                        collaboration.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - collaboration.quantity;
                        collaboration.quantity = 0; // Dùng hết thẻ bài

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Collaboration newCollaboration = new Collaboration();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCollaboration = collaboration.GetNewBreakthroughPower(collaboration, increasePerUpgrade);
                    collaboration.UpdateCollaborationsBreakthrough(newCollaboration, collaboration.star + 1, collaboration.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(collaboration.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp Collaboration!");
                }
            });
        }
        else if (obj is CardMonsters cardMonsters)
        {
            PropertyInfo[] properties = cardMonsters.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardMonsters, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardMonsters.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = cardMonsters.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardMonsters.quantity.ToString() + "/" + (cardMonsters.star + 1).ToString();

            CreateStarUI(cardMonsters.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = cardMonsters.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = cardMonsters.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + cardMonsters.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (cardMonsters.quantity >= requiredQuantity)
                    {
                        cardMonsters.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - cardMonsters.quantity;
                        cardMonsters.quantity = 0; // Dùng hết thẻ bài

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardMonsters newMonster = new CardMonsters();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newMonster = cardMonsters.GetNewBreakthroughPower(cardMonsters, increasePerUpgrade);
                    cardMonsters.UpdateCardMonstersBreakthrough(newMonster, cardMonsters.star + 1, cardMonsters.quantity);
                    cardMonsters.UpdateFactCardMonsters(newMonster);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(cardMonsters.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp CardMonsters!");
                }
            });
        }
        else if (obj is Equipments equipments)
        {
            PropertyInfo[] properties = equipments.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(equipments, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (equipments.star + 1).ToString();
            }
            GameObject equipmentObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage equipmentImage = equipmentObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = equipments.image.Replace(".png", "");
            Texture equipTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            equipmentImage.texture = equipTexture;

            TextMeshProUGUI equipQuantity = equipmentObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            equipQuantity.text = equipments.quantity.ToString() + "/" + (equipments.star + 1).ToString();

            CreateStarUI(equipments.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = equipments.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng trang bị
                bool hasEnoughEquipments = equipments.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + equipments.quantity >= requiredQuantity;

                if (hasEnoughEquipments || hasEnoughItems)
                {
                    // Giảm số lượng trang bị trước
                    if (equipments.quantity >= requiredQuantity)
                    {
                        equipments.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu trang bị không đủ, dùng cả trang bị + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - equipments.quantity;
                        equipments.quantity = 0; // Dùng hết trang bị

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Equipments newEquipment = new Equipments();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newEquipment = equipments.GetNewBreakthroughPower(equipments, increasePerUpgrade);
                    equipments.UpdateEquipmentsBreakthrough(newEquipment, equipments.star + 1, equipments.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(equipments.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp Equipments!");
                }
            });
        }
        else if (obj is Medals medal)
        {
            PropertyInfo[] properties = medal.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(medal, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (medal.star + 1).ToString();
            }
            GameObject medalObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage medalImage = medalObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = medal.image.Replace(".png", "");
            Texture medalTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            medalImage.texture = medalTexture;

            TextMeshProUGUI medalQuantity = medalObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            medalQuantity.text = medal.quantity.ToString() + "/" + (medal.star + 1).ToString();

            CreateStarUI(medal.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = medal.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng huy chương
                bool hasEnoughMedals = medal.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + medal.quantity >= requiredQuantity;

                if (hasEnoughMedals || hasEnoughItems)
                {
                    // Giảm số lượng huy chương trước
                    if (medal.quantity >= requiredQuantity)
                    {
                        medal.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu huy chương không đủ, dùng cả huy chương + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - medal.quantity;
                        medal.quantity = 0; // Dùng hết huy chương

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Medals newMedal = new Medals();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newMedal = medal.GetNewBreakthroughPower(medal, increasePerUpgrade);
                    medal.UpdateMedalsBreakthrough(newMedal, medal.star + 1, medal.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(medal.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp Medals!");
                }
            });
        }
        else if (obj is Skills skill)
        {
            PropertyInfo[] properties = skill.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(skill, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (skill.star + 1).ToString();
            }
            GameObject skillObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage skillImage = skillObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = skill.image.Replace(".png", "");
            Texture skillTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            skillImage.texture = skillTexture;

            TextMeshProUGUI skillQuantity = skillObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            skillQuantity.text = skill.quantity.ToString() + "/" + (skill.star + 1).ToString();

            CreateStarUI(skill.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = skill.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng kỹ năng
                bool hasEnoughSkills = skill.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + skill.quantity >= requiredQuantity;

                if (hasEnoughSkills || hasEnoughItems)
                {
                    // Giảm số lượng kỹ năng trước
                    if (skill.quantity >= requiredQuantity)
                    {
                        skill.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu kỹ năng không đủ, dùng cả kỹ năng + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - skill.quantity;
                        skill.quantity = 0; // Dùng hết kỹ năng

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Skills newSkill = new Skills();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newSkill = skill.GetNewBreakthroughPower(skill, increasePerUpgrade);
                    skill.UpdateSkillsBreakthrough(newSkill, skill.star + 1, skill.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(skill.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp kỹ năng!");
                }
            });
        }
        else if (obj is Symbols symbol)
        {
            PropertyInfo[] properties = symbol.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(symbol, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (symbol.star + 1).ToString();
            }
            GameObject symbolObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage symbolImage = symbolObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = symbol.image.Replace(".png", "");
            Texture symbolTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            symbolImage.texture = symbolTexture;

            TextMeshProUGUI symbolQuantity = symbolObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            symbolQuantity.text = symbol.quantity.ToString() + "/" + (symbol.star + 1).ToString();

            CreateStarUI(symbol.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = symbol.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng biểu tượng
                bool hasEnoughSymbols = symbol.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + symbol.quantity >= requiredQuantity;

                if (hasEnoughSymbols || hasEnoughItems)
                {
                    // Giảm số lượng biểu tượng trước
                    if (symbol.quantity >= requiredQuantity)
                    {
                        symbol.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu biểu tượng không đủ, dùng cả biểu tượng + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - symbol.quantity;
                        symbol.quantity = 0; // Dùng hết biểu tượng

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Symbols newSymbol = new Symbols();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newSymbol = symbol.GetNewBreakthroughPower(symbol, increasePerUpgrade);
                    symbol.UpdateSymbolsBreakthrough(newSymbol, symbol.star + 1, symbol.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(symbol.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp biểu tượng!");
                }
            });
        }
        else if (obj is Titles title)
        {
            PropertyInfo[] properties = title.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(title, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (title.star + 1).ToString();
            }
            GameObject titleObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage titleImage = titleObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = title.image.Replace(".png", "");
            Texture titleTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            titleImage.texture = titleTexture;

            TextMeshProUGUI titleQuantity = titleObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            titleQuantity.text = title.quantity.ToString() + "/" + (title.star + 1).ToString();

            CreateStarUI(title.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = title.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng danh hiệu
                bool hasEnoughTitles = title.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + title.quantity >= requiredQuantity;

                if (hasEnoughTitles || hasEnoughItems)
                {
                    // Giảm số lượng danh hiệu trước
                    if (title.quantity >= requiredQuantity)
                    {
                        title.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu danh hiệu không đủ, dùng cả danh hiệu + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - title.quantity;
                        title.quantity = 0; // Dùng hết danh hiệu

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Titles newTitle = new Titles();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newTitle = title.GetNewBreakthroughPower(title, increasePerUpgrade);
                    title.UpdateTitlesBreakthrough(newTitle, title.star + 1, title.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(title.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp danh hiệu!");
                }
            });
        }
        else if (obj is MagicFormationCircle magicFormationCircle)
        {
            PropertyInfo[] properties = magicFormationCircle.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(magicFormationCircle, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (magicFormationCircle.star + 1).ToString();
            }
            GameObject magicFormationObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage magicFormationImage = magicFormationObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = magicFormationCircle.image.Replace(".png", "");
            Texture magicFormationTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            magicFormationImage.texture = magicFormationTexture;

            TextMeshProUGUI magicFormationQuantity = magicFormationObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            magicFormationQuantity.text = magicFormationCircle.quantity.ToString() + "/" + (magicFormationCircle.star + 1).ToString();

            CreateStarUI(magicFormationCircle.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = magicFormationCircle.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng vòng phép
                bool hasEnoughMagicFormation = magicFormationCircle.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + magicFormationCircle.quantity >= requiredQuantity;

                if (hasEnoughMagicFormation || hasEnoughItems)
                {
                    // Giảm số lượng vòng phép trước
                    if (magicFormationCircle.quantity >= requiredQuantity)
                    {
                        magicFormationCircle.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu vòng phép không đủ, dùng cả vòng phép + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - magicFormationCircle.quantity;
                        magicFormationCircle.quantity = 0; // Dùng hết vòng phép

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    MagicFormationCircle newMagicFormationCircle = new MagicFormationCircle();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newMagicFormationCircle = magicFormationCircle.GetNewBreakthroughPower(magicFormationCircle, increasePerUpgrade);
                    magicFormationCircle.UpdateMagicFormationCircleBreakthrough(newMagicFormationCircle, magicFormationCircle.star + 1, magicFormationCircle.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(magicFormationCircle.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp vòng phép!");
                }
            });
        }
        else if (obj is Relics relics)
        {
            PropertyInfo[] properties = relics.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(relics, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (relics.star + 1).ToString();
            }
            GameObject relicsObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage relicsImage = relicsObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = relics.image.Replace(".png", "");
            Texture relicsTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            relicsImage.texture = relicsTexture;

            TextMeshProUGUI relicsQuantity = relicsObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            relicsQuantity.text = relics.quantity.ToString() + "/" + (relics.star + 1).ToString();

            CreateStarUI(relics.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = relics.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng di vật
                bool hasEnoughRelics = relics.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + relics.quantity >= requiredQuantity;

                if (hasEnoughRelics || hasEnoughItems)
                {
                    // Giảm số lượng di vật trước
                    if (relics.quantity >= requiredQuantity)
                    {
                        relics.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu di vật không đủ, dùng cả di vật + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - relics.quantity;
                        relics.quantity = 0; // Dùng hết di vật

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Relics newRelics = new Relics();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newRelics = relics.GetNewBreakthroughPower(relics, increasePerUpgrade);
                    relics.UpdateRelicsBreakthrough(newRelics, relics.star + 1, relics.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(relics.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp di vật!");
                }
            });
        }
        else if (obj is CardColonels cardColonels)
        {
            PropertyInfo[] properties = cardColonels.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardColonels, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardColonels.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = cardColonels.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardColonels.quantity.ToString() + "/" + (cardColonels.star + 1).ToString();

            CreateStarUI(cardColonels.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = cardColonels.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = cardColonels.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + cardColonels.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (cardColonels.quantity >= requiredQuantity)
                    {
                        cardColonels.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - cardColonels.quantity;
                        cardColonels.quantity = 0; // Dùng hết thẻ bài

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardColonels newCard = new CardColonels();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardColonels.GetNewBreakthroughPower(cardColonels, increasePerUpgrade);
                    cardColonels.UpdateCardColonelsBreakthrough(newCard, cardColonels.star + 1, cardColonels.quantity);
                    cardColonels.UpdateFactCardColonels(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(cardColonels.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
        else if (obj is CardGenerals cardGenerals)
        {
            PropertyInfo[] properties = cardGenerals.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardGenerals, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardGenerals.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = cardGenerals.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardGenerals.quantity.ToString() + "/" + (cardGenerals.star + 1).ToString();

            CreateStarUI(cardGenerals.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = cardGenerals.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thẻ bài
                bool hasEnoughCards = cardGenerals.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + cardGenerals.quantity >= requiredQuantity;

                if (hasEnoughCards || hasEnoughItems)
                {
                    // Giảm số lượng thẻ bài trước
                    if (cardGenerals.quantity >= requiredQuantity)
                    {
                        cardGenerals.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thẻ bài không đủ, dùng cả thẻ bài + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - cardGenerals.quantity;
                        cardGenerals.quantity = 0; // Dùng hết thẻ bài

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardGenerals newCard = new CardGenerals();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardGenerals.GetNewBreakthroughPower(cardGenerals, increasePerUpgrade);
                    cardGenerals.UpdateCardGeneralsBreakthrough(newCard, cardGenerals.star + 1, cardGenerals.quantity);
                    cardGenerals.UpdateFactCardGenerals(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(cardGenerals.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
        else if (obj is CardAdmirals cardAdmirals)
        {
            PropertyInfo[] properties = cardAdmirals.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardAdmirals, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = equipmentTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardAdmirals.star + 1).ToString();
            }
            GameObject cardObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage cardImage = cardObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = cardAdmirals.image.Replace(".png", "");
            Texture cardTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = cardTexture;

            TextMeshProUGUI cardQuantity = cardObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            cardQuantity.text = cardAdmirals.quantity.ToString() + "/" + (cardAdmirals.star + 1).ToString();

            CreateStarUI(cardAdmirals.star);
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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardAdmirals newCard = new CardAdmirals();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCard = cardAdmirals.GetNewBreakthroughPower(cardAdmirals, increasePerUpgrade);
                    cardAdmirals.UpdateCardAdmiralsBreakthrough(newCard, cardAdmirals.star + 1, cardAdmirals.quantity);
                    cardAdmirals.UpdateFactCardAdmirals(newCard);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(cardAdmirals.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thẻ bài!");
                }
            });
        }
        else if (obj is Achievements achievements)
        {
            PropertyInfo[] properties = achievements.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(achievements, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (achievements.star + 1).ToString();
            }
            GameObject achievementsObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage achievementsImage = achievementsObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = achievements.image.Replace(".png", "");
            Texture achievementsTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            achievementsImage.texture = achievementsTexture;

            TextMeshProUGUI achievementsQuantity = achievementsObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            achievementsQuantity.text = achievements.quantity.ToString() + "/" + (achievements.star + 1).ToString();

            CreateStarUI(achievements.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = achievements.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng thành tích
                bool hasEnoughAchievements = achievements.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + achievements.quantity >= requiredQuantity;

                if (hasEnoughAchievements || hasEnoughItems)
                {
                    // Giảm số lượng thành tích trước
                    if (achievements.quantity >= requiredQuantity)
                    {
                        achievements.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu thành tích không đủ, dùng cả thành tích + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - achievements.quantity;
                        achievements.quantity = 0; // Dùng hết thành tích

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Achievements newAchievements = new Achievements();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newAchievements = achievements.GetNewBreakthroughPower(achievements, increasePerUpgrade);
                    achievements.UpdateAchievementsBreakthrough(newAchievements, achievements.star + 1, achievements.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(achievements.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp thành tích!");
                }
            });
        }
        else if (obj is Talisman talisman)
        {
            PropertyInfo[] properties = talisman.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(talisman, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (talisman.star + 1).ToString();
            }
            GameObject magicFormationObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage magicFormationImage = magicFormationObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = talisman.image.Replace(".png", "");
            Texture magicFormationTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            magicFormationImage.texture = magicFormationTexture;

            TextMeshProUGUI magicFormationQuantity = magicFormationObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            magicFormationQuantity.text = talisman.quantity.ToString() + "/" + (talisman.star + 1).ToString();

            CreateStarUI(talisman.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = talisman.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng vòng phép
                bool hasEnoughMagicFormation = talisman.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + talisman.quantity >= requiredQuantity;

                if (hasEnoughMagicFormation || hasEnoughItems)
                {
                    // Giảm số lượng vòng phép trước
                    if (talisman.quantity >= requiredQuantity)
                    {
                        talisman.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu vòng phép không đủ, dùng cả vòng phép + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - talisman.quantity;
                        talisman.quantity = 0; // Dùng hết vòng phép

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Talisman newtalisman = new Talisman();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newtalisman = talisman.GetNewBreakthroughPower(talisman, increasePerUpgrade);
                    talisman.UpdateTalismanBreakthrough(newtalisman, talisman.star + 1, talisman.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(talisman.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp vòng phép!");
                }
            });
        }
        else if (obj is Puppet puppet)
        {
            PropertyInfo[] properties = puppet.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(puppet, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (puppet.star + 1).ToString();
            }
            GameObject magicFormationObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage magicFormationImage = magicFormationObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = puppet.image.Replace(".png", "");
            Texture magicFormationTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            magicFormationImage.texture = magicFormationTexture;

            TextMeshProUGUI magicFormationQuantity = magicFormationObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            magicFormationQuantity.text = puppet.quantity.ToString() + "/" + (puppet.star + 1).ToString();

            CreateStarUI(puppet.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = puppet.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng vòng phép
                bool hasEnoughMagicFormation = puppet.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + puppet.quantity >= requiredQuantity;

                if (hasEnoughMagicFormation || hasEnoughItems)
                {
                    // Giảm số lượng vòng phép trước
                    if (puppet.quantity >= requiredQuantity)
                    {
                        puppet.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu vòng phép không đủ, dùng cả vòng phép + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - puppet.quantity;
                        puppet.quantity = 0; // Dùng hết vòng phép

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Puppet newpuppet = new Puppet();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newpuppet = puppet.GetNewBreakthroughPower(puppet, increasePerUpgrade);
                    puppet.UpdatePuppetBreakthrough(newpuppet, puppet.star + 1, puppet.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(puppet.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp vòng phép!");
                }
            });
        }
        else if (obj is Alchemy alchemy)
        {
            PropertyInfo[] properties = alchemy.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(alchemy, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (alchemy.star + 1).ToString();
            }
            GameObject magicFormationObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage magicFormationImage = magicFormationObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = alchemy.image.Replace(".png", "");
            Texture magicFormationTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            magicFormationImage.texture = magicFormationTexture;

            TextMeshProUGUI magicFormationQuantity = magicFormationObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            magicFormationQuantity.text = alchemy.quantity.ToString() + "/" + (alchemy.star + 1).ToString();

            CreateStarUI(alchemy.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = alchemy.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng vòng phép
                bool hasEnoughMagicFormation = alchemy.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + alchemy.quantity >= requiredQuantity;

                if (hasEnoughMagicFormation || hasEnoughItems)
                {
                    // Giảm số lượng vòng phép trước
                    if (alchemy.quantity >= requiredQuantity)
                    {
                        alchemy.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu vòng phép không đủ, dùng cả vòng phép + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - alchemy.quantity;
                        alchemy.quantity = 0; // Dùng hết vòng phép

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Alchemy newalchemy = new Alchemy();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newalchemy = alchemy.GetNewBreakthroughPower(alchemy, increasePerUpgrade);
                    alchemy.UpdateAlchemyBreakthrough(newalchemy, alchemy.star + 1, alchemy.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(alchemy.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp vòng phép!");
                }
            });
        }
        else if (obj is Forge forge)
        {
            PropertyInfo[] properties = forge.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(forge, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (forge.star + 1).ToString();
            }
            GameObject magicFormationObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage magicFormationImage = magicFormationObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = forge.image.Replace(".png", "");
            Texture magicFormationTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            magicFormationImage.texture = magicFormationTexture;

            TextMeshProUGUI magicFormationQuantity = magicFormationObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            magicFormationQuantity.text = forge.quantity.ToString() + "/" + (forge.star + 1).ToString();

            CreateStarUI(forge.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = forge.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng vòng phép
                bool hasEnoughMagicFormation = forge.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + forge.quantity >= requiredQuantity;

                if (hasEnoughMagicFormation || hasEnoughItems)
                {
                    // Giảm số lượng vòng phép trước
                    if (forge.quantity >= requiredQuantity)
                    {
                        forge.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu vòng phép không đủ, dùng cả vòng phép + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - forge.quantity;
                        forge.quantity = 0; // Dùng hết vòng phép

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    Forge newforge = new Forge();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newforge = forge.GetNewBreakthroughPower(forge, increasePerUpgrade);
                    forge.UpdateForgeBreakthrough(newforge, forge.star + 1, forge.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(forge.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp vòng phép!");
                }
            });
        }
        else if (obj is CardLife cardLife)
        {
            PropertyInfo[] properties = cardLife.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardLife, null);
                CreatePropertyUpgradeUI(property, value);
            }
            Items item = new Items();
            List<Items> items = new List<Items>();
            items = item.GetItemForBreakthourgh(mainType);
            string fileNameWithoutExtension = "";
            foreach (Items items1 in items)
            {
                GameObject itemObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

                RawImage eImage = itemObject.transform.Find("MaterialImage").GetComponent<RawImage>();
                fileNameWithoutExtension = items1.image.Replace(".png", "");
                Texture itemTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
                eImage.texture = itemTexture;

                TextMeshProUGUI eQuantity = itemObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                eQuantity.text = items1.quantity.ToString() + "/" + (cardLife.star + 1).ToString();
            }
            GameObject magicFormationObject = Instantiate(ElementDetails2Prefab, UpgradeMaterialContent);

            RawImage magicFormationImage = magicFormationObject.transform.Find("MaterialImage").GetComponent<RawImage>();
            fileNameWithoutExtension = cardLife.image.Replace(".png", "");
            Texture magicFormationTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            magicFormationImage.texture = magicFormationTexture;

            TextMeshProUGUI magicFormationQuantity = magicFormationObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
            magicFormationQuantity.text = cardLife.quantity.ToString() + "/" + (cardLife.star + 1).ToString();

            CreateStarUI(cardLife.star);
            breakthroughButton.onClick.RemoveAllListeners();
            breakthroughButton.onClick.AddListener(() =>
            {
                int requiredQuantity = cardLife.star + 1;
                int totalItemQuantity = 0;

                // Kiểm tra số lượng vòng phép
                bool hasEnoughMagicFormation = cardLife.quantity >= requiredQuantity;

                // Kiểm tra tổng số lượng vật phẩm
                foreach (Items items1 in items)
                {
                    totalItemQuantity += items1.quantity;
                }
                bool hasEnoughItems = totalItemQuantity + cardLife.quantity >= requiredQuantity;

                if (hasEnoughMagicFormation || hasEnoughItems)
                {
                    // Giảm số lượng vòng phép trước
                    if (cardLife.quantity >= requiredQuantity)
                    {
                        cardLife.quantity -= requiredQuantity;
                    }
                    else
                    {
                        // Nếu vòng phép không đủ, dùng cả vòng phép + vật phẩm để bù vào
                        int remainingRequired = requiredQuantity - cardLife.quantity;
                        cardLife.quantity = 0; // Dùng hết vòng phép

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
                        items1.UpdateUserItemsQuantity(items1);
                    }
                    // Cập nhật cấp sao (Star)
                    CardLife newCardLife = new CardLife();
                    Teams teams = new Teams();
                    double currentPower = teams.GetTeamsPower(User.CurrentUserId);
                    newCardLife = cardLife.GetNewBreakthroughPower(cardLife, increasePerUpgrade);
                    cardLife.UpdateCardLifeBreakthrough(newCardLife, cardLife.star + 1, cardLife.quantity);
                    double newPower = teams.GetTeamsPower(User.CurrentUserId);
                    FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);

                    // Cập nhật giao diện
                    Close(UpgradeElementContent);
                    Close(UpgradeMaterialContent);
                    GetUpgrade(obj);
                    CreateStarUI(cardLife.star);
                }
                else
                {
                    Debug.Log("❌ Không đủ tài nguyên để nâng cấp vòng phép!");
                }
            });
        }
    }
    public void CreateStarUI(int star)
    {
        Transform currentStar = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/CurrentStar");
        Transform nextStar = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/NextStar");
        int currentimageIndex = (star == 0) ? 0 : ((star - 1) % 10) + 1;
        int currentstarIndex = (star == 0) ? 0 : (star - 1) / 10;
        int newStar = (star + 1 > 100000) ? 0 : star + 1;
        int nextimageIndex = (newStar == 0) ? 0 : ((newStar - 1) % 10) + 1;
        int nextstarIndex = (newStar == 0) ? 0 : (newStar - 1) / 10;
        for (int i = 0; i < currentimageIndex; i++)
        {
            GameObject starObject = Instantiate(StarPrefab, currentStar);

            RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
            GetStarImage(starImage, currentstarIndex);
        }
        for (int i = 0; i < nextimageIndex; i++)
        {
            GameObject starObject = Instantiate(StarPrefab, nextStar);

            RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
            GetStarImage(starImage, nextstarIndex);
        }
        GridLayoutGroup currentGridLayout = currentStar.GetComponent<GridLayoutGroup>();
        if (currentGridLayout != null)
        {
            currentGridLayout.cellSize = new Vector2(20, 20);
        }
        GridLayoutGroup nextGridLayout = nextStar.GetComponent<GridLayoutGroup>();
        if (nextGridLayout != null)
        {
            nextGridLayout.cellSize = new Vector2(20, 20);
        }
    }
    public void GetStarImage(RawImage starImage, int starIndex)
    {
        Texture starTexture = Resources.Load<Texture>($"UI/UI/Star1");
        switch (starIndex)
        {
            case 0:
                starTexture = Resources.Load<Texture>($"UI/UI/Star1");
                starImage.texture = starTexture;
                break;
            case 1:
                starTexture = Resources.Load<Texture>($"UI/UI/Star2");
                starImage.texture = starTexture;
                break;
            case 2:
                starTexture = Resources.Load<Texture>($"UI/UI/Star3");
                starImage.texture = starTexture;
                break;
            case 3:
                starTexture = Resources.Load<Texture>($"UI/UI/Star4");
                starImage.texture = starTexture;
                break;
            case 4:
                starTexture = Resources.Load<Texture>($"UI/UI/Star5");
                starImage.texture = starTexture;
                break;
            case 5:
                starTexture = Resources.Load<Texture>($"UI/UI/Star6");
                starImage.texture = starTexture;
                break;
            case 6:
                starTexture = Resources.Load<Texture>($"UI/UI/Star7");
                starImage.texture = starTexture;
                break;
            case 7:
                starTexture = Resources.Load<Texture>($"UI/UI/Star8");
                starImage.texture = starTexture;
                break;
            case 8:
                starTexture = Resources.Load<Texture>($"UI/UI/Star9");
                starImage.texture = starTexture;
                break;
            case 9:
                starTexture = Resources.Load<Texture>($"UI/UI/Star10");
                starImage.texture = starTexture;
                break;
            default:
                starTexture = Resources.Load<Texture>($"UI/UI/Star1");
                starImage.texture = starTexture;
                break;
        }
    }
    public void CreateLevelUI(int level)
    {
        TextMeshProUGUI currentLevelText = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/Level/CurrentLevelText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nextLevelText = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/Level/NextLevelText").GetComponent<TextMeshProUGUI>();
        currentLevelText.text = level.ToString();
        int nextLevel = level + 1;
        if ((int)level == 100000)
        {
            nextLevelText.text = "Max";
        }
        else
        {
            nextLevelText.text = nextLevel.ToString();
        }
    }
    public void OnButtonClicked(string buttonName)
    {
        // Tìm button hiện tại từ RightButtonContent
        Button button = RightButtonContent.Find(buttonName)?.GetComponent<Button>();
        if (button == null) return;

        // Gán giá trị clickedButton
        clickedButton = button;

        // Đổi background các button
        ChangeBackgroundButton();
    }
    public void ChangeBackgroundButton()
    {
        foreach (Transform child in RightButtonContent)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                ChangeButtonBackground(button.gameObject, "Background_V4_216");
            }
        }
        // Đổi background cho button được nhấn
        if (clickedButton != null)
        {
            ChangeButtonBackground(clickedButton.gameObject, "Background_V4_241"); // Background clicked
        }
    }
    private void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = Resources.Load<Texture>($"UI/Background4/{image}");
            if (texture != null)
            {
                buttonImage.texture = texture;
            }
            else
            {
                Debug.LogError($"Texture '{image}' not found in Resources.");
            }
        }
        else
        {
            Debug.LogError("Button does not have a RawImage component.");
        }
    }
    public void CreatePropertyUI(int status, PropertyInfo property, object value)
    {
        if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
                && !property.Name.Equals("status") && !property.Name.Equals("name")
                && !property.Name.Equals("image"))
        {
            if (property.Name.Equals("description"))
            {
                // Tạo đối tượng TextMeshProUGUI mới (TextMeshProUGUI cần được sử dụng thay vì Text)
                GameObject descriptionTextObject = new GameObject("DescriptionText");
                descriptionTextObject.transform.SetParent(descriptionPopupPanel, false); // Thêm vào panel với vị trí chính xác

                // Thêm component TextMeshProUGUI vào đối tượng mới
                TextMeshProUGUI descriptionText = descriptionTextObject.AddComponent<TextMeshProUGUI>();

                // Cấu hình các thuộc tính cơ bản cho TextMeshProUGUI
                descriptionText.text = value != null ? value.ToString() : "null"; // Gán nội dung mô tả vào text
                descriptionText.fontSize = 24; // Cài đặt kích thước font, có thể thay đổi theo nhu cầu
                descriptionText.alignment = TextAlignmentOptions.TopLeft; // Cài đặt căn chỉnh văn bản

                // Bạn có thể điều chỉnh thêm các thuộc tính như màu sắc, độ đậm, v.v.
                // Đổi màu chữ bằng mã hex #844000
                Color color;
                if (ColorUtility.TryParseHtmlString(descriptionColor, out color)) // Chuyển mã hex thành Color
                {
                    descriptionText.color = color; // Gán màu cho text
                }

                // Nếu bạn cần chỉnh sửa thêm chiều rộng của TextMeshProUGUI, có thể cần chỉnh sửa RectTransform của đối tượng
                RectTransform rectTransform = descriptionText.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(600, 100);
                rectTransform.anchoredPosition = new Vector2(20, 250); // Điều chỉnh kích thước nếu cần
                GridLayoutGroup gridLayout = descriptionPopupPanel.GetComponent<GridLayoutGroup>();
                if (gridLayout != null)
                {
                    gridLayout.cellSize = new Vector2(670, 800);
                }
            }
            else if (property.Name.Equals("power") || property.Name.Equals("rare") || property.Name.Equals("type")
            || property.Name.Equals("star") || property.Name.Equals("level") || property.Name.Equals("all_power"))
            {
                if (value != null)
                {
                    bool shouldDisplay = false;

                    if (value is int intValue)
                    {
                        shouldDisplay = intValue != -1;
                    }
                    else if (value is double doubleValue)
                    {
                        shouldDisplay = doubleValue != -1;
                    }
                    else if (value is string)
                    {
                        shouldDisplay = true;
                    }

                    if (shouldDisplay)
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, firstPopupPanel);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                            elementContentText.text = value.ToString();
                    }
                }

            }
            else if (property.Name.Equals("health") || property.Name.Equals("physical_attack") || property.Name.Equals("physical_defense") ||
                property.Name.Equals("magical_attack") || property.Name.Equals("magical_defense") ||
                property.Name.Equals("chemical_attack") || property.Name.Equals("chemical_defense") ||
                property.Name.Equals("atomic_attack") || property.Name.Equals("atomic_defense") ||
                property.Name.Equals("mental_attack") || property.Name.Equals("mental_defense") ||
                property.Name.Equals("all_health") || property.Name.Equals("all_physical_attack") || property.Name.Equals("all_physical_defense") ||
                property.Name.Equals("all_magical_attack") || property.Name.Equals("all_magical_defense") ||
                property.Name.Equals("all_chemical_attack") || property.Name.Equals("all_chemical_defense") ||
                property.Name.Equals("all_atomic_attack") || property.Name.Equals("all_atomic_defense") ||
                property.Name.Equals("all_mental_attack") || property.Name.Equals("all_mental_defense"))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
                    {
                        if (status == 1)
                        {
                            if (property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                    elementContentText.text = intValue.ToString();

                                RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                                CreatePropertyRuneUI(property.Name, runeImage);
                            }
                        }
                        else if (status == 0)
                        {
                            if (!property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                    elementContentText.text = intValue.ToString();

                                RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                                CreatePropertyRuneUI(property.Name, runeImage);
                            }
                        }
                    }
                }
            }
            else if (property.Name.Equals("speed") || property.Name.Equals("critical_damage_rate") || property.Name.Equals("critical_rate") ||
                property.Name.Equals("penetration_rate") || property.Name.Equals("evasion_rate") ||
                property.Name.Equals("damage_absorption_rate") || property.Name.Equals("vitality_regeneration_rate") ||
                property.Name.Equals("accuracy_rate") || property.Name.Equals("lifesteal_rate") ||
                property.Name.Equals("shield_strength") || property.Name.Equals("tenacity") ||
                property.Name.Equals("resistance_rate") || property.Name.Equals("combo_rate") ||
                property.Name.Equals("mana") || property.Name.Equals("mana_regeneration_rate") ||
                property.Name.Equals("reflection_rate") ||
                property.Name.Equals("damage_to_different_faction_rate") || property.Name.Equals("resistance_to_different_faction_rate") ||
                property.Name.Equals("damage_to_same_faction_rate") || property.Name.Equals("resistance_to_same_faction_rate") ||
                property.Name.Equals("all_speed") || property.Name.Equals("all_critical_damage_rate") || property.Name.Equals("all_critical_rate") ||
                property.Name.Equals("all_penetration_rate") || property.Name.Equals("all_evasion_rate") ||
                property.Name.Equals("all_damage_absorption_rate") || property.Name.Equals("all_vitality_regeneration_rate") ||
                property.Name.Equals("all_accuracy_rate") || property.Name.Equals("all_lifesteal_rate") ||
                property.Name.Equals("all_shield_strength") || property.Name.Equals("all_tenacity") ||
                property.Name.Equals("all_resistance_rate") || property.Name.Equals("all_combo_rate") ||
                property.Name.Equals("all_mana") || property.Name.Equals("all_mana_regeneration_rate") ||
                property.Name.Equals("all_reflection_rate") ||
                property.Name.Equals("all_damage_to_different_faction_rate") || property.Name.Equals("all_resistance_to_different_faction_rate") ||
                property.Name.Equals("all_damage_to_same_faction_rate") || property.Name.Equals("all_resistance_to_same_faction_rate"))
            {
                // Kiểm tra nếu value không phải null
                if (value != null)
                {
                    if (value is double intValue && intValue != -1)
                    {
                        if (status == 1)
                        {
                            if (property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, element2PopupPanel);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                    elementContentText.text = intValue.ToString();
                                RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                                CreatePropertyRuneUI(property.Name, runeImage);
                            }
                        }
                        else if (status == 0)
                        {
                            if (!property.Name.Contains("all"))
                            {
                                // Tạo một element mới từ prefab
                                GameObject elementObject = Instantiate(ElementDetailsPrefab, elementPopupPanel);

                                // Gán tên thuộc tính vào TitleText
                                TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                                if (elementTitleText != null)
                                    elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                                // Gán giá trị thuộc tính vào ContentText
                                TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                                if (elementContentText != null)
                                    elementContentText.text = intValue.ToString();

                                RawImage runeImage = elementObject.transform.Find("RuneImage").GetComponent<RawImage>();
                                CreatePropertyRuneUI(property.Name, runeImage);
                            }
                        }
                    }
                }
            }
        }
    }
    public void CreatePropertyRuneUI(string title, RawImage runeImage)
    {
        Texture runeTexture;
        if (title.Equals("physical_attack") || title.Equals("all_physical_attack"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Physical_Attack");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("physical_defense") || title.Equals("all_physical_defense"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Physical_Defense");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("magical_attack") || title.Equals("all_magical_attack"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Magic_Attack");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("magical_defense") || title.Equals("all_magical_defense"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Magic_Defense");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("chemical_attack") || title.Equals("all_chemical_attack"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Chemical_Attack");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("chemical_defense") || title.Equals("all_chemical_defense"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Chemical_Defense");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("atomic_attack") || title.Equals("all_atomic_attack"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Atomic_Attack");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("atomic_defense") || title.Equals("all_atomic_defense"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Atomic_Defense");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("mental_attack") || title.Equals("all_mental_attack"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Mental_Attack");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("mental_defense") || title.Equals("all_mental_defense"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Mental_Defense");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("health") || title.Equals("all_health"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Mental_1");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("speed") || title.Equals("critical_damage_rate") || title.Equals("critical_rate") ||
                title.Equals("penetration_rate") || title.Equals("evasion_rate") ||
                title.Equals("damage_absorption_rate") || title.Equals("vitality_regeneration_rate") ||
                title.Equals("all_speed") || title.Equals("all_critical_damage_rate") || title.Equals("all_critical_rate") ||
                title.Equals("all_penetration_rate") || title.Equals("all_evasion_rate") ||
                title.Equals("all_damage_absorption_rate") || title.Equals("all_vitality_regeneration_rate"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Atomic_1");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("accuracy_rate") || title.Equals("lifesteal_rate") ||
                title.Equals("shield_strength") || title.Equals("tenacity") ||
                title.Equals("resistance_rate") || title.Equals("combo_rate") ||
                title.Equals("mana") || title.Equals("mana_regeneration_rate") ||
                title.Equals("reflection_rate") ||
                title.Equals("all_accuracy_rate") || title.Equals("all_lifesteal_rate") ||
                title.Equals("all_shield_strength") || title.Equals("all_tenacity") ||
                title.Equals("all_resistance_rate") || title.Equals("all_combo_rate") ||
                title.Equals("all_mana") || title.Equals("all_mana_regeneration_rate") ||
                title.Equals("all_reflection_rate"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Chemical_1");
            runeImage.texture = runeTexture;
        }
        else if (title.Equals("damage_to_different_faction_rate") || title.Equals("resistance_to_different_faction_rate") ||
                title.Equals("damage_to_same_faction_rate") || title.Equals("resistance_to_same_faction_rate") ||
                title.Equals("all_damage_to_different_faction_rate") || title.Equals("all_resistance_to_different_faction_rate") ||
                title.Equals("all_damage_to_same_faction_rate") || title.Equals("all_resistance_to_same_faction_rate"))
        {
            runeTexture = Resources.Load<Texture>($"UI/Rune/Magic_1");
            runeImage.texture = runeTexture;
        }
        runeImage.gameObject.SetActive(true);
    }
    public void CreatePropertyLevelUI(PropertyInfo property, object value)
    {
        if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
                && !property.Name.Equals("status") && !property.Name.Equals("name")
                && !property.Name.Equals("image") && !property.Name.Equals("description") && !property.Name.Equals("power")
                && !property.Name.Equals("rare") && !property.Name.Equals("type")
                && !property.Name.Equals("star") && !property.Name.Equals("level"))
        {
            // Kiểm tra nếu value không phải null
            if (value != null)
            {
                if (value is double intValue && intValue != -1)
                {
                    if (!property.Name.Contains("all"))
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, LevelElementContent);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                        {
                            double newintValue = increasePerLevel * intValue;
                            elementContentText.text = "+" + newintValue.ToString();
                            Color greenColor;
                            if (ColorUtility.TryParseHtmlString("#32CD32", out greenColor)) // Màu xanh lá LimeGreen
                            {
                                elementContentText.color = greenColor;
                                elementContentText.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, Color.green); // Màu phát sáng
                                elementContentText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.5f); // Độ mạnh phát sáng (giảm giá trị
                            }
                        }
                    }
                }
            }
        }
        else if (property.Name.Equals("level"))
        {
            CreateLevelUI((int)value);
        }
    }
    public void CreatePropertyUpgradeUI(PropertyInfo property, object value)
    {
        if (!property.Name.Equals("id") && !property.Name.Equals("currency") && !property.Name.Equals("sequence")
                && !property.Name.Equals("experiment") && !property.Name.Equals("quantity") && !property.Name.Equals("block")
                && !property.Name.Equals("status") && !property.Name.Equals("name")
                && !property.Name.Equals("image") && !property.Name.Equals("description") && !property.Name.Equals("power")
                && !property.Name.Equals("rare") && !property.Name.Equals("type")
                && !property.Name.Equals("star") && !property.Name.Equals("level"))
        {
            // Kiểm tra nếu value không phải null
            if (value != null)
            {
                if (value is double intValue && intValue != -1)
                {
                    if (!property.Name.Contains("all"))
                    {
                        // Tạo một element mới từ prefab
                        GameObject elementObject = Instantiate(ElementDetailsPrefab, UpgradeElementContent);

                        // Gán tên thuộc tính vào TitleText
                        TextMeshProUGUI elementTitleText = elementObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
                        if (elementTitleText != null)
                            elementTitleText.text = StringConverter.SnakeCaseToTitleCase(property.Name.Replace("all_", ""));

                        // Gán giá trị thuộc tính vào ContentText
                        TextMeshProUGUI elementContentText = elementObject.transform.Find("ContentText").GetComponent<TextMeshProUGUI>();
                        if (elementContentText != null)
                        {
                            double newintValue = increasePerUpgrade * intValue;
                            elementContentText.text = "+" + newintValue.ToString();
                            Color greenColor;
                            if (ColorUtility.TryParseHtmlString("#32CD32", out greenColor)) // Màu xanh lá LimeGreen
                            {
                                elementContentText.color = greenColor;
                                elementContentText.fontMaterial.SetColor(ShaderUtilities.ID_GlowColor, Color.green); // Màu phát sáng
                                elementContentText.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.5f); // Độ mạnh phát sáng (giảm giá trị
                            }
                        }
                    }
                }
            }
        }
        else if (property.Name.Equals("star"))
        {
            TextMeshProUGUI currentLevelText = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/CurrentLevelText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nextLevelText = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/NextLevelText").GetComponent<TextMeshProUGUI>();
            currentLevelText.text = value.ToString();
            int nextLevel = (int)value + 1;
            if ((int)value == 100000)
            {
                nextLevelText.text = "Max";
            }
            else
            {
                nextLevelText.text = nextLevel.ToString();
            }
        }
    }
    public void CreateMaterialUI(List<Items> items)
    {
        foreach (Items items1 in items)
        {
            GameObject itemObject = Instantiate(ItemThird, LevelMaterialContent);

            RawImage eImage = itemObject.transform.Find("ItemImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = items1.image.Replace(".png", "");
            Texture equipmentTexture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            eImage.texture = equipmentTexture;

            TextMeshProUGUI eQuantity = itemObject.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
            eQuantity.text = items1.quantity.ToString();
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
                int expPerBottle = items1.GetItemExp(items1.name);

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
                items1.UpdateUserItemsQuantity(items1);
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
                int expPerBottle = items1.GetItemExp(items1.name);

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
                items1.UpdateUserItemsQuantity(items1);
            }
        }
        return status;
    }
    public bool BreakthroughCondition()
    {
        bool status = false;

        return status;
    }
    public void ChangeSizeImage(RawImage Image, Texture texture)
    {
        // Lấy tỷ lệ khung hình của ảnh gốc
        float aspectRatio = (float)texture.height / texture.width;

        // Chiều rộng cố định là 250
        float newWidth = 250f;
        float newHeight = newWidth * aspectRatio; // Tính chiều cao dựa theo tỷ lệ

        // Cập nhật kích thước ảnh
        Image.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
