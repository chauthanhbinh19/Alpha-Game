using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuEquipmentManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private Transform SetPanel;
    private GameObject TypeButtonPrefab;
    private GameObject MainMenuEquipmentPanelPrefab;
    private GameObject PopupEquipmentsPanelPrefab;
    private GameObject EquipmentsWearingPrefab;
    private GameObject currentObject;
    private GameObject slotObject;
    private GameObject Slot1Prefab;
    private GameObject Slot4Prefab;
    private GameObject Slot6Prefab;
    private GameObject Slot8Prefab;
    private GameObject Slot10Prefab;
    private GameObject Slot12Prefab;
    private GameObject Slot14Prefab;
    private GameObject Slot16Prefab;
    private GameObject popupEquipmentObject;
    private GameObject SetButtonPrefab;
    private GameObject StarPrefab;
    private Button equipOneTypeButton;
    private Button equipAllTypeButton;
    private RawImage mainImage;
    private string mainType;
    private int pageSize;
    private int offset;
    private int currentPage;
    private int totalPage;
    private string statusToggle;
    private string set;
    TeamsService teamsService;
    private string search;
    // private string type;
    private string rare;
    EquipmentType equipmentType;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        pageSize = 100;
        offset = 0;
        currentPage = 1;
        set = "set1";
        search = "";
        // type = AppConstants.Type.ALL;
        rare = AppConstants.Rare.ALL;
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MainMenuEquipmentPanelPrefab = UIManager.Instance.Get("MainMenuEquipmentPanelPrefab");
        PopupEquipmentsPanelPrefab = UIManager.Instance.Get("PopupEquipmentsPanelPrefab");
        EquipmentsWearingPrefab = UIManager.Instance.Get("EquipmentsWearingPrefab");
        TypeButtonPrefab = UIManager.Instance.Get("TypeButtonPrefab");
        Slot1Prefab = UIManager.Instance.Get("Slot1Prefab");
        Slot4Prefab = UIManager.Instance.Get("Slot4Prefab");
        Slot6Prefab = UIManager.Instance.Get("Slot6Prefab");
        Slot8Prefab = UIManager.Instance.Get("Slot8Prefab");
        Slot10Prefab = UIManager.Instance.Get("Slot10Prefab");
        Slot12Prefab = UIManager.Instance.Get("Slot12Prefab");
        Slot14Prefab = UIManager.Instance.Get("Slot14Prefab");
        Slot16Prefab = UIManager.Instance.Get("Slot16Prefab");
        SetButtonPrefab = UIManager.Instance.Get("SetButtonPrefab");
        StarPrefab = UIManager.Instance.Get("StarPrefab");

        teamsService = TeamsService.Create();
    }
    public async Task CreateMainMenuEquipmentManagerAsync(object data)
    {
        currentObject = Instantiate(MainMenuEquipmentPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        TabButtonPanel = transform.Find("Scroll View/Viewport/Content");
        SlotPanel = transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainMenuSet1.EQUIPMENTS);
        SetPanel = transform.Find("DictionaryCards/SetGroup/Viewport/Content");
        mainImage = transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
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
        background.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.BACKGROUND_58_URL);
        RawImage closeButtonBackground = closeButton.GetComponent<RawImage>();
        RawImage homeButtonBackground = homeButton.GetComponent<RawImage>();
        closeButtonBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Button.BACK_BUTTON_BACKGROUND_URL);
        homeButtonBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Button.HOME_BUTTON_BACKGROUND_URL);
        RawImage scrollViewBackground = transform.Find("DictionaryCards/ScrollViewBackground").GetComponent<RawImage>();
        scrollViewBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Background.SCROLLVIEW_BACKGROUND_1_URL);
        RawImage titleBackground = transform.Find("DictionaryCards/TitleBackground").GetComponent<RawImage>();
        titleBackground.texture = TextureHelper.LoadTextureCached(ImageConstants.Button.TITLE_BUTTON_BACKGROUND_URL);
        equipOneTypeButton = transform.Find("DictionaryCards/EquipOneTypeButton").GetComponent<Button>();
        equipAllTypeButton = transform.Find("DictionaryCards/EquipAllTypeButton").GetComponent<Button>();

        List<string> uniqueTypes = await EquipmentsService.Create().GetUniqueEquipmentsTypesAsync();
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                // Tạo một nút mới từ prefab
                string subtype = uniqueTypes[i];
                GameObject button = Instantiate(TypeButtonPrefab, TabButtonPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await OnButtonClickAsync(button, data, subtype);
                });

                if (i == 0)
                {
                    mainType = subtype;
                    ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    if (data is CardHeroes cardHero)
                    {
                        await CreateCardHeroesEquipmentsAsync(cardHero);
                    }
                    else if (data is Books book)
                    {
                        await CreateBooksEquipmentsAsync(book);
                    }
                    else if (data is CardCaptains cardCaptain)
                    {
                        await CreateCardCaptainsEquipmentsAsync(cardCaptain);
                    }
                    else if (data is Pets pet)
                    {
                        await CreatePetsEquipmentsAsync(pet);
                    }
                    else if (data is CardMilitaries cardMilitary)
                    {
                        await CreateCardMilitaryEquipmentsAsync(cardMilitary);
                    }
                    else if (data is CardSpells cardSpell)
                    {
                        await CreateCardSpellEquipmentsAsync(cardSpell);
                    }
                    else if (data is CardMonsters cardMonster)
                    {
                        await CreateCardMonstersEquipmentsAsync(cardMonster);
                    }
                    else if (data is CardColonels cardColonel)
                    {
                        await CreateCardColonelsEquipmentsAsync(cardColonel);
                    }
                    else if (data is CardGenerals cardGeneral)
                    {
                        await CreateCardGeneralsEquipmentsAsync(cardGeneral);
                    }
                    else if (data is CardAdmirals cardAdmiral)
                    {
                        await CreateCardAdmiralsEquipmentsAsync(cardAdmiral);
                    }
                }
                else
                {
                    ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                }
            }
            LoadAnimation();
        }

        equipOneTypeButton.onClick.AddListener((UnityEngine.Events.UnityAction)(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (data is CardHeroes cardHero)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardHeroAsync((string)cardHero.Id, mainType);
                if (success)
                {
                    await CreateCardHeroesEquipmentsAsync(cardHero);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments of type to CardHero.");
                }
            }
            else if (data is CardCaptains cardCaptain)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardCaptainAsync(cardCaptain.Id, mainType);
                if (success)
                {
                    await CreateCardCaptainsEquipmentsAsync(cardCaptain);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments of type to CardCaptain.");
                }
            }
            else if (data is CardColonels cardColonel)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardColonelAsync(cardColonel.Id, mainType);
                if (success)
                {
                    await CreateCardColonelsEquipmentsAsync(cardColonel);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments of type to CardColonel.");
                }
            }
            else if (data is CardGenerals cardGeneral)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardGeneralAsync(cardGeneral.Id, mainType);
                if (success)
                {
                    await CreateCardGeneralsEquipmentsAsync(cardGeneral);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments of type to CardGeneral.");
                }
            }
            else if (data is CardAdmirals cardAdmiral)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardAdmiralAsync(cardAdmiral.Id, mainType);
                if (success)
                {
                    await CreateCardAdmiralsEquipmentsAsync(cardAdmiral);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments of type to CardAdmiral.");
                }
            }
            else if (data is CardMonsters cardMonster)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardMonsterAsync(cardMonster.Id, mainType);
                if (success)
                {
                    await CreateCardMonstersEquipmentsAsync(cardMonster);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments of type to CardMonster.");
                }
            }
            else if (data is CardMilitaries cardMilitary)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardMilitaryAsync(cardMilitary.Id, mainType);
                if (success)
                {
                    await CreateCardMilitaryEquipmentsAsync(cardMilitary);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments of type to CardMilitary.");
                }
            }
            else if (data is CardSpells cardSpell)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardSpellAsync(cardSpell.Id, mainType);
                if (success)
                {
                    await CreateCardSpellEquipmentsAsync(cardSpell);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments of type to CardSpell.");
                }
            }
            else if (data is Books book)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToBookAsync(book.Id, mainType);
                if (success)
                {
                    await CreateBooksEquipmentsAsync(book);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments of type to Book.");
                }
            }
            else if (data is Pets pet)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToPetAsync(pet.Id, mainType);
                if (success)
                {
                    await CreatePetsEquipmentsAsync(pet);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments of type to Pet.");
                }
            }
        }));

        equipAllTypeButton.onClick.AddListener((UnityEngine.Events.UnityAction)(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (data is CardHeroes cardHero)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardHeroAsync((string)cardHero.Id);
                if (success)
                {
                    await CreateCardHeroesEquipmentsAsync(cardHero);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments to CardHero.");
                }
            }
            else if (data is CardCaptains cardCaptain)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardCaptainAsync(cardCaptain.Id);
                if (success)
                {
                    await CreateCardCaptainsEquipmentsAsync(cardCaptain);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments to CardCaptain.");
                }
            }
            else if (data is CardColonels cardColonel)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardColonelAsync(cardColonel.Id);
                if (success)
                {
                    await CreateCardColonelsEquipmentsAsync(cardColonel);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments to CardColonel.");
                }
            }
            else if (data is CardGenerals cardGeneral)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardGeneralAsync(cardGeneral.Id);
                if (success)
                {
                    await CreateCardGeneralsEquipmentsAsync(cardGeneral);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments to CardGeneral.");
                }
            }
            else if (data is CardAdmirals cardAdmiral)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardAdmiralAsync(cardAdmiral.Id);
                if (success)
                {
                    await CreateCardAdmiralsEquipmentsAsync(cardAdmiral);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments to CardAdmiral.");
                }
            }
            else if (data is CardMonsters cardMonster)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardMonsterAsync(cardMonster.Id);
                if (success)
                {
                    await CreateCardMonstersEquipmentsAsync(cardMonster);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments to CardMonster.");
                }
            }
            else if (data is CardMilitaries cardMilitary)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardMilitaryAsync(cardMilitary.Id);
                if (success)
                {
                    await CreateCardMilitaryEquipmentsAsync(cardMilitary);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments to CardMilitary.");
                }
            }
            else if (data is CardSpells cardSpell)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardSpellAsync(cardSpell.Id);
                if (success)
                {
                    await CreateCardSpellEquipmentsAsync(cardSpell);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments to CardSpell.");
                }
            }
            else if (data is Books book)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToBookAsync(book.Id);
                if (success)
                {
                    await CreateBooksEquipmentsAsync(book);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments to Book.");
                }
            }
            else if (data is Pets pet)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToPetAsync(pet.Id);
                if (success)
                {
                    await CreatePetsEquipmentsAsync(pet);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else
                {
                    Debug.LogError("Failed to equip all equipments to Pet.");
                }
            }
        }));

        _ = CreateSetButtonAsync(data);
    }
    async Task OnButtonClickAsync(GameObject clickedButton, object data, string type)
    {
        foreach (Transform child in TabButtonPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ChangeButtonBackground(button.gameObject, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL); // Giả sử bạn có texture trắng
            }
        }

        mainType = type;
        ChangeButtonBackground(clickedButton, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
        _ = CreateSetButtonAsync(data);
        if (data is CardHeroes cardHeroes)
        {
            await CreateCardHeroesEquipmentsAsync(cardHeroes);
        }
        else if (data is Books books)
        {
            await CreateBooksEquipmentsAsync(books);
        }
        else if (data is CardCaptains cardCaptains)
        {
            await CreateCardCaptainsEquipmentsAsync(cardCaptains);
        }
        else if (data is Pets pets)
        {
            await CreatePetsEquipmentsAsync(pets);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            await CreateCardMilitaryEquipmentsAsync(cardMilitary);
        }
        else if (data is CardSpells cardSpell)
        {
            await CreateCardSpellEquipmentsAsync(cardSpell);
        }
        else if (data is CardMonsters cardMonsters)
        {
            await CreateCardMonstersEquipmentsAsync(cardMonsters);
        }
        else if (data is CardColonels cardColonels)
        {
            await CreateCardColonelsEquipmentsAsync(cardColonels);
        }
        else if (data is CardGenerals cardGenerals)
        {
            await CreateCardGeneralsEquipmentsAsync(cardGenerals);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            await CreateCardAdmiralsEquipmentsAsync(cardAdmirals);
        }
    }
    private void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = TextureHelper.LoadTextureCached($"{image}");
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
    public async Task CreateSetButtonAsync(object data)
    {
        ButtonEvent.Instance.Close(SetPanel);
        List<string> uniqueSet = await EquipmentsService.Create().GetEquipmentsSetAsync(mainType);
        if (uniqueSet.Count > 0)
        {
            for (int i = 0; i < uniqueSet.Count; i++)
            {
                string subtype = uniqueSet[i];
                GameObject button = Instantiate(SetButtonPrefab, SetPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = subtype.Replace("set", "");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await OnSetButtonClickAsync(button, data, subtype);
                });
                if (i == 0)
                {
                    set = subtype;
                    ChangeButtonBackground(button, ImageConstants.Button.SET_BUTTON_AFTER_CLICK_URL);
                }
                else
                {
                    ChangeButtonBackground(button, ImageConstants.Button.SET_BUTTON_BEFORE_CLICK_URL);
                }
            }
        }
    }
    async Task OnSetButtonClickAsync(GameObject clickedButton, object data, string type)
    {
        foreach (Transform child in SetPanel)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ChangeButtonBackground(button.gameObject, ImageConstants.Button.SET_BUTTON_BEFORE_CLICK_URL); // Giả sử bạn có texture trắng
            }
        }

        set = type;
        ChangeButtonBackground(clickedButton, ImageConstants.Button.SET_BUTTON_AFTER_CLICK_URL);
        // CreateSetButton();
        if (data is CardHeroes cardHero)
        {
            await CreateCardHeroesEquipmentsAsync(cardHero);
        }
        else if (data is Books book)
        {
            await CreateBooksEquipmentsAsync(book);
        }
        else if (data is CardCaptains cardCaptain)
        {
            await CreateCardCaptainsEquipmentsAsync(cardCaptain);
        }
        else if (data is Pets pet)
        {
            await CreatePetsEquipmentsAsync(pet);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            await CreateCardMilitaryEquipmentsAsync(cardMilitary);
        }
        else if (data is CardSpells cardSpell)
        {
            await CreateCardSpellEquipmentsAsync(cardSpell);
        }
        else if (data is CardMonsters cardMonster)
        {
            await CreateCardMonstersEquipmentsAsync(cardMonster);
        }
        else if (data is CardColonels cardColonel)
        {
            await CreateCardColonelsEquipmentsAsync(cardColonel);
        }
        else if (data is CardGenerals cardGeneral)
        {
            await CreateCardGeneralsEquipmentsAsync(cardGeneral);
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            await CreateCardAdmiralsEquipmentsAsync(cardAdmiral);
        }
    }
    public async Task CreateCardHeroesEquipmentsAsync(CardHeroes cardHero)
    {
        ButtonEvent.Instance.Close(SlotPanel);

        List<Equipments> equipments = new List<Equipments>();
        equipments = await UserEquipmentsService.Create().GetCardHeroesEquipmentsAsync(User.CurrentUserId, cardHero.Id, mainType);
        equipments = equipments.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardHero.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        equipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(mainType);
        if (equipmentType.SlotValue == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (equipmentType.SlotValue == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardHero, EquipmentSlot1Button, 1, equipments);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments);
            }
        }
    }
    public async Task CreateCardCaptainsEquipmentsAsync(CardCaptains cardCaptain)
    {
        ButtonEvent.Instance.Close(SlotPanel);

        List<Equipments> equipments = new List<Equipments>();
        equipments = await UserEquipmentsService.Create().GetCardCaptainsEquipmentsAsync(User.CurrentUserId, cardCaptain.Id, mainType);
        equipments = equipments.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardCaptain.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        equipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(mainType);
        if (equipmentType.SlotValue == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (equipmentType.SlotValue == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardCaptain, EquipmentSlot1Button, 1, equipments);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments);
            }
        }
    }
    public async Task CreateCardColonelsEquipmentsAsync(CardColonels cardColonel)
    {
        ButtonEvent.Instance.Close(SlotPanel);

        List<Equipments> equipments = new List<Equipments>();
        equipments = await UserEquipmentsService.Create().GetCardColonelsEquipmentsAsync(User.CurrentUserId, cardColonel.Id, mainType);
        equipments = equipments.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardColonel.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        equipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(mainType);
        if (equipmentType.SlotValue == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (equipmentType.SlotValue == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardColonel, EquipmentSlot1Button, 1, equipments);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments);
            }
        }
    }
    public async Task CreateCardGeneralsEquipmentsAsync(CardGenerals cardGeneral)
    {
        ButtonEvent.Instance.Close(SlotPanel);

        List<Equipments> equipments = new List<Equipments>();
        equipments = await UserEquipmentsService.Create().GetCardGeneralsEquipmentsAsync(User.CurrentUserId, cardGeneral.Id, mainType);
        equipments = equipments.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardGeneral.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        equipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(mainType);
        if (equipmentType.SlotValue == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (equipmentType.SlotValue == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardGeneral, EquipmentSlot1Button, 1, equipments);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments);
            }
        }
    }
    public async Task CreateCardAdmiralsEquipmentsAsync(CardAdmirals cardAdmiral)
    {
        ButtonEvent.Instance.Close(SlotPanel);

        List<Equipments> equipments = new List<Equipments>();
        equipments = await UserEquipmentsService.Create().GetCardAdmiralsEquipmentsAsync(User.CurrentUserId, cardAdmiral.Id, mainType);
        equipments = equipments.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardAdmiral.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        equipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(mainType);
        if (equipmentType.SlotValue == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (equipmentType.SlotValue == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardAdmiral, EquipmentSlot1Button, 1, equipments);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments);
            }
        }
    }
    public async Task CreateCardMonstersEquipmentsAsync(CardMonsters cardMonster)
    {
        ButtonEvent.Instance.Close(SlotPanel);

        List<Equipments> equipments = new List<Equipments>();
        equipments = await UserEquipmentsService.Create().GetCardMonstersEquipmentsAsync(User.CurrentUserId, cardMonster.Id, mainType);
        equipments = equipments.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardMonster.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        equipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(mainType);
        if (equipmentType.SlotValue == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (equipmentType.SlotValue == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardMonster, EquipmentSlot1Button, 1, equipments);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments);
            }
        }
    }
    public async Task CreateCardMilitaryEquipmentsAsync(CardMilitaries cardMilitary)
    {
        ButtonEvent.Instance.Close(SlotPanel);

        List<Equipments> equipments = new List<Equipments>();
        equipments = await UserEquipmentsService.Create().GetCardMilitariesEquipmentsAsync(User.CurrentUserId, cardMilitary.Id, mainType);
        equipments = equipments.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardMilitary.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        equipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(mainType);
        if (equipmentType.SlotValue == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (equipmentType.SlotValue == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardMilitary, EquipmentSlot1Button, 1, equipments);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments);
            }
        }
    }
    public async Task CreateCardSpellEquipmentsAsync(CardSpells cardSpell)
    {
        ButtonEvent.Instance.Close(SlotPanel);

        List<Equipments> equipments = new List<Equipments>();
        equipments = await UserEquipmentsService.Create().GetCardSpellsEquipmentsAsync(User.CurrentUserId, cardSpell.Id, mainType);
        equipments = equipments.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardSpell.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        equipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(mainType);
        if (equipmentType.SlotValue == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (equipmentType.SlotValue == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardSpell, EquipmentSlot1Button, 1, equipments);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments);
            }
        }
    }
    public async Task CreateBooksEquipmentsAsync(Books book)
    {
        ButtonEvent.Instance.Close(SlotPanel);

        List<Equipments> equipments = new List<Equipments>();
        equipments = await UserEquipmentsService.Create().GetBooksEquipmentsAsync(User.CurrentUserId, book.Id, mainType);
        equipments = equipments.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(book.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        equipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(mainType);
        if (equipmentType.SlotValue == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (equipmentType.SlotValue == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(book, EquipmentSlot1Button, 1, equipments);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments);
            }
        }
    }
    public async Task CreatePetsEquipmentsAsync(Pets pet)
    {
        ButtonEvent.Instance.Close(SlotPanel);

        List<Equipments> equipments = new List<Equipments>();
        equipments = await UserEquipmentsService.Create().GetPetsEquipmentsAsync(User.CurrentUserId, pet.Id, mainType);
        equipments = equipments.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(pet.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        equipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(mainType);
        if (equipmentType.SlotValue == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (equipmentType.SlotValue == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(pet, EquipmentSlot1Button, 1, equipments);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments);
            }
        }
    }
    public Button[] CreateButtonArray(int numberOfSlot)
    {
        Button[] slotButtons;
        if (numberOfSlot == 4)
        {
            slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>()
            };
            return slotButtons;
        }
        else if (numberOfSlot == 6)
        {
            slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
            };
            return slotButtons;
        }
        else if (numberOfSlot == 8)
        {
            slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>()
            };
            return slotButtons;
        }
        else if (numberOfSlot == 10)
        {
            slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
            };
            return slotButtons;
        }
        else if (numberOfSlot == 12)
        {
            slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
            };
            return slotButtons;
        }
        else if (numberOfSlot == 14)
        {
            slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
            };
            return slotButtons;
        }
        else if (numberOfSlot == 16)
        {
            slotButtons = new Button[]
            {
                slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot15Button").GetComponent<Button>(),
                slotObject.transform.Find("EquipmentSlot16Button").GetComponent<Button>(),
            };
            return slotButtons;
        }
        return null;
    }
    public void ApplyEquipmentImage(object data, Button button, int position, List<Equipments> equipmentList)
    {
        bool foundEquipment = false;
        Equipments foundEquip = null;
        foreach (Equipments equipment in equipmentList)
        {
            if (equipment.Position == position)
            {
                string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(equipment.Image);
                Texture equipmentTexture = TextureHelper.LoadTextureCached(fileNameWithoutExtension);

                if (equipmentTexture != null)
                {
                    RawImage rawImage = button.GetComponent<RawImage>();
                    rawImage.texture = equipmentTexture;

                    TextMeshProUGUI LevelText = button.transform.Find("Level").GetComponent<TextMeshProUGUI>();
                    if (LevelText != null)
                    {
                        if (equipment.Level != 0)
                        {
                            LevelText.text = equipment.Level.ToString();
                        }
                    }
                    else
                    {
                        Debug.LogError("Không tìm thấy TextMeshProUGUI trong button: " + button.name);
                    }

                    Transform currentStar = button.transform.Find("Star");
                    CreateStarUI(equipment.Star, currentStar);

                    Transform borderEffect = button.transform.Find("BorderEffect");
                    if (borderEffect != null)
                    {
                        if (equipmentType.CanUseBorderEffect)
                        {
                            borderEffect.gameObject.SetActive(true);
                        }
                    }
                }

                foundEquipment = true; // Đánh dấu là đã tìm thấy thiết bị
                foundEquip = equipment; // Lưu lại equipment
                break;
            }
        }
        // Nếu không tìm thấy thiết bị nào, thêm sự kiện onClick
        if (!foundEquipment)
        {
            // button.onClick.RemoveAllListeners(); // Xóa các sự kiện trước đó (nếu có)
            button.onClick.AddListener(async () =>
            {
                await CreatePopupEquipmentsAsync(data, position);
            });
        }
        else
        {
            // Đã tìm thấy equipment
            Equipments tempEquip = foundEquip;
            button.onClick.AddListener(() =>
            {
                MainMenuDetailsManager.Instance.PopupDetails(tempEquip, MainPanel);
            });
        }
    }
    public async Task CreatePopupEquipmentsAsync(object data, int position, string statusToggle = "NOT EQUIP")
    {
        popupEquipmentObject = Instantiate(PopupEquipmentsPanelPrefab, MainPanel);
        Transform contentPanel = popupEquipmentObject.transform.Find("Scroll View/Viewport/Content");
        Text PageText = popupEquipmentObject.transform.Find("Pagination/Page").GetComponent<Text>();
        Toggle toggle = popupEquipmentObject.transform.Find("Toggle").GetComponent<Toggle>();
        toggle.isOn = (statusToggle == "ALL");
        toggle.onValueChanged.AddListener(async (bool isOn) =>
        {
            string newStatusToggle = isOn ? "ALL" : "NOT EQUIP";
            Destroy(popupEquipmentObject);
            await CreatePopupEquipmentsAsync(data, position, newStatusToggle); // Gọi lại nhưng giữ statusToggle mới
        });
        Button nextButton = popupEquipmentObject.transform.Find("Pagination/Next").GetComponent<Button>();
        Button previousButton = popupEquipmentObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        Button closeButton = popupEquipmentObject.transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => Destroy(popupEquipmentObject));
        List<Equipments> equipments = new List<Equipments>();
        if (data is CardHeroes cardHero)
        {
            equipments = await UserEquipmentsService.Create().GetAllCardHeroesEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardCaptains cardCaptain)
        {
            equipments = await UserEquipmentsService.Create().GetAllCardCaptainsEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardColonels cardColonel)
        {
            equipments = await UserEquipmentsService.Create().GetAllCardColonelsEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardGenerals cardGeneral)
        {
            equipments = await UserEquipmentsService.Create().GetAllCardGeneralsEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            equipments = await UserEquipmentsService.Create().GetAllCardAdmiralsEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardMonsters cardMonster)
        {
            equipments = await UserEquipmentsService.Create().GetAllCardMonstersEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            equipments = await UserEquipmentsService.Create().GetAllCardMilitariesEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardSpells cardSpell)
        {
            equipments = await UserEquipmentsService.Create().GetAllCardSpellsEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is Books book)
        {
            equipments = await UserEquipmentsService.Create().GetAllBooksEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is Pets pet)
        {
            equipments = await UserEquipmentsService.Create().GetAllPetsEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        equipments = equipments.Where(e => e.Set == set).ToList();
        int totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, mainType, rare);
        totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);

        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        CreatePopupEquipmentsUI(data, equipments, contentPanel, position);
        nextButton.onClick.AddListener(async () => { await ChangeNextPageAsync(data, PageText, contentPanel, mainType, position); });
        previousButton.onClick.AddListener(async () => { await ChangePreviousPageAsync(data, PageText, contentPanel, mainType, position); });
    }
    public void CreatePopupEquipmentsUI(object data, List<Equipments> equipmentsList, Transform content, int position)
    {
        foreach (var equipment in equipmentsList)
        {
            GameObject equipmentObject = Instantiate(EquipmentsWearingPrefab, content);

            TextMeshProUGUI titleText = equipmentObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            titleText.text = equipment.Name.Replace("_", " ");

            TextMeshProUGUI powerText = equipmentObject.transform.Find("PowerText").GetComponent<TextMeshProUGUI>();
            powerText.text = equipment.Power.ToString();

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            Texture texture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(equipment.Image));
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{equipment.Rare}");
            rareImage.texture = rareTexture;

            Button equipButton = equipmentObject.transform.Find("EquipButton").GetComponent<Button>();
            equipButton.onClick.AddListener((UnityEngine.Events.UnityAction)(async () =>
            {
                Destroy(popupEquipmentObject);
                if (data is CardHeroes cardHero)
                {
                    await UserEquipmentsService.Create().InsertCardHeroEquipmentsAsync((string)cardHero.Id, equipment, position);
                    await CreateCardHeroesEquipmentsAsync(cardHero);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardCaptains cardCaptain)
                {
                    await UserEquipmentsService.Create().InsertCardCaptainEquipmentsAsync(cardCaptain.Id, equipment, position);
                    await CreateCardCaptainsEquipmentsAsync(cardCaptain);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardColonels cardColonel)
                {
                    await UserEquipmentsService.Create().InsertCardColonelEquipmentsAsync(cardColonel.Id, equipment, position);
                    await CreateCardColonelsEquipmentsAsync(cardColonel);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardGenerals cardGeneral)
                {
                    await UserEquipmentsService.Create().InsertCardGeneralEquipmentsAsync(cardGeneral.Id, equipment, position);
                    await CreateCardGeneralsEquipmentsAsync(cardGeneral);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardAdmirals cardAdmiral)
                {
                    await UserEquipmentsService.Create().InsertCardAdmiralEquipmentsAsync(cardAdmiral.Id, equipment, position);
                    await CreateCardAdmiralsEquipmentsAsync(cardAdmiral);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardMonsters cardMonster)
                {
                    await UserEquipmentsService.Create().InsertCardMonsterEquipmentsAsync(cardMonster.Id, equipment, position);
                    await CreateCardMonstersEquipmentsAsync(cardMonster);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardMilitaries cardMilitary)
                {
                    await UserEquipmentsService.Create().InsertCardMilitaryEquipmentsAsync(cardMilitary.Id, equipment, position);
                    await CreateCardMilitaryEquipmentsAsync(cardMilitary);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardSpells cardSpell)
                {
                    await UserEquipmentsService.Create().InsertCardSpellEquipmentsAsync(cardSpell.Id, equipment, position);
                    await CreateCardSpellEquipmentsAsync(cardSpell);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is Books book)
                {
                    await UserEquipmentsService.Create().InsertBookEquipmentsAsync(book.Id, equipment, position);
                    await CreateBooksEquipmentsAsync(book);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is Pets pet)
                {
                    await UserEquipmentsService.Create().InsertPetEquipmentsAsync(pet.Id, equipment, position);
                    await CreatePetsEquipmentsAsync(pet);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }

                Destroy(popupEquipmentObject);
            }));
        }
        GridLayoutGroup gridLayout = content.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(340, 130);
        }
    }
    public async Task ChangeNextPageAsync(object data, Text PageText, Transform content, string subType, int position)
    {
        if (currentPage < totalPage)
        {
            ButtonEvent.Instance.Close(content);
            int totalRecord = 0;

            if (data is CardHeroes cardHero)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardHeroesEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardCaptains cardCaptain)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardCaptainsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardColonels cardColonel)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardColonelsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardGenerals cardGeneral)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardGeneralsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardAdmirals cardAdmiral)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardAdmiralsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMonsters cardMonster)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardMonstersEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMilitaries cardMilitary)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardMilitariesEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardSpells cardSpell)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardSpellsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Books book)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllBooksEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Pets pet)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllPetsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public async Task ChangePreviousPageAsync(object data, Text PageText, Transform content, string subType, int position)
    {
        if (currentPage > 1)
        {
            ButtonEvent.Instance.Close(content);
            int totalRecord = 0;

            if (data is CardHeroes cardHero)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardHeroesEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardCaptains cardCaptain)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardCaptainsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardColonels cardColonel)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardColonelsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardGenerals cardGeneral)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardGeneralsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardAdmirals cardAdmiral)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardAdmiralsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMonsters cardMonster)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardMonstersEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMilitaries cardMilitary)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardMilitariesEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardSpells cardSpell)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardSpellsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Books book)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllBooksEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Pets pet)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = PageHelper.CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllPetsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }


            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void CreateStarUI(int star, Transform currentStar)
    {
        int imageIndex = (star == 0) ? 0 : ((star - 1) % 10) + 1;
        int starIndex = (star == 0) ? 1 : (star - 1) / 10;
        for (int i = 0; i < imageIndex; i++)
        {
            GameObject starObject = Instantiate(StarPrefab, currentStar);

            RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
            GetStarImage(starImage, starIndex);
        }
        GridLayoutGroup GridLayout = currentStar.GetComponent<GridLayoutGroup>();
        if (GridLayout != null)
        {
            GridLayout.cellSize = new Vector2(20, 20);
        }
    }
    public void GetStarImage(RawImage starImage, int starIndex)
    {
        Texture starTexture = TextureHelper.LoadTextureCached($"UI/UI/Star1");
        switch (starIndex)
        {
            case 0:
                starTexture = TextureHelper.LoadTextureCached($"UI/UI/Star1");
                starImage.texture = starTexture;
                break;
            case 1:
                starTexture = TextureHelper.LoadTextureCached($"UI/UI/Star2");
                starImage.texture = starTexture;
                break;
            case 2:
                starTexture = TextureHelper.LoadTextureCached($"UI/UI/Star3");
                starImage.texture = starTexture;
                break;
            case 3:
                starTexture = TextureHelper.LoadTextureCached($"UI/UI/Star4");
                starImage.texture = starTexture;
                break;
            case 4:
                starTexture = TextureHelper.LoadTextureCached($"UI/UI/Star5");
                starImage.texture = starTexture;
                break;
            case 5:
                starTexture = TextureHelper.LoadTextureCached($"UI/UI/Star6");
                starImage.texture = starTexture;
                break;
            case 6:
                starTexture = TextureHelper.LoadTextureCached($"UI/UI/Star7");
                starImage.texture = starTexture;
                break;
            case 7:
                starTexture = TextureHelper.LoadTextureCached($"UI/UI/Star8");
                starImage.texture = starTexture;
                break;
            case 8:
                starTexture = TextureHelper.LoadTextureCached($"UI/UI/Star9");
                starImage.texture = starTexture;
                break;
            case 9:
                starTexture = TextureHelper.LoadTextureCached($"UI/UI/Star10");
                starImage.texture = starTexture;
                break;
            default:
                starTexture = TextureHelper.LoadTextureCached($"UI/UI/Star1");
                starImage.texture = starTexture;
                break;
        }
    }
    public void LoadAnimation()
    {
        TabButtonPanel.gameObject.AddComponent<SlideLeftToRightAnimation>();
    }
}
