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
                else
                {
                    ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                }
            }
            LoadAnimation();
        }

        equipOneTypeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (data is CardHeroes cardHeroes)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardHeroAsync(cardHeroes.Id, mainType);
                if (success)
                {
                    await CreateCardHeroesEquipmentsAsync(cardHeroes);
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
            else if (data is CardCaptains cardCaptains)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardCaptainAsync(cardCaptains.Id, mainType);
                if (success)
                {
                    await CreateCardCaptainsEquipmentsAsync(cardCaptains);
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
            else if (data is CardColonels cardColonels)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardColonelAsync(cardColonels.Id, mainType);
                if (success)
                {
                    await CreateCardColonelsEquipmentsAsync(cardColonels);
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
            else if (data is CardGenerals cardGenerals)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardGeneralAsync(cardGenerals.Id, mainType);
                if (success)
                {
                    await CreateCardGeneralsEquipmentsAsync(cardGenerals);
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
            else if (data is CardAdmirals cardAdmirals)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardAdmiralAsync(cardAdmirals.Id, mainType);
                if (success)
                {
                    await CreateCardAdmiralsEquipmentsAsync(cardAdmirals);
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
            else if (data is CardMonsters cardMonsters)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToCardMonsterAsync(cardMonsters.Id, mainType);
                if (success)
                {
                    await CreateCardMonstersEquipmentsAsync(cardMonsters);
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
            else if (data is Books books)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToBookAsync(books.Id, mainType);
                if (success)
                {
                    await CreateBooksEquipmentsAsync(books);
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
            else if (data is Pets pets)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsOfTypeToPetAsync(pets.Id, mainType);
                if (success)
                {
                    await CreatePetsEquipmentsAsync(pets);
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
        });

        equipAllTypeButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            if (data is CardHeroes cardHeroes)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardHeroAsync(cardHeroes.Id);
                if (success)
                {
                    await CreateCardHeroesEquipmentsAsync(cardHeroes);
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
            else if (data is CardCaptains cardCaptains)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardCaptainAsync(cardCaptains.Id);
                if (success)
                {
                    await CreateCardCaptainsEquipmentsAsync(cardCaptains);
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
            else if (data is CardColonels cardColonels)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardColonelAsync(cardColonels.Id);
                if (success)
                {
                    await CreateCardColonelsEquipmentsAsync(cardColonels);
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
            else if (data is CardGenerals cardGenerals)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardGeneralAsync(cardGenerals.Id);
                if (success)
                {
                    await CreateCardGeneralsEquipmentsAsync(cardGenerals);
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
            else if (data is CardAdmirals cardAdmirals)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardAdmiralAsync(cardAdmirals.Id);
                if (success)
                {
                    await CreateCardAdmiralsEquipmentsAsync(cardAdmirals);
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
            else if (data is CardMonsters cardMonsters)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToCardMonsterAsync(cardMonsters.Id);
                if (success)
                {
                    await CreateCardMonstersEquipmentsAsync(cardMonsters);
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
            else if (data is Books books)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToBookAsync(books.Id);
                if (success)
                {
                    await CreateBooksEquipmentsAsync(books);
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
            else if (data is Pets pets)
            {
                bool success = await UserEquipmentsService.Create().EquipAllEquipmentsToPetAsync(pets.Id);
                if (success)
                {
                    await CreatePetsEquipmentsAsync(pets);
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
        });

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
    public void Close(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public async Task CreateSetButtonAsync(object data)
    {
        Close(SetPanel);
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
    public async Task CreateCardHeroesEquipmentsAsync(CardHeroes cardHero)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardHeroesEquipmentsAsync(User.CurrentUserId, cardHero.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardHero.Image);
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
            ApplyEquipmentImage(cardHero, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardCaptainsEquipmentsAsync(CardCaptains cardCaptain)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardCaptainsEquipmentsAsync(User.CurrentUserId, cardCaptain.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardCaptain.Image);
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
            ApplyEquipmentImage(cardCaptain, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardColonelsEquipmentsAsync(CardColonels cardColonel)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardColonelsEquipmentsAsync(User.CurrentUserId, cardColonel.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardColonel.Image);
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
            ApplyEquipmentImage(cardColonel, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardGeneralsEquipmentsAsync(CardGenerals cardGeneral)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardGeneralsEquipmentsAsync(User.CurrentUserId, cardGeneral.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardGeneral.Image);
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
            ApplyEquipmentImage(cardGeneral, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardAdmiralsEquipmentsAsync(CardAdmirals cardAdmiral)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardAdmiralsEquipmentsAsync(User.CurrentUserId, cardAdmiral.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardAdmiral.Image);
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
            ApplyEquipmentImage(cardAdmiral, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardMonstersEquipmentsAsync(CardMonsters cardMonster)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardMonstersEquipmentsAsync(User.CurrentUserId, cardMonster.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMonster.Image);
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
            ApplyEquipmentImage(cardMonster, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardMilitaryEquipmentsAsync(CardMilitaries cardMilitary)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardMilitariesEquipmentsAsync(User.CurrentUserId, cardMilitary.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardMilitary.Image);
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
            ApplyEquipmentImage(cardMilitary, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardSpellEquipmentsAsync(CardSpells cardSpell)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardSpellsEquipmentsAsync(User.CurrentUserId, cardSpell.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(cardSpell.Image);
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
            ApplyEquipmentImage(cardSpell, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateBooksEquipmentsAsync(Books book)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetBooksEquipmentsAsync(User.CurrentUserId, book.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(book.Image);
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
            ApplyEquipmentImage(book, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreatePetsEquipmentsAsync(Pets pet)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetPetsEquipmentsAsync(User.CurrentUserId, pet.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(pet.Image);
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
            ApplyEquipmentImage(pet, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (equipmentType.SlotValue == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (equipmentType.SlotValue == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (equipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(equipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipmentList);
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
                string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(equipment.Image);
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
    public int CalculateTotalPages(int totalRecords, int pageSize)
    {
        if (pageSize <= 0) return 0; // Đảm bảo pageSize không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / pageSize);
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
        Button NextButton = popupEquipmentObject.transform.Find("Pagination/Next").GetComponent<Button>();
        Button PreviousButton = popupEquipmentObject.transform.Find("Pagination/Previous").GetComponent<Button>();
        Button CloseButton = popupEquipmentObject.transform.Find("CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() => Destroy(popupEquipmentObject));
        Equipments equipments = new Equipments();
        List<Equipments> equipmentsList = new List<Equipments>();
        if (data is CardHeroes cardHeroes)
        {
            equipmentsList = await UserEquipmentsService.Create().GetAllCardHeroesEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardCaptains cardCaptains)
        {
            equipmentsList = await UserEquipmentsService.Create().GetAllCardCaptainsEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardColonels cardColonels)
        {
            equipmentsList = await UserEquipmentsService.Create().GetAllCardColonelsEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardGenerals cardGenerals)
        {
            equipmentsList = await UserEquipmentsService.Create().GetAllCardGeneralsEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardAdmirals cardAdmirals)
        {
            equipmentsList = await UserEquipmentsService.Create().GetAllCardAdmiralsEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardMonsters cardMonsters)
        {
            equipmentsList = await UserEquipmentsService.Create().GetAllCardMonstersEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            equipmentsList = await UserEquipmentsService.Create().GetAllCardMilitariesEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is CardSpells cardSpell)
        {
            equipmentsList = await UserEquipmentsService.Create().GetAllCardSpellsEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is Books books)
        {
            equipmentsList = await UserEquipmentsService.Create().GetAllBooksEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        else if (data is Pets pets)
        {
            equipmentsList = await UserEquipmentsService.Create().GetAllPetsEquipmentsAsync(User.CurrentUserId, mainType, pageSize, offset, statusToggle);
        }
        equipmentsList = equipmentsList.Where(e => e.Set == set).ToList();
        int totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, mainType, rare);
        totalPage = CalculateTotalPages(totalRecord, pageSize);

        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        CreatePopupEquipmentsUI(data, equipmentsList, contentPanel, position);
        NextButton.onClick.AddListener(async () => { await ChangeNextPageAsync(data, PageText, contentPanel, mainType, position); });
        PreviousButton.onClick.AddListener(async () => { await ChangePreviousPageAsync(data, PageText, contentPanel, mainType, position); });
    }
    public void CreatePopupEquipmentsUI(object data, List<Equipments> equipmentsList, Transform content, int position)
    {
        foreach (var equipment in equipmentsList)
        {
            GameObject equipmentObject = Instantiate(EquipmentsWearingPrefab, content);

            TextMeshProUGUI Title = equipmentObject.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            Title.text = equipment.Name.Replace("_", " ");

            TextMeshProUGUI Power = equipmentObject.transform.Find("PowerText").GetComponent<TextMeshProUGUI>();
            Power.text = equipment.Power.ToString();

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = equipment.Image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{equipment.Rare}");
            rareImage.texture = rareTexture;

            Button EquipButton = equipmentObject.transform.Find("EquipButton").GetComponent<Button>();
            EquipButton.onClick.AddListener(async () =>
            {
                Destroy(popupEquipmentObject);
                if (data is CardHeroes cardHeroes)
                {
                    await UserEquipmentsService.Create().InsertCardHeroEquipmentsAsync(cardHeroes.Id, equipment, position);
                    await CreateCardHeroesEquipmentsAsync(cardHeroes);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardCaptains cardCaptains)
                {
                    await UserEquipmentsService.Create().InsertCardCaptainEquipmentsAsync(cardCaptains.Id, equipment, position);
                    await CreateCardCaptainsEquipmentsAsync(cardCaptains);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardColonels cardColonels)
                {
                    await UserEquipmentsService.Create().InsertCardColonelEquipmentsAsync(cardColonels.Id, equipment, position);
                    await CreateCardColonelsEquipmentsAsync(cardColonels);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardGenerals cardGenerals)
                {
                    await UserEquipmentsService.Create().InsertCardGeneralEquipmentsAsync(cardGenerals.Id, equipment, position);
                    await CreateCardGeneralsEquipmentsAsync(cardGenerals);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardAdmirals cardAdmirals)
                {
                    await UserEquipmentsService.Create().InsertCardAdmiralEquipmentsAsync(cardAdmirals.Id, equipment, position);
                    await CreateCardAdmiralsEquipmentsAsync(cardAdmirals);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardMonsters cardMonsters)
                {
                    await UserEquipmentsService.Create().InsertCardMonsterEquipmentsAsync(cardMonsters.Id, equipment, position);
                    await CreateCardMonstersEquipmentsAsync(cardMonsters);
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
                else if (data is Books books)
                {
                    await UserEquipmentsService.Create().InsertBookEquipmentsAsync(books.Id, equipment, position);
                    await CreateBooksEquipmentsAsync(books);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is Pets pets)
                {
                    await UserEquipmentsService.Create().InsertPetEquipmentsAsync(pets.Id, equipment, position);
                    await CreatePetsEquipmentsAsync(pets);
                    double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }

                Destroy(popupEquipmentObject);
            });
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
            Close(content);
            int totalRecord = 0;

            if (data is CardHeroes cardHeroes)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardHeroesEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardCaptains cardCaptains)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardCaptainsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardColonels cardColonels)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardColonelsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardGenerals cardGenerals)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardGeneralsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardAdmirals cardAdmirals)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardAdmiralsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMonsters cardMonsters)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardMonstersEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMilitaries cardMilitary)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardMilitariesEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardSpells cardSpell)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardSpellsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Books books)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllBooksEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Pets pets)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
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
            Close(content);
            int totalRecord = 0;

            if (data is CardHeroes cardHeroes)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardHeroesEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardCaptains cardCaptains)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardCaptainsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardColonels cardColonels)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardColonelsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardGenerals cardGenerals)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardGeneralsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardAdmirals cardAdmirals)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardAdmiralsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMonsters cardMonsters)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardMonstersEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMilitaries cardMilitary)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardMilitariesEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardSpells cardSpell)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardSpellsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Books books)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllBooksEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Pets pets)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
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
