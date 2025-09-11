using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MasterOfPhysicalManager : MonoBehaviour
{
    public static MasterOfPhysicalManager Instance { get; private set; }
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private GameObject MasterOfPhysicalPanelPrefab;
    private GameObject SlotPrefab;
    private GameObject buttonPrefab;
    private GameObject currentObject;
    private Button UpLevelButton;
    private Button UpMaxLevelButton;
    private Transform LevelCondition;
    private string mainType;
    private string parentType;
    private TMP_FontAsset EuroStyleNormalFont;
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
        MasterOfPhysicalPanelPrefab = UIManager.Instance.GetGameObjectMaster("MasterOfPhysicalPanelPrefab");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        SlotPrefab = UIManager.Instance.GetGameObjectMaster("MasterOfPhysicalSlotPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
    }

    public void CreateMasterOfPhysicalManager(object data)
    {
        currentObject = Instantiate(MasterOfPhysicalPanelPrefab, MainPanel);
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.Master.MasterOfPhysical);
        parentType = AppConstants.Master.MasterOfPhysical;
        UpLevelButton = currentObject.transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        UpMaxLevelButton = currentObject.transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() => ButtonEvent.Instance.Close(MainPanel));
        CloseButton.onClick.AddListener(() => Destroy(currentObject));

        LevelCondition = currentObject.transform.Find("DictionaryCards/LevelCondition");

        Dictionary<string, int> uniqueTypes = new Dictionary<string, int>();
        Features features = new Features();
        uniqueTypes = FeaturesService.Create().GetFeaturesByType(AppConstants.Master.MasterOfPhysical);
        if (uniqueTypes.Count > 0)
        {
            int index = 0;
            foreach (var kvp in uniqueTypes)
            {
                // Tạo một nút mới từ prefab
                string subtype = kvp.Key;
                int value = kvp.Value;
                GameObject button = Instantiate(buttonPrefab, TabButtonPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnButtonClick(button, data, subtype, value));

                if (index == 0)
                {
                    mainType = subtype;
                    UIManager.Instance.ChangeButtonBackground(button, "Background_V4_166");
                    if (data is CardHeroes cardHeroes)
                    {
                        // mainId = cardHeroes.id;
                        DetailMasterManager.Instance.CreateCardHeroesEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardHeroes);
                        if (cardHeroes.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    else if (data is Books books)
                    {
                        // mainId = books.id;
                        DetailMasterManager.Instance.CreateBooksEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, books);
                        if (books.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    else if (data is CardCaptains cardCaptains)
                    {
                        // mainId = cardCaptains.id;
                        DetailMasterManager.Instance.CreateCardCaptainsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardCaptains);
                        if (cardCaptains.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    else if (data is Pets pets)
                    {
                        // mainId = pets.id;
                        DetailMasterManager.Instance.CreatePetsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, pets);
                        if (pets.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    else if (data is CardMilitary cardMilitary)
                    {
                        // mainId = cardMilitary.id;
                        DetailMasterManager.Instance.CreateCardMilitaryEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardMilitary);
                        if (cardMilitary.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    else if (data is CardSpell cardSpell)
                    {
                        // mainId = cardSpell.id;
                        DetailMasterManager.Instance.CreateCardSpellEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardSpell);
                        if (cardSpell.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    else if (data is CardMonsters cardMonsters)
                    {
                        // mainId = cardMonsters.id;
                        DetailMasterManager.Instance.CreateCardMonstersEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardMonsters);
                        if (cardMonsters.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    else if (data is CardColonels cardColonels)
                    {
                        // mainId = cardColonels.id;
                        DetailMasterManager.Instance.CreateCardColonelsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardColonels);
                        if (cardColonels.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    else if (data is CardGenerals cardGenerals)
                    {
                        // mainId = cardGenerals.id;
                        DetailMasterManager.Instance.CreateCardGeneralsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardGenerals);
                        if (cardGenerals.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    else if (data is CardAdmirals cardAdmirals)
                    {
                        // mainId = cardAdmirals.id;
                        DetailMasterManager.Instance.CreateCardAdmiralsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardAdmirals);
                        if (cardAdmirals.level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    // else if (data is Equipments equipments)
                    // {
                    //     // mainId = cardAdmirals.id;
                    //     DetailMasterManager.Instance.CreateEquipmentsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, equipments);
                    //     if (equipments.level >= value)
                    //     {
                    //         LevelCondition.gameObject.SetActive(false);
                    //     }
                    //     else
                    //     {
                    //         CreateWarningLevelCondition(value);
                    //     }
                    // }
                }
                else
                {
                    UIManager.Instance.ChangeButtonBackground(button, "Background_V4_167");
                }
                ButtonEvent.Instance.CheckLockedButton(data, value, button);
                index = index + 1;
            }
            LoadAnimation();
        }
    }
    void OnButtonClick(GameObject clickedButton, object data, string type, int value)
    {
        foreach (Transform child in TabButtonPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                UIManager.Instance.ChangeButtonBackground(button.gameObject, "Background_V4_167"); // Giả sử bạn có texture trắng
            }
        }

        mainType = type;
        UIManager.Instance.ChangeButtonBackground(clickedButton, "Background_V4_166");

        if (data is CardHeroes cardHeroes)
        {
            // mainId = cardHeroes.id;
            DetailMasterManager.Instance.CreateCardHeroesEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardHeroes);
            if (cardHeroes.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        else if (data is Books books)
        {
            // mainId = books.id;
            DetailMasterManager.Instance.CreateBooksEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, books);
            if (books.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        else if (data is CardCaptains cardCaptains)
        {
            // mainId = cardCaptains.id;
            DetailMasterManager.Instance.CreateCardCaptainsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardCaptains);
            if (cardCaptains.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        else if (data is Pets pets)
        {
            // mainId = pets.id;
            DetailMasterManager.Instance.CreatePetsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, pets);
            if (pets.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        else if (data is CardMilitary cardMilitary)
        {
            // mainId = cardMilitary.id;
            DetailMasterManager.Instance.CreateCardMilitaryEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardMilitary);
            if (cardMilitary.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        else if (data is CardSpell cardSpell)
        {
            // mainId = cardSpell.id;
            DetailMasterManager.Instance.CreateCardSpellEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardSpell);
            if (cardSpell.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        else if (data is CardMonsters cardMonsters)
        {
            // mainId = cardMonsters.id;
            DetailMasterManager.Instance.CreateCardMonstersEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardMonsters);
            if (cardMonsters.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        else if (data is CardColonels cardColonels)
        {
            // mainId = cardColonels.id;
            DetailMasterManager.Instance.CreateCardColonelsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardColonels);
            if (cardColonels.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        else if (data is CardGenerals cardGenerals)
        {
            // mainId = cardGenerals.id;
            DetailMasterManager.Instance.CreateCardGeneralsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardGenerals);
            if (cardGenerals.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            // mainId = cardAdmirals.id;
            DetailMasterManager.Instance.CreateCardAdmiralsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardAdmirals);
            if (cardAdmirals.level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        // else if (data is Equipments equipments)
        // {
        //     // mainId = cardAdmirals.id;
        //     DetailMasterManager.Instance.CreateEquipmentsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, equipments);
        //     if (equipments.level >= value)
        //     {
        //         LevelCondition.gameObject.SetActive(false);
        //     }
        //     else
        //     {
        //         CreateWarningLevelCondition(value);
        //     }
        // }
    }
    public void CreateWarningLevelCondition(int value)
    {
        LevelCondition.gameObject.SetActive(true);
        TextMeshProUGUI warningText = LevelCondition.Find("WarningText").GetComponent<TextMeshProUGUI>();
        warningText.font = EuroStyleNormalFont;
        warningText.fontSize = 50;
        warningText.fontStyle = FontStyles.Bold; 
        warningText.text = MessageHelper.WaringLevel(value);
    }
    public void LoadAnimation()
    {
        TabButtonPanel.gameObject.AddComponent<SlideLeftToRightAnimation>();
    }
}
