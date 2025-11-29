using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuCyberManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private GameObject MainMenuCyberPanelPrefab;
    private GameObject SlotPrefab;
    private GameObject buttonPrefab;
    private GameObject currentObject;
    private Button UpLevelButton;
    private Button UpMaxLevelButton;
    private Transform LevelCondition;
    private string mainType;
    private string parentType;
    private TMP_FontAsset EuroStyleNormalFont;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MainMenuCyberPanelPrefab = UIMainMenuSet1Manager.Instance.GetGameObjectMainMenu1("MainMenuCyberPanelPrefab");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        SlotPrefab = UIMainMenuSet1Manager.Instance.GetGameObjectMainMenu1("CyberSlotPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
    }
    public void CreateMainMenuCyberManager(object data)
    {
        currentObject = Instantiate(MainMenuCyberPanelPrefab, MainPanel);
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainMenuSet1.CYBER);
        parentType = AppConstants.MainMenuSet1.CYBER;
        UpLevelButton = currentObject.transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        UpMaxLevelButton = currentObject.transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
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

        LevelCondition = currentObject.transform.Find("DictionaryCards/LevelCondition");

        Dictionary<string, int> uniqueTypes = new Dictionary<string, int>();
        Features features = new Features();
        uniqueTypes = FeaturesService.Create().GetFeaturesByType(AppConstants.MainMenuSet1.CYBER);
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
                btn.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    OnButtonClick(button, data, subtype, value);
                });

                if (index == 0)
                {
                    mainType = subtype;
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    if (data is CardHeroes cardHeroes)
                    {
                        // mainId = cardHeroes.id;
                        DetailMenuManager.Instance.CreateCardHeroesEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardHeroes);
                        if (cardHeroes.Level >= value)
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
                        DetailMenuManager.Instance.CreateBooksEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, books);
                        if (books.Level >= value)
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
                        DetailMenuManager.Instance.CreateCardCaptainsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardCaptains);
                        if (cardCaptains.Level >= value)
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
                        DetailMenuManager.Instance.CreatePetsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, pets);
                        if (pets.Level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    else if (data is CardMilitaries cardMilitary)
                    {
                        // mainId = cardMilitary.id;
                        DetailMenuManager.Instance.CreateCardMilitaryEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardMilitary);
                        if (cardMilitary.Level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    else if (data is CardSpells cardSpell)
                    {
                        // mainId = cardSpell.id;
                        DetailMenuManager.Instance.CreateCardSpellEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardSpell);
                        if (cardSpell.Level >= value)
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
                        DetailMenuManager.Instance.CreateCardMonstersEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardMonsters);
                        if (cardMonsters.Level >= value)
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
                        DetailMenuManager.Instance.CreateCardColonelsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardColonels);
                        if (cardColonels.Level >= value)
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
                        DetailMenuManager.Instance.CreateCardGeneralsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardGenerals);
                        if (cardGenerals.Level >= value)
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
                        DetailMenuManager.Instance.CreateCardAdmiralsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardAdmirals);
                        if (cardAdmirals.Level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                    else if (data is Equipments equipments)
                    {
                        // mainId = cardAdmirals.id;
                        DetailMenuManager.Instance.CreateEquipmentsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, equipments);
                        if (equipments.Level >= value)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(value);
                        }
                    }
                }
                else
                {
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
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
                UIManager.Instance.ChangeButtonBackground(button.gameObject, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL); // Giả sử bạn có texture trắng
            }
        }

        mainType = type;
        UIManager.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);

        if (data is CardHeroes cardHeroes)
        {
            // mainId = cardHeroes.id;
            DetailMenuManager.Instance.CreateCardHeroesEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardHeroes);
            if (cardHeroes.Level >= value)
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
            DetailMenuManager.Instance.CreateBooksEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, books);
            if (books.Level >= value)
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
            DetailMenuManager.Instance.CreateCardCaptainsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardCaptains);
            if (cardCaptains.Level >= value)
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
            DetailMenuManager.Instance.CreatePetsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, pets);
            if (pets.Level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        else if (data is CardMilitaries cardMilitary)
        {
            // mainId = cardMilitary.id;
            DetailMenuManager.Instance.CreateCardMilitaryEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardMilitary);
            if (cardMilitary.Level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        else if (data is CardSpells cardSpell)
        {
            // mainId = cardSpell.id;
            DetailMenuManager.Instance.CreateCardSpellEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardSpell);
            if (cardSpell.Level >= value)
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
            DetailMenuManager.Instance.CreateCardMonstersEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardMonsters);
            if (cardMonsters.Level >= value)
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
            DetailMenuManager.Instance.CreateCardColonelsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardColonels);
            if (cardColonels.Level >= value)
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
            DetailMenuManager.Instance.CreateCardGeneralsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardGenerals);
            if (cardGenerals.Level >= value)
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
            DetailMenuManager.Instance.CreateCardAdmiralsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardAdmirals);
            if (cardAdmirals.Level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
        else if (data is Equipments equipments)
        {
            // mainId = cardAdmirals.id;
            DetailMenuManager.Instance.CreateEquipmentsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, equipments);
            if (equipments.Level >= value)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(value);
            }
        }
    }
    public void CreateWarningLevelCondition(int value)
    {
        LevelCondition.gameObject.SetActive(true);
        TextMeshProUGUI warningText = LevelCondition.Find("WarningText").GetComponent<TextMeshProUGUI>();
        warningText.font = EuroStyleNormalFont;
        warningText.fontSize = 50;
        warningText.fontStyle = FontStyles.Bold; 
        warningText.text = MessageHelper.WaringLevel(value);
        LevelCondition.gameObject.AddComponent<SlideBottomToTopAnimation>();
    }
    public void LoadAnimation()
    {
        TabButtonPanel.gameObject.AddComponent<SlideLeftToRightAnimation>();
    }
}
