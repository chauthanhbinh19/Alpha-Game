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
    private GameObject buttonPrefab;
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
    private GameObject TabButton5;
    private GameObject StarPrefab;
    private RawImage mainImage;
    private string mainType;
    private int pageSize;
    private int offset;
    private int currentPage;
    private int totalPage;
    private string statusToggle;
    private string set;
    TeamsService teamsService;
    private string rare;
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
        rare = AppConstants.Rare.ALL;
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MainMenuEquipmentPanelPrefab = UIManager.Instance.Get("MainMenuEquipmentPanelPrefab");
        PopupEquipmentsPanelPrefab = UIManager.Instance.Get("PopupEquipmentsPanelPrefab");
        EquipmentsWearingPrefab = UIManager.Instance.Get("EquipmentsWearingPrefab");
        buttonPrefab = UIManager.Instance.Get("TabButton");
        Slot1Prefab = UIManager.Instance.Get("Slot1Prefab");
        Slot4Prefab = UIManager.Instance.Get("Slot4Prefab");
        Slot6Prefab = UIManager.Instance.Get("Slot6Prefab");
        Slot8Prefab = UIManager.Instance.Get("Slot8Prefab");
        Slot10Prefab = UIManager.Instance.Get("Slot10Prefab");
        Slot12Prefab = UIManager.Instance.Get("Slot12Prefab");
        Slot14Prefab = UIManager.Instance.Get("Slot14Prefab");
        Slot16Prefab = UIManager.Instance.Get("Slot16Prefab");
        TabButton5 = UIManager.Instance.Get("TabButton5");
        StarPrefab = UIManager.Instance.Get("StarPrefab");

        teamsService = TeamsService.Create();
    }
    public async Task CreateMainMenuEquipmentManagerAsync(object data)
    {
        currentObject = Instantiate(MainMenuEquipmentPanelPrefab, MainPanel);
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainMenuSet1.EQUIPMENTS);
        SetPanel = currentObject.transform.Find("DictionaryCards/SetGroup/Viewport/Content");
        mainImage = currentObject.transform.Find("DictionaryCards/CardImage").GetComponent<RawImage>();
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

        List<string> uniqueTypes = await EquipmentsService.Create().GetUniqueEquipmentsTypesAsync();
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                // Tạo một nút mới từ prefab
                string subtype = uniqueTypes[i];
                GameObject button = Instantiate(buttonPrefab, TabButtonPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
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
        _=CreateSetButtonAsync(data);
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
        _=CreateSetButtonAsync(data);
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
            Texture texture = Resources.Load<Texture>($"{image}");
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
                GameObject button = Instantiate(TabButton5, SetPanel);

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
    public async Task CreateCardHeroesEquipmentsAsync(CardHeroes cardHeroes)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardHeroesEquipmentsAsync(User.CurrentUserId, cardHeroes.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = cardHeroes.Image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardHeroes, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 16)
        {
            slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHeroes, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardCaptainsEquipmentsAsync(CardCaptains cardCaptains)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardCaptainsEquipmentsAsync(User.CurrentUserId, cardCaptains.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = cardCaptains.Image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardCaptains, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptains, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardColonelsEquipmentsAsync(CardColonels cardColonels)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardColonelsEquipmentsAsync(User.CurrentUserId, cardColonels.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = cardColonels.Image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardColonels, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonels, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardGeneralsEquipmentsAsync(CardGenerals cardGenerals)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardGeneralsEquipmentsAsync(User.CurrentUserId, cardGenerals.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = cardGenerals.Image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardGenerals, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGenerals, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardAdmiralsEquipmentsAsync(CardAdmirals cardAdmirals)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardAdmiralsEquipmentsAsync(User.CurrentUserId, cardAdmirals.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = cardAdmirals.Image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardAdmirals, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmirals, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardMonstersEquipmentsAsync(CardMonsters cardMonsters)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardMonstersEquipmentsAsync(User.CurrentUserId, cardMonsters.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = cardMonsters.Image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardMonsters, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonsters, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateCardMilitaryEquipmentsAsync(CardMilitaries cardMilitary)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetCardMilitariesEquipmentsAsync(User.CurrentUserId, cardMilitary.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = cardMilitary.Image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardMilitary, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
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
        string fileNameWithoutExtension = cardSpell.Image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardSpell, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreateBooksEquipmentsAsync(Books books)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetBooksEquipmentsAsync(User.CurrentUserId, books.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = books.Image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(books, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(books, slotButtons[i], i + 1, equipmentList);
            }
        }
    }
    public async Task CreatePetsEquipmentsAsync(Pets pets)
    {
        Close(SlotPanel);

        List<Equipments> equipmentList = new List<Equipments>();
        equipmentList = await UserEquipmentsService.Create().GetPetsEquipmentsAsync(User.CurrentUserId, pets.Id, mainType);
        equipmentList = equipmentList.Where(e => e.Set == set).ToList();
        string fileNameWithoutExtension = pets.Image.Replace(".png", "");
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        mainImage.texture = texture;
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            mainImage.gameObject.SetActive(false);
        }
        else
        {
            mainImage.gameObject.SetActive(true);
        }
        if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 1)
        {
            slotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = slotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(pets, EquipmentSlot1Button, 1, equipmentList);
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 4)
        {
            slotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 6)
        {
            slotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 8)
        {
            slotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 10)
        {
            slotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 12)
        {
            slotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 14)
        {
            slotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList);
            }
        }
        else if (EvaluateSlotForEquipment.CheckSlotForEquipments(mainType) == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EvaluateSlotForEquipment.CheckSlotForEquipments(mainType));
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pets, slotButtons[i], i + 1, equipmentList);
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
                string fileNameWithoutExtension = equipment.Image.Replace(".png", "").Replace(".jpg", "");
                Texture equipmentTexture = Resources.Load<Texture>(fileNameWithoutExtension);

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
                        if (EvaluateSlotForEquipment.CanUseBorderEffect(mainType))
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
        int totalRecord = await  UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, mainType, rare);
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
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.Rare}");
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
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardHeroesEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardCaptains cardCaptains)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardCaptainsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardColonels cardColonels)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardColonelsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardGenerals cardGenerals)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardGeneralsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardAdmirals cardAdmirals)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardAdmiralsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMonsters cardMonsters)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardMonstersEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMilitaries cardMilitary)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardMilitariesEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardSpells cardSpell)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardSpellsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Books books)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllBooksEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Pets pets)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
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
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardHeroesEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardCaptains cardCaptains)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardCaptainsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardColonels cardColonels)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardColonelsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardGenerals cardGenerals)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardGeneralsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardAdmirals cardAdmirals)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardAdmiralsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMonsters cardMonsters)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardMonstersEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardMilitaries cardMilitary)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardMilitariesEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is CardSpells cardSpell)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllCardSpellsEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Books books)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = await UserEquipmentsService.Create().GetAllBooksEquipmentsAsync(User.CurrentUserId, subType, pageSize, offset, statusToggle);
                CreatePopupEquipmentsUI(data, equipments, content, position);
            }
            else if (data is Pets pets)
            {
                totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, subType, rare);
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
    public void LoadAnimation()
    {
        TabButtonPanel.gameObject.AddComponent<SlideLeftToRightAnimation>();
    }
}
