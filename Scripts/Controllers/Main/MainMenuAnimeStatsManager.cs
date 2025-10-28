using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuAnimeStatsManager : MonoBehaviour
{
    private Transform MainPanel;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private GameObject MainMenuAnimePanelPrefab;
    private GameObject AnimeSlotPrefab;
    private GameObject buttonPrefab;
    private GameObject currentObject;
    private GameObject slotObject;
    private GameObject ElementDetails2Prefab;
    private Button UpLevelButton;
    private Button UpMaxLevelButton;
    private Transform LevelCondition;
    private string animeType;
    private string mainType;
    UserItemsService userItemsService;
    TeamsService teamsService;
    private int maxLevel;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        MainMenuAnimePanelPrefab = UIManager.Instance.GetGameObject("MainMenuAnimePanelPrefab");
        buttonPrefab = UIManager.Instance.GetGameObject("TabButton");
        AnimeSlotPrefab = UIManager.Instance.GetGameObject("AnimeSlotPrefab");
        ElementDetails2Prefab = UIManager.Instance.GetGameObject("ElementDetails2Prefab");

        userItemsService = UserItemsService.Create();
        teamsService = TeamsService.Create();
        maxLevel = 10000;
    }
    public void CreateAnimeButton(Transform arenaMenuPanel)
    {
        Button[] buttons = arenaMenuPanel.gameObject.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            string buttonName = button.name; // Lưu lại giá trị cục bộ để tránh lỗi closure
            button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                CreateMainMenuAnimeStatsManager(buttonName);
            });
        }
    }
    public void CreateMainMenuAnimeStatsManager(string nameType)
    {
        animeType = nameType;
        currentObject = Instantiate(MainMenuAnimePanelPrefab, MainPanel);
        TabButtonPanel = currentObject.transform.Find("Scroll View/Viewport/Content");
        SlotPanel = currentObject.transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = currentObject.transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(nameType);
        UpLevelButton = currentObject.transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        UpMaxLevelButton = currentObject.transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Button CloseButton = currentObject.transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button HomeButton = currentObject.transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            ButtonEvent.Instance.Close(MainPanel);
        });
        CloseButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            Destroy(currentObject);
        });

        LevelCondition = currentObject.transform.Find("DictionaryCards/LevelCondition");

        Dictionary<string, int> uniqueTypes = new Dictionary<string, int>();
        Features features = new Features();
        uniqueTypes = FeaturesService.Create().GetFeaturesByType(LocalizationManager.Get(nameType));
        if (uniqueTypes.Count > 0)
        {
            int index = 0;
            foreach (var kvp in uniqueTypes)
            {
                // Tạo một nút mới từ prefab
                string subtype = kvp.Key;
                int value = kvp.Value;
                GameObject button = Instantiate(buttonPrefab, TabButtonPanel);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
                    OnButtonClick(button, subtype, value);
                });

                if (index == 0)
                {
                    mainType = subtype;
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    // mainId = cardHeroes.id;
                    CreateAnimeStats();
                    if (User.CurrentUserLevel >= value)
                    {
                        LevelCondition.gameObject.SetActive(false);
                    }
                    else
                    {
                        LevelCondition.gameObject.SetActive(true);
                        TextMeshProUGUI warningText = LevelCondition.Find("WarningText").GetComponent<TextMeshProUGUI>();
                        warningText.text = MessageHelper.WaringLevel(value);
                    }
                }
                else
                {
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                }
                ButtonEvent.Instance.CheckLockedButton("AnimeStats",value, button);
                index = index + 1;
            }
        }
    }
    void OnButtonClick(GameObject clickedButton, string type, int value)
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

        // mainId = cardHeroes.id;
        CreateAnimeStats();
        if (User.CurrentUserLevel >= value)
        {
            LevelCondition.gameObject.SetActive(false);
        }
        else
        {
            LevelCondition.gameObject.SetActive(true);
            TextMeshProUGUI warningText = LevelCondition.Find("WarningText").GetComponent<TextMeshProUGUI>();
            warningText.text = MessageHelper.WaringLevel(value);
        }
    }
    public void CreateAnimeStats()
    {
        var animeStatsService = AnimeStatsService.Create();
        AnimeStats animeStats = animeStatsService.GetAnimeStats(mainType, User.CurrentUserId);
        slotObject = Instantiate(AnimeSlotPrefab, SlotPanel);

        Currencies silver = UserCurrencyService.Create().GetUserCurrencyByName(AppConstants.Currency.SILVER);

        string itemName = animeType + " " + mainType;
        Items item = userItemsService.GetUserItemByName(itemName);
        SetUI(slotObject, mainType, animeStats.Level);
        SetMaterialUI(currentObject, mainType, item, silver.Quantity, animeStats.Level);
        UpLevelButton.onClick.RemoveAllListeners();
        UpMaxLevelButton.onClick.RemoveAllListeners();
        UpLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);

            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, animeStats.Level, false, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                AnimeStats newanimeStats = new AnimeStats();
                newanimeStats = EnhanceAnimeStats(animeStats, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.Id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                UpLevel(newanimeStats, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateAnimeStats();
            }
        });
        UpMaxLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK);
            
            var result = EvaluateItem.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, animeStats.Level, true, maxLevel);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                userItemsService.UpdateUserItemsQuantity(item);
                AnimeStats newanimeStats = new AnimeStats();
                newanimeStats = EnhanceAnimeStats(animeStats, result.levelsGained);
                UserCurrencyService.Create().UpdateUserCurrency(silver.Id, result.currencyLeft);

                double currentPower = teamsService.GetTeamsPower(User.CurrentUserId);
                UpLevel(newanimeStats, mainType);
                double newPower = teamsService.GetTeamsPower(User.CurrentUserId);
                FindObjectOfType<Power>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(slotObject);
                CreateAnimeStats();
            }
        });
    }
    public void SetUI(GameObject gameObject, string type, int level = 0)
    {
        RawImage BackgroundImage = gameObject.transform.Find("Background").GetComponent<RawImage>();
        Texture backgroundTexture = Resources.Load<Texture>("UI/Background3/Anime");
        BackgroundImage.texture = backgroundTexture;
        
        Transform backgroundTransform = gameObject.transform.Find("BackgroundCircle");
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
            Transform aptitudeSkill = gameObject.transform.Find($"UpgradeSkill{i}");
            if (aptitudeSkill == null) continue;

            RawImage aptitudeImage = aptitudeSkill.Find("AptitudeImage").GetComponent<RawImage>();
            TextMeshProUGUI levelText = aptitudeSkill.Find("LevelText").GetComponent<TextMeshProUGUI>();

            Texture texture = Resources.Load<Texture>($"UI/animeStats/Anime");
            aptitudeImage.texture = texture;

            if (aptitudeImage != null) aptitudeImage.color = Color.black;
            if (levelText != null) levelText.text = $"0/{levelsPerSkill}";
        }

        // Xác định số kỹ năng được kích hoạt
        int activeSkillsCount = Mathf.Clamp((level / levelsPerSkill), 1, totalSkills);
        for (int i = 1; i <= activeSkillsCount; i++)
        {
            Transform activeSkill = gameObject.transform.Find($"UpgradeSkill{i}");
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
        TextMeshProUGUI LevelText = gameObject.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        LevelText.text = level.ToString();
    }
    public void SetMaterialUI(GameObject gameObject, string type, Items item, double currencyQuantity, int rankLevel)
    {
        Transform currencyPanel = gameObject.transform.Find("DictionaryCards/Currency");
        List<Currencies> currencies = UserCurrencyService.Create().GetUserCurrency();
        ButtonEvent.Instance.Close(currencyPanel);
        CurrencyManager.Instance.GetMainCurrency(currencies, currencyPanel);

        var oneResult = EvaluateItem.CalculateLevelUp(item.Quantity, currencyQuantity, 1, 10, rankLevel, false, maxLevel);
        var maxResult = EvaluateItem.CalculateLevelUp(item.Quantity, currencyQuantity, 1, 10, rankLevel, true, maxLevel);
        RawImage OneLevelCurrencyImage = gameObject.transform.Find("DictionaryCards/OneLevelCurrency/CurrencyImage").GetComponent<RawImage>();
        RawImage MaxLevelCurrencyImage = gameObject.transform.Find("DictionaryCards/MaxLevelCurrency/CurrencyImage").GetComponent<RawImage>();
        Texture OneLevelCurrencyTexture = Resources.Load<Texture>($"{ImageConstants.Currency.SILVER}");
        Texture MaxLevelCurrencyTexture = Resources.Load<Texture>($"{ImageConstants.Currency.SILVER}");
        OneLevelCurrencyImage.texture = OneLevelCurrencyTexture;
        MaxLevelCurrencyImage.texture = MaxLevelCurrencyTexture;

        TextMeshProUGUI OneLevelCurrencyText = gameObject.transform.Find("DictionaryCards/OneLevelCurrency/QuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI MaxLevelCurrencyText = gameObject.transform.Find("DictionaryCards/MaxLevelCurrency/QuantityText").GetComponent<TextMeshProUGUI>();
        OneLevelCurrencyText.text = oneResult.totalCurrencyUsed.ToString();
        MaxLevelCurrencyText.text = maxResult.totalCurrencyUsed.ToString();

        Transform OneLevelMaterial = currentObject.transform.Find("DictionaryCards/OneLevelMaterial");
        Transform MaxLevelMaterial = currentObject.transform.Find("DictionaryCards/MaxLevelMaterial");
        ButtonEvent.Instance.Close(OneLevelMaterial);
        ButtonEvent.Instance.Close(MaxLevelMaterial);
        GameObject oneLevelMaterialObject = Instantiate(ElementDetails2Prefab, OneLevelMaterial);
        GameObject maxLevelMaterialObject = Instantiate(ElementDetails2Prefab, MaxLevelMaterial);

        RawImage oneLevelImage = oneLevelMaterialObject.transform.Find("MaterialImage").GetComponent<RawImage>();
        Texture oneLevelTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(item.Image)}");
        oneLevelImage.texture = oneLevelTexture;

        RectTransform oneLevelRectTransform = oneLevelImage.GetComponent<RectTransform>();
        oneLevelRectTransform.sizeDelta = new Vector2(40, 40);

        TextMeshProUGUI oneLevelQuantity = oneLevelMaterialObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        oneLevelQuantity.text = item.Quantity + "/" + oneResult.totalMaterialUsed;

        RawImage maxLevelImage = maxLevelMaterialObject.transform.Find("MaterialImage").GetComponent<RawImage>();
        Texture maxLevelTexture = Resources.Load<Texture>($"{ImageExtensionHandler.RemoveImageExtension(item.Image)}");
        maxLevelImage.texture = maxLevelTexture;

        TextMeshProUGUI maxLevelQuantity = maxLevelMaterialObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        maxLevelQuantity.text = item.Quantity + "/" + maxResult.totalMaterialUsed;

        RectTransform maxLevelRectTransform = maxLevelImage.GetComponent<RectTransform>();
        maxLevelRectTransform.sizeDelta = new Vector2(40, 40);
    }
    public AnimeStats EnhanceAnimeStats(AnimeStats animeStats, int level)
    {
        int startLevel = animeStats.Level;
        int endLevel = startLevel + level;

        for (int lvl = startLevel; lvl < endLevel; lvl++)
        {
            int statMultiplier = 1;  // Hệ số nhân dựa trên cấp độ hiện tại

            if (lvl >= 0 && lvl <= 500)
            {
                animeStats.Health += 10000000 * statMultiplier;
            }
            else if (lvl > 500 && lvl <= 1000)
            {
                animeStats.PhysicalAttack += 1500000 * statMultiplier;
                animeStats.PhysicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1000 && lvl <= 1500)
            {
                animeStats.MagicalAttack += 1500000 * statMultiplier;
                animeStats.MagicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 1500 && lvl <= 2000)
            {
                animeStats.ChemicalAttack += 1500000 * statMultiplier;
                animeStats.ChemicalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2000 && lvl <= 2500)
            {
                animeStats.AtomicAttack += 1500000 * statMultiplier;
                animeStats.AtomicDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 2500 && lvl <= 3000)
            {
                animeStats.MentalAttack += 1500000 * statMultiplier;
                animeStats.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 3000 && lvl <= 3500)
            {
                animeStats.Speed += 1500000 * statMultiplier;
                animeStats.CriticalDamageRate += 0.1 * statMultiplier;
                animeStats.CriticalRate += 0.1 * statMultiplier;
                animeStats.CriticalResistanceRate += 0.1 * statMultiplier;
                animeStats.IgnoreCriticalRate += 0.1 * statMultiplier;
                animeStats.PenetrationRate += 0.1 * statMultiplier;
                animeStats.PenetrationResistanceRate += 0.1 * statMultiplier;
            }
            else if (lvl > 3500 && lvl <= 4000)
            {
                animeStats.EvasionRate += 0.1 * statMultiplier;
                animeStats.DamageAbsorptionRate += 0.1 * statMultiplier;
                animeStats.IgnoreDamageAbsorptionRate += 0.1 * statMultiplier;
                animeStats.AbsorbedDamageRate += 0.1 * statMultiplier;
                animeStats.VitalityRegenerationRate += 0.1 * statMultiplier;
                animeStats.VitalityRegenerationResistanceRate += 0.1 * statMultiplier;
                animeStats.AccuracyRate += 0.1 * statMultiplier;
            }
            else if (lvl > 4000 && lvl <= 4500)
            {
                animeStats.LifestealRate += 0.1 * statMultiplier;
                animeStats.Mana += 1500000 * statMultiplier;
                animeStats.ManaRegenerationRate += 0.1 * statMultiplier;
                animeStats.ShieldStrength += 1500000 * statMultiplier;
            }
            else if (lvl > 4500 && lvl <= 5000)
            {
                animeStats.Tenacity += 0.5 * statMultiplier;
                animeStats.ResistanceRate += 0.1 * statMultiplier;
                animeStats.ComboRate += 0.1 * statMultiplier;
                animeStats.IgnoreComboRate += 0.1 * statMultiplier;
                animeStats.ComboDamageRate += 0.1 * statMultiplier;
                animeStats.ComboResistanceRate += 0.1 * statMultiplier;
                animeStats.StunRate += 0.1 * statMultiplier;
                animeStats.IgnoreStunRate += 0.1 * statMultiplier;
            }
            else if (lvl > 5000 && lvl <= 5500)
            {
                animeStats.ReflectionRate += 0.1 * statMultiplier;
                animeStats.IgnoreReflectionRate += 0.1 * statMultiplier;
                animeStats.ReflectionDamageRate += 0.1 * statMultiplier;
                animeStats.ReflectionResistanceRate += 0.1 * statMultiplier;
                animeStats.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                animeStats.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                animeStats.DamageToSameFactionRate += 0.1 * statMultiplier;
                animeStats.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
            else if (lvl > 6000 && lvl <= 6500)
            {
                animeStats.NormalDamageRate += 0.1 * statMultiplier;
                animeStats.NormalResistanceRate += 0.1 * statMultiplier;
                animeStats.SkillDamageRate += 0.1 * statMultiplier;
                animeStats.SkillResistanceRate += 0.1 * statMultiplier;
                animeStats.PercentAllHealth += 5 * statMultiplier;
                animeStats.PercentAllPhysicalAttack += 5 * statMultiplier;
                animeStats.PercentAllPhysicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 6500 && lvl <= 7000)
            {
                animeStats.PercentAllMagicalAttack += 5 * statMultiplier;
                animeStats.PercentAllMagicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7000 && lvl <= 7500)
            {
                animeStats.PercentAllChemicalAttack += 5 * statMultiplier;
                animeStats.PercentAllChemicalDefense += 5 * statMultiplier;
            }
            else if (lvl > 7500 && lvl <= 8000)
            {
                animeStats.PercentAllAtomicAttack += 5 * statMultiplier;
                animeStats.PercentAllAtomicDefense += 5 * statMultiplier;
            }
            else if (lvl > 8000 && lvl <= 8500)
            {
                animeStats.PercentAllMentalAttack += 5 * statMultiplier;
                animeStats.PercentAllMentalDefense += 5 * statMultiplier;
            }
            else if (lvl > 8500 && lvl <= 9000)
            {
                animeStats.PhysicalAttack += 1500000 * statMultiplier;
                animeStats.MagicalAttack += 1500000 * statMultiplier;
                animeStats.ChemicalAttack += 1500000 * statMultiplier;
                animeStats.AtomicAttack += 1500000 * statMultiplier;
                animeStats.MentalAttack += 1500000 * statMultiplier;
            }
            else if (lvl > 9000 && lvl <= 9500)
            {
                animeStats.PhysicalDefense += 1500000 * statMultiplier;
                animeStats.MagicalDefense += 1500000 * statMultiplier;
                animeStats.ChemicalDefense += 1500000 * statMultiplier;
                animeStats.AtomicDefense += 1500000 * statMultiplier;
                animeStats.MentalDefense += 1500000 * statMultiplier;
            }
            else if (lvl > 9500 && lvl <= 10000)
            {
                animeStats.Speed += 1500000 * statMultiplier;
                animeStats.CriticalDamageRate += 0.1 * statMultiplier;
                animeStats.CriticalRate += 0.1 * statMultiplier;
                animeStats.PenetrationRate += 0.1 * statMultiplier;
                animeStats.EvasionRate += 0.1 * statMultiplier;
                animeStats.DamageAbsorptionRate += 0.1 * statMultiplier;
                animeStats.VitalityRegenerationRate += 0.1 * statMultiplier;
                animeStats.AccuracyRate += 0.1 * statMultiplier;
                animeStats.LifestealRate += 0.1 * statMultiplier;
                animeStats.Mana += 1500000 * statMultiplier;
                animeStats.ManaRegenerationRate += 0.1 * statMultiplier;
                animeStats.ShieldStrength += 1500000 * statMultiplier;
                animeStats.Tenacity += 0.5 * statMultiplier;
                animeStats.ResistanceRate += 0.1 * statMultiplier;
                animeStats.ComboRate += 0.1 * statMultiplier;
                animeStats.ReflectionRate += 0.1 * statMultiplier;
                animeStats.DamageToDifferentFactionRate += 0.1 * statMultiplier;
                animeStats.ResistanceToDifferentFactionRate += 0.1 * statMultiplier;
                animeStats.DamageToSameFactionRate += 0.1 * statMultiplier;
                animeStats.ResistanceToSameFactionRate += 0.1 * statMultiplier;
            }
        }

        animeStats.Level = endLevel; // Cập nhật cấp độ cuối cùng sau khi nâng cấp
        return animeStats;
    }
    public void UpLevel(AnimeStats animeStats, string type)
    {
        AnimeStatsService.Create().InsertOrUpdateAnimeStats(animeStats, type, User.CurrentUserId);
    }
}
