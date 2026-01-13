using System.Collections;
using System.Collections.Generic;
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
    private int fontSize;
    private int maxLevel;
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
        fontSize = 20;
        maxLevel = 10000;
    }
    public async Task CreateCardHeroesEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button upLevelButton, Button upMaxLevelButton, Features feature, string type, CardHeroes cardHero)
    {
        Rank rank = await UserCardHeroesRankService.Create().GetCardHeroRankAsync(feature.FeatureName, cardHero.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);
        rank.Id = feature.Id;

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, rank.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardHero, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardHeroesEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardHero);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardHero, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

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

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, rank.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(book, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateBooksEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, book);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(book, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

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

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, rank.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardCaptain, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardCaptainsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardCaptain);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardCaptain, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

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

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, rank.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(pet, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreatePetsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, pet);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(pet, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

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

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, rank.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardMilitary, newRank, feature.FeatureName);
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
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                double currentPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                await rankService.UpLevelAsync(cardMilitary, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
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

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, rank.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardSpell, newRank, feature.FeatureName);
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
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                double currentPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                await rankService.UpLevelAsync(cardSpell, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
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

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, rank.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardMonster, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardMonstersEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardMonster);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardMonster, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

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

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, rank.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardColonel, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardColonelsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardColonel);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardColonel, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

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

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, rank.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardGeneral, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardGeneralsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardGeneral);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardGeneral, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

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

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, rank.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardAdmiral, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardAdmiralsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, cardAdmiral);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(cardAdmiral, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

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

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(feature.FeatureName);
        UIManager.Instance.SetUI(slotObject, feature.FeatureName, rank.Level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = upLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = upMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(equipment, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateEquipmentsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, equipment);
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, rank.Level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await rankService.UpLevelAsync(equipment, newRank, feature.FeatureName);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateEquipmentsEquipmentsAsync(prefab, SlotPanel, currentObject, upLevelButton, upMaxLevelButton, feature, type, equipment);
            }
        });
    }
}