using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Bson;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuEquipmentManager : MonoBehaviour
{
    public static MainMenuEquipmentManager Instance { get; private set; }
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private Transform SetPanel;
    private GameObject EquipmentTabButtonPrefab;
    private GameObject EquipmentSetButtonPrefab;
    private GameObject EquipmentDetailButtonPrefab;
    private GameObject TypeButtonPrefab;
    private GameObject MainMenuEquipmentPanelPrefab;
    private GameObject PopupEquipmentsPanelPrefab;
    private GameObject EquipmentsWearingPrefab;
    private GameObject CurrentObject;
    private GameObject SlotObject;
    private GameObject Slot1Prefab;
    private GameObject Slot4Prefab;
    private GameObject Slot6Prefab;
    private GameObject Slot8Prefab;
    private GameObject Slot10Prefab;
    private GameObject Slot12Prefab;
    private GameObject Slot14Prefab;
    private GameObject Slot16Prefab;
    private GameObject PopupEquipmentObject;
    private GameObject SetButtonPrefab;
    private GameObject StarPrefab;
    private Button EquipOneTypeButton;
    private Button EquipAllTypeButton;
    private RawImage MainImage;
    private PaginationManager PaginationManager;
    private string MainType;
    private const int PAGE_SIZE = 100;
    private int Offset = 0;
    private int CurrentPage = 1;
    private int TotalItems;
    private int TotalPage;
    private string StatusToggle;
    private string Set;
    private string Search = "";
    private string Type = AppConstants.Type.ALL;
    private string Rare = AppConstants.Rare.ALL;
    private bool IsSearchingOrFiltering = false;
    EquipmentType EquipmentType;

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
        // type = AppConstants.Type.ALL;
        Rare = AppConstants.Rare.ALL;
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
        EquipmentTabButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.EQUIPMENT_TAB_BUTTON_PREFAB);
        EquipmentSetButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.EQUIPMENT_SET_BUTTON_PREFAB);
        EquipmentDetailButtonPrefab = UIManager.Instance.Get(AppConstants.Prefab.Component.EQUIPMENT_DETAIL_BUTTON_PREFAB);
    }
    public async Task CreateMainMenuEquipmentManagerAsync(object data)
    {
        GameObject topupPanelObject = Instantiate(MainMenuEquipmentPanelPrefab, MainPanel);
        Transform transform = topupPanelObject.transform;
        Transform tabButtonTransform = transform.Find("Tab Scroll View/Viewport/Content");
        Transform setButtonTransform = transform.Find("Set Scroll View/Viewport/Content");
        Transform contentTransform = transform.Find("Scroll View/Viewport/Content");
        TextMeshProUGUI titleText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(AppDisplayConstants.Title.SHOP_PACKAGE);
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(topupPanelObject);
        });
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });

        List<string> typeList = await EquipmentsService.Create().GetUniqueEquipmentsTypesAsync();
        // Danh sách lưu trữ các UI Tab để quản lý toggle trạng thái Selected / Default
        List<(GameObject defaultObj, GameObject selectedObj)> tabUIList = new List<(GameObject, GameObject)>();

        for (int i = 0; i < typeList.Count; i++)
        {
            string type = typeList[i];

            // Instantiate tab button và đặt parent vào Content của Tab Scroll View
            GameObject topupTabButtonObject = Instantiate(EquipmentTabButtonPrefab, tabButtonTransform);

            GameObject defaultObj = topupTabButtonObject.transform.Find("Default").gameObject;
            GameObject selectedObj = topupTabButtonObject.transform.Find("Selected").gameObject;

            TextMeshProUGUI titleText1 = defaultObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI titleText2 = selectedObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();

            titleText1.text = type.Replace("_", " ");
            titleText2.text = type.Replace("_", " ");

            // Trạng thái mặc định: Phần tử đầu tiên (i == 0) sẽ Active Selected, còn lại Active Default
            bool isFirst = (i == 0);
            defaultObj.SetActive(!isFirst);
            selectedObj.SetActive(isFirst);

            tabUIList.Add((defaultObj, selectedObj));

            // Đăng ký sự kiện Click cho Tab Button
            Button tabBtn = topupTabButtonObject.GetComponent<Button>();
            tabBtn.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND_2);

                // 1. Chuyển tất cả các Tab về dạng Default
                foreach (var tabUI in tabUIList)
                {
                    tabUI.defaultObj.SetActive(true);
                    tabUI.selectedObj.SetActive(false);
                }

                // 2. Bật dạng Selected cho Tab vừa được click
                defaultObj.SetActive(false);
                selectedObj.SetActive(true);

                // TODO: Gọi hàm load/filter danh sách gói nạp theo category này vào contentTransform
                await CreateSetButtonAsync(data, type, setButtonTransform, contentTransform);
            });
        }

        // Load gói nạp của Tab đầu tiên nếu có danh mục
        if (typeList.Count > 0)
        {
            await CreateSetButtonAsync(data, typeList[0], setButtonTransform, contentTransform);
        }
    }
    public async Task CreateSetButtonAsync(object data, string type, Transform setButtonTransform, Transform contentTransform)
    {
        // Dọn dẹp các item cũ trong ScrollView
        for (int i = setButtonTransform.childCount - 1; i >= 0; i--)
        {
            Transform child = setButtonTransform.GetChild(i);
            child.SetParent(null);
            Destroy(child.gameObject);
        }

        List<string> setList = await EquipmentsService.Create().GetEquipmentsSetAsync(type);
        setList = setList
            .OrderBy(x => int.Parse(x.Replace("set", "")))
            .ToList();
        // Danh sách lưu trữ các UI Tab để quản lý toggle trạng thái Selected / Default
        List<(GameObject defaultObj, GameObject selectedObj)> tabUIList = new List<(GameObject, GameObject)>();

        for (int i = 0; i < setList.Count; i++)
        {
            string set = setList[i];

            // Instantiate tab button và đặt parent vào Content của Tab Scroll View
            GameObject topupTabButtonObject = Instantiate(EquipmentSetButtonPrefab, setButtonTransform);

            GameObject defaultObj = topupTabButtonObject.transform.Find("Default").gameObject;
            GameObject selectedObj = topupTabButtonObject.transform.Find("Selected").gameObject;

            TextMeshProUGUI titleText1 = defaultObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI titleText2 = selectedObj.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();

            string setNumber = set.Replace("set", "");

            titleText1.text = setNumber;
            titleText2.text = setNumber;

            // Trạng thái mặc định: Phần tử đầu tiên (i == 0) sẽ Active Selected, còn lại Active Default
            bool isFirst = (i == 0);
            defaultObj.SetActive(!isFirst);
            selectedObj.SetActive(isFirst);

            tabUIList.Add((defaultObj, selectedObj));

            // Đăng ký sự kiện Click cho Tab Button
            Button tabBtn = topupTabButtonObject.GetComponent<Button>();
            tabBtn.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND_2);

                // 1. Chuyển tất cả các Tab về dạng Default
                foreach (var tabUI in tabUIList)
                {
                    tabUI.defaultObj.SetActive(true);
                    tabUI.selectedObj.SetActive(false);
                }

                // 2. Bật dạng Selected cho Tab vừa được click
                defaultObj.SetActive(false);
                selectedObj.SetActive(true);

                // TODO: Gọi hàm load/filter danh sách gói nạp theo category này vào contentTransform
                await LoadPackagesByCategoryAsync(data, type, set, contentTransform);
            });
        }

        // Load gói nạp của Tab đầu tiên nếu có danh mục
        if (setList.Count > 0)
        {
            await LoadPackagesByCategoryAsync(data, type, setList[0], contentTransform);
        }
    }
    public async Task LoadPackagesByCategoryAsync(object data, string type, string set, Transform contentTransform)
    {
        // Dọn dẹp các item cũ trong ScrollView
        for (int i = contentTransform.childCount - 1; i >= 0; i--)
        {
            Transform child = contentTransform.GetChild(i);
            child.SetParent(null);
            Destroy(child.gameObject);
        }

        EquipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(type);

        if (EquipmentType == null)
        {
            return;
        }

        List<Equipments> equipments = new List<Equipments>();

        if (data is CardHeroes cardHero)
        {
            equipments = await UserEquipmentsService.Create().GetUserCardHeroesEquipmentsAsync(User.CurrentUserId, cardHero.Id, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is Books book)
        {
            equipments = await UserEquipmentsService.Create().GetUserBooksEquipmentsAsync(User.CurrentUserId, book.Id, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardCaptains cardCaptain)
        {
            equipments = await UserEquipmentsService.Create().GetUserCardCaptainsEquipmentsAsync(User.CurrentUserId, cardCaptain.Id, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is Pets pet)
        {
            equipments = await UserEquipmentsService.Create().GetUserPetsEquipmentsAsync(User.CurrentUserId, pet.Id, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardMilitaries cardMilitary)
        {
            equipments = await UserEquipmentsService.Create().GetUserCardMilitariesEquipmentsAsync(User.CurrentUserId, cardMilitary.Id, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardSpells cardSpell)
        {
            equipments = await UserEquipmentsService.Create().GetUserCardSpellsEquipmentsAsync(User.CurrentUserId, cardSpell.Id, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardMonsters cardMonster)
        {
            equipments = await UserEquipmentsService.Create().GetUserCardMonstersEquipmentsAsync(User.CurrentUserId, cardMonster.Id, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardColonels cardColonel)
        {
            equipments = await UserEquipmentsService.Create().GetUserCardColonelsEquipmentsAsync(User.CurrentUserId, cardColonel.Id, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardGenerals cardGeneral)
        {
            equipments = await UserEquipmentsService.Create().GetUserCardGeneralsEquipmentsAsync(User.CurrentUserId, cardGeneral.Id, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            equipments = await UserEquipmentsService.Create().GetUserCardAdmiralsEquipmentsAsync(User.CurrentUserId, cardAdmiral.Id, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }

        for (int i = 1; i <= EquipmentType.SlotValue; i++)
        {
            GameObject equipmentDetailButtonObject = Instantiate(EquipmentDetailButtonPrefab, contentTransform);
            RawImage image = equipmentDetailButtonObject.transform.Find("Image").GetComponent<RawImage>();
            TextMeshProUGUI titleText = equipmentDetailButtonObject.transform.Find("Content/TitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI typeText = equipmentDetailButtonObject.transform.Find("Content/TypeText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nameText = equipmentDetailButtonObject.transform.Find("Content/NameText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI rareTitleText = equipmentDetailButtonObject.transform.Find("Content/RareTitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI rareText = equipmentDetailButtonObject.transform.Find("Content/RareText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI lockTitleText = equipmentDetailButtonObject.transform.Find("Content/LockTitleText").GetComponent<TextMeshProUGUI>();
            RawImage lockImage = equipmentDetailButtonObject.transform.Find("Content/LockImage").GetComponent<RawImage>();
            TextMeshProUGUI starTitleText = equipmentDetailButtonObject.transform.Find("Content/StarTitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI starText = equipmentDetailButtonObject.transform.Find("Content/StarText").GetComponent<TextMeshProUGUI>();
            Transform starTransform = equipmentDetailButtonObject.transform.Find("Content/Star");
            TextMeshProUGUI powerTitleText = equipmentDetailButtonObject.transform.Find("Content/PowerTitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI powerText = equipmentDetailButtonObject.transform.Find("Content/PowerText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI setTitleText = equipmentDetailButtonObject.transform.Find("Content/SetTitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI setText = equipmentDetailButtonObject.transform.Find("Content/SetText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI positionTitleText = equipmentDetailButtonObject.transform.Find("Content/PositionTitleText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI positionText = equipmentDetailButtonObject.transform.Find("Content/PositionText").GetComponent<TextMeshProUGUI>();
            Button addButton = equipmentDetailButtonObject.transform.Find("Content/AddButton").GetComponent<Button>();
            Button changeButton = equipmentDetailButtonObject.transform.Find("Content/ChangeButton").GetComponent<Button>();
            Button removeButton = equipmentDetailButtonObject.transform.Find("Content/RemoveButton").GetComponent<Button>();
            // if()
            rareTitleText.text = LocalizationManager.Get(AppDisplayConstants.Title.RARE);
            lockTitleText.text = LocalizationManager.Get(AppDisplayConstants.Title.LOCK);
            starTitleText.text = LocalizationManager.Get(AppDisplayConstants.Title.STAR);
            powerTitleText.text = LocalizationManager.Get(AppDisplayConstants.Title.POWER);
            setTitleText.text = LocalizationManager.Get(AppDisplayConstants.Title.SET);
            positionTitleText.text = LocalizationManager.Get(AppDisplayConstants.Title.POSITION);

            var equipment = equipments.FirstOrDefault(x => x.Position == i);
            positionText.text = i.ToString();
            setText.text = set.Replace("set", "");

            if (equipment != null)
            {
                // Có equipment
                typeText.text = equipment.Type;
                nameText.text = equipment.Name;
                rareText.text = equipment.Rarity;
                powerText.text = NumberFormatterHelper.FormatNumber(equipment.Power);
                starText.text = equipment.Star.ToString();

                image.texture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(equipment.Image));

                if (equipment.Block > 0)
                {
                    lockImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Main.PADLOCK_UNLOCK_URL);
                }
                else
                {
                    lockImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Main.PADLOCK_URL);
                }

                addButton.gameObject.SetActive(false);
                changeButton.gameObject.SetActive(true);
                removeButton.gameObject.SetActive(true);

                changeButton.onClick.AddListener(() =>
                {
                    
                });

                removeButton.onClick.AddListener(() =>
                {
                    
                });
            }
            else
            {
                // Không có equipment
                typeText.text = "NULL";
                nameText.text = "NULL";
                rareText.text = "NULL";
                powerText.text = "0000-0000-0000-0000";
                starText.text = "0";

                image.texture = TextureHelper.LoadTextureCached("UI/Icon/new-page");

                lockImage.texture = TextureHelper.LoadTextureCached(ImageConstants.Main.PADLOCK_URL);

                addButton.gameObject.SetActive(true);
                changeButton.gameObject.SetActive(false);
                removeButton.gameObject.SetActive(false);

                addButton.onClick.AddListener(() =>
                {
                    
                });
            }
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
        List<string> uniqueSet = await EquipmentsService.Create().GetEquipmentsSetAsync(MainType);
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
                    Set = subtype;
                    ChangeButtonBackground(button, ImageConstants.Button.SET_BUTTON_AFTER_CLICK_URL);
                }
                else
                {
                    ChangeButtonBackground(button, ImageConstants.Button.SET_BUTTON_BEFORE_CLICK_URL);
                }
            }
        }
    }
    public async Task OnSetButtonClickAsync(GameObject clickedButton, object data, string type)
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

        Set = type;
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
        equipments = await UserEquipmentsService.Create().GetUserCardHeroesEquipmentsAsync(User.CurrentUserId, cardHero.Id, MainType);
        equipments = equipments.Where(e => e.Set == Set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardHero.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        MainImage.texture = texture;
        EquipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(MainType);
        if (EquipmentType.SlotValue == 1)
        {
            MainImage.gameObject.SetActive(false);
        }
        else
        {
            MainImage.gameObject.SetActive(true);
        }
        if (EquipmentType.SlotValue == 1)
        {
            SlotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardHero, EquipmentSlot1Button, 1, equipments);
        }
        else if (EquipmentType.SlotValue == 4)
        {
            SlotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EquipmentType.SlotValue == 6)
        {
            SlotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 8)
        {
            SlotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 10)
        {
            SlotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 12)
        {
            SlotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 14)
        {
            SlotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardHero, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 16)
        {
            SlotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
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
        equipments = await UserEquipmentsService.Create().GetUserCardCaptainsEquipmentsAsync(User.CurrentUserId, cardCaptain.Id, MainType);
        equipments = equipments.Where(e => e.Set == Set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardCaptain.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        MainImage.texture = texture;
        EquipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(MainType);
        if (EquipmentType.SlotValue == 1)
        {
            MainImage.gameObject.SetActive(false);
        }
        else
        {
            MainImage.gameObject.SetActive(true);
        }
        if (EquipmentType.SlotValue == 1)
        {
            SlotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardCaptain, EquipmentSlot1Button, 1, equipments);
        }
        else if (EquipmentType.SlotValue == 4)
        {
            SlotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EquipmentType.SlotValue == 6)
        {
            SlotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 8)
        {
            SlotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 10)
        {
            SlotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 12)
        {
            SlotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 14)
        {
            SlotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardCaptain, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
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
        equipments = await UserEquipmentsService.Create().GetUserCardColonelsEquipmentsAsync(User.CurrentUserId, cardColonel.Id, MainType);
        equipments = equipments.Where(e => e.Set == Set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardColonel.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        MainImage.texture = texture;
        EquipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(MainType);
        if (EquipmentType.SlotValue == 1)
        {
            MainImage.gameObject.SetActive(false);
        }
        else
        {
            MainImage.gameObject.SetActive(true);
        }
        if (EquipmentType.SlotValue == 1)
        {
            SlotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardColonel, EquipmentSlot1Button, 1, equipments);
        }
        else if (EquipmentType.SlotValue == 4)
        {
            SlotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EquipmentType.SlotValue == 6)
        {
            SlotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 8)
        {
            SlotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 10)
        {
            SlotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 12)
        {
            SlotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 14)
        {
            SlotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardColonel, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
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
        equipments = await UserEquipmentsService.Create().GetUserCardGeneralsEquipmentsAsync(User.CurrentUserId, cardGeneral.Id, MainType);
        equipments = equipments.Where(e => e.Set == Set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardGeneral.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        MainImage.texture = texture;
        EquipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(MainType);
        if (EquipmentType.SlotValue == 1)
        {
            MainImage.gameObject.SetActive(false);
        }
        else
        {
            MainImage.gameObject.SetActive(true);
        }
        if (EquipmentType.SlotValue == 1)
        {
            SlotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardGeneral, EquipmentSlot1Button, 1, equipments);
        }
        else if (EquipmentType.SlotValue == 4)
        {
            SlotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EquipmentType.SlotValue == 6)
        {
            SlotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 8)
        {
            SlotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 10)
        {
            SlotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 12)
        {
            SlotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 14)
        {
            SlotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardGeneral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
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
        equipments = await UserEquipmentsService.Create().GetUserCardAdmiralsEquipmentsAsync(User.CurrentUserId, cardAdmiral.Id, MainType);
        equipments = equipments.Where(e => e.Set == Set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardAdmiral.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        MainImage.texture = texture;
        EquipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(MainType);
        if (EquipmentType.SlotValue == 1)
        {
            MainImage.gameObject.SetActive(false);
        }
        else
        {
            MainImage.gameObject.SetActive(true);
        }
        if (EquipmentType.SlotValue == 1)
        {
            SlotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardAdmiral, EquipmentSlot1Button, 1, equipments);
        }
        else if (EquipmentType.SlotValue == 4)
        {
            SlotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EquipmentType.SlotValue == 6)
        {
            SlotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 8)
        {
            SlotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 10)
        {
            SlotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 12)
        {
            SlotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 14)
        {
            SlotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardAdmiral, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
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
        equipments = await UserEquipmentsService.Create().GetUserCardMonstersEquipmentsAsync(User.CurrentUserId, cardMonster.Id, MainType);
        equipments = equipments.Where(e => e.Set == Set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardMonster.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        MainImage.texture = texture;
        EquipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(MainType);
        if (EquipmentType.SlotValue == 1)
        {
            MainImage.gameObject.SetActive(false);
        }
        else
        {
            MainImage.gameObject.SetActive(true);
        }
        if (EquipmentType.SlotValue == 1)
        {
            SlotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardMonster, EquipmentSlot1Button, 1, equipments);
        }
        else if (EquipmentType.SlotValue == 4)
        {
            SlotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EquipmentType.SlotValue == 6)
        {
            SlotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 8)
        {
            SlotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 10)
        {
            SlotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 12)
        {
            SlotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 14)
        {
            SlotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMonster, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
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
        equipments = await UserEquipmentsService.Create().GetUserCardMilitariesEquipmentsAsync(User.CurrentUserId, cardMilitary.Id, MainType);
        equipments = equipments.Where(e => e.Set == Set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardMilitary.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        MainImage.texture = texture;
        EquipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(MainType);
        if (EquipmentType.SlotValue == 1)
        {
            MainImage.gameObject.SetActive(false);
        }
        else
        {
            MainImage.gameObject.SetActive(true);
        }
        if (EquipmentType.SlotValue == 1)
        {
            SlotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardMilitary, EquipmentSlot1Button, 1, equipments);
        }
        else if (EquipmentType.SlotValue == 4)
        {
            SlotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EquipmentType.SlotValue == 6)
        {
            SlotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 8)
        {
            SlotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 10)
        {
            SlotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 12)
        {
            SlotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 14)
        {
            SlotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardMilitary, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
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
        equipments = await UserEquipmentsService.Create().GetUserCardSpellsEquipmentsAsync(User.CurrentUserId, cardSpell.Id, MainType);
        equipments = equipments.Where(e => e.Set == Set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(cardSpell.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        MainImage.texture = texture;
        EquipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(MainType);
        if (EquipmentType.SlotValue == 1)
        {
            MainImage.gameObject.SetActive(false);
        }
        else
        {
            MainImage.gameObject.SetActive(true);
        }
        if (EquipmentType.SlotValue == 1)
        {
            SlotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(cardSpell, EquipmentSlot1Button, 1, equipments);
        }
        else if (EquipmentType.SlotValue == 4)
        {
            SlotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EquipmentType.SlotValue == 6)
        {
            SlotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 8)
        {
            SlotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 10)
        {
            SlotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 12)
        {
            SlotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 14)
        {
            SlotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(cardSpell, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
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
        equipments = await UserEquipmentsService.Create().GetUserBooksEquipmentsAsync(User.CurrentUserId, book.Id, MainType);
        equipments = equipments.Where(e => e.Set == Set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(book.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        MainImage.texture = texture;
        EquipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(MainType);
        if (EquipmentType.SlotValue == 1)
        {
            MainImage.gameObject.SetActive(false);
        }
        else
        {
            MainImage.gameObject.SetActive(true);
        }
        if (EquipmentType.SlotValue == 1)
        {
            SlotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(book, EquipmentSlot1Button, 1, equipments);
        }
        else if (EquipmentType.SlotValue == 4)
        {
            SlotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EquipmentType.SlotValue == 6)
        {
            SlotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 8)
        {
            SlotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 10)
        {
            SlotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 12)
        {
            SlotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 14)
        {
            SlotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(book, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
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
        equipments = await UserEquipmentsService.Create().GetUserPetsEquipmentsAsync(User.CurrentUserId, pet.Id, MainType);
        equipments = equipments.Where(e => e.Set == Set).ToList();
        string fileNameWithoutExtension = ImageHelper.RemoveImageExtension(pet.Image);
        Texture texture = TextureHelper.LoadTextureCached($"{fileNameWithoutExtension}");
        MainImage.texture = texture;
        EquipmentType = await EquipmentTypeService.Create().GetEquipmentTypeByNameAsync(MainType);
        if (EquipmentType.SlotValue == 1)
        {
            MainImage.gameObject.SetActive(false);
        }
        else
        {
            MainImage.gameObject.SetActive(true);
        }
        if (EquipmentType.SlotValue == 1)
        {
            SlotObject = Instantiate(Slot1Prefab, SlotPanel);
            Button EquipmentSlot1Button = SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>();
            ApplyEquipmentImage(pet, EquipmentSlot1Button, 1, equipments);
        }
        else if (EquipmentType.SlotValue == 4)
        {
            SlotObject = Instantiate(Slot4Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments); // i + 1 vì vị trí bắt đầu từ 1
            }
        }
        else if (EquipmentType.SlotValue == 6)
        {
            SlotObject = Instantiate(Slot6Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 8)
        {
            SlotObject = Instantiate(Slot8Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 10)
        {
            SlotObject = Instantiate(Slot10Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 12)
        {
            SlotObject = Instantiate(Slot12Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 14)
        {
            SlotObject = Instantiate(Slot14Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
            // Duyệt danh sách thiết bị và áp hình ảnh
            for (int i = 0; i < slotButtons.Length; i++)
            {
                ApplyEquipmentImage(pet, slotButtons[i], i + 1, equipments);
            }
        }
        else if (EquipmentType.SlotValue == 16)
        {
            GameObject slotObject = Instantiate(Slot16Prefab, SlotPanel);
            Button[] slotButtons = CreateButtonArray(EquipmentType.SlotValue);
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
                SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>()
            };
            return slotButtons;
        }
        else if (numberOfSlot == 6)
        {
            slotButtons = new Button[]
            {
                SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
            };
            return slotButtons;
        }
        else if (numberOfSlot == 8)
        {
            slotButtons = new Button[]
            {
                SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>()
            };
            return slotButtons;
        }
        else if (numberOfSlot == 10)
        {
            slotButtons = new Button[]
            {
                SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
            };
            return slotButtons;
        }
        else if (numberOfSlot == 12)
        {
            slotButtons = new Button[]
            {
                SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
            };
            return slotButtons;
        }
        else if (numberOfSlot == 14)
        {
            slotButtons = new Button[]
            {
                SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
            };
            return slotButtons;
        }
        else if (numberOfSlot == 16)
        {
            slotButtons = new Button[]
            {
                SlotObject.transform.Find("EquipmentSlot1Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot2Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot3Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot4Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot5Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot6Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot7Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot8Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot9Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot10Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot11Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot12Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot13Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot14Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot15Button").GetComponent<Button>(),
                SlotObject.transform.Find("EquipmentSlot16Button").GetComponent<Button>(),
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
                        if (EquipmentType.CanUseBorderEffect)
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
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                // MainMenuDetailsManager.Instance.PopupDetails(tempEquip, MainPanel);
            });
        }
    }
    public async Task CreatePopupEquipmentsAsync(object data, int position, string statusToggle = "NOT EQUIP")
    {
        PopupEquipmentObject = Instantiate(PopupEquipmentsPanelPrefab, MainPanel);
        Transform contentPanel = PopupEquipmentObject.transform.Find("Scroll View/Viewport/Content");
        Text PageText = PopupEquipmentObject.transform.Find("Pagination/Page").GetComponent<Text>();
        Toggle toggle = PopupEquipmentObject.transform.Find("Toggle").GetComponent<Toggle>();
        toggle.isOn = (statusToggle == "ALL");
        toggle.onValueChanged.AddListener(async (bool isOn) =>
        {
            string newStatusToggle = isOn ? "ALL" : "NOT EQUIP";
            Destroy(PopupEquipmentObject);
            await CreatePopupEquipmentsAsync(data, position, newStatusToggle); // Gọi lại nhưng giữ statusToggle mới
        });
        Button closeButton = PopupEquipmentObject.transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => Destroy(PopupEquipmentObject));

        PaginationManager = transform.Find("PaginationPanelPrefab").GetComponent<PaginationManager>();

        
        // equipments = equipments.Where(e => e.Set == Set).ToList();
        // int totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, Search, MainType, Rare);
        // TotalPage = PageHelper.CalculateTotalPages(totalRecord, PAGE_SIZE);

        PageText.text = CurrentPage.ToString() + "/" + TotalPage.ToString();
        // CreatePopupEquipmentsUI(data, equipments, contentPanel, position);
    }
    public async Task LoadCurrentPageAsync(object data)
    {
        int totalRecord = 0;
        List<Equipments> equipments = new List<Equipments>();
        if (data is CardHeroes cardHero)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardHeroesEquipmentsAsync(User.CurrentUserId, MainType, PAGE_SIZE, Offset, "statusToggle");

            totalRecord = await UserEquipmentsService.Create().GetUserCardHeroesEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);
        }
        else if (data is CardCaptains cardCaptain)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardCaptainsEquipmentsAsync(User.CurrentUserId, MainType, PAGE_SIZE, Offset, "statusToggle");

            totalRecord = await UserEquipmentsService.Create().GetUserCardCaptainsEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);
        }
        else if (data is CardColonels cardColonel)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardColonelsEquipmentsAsync(User.CurrentUserId, MainType, PAGE_SIZE, Offset, "statusToggle");

            totalRecord = await UserEquipmentsService.Create().GetUserCardColonelsEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);
        }
        else if (data is CardGenerals cardGeneral)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardGeneralsEquipmentsAsync(User.CurrentUserId, MainType, PAGE_SIZE, Offset, "statusToggle");

            totalRecord = await UserEquipmentsService.Create().GetUserCardGeneralsEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardAdmiralsEquipmentsAsync(User.CurrentUserId, MainType, PAGE_SIZE, Offset, "statusToggle");

            totalRecord = await UserEquipmentsService.Create().GetUserCardAdmiralsEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);
        }
        else if (data is CardMonsters cardMonster)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardMonstersEquipmentsAsync(User.CurrentUserId, MainType, PAGE_SIZE, Offset, "statusToggle");

            totalRecord = await UserEquipmentsService.Create().GetUserCardMonstersEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);
        }
        else if (data is CardMilitaries cardMilitary)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardMilitariesEquipmentsAsync(User.CurrentUserId, MainType, PAGE_SIZE, Offset, "statusToggle");

            totalRecord = await UserEquipmentsService.Create().GetUserCardMilitariesEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);
        }
        else if (data is CardSpells cardSpell)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardSpellsEquipmentsAsync(User.CurrentUserId, MainType, PAGE_SIZE, Offset, "statusToggle");

            totalRecord = await UserEquipmentsService.Create().GetUserCardSpellsEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);
        }
        else if (data is Books book)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserBooksEquipmentsAsync(User.CurrentUserId, MainType, PAGE_SIZE, Offset, "statusToggle");

            totalRecord = await UserEquipmentsService.Create().GetUserBooksEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);
        }
        else if (data is Pets pet)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserPetsEquipmentsAsync(User.CurrentUserId, MainType, PAGE_SIZE, Offset, "statusToggle");

            totalRecord = await UserEquipmentsService.Create().GetUserPetsEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);
        }

        TotalItems = totalRecord;

        if (IsSearchingOrFiltering && PaginationManager != null)
        {
            // Tạm thời gỡ sự kiện để việc Init không kích hoạt ngược lại hàm Load lần nữa
            PaginationManager.OnPageChanged -= OnPageSelected;

            // Vẽ lại dải nút số dựa trên TotalItems mới sau khi đã Lọc/Search
            // Luôn ép về Trang 1 vì mỗi lần Search/Filter là tính lại từ đầu
            PaginationManager.InitPagination(TotalItems, PAGE_SIZE, CurrentPage);

            // Đăng ký lại sự kiện sau khi Init đã hoàn tất sạch sẽ
            PaginationManager.OnPageChanged += OnPageSelected;
        }
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
            Texture rareTexture = TextureHelper.LoadTextureCached($"UI/UI/{equipment.Rarity}");
            rareImage.texture = rareTexture;

            Button equipButton = equipmentObject.transform.Find("EquipButton").GetComponent<Button>();
            equipButton.onClick.AddListener((UnityEngine.Events.UnityAction)(async () =>
            {
                Destroy(PopupEquipmentObject);
                if (data is CardHeroes cardHero)
                {
                    await UserEquipmentsService.Create().InsertUserCardHeroEquipmentsAsync(User.CurrentUserId, (string)cardHero.Id, equipment, position);
                    await CreateCardHeroesEquipmentsAsync(cardHero);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardCaptains cardCaptain)
                {
                    await UserEquipmentsService.Create().InsertUserCardCaptainEquipmentsAsync(User.CurrentUserId, cardCaptain.Id, equipment, position);
                    await CreateCardCaptainsEquipmentsAsync(cardCaptain);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardColonels cardColonel)
                {
                    await UserEquipmentsService.Create().InsertUserCardColonelEquipmentsAsync(User.CurrentUserId, cardColonel.Id, equipment, position);
                    await CreateCardColonelsEquipmentsAsync(cardColonel);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardGenerals cardGeneral)
                {
                    await UserEquipmentsService.Create().InsertUserCardGeneralEquipmentsAsync(User.CurrentUserId, cardGeneral.Id, equipment, position);
                    await CreateCardGeneralsEquipmentsAsync(cardGeneral);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardAdmirals cardAdmiral)
                {
                    await UserEquipmentsService.Create().InsertUserCardAdmiralEquipmentsAsync(User.CurrentUserId, cardAdmiral.Id, equipment, position);
                    await CreateCardAdmiralsEquipmentsAsync(cardAdmiral);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardMonsters cardMonster)
                {
                    await UserEquipmentsService.Create().InsertUserCardMonsterEquipmentsAsync(User.CurrentUserId, cardMonster.Id, equipment, position);
                    await CreateCardMonstersEquipmentsAsync(cardMonster);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardMilitaries cardMilitary)
                {
                    await UserEquipmentsService.Create().InsertUserCardMilitaryEquipmentsAsync(User.CurrentUserId, cardMilitary.Id, equipment, position);
                    await CreateCardMilitaryEquipmentsAsync(cardMilitary);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardSpells cardSpell)
                {
                    await UserEquipmentsService.Create().InsertUserCardSpellEquipmentsAsync(User.CurrentUserId, cardSpell.Id, equipment, position);
                    await CreateCardSpellEquipmentsAsync(cardSpell);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is Books book)
                {
                    await UserEquipmentsService.Create().InsertUserBookEquipmentsAsync(User.CurrentUserId, book.Id, equipment, position);
                    await CreateBooksEquipmentsAsync(book);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is Pets pet)
                {
                    await UserEquipmentsService.Create().InsertUserPetEquipmentsAsync(User.CurrentUserId, pet.Id, equipment, position);
                    await CreatePetsEquipmentsAsync(pet);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                }

                Destroy(PopupEquipmentObject);
            }));
        }
        GridLayoutGroup gridLayout = content.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.cellSize = new Vector2(340, 130);
        }
    }
    private void OnPageSelected(int pageNumber)
    {
        CurrentPage = pageNumber;
        Offset = (CurrentPage - 1) * PAGE_SIZE;
        IsSearchingOrFiltering = false;
        // _ = LoadCurrentPageAsync();
    }

    private void OnDestroy()
    {
        // Luôn luôn hủy đăng ký sự kiện khi Object bị xóa để tránh lỗi bộ nhớ
        if (PaginationManager != null)
        {
            PaginationManager.OnPageChanged -= OnPageSelected;
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
