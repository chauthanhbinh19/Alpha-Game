using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MainMenuOmnitheusManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private GameObject MainMenuOmnitheusPanelPrefab;
    private GameObject SlotPrefab;
    private GameObject TypeButtonPrefab;
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
        MainMenuOmnitheusPanelPrefab = UIManager.Instance.Get("MainMenuOmnitheusPanelPrefab");
        TypeButtonPrefab = UIManager.Instance.Get("TypeButtonPrefab");
        SlotPrefab = UIManager.Instance.Get("OmnitheusSlotPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
    }
    public async Task CreateMainMenuOmnitheusManagerAsync(object data)
    {
        currentObject = Instantiate(MainMenuOmnitheusPanelPrefab, MainPanel);
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainMenuSet3.OMNITHEUS);
        parentType = AppConstants.MainMenuSet3.OMNITHEUS;
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
        uniqueTypes = await FeaturesService.Create().GetFeaturesByTypeAsync(AppConstants.MainMenuSet3.OMNITHEUS);
        if (uniqueTypes.Count > 0)
        {
            int index = 0;
            foreach (var kvp in uniqueTypes)
            {
                // Tạo một nút mới từ prefab
                string subtype = kvp.Key;
                int value = kvp.Value;
                GameObject button = Instantiate(TypeButtonPrefab, TabButtonPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await OnButtonClickAsync(button, data, subtype, value);
                });

                if (index == 0)
                {
                    mainType = subtype;
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    if (data is CardHeroes cardHeroes)
                    {
                        // mainId = cardHeroes.id;
                        await DetailMenuManager.Instance.CreateCardHeroesEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardHeroes);
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
                        await DetailMenuManager.Instance.CreateBooksEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, books);
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
                        await DetailMenuManager.Instance.CreateCardCaptainsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardCaptains);
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
                        await DetailMenuManager.Instance.CreatePetsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, pets);
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
                        await DetailMenuManager.Instance.CreateCardMilitaryEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardMilitary);
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
                        await DetailMenuManager.Instance.CreateCardSpellEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardSpell);
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
                        await DetailMenuManager.Instance.CreateCardMonstersEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardMonsters);
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
                        await DetailMenuManager.Instance.CreateCardColonelsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardColonels);
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
                        await DetailMenuManager.Instance.CreateCardGeneralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardGenerals);
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
                        await DetailMenuManager.Instance.CreateCardAdmiralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardAdmirals);
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
                        await DetailMenuManager.Instance.CreateEquipmentsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, equipments);
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
    async Task OnButtonClickAsync(GameObject clickedButton, object data, string type, int value)
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
            await DetailMenuManager.Instance.CreateCardHeroesEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardHeroes);
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
            await DetailMenuManager.Instance.CreateBooksEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, books);
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
            await DetailMenuManager.Instance.CreateCardCaptainsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardCaptains);
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
            await DetailMenuManager.Instance.CreatePetsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, pets);
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
            await DetailMenuManager.Instance.CreateCardMilitaryEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardMilitary);
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
            await DetailMenuManager.Instance.CreateCardSpellEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardSpell);
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
            await DetailMenuManager.Instance.CreateCardMonstersEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardMonsters);
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
            await DetailMenuManager.Instance.CreateCardColonelsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardColonels);
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
            await DetailMenuManager.Instance.CreateCardGeneralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardGenerals);
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
            await DetailMenuManager.Instance.CreateCardAdmiralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, cardAdmirals);
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
            await DetailMenuManager.Instance.CreateEquipmentsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, parentType, equipments);
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
