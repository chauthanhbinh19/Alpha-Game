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
    private Transform ContentTransform;
    TextMeshProUGUI TotalSlotText;
    private object Data;
    private int Position;
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
    private string PopupType = "ADD";
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
        TotalSlotText = transform.Find("TotalSlotText").GetComponent<TextMeshProUGUI>();
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
                Type = type;
                await CreateSetButtonAsync(data, type, setButtonTransform, contentTransform);
            });
        }

        // Load gói nạp của Tab đầu tiên nếu có danh mục
        if (typeList.Count > 0)
        {
            Type = typeList[0];
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
                Set = set;
                await LoadEquipmentAsync(data, type, set, contentTransform);
            });
        }

        // Load gói nạp của Tab đầu tiên nếu có danh mục
        if (setList.Count > 0)
        {
            Set = setList[0];
            await LoadEquipmentAsync(data, type, setList[0], contentTransform);
        }
    }
    public async Task LoadEquipmentAsync(object data, string type, string set, Transform contentTransform)
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

        TotalSlotText.text = EquipmentType.SlotValue.ToString();

        List<Equipments> equipments = new List<Equipments>();
        List<string> cardIdList = new List<string>();
        

        if (data is CardHeroes cardHero)
        {
            cardIdList.Add(cardHero.Id);
            equipments = await UserEquipmentsService.Create().GetUserCardHeroesEquipmentsAsync(User.CurrentUserId, cardIdList, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is Books book)
        {
            cardIdList.Add(book.Id);
            equipments = await UserEquipmentsService.Create().GetUserBooksEquipmentsAsync(User.CurrentUserId, cardIdList, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardCaptains cardCaptain)
        {
            cardIdList.Add(cardCaptain.Id);
            equipments = await UserEquipmentsService.Create().GetUserCardCaptainsEquipmentsAsync(User.CurrentUserId, cardIdList, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is Pets pet)
        {
            cardIdList.Add(pet.Id);
            equipments = await UserEquipmentsService.Create().GetUserPetsEquipmentsAsync(User.CurrentUserId, cardIdList, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardMilitaries cardMilitary)
        {
            cardIdList.Add(cardMilitary.Id);
            equipments = await UserEquipmentsService.Create().GetUserCardMilitariesEquipmentsAsync(User.CurrentUserId, cardIdList, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardSpells cardSpell)
        {
            cardIdList.Add(cardSpell.Id);
            equipments = await UserEquipmentsService.Create().GetUserCardSpellsEquipmentsAsync(User.CurrentUserId, cardIdList, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardMonsters cardMonster)
        {
            cardIdList.Add(cardMonster.Id);
            equipments = await UserEquipmentsService.Create().GetUserCardMonstersEquipmentsAsync(User.CurrentUserId, cardIdList, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardColonels cardColonel)
        {
            cardIdList.Add(cardColonel.Id);
            equipments = await UserEquipmentsService.Create().GetUserCardColonelsEquipmentsAsync(User.CurrentUserId, cardIdList, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardGenerals cardGeneral)
        {
            cardIdList.Add(cardGeneral.Id);
            equipments = await UserEquipmentsService.Create().GetUserCardGeneralsEquipmentsAsync(User.CurrentUserId, cardIdList, type);
            equipments = equipments.Where(e => e.Set == set).ToList();
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            cardIdList.Add(cardAdmiral.Id);
            equipments = await UserEquipmentsService.Create().GetUserCardAdmiralsEquipmentsAsync(User.CurrentUserId, cardIdList, type);
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
            int position = i;
            positionText.text = position.ToString();
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

                CreateStarUI(equipment.Star, starTransform);

                addButton.gameObject.SetActive(false);
                changeButton.gameObject.SetActive(true);
                removeButton.gameObject.SetActive(true);

                changeButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupType = "CHANGE";
                    await CreatePopupEquipmentsAsync(data, position);
                });

                removeButton.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
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

                addButton.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    PopupType = "ADD";
                    await CreatePopupEquipmentsAsync(data, position);
                });
            }
        }
    }
    public async Task CreatePopupEquipmentsAsync(object data, int position, string statusToggle = "NOT EQUIP")
    {
        PopupEquipmentObject = Instantiate(PopupEquipmentsPanelPrefab, MainPanel);
        Transform contentTransform = PopupEquipmentObject.transform.Find("Scroll View/Viewport/Content");
        // Toggle toggle = PopupEquipmentObject.transform.Find("Toggle").GetComponent<Toggle>();
        // toggle.isOn = (statusToggle == "ALL");
        // toggle.onValueChanged.AddListener(async (bool isOn) =>
        // {
        //     string newStatusToggle = isOn ? "ALL" : "NOT EQUIP";
        //     Destroy(PopupEquipmentObject);
        //     await CreatePopupEquipmentsAsync(data, position, newStatusToggle); // Gọi lại nhưng giữ statusToggle mới
        // });
        Button closeButton = PopupEquipmentObject.transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(PopupEquipmentObject);
        });

        PaginationManager = PopupEquipmentObject.transform.Find("PaginationPanelPrefab").GetComponent<PaginationManager>();


        // equipments = equipments.Where(e => e.Set == Set).ToList();
        // int totalRecord = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, Search, MainType, Rare);

        // CreatePopupEquipmentsUI(data, equipments, contentPanel, position);
        Data = data;
        ContentTransform = contentTransform;
        Position = position;
        IsSearchingOrFiltering = true;
        await LoadCurrentPageAsync(data, position, contentTransform);
    }
    public async Task LoadCurrentPageAsync(object data, int position, Transform contentTransform)
    {
        int totalRecord = 0;
        List<Equipments> equipments = new List<Equipments>();
        if (data is CardHeroes cardHero)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardHeroesEquipmentsAsync(User.CurrentUserId, Search, Type, Rare, Set, PAGE_SIZE, Offset, "ALL");
            totalRecord = await UserEquipmentsService.Create().GetUserCardHeroesEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);

            if (!PopupType.Equals("ADD", StringComparison.OrdinalIgnoreCase))
            {
                equipments = equipments
                    .Where(e => e.Id != cardHero.Id)
                    .ToList();

                totalRecord = totalRecord - 1;
            }
        }
        else if (data is CardCaptains cardCaptain)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardCaptainsEquipmentsAsync(User.CurrentUserId, Search, Type, Rare, Set, PAGE_SIZE, Offset, "ALL");
            totalRecord = await UserEquipmentsService.Create().GetUserCardCaptainsEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);

            if (!PopupType.Equals("ADD", StringComparison.OrdinalIgnoreCase))
            {
                equipments = equipments
                    .Where(e => e.Id != cardCaptain.Id)
                    .ToList();

                totalRecord = totalRecord - 1;
            }
        }
        else if (data is CardColonels cardColonel)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardColonelsEquipmentsAsync(User.CurrentUserId, Search, Type, Rare, Set, PAGE_SIZE, Offset, "ALL");
            totalRecord = await UserEquipmentsService.Create().GetUserCardColonelsEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);

            if (!PopupType.Equals("ADD", StringComparison.OrdinalIgnoreCase))
            {
                equipments = equipments
                    .Where(e => e.Id != cardColonel.Id)
                    .ToList();

                totalRecord = totalRecord - 1;
            }
        }
        else if (data is CardGenerals cardGeneral)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardGeneralsEquipmentsAsync(User.CurrentUserId, Search, Type, Rare, Set, PAGE_SIZE, Offset, "ALL");
            totalRecord = await UserEquipmentsService.Create().GetUserCardGeneralsEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);

            if (!PopupType.Equals("ADD", StringComparison.OrdinalIgnoreCase))
            {
                equipments = equipments
                    .Where(e => e.Id != cardGeneral.Id)
                    .ToList();

                totalRecord = totalRecord - 1;
            }
        }
        else if (data is CardAdmirals cardAdmiral)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardAdmiralsEquipmentsAsync(User.CurrentUserId, Search, Type, Rare, Set, PAGE_SIZE, Offset, "ALL");
            totalRecord = await UserEquipmentsService.Create().GetUserCardAdmiralsEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);

            if (!PopupType.Equals("ADD", StringComparison.OrdinalIgnoreCase))
            {
                equipments = equipments
                    .Where(e => e.Id != cardAdmiral.Id)
                    .ToList();

                totalRecord = totalRecord - 1;
            }
        }
        else if (data is CardMonsters cardMonster)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardMonstersEquipmentsAsync(User.CurrentUserId, Search, Type, Rare, Set, PAGE_SIZE, Offset, "ALL");
            totalRecord = await UserEquipmentsService.Create().GetUserCardMonstersEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);

            if (!PopupType.Equals("ADD", StringComparison.OrdinalIgnoreCase))
            {
                equipments = equipments
                    .Where(e => e.Id != cardMonster.Id)
                    .ToList();

                totalRecord = totalRecord - 1;
            }
        }
        else if (data is CardMilitaries cardMilitary)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardMilitariesEquipmentsAsync(User.CurrentUserId, Search, Type, Rare, Set, PAGE_SIZE, Offset, "ALL");
            totalRecord = await UserEquipmentsService.Create().GetUserCardMilitariesEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);

            if (!PopupType.Equals("ADD", StringComparison.OrdinalIgnoreCase))
            {
                equipments = equipments
                    .Where(e => e.Id != cardMilitary.Id)
                    .ToList();

                totalRecord = totalRecord - 1;
            }
        }
        else if (data is CardSpells cardSpell)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserCardSpellsEquipmentsAsync(User.CurrentUserId, Search, Type, Rare, Set, PAGE_SIZE, Offset, "ALL");
            totalRecord = await UserEquipmentsService.Create().GetUserCardSpellsEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);

            if (!PopupType.Equals("ADD", StringComparison.OrdinalIgnoreCase))
            {
                equipments = equipments
                    .Where(e => e.Id != cardSpell.Id)
                    .ToList();

                totalRecord = totalRecord - 1;
            }
        }
        else if (data is Books book)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserBooksEquipmentsAsync(User.CurrentUserId, Search, Type, Rare, Set, PAGE_SIZE, Offset, "ALL");
            totalRecord = await UserEquipmentsService.Create().GetUserBooksEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);

            if (!PopupType.Equals("ADD", StringComparison.OrdinalIgnoreCase))
            {
                equipments = equipments
                    .Where(e => e.Id != book.Id)
                    .ToList();

                totalRecord = totalRecord - 1;
            }
        }
        else if (data is Pets pet)
        {
            equipments = await UserEquipmentsService.Create().GetAllUserPetsEquipmentsAsync(User.CurrentUserId, Search, Type, Rare, Set, PAGE_SIZE, Offset, "ALL");
            totalRecord = await UserEquipmentsService.Create().GetUserPetsEquipmentsCountAsync(User.CurrentUserId, Search, Type, Rare, Set);

            if (!PopupType.Equals("ADD", StringComparison.OrdinalIgnoreCase))
            {
                equipments = equipments
                    .Where(e => e.Id != pet.Id)
                    .ToList();

                totalRecord = totalRecord - 1;
            }
        }

        TotalItems = totalRecord;
        CreatePopupEquipmentsUI(data, equipments, position, contentTransform);

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
    public void CreatePopupEquipmentsUI(object data, List<Equipments> equipmentsList, int position, Transform contentTransform)
    {
        foreach (var equipment in equipmentsList)
        {
            GameObject equipmentObject = Instantiate(EquipmentsWearingPrefab, contentTransform);

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
            equipButton.onClick.AddListener((async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                if (data is CardHeroes cardHero)
                {
                    await UserEquipmentsService.Create().InsertUserCardHeroEquipmentsAsync(User.CurrentUserId, cardHero.Id, equipment, position);
                    // await CreateCardHeroesEquipmentsAsync(cardHero);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardCaptains cardCaptain)
                {
                    await UserEquipmentsService.Create().InsertUserCardCaptainEquipmentsAsync(User.CurrentUserId, cardCaptain.Id, equipment, position);
                    // await CreateCardCaptainsEquipmentsAsync(cardCaptain);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardColonels cardColonel)
                {
                    await UserEquipmentsService.Create().InsertUserCardColonelEquipmentsAsync(User.CurrentUserId, cardColonel.Id, equipment, position);
                    // await CreateCardColonelsEquipmentsAsync(cardColonel);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardGenerals cardGeneral)
                {
                    await UserEquipmentsService.Create().InsertUserCardGeneralEquipmentsAsync(User.CurrentUserId, cardGeneral.Id, equipment, position);
                    // await CreateCardGeneralsEquipmentsAsync(cardGeneral);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardAdmirals cardAdmiral)
                {
                    await UserEquipmentsService.Create().InsertUserCardAdmiralEquipmentsAsync(User.CurrentUserId, cardAdmiral.Id, equipment, position);
                    // await CreateCardAdmiralsEquipmentsAsync(cardAdmiral);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardMonsters cardMonster)
                {
                    await UserEquipmentsService.Create().InsertUserCardMonsterEquipmentsAsync(User.CurrentUserId, cardMonster.Id, equipment, position);
                    // await CreateCardMonstersEquipmentsAsync(cardMonster);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardMilitaries cardMilitary)
                {
                    await UserEquipmentsService.Create().InsertUserCardMilitaryEquipmentsAsync(User.CurrentUserId, cardMilitary.Id, equipment, position);
                    // await CreateCardMilitaryEquipmentsAsync(cardMilitary);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is CardSpells cardSpell)
                {
                    await UserEquipmentsService.Create().InsertUserCardSpellEquipmentsAsync(User.CurrentUserId, cardSpell.Id, equipment, position);
                    // await CreateCardSpellEquipmentsAsync(cardSpell);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is Books book)
                {
                    await UserEquipmentsService.Create().InsertUserBookEquipmentsAsync(User.CurrentUserId, book.Id, equipment, position);
                    // await CreateBooksEquipmentsAsync(book);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                }
                else if (data is Pets pet)
                {
                    await UserEquipmentsService.Create().InsertUserPetEquipmentsAsync(User.CurrentUserId, pet.Id, equipment, position);
                    // await CreatePetsEquipmentsAsync(pet);
                    double newPower = await TeamsService.Create().GetTeamsPowerAsync(User.CurrentUserId);
                    double currentPower = User.CurrentUserPower;
                    User.CurrentUserPower = newPower;
                    PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                }

                Destroy(PopupEquipmentObject);
            }));
        }
        GridLayoutGroup gridLayout = contentTransform.GetComponent<GridLayoutGroup>();
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
        _ = LoadCurrentPageAsync(Data, Position, ContentTransform);
    }

    private void OnDestroy()
    {
        // Luôn luôn hủy đăng ký sự kiện khi Object bị xóa để tránh lỗi bộ nhớ
        if (PaginationManager != null)
        {
            PaginationManager.OnPageChanged -= OnPageSelected;
        }
    }
    public void CreateStarUI(int star, Transform contentTransform)
    {
        int imageIndex = (star == 0) ? 0 : ((star - 1) % 10) + 1;
        int starIndex = (star == 0) ? 1 : (star - 1) / 10;
        for (int i = 0; i < imageIndex; i++)
        {
            GameObject starObject = Instantiate(StarPrefab, contentTransform);

            RawImage starImage = starObject.transform.Find("ItemImage").GetComponent<RawImage>();
            GetStarImage(starImage, starIndex);
        }
        GridLayoutGroup GridLayout = contentTransform.GetComponent<GridLayoutGroup>();
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
