using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MasterOfScienceManager : MonoBehaviour
{
    public static MasterOfScienceManager Instance { get; private set; }
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private GameObject MasterOfSciencePanelPrefab;
    private GameObject SlotPrefab;
    private GameObject TypeButtonPrefab;
    private GameObject currentObject;
    private Button UpLevelButton;
    private Button UpMaxLevelButton;
    private Transform LevelCondition;
    private Features feature;
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
        Initialize();
    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MasterOfSciencePanelPrefab = UIManager.Instance.Get("MasterOfSciencePanelPrefab");
        TypeButtonPrefab = UIManager.Instance.Get("TypeButtonPrefab");
        SlotPrefab = UIManager.Instance.Get("MasterOfScienceSlotPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
    }
    public async Task CreateMasterOfScienceManagerAsync(object data)
    {
        currentObject = Instantiate(MasterOfSciencePanelPrefab, MainPanel);
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.Master.MASTER_OF_SCIENCE);
        parentType = AppConstants.Master.MASTER_OF_SCIENCE;
        UpLevelButton = currentObject.transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        UpMaxLevelButton = currentObject.transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
            await HomeManager.Instance.CreateHomePanelAsync();
        });
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });

        LevelCondition = currentObject.transform.Find("DictionaryCards/LevelCondition");

        Dictionary<string, Features> uniqueTypes = new Dictionary<string, Features>();
        uniqueTypes = await FeaturesService.Create().GetFeaturesByTypeAsync(AppConstants.Master.MASTER_OF_SCIENCE);
        if (uniqueTypes.Count > 0)
        {
            int index = 0;
            foreach (var kvp in uniqueTypes)
            {
                // Tạo một nút mới từ prefab
                string subtype = kvp.Key;
                int requiredLevel = kvp.Value.RequiredLevel;
                GameObject button = Instantiate(TypeButtonPrefab, TabButtonPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await OnButtonClickAsync(button, data, kvp.Value, requiredLevel);
                });

                if (index == 0)
                {
                    feature = kvp.Value;
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    if (data is CardHeroes cardHeroes)
                    {
                        // mainId = cardHeroes.id;
                        await DetailMasterManager.Instance.CreateCardHeroesEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardHeroes);
                        if (cardHeroes.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is Books books)
                    {
                        // mainId = books.id;
                        await DetailMasterManager.Instance.CreateBooksEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, books);
                        if (books.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardCaptains cardCaptains)
                    {
                        // mainId = cardCaptains.id;
                        await DetailMasterManager.Instance.CreateCardCaptainsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardCaptains);
                        if (cardCaptains.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is Pets pets)
                    {
                        // mainId = pets.id;
                        await DetailMasterManager.Instance.CreatePetsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, pets);
                        if (pets.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardMilitaries cardMilitary)
                    {
                        // mainId = cardMilitary.id;
                        await DetailMasterManager.Instance.CreateCardMilitaryEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardMilitary);
                        if (cardMilitary.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardSpells cardSpell)
                    {
                        // mainId = cardSpell.id;
                        await DetailMasterManager.Instance.CreateCardSpellEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardSpell);
                        if (cardSpell.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardMonsters cardMonsters)
                    {
                        // mainId = cardMonsters.id;
                        await DetailMasterManager.Instance.CreateCardMonstersEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardMonsters);
                        if (cardMonsters.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardColonels cardColonels)
                    {
                        // mainId = cardColonels.id;
                        await DetailMasterManager.Instance.CreateCardColonelsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardColonels);
                        if (cardColonels.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardGenerals cardGenerals)
                    {
                        // mainId = cardGenerals.id;
                        await DetailMasterManager.Instance.CreateCardGeneralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardGenerals);
                        if (cardGenerals.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardAdmirals cardAdmirals)
                    {
                        // mainId = cardAdmirals.id;
                        await DetailMasterManager.Instance.CreateCardAdmiralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardAdmirals);
                        if (cardAdmirals.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    // else if (data is Equipments equipments)
                    // {
                    //     // mainId = cardAdmirals.id;
                    //     await DetailMasterManager.Instance.CreateEquipmentsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, equipments);
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
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                }
                ButtonEvent.Instance.CheckLockedButton(data, requiredLevel, button);
                index = index + 1;
            }
            LoadAnimation();
        }
    }
    async Task OnButtonClickAsync(GameObject clickedButton, object data, Features subFeature, int requiredLevel)
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

        feature = subFeature;
        UIManager.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);

        if (data is CardHeroes cardHeroes)
        {
            // mainId = cardHeroes.id;
            await DetailMasterManager.Instance.CreateCardHeroesEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardHeroes);
            if (cardHeroes.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is Books books)
        {
            // mainId = books.id;
            await DetailMasterManager.Instance.CreateBooksEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, books);
            if (books.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardCaptains cardCaptains)
        {
            // mainId = cardCaptains.id;
            await DetailMasterManager.Instance.CreateCardCaptainsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardCaptains);
            if (cardCaptains.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is Pets pets)
        {
            // mainId = pets.id;
            await DetailMasterManager.Instance.CreatePetsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, pets);
            if (pets.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardMilitaries cardMilitary)
        {
            // mainId = cardMilitary.id;
            await DetailMasterManager.Instance.CreateCardMilitaryEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardMilitary);
            if (cardMilitary.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardSpells cardSpell)
        {
            // mainId = cardSpell.id;
            await DetailMasterManager.Instance.CreateCardSpellEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardSpell);
            if (cardSpell.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardMonsters cardMonsters)
        {
            // mainId = cardMonsters.id;
            await DetailMasterManager.Instance.CreateCardMonstersEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardMonsters);
            if (cardMonsters.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardColonels cardColonels)
        {
            // mainId = cardColonels.id;
            await DetailMasterManager.Instance.CreateCardColonelsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardColonels);
            if (cardColonels.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardGenerals cardGenerals)
        {
            // mainId = cardGenerals.id;
            await DetailMasterManager.Instance.CreateCardGeneralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardGenerals);
            if (cardGenerals.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            // mainId = cardAdmirals.id;
            await DetailMasterManager.Instance.CreateCardAdmiralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardAdmirals);
            if (cardAdmirals.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        // else if (data is Equipments equipments)
        // {
        //     // mainId = cardAdmirals.id;
        //     await DetailMasterManager.Instance.CreateEquipmentsEquipments(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, equipments);
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
