using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyCheckinManager : MonoBehaviour
{
    public static DailyCheckinManager Instance { get; private set; }
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private GameObject buttonPrefab;
    private GameObject DailyCheckinPanelPrefab;
    private GameObject DailyCheckinComponentPrefab;
    private Transform DailyCheckinPanel;
    private string mainType;
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

    // Update is called once per frame
    void Update()
    {

    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        DailyCheckinPanelPrefab = UIManager.Instance.GetGameObject("DailyCheckinPanelPrefab");
        DailyCheckinComponentPrefab = UIManager.Instance.GetGameObject("DailyCheckinComponentPrefab");
    }
    private static readonly Dictionary<string, Func<string, CheckinItemInfoDTO>> DailyCheckinResolvers =
    new Dictionary<string, Func<string, CheckinItemInfoDTO>>
{
    { AppConstants.MainType.CARD_HERO, id => {
        var obj = CardHeroesService.Create().GetCardHeroesById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.ALCHEMY, id => {
        var obj = AlchemyService.Create().GetAlchemyById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.AVATAR, id => {
        var obj = AvatarsService.Create().GetAvatarsById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.BORDER, id => {
        var obj = BordersService.Create().GetBordersById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.BOOK, id => {
        var obj = BooksService.Create().GetBooksById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.CARD_ADMIRAL, id => {
        var obj = CardAdmiralsService.Create().GetCardAdmiralsById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.CARD_CAPTAIN, id => {
        var obj = CardCaptainsService.Create().GetCardCaptainsById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.CARD_COLONEL, id => {
        var obj = CardColonelsService.Create().GetCardColonelsById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.CARD_GENERAL, id => {
        var obj = CardGeneralsService.Create().GetCardGeneralsById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.CARD_LIFE, id => {
        var obj = CardLifeService.Create().GetCardLifeById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.CARD_MILITARY, id => {
        var obj = CardMilitaryService.Create().GetCardMilitaryById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.CARD_MONSTER, id => {
        var obj = CardMonstersService.Create().GetCardMonstersById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.CARD_SPELL, id => {
        var obj = CardSpellService.Create().GetCardSpellById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.COLLABORATION_EQUIPMENT, id => {
        var obj = CollaborationEquipmentService.Create().GetCollaborationEquipmentsById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.COLLABORATION, id => {
        var obj = CollaborationService.Create().GetCollaborationsById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.EQUIPMENT, id => {
        var obj = EquipmentsService.Create().GetEquipmentById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.FORGE, id => {
        var obj = ForgeService.Create().GetForgeById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.MAGIC_FORMATION_CIRCLE, id => {
        var obj = MagicFormationCircleService.Create().GetMagicFormationCircleById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.MEDAL, id => {
        var obj = MedalsService.Create().GetMedalsById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.PET, id => {
        var obj = PetsService.Create().GetPetsById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.PUPPET, id => {
        var obj = PuppetService.Create().GetPuppetById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.RELIC, id => {
        var obj = RelicsService.Create().GetRelicsById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.SKILL, id => {
        var obj = SkillsService.Create().GetSkillsById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.SYMBOL, id => {
        var obj = SymbolsService.Create().GetSymbolsById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.TALISMAN, id => {
        var obj = TalismanService.Create().GetTalismanById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    { AppConstants.MainType.TITLE, id => {
        var obj = TitlesService.Create().GetTitlesById(id);
        return new CheckinItemInfoDTO(obj.image, obj.quantity);
    }},
    // { AppConstants.MainType.Item, id => { var obj = ItemsService.Create().GetItemsById(id); return new CheckinItemInfoDTO(obj.image, obj.quantity); } },
};
    public void CreateDailyCheckinGroup()
    {
        GameObject popupObject = Instantiate(DailyCheckinPanelPrefab, MainPanel);
        TextMeshProUGUI titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        TabButtonPanel = popupObject.transform.Find("Scroll View/Viewport/Content");
        Button CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ButtonEvent.Instance.Close(MainPanel);
        });
        Button HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ButtonEvent.Instance.Close(MainPanel);
        });
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.DailyCheckin);
        DailyCheckinPanel = popupObject.transform.Find("DictionaryCards/Scroll View/Viewport/Content");
        // Dictionary<string, int> uniqueTypes = new Dictionary<string, int>();
        // Features features = new Features();
        List<string> uniqueTypes = new List<string> { "daily_checkin", "daily_checkin_event", "daily_checkin_vip" };
        if (uniqueTypes.Count > 0)
        {
            int index = 0;
            foreach (var uniqueType in uniqueTypes)
            {
                string subType = uniqueType;
                GameObject button = Instantiate(buttonPrefab, TabButtonPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = uniqueType.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    OnButtonClick(button, subType);
                });

                if (index == 0)
                {
                    mainType = uniqueType;
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    if (mainType.Equals("daily_checkin"))
                    {
                        List<UserDailyCheckin> userDailyCheckins = UserDailyCheckinService.Create().GetUserDailyCheckin(User.CurrentUserId);
                        CheckObjectType(userDailyCheckins);
                    }
                }
                else
                {
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                }
                index = index + 1;
            }
        }
    }
    void OnButtonClick(GameObject clickedButton, string type)
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
        if (mainType.Equals("daily_checkin"))
        {
            List<UserDailyCheckin> userDailyCheckins = UserDailyCheckinService.Create().GetUserDailyCheckin(User.CurrentUserId);
            CheckObjectType(userDailyCheckins);
        }
    }
    public void CheckObjectType(List<UserDailyCheckin> userDailyCheckins)
    {
        foreach (UserDailyCheckin userDailyCheckin in userDailyCheckins)
        {
            string type = userDailyCheckin.DailyCheckin.type;
            string objectId = userDailyCheckin.DailyCheckin.object_id;
            int quantity = userDailyCheckin.DailyCheckin.quantity;

            if (DailyCheckinResolvers.TryGetValue(type, out var resolver))
            {
                CheckinItemInfoDTO item = resolver(objectId);
                CreateDailyCheckinUI(item.image, quantity);
            }
            else
            {
                Debug.LogWarning($"Unknown daily checkin type: {type}");
            }

        }
    }
    public void CreateDailyCheckinUI(string image, int quantity)
    {
        GameObject dailyCheckinComponent = Instantiate(DailyCheckinComponentPrefab, DailyCheckinPanel);
        RawImage dailyCheckinImage = dailyCheckinComponent.transform.Find("Image").GetComponent<RawImage>();
        TextMeshProUGUI quantityText = dailyCheckinComponent.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();

        string fileNameWithoutExtension = ImageExtensionHandler.RemoveImageExtension(image);
        Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
        dailyCheckinImage.texture = texture;

        quantityText.text = NumberFormatter.FormatNumber(quantity, false);
    }
}
