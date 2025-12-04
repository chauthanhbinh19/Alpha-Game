using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        DailyCheckinPanelPrefab = UIManager.Instance.GetGameObject("DailyCheckinPanelPrefab");
        DailyCheckinComponentPrefab = UIManager.Instance.GetGameObject("DailyCheckinComponentPrefab");
    }
    private static readonly Dictionary<string, Func<string, Task<CheckinItemInfoDTO>>> DailyCheckinResolvers =
    new Dictionary<string, Func<string, Task<CheckinItemInfoDTO>>>
{
    { AppConstants.MainType.CARD_HERO, async id => {
        var obj = await CardHeroesService.Create().GetCardHeroByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.ALCHEMY, async id => {
        var obj = await AlchemiesService.Create().GetAlchemyByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.AVATAR, async id => {
        var obj = await AvatarsService.Create().GetAvatarByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.BORDER, async id => {
        var obj = await BordersService.Create().GetBorderByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.BOOK, async id => {
        var obj = await BooksService.Create().GetBookByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.CARD_ADMIRAL, async id => {
        var obj = await CardAdmiralsService.Create().GetCardAdmiralByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.CARD_CAPTAIN, async id => {
        var obj = await CardCaptainsService.Create().GetCardCaptainByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.CARD_COLONEL, async id => {
        var obj = await CardColonelsService.Create().GetCardColonelByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.CARD_GENERAL, async id => {
        var obj = await CardGeneralsService.Create().GetCardGeneralByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.CARD_LIFE, async id => {
        var obj = await CardLivesService.Create().GetCardLifeByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.CARD_MILITARY, async id => {
        var obj = await CardMilitariesService.Create().GetCardMilitaryByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.CARD_MONSTER, async id => {
        var obj = await CardMonstersService.Create().GetCardMonsterByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.CARD_SPELL, async id => {
        var obj = await CardSpellsService.Create().GetCardSpellByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.COLLABORATION_EQUIPMENT, async id => {
        var obj = await CollaborationEquipmentsService.Create().GetCollaborationEquipmentByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.COLLABORATION, async id => {
        var obj = await CollaborationsService.Create().GetCollaborationByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.EQUIPMENT, async id => {
        var obj = await EquipmentsService.Create().GetEquipmentByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.FORGE, async id => {
        var obj = await ForgesService.Create().GetForgeByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.MAGIC_FORMATION_CIRCLE, async id => {
        var obj = await MagicFormationCirclesService.Create().GetMagicFormationCircleByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.MEDAL, async id => {
        var obj = await MedalsService.Create().GetMedalByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.PET, async id => {
        var obj = await PetsService.Create().GetPetByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.PUPPET, async id => {
        var obj = await PuppetsService.Create().GetPuppetByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.RELIC, async id => {
        var obj = await RelicsService.Create().GetRelicByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.SKILL, async id => {
        var obj = await SkillsService.Create().GetSkillByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.SYMBOL, async id => {
        var obj = await SymbolsService.Create().GetSymbolByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.TALISMAN, async id => {
        var obj = await TalismansService.Create().GetTalismanByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
    { AppConstants.MainType.TITLE, async id => {
        var obj = await TitlesService.Create().GetTitleByIdAsync(id);
        return new CheckinItemInfoDTO(obj.Image, obj.Quantity);
    }},
};
    public async Task CreateDailyCheckinGroupAsync()
    {
        GameObject popupObject = Instantiate(DailyCheckinPanelPrefab, MainPanel);
        TextMeshProUGUI titleText = popupObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        TabButtonPanel = popupObject.transform.Find("Scroll View/Viewport/Content");
        Button CloseButton = popupObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(popupObject);
        });
        Button HomeButton = popupObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        titleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.DAILY_CHECKIN);
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
                btn.onClick.AddListener(async () =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await OnButtonClickAsync(button, subType);
                });

                if (index == 0)
                {
                    mainType = uniqueType;
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    if (mainType.Equals("daily_checkin"))
                    {
                        List<UserDailyCheckin> userDailyCheckins = await UserDailyCheckinService.Create().GetUserDailyCheckinAsync(User.CurrentUserId);
                        _=CheckObjectTypeAsync(userDailyCheckins);
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
    async Task OnButtonClickAsync(GameObject clickedButton, string type)
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
            List<UserDailyCheckin> userDailyCheckins = await UserDailyCheckinService.Create().GetUserDailyCheckinAsync(User.CurrentUserId);
            _=CheckObjectTypeAsync(userDailyCheckins);
        }
    }
    public async Task CheckObjectTypeAsync(List<UserDailyCheckin> userDailyCheckins)
    {
        foreach (UserDailyCheckin userDailyCheckin in userDailyCheckins)
        {
            string type = userDailyCheckin.DailyCheckin.Type;
            string objectId = userDailyCheckin.DailyCheckin.ObjectId;
            double quantity = userDailyCheckin.DailyCheckin.Quantity;

            if (DailyCheckinResolvers.TryGetValue(type, out var resolver))
            {
                CheckinItemInfoDTO item = await resolver(objectId);
                CreateDailyCheckinUI(item.Image, quantity);
            }
            else
            {
                Debug.LogWarning($"Unknown daily checkin type: {type}");
            }

        }
    }
    public void CreateDailyCheckinUI(string image, double quantity)
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
