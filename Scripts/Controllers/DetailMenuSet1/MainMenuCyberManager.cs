using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MainMenuCyberManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private GameObject MainMenuCyberPanelPrefab;
    private GameObject SlotPrefab;
    private GameObject TypeButtonPrefab;
    private GameObject currentObject;
    private Button upLevelButton;
    private Button upMaxLevelButton;
    private Transform LevelCondition;
    private Features feature;
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
        MainMenuCyberPanelPrefab = UIManager.Instance.Get("MainMenuCyberPanelPrefab");
        TypeButtonPrefab = UIManager.Instance.Get("TypeButtonPrefab");
        SlotPrefab = UIManager.Instance.Get("CyberSlotPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
    }
    public async Task CreateMainMenuCyberManagerAsync(object data)
    {
        currentObject = Instantiate(MainMenuCyberPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        TabButtonPanel = transform.Find("Scroll View/Viewport/Content");
        SlotPanel = transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainMenuSet1.CYBER);
        parentType = AppConstants.MainMenuSet1.CYBER;
        upLevelButton = transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        upMaxLevelButton = transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button homeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        RawImage background = transform.Find("DictionaryBackground").GetComponent<RawImage>();
        background.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.BACKGROUND_62_URL);
        RawImage closeButtonBackground = closeButton.GetComponent<RawImage>();
        RawImage homeButtonBackground = homeButton.GetComponent<RawImage>();
        closeButtonBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Button.BACK_BUTTON_BACKGROUND_URL);
        homeButtonBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Button.HOME_BUTTON_BACKGROUND_URL);
        RawImage scrollViewBackground = transform.Find("DictionaryCards/ScrollViewBackground").GetComponent<RawImage>();
        scrollViewBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SCROLLVIEW_BACKGROUND_1_URL);
        RawImage titleBackground = transform.Find("DictionaryCards/TitleBackground").GetComponent<RawImage>();
        titleBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Button.TITLE_BUTTON_BACKGROUND_URL);

        LevelCondition = transform.Find("DictionaryCards/LevelCondition");

        Dictionary<string, Features> uniqueTypes = new Dictionary<string, Features>();
        uniqueTypes = await FeaturesService.Create().GetFeaturesByTypeAsync(AppConstants.MainMenuSet1.CYBER);
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
                    if (data is CardHeroes cardHero)
                    {
                        // mainId = cardHero.id;
                        await DetailMenuManager.Instance.CreateCardHeroesEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardHero);
                        if (cardHero.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is Books book)
                    {
                        // mainId = book.id;
                        await DetailMenuManager.Instance.CreateBooksEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, book);
                        if (book.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardCaptains cardCaptain)
                    {
                        // mainId = cardCaptain.id;
                        await DetailMenuManager.Instance.CreateCardCaptainsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardCaptain);
                        if (cardCaptain.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is Pets pet)
                    {
                        // mainId = pet.id;
                        await DetailMenuManager.Instance.CreatePetsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, pet);
                        if (pet.Level >= requiredLevel)
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
                        await DetailMenuManager.Instance.CreateCardMilitaryEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardMilitary);
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
                        await DetailMenuManager.Instance.CreateCardSpellEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardSpell);
                        if (cardSpell.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardMonsters cardMonster)
                    {
                        // mainId = cardMonster.id;
                        await DetailMenuManager.Instance.CreateCardMonstersEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardMonster);
                        if (cardMonster.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardColonels cardColonel)
                    {
                        // mainId = cardColonel.id;
                        await DetailMenuManager.Instance.CreateCardColonelsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardColonel);
                        if (cardColonel.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardGenerals cardGeneral)
                    {
                        // mainId = cardGeneral.id;
                        await DetailMenuManager.Instance.CreateCardGeneralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardGeneral);
                        if (cardGeneral.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardAdmirals cardAdmiral)
                    {
                        // mainId = cardAdmiral.id;
                        await DetailMenuManager.Instance.CreateCardAdmiralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardAdmiral);
                        if (cardAdmiral.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is Equipments equipment)
                    {
                        // mainId = cardAdmiral.id;
                        await DetailMenuManager.Instance.CreateEquipmentsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, equipment);
                        if (equipment.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is CardSoldiers cardSoldier)
                    {
                        // mainId = cardAdmirals.id;
                        await DetailMenuManager.Instance.CreateCardSoldiersEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardSoldier);
                        if (cardSoldier.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
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

        if (data is CardHeroes cardHero)
        {
            // mainId = cardHero.id;
            await DetailMenuManager.Instance.CreateCardHeroesEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardHero);
            if (cardHero.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is Books book)
        {
            // mainId = book.id;
            await DetailMenuManager.Instance.CreateBooksEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, book);
            if (book.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardCaptains cardCaptain)
        {
            // mainId = cardCaptain.id;
            await DetailMenuManager.Instance.CreateCardCaptainsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardCaptain);
            if (cardCaptain.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is Pets pet)
        {
            // mainId = pet.id;
            await DetailMenuManager.Instance.CreatePetsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, pet);
            if (pet.Level >= requiredLevel)
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
            await DetailMenuManager.Instance.CreateCardMilitaryEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardMilitary);
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
            await DetailMenuManager.Instance.CreateCardSpellEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardSpell);
            if (cardSpell.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardMonsters cardMonster)
        {
            // mainId = cardMonster.id;
            await DetailMenuManager.Instance.CreateCardMonstersEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardMonster);
            if (cardMonster.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardColonels cardColonel)
        {
            // mainId = cardColonel.id;
            await DetailMenuManager.Instance.CreateCardColonelsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardColonel);
            if (cardColonel.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardGenerals cardGeneral)
        {
            // mainId = cardGeneral.id;
            await DetailMenuManager.Instance.CreateCardGeneralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardGeneral);
            if (cardGeneral.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            // mainId = cardAdmiral.id;
            await DetailMenuManager.Instance.CreateCardAdmiralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardAdmiral);
            if (cardAdmiral.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is Equipments equipment)
        {
            // mainId = cardAdmiral.id;
            await DetailMenuManager.Instance.CreateEquipmentsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, equipment);
            if (equipment.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is CardSoldiers cardSoldier)
        {
            // mainId = cardAdmirals.id;
            await DetailMenuManager.Instance.CreateCardSoldiersEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, parentType, cardSoldier);
            if (cardSoldier.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
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
        warningText.text = MessageConstants.WaringLevel(value);
        LevelCondition.gameObject.AddComponent<SlideBottomToTopAnimation>();
    }
    public void LoadAnimation()
    {
        TabButtonPanel.gameObject.AddComponent<SlideLeftToRightAnimation>();
    }
}
