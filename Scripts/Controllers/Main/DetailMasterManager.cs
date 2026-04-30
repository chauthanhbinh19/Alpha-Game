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
        SetUI(slotObject, feature.FeatureName, master.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
        SetUI(slotObject, feature.FeatureName, master.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
        SetUI(slotObject, feature.FeatureName, master.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
        SetUI(slotObject, feature.FeatureName, master.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
        SetUI(slotObject, feature.FeatureName, master.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
        SetUI(slotObject, feature.FeatureName, master.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
        SetUI(slotObject, feature.FeatureName, master.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
        SetUI(slotObject, feature.FeatureName, master.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
        SetUI(slotObject, feature.FeatureName, master.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
        SetUI(slotObject, feature.FeatureName, master.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, master.Level, MAX_LEVEL);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

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
    //     SetUI(slotObject, mainType, Master.level, type);
    //     SetMaterialUI(currentObject, mainType, Master.level, item.quantity);
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
    public void SetUI(GameObject gameObject, string type, int level = 0, string mainType = "")
    {
        if (mainType.Equals(AppConstants.MainMenuSet1.AFFINITY) || mainType.Equals(AppConstants.MainMenuSet1.BLESSING))
        {
            return;
        }
        Transform transform = gameObject.transform;
        Transform backgroundImageTransform = transform.Find("Background");
        if (backgroundImageTransform != null)
        {
            RawImage backgroundImage = transform.Find("Background").GetComponent<RawImage>();
            Texture backgroundTexture = TextureHelper.LoadTextureCached($"UI/Background3/{mainType}");
            backgroundImage.texture = backgroundTexture;
            backgroundImage.rectTransform.sizeDelta = new Vector2(350, 350);
        }

        Transform backgroundTransform = transform.Find("BackgroundCircle");
        if (backgroundTransform != null)
        {
            RawImage backgroundImageCircle = backgroundTransform.GetComponent<RawImage>();
            if (backgroundImageCircle != null)
            {
                backgroundImageCircle.gameObject.AddComponent<RotateAnimation>();
            }
        }

        int totalSkills = 10;
        int levelsPerSkill = 1000;

        // Đặt tất cả kỹ năng về trạng thái mặc định (đen + text "0/1000")
        for (int i = 1; i <= totalSkills; i++)
        {
            Transform aptitudeSkill = transform.Find($"UpgradeSkill{i}");
            if (aptitudeSkill == null) continue;

            RawImage aptitudeImage = aptitudeSkill.Find("AptitudeImage").GetComponent<RawImage>();
            TextMeshProUGUI levelText = aptitudeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

            Texture texture = TextureHelper.LoadTextureCached($"UI/Rank/{type}");
            aptitudeImage.texture = texture;

            if (aptitudeImage != null) aptitudeImage.color = Color.black;
            if (levelText != null) levelText.text = $"0/{levelsPerSkill}";
        }

        // Xác định số kỹ năng được kích hoạt
        int activeSkillsCount = Mathf.Clamp((level / levelsPerSkill), 1, totalSkills);
        for (int i = 1; i <= activeSkillsCount; i++)
        {
            Transform activeSkill = transform.Find($"UpgradeSkill{i}");
            if (activeSkill != null)
            {
                RawImage activeImage = activeSkill.Find("AptitudeImage").GetComponent<RawImage>();
                TextMeshProUGUI activeLevelText = activeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

                if (activeImage != null && level != 0) activeImage.color = Color.white;

                if (activeLevelText != null)
                {
                    // Kiểm tra nếu level là bội số của levelsPerSkill (1000, 2000, ..., 10000)
                    int displayedLevel = (level % levelsPerSkill == 0) ? levelsPerSkill : level % levelsPerSkill;
                    activeLevelText.text = $"{displayedLevel}/{levelsPerSkill}";
                }
            }
        }
        TextMeshProUGUI totalLevelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        totalLevelText.text = level.ToString();
    }
    public void SetMaterialUI(GameObject gameobject, string itemImage, double itemQuantity, double currencyQuantity, int rankLevel, int maxLevel)
    {
        Transform transform = gameObject.transform;
        Transform currencyPanel = transform.Find("DictionaryCards/Currency");
        // List<Currencies> currencies = await UserCurrenciesService.Create().GetUserCurrencyAsync(User.CurrentUserId);
        ButtonEvent.Instance.Close(currencyPanel);
        GameObject itemObject = UIManager.Instance.Get("ItemPrefab");
        GameObject tempObject = Instantiate(itemObject, currencyPanel);
        RawImage image = tempObject.transform.Find("Image").GetComponent<RawImage>();
        image.texture = TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(itemImage));
        TextMeshProUGUI quantityText = tempObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        quantityText.text = NumberFormatterHelper.FormatNumber(itemQuantity, true);
        // CurrenciesManager.Instance.GetMainCurrency(currencies, currencyPanel);

        var oneResult = ItemHelper.CalculateLevelUp(itemQuantity, currencyQuantity, 1, 10, rankLevel, false, maxLevel);
        var maxResult = ItemHelper.CalculateLevelUp(itemQuantity, currencyQuantity, 1, 10, rankLevel, true, maxLevel);
        RawImage oneLevelCurrencyImage = transform.Find("DictionaryCards/OneLevelCurrency/CurrencyImage").GetComponent<RawImage>();
        RawImage maxLevelCurrencyImage = transform.Find("DictionaryCards/MaxLevelCurrency/CurrencyImage").GetComponent<RawImage>();
        Texture oneLevelCurrencyTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Currency.SILVER}");
        Texture maxLevelCurrencyTexture = TextureHelper.LoadTextureCached($"{ImageConstants.Currency.SILVER}");
        oneLevelCurrencyImage.texture = oneLevelCurrencyTexture;
        maxLevelCurrencyImage.texture = maxLevelCurrencyTexture;

        TextMeshProUGUI oneLevelCurrencyText = transform.Find("DictionaryCards/OneLevelCurrency/QuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI maxLevelCurrencyText = transform.Find("DictionaryCards/MaxLevelCurrency/QuantityText").GetComponent<TextMeshProUGUI>();
        oneLevelCurrencyText.text = oneResult.totalCurrencyUsed.ToString();
        maxLevelCurrencyText.text = maxResult.totalCurrencyUsed.ToString();

        RawImage oneLevelImage = transform.Find("DictionaryCards/OneLevelMaterial/MaterialImage").GetComponent<RawImage>();
        Texture oneLevelTexture = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(itemImage)}");
        oneLevelImage.texture = oneLevelTexture;

        TextMeshProUGUI oneLevelQuantity = transform.Find("DictionaryCards/OneLevelMaterial/QuantityText").GetComponent<TextMeshProUGUI>();
        oneLevelQuantity.text = oneResult.totalMaterialUsed.ToString();

        // RectTransform oneLevelRectTransform = oneLevelImage.GetComponent<RectTransform>();
        // oneLevelRectTransform.sizeDelta = new Vector2(40, 40);

        RawImage maxLevelImage = transform.Find("DictionaryCards/MaxLevelMaterial/MaterialImage").GetComponent<RawImage>();
        Texture maxLevelTexture = TextureHelper.LoadTextureCached($"{ImageHelper.RemoveImageExtension(itemImage)}");
        maxLevelImage.texture = maxLevelTexture;

        TextMeshProUGUI maxLevelQuantity = transform.Find("DictionaryCards/MaxLevelMaterial/QuantityText").GetComponent<TextMeshProUGUI>();
        maxLevelQuantity.text = maxResult.totalMaterialUsed.ToString();

        // RectTransform maxLevelRectTransform = maxLevelImage.GetComponent<RectTransform>();
        // maxLevelRectTransform.sizeDelta = new Vector2(40, 40);
    }
}