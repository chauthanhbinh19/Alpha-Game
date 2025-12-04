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
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardHeroes cardHeroes)
    {
        Rank rank = await UserCardHeroesRankService.Create().GetCardHeroRankAsync(mainType, cardHeroes.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardHeroes, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardHeroesEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardHeroes);
            }
        });
        UpMaxLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardHeroes, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardHeroesEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardHeroes);
            }
        });
    }
    public async Task CreateBooksEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, Books books)
    {
        Rank rank = await UserBooksRankService.Create().GetBookRankAsync(mainType, books.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(books, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateBooksEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, books);
            }
        });
        UpMaxLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(books, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateBooksEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, books);
            }
        });
    }
    public async Task CreateCardCaptainsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardCaptains cardCaptains)
    {
        Rank rank = await UserCardCaptainsRankService.Create().GetCardCaptainRankAsync(mainType, cardCaptains.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardCaptains, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardCaptainsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardCaptains);
            }
        });
        UpMaxLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardCaptains, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardCaptainsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardCaptains);
            }
        });
    }
    public async Task CreatePetsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, Pets pets)
    {
        Rank rank = await UserPetsRankService.Create().GetPetRankAsync(mainType, pets.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(pets, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreatePetsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, pets);
            }
        });
        UpMaxLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(pets, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreatePetsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, pets);
            }
        });
    }
    public async Task CreateCardMilitaryEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardMilitaries cardMilitary)
    {
        Rank rank = await UserCardMilitariesRankService.Create().GetCardMilitaryRankAsync(mainType, cardMilitary.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardMilitary, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardMilitaryEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardMilitary);
            }
        });
        UpMaxLevelButton.onClick.AddListener(async () =>
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
                await rankService.UpLevelAsync(cardMilitary, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                await CreateCardMilitaryEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardMilitary);
            }
        });
    }
    public async Task CreateCardSpellEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardSpells cardSpell)
    {
        Rank rank = await UserCardSpellsRankService.Create().GetCardSpellRankAsync(mainType, cardSpell.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardSpell, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardSpellEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardSpell);
            }
        });
        UpMaxLevelButton.onClick.AddListener(async () =>
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
                await rankService.UpLevelAsync(cardSpell, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                await CreateCardSpellEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardSpell);
            }
        });
    }
    public async Task CreateCardMonstersEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardMonsters cardMonsters)
    {
        Rank rank = await UserCardMonstersRankService.Create().GetCardMonsterRankAsync(mainType, cardMonsters.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardMonsters, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardMonstersEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardMonsters);
            }
        });
        UpMaxLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardMonsters, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardMonstersEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardMonsters);
            }
        });
    }
    public async Task CreateCardColonelsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardColonels cardColonels)
    {
        Rank rank = await UserCardColonelsRankService.Create().GetCardColonelRankAsync(mainType, cardColonels.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardColonels, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardColonelsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardColonels);
            }
        });
        UpMaxLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardColonels, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardColonelsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardColonels);
            }
        });
    }
    public async Task CreateCardGeneralsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardGenerals cardGenerals)
    {
        Rank rank = await UserCardGeneralsRankService.Create().GetCardGeneralRankAsync(mainType, cardGenerals.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardGenerals, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardGeneralsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardGenerals);
            }
        });
        UpMaxLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardGenerals, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardGeneralsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardGenerals);
            }
        });
    }
    public async Task CreateCardAdmiralsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardAdmirals cardAdmirals)
    {
        Rank rank = await UserCardAdmiralsRankService.Create().GetCardAdmiralRankAsync(mainType, cardAdmirals.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardAdmirals, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardAdmiralsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardAdmirals);
            }
        });
        UpMaxLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(cardAdmirals, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardAdmiralsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardAdmirals);
            }
        });
    }
    public async Task CreateEquipmentsEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, Equipments equipments)
    {
        Rank rank = await UserEquipmentsRankService.Create().GetEquipmentRankAsync(mainType, equipments.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        RankService rankService = new RankService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, rank.Level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UP_ONE_LEVEL);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(equipments, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateEquipmentsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, equipments);
            }
        });
        UpMaxLevelButton.onClick.AddListener(async () =>
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

                await rankService.UpLevelAsync(equipments, newRank, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateEquipmentsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, equipments);
            }
        });
    }
}