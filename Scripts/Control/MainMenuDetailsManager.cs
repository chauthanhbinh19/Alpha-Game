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
    private string mainType;
    private string descriptionColor = "#844000";
    private double increasePerLevel = 0.1;
    private double increasePerUpgrade = 1.1;
    // Start is called before the first frame update
    void Start()
    {
        MainMenuDetailPanel2Prefab = UIManager.Instance.GetGameObject("MainMenuDetailPanel2Prefab");
        TabButton4 = UIManager.Instance.GetGameObject("TabButton4");
        ElementDetailsPrefab = UIManager.Instance.GetGameObject("ElementDetailsPrefab");
        NumberDetailPrefab = UIManager.Instance.GetGameObject("NumberDetailPrefab");
        NumberDetail2Prefab = UIManager.Instance.GetGameObject("NumberDetail2Prefab");
        ItemThird = UIManager.Instance.GetGameObject("ItemThird");
        StarPrefab = UIManager.Instance.GetGameObject("StarPrefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");


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
        HomeButton.onClick.AddListener(() => Close(MainPanel));
        // Kiểm tra kiểu của data và ép kiểu phù hợp
        if (data is CardHeroes card)
        {
            // Xử lý đối tượng Card
            mainType = "CardHeroes";
            ShowCardHeroesDetails(card);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("CardHeroes");
            });
        }
        else if (data is Books book)
        {
            // Xử lý đối tượng Book
            ShowBooksDetails(book);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("Books");
            });
        }
        else if (data is CardCaptains captain)
        {
            // Xử lý đối tượng Captain
            ShowCardCaptainsDetails(captain);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("CardCaptains");
            });
        }
        else if (data is Pets pet)
        {
            // Xử lý đối tượng Pet
            ShowPetsDetails(pet);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("Pets");
            });
        }
        else if (data is CollaborationEquipment collaborationEquipmentsequipment)
        {
            // Xử lý đối tượng CollaborationEquipment
            ShowCollaborationEquipmentsDetails(collaborationEquipmentsequipment);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("CollaborationEquipments");
            });
        }
        else if (data is CardMilitary military)
        {
            // Xử lý đối tượng Military
            ShowCardMilitaryDetails(military);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("CardMilitary");
            });
        }
        else if (data is CardSpell spell)
        {
            // Xử lý đối tượng Spell
            ShowCardSpellDetails(spell);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("CardSpell");
            });
        }
        else if (data is Collaboration collaboration)
        {
            // Xử lý đối tượng Collaboration
            ShowCollaborationsDetails(collaboration);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("Collaborations");
            });
        }
        else if (data is CardMonsters monster)
        {
            // Xử lý đối tượng Monster
            ShowCardMonstersDetails(monster);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("CardMonsters");
            });
        }
        else if (data is Equipments equipment)
        {
            // Xử lý đối tượng Equipment
            ShowEquipmentsDetails(equipment);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("Equipments");
            });
        }
        else if (data is Medals medal)
        {
            // Xử lý đối tượng Medal
            ShowMedalsDetails(medal);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("Medals");
            });
        }
        else if (data is Skills skill)
        {
            // Xử lý đối tượng Skill
            ShowSkillsDetails(skill);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("Skills");
            });
        }
        else if (data is Symbols symbol)
        {
            // Xử lý đối tượng Symbol
            ShowSymbolsDetails(symbol);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("Symbols");
            });
        }
        else if (data is Titles title)
        {
            // Xử lý đối tượng Title
            ShowTitlesDetails(title);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("Titles");
            });
        }
        else if (data is MagicFormationCircle magicFormationCircle)
        {
            // Xử lý đối tượng Title
            ShowMagicFormationCircleDetails(magicFormationCircle);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("MagicFormationCircle");
            });
        }
        else if (data is Relics relics)
        {
            // Xử lý đối tượng Title
            ShowRelicsDetails(relics);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("Relics");
            });
        }
        else if (data is CardColonels colonels)
        {
            // Xử lý đối tượng colonels
            ShowCardColonelsDetails(colonels);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("CardColonels");
            });
        }
        else if (data is CardGenerals generals)
        {
            // Xử lý đối tượng Generals
            ShowCardGeneralsDetails(generals);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("CardGenerals");
            });
        }
        else if (data is CardAdmirals admirals)
        {
            // Xử lý đối tượng admirals
            ShowCardAdmiralsDetails(admirals);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("CardAdmirals");
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
            ShowAchievementsDetails(achievements);
            CloseButton.onClick.AddListener(() =>
            {
                Close(MainPanel);
                FindAnyObjectByType<MainMenuManager>().GetType("Achievements");
            });
        }
        else
        {
            Debug.LogError("Không hỗ trợ loại dữ liệu này!");
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
        if (obj is CardHeroes cardHeroes)
        {
            GameObject firstDetailsObject = Instantiate(NumberDetail2Prefab, DetailsContent);
            GameObject elementDetailsObject = Instantiate(NumberDetailPrefab, DetailsContent);
            GameObject descriptionDetailsObject = Instantiate(NumberDetailPrefab, DetailsContent);
            Transform firstPopupPanel = firstDetailsObject.transform.Find("ElementDetails");
            Transform elementPopupPanel = elementDetailsObject.transform.Find("ElementDetails");
            Transform descriptionPopupPanel = descriptionDetailsObject.transform.Find("ElementDetails");

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

        Button up1LevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpOneLevelButton").GetComponent<Button>();
        Button upMaxLevelButton = currentObject.transform.Find("DictionaryCards/Content/LevelPanel/UpTenLevelButton").GetComponent<Button>();
        if (obj is CardHeroes cardHeroes)
        {
            PropertyInfo[] properties = cardHeroes.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardHeroes, null);
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
            Items item = new Items();
            List<Items> items = new List<Items>();
            items.Add(item.GetUserItemByName("Exp Bottle lv1"));
            items.Add(item.GetUserItemByName("Exp Bottle lv2"));
            items.Add(item.GetUserItemByName("Exp Bottle lv3"));
            items.Add(item.GetUserItemByName("Exp Bottle lv4"));
            items.Add(item.GetUserItemByName("Exp Bottle lv5"));
            items.Add(item.GetUserItemByName("Exp Bottle lv6"));
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

            up1LevelButton.onClick.AddListener(() =>
            {
                CardHeroes currentCard = new CardHeroes();
                currentCard = cardHeroes.GetUserCardHeroesById(cardHeroes.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int experimentCondition = currentLevel * 100;
                if (currentLevel == 0)
                {
                    experimentCondition = 1 * 100;
                }
                foreach (Items items1 in items)
                {
                    int expPerBottle = 0;

                    expPerBottle = items1.GetItemExp(items1.name);
                    // Nếu là loại Exp Bottle và có expPerBottle > 0
                    if (expPerBottle > 0)
                    {
                        int totalExpFromThisItem = expPerBottle * items1.quantity; // Tổng EXP từ item này
                        int requiredExp = experimentCondition - totalExperiment; // Số EXP cần thêm

                        if (requiredExp <= totalExpFromThisItem)
                        {
                            // Nếu EXP từ item này đủ để đạt điều kiện, chỉ cộng số cần thiết
                            totalExperiment += requiredExp;
                            items1.quantity -= (int)Math.Ceiling((double)requiredExp / expPerBottle); // Giảm số lượng đã dùng
                            break; // Thoát vòng lặp vì đã đạt điều kiện
                        }
                        else
                        {
                            // Nếu không đủ, cộng toàn bộ EXP từ item này và tiếp tục vòng lặp
                            totalExperiment += totalExpFromThisItem;
                            items1.quantity = 0; // Sử dụng hết số lượng
                        }
                    }
                }
                foreach (Items items1 in items)
                {
                    item.UpdateUserItemsQuantity(items1);
                }
                CardHeroes newCard = new CardHeroes();
                newCard = cardHeroes.GetNewPower(cardHeroes, 0.1);
                cardHeroes.UpdateCardHeroesLevel(newCard, currentLevel);
                cardHeroes.UpdateFactCardHeroes(newCard);
                totalExperiment -= experimentCondition;
                currentLevel = currentLevel + 1;
                experimentCondition = currentLevel * 100;
                Close(LevelElementContent);
                Close(LevelMaterialContent);
                GetLevel(obj);
                CreateLevelUI(currentLevel);
            });
            upMaxLevelButton.onClick.AddListener(() =>
            {
                CardHeroes currentCard = cardHeroes.GetUserCardHeroesById(cardHeroes.id);
                int totalExperiment = currentCard.experiment;
                int currentLevel = currentCard.level;
                int maxLevel = 100000;
                int experimentCondition = currentLevel * 100;

                // Đảm bảo level 0 có điều kiện đặc biệt
                if (currentLevel == 0)
                {
                    experimentCondition = 1 * 100;
                }

                foreach (Items items1 in items)
                {
                    int expPerBottle = items1.GetItemExp(items1.name);
                    if (expPerBottle > 0)
                    {
                        while (items1.quantity > 0 && currentLevel < maxLevel && currentLevel < User.CurrentUserLevel && totalExperiment >= experimentCondition)
                        {
                            int requiredExp = experimentCondition - totalExperiment;

                            if (requiredExp <= expPerBottle)
                            {
                                // Nếu vật phẩm này đủ nâng cấp
                                totalExperiment += requiredExp;
                                items1.quantity -= 1; // Giảm 1 vật phẩm
                                totalExperiment -= experimentCondition;

                                currentLevel++; // Tăng level
                                experimentCondition = currentLevel * 100;
                            }
                            else
                            {
                                // Nếu không đủ nâng cấp
                                totalExperiment += expPerBottle;
                                items1.quantity -= 1;
                            }
                        }

                        // Nếu đạt max level hoặc hết vật phẩm, thoát vòng lặp
                        if (currentLevel >= maxLevel || currentLevel >= User.CurrentUserLevel)
                        {
                            break;
                        }
                    }
                }

                // Cập nhật vật phẩm sau khi sử dụng
                foreach (Items items1 in items)
                {
                    item.UpdateUserItemsQuantity(items1);
                }

                // Tạo thẻ mới và cập nhật thông số
                CardHeroes newCard = cardHeroes.GetNewPower(cardHeroes, 0.1);
                cardHeroes.UpdateCardHeroesLevel(newCard, currentLevel);
                cardHeroes.UpdateFactCardHeroes(newCard);

                // Đóng các UI cũ và cập nhật UI mới
                Close(LevelElementContent);
                Close(LevelMaterialContent);
                GetLevel(obj);
                CreateLevelUI(currentLevel);
            });

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
        if (obj is CardHeroes cardHeroes)
        {
            PropertyInfo[] properties = cardHeroes.GetType().GetProperties();
            foreach (var property in properties)
            {
                // Lấy giá trị của thuộc tính
                object value = property.GetValue(cardHeroes, null);
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
            Items item = new Items();
            List<Items> items = new List<Items>();
            items.Add(item.GetUserItemByName("Breakthrough Token"));
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

            int imageIndex = ((cardHeroes.star + 1) % 5);
            int starIndex = ((cardHeroes.star - 1) % 7);
            if (cardHeroes.star == 0)
            {
                starIndex = 0;
            }
            Transform currentStar = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/CurrentStar");
            Transform nextStar = currentObject.transform.Find("DictionaryCards/Content/UpgradePanel/Level/NextStar");
            for (int i = 0; i < imageIndex - 1; i++)
            {
                GameObject starObject = Instantiate(StarPrefab, currentStar);

                RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
                GetStarImage(starImage, starIndex);
            }
            for (int i = 0; i < imageIndex; i++)
            {
                GameObject starObject = Instantiate(StarPrefab, nextStar);

                RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
                GetStarImage(starImage, starIndex);
            }
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
}
