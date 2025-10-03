using System.Collections;
using System.Collections.Generic;
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
        teamsService = TeamsService.Create();
        userItemsService = UserItemsService.Create();
        EuroStyleNormalFont = UIManager.Instance.GetTMPFontAsset("EuroStyleNormalFont");
        fontSize = 20;
        maxLevel = 10000;
    }
    public void CreateCardHeroesEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardHeroes cardHeroes)
    {
        Master master = UserCardHeroesMasterService.Create().GetCardHeroesMaster(mainType, cardHeroes.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, master.level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardHeroes, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardHeroesEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardHeroes);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardHeroes, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardHeroesEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardHeroes);
            }
        });
    }
    public void CreateBooksEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, Books books)
    {
        Master master = UserBooksMasterService.Create().GetBooksMaster(mainType, books.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, master.level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(books, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateBooksEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, books);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(books, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateBooksEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, books);
            }
        });
    }
    public void CreateCardCaptainsEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardCaptains cardCaptains)
    {
        Master master = UserCardCaptainsMasterService.Create().GetCardCaptainsMaster(mainType, cardCaptains.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, master.level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, 1);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardCaptains, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardCaptainsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardCaptains);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardCaptains, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardCaptainsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardCaptains);
            }
        });
    }
    public void CreatePetsEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, Pets pets)
    {
        Master master = UserPetsMasterService.Create().GetPetsMaster(mainType, pets.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, master.level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, 1);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(pets, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreatePetsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, pets);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(pets, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreatePetsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, pets);
            }
        });
    }
    public void CreateCardMilitaryEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardMilitary cardMilitary)
    {
        Master master = UserCardMilitaryMasterService.Create().GetCardMilitaryMaster(mainType, cardMilitary.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, master.level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardMilitary, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardMilitaryEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardMilitary);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardMilitary, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardMilitaryEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardMilitary);
            }
        });
    }
    public void CreateCardSpellEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardSpell cardSpell)
    {
        Master master = UserCardSpellMasterService.Create().GetCardSpellMaster(mainType, cardSpell.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, master.level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardSpell, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardSpellEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardSpell);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardSpell, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardSpellEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardSpell);
            }
        });
    }
    public void CreateCardMonstersEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardMonsters cardMonsters)
    {
        Master master = UserCardMonstersMasterService.Create().GetCardMonstersMaster(mainType, cardMonsters.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, master.level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardMonsters, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardMonstersEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardMonsters);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardMonsters, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardMonstersEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardMonsters);
            }
        });
    }
    public void CreateCardColonelsEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardColonels cardColonels)
    {
        Master master = UserCardColonelsMasterService.Create().GetCardColonelsMaster(mainType, cardColonels.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, master.level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardColonels, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardColonelsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardColonels);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardColonels, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardColonelsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardColonels);
            }
        });
    }
    public void CreateCardGeneralsEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardGenerals cardGenerals)
    {
        Master master = UserCardGeneralsMasterService.Create().GetCardGeneralsMaster(mainType, cardGenerals.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, master.level, maxLevel);

        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardGenerals, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardGeneralsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardGenerals);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardGenerals, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardGeneralsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardGenerals);
            }
        });
    }
    public void CreateCardAdmiralsEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardAdmirals cardAdmirals)
    {
        Master master = UserCardAdmiralsMasterService.Create().GetCardAdmiralsMaster(mainType, cardAdmirals.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        MasterService MasterService = new MasterService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, master.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, master.level, maxLevel);
        
        TextMeshProUGUI UpLevelButtonText = UpLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpLevelButtonText.font = EuroStyleNormalFont;
        UpLevelButtonText.fontSize = fontSize;
        UpLevelButtonText.fontStyle = FontStyles.Bold;
        UpLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);
        TextMeshProUGUI UpMaxLevelButtonText = UpMaxLevelButton.GetComponentInChildren<TextMeshProUGUI>();
        UpMaxLevelButtonText.font = EuroStyleNormalFont;
        UpMaxLevelButtonText.fontSize = fontSize;
        UpMaxLevelButtonText.fontStyle = FontStyles.Bold;
        UpMaxLevelButtonText.text = LocalizationManager.Get(AppDisplayConstants.MainType.UpOneLevel);

        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardAdmirals, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardAdmiralsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardAdmirals);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, master.level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Master newMaster = new Master();
                newMaster = MasterService.EnhanceMaster(master, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);
                
                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                MasterService.UpLevel(cardAdmirals, newMaster, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardAdmiralsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardAdmirals);
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