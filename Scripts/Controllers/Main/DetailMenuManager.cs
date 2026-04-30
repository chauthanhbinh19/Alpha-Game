using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailMenuManager : MonoBehaviour
{
    public static DetailMenuManager Instance { get; private set; }
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
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardHeroes cardHero)
    {
        Rank rank = await UserCardHeroesRankService.Create().GetCardHeroRankAsync(feature.FeatureName, cardHero.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        rank.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        SetUI(slotObject, feature.FeatureName, rank.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, rank.Level, MAX_LEVEL);

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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, false, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardHero, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardHeroesEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardHero);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, true, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardHero, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardHeroesEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardHero);
            }
        });
    }
    public async Task CreateBooksEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, Books book)
    {
        Rank rank = await UserBooksRankService.Create().GetBookRankAsync(feature.FeatureName, book.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        rank.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        SetUI(slotObject, feature.FeatureName, rank.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, rank.Level, MAX_LEVEL);

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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, false, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(book, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateBooksEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, book);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, true, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(book, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateBooksEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, book);
            }
        });
    }
    public async Task CreateCardCaptainsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardCaptains cardCaptain)
    {
        Rank rank = await UserCardCaptainsRankService.Create().GetCardCaptainRankAsync(feature.FeatureName, cardCaptain.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        rank.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        SetUI(slotObject, feature.FeatureName, rank.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, rank.Level, MAX_LEVEL);

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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, false, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardCaptain, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardCaptainsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardCaptain);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, true, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardCaptain, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardCaptainsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardCaptain);
            }
        });
    }
    public async Task CreatePetsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, Pets pet)
    {
        Rank rank = await UserPetsRankService.Create().GetPetRankAsync(feature.FeatureName, pet.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        rank.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        SetUI(slotObject, feature.FeatureName, rank.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, rank.Level, MAX_LEVEL);

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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, false, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(pet, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreatePetsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, pet);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, true, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(pet, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreatePetsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, pet);
            }
        });
    }
    public async Task CreateCardMilitaryEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardMilitaries cardMilitary)
    {
        Rank rank = await UserCardMilitariesRankService.Create().GetCardMilitaryRankAsync(feature.FeatureName, cardMilitary.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        rank.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        SetUI(slotObject, feature.FeatureName, rank.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, rank.Level, MAX_LEVEL);

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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, false, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardMilitary, newRank, feature.FeatureName);
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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, true, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                double currentPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                await rankService.UpLevelAsync(cardMilitary, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                await CreateCardMilitaryEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardMilitary);
            }
        });
    }
    public async Task CreateCardSpellEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardSpells cardSpell)
    {
        Rank rank = await UserCardSpellsRankService.Create().GetCardSpellRankAsync(feature.FeatureName, cardSpell.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        rank.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        SetUI(slotObject, feature.FeatureName, rank.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, rank.Level, MAX_LEVEL);

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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, false, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardSpell, newRank, feature.FeatureName);
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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, true, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                double currentPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                await rankService.UpLevelAsync(cardSpell, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                await CreateCardSpellEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardSpell);
            }
        });
    }
    public async Task CreateCardMonstersEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardMonsters cardMonster)
    {
        Rank rank = await UserCardMonstersRankService.Create().GetCardMonsterRankAsync(feature.FeatureName, cardMonster.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        rank.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        SetUI(slotObject, feature.FeatureName, rank.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, rank.Level, MAX_LEVEL);

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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, false, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardMonster, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardMonstersEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardMonster);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, true, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardMonster, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardMonstersEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardMonster);
            }
        });
    }
    public async Task CreateCardColonelsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardColonels cardColonel)
    {
        Rank rank = await UserCardColonelsRankService.Create().GetCardColonelRankAsync(feature.FeatureName, cardColonel.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        rank.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        SetUI(slotObject, feature.FeatureName, rank.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, rank.Level, MAX_LEVEL);

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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, false, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardColonel, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardColonelsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardColonel);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, true, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardColonel, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardColonelsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardColonel);
            }
        });
    }
    public async Task CreateCardGeneralsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardGenerals cardGeneral)
    {
        Rank rank = await UserCardGeneralsRankService.Create().GetCardGeneralRankAsync(feature.FeatureName, cardGeneral.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        rank.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        SetUI(slotObject, feature.FeatureName, rank.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, rank.Level, MAX_LEVEL);

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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, false, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardGeneral, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardGeneralsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardGeneral);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, true, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardGeneral, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardGeneralsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardGeneral);
            }
        });
    }
    public async Task CreateCardAdmiralsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardAdmirals cardAdmiral)
    {
        Rank rank = await UserCardAdmiralsRankService.Create().GetCardAdmiralRankAsync(feature.FeatureName, cardAdmiral.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        rank.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        SetUI(slotObject, feature.FeatureName, rank.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, rank.Level, MAX_LEVEL);

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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, false, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardAdmiral, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardAdmiralsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardAdmiral);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, true, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardAdmiral, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardAdmiralsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardAdmiral);
            }
        });
    }
    public async Task CreateEquipmentsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, Equipments equipment)
    {
        Rank rank = await UserEquipmentsRankService.Create().GetEquipmentRankAsync(feature.FeatureName, equipment.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        rank.Id = feature.Id;

        Currencies currency = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        SetUI(slotObject, feature.FeatureName, rank.Level, type);
        SetMaterialUI(currentObject, item.Image, item.Quantity, currency.Quantity, rank.Level, MAX_LEVEL);

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
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, false, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(equipment, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateEquipmentsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, equipment);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = ItemHelper.CalculateLevelUp(item.Quantity, currency.Quantity, MATERIAL_PER_LEVEL, CURRENCY_PER_LEVEL, rank.Level, true, MAX_LEVEL);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(currency.Id, result.currencyLeft);

                await rankService.UpLevelAsync(equipment, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                PowerController.Instance.ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateEquipmentsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, equipment);
            }
        });
    }
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