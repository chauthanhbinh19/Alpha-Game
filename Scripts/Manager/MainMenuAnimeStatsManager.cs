using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MainMenuAnimeStatsManager : MonoBehaviour
{
    private Transform MainPanel;
    private GameObject AnimeButtonPrefab;
    private Transform TabButtonPanel;
    private Transform SlotPanel;
    private GameObject MainMenuAnimePanelPrefab;
    private GameObject AnimeSlotPrefab;
    private GameObject TypeButtonPrefab;
    private GameObject currentObject;
    private GameObject SlotObject;
    private Button upLevelButton;
    private Button upMaxLevelButton;
    private Transform LevelCondition;
    private string animeType;
    private Features feature;
    UserItemsService userItemsService;
    TeamsService teamsService;
    Texture2D itemBackground;
    private const int MAX_LEVEL = 10000;
    public static MainMenuAnimeStatsManager Instance { get; private set; }
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
        MainMenuAnimePanelPrefab = UIManager.Instance.Get("MainMenuAnimePanelPrefab");
        TypeButtonPrefab = UIManager.Instance.Get("TypeButtonPrefab");
        AnimeSlotPrefab = UIManager.Instance.Get("AnimeSlotPrefab");
        AnimeButtonPrefab = UIManager.Instance.Get("AnimeButtonPrefab");

        userItemsService = UserItemsService.Create();
        teamsService = TeamsService.Create();
    }
    public void CreateAnimeButton(Transform animeMenuPanel)
    {
        
        CreateAnimeButtonUI(AppDisplayConstants.Anime.ONE_PIECE, AppConstants.Anime.ONE_PIECE, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.ONE_PIECE_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.NARUTO, AppConstants.Anime.NARUTO, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.NARUTO_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.DRAGON_BALL, AppConstants.Anime.DRAGON_BALL, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.DRAGON_BALL_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.FAIRY_TAIL, AppConstants.Anime.FAIRY_TAIL, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.FAIRY_TAIL_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.SWORD_ART_ONLINE, AppConstants.Anime.SWORD_ART_ONLINE, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.SWORD_ART_ONLINE_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.DEMON_SLAYER, AppConstants.Anime.DEMON_SLAYER, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.DEMON_SLAYER_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.BLEACH, AppConstants.Anime.BLEACH, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.BLEACH_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.JUJUTSU_KAISEN, AppConstants.Anime.JUJUTSU_KAISEN, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.JUJUTSU_KAISEN_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.BLACK_CLOVER, AppConstants.Anime.BLACK_CLOVER, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.BLACK_CLOVER_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.HUNTER_X_HUNTER, AppConstants.Anime.HUNTER_X_HUNTER, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.HUNTER_X_HUNTER_URL), animeMenuPanel);
        CreateAnimeButtonUI(AppDisplayConstants.Anime.ONE_PUNCH_MAN, AppConstants.Anime.ONE_PUNCH_MAN, itemBackground, TextureHelper.LoadTexture2DCached(ImageConstants.Anime.ONE_PUNCH_MAN_URL), animeMenuPanel);

        FindAnyObjectByType<MainMenuAnimeStatsManager>().CreateAnimeButtonEvent(animeMenuPanel);
        animeMenuPanel.gameObject.AddComponent<StaggeredSlideAnimation>();
    }
    private void CreateAnimeButtonUI(string itemDisplayName, string itemName, Texture2D itemBackground, Texture2D itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(AnimeButtonPrefab, panel);
        Transform transform = newButton.transform;
        newButton.name = itemName;

        // Gán màu cho itemBackground
        // RawImage  background = newButton.transform.Find("ItemBackground").GetComponent<RawImage>();
        // if (background != null && itemBackground != null)
        // {
        //     background.texture = itemBackground;
        // }

        // Gán hình ảnh cho itemImage
        RawImage image = transform.Find("Image").GetComponent<RawImage>();
        if (image != null && itemImage != null)
        {
            image.texture = itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemDisplayName);
        }

        //Tạo animation cho border image
        RawImage borderImage = transform.Find("BorderImage").GetComponent<RawImage>();
        // Gán script RotateUI
        borderImage.gameObject.AddComponent<RotateAnimation>();
    }
    public void CreateAnimeButtonEvent(Transform arenaMenuPanel)
    {
        Button[] buttons = arenaMenuPanel.gameObject.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            string buttonName = button.name; // Lưu lại giá trị cục bộ để tránh lỗi closure
            button.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                await CreateMainMenuAnimeStatsManagerAsync(buttonName);
            });
        }
    }
    public async Task CreateMainMenuAnimeStatsManagerAsync(string nameType)
    {
        animeType = nameType;
        currentObject = Instantiate(MainMenuAnimePanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        TabButtonPanel = transform.Find("Scroll View/Viewport/Content");
        SlotPanel = transform.Find("DictionaryCards/Slot");
        TextMeshProUGUI titleText = transform.Find("DictionaryCards/Title").GetComponent<TextMeshProUGUI>();
        titleText.text = LocalizationManager.Get(nameType);
        upLevelButton = transform.Find("DictionaryCards/UpLevelButton").GetComponent<Button>();
        upMaxLevelButton = transform.Find("DictionaryCards/UpMaxLevelButton").GetComponent<Button>();
        Button closeButton = transform.Find("DictionaryCards/CloseButton").GetComponent<Button>();
        Button homeButton = transform.Find("DictionaryCards/HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);
        });
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });

        LevelCondition = transform.Find("DictionaryCards/LevelCondition");

        Dictionary<string, Features> uniqueTypes = new Dictionary<string, Features>();
        uniqueTypes = await FeaturesService.Create().GetFeaturesByTypeAsync(LocalizationManager.Get(nameType));
        if (uniqueTypes.Count > 0)
        {
            int index = 0;
            foreach (var kvp in uniqueTypes)
            {
                // Tạo một nút mới từ prefab
                string subtype = kvp.Key;
                int requiredLevel = kvp.Value.RequiredLevel;
                GameObject button = Instantiate(TypeButtonPrefab, TabButtonPanel);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    OnButtonClick(button, kvp.Value, requiredLevel);
                });

                if (index == 0)
                {
                    feature = kvp.Value;
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);
                    // mainId = cardHeroes.id;
                    await CreateAnimeStatsAsync();
                    if (User.CurrentUserLevel >= requiredLevel)
                    {
                        LevelCondition.gameObject.SetActive(false);
                    }
                    else
                    {
                        LevelCondition.gameObject.SetActive(true);
                        TextMeshProUGUI warningText = LevelCondition.Find("WarningText").GetComponent<TextMeshProUGUI>();
                        warningText.text = MessageConstants.WaringLevel(requiredLevel);
                    }
                }
                else
                {
                    UIManager.Instance.ChangeButtonBackground(button, ImageConstants.Button.TAB_BUTTON_BEFORE_CLICK_URL);
                }
                ButtonEvent.Instance.CheckLockedButton("AnimeStats",requiredLevel, button);
                index = index + 1;
            }
        }
    }
    void OnButtonClick(GameObject clickedButton, Features subFeature, int requiredLevel)
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

        feature = subFeature;
        UIManager.Instance.ChangeButtonBackground(clickedButton, ImageConstants.Button.TAB_BUTTON_AFTER_CLICK_URL);

        // mainId = cardHeroes.id;
        _=CreateAnimeStatsAsync();
        if (User.CurrentUserLevel >= requiredLevel)
        {
            LevelCondition.gameObject.SetActive(false);
        }
        else
        {
            LevelCondition.gameObject.SetActive(true);
            TextMeshProUGUI warningText = LevelCondition.Find("WarningText").GetComponent<TextMeshProUGUI>();
            warningText.text = MessageConstants.WaringLevel(requiredLevel);
        }
    }
    public async Task CreateAnimeStatsAsync()
    {
        AnimeStats animeStats = await AnimeStatsService.Create().GetAnimeStatsAsync(feature.Id, User.CurrentUserId);
        SlotObject = Instantiate(AnimeSlotPrefab, SlotPanel);
        animeStats.Id = feature.Id;

        Currencies silver = await UserCurrenciesService.Create().GetUserCurrencyByNameAsync(AppConstants.Currency.SILVER);

        string itemName = animeType + " " + feature.FeatureName;
        Items item = await userItemsService.GetUserItemByNameAsync(itemName);
        SetUI(SlotObject, animeStats.Level);
        SetMaterialUI(currentObject, item.Image, item.Quantity, silver.Quantity, animeStats.Level);
        upLevelButton.onClick.RemoveAllListeners();
        upMaxLevelButton.onClick.RemoveAllListeners();
        upLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);

            var result = ItemHelper.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, animeStats.Level, false, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                AnimeStats newAnimeStats = new AnimeStats();
                newAnimeStats = EnhanceAnimeStats(animeStats, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                UpLevel(newAnimeStats);
                double newPower =  await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(SlotObject);
                await CreateAnimeStatsAsync();
            }
        });
        upMaxLevelButton.onClick.AddListener(async () =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            
            var result = ItemHelper.CalculateLevelUp(item.Quantity, silver.Quantity, 1, 10, animeStats.Level, true, MAX_LEVEL);
            if (result.message.Equals(AppConstants.Status.SUCCESS))
            {
                item.Quantity = result.totalMaterialUsed;
                await userItemsService.UpdateUserItemQuantityAsync(item);
                AnimeStats newanimeStats = new AnimeStats();
                newanimeStats = EnhanceAnimeStats(animeStats, result.levelsGained);
                await UserCurrenciesService.Create().UpdateUserCurrencyAsync(silver.Id, result.currencyLeft);

                UpLevel(newanimeStats);
                double newPower = await teamsService.GetTeamsPowerAsync(User.CurrentUserId);
                double currentPower = User.CurrentUserPower;
                User.CurrentUserPower = newPower;
                FindObjectOfType<PowerController>().ShowPower(currentPower, newPower - currentPower, 1);
                Destroy(SlotObject);
                await CreateAnimeStatsAsync();
            }
        });
    }
    public void SetUI(GameObject gameObject, int level = 0)
    {
        Transform transform = gameObject.transform;
        RawImage backgroundImage = transform.Find("Background").GetComponent<RawImage>();
        Texture backgroundTexture = TextureHelper.LoadTextureCached("UI/Background3/Anime");
        backgroundImage.texture = backgroundTexture;
        
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

            Texture texture = TextureHelper.LoadTextureCached($"UI/animeStats/Anime");
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
    public void SetMaterialUI(GameObject gameObject, string itemImage, double itemQuantity, double currencyQuantity, int rankLevel)
    {
        Transform transform = gameObject.transform;
        Transform currencyPanel = transform.Find("DictionaryCards/Currency");
        // List<Currencies> currencies = await UserCurrenciesService.Create().GetUserCurrencyAsync(User.CurrentUserId);
        ButtonEvent.Instance.Close(currencyPanel);
        GameObject itemObject = UIManager.Instance.Get("ItemPrefab");
        GameObject tempObject = Instantiate(itemObject, currencyPanel);
        RawImage image = tempObject.transform.Find("Image").GetComponent<RawImage>();
        image.texture =TextureHelper.LoadTextureCached(ImageHelper.RemoveImageExtension(itemImage));
        TextMeshProUGUI quantityText = tempObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        quantityText.text = NumberFormatterHelper.FormatNumber(itemQuantity, true);
        // CurrenciesManager.Instance.GetMainCurrency(currencies, currencyPanel);

        var oneResult = ItemHelper.CalculateLevelUp(itemQuantity, currencyQuantity, 1, 10, rankLevel, false, MAX_LEVEL);
        var maxResult = ItemHelper.CalculateLevelUp(itemQuantity, currencyQuantity, 1, 10, rankLevel, true, MAX_LEVEL);
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
    public void UpLevel(AnimeStats animeStats)
    {
        _=AnimeStatsService.Create().InsertOrUpdateAnimeStatsAsync(animeStats, User.CurrentUserId);
    }
}
