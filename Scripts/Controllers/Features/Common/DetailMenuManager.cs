using System.Collections;
using System.Collections.Generic;
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
    public void CreateCardHeroesEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, CardHeroes cardHeroes)
    {
        Rank rank = UserCardHeroesRankService.Create().GetCardHeroesRank(mainType, cardHeroes.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        RankService rankService = new RankService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, rank.level, maxLevel);

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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardHeroes, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardHeroesEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardHeroes);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardHeroes, newRank, mainType);
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
        Rank rank = UserBooksRankService.Create().GetBooksRank(mainType, books.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        RankService rankService = new RankService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, rank.level, maxLevel);

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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(books, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateBooksEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, books);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(books, newRank, mainType);
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
        Rank rank = UserCardCaptainsRankService.Create().GetCardCaptainsRank(mainType, cardCaptains.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        RankService rankService = new RankService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, rank.level, maxLevel);

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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardCaptains, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardCaptainsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardCaptains);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardCaptains, newRank, mainType);
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
        Rank rank = UserPetsRankService.Create().GetPetsRank(mainType, pets.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        RankService rankService = new RankService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, rank.level, maxLevel);

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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(pets, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreatePetsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, pets);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(pets, newRank, mainType);
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
        Rank rank = UserCardMilitaryRankService.Create().GetCardMilitaryRank(mainType, cardMilitary.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        RankService rankService = new RankService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, rank.level, maxLevel);

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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardMilitary, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardMilitaryEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardMilitary);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardMilitary, newRank, mainType);
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
        Rank rank = UserCardSpellRankService.Create().GetCardSpellRank(mainType, cardSpell.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        RankService rankService = new RankService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, rank.level, maxLevel);

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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardSpell, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardSpellEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardSpell);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardSpell, newRank, mainType);
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
        Rank rank = UserCardMonstersRankService.Create().GetCardMonstersRank(mainType, cardMonsters.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        RankService rankService = new RankService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, rank.level, maxLevel);

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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardMonsters, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardMonstersEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardMonsters);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardMonsters, newRank, mainType);
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
        Rank rank = UserCardColonelsRankService.Create().GetCardColonelsRank(mainType, cardColonels.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        RankService rankService = new RankService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, rank.level, maxLevel);

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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardColonels, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardColonelsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardColonels);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardColonels, newRank, mainType);
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
        Rank rank = UserCardGeneralsRankService.Create().GetCardGeneralsRank(mainType, cardGenerals.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        RankService rankService = new RankService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, rank.level, maxLevel);

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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardGenerals, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardGeneralsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardGenerals);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardGenerals, newRank, mainType);
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
        Rank rank = UserCardAdmiralsRankService.Create().GetCardAdmiralsRank(mainType, cardAdmirals.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        RankService rankService = new RankService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, rank.level, maxLevel);

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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardAdmirals, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardAdmiralsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardAdmirals);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(cardAdmirals, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateCardAdmiralsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, cardAdmirals);
            }
        });
    }
    public void CreateEquipmentsEquipments(GameObject prefab, Transform SlotPanel, GameObject currentObject,
     Button UpLevelButton, Button UpMaxLevelButton, string mainType, string type, Equipments equipments)
    {
        Rank rank = UserEquipmentsRankService.Create().GetEquipmentsRank(mainType, equipments.id);
        GameObject slotObject = Instantiate(prefab, SlotPanel);

        Currency silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.Silver);

        Items item = new Items();
        RankService rankService = new RankService();

        item = userItemsService.GetUserItemByName(mainType);
        UIManager.Instance.SetUI(slotObject, mainType, rank.level, type);
        UIManager.Instance.SetMaterialUI(currentObject, item.image, item.quantity, silver.quantity, rank.level, maxLevel);

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
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, false, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(equipments, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateEquipmentsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, equipments);
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.ButtonClick);
            var result = EvaluateItem.CalculateLevelUp(item.quantity, silver.quantity, 1, 10, rank.level, true, maxLevel);

            if (result.message.Equals(AppConstants.Status.Success))
            {
                item.quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                Rank newRank = new Rank();
                newRank = rankService.EnhanceRank(rank, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                rankService.UpLevel(equipments, newRank, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateEquipmentsEquipments(prefab, SlotPanel, currentObject, UpLevelButton, UpMaxLevelButton, mainType, type, equipments);
            }
        });
    }
}