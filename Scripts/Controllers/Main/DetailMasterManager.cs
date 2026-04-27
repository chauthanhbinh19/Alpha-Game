using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailMasterManager : MonoBehaviour
{
    public static DetailMasterManager Instance { get; private set; }
    TeamsService teamsService;
    UserItemsService userItemsService;
    private TMP_FontAsset EuroStyleNormalFont;
    private const int FONT_SIZE = 20;
    private const int MAX_LEVEL = 10000;
    private const int MATERIAL_PER_LEVEL = 1;
    private const int CURRENCY_PER_LEVEL = 10;
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
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
    }
    public async Task CreateCardHeroesEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardHeroes cardHeroes)
    {
        Master master = await UserCardHeroesMasterService.Create().GetCardHeroMasterAsync(feature.FeatureName, cardHeroes.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        master.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, master.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

        TextMeshProUGUI upLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upLevelButtonText.font = EuroStyleNormalFont;
        upLevelButtonText.fontSize = FONT_SIZE;
        upLevelButtonText.fontStyle = FontStyles.Bold;
        upLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI upMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upMaxLevelButtonText.font = EuroStyleNormalFont;
        upMaxLevelButtonText.fontSize = FONT_SIZE;
        upMaxLevelButtonText.fontStyle = FontStyles.Bold;
        upMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, false, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await MasterService.UpLevelAsync(cardHeroes, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardHeroesEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardHeroes);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, true, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await MasterService.UpLevelAsync(cardHeroes, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardHeroesEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardHeroes);
            }
        });
    }
    public async Task CreateBooksEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, Books books)
    {
        Master master = await UserBooksMasterService.Create().GetBookMasterAsync(feature.FeatureName, books.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        master.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, master.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

        TextMeshProUGUI upLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upLevelButtonText.font = EuroStyleNormalFont;
        upLevelButtonText.fontSize = FONT_SIZE;
        upLevelButtonText.fontStyle = FontStyles.Bold;
        upLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI upMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upMaxLevelButtonText.font = EuroStyleNormalFont;
        upMaxLevelButtonText.fontSize = FONT_SIZE;
        upMaxLevelButtonText.fontStyle = FontStyles.Bold;
        upMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, true, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(books, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateBooksEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, books);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, false, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(books, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateBooksEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, books);
            }
        });
    }
    public async Task CreateCardCaptainsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardCaptains cardCaptains)
    {
        Master master = await UserCardCaptainsMasterService.Create().GetCardCaptainMasterAsync(feature.FeatureName, cardCaptains.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        master.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, master.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

        TextMeshProUGUI upLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upLevelButtonText.font = EuroStyleNormalFont;
        upLevelButtonText.fontSize = FONT_SIZE;
        upLevelButtonText.fontStyle = FontStyles.Bold;
        upLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI upMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upMaxLevelButtonText.font = EuroStyleNormalFont;
        upMaxLevelButtonText.fontSize = FONT_SIZE;
        upMaxLevelButtonText.fontStyle = FontStyles.Bold;
        upMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, true, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, 1);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardCaptains, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardCaptainsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardCaptains);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, false, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardCaptains, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardCaptainsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardCaptains);
            }
        });
    }
    public async Task CreatePetsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, Pets pets)
    {
        Master master = await UserPetsMasterService.Create().GetPetMasterAsync(feature.FeatureName, pets.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        master.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, master.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

        TextMeshProUGUI upLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upLevelButtonText.font = EuroStyleNormalFont;
        upLevelButtonText.fontSize = FONT_SIZE;
        upLevelButtonText.fontStyle = FontStyles.Bold;
        upLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI upMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upMaxLevelButtonText.font = EuroStyleNormalFont;
        upMaxLevelButtonText.fontSize = FONT_SIZE;
        upMaxLevelButtonText.fontStyle = FontStyles.Bold;
        upMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, true, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, 1);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(pets, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreatePetsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, pets);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, false, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await MasterService.UpLevelAsync(pets, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreatePetsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, pets);
            }
        });
    }
    public async Task CreateCardMilitaryEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardMilitaries cardMilitary)
    {
        Master master = await UserCardMilitariesMasterService.Create().GetCardMilitaryMasterAsync(feature.FeatureName, cardMilitary.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        master.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, master.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

        TextMeshProUGUI upLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upLevelButtonText.font = EuroStyleNormalFont;
        upLevelButtonText.fontSize = FONT_SIZE;
        upLevelButtonText.fontStyle = FontStyles.Bold;
        upLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI upMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upMaxLevelButtonText.font = EuroStyleNormalFont;
        upMaxLevelButtonText.fontSize = FONT_SIZE;
        upMaxLevelButtonText.fontStyle = FontStyles.Bold;
        upMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, false, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardMilitary, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardMilitaryEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardMilitary);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, true, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardMilitary, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardMilitaryEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardMilitary);
            }
        });
    }
    public async Task CreateCardSpellEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardSpells cardSpell)
    {
        Master master = await UserCardSpellsMasterService.Create().GetCardSpellMasterAsync(feature.FeatureName, cardSpell.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        master.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, master.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

        TextMeshProUGUI upLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upLevelButtonText.font = EuroStyleNormalFont;
        upLevelButtonText.fontSize = FONT_SIZE;
        upLevelButtonText.fontStyle = FontStyles.Bold;
        upLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI upMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upMaxLevelButtonText.font = EuroStyleNormalFont;
        upMaxLevelButtonText.fontSize = FONT_SIZE;
        upMaxLevelButtonText.fontStyle = FontStyles.Bold;
        upMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, false, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardSpell, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardSpellEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardSpell);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, true, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardSpell, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardSpellEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardSpell);
            }
        });
    }
    public async Task CreateCardMonstersEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardMonsters cardMonsters)
    {
        Master master = await UserCardMonstersMasterService.Create().GetCardMonsterMasterAsync(feature.FeatureName, cardMonsters.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        master.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, master.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

        TextMeshProUGUI upLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upLevelButtonText.font = EuroStyleNormalFont;
        upLevelButtonText.fontSize = FONT_SIZE;
        upLevelButtonText.fontStyle = FontStyles.Bold;
        upLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI upMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upMaxLevelButtonText.font = EuroStyleNormalFont;
        upMaxLevelButtonText.fontSize = FONT_SIZE;
        upMaxLevelButtonText.fontStyle = FontStyles.Bold;
        upMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, false, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardMonsters, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardMonstersEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardMonsters);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, true, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardMonsters, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardMonstersEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardMonsters);
            }
        });
    }
    public async Task CreateCardColonelsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardColonels cardColonels)
    {
        Master master = await UserCardColonelsMasterService.Create().GetCardColonelMasterAsync(feature.FeatureName, cardColonels.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        master.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, master.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

        TextMeshProUGUI upLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upLevelButtonText.font = EuroStyleNormalFont;
        upLevelButtonText.fontSize = FONT_SIZE;
        upLevelButtonText.fontStyle = FontStyles.Bold;
        upLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI upMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upMaxLevelButtonText.font = EuroStyleNormalFont;
        upMaxLevelButtonText.fontSize = FONT_SIZE;
        upMaxLevelButtonText.fontStyle = FontStyles.Bold;
        upMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, false, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardColonels, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardColonelsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardColonels);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, true, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardColonels, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardColonelsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardColonels);
            }
        });
    }
    public async Task CreateCardGeneralsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardGenerals cardGenerals)
    {
        Master master = await UserCardGeneralsMasterService.Create().GetCardGeneralMasterAsync(feature.FeatureName, cardGenerals.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        master.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, master.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

        TextMeshProUGUI upLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upLevelButtonText.font = EuroStyleNormalFont;
        upLevelButtonText.fontSize = FONT_SIZE;
        upLevelButtonText.fontStyle = FontStyles.Bold;
        upLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI upMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upMaxLevelButtonText.font = EuroStyleNormalFont;
        upMaxLevelButtonText.fontSize = FONT_SIZE;
        upMaxLevelButtonText.fontStyle = FontStyles.Bold;
        upMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, false, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardGenerals, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardGeneralsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardGenerals);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, true, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardGenerals, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardGeneralsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardGenerals);
            }
        });
    }
    public async Task CreateCardAdmiralsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardAdmirals cardAdmirals)
    {
        Master master = await UserCardAdmiralsMasterService.Create().GetCardAdmiralMasterAsync(feature.FeatureName, cardAdmirals.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        master.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, master.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);
        
        TextMeshProUGUI upLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upLevelButtonText.font = EuroStyleNormalFont;
        upLevelButtonText.fontSize = FONT_SIZE;
        upLevelButtonText.fontStyle = FontStyles.Bold;
        upLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI upMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        upMaxLevelButtonText.font = EuroStyleNormalFont;
        upMaxLevelButtonText.fontSize = FONT_SIZE;
        upMaxLevelButtonText.fontStyle = FontStyles.Bold;
        upMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, false, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardAdmirals, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardAdmiralsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardAdmirals);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, master.Level, true, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardAdmirals, newMaster, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardAdmiralsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardAdmirals);
            }
        });
    }
    // public void CreateEquipmentsEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
    //  Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, Equipments equipments)
    // {
    //     Master Master = UserEquipmentsMasterService.Create().GetEquipmentsMaster(mainType, equipments.id);
    //     GameObject slotObject = Instantiate(prefab, SlotPanel);

    //     Items item = new Items();
    //     MasterService MasterService = new MasterService();

    //     item = userItemsService.GetUserItemByName(mainType);
    //     UIManager.Instance.SetUI(slotObject, mainType, Master.level, type);
    //     UIManager.Instance.SetMaterialUI(currentObject, mainType, Master.level, item.quantity);
    //     UpLevelButton.onClick.RemoveAllListeners();
    //     UpMaxLevelButton.onClick.RemoveAllListeners();
    //     UpLevelButton.onClick.AddListener(() =>
    //     {
    //         int levelsPerSkill = 1000;
    //         int materialQuantity = (Master.level == 0) ? 1 : (Master.level % levelsPerSkill == 0 ? levelsPerSkill : Master.level % levelsPerSkill);
    //         if (item.quantity >= materialQuantity)
    //         {
    //             item.quantity = result.totalMaterialUsed;
    //             userItemsService.UpdateUserItemsQuantity(item);
    //             Master newMaster = new Master();
    //             newMaster = MasterService.EnhanceMaster(Master, 1);
                
    //             double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
    //             MasterService.UpLevel(equipments, newMaster, mainType);
    //             double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
    //             FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
    //             Destroy(slotObject);
    //             CreateEquipmentsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, equipments);
    //         }
    //     });
    //     UpMaxLevelButton.onClick.AddListener(() =>
    //     {
    //         int level = EvaluateItem.CalculateMaxMaterialLevel(item.quantity, Master.level);
    //         int materialQuantity = EvaluateItem.CalculateMaxMaterialQuantity(item.quantity, Master.level);
    //         if (item.quantity >= materialQuantity)
    //         {
    //             item.quantity = result.totalMaterialUsed;
    //             userItemsService.UpdateUserItemsQuantity(item);
    //             Master newMaster = new Master();
    //             newMaster = MasterService.EnhanceMaster(Master, level);
                
    //             double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
    //             MasterService.UpLevel(equipments, newMaster, mainType);
    //             double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
    //             FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
    //             Destroy(slotObject);
    //             CreateEquipmentsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, equipments);
    //         }
    //     });
    // }
}