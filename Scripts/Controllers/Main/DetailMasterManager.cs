using System.Collections;
using System.Collections.Generic;
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
        Master master = await UserCardHeroesMasterService.Create().GetCardHeroMasterAsync(mainType, cardHeroes.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, master.Level, maxLevel);

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
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await MasterService.UpLevelAsync(cardHeroes, newMaster, mainType);
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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await MasterService.UpLevelAsync(cardHeroes, newMaster, mainType);
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
        Master master = await UserBooksMasterService.Create().GetBookMasterAsync(mainType, books.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, master.Level, maxLevel);

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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(books, newMaster, mainType);
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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(books, newMaster, mainType);
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
        Master master = await UserCardCaptainsMasterService.Create().GetCardCaptainMasterAsync(mainType, cardCaptains.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, master.Level, maxLevel);

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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, 1);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardCaptains, newMaster, mainType);
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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardCaptains, newMaster, mainType);
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
        Master master = await UserPetsMasterService.Create().GetPetMasterAsync(mainType, pets.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, master.Level, maxLevel);

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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, 1);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(pets, newMaster, mainType);
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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                await MasterService.UpLevelAsync(pets, newMaster, mainType);
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
        Master master = await UserCardMilitariesMasterService.Create().GetCardMilitaryMasterAsync(mainType, cardMilitary.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, master.Level, maxLevel);

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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardMilitary, newMaster, mainType);
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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardMilitary, newMaster, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardMilitaryEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardMilitary);
            }
        });
    }
    public async Task CreateCardSpellEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardSpells cardSpell)
    {
        Master master = await UserCardSpellsMasterService.Create().GetCardSpellMasterAsync(mainType, cardSpell.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, master.Level, maxLevel);

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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardSpell, newMaster, mainType);
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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardSpell, newMaster, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardSpellEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardSpell);
            }
        });
    }
    public async Task CreateCardMonstersEquipmentsAsync(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardMonsters cardMonsters)
    {
        Master master = await UserCardMonstersMasterService.Create().GetCardMonsterMasterAsync(mainType, cardMonsters.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, master.Level, maxLevel);

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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardMonsters, newMaster, mainType);
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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardMonsters, newMaster, mainType);
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
        Master master = await UserCardColonelsMasterService.Create().GetCardColonelMasterAsync(mainType, cardColonels.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, master.Level, maxLevel);

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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardColonels, newMaster, mainType);
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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardColonels, newMaster, mainType);
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
        Master master = await UserCardGeneralsMasterService.Create().GetCardGeneralMasterAsync(mainType, cardGenerals.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, master.Level, maxLevel);

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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardGenerals, newMaster, mainType);
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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardGenerals, newMaster, mainType);
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
        Master master = await UserCardAdmiralsMasterService.Create().GetCardAdmiralMasterAsync(mainType, cardAdmirals.Id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = await userItemsService.GetUserItemByNameAsync(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.Level, type);
        await UIManager.Instance.SetMaterialUIAsync(currentObject, item.Image, item.Quantity, silver.Quantity, master.Level, maxLevel);
        
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

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardAdmirals, newMaster, mainType);
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
            
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, master.Level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);
                
                await MasterService.UpLevelAsync(cardAdmirals, newMaster, mainType);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);

                Destroy(slotObject);
                await CreateCardAdmiralsEquipmentsAsync(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardAdmirals);
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