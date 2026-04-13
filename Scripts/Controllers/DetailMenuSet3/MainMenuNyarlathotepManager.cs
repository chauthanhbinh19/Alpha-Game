using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MainMenuNyarlathotepManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private GameObject MainMenuNyarlathotepPanelPrefab;
    private GameObject SlotPrefab;
    private GameObject TypeButtonPrefab;
    private GameObject currentObject;
    private Button UpLevelButton;
    private Button UpMaxLevelButton;
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
        MainMenuNyarlathotepPanelPrefab = UIManager.Instance.Get("MainMenuNyarlathotepPanelPrefab");
        TypeButtonPrefab = UIManager.Instance.Get("TypeButtonPrefab");
        SlotPrefab = UIManager.Instance.Get("NyarlathotepSlotPrefab");
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
    }
    public async Task CreateMainMenuNyarlathotepManagerAsync(object data)
    {
        currentObject = Instantiate(MainMenuNyarlathotepPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        TabButtonPanel = transform.Find("Scroll View/Viewport/Content");
        SlotPanel = transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainMenuSet3.NYARLATHOTEP);
        parentType = AppConstants.MainMenuSet3.NYARLATHOTEP;
        UpLevelButton = transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        UpMaxLevelButton = transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
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
        background.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.BACKGROUND_119_URL);
        RawImage closeButtonBackground = closeButton.GetComponent<RawImage>();
        RawImage homeButtonBackground = homeButton.GetComponent<RawImage>();
        closeButtonBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Button.BACK_BUTTON_BACKGROUND_URL);
        homeButtonBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Button.HOME_BUTTON_BACKGROUND_URL);
        RawImage scrollViewBackground = transform.Find("DictionaryCards/ScrollViewBackground").GetComponent<RawImage>();
        scrollViewBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SCROLLVIEW_BACKGROUND_3_URL);
        RawImage titleBackground = transform.Find("DictionaryCards/TitleBackground").GetComponent<RawImage>();
        titleBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Button.TITLE_BUTTON_BACKGROUND_URL);

        LevelCondition = transform.Find("DictionaryCards/LevelCondition");

        Dictionary<string, Features> uniqueTypes = new Dictionary<string, Features>();
        uniqueTypes = await FeaturesService.Create().GetFeaturesByTypeAsync(AppConstants.MainMenuSet3.NYARLATHOTEP);
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
                        await DetailMenuManager.Instance.CreateCardHeroesEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardHeroes);
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
                        await DetailMenuManager.Instance.CreateBooksEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, books);
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
                        await DetailMenuManager.Instance.CreateCardCaptainsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardCaptains);
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
                        await DetailMenuManager.Instance.CreatePetsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, pets);
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
                        await DetailMenuManager.Instance.CreateCardMilitaryEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardMilitary);
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
                        await DetailMenuManager.Instance.CreateCardSpellEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardSpell);
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
                        await DetailMenuManager.Instance.CreateCardMonstersEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardMonsters);
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
                        await DetailMenuManager.Instance.CreateCardColonelsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardColonels);
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
                        await DetailMenuManager.Instance.CreateCardGeneralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardGenerals);
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
                        await DetailMenuManager.Instance.CreateCardAdmiralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardAdmirals);
                        if (cardAdmirals.Level >= requiredLevel)
                        {
                            LevelCondition.gameObject.SetActive(false);
                        }
                        else
                        {
                            CreateWarningLevelCondition(requiredLevel);
                        }
                    }
                    else if (data is Equipments equipments)
                    {
                        // mainId = cardAdmirals.id;
                        await DetailMenuManager.Instance.CreateEquipmentsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, equipments);
                        if (equipments.Level >= requiredLevel)
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

        if (data is CardHeroes cardHeroes)
        {
            // mainId = cardHeroes.id;
            await DetailMenuManager.Instance.CreateCardHeroesEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardHeroes);
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
            await DetailMenuManager.Instance.CreateBooksEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, books);
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
            await DetailMenuManager.Instance.CreateCardCaptainsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardCaptains);
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
            await DetailMenuManager.Instance.CreatePetsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, pets);
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
            await DetailMenuManager.Instance.CreateCardMilitaryEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardMilitary);
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
            await DetailMenuManager.Instance.CreateCardSpellEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardSpell);
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
            await DetailMenuManager.Instance.CreateCardMonstersEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardMonsters);
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
            await DetailMenuManager.Instance.CreateCardColonelsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardColonels);
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
            await DetailMenuManager.Instance.CreateCardGeneralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardGenerals);
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
            await DetailMenuManager.Instance.CreateCardAdmiralsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, cardAdmirals);
            if (cardAdmirals.Level >= requiredLevel)
            {
                LevelCondition.gameObject.SetActive(false);
            }
            else
            {
                CreateWarningLevelCondition(requiredLevel);
            }
        }
        else if (data is Equipments equipments)
        {
            // mainId = cardAdmirals.id;
            await DetailMenuManager.Instance.CreateEquipmentsEquipmentsAsync(SlotPrefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, feature, parentType, equipments);
            if (equipments.Level >= requiredLevel)
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
