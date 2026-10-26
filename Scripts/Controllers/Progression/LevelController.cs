using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private const string UpgradeLevelFeatureType = "Upgrade Level";
    private const string LevelObjectColumn = "LEVEL";

    private static readonly Dictionary<Type, (string ObjectType, string FeatureCodeName)> LevelUpSettingsByStatType =
        new Dictionary<Type, (string ObjectType, string FeatureCodeName)>
    {
        { typeof(Achievements), (AppConstants.ObjectType.ACHIEVEMENTS, AppConstants.Level.CodeName.UPGRADE_ACHIEVEMENTS_LEVEL) },
        { typeof(Alchemies), (AppConstants.ObjectType.ALCHEMIES, AppConstants.Level.CodeName.UPGRADE_ALCHEMIES_LEVEL) },
        { typeof(Architectures), (AppConstants.ObjectType.ARCHITECTURES, AppConstants.Level.CodeName.UPGRADE_ARCHITECTURES_LEVEL) },
        { typeof(Artifacts), (AppConstants.ObjectType.ARTIFACTS, AppConstants.Level.CodeName.UPGRADE_ARTIFACTS_LEVEL) },
        { typeof(Artworks), (AppConstants.ObjectType.ARTWORKS, AppConstants.Level.CodeName.UPGRADE_ARTWORKS_LEVEL) },
        { typeof(Avatars), (AppConstants.ObjectType.AVATARS, AppConstants.Level.CodeName.UPGRADE_AVATARS_LEVEL) },
        { typeof(Badges), (AppConstants.ObjectType.BADGES, AppConstants.Level.CodeName.UPGRADE_BADGES_LEVEL) },
        { typeof(Beverages), (AppConstants.ObjectType.BEVERAGES, AppConstants.Level.CodeName.UPGRADE_BEVERAGES_LEVEL) },
        { typeof(Books), (AppConstants.ObjectType.BOOKS, AppConstants.Level.CodeName.UPGRADE_BOOKS_LEVEL) },
        { typeof(Borders), (AppConstants.ObjectType.BORDERS, AppConstants.Level.CodeName.UPGRADE_BORDERS_LEVEL) },
        { typeof(Buildings), (AppConstants.ObjectType.BUILDINGS, AppConstants.Level.CodeName.UPGRADE_BUILDINGS_LEVEL) },
        { typeof(CardAdmirals), (AppConstants.ObjectType.CARD_ADMIRALS, AppConstants.Level.CodeName.UPGRADE_CARD_ADMIRALS_LEVEL) },
        { typeof(CardCaptains), (AppConstants.ObjectType.CARD_CAPTAINS, AppConstants.Level.CodeName.UPGRADE_CARD_CAPTAINS_LEVEL) },
        { typeof(CardColonels), (AppConstants.ObjectType.CARD_COLONELS, AppConstants.Level.CodeName.UPGRADE_CARD_COLONELS_LEVEL) },
        { typeof(CardGenerals), (AppConstants.ObjectType.CARD_GENERALS, AppConstants.Level.CodeName.UPGRADE_CARD_GENERALS_LEVEL) },
        { typeof(CardHeroes), (AppConstants.ObjectType.CARD_HEROES, AppConstants.Level.CodeName.UPGRADE_CARD_HEROES_LEVEL) },
        { typeof(CardLives), (AppConstants.ObjectType.CARD_LIVES, AppConstants.Level.CodeName.UPGRADE_CARD_LIVES_LEVEL) },
        { typeof(CardMilitaries), (AppConstants.ObjectType.CARD_MILITARIES, AppConstants.Level.CodeName.UPGRADE_CARD_MILITARIES_LEVEL) },
        { typeof(CardMonsters), (AppConstants.ObjectType.CARD_MONSTERS, AppConstants.Level.CodeName.UPGRADE_CARD_MONSTERS_LEVEL) },
        { typeof(CardSoldiers), (AppConstants.ObjectType.CARD_SOLDIERS, AppConstants.Level.CodeName.UPGRADE_CARD_SOLDIERS_LEVEL) },
        { typeof(CardSpells), (AppConstants.ObjectType.CARD_SPELLS, AppConstants.Level.CodeName.UPGRADE_CARD_SPELLS_LEVEL) },
        { typeof(CollaborationEquipments), (AppConstants.ObjectType.COLLABORATION_EQUIPMENTS, AppConstants.Level.CodeName.UPGRADE_COLLABORATION_EQUIPMENTS_LEVEL) },
        { typeof(Collaborations), (AppConstants.ObjectType.COLLABORATIONS, AppConstants.Level.CodeName.UPGRADE_COLLABORATIONS_LEVEL) },
        { typeof(Cores), (AppConstants.ObjectType.CORES, AppConstants.Level.CodeName.UPGRADE_CORES_LEVEL) },
        { typeof(Emojis), (AppConstants.ObjectType.EMOJIS, AppConstants.Level.CodeName.UPGRADE_EMOJIS_LEVEL) },
        { typeof(Equipments), (AppConstants.ObjectType.EQUIPMENTS, AppConstants.Level.CodeName.UPGRADE_EQUIPMENTS_LEVEL) },
        { typeof(Fashions), (AppConstants.ObjectType.FASHIONS, AppConstants.Level.CodeName.UPGRADE_FASHIONS_LEVEL) },
        { typeof(Foods), (AppConstants.ObjectType.FOODS, AppConstants.Level.CodeName.UPGRADE_FOODS_LEVEL) },
        { typeof(Forges), (AppConstants.ObjectType.FORGES, AppConstants.Level.CodeName.UPGRADE_FORGES_LEVEL) },
        { typeof(Furnitures), (AppConstants.ObjectType.FURNITURES, AppConstants.Level.CodeName.UPGRADE_FURNITURES_LEVEL) },
        { typeof(MagicFormationCircles), (AppConstants.ObjectType.MAGIC_FORMATION_CIRCLES, AppConstants.Level.CodeName.UPGRADE_MAGIC_FORMATION_CIRCLES_LEVEL) },
        { typeof(MechaBeasts), (AppConstants.ObjectType.MECHA_BEASTS, AppConstants.Level.CodeName.UPGRADE_MECHA_BEASTS_LEVEL) },
        { typeof(Medals), (AppConstants.ObjectType.MEDALS, AppConstants.Level.CodeName.UPGRADE_MEDALS_LEVEL) },
        { typeof(Pets), (AppConstants.ObjectType.PETS, AppConstants.Level.CodeName.UPGRADE_PETS_LEVEL) },
        { typeof(Plants), (AppConstants.ObjectType.PLANTS, AppConstants.Level.CodeName.UPGRADE_PLANTS_LEVEL) },
        { typeof(Puppets), (AppConstants.ObjectType.PUPPETS, AppConstants.Level.CodeName.UPGRADE_PUPPETS_LEVEL) },
        { typeof(Relics), (AppConstants.ObjectType.RELICS, AppConstants.Level.CodeName.UPGRADE_RELICS_LEVEL) },
        { typeof(Robots), (AppConstants.ObjectType.ROBOTS, AppConstants.Level.CodeName.UPGRADE_ROBOTS_LEVEL) },
        { typeof(Runes), (AppConstants.ObjectType.RUNES, AppConstants.Level.CodeName.UPGRADE_RUNES_LEVEL) },
        { typeof(Skills), (AppConstants.ObjectType.SKILLS, AppConstants.Level.CodeName.UPGRADE_SKILLS_LEVEL) },
        { typeof(SpiritBeasts), (AppConstants.ObjectType.SPIRIT_BEASTS, AppConstants.Level.CodeName.UPGRADE_SPIRIT_BEASTS_LEVEL) },
        { typeof(SpiritCards), (AppConstants.ObjectType.SPIRIT_CARDS, AppConstants.Level.CodeName.UPGRADE_SPIRIT_CARDS_LEVEL) },
        { typeof(Symbols), (AppConstants.ObjectType.SYMBOLS, AppConstants.Level.CodeName.UPGRADE_SYMBOLS_LEVEL) },
        { typeof(Talismans), (AppConstants.ObjectType.TALISMANS, AppConstants.Level.CodeName.UPGRADE_TALISMANS_LEVEL) },
        { typeof(Technologies), (AppConstants.ObjectType.TECHNOLOGIES, AppConstants.Level.CodeName.UPGRADE_TECHNOLOGIES_LEVEL) },
        { typeof(Titles), (AppConstants.ObjectType.TITLES, AppConstants.Level.CodeName.UPGRADE_TITLES_LEVEL) },
        { typeof(Vehicles), (AppConstants.ObjectType.VEHICLES, AppConstants.Level.CodeName.UPGRADE_VEHICLES_LEVEL) },
        { typeof(Weapons), (AppConstants.ObjectType.WEAPONS, AppConstants.Level.CodeName.UPGRADE_WEAPONS_LEVEL) },
        { typeof(Outfits), (AppConstants.ObjectType.OUTFITS, AppConstants.Level.CodeName.UPGRADE_OUTFITS_LEVEL) }
    };

    public static LevelController Instance { get; private set; }
    private Transform MainPanel;
    public GameObject LevelPanelPrefab;
    private GameObject CurrentPanel;
    private GameObject ItemPopup2Prefab;

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
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        LevelPanelPrefab = UIManager.Instance.Get(PrefabConstants.Progression.LEVEL_PANEL_PREFAB);
        ItemPopup2Prefab = UIManager.Instance.Get(PrefabConstants.Component.ITEM_POPUP_2_PREFAB);
    }
    public async Task CreateLevelPanelAsync<T>(T stat) where T : IStats
    {}

    // public async Task CreateLevelPanelAsync<T>(T stat) where T : IStats
    // {
    //     if (stat == null)
    //         throw new ArgumentNullException(nameof(stat));

    //     if (!LevelUpSettingsByStatType.TryGetValue(stat.GetType(), out var levelUpSettings))
    //     {
    //         throw new NotSupportedException(
    //             $"Level-up is not configured for stat type '{stat.GetType().Name}'.");
    //     }

    //     string objectType = levelUpSettings.ObjectType;
    //     GameConfig gameConfig = await GameConfigService.Create()
    //         .GetGameConfigAsync(objectType, LevelObjectColumn);
    //     if (gameConfig == null ||
    //         !int.TryParse(gameConfig.MaxValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int maxLevel))
    //     {
    //         throw new InvalidOperationException(
    //             $"Missing or invalid max-level GameConfig for object type '{objectType}' and column '{LevelObjectColumn}'.");
    //     }

    //     if (!int.TryParse(gameConfig.MinValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int minLevel))
    //     {
    //         throw new InvalidOperationException(
    //             $"Invalid min-level GameConfig '{gameConfig.MinValue}' for object type '{objectType}'.");
    //     }

    //     if (stat.Level < minLevel || stat.Level > maxLevel)
    //     {
    //         throw new InvalidOperationException(
    //             $"Current level {stat.Level} is outside configured range {minLevel}-{maxLevel} for '{objectType}'.");
    //     }

    //     Features levelFeature = await FeaturesService.Create()
    //         .GetFeatureByTypeAndCodeNameAsync(UpgradeLevelFeatureType, levelUpSettings.FeatureCodeName);

    //     if (levelFeature == null)
    //     {
    //         throw new InvalidOperationException(
    //             $"No '{UpgradeLevelFeatureType}' feature matches code name '{levelUpSettings.FeatureCodeName}'.");
    //     }

    //     CurrentPanel = Instantiate(LevelPanelPrefab, MainPanel);
    //     Transform panelTransform = CurrentPanel.transform;

    //     // --- Khởi tạo và tìm UI Components ---
    //     TextMeshProUGUI currentLevelText = panelTransform.Find("CurrentLevel").GetComponent<TextMeshProUGUI>();
    //     TextMeshProUGUI nextLevelText = panelTransform.Find("NextLevel").GetComponent<TextMeshProUGUI>();
    //     Slider progressionSlider = panelTransform.Find("ProgressionSlider").GetComponent<Slider>();
    //     Slider quantitySlider = panelTransform.Find("QuantitySlider").GetComponent<Slider>();
    //     TextMeshProUGUI experienceText = panelTransform.Find("ExperienceText").GetComponent<TextMeshProUGUI>();
    //     TextMeshProUGUI userItemQuantityText = panelTransform.Find("UserItemQuantityText").GetComponent<TextMeshProUGUI>();
    //     TextMeshProUGUI itemUsedQuantityText = panelTransform.Find("ItemUsedQuantityText").GetComponent<TextMeshProUGUI>();
    //     RawImage userItemImage = panelTransform.Find("UserItemImage").GetComponent<RawImage>();
    //     RawImage itemUsedImage = panelTransform.Find("ItemUsedImage").GetComponent<RawImage>();

    //     // TÌM COMPONENT THÔNG BÁO TRÊN UI
    //     TextMeshProUGUI notificationText = panelTransform.Find("Notification/ContentText").GetComponent<TextMeshProUGUI>();

    //     Button increaseOneButton = panelTransform.Find("IncreaseOneButton").GetComponent<Button>();
    //     Button increaseTenButton = panelTransform.Find("IncreaseTenButton").GetComponent<Button>();
    //     Button increaseMaxButton = panelTransform.Find("IncreaseMaxButton").GetComponent<Button>();
    //     Button decreaseOneButton = panelTransform.Find("DecreaseOneButton").GetComponent<Button>();
    //     Button decreaseTenButton = panelTransform.Find("DecreaseTenButton").GetComponent<Button>();
    //     Button decreaseMaxButton = panelTransform.Find("DecreaseMaxButton").GetComponent<Button>();
    //     Button confirmButton = panelTransform.Find("ConfirmButton").GetComponent<Button>();
    //     Button closeButton = panelTransform.Find("CloseButton").GetComponent<Button>();
    //     Transform currentStatsContent = panelTransform.Find("Scroll View/Viewport/Content/CurrentStats");
    //     Transform nextStatsContent = panelTransform.Find("Scroll View/Viewport/Content/NextStats");

    //     int currentLevel = stat.Level;
    //     double currentExp = stat.Experience;

    //     int targetLevel = currentLevel;
    //     double targetExp = currentExp;

    //     long currentMaterialCount = 0;

    //     double availableMaterialCount = 0;
    //     RecipeItemDTO experienceItemForDeduction = null;
    //     Dictionary<int, (long RequiredExp, string FormulaType)> requiredExpByLevel =
    //         new Dictionary<int, (long RequiredExp, string FormulaType)>();
    //     List<(int MinLevel, int MaxLevel, string ItemId, double ItemValue)> recipeExpRanges =
    //         new List<(int MinLevel, int MaxLevel, string ItemId, double ItemValue)>();
    //     IGameGrowthConfigService growthConfigService = GameGrowthConfigService.Create();
    //     int previewRevision = 0;
    //     string targetFormulaType = string.Empty;

    //     quantitySlider.minValue = 0;
    //     quantitySlider.maxValue = 0;

    //     #region Local Functions

    //     void SetNotification(string translationKey, Color color, params object[] args)
    //     {
    //         if (notificationText == null) return;

    //         string translatedValue = LocalizationManager.Get(translationKey);

    //         if (args != null && args.Length > 0)
    //         {
    //             translatedValue = string.Format(translatedValue, args);
    //         }

    //         notificationText.text = translatedValue;
    //         notificationText.color = color;
    //     }

    //     void SetConfirmButtonState(bool interactable, bool isMax = false)
    //     {
    //         confirmButton.interactable = interactable;
    //     }

    //     async Task<(long RequiredExp, string FormulaType)> GetRequiredExpAsync(int level)
    //     {
    //         if (requiredExpByLevel.TryGetValue(level, out var cachedRequiredExp))
    //             return cachedRequiredExp;

    //         GameGrowthConfig config = await growthConfigService
    //             .GetConfigAsync(objectType, levelFeature.CodeName, level);
    //         long requiredExp = await growthConfigService
    //             .CalculateRequiredExpAsync(objectType, levelFeature.CodeName, level);

    //         if (config == null)
    //             throw new InvalidOperationException($"Missing growth formula for level {level}.");
    //         if (requiredExp <= 0)
    //             throw new InvalidOperationException(
    //                 $"Growth formula '{config.FormulaType}' returned non-positive EXP for level {level}.");

    //         var result = (RequiredExp: requiredExp, FormulaType: config.FormulaType);
    //         requiredExpByLevel.Add(level, result);
    //         return result;
    //     }

    //     async Task<double> GetRecipeItemValueAsync(int level)
    //     {
    //         var cachedRange = recipeExpRanges
    //             .Where(range => level >= range.MinLevel && level <= range.MaxLevel)
    //             .OrderByDescending(range => range.MinLevel)
    //             .FirstOrDefault();

    //         if (cachedRange.MaxLevel > 0)
    //         {
    //             if (!string.Equals(experienceItemForDeduction?.ItemId, cachedRange.ItemId, StringComparison.Ordinal))
    //             {
    //                 throw new InvalidOperationException(
    //                     $"Level-up recipe for level {level} uses EXP item '{cachedRange.ItemId}', which differs from the selected item '{experienceItemForDeduction?.ItemId}'.");
    //             }
    //             return cachedRange.ItemValue;
    //         }

    //         List<RecipeItemDTO> recipeItems = await RecipeService.Create()
    //             .GetRecipeItemsAsync(levelFeature.FeatureName, level, User.CurrentUserId);
    //         if (recipeItems == null || recipeItems.Count == 0)
    //         {
    //             throw new InvalidOperationException(
    //                 $"No level-up recipe found for feature '{levelFeature.FeatureName}' at level {level}.");
    //         }

    //         int selectedMinLevel = recipeItems.Max(item => item.MinLevel);
    //         int selectedMaxLevel = recipeItems
    //             .Where(item => item.MinLevel == selectedMinLevel)
    //             .Max(item => item.MaxLevel);
    //         List<RecipeItemDTO> selectedRangeItems = recipeItems
    //             .Where(item => item.MinLevel == selectedMinLevel && item.MaxLevel == selectedMaxLevel)
    //             .ToList();

    //         List<RecipeItemDTO> experienceItems = selectedRangeItems
    //             .Where(item => item.ItemValue > 0)
    //             .ToList();
    //         if (experienceItems.Count != 1)
    //         {
    //             throw new InvalidOperationException(
    //                 $"Expected one recipe item with a positive ItemValue in recipe '{levelFeature.FeatureName}' for levels {selectedMinLevel}-{selectedMaxLevel}, found {experienceItems.Count}.");
    //         }

    //         RecipeItemDTO experienceItem = experienceItems[0];
    //         if (experienceItem.ItemValue <= 0 || double.IsNaN(experienceItem.ItemValue) || double.IsInfinity(experienceItem.ItemValue))
    //         {
    //             throw new InvalidOperationException(
    //                 $"Recipe item '{experienceItem.ItemId}' has invalid ItemValue '{experienceItem.ItemValue}' for levels {selectedMinLevel}-{selectedMaxLevel}.");
    //         }

    //         if (experienceItemForDeduction == null)
    //         {
    //             experienceItemForDeduction = experienceItem;
    //             availableMaterialCount = Math.Max(0, Math.Floor(experienceItem.UserQuantity));
    //             string texturePath = ImageHelper.RemoveImageExtension(experienceItem.ItemImage);
    //             Texture itemTexture = TextureHelper.LoadTexture2DCached(texturePath);
    //             userItemImage.texture = itemTexture;
    //             itemUsedImage.texture = itemTexture;
    //             quantitySlider.maxValue = (float)availableMaterialCount;
    //         }
    //         else if (!string.Equals(experienceItemForDeduction.ItemId, experienceItem.ItemId, StringComparison.Ordinal))
    //         {
    //             throw new InvalidOperationException(
    //                 $"Level-up recipe changes EXP item from '{experienceItemForDeduction.ItemId}' to '{experienceItem.ItemId}' between levels {experienceItemForDeduction.MinLevel}-{experienceItemForDeduction.MaxLevel} and {selectedMinLevel}-{selectedMaxLevel}.");
    //         }

    //         double recipeAvailableQuantity = Math.Max(0, Math.Floor(experienceItem.UserQuantity));
    //         availableMaterialCount = Math.Min(availableMaterialCount, recipeAvailableQuantity);
    //         quantitySlider.maxValue = (float)availableMaterialCount;
    //         recipeExpRanges.Add((selectedMinLevel, selectedMaxLevel, experienceItem.ItemId, experienceItem.ItemValue));
    //         return experienceItem.ItemValue;
    //     }

    //     async Task<(int Level, double Experience, string FormulaType)> CalculateLevelFromItemsAsync(long itemsToUse)
    //     {
    //         int calculatedLevel = currentLevel;
    //         double remainingExp = currentExp;
    //         long remainingItems = itemsToUse;
    //         string formulaType = string.Empty;

    //         while (calculatedLevel < maxLevel)
    //         {
    //             var growth = await GetRequiredExpAsync(calculatedLevel);
    //             formulaType = growth.FormulaType;

    //             if (remainingExp >= growth.RequiredExp)
    //             {
    //                 remainingExp -= growth.RequiredExp;
    //                 calculatedLevel++;
    //                 continue;
    //             }

    //             if (remainingItems == 0)
    //                 break;

    //             double itemValue = await GetRecipeItemValueAsync(calculatedLevel);
    //             long itemsNeeded = Math.Max(
    //                 1,
    //                 (long)Math.Ceiling((growth.RequiredExp - remainingExp) / itemValue));
    //             long itemsToApply = Math.Min(remainingItems, itemsNeeded);
    //             remainingExp += itemsToApply * itemValue;
    //             remainingItems -= itemsToApply;

    //             if (remainingExp >= growth.RequiredExp)
    //             {
    //                 remainingExp -= growth.RequiredExp;
    //                 calculatedLevel++;
    //             }
    //             else
    //             {
    //                 break;
    //             }
    //         }

    //         if (calculatedLevel >= maxLevel)
    //             return (maxLevel, 0, string.Empty);

    //         var targetGrowth = await GetRequiredExpAsync(calculatedLevel);
    //         return (calculatedLevel, remainingExp, targetGrowth.FormulaType);
    //     }

    //     async Task<long> CalculateMaxMaterialsNeededAsync()
    //     {
    //         int calculatedLevel = currentLevel;
    //         double remainingExp = currentExp;
    //         long itemsNeeded = 0;

    //         while (calculatedLevel < maxLevel)
    //         {
    //             var growth = await GetRequiredExpAsync(calculatedLevel);
    //             if (remainingExp >= growth.RequiredExp)
    //             {
    //                 remainingExp -= growth.RequiredExp;
    //                 calculatedLevel++;
    //                 continue;
    //             }

    //             double itemValue = await GetRecipeItemValueAsync(calculatedLevel);
    //             long itemsForLevel = Math.Max(
    //                 1,
    //                 (long)Math.Ceiling((growth.RequiredExp - remainingExp) / itemValue));
    //             itemsNeeded = checked(itemsNeeded + itemsForLevel);
    //             remainingExp += itemsForLevel * itemValue - growth.RequiredExp;
    //             calculatedLevel++;
    //         }

    //         return itemsNeeded;
    //     }

    //     // Tạo bản Clone riêng biệt cho BaseStats để xem trước (Preview UI)
    //     IStats CreatePreviewBaseStats(int newLevel)
    //     {
    //         try
    //         {
    //             var baseStatsProp = typeof(T).GetProperty("BaseStats");
    //             object originalBaseStats = baseStatsProp?.GetValue(stat);

    //             // Lấy nguồn clone (BaseStats hoặc stat gốc)
    //             object sourceToClone = originalBaseStats ?? stat;
    //             if (sourceToClone == null) return null;

    //             // Deep clone bằng JSON
    //             string json = Newtonsoft.Json.JsonConvert.SerializeObject(sourceToClone);
    //             Type typeToDeserialize = sourceToClone.GetType();
    //             IStats previewBaseStats = (IStats)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeToDeserialize);

    //             if (previewBaseStats != null)
    //             {
    //                 // FIX 1: Gán chính xác newLevel, KHÔNG dùng Math.Max(1, newLevel) gây nhảy level!
    //                 previewBaseStats.Level = newLevel;

    //                 // FIX 2: Tính toán lại power cho bản Preview
    //                 QualityEvaluatorHelper.GetQualityPower(previewBaseStats);
    //                 LevelEvaluatorHelper.GetLevelPower(previewBaseStats);
    //                 StarEvaluatorHelper.GetStarPower(previewBaseStats);
    //             }

    //             return previewBaseStats;
    //         }
    //         catch (Exception ex)
    //         {
    //             Debug.LogWarning($"Lỗi khi clone Preview BaseStats: {ex.Message}");
    //             return null;
    //         }
    //     }
    //     int lastTargetLevel = -1;
    //     // 1. Hàm vẽ CurrentStats (CHỈ GỌI 1 LẦN DUY NHẤT LÚC KHỞI TẠO HOẶC SAU CONFIRM)
    //     void RenderCurrentStats()
    //     {
    //         if (currentStatsContent == null) return;

    //         foreach (Transform child in currentStatsContent)
    //             Destroy(child.gameObject);

    //         var baseStatsProp = typeof(T).GetProperty("BaseStats");
    //         object currentBaseStatsObj = baseStatsProp?.GetValue(stat);

    //         IStats currentDisplayStats = stat;
    //         if (currentBaseStatsObj != null)
    //         {
    //             string json = Newtonsoft.Json.JsonConvert.SerializeObject(currentBaseStatsObj);
    //             currentDisplayStats = (IStats)Newtonsoft.Json.JsonConvert.DeserializeObject(json, currentBaseStatsObj.GetType());
    //         }

    //         if (currentDisplayStats != null)
    //         {
    //             currentDisplayStats.Level = currentLevel;
    //             QualityEvaluatorHelper.GetQualityPower(currentDisplayStats);
    //             LevelEvaluatorHelper.GetLevelPower(currentDisplayStats);
    //             StarEvaluatorHelper.GetStarPower(currentDisplayStats);

    //             StatsManager.Instance.CreateStatsManager(currentDisplayStats, currentStatsContent);
    //         }
    //     }

    //     // 2. Hàm vẽ NextStats (CHỈ GỌI KHI TARGET LEVEL THAY ĐỔI)
    //     void RenderNextStats()
    //     {
    //         if (nextStatsContent == null) return;

    //         foreach (Transform child in nextStatsContent)
    //             Destroy(child.gameObject);

    //         // Nếu Target Level bằng Level hiện tại -> Bắt buộc dùng bản sao y chang Current
    //         if (targetLevel == currentLevel)
    //         {
    //             var baseStatsProp = typeof(T).GetProperty("BaseStats");
    //             object currentBaseStatsObj = baseStatsProp?.GetValue(stat);

    //             IStats currentDisplayStats = stat;
    //             if (currentBaseStatsObj != null)
    //             {
    //                 string json = Newtonsoft.Json.JsonConvert.SerializeObject(currentBaseStatsObj);
    //                 currentDisplayStats = (IStats)Newtonsoft.Json.JsonConvert.DeserializeObject(json, currentBaseStatsObj.GetType());
    //             }

    //             if (currentDisplayStats != null)
    //             {
    //                 currentDisplayStats.Level = currentLevel;
    //                 QualityEvaluatorHelper.GetQualityPower(currentDisplayStats);
    //                 LevelEvaluatorHelper.GetLevelPower(currentDisplayStats);
    //                 StarEvaluatorHelper.GetStarPower(currentDisplayStats);

    //                 StatsManager.Instance.CreateStatsManager(currentDisplayStats, nextStatsContent);
    //             }
    //         }
    //         else
    //         {
    //             // Target Level tăng lên -> Mới clone Preview Level mới
    //             IStats previewBaseStats = CreatePreviewBaseStats(targetLevel);
    //             if (previewBaseStats != null)
    //             {
    //                 StatsManager.Instance.CreateStatsManager(previewBaseStats, nextStatsContent);
    //             }
    //         }
    //     }

    //     // 3. Hàm Refresh UI chính (GỌI KHI CLICK/SLIDER THAY ĐỔI)
    //     async Task RefreshUIAsync(int revision)
    //     {
    //         // A. Cập nhật Text, Slider EXP, Slider Item (Luôn cập nhật - cực nhẹ)
    //         currentLevelText.text = currentLevel.ToString();
    //         nextLevelText.text = targetLevel >= maxLevel ? "MAX" : targetLevel.ToString();
    //         userItemQuantityText.text = NumberFormatterHelper.FormatNumber(availableMaterialCount, true);
    //         itemUsedQuantityText.text = NumberFormatterHelper.FormatNumber(currentMaterialCount, true);
    //         quantitySlider.SetValueWithoutNotify(currentMaterialCount);

    //         if (targetLevel >= maxLevel)
    //         {
    //             progressionSlider.value = 1f;
    //             experienceText.text = "MAX";
    //         }
    //         else
    //         {
    //             var growth = await GetRequiredExpAsync(targetLevel);
    //             if (revision != previewRevision)
    //                 return;

    //             targetFormulaType = growth.FormulaType;
    //             progressionSlider.value = Mathf.Clamp01((float)(targetExp / growth.RequiredExp));
    //             experienceText.text = $"{targetExp:N0}/{growth.RequiredExp:N0} ({targetFormulaType})";
    //         }

    //         // B. CHỈ RE-RENDER NEXT STATS KHI TARGET LEVEL THỰC SỰ THAY ĐỔI!
    //         if (targetLevel != lastTargetLevel)
    //         {
    //             RenderNextStats();
    //             lastTargetLevel = targetLevel; // Cập nhật lại mốc level mới nhất đã render
    //         }

    //         // C. Cập nhật trạng thái thông báo và nút Confirm
    //         if (currentMaterialCount <= 0)
    //         {
    //             itemUsedQuantityText.color = Color.white;
    //             SetNotification(MessageConstants.PLEASE_SELECT_QUANTITY, Color.yellow);
    //             SetConfirmButtonState(false);
    //         }
    //         else
    //         {
    //             itemUsedQuantityText.color = Color.white;

    //             if (targetLevel >= maxLevel)
    //             {
    //                 SetNotification(MessageConstants.MAX_LEVEL_REACHED, Color.green, maxLevel);
    //                 SetConfirmButtonState(false, true);
    //             }
    //             else
    //             {
    //                 SetNotification(MessageConstants.READY_TO_UPGRADE, Color.green, targetLevel);
    //                 SetConfirmButtonState(true);
    //             }
    //         }
    //     }

    //     async Task RecalculateAndRefreshAsync()
    //     {
    //         int revision = ++previewRevision;
    //         confirmButton.interactable = false;
    //         var preview = await CalculateLevelFromItemsAsync(currentMaterialCount);
    //         if (revision != previewRevision)
    //             return;

    //         targetLevel = preview.Level;
    //         targetExp = preview.Experience;
    //         targetFormulaType = preview.FormulaType;
    //         await RefreshUIAsync(revision);
    //     }

    //     async Task ChangeMaterialCountAsync(long amount)
    //     {
    //         currentMaterialCount = Math.Max(
    //             0,
    //             Math.Min(currentMaterialCount + amount, (long)availableMaterialCount));
    //         await RecalculateAndRefreshAsync();
    //     }

    //     async Task SetMaxMaterialCountAsync()
    //     {
    //         long maxNeed = await CalculateMaxMaterialsNeededAsync();
    //         currentMaterialCount = Math.Min(maxNeed, (long)availableMaterialCount);
    //         await RecalculateAndRefreshAsync();
    //     }

    //     async Task RunPreviewUpdateAsync(Func<Task> update)
    //     {
    //         try
    //         {
    //             await update();
    //         }
    //         catch (Exception exception)
    //         {
    //             Debug.LogError($"Level-up preview failed: {exception}");
    //             SetNotification(MessageConstants.SERVER_ERROR, Color.red, exception.Message);
    //             confirmButton.interactable = false;
    //         }
    //     }

    //     #endregion

    //     if (currentLevel < maxLevel)
    //     {
    //         await RunPreviewUpdateAsync(async () =>
    //         {
    //             await GetRecipeItemValueAsync(currentLevel);
    //         });
    //     }

    //     quantitySlider.onValueChanged.AddListener(value =>
    //     {
    //         currentMaterialCount = (long)Math.Round(value);
    //         _ = RunPreviewUpdateAsync(RecalculateAndRefreshAsync);
    //     });
    //     RenderCurrentStats();
    //     await RunPreviewUpdateAsync(RecalculateAndRefreshAsync);

    //     async Task ExecuteServiceInsertAsync()
    //     {
    //         try
    //         {
    //             if (stat is Achievements achievement)
    //             {
    //                 List<RecipeItemDTO> itemsToDeduct = new List<RecipeItemDTO>
    //                 {
    //                     new RecipeItemDTO
    //                     {
    //                         ItemId = experienceItemForDeduction.ItemId,
    //                         RequiredQuantity = currentMaterialCount
    //                     }
    //                 };

    //                 InsertOrUpdateResult<bool> result = await UserAchievementsService.Create()
    //                     .UpdateUserAchievementLevelAsync(
    //                         User.CurrentUserId,
    //                         achievement,
    //                         itemsToDeduct);
    //                 if (result == null || !result.IsSuccess || !result.Data)
    //                 {
    //                     throw new InvalidOperationException(result?.Message ?? MessageConstants.FAILED_TO_EXECUTE_ACTION);
    //                 }
    //                 UserAchievementsController.Instance.RefreshCurrentDetailsUI(achievement);
    //             }
    //             else if (stat is Alchemies alchemy)
    //             {
    //                 await UserAlchemiesService.Create().UpdateUserAlchemyLevelAsync(User.CurrentUserId, alchemy);
    //                 UserAlchemiesController.Instance.RefreshCurrentDetailsUI(alchemy);
    //             }
    //             else if (stat is Architectures architecture)
    //             {
    //                 await UserArchitecturesService.Create().UpdateUserArchitectureLevelAsync(User.CurrentUserId, architecture);
    //                 UserArchitecturesController.Instance.RefreshCurrentDetailsUI(architecture);
    //             }
    //             else if (stat is Artifacts artifact)
    //             {
    //                 await UserArtifactsService.Create().UpdateUserArtifactLevelAsync(User.CurrentUserId, artifact);
    //                 UserArtifactsController.Instance.RefreshCurrentDetailsUI(artifact);
    //             }
    //             else if (stat is Artworks artwork)
    //             {
    //                 await UserArtworksService.Create().UpdateUserArtworkLevelAsync(User.CurrentUserId, artwork);
    //                 UserArtworksController.Instance.RefreshCurrentDetailsUI(artwork);
    //             }
    //             else if (stat is Avatars avatar)
    //             {
    //                 await UserAvatarsService.Create().UpdateUserAvatarLevelAsync(User.CurrentUserId, avatar);
    //                 UserAvatarsController.Instance.RefreshCurrentDetailsUI(avatar);
    //             }
    //             else if (stat is Badges badge)
    //             {
    //                 await UserBadgesService.Create().UpdateUserBadgeLevelAsync(User.CurrentUserId, badge);
    //                 UserBadgesController.Instance.RefreshCurrentDetailsUI(badge);
    //             }
    //             else if (stat is Beverages beverage)
    //             {
    //                 await UserBeveragesService.Create().UpdateUserBeverageLevelAsync(User.CurrentUserId, beverage);
    //                 UserBeveragesController.Instance.RefreshCurrentDetailsUI(beverage);
    //             }
    //             else if (stat is Books book)
    //             {
    //                 await UserBooksService.Create().UpdateUserBookLevelAsync(User.CurrentUserId, book);
    //                 UserBooksController.Instance.RefreshCurrentDetailsUI(book);
    //             }
    //             else if (stat is Borders border)
    //             {
    //                 await UserBordersService.Create().UpdateUserBorderLevelAsync(User.CurrentUserId, border);
    //                 UserBordersController.Instance.RefreshCurrentDetailsUI(border);
    //             }
    //             else if (stat is Buildings building)
    //             {
    //                 await UserBuildingsService.Create().UpdateUserBuildingLevelAsync(User.CurrentUserId, building);
    //                 UserBuildingsController.Instance.RefreshCurrentDetailsUI(building);
    //             }
    //             else if (stat is CardAdmirals admiral)
    //             {
    //                 await UserCardAdmiralsService.Create().UpdateUserCardAdmiralLevelAsync(User.CurrentUserId, admiral);
    //                 UserCardAdmiralsController.Instance.RefreshCurrentDetailsUI(admiral);
    //             }
    //             else if (stat is CardCaptains captain)
    //             {
    //                 await UserCardCaptainsService.Create().UpdateUserCardCaptainLevelAsync(User.CurrentUserId, captain);
    //                 UserCardCaptainsController.Instance.RefreshCurrentDetailsUI(captain);
    //             }
    //             else if (stat is CardColonels colonel)
    //             {
    //                 await UserCardColonelsService.Create().UpdateUserCardColonelLevelAsync(User.CurrentUserId, colonel);
    //                 UserCardColonelsController.Instance.RefreshCurrentDetailsUI(colonel);
    //             }
    //             else if (stat is CardGenerals general)
    //             {
    //                 await UserCardGeneralsService.Create().UpdateUserCardGeneralLevelAsync(User.CurrentUserId, general);
    //                 UserCardGeneralsController.Instance.RefreshCurrentDetailsUI(general);
    //             }
    //             else if (stat is CardHeroes hero)
    //             {
    //                 await UserCardHeroesService.Create().UpdateUserCardHeroLevelAsync(User.CurrentUserId, hero);
    //                 UserCardHeroesController.Instance.RefreshCurrentDetailsUI(hero);
    //             }
    //             else if (stat is CardLives cardLife)
    //             {
    //                 await UserCardLivesService.Create().UpdateUserCardLifeLevelAsync(User.CurrentUserId, cardLife);
    //                 UserCardLivesController.Instance.RefreshCurrentDetailsUI(cardLife);
    //             }
    //             else if (stat is CardMilitaries military)
    //             {
    //                 await UserCardMilitariesService.Create().UpdateUserCardMilitaryLevelAsync(User.CurrentUserId, military);
    //                 UserCardMilitariesController.Instance.RefreshCurrentDetailsUI(military);
    //             }
    //             else if (stat is CardMonsters monster)
    //             {
    //                 await UserCardMonstersService.Create().UpdateUserCardMonsterLevelAsync(User.CurrentUserId, monster);
    //                 UserCardMonstersController.Instance.RefreshCurrentDetailsUI(monster);
    //             }
    //             else if (stat is CardSoldiers soldier)
    //             {
    //                 await UserCardSoldiersService.Create().UpdateUserCardSoldierLevelAsync(User.CurrentUserId, soldier);
    //                 UserCardSoldiersController.Instance.RefreshCurrentDetailsUI(soldier);
    //             }
    //             else if (stat is CardSpells spell)
    //             {
    //                 await UserCardSpellsService.Create().UpdateUserCardSpellLevelAsync(User.CurrentUserId, spell);
    //                 UserCardSpellsController.Instance.RefreshCurrentDetailsUI(spell);
    //             }
    //             else if (stat is CollaborationEquipments collabEquip)
    //             {
    //                 await UserCollaborationEquipmentsService.Create().UpdateUserCollaborationEquipmentLevelAsync(User.CurrentUserId, collabEquip);
    //                 UserCollaborationEquipmentsController.Instance.RefreshCurrentDetailsUI(collabEquip);
    //             }
    //             else if (stat is Collaborations collab)
    //             {
    //                 await UserCollaborationsService.Create().UpdateUserCollaborationLevelAsync(User.CurrentUserId, collab);
    //                 UserCollaborationsController.Instance.RefreshCurrentDetailsUI(collab);
    //             }
    //             else if (stat is Cores core)
    //             {
    //                 await UserCoresService.Create().UpdateUserCoreLevelAsync(User.CurrentUserId, core);
    //                 UserCoresController.Instance.RefreshCurrentDetailsUI(core);
    //             }
    //             else if (stat is Emojis emoji)
    //             {
    //                 await UserEmojisService.Create().UpdateUserEmojiLevelAsync(User.CurrentUserId, emoji);
    //                 UserEmojisController.Instance.RefreshCurrentDetailsUI(emoji);
    //             }
    //             else if (stat is Equipments equipment)
    //             {
    //                 await UserEquipmentsService.Create().UpdateUserEquipmentLevelAsync(User.CurrentUserId, equipment);
    //                 UserEquipmentsController.Instance.RefreshCurrentDetailsUI(equipment);
    //             }
    //             else if (stat is Fashions fashion)
    //             {
    //                 await UserFashionsService.Create().UpdateUserFashionLevelAsync(User.CurrentUserId, fashion);
    //                 UserFashionsController.Instance.RefreshCurrentDetailsUI(fashion);
    //             }
    //             else if (stat is Foods food)
    //             {
    //                 await UserFoodsService.Create().UpdateUserFoodLevelAsync(User.CurrentUserId, food);
    //                 UserFoodsController.Instance.RefreshCurrentDetailsUI(food);
    //             }
    //             else if (stat is Forges forge)
    //             {
    //                 await UserForgesService.Create().UpdateUserForgeLevelAsync(User.CurrentUserId, forge);
    //                 UserForgesController.Instance.RefreshCurrentDetailsUI(forge);
    //             }
    //             else if (stat is Furnitures furniture)
    //             {
    //                 await UserFurnituresService.Create().UpdateUserFurnitureLevelAsync(User.CurrentUserId, furniture);
    //                 UserFurnituresController.Instance.RefreshCurrentDetailsUI(furniture);
    //             }
    //             else if (stat is MagicFormationCircles circle)
    //             {
    //                 await UserMagicFormationCirclesService.Create().UpdateUserMagicFormationCircleLevelAsync(User.CurrentUserId, circle);
    //                 UserMagicFormationCirclesController.Instance.RefreshCurrentDetailsUI(circle);
    //             }
    //             else if (stat is MechaBeasts mechaBeast)
    //             {
    //                 await UserMechaBeastsService.Create().UpdateUserMechaBeastLevelAsync(User.CurrentUserId, mechaBeast);
    //                 UserMechaBeastsController.Instance.RefreshCurrentDetailsUI(mechaBeast);
    //             }
    //             else if (stat is Medals medal)
    //             {
    //                 await UserMedalsService.Create().UpdateUserMedalLevelAsync(User.CurrentUserId, medal);
    //                 UserMedalsController.Instance.RefreshCurrentDetailsUI(medal);
    //             }
    //             else if (stat is Pets pet)
    //             {
    //                 await UserPetsService.Create().UpdateUserPetLevelAsync(User.CurrentUserId, pet);
    //                 UserPetsController.Instance.RefreshCurrentDetailsUI(pet);
    //             }
    //             else if (stat is Plants plant)
    //             {
    //                 await UserPlantsService.Create().UpdateUserPlantLevelAsync(User.CurrentUserId, plant);
    //                 UserPlantsController.Instance.RefreshCurrentDetailsUI(plant);
    //             }
    //             else if (stat is Puppets puppet)
    //             {
    //                 await UserPuppetsService.Create().UpdateUserPuppetLevelAsync(User.CurrentUserId, puppet);
    //                 UserPuppetsController.Instance.RefreshCurrentDetailsUI(puppet);
    //             }
    //             else if (stat is Relics relic)
    //             {
    //                 await UserRelicsService.Create().UpdateUserRelicLevelAsync(User.CurrentUserId, relic);
    //                 UserRelicsController.Instance.RefreshCurrentDetailsUI(relic);
    //             }
    //             else if (stat is Robots robot)
    //             {
    //                 await UserRobotsService.Create().UpdateUserRobotLevelAsync(User.CurrentUserId, robot);
    //                 UserRobotsController.Instance.RefreshCurrentDetailsUI(robot);
    //             }
    //             else if (stat is Runes rune)
    //             {
    //                 await UserRunesService.Create().UpdateUserRuneLevelAsync(User.CurrentUserId, rune);
    //                 UserRunesController.Instance.RefreshCurrentDetailsUI(rune);
    //             }
    //             else if (stat is Skills skill)
    //             {
    //                 await UserSkillsService.Create().UpdateUserSkillLevelAsync(User.CurrentUserId, skill);
    //                 UserSkillsController.Instance.RefreshCurrentDetailsUI(skill);
    //             }
    //             else if (stat is SpiritBeasts spiritBeast)
    //             {
    //                 await UserSpiritBeastsService.Create().UpdateUserSpiritBeastLevelAsync(User.CurrentUserId, spiritBeast);
    //                 UserSpiritBeastsController.Instance.RefreshCurrentDetailsUI(spiritBeast);
    //             }
    //             else if (stat is SpiritCards spiritCard)
    //             {
    //                 await UserSpiritCardsService.Create().UpdateUserSpiritCardLevelAsync(User.CurrentUserId, spiritCard);
    //                 UserSpiritCardsController.Instance.RefreshCurrentDetailsUI(spiritCard);
    //             }
    //             else if (stat is Symbols symbol)
    //             {
    //                 await UserSymbolsService.Create().UpdateUserSymbolLevelAsync(User.CurrentUserId, symbol);
    //                 UserSymbolsController.Instance.RefreshCurrentDetailsUI(symbol);
    //             }
    //             else if (stat is Talismans talisman)
    //             {
    //                 await UserTalismansService.Create().UpdateUserTalismanLevelAsync(User.CurrentUserId, talisman);
    //                 UserTalismansController.Instance.RefreshCurrentDetailsUI(talisman);
    //             }
    //             else if (stat is Technologies technology)
    //             {
    //                 await UserTechnologiesService.Create().UpdateUserTechnologyLevelAsync(User.CurrentUserId, technology);
    //                 UserTechnologiesController.Instance.RefreshCurrentDetailsUI(technology);
    //             }
    //             else if (stat is Titles title)
    //             {
    //                 await UserTitlesService.Create().UpdateUserTitleLevelAsync(User.CurrentUserId, title);
    //                 UserTitlesController.Instance.RefreshCurrentDetailsUI(title);
    //             }
    //             else if (stat is Vehicles vehicle)
    //             {
    //                 await UserVehiclesService.Create().UpdateUserVehicleLevelAsync(User.CurrentUserId, vehicle);
    //                 UserVehiclesController.Instance.RefreshCurrentDetailsUI(vehicle);
    //             }
    //             else if (stat is Weapons weapon)
    //             {
    //                 await UserWeaponsService.Create().UpdateUserWeaponLevelAsync(User.CurrentUserId, weapon);
    //                 UserWeaponsController.Instance.RefreshCurrentDetailsUI(weapon);
    //             }
    //             else if (stat is Outfits outfit)
    //             {
    //                 await UserOutfitsService.Create().UpdateUserOutfitLevelAsync(User.CurrentUserId, outfit);
    //                 UserOutfitsController.Instance.RefreshCurrentDetailsUI(outfit);
    //             }
    //         }
    //         catch (Exception ex)
    //         {
    //             SetNotification(MessageConstants.SERVER_ERROR, Color.red, ex.Message);
    //             throw;
    //         }
    //     }

    //     #region Button Events listeners
    //     increaseOneButton.onClick.AddListener(() =>
    //     {
    //         AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
    //         _ = RunPreviewUpdateAsync(() => ChangeMaterialCountAsync(1));
    //     });

    //     increaseTenButton.onClick.AddListener(() =>
    //     {
    //         AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
    //         _ = RunPreviewUpdateAsync(() => ChangeMaterialCountAsync(10));
    //     });

    //     increaseMaxButton.onClick.AddListener(() =>
    //     {
    //         AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
    //         _ = RunPreviewUpdateAsync(SetMaxMaterialCountAsync);
    //     });

    //     decreaseOneButton.onClick.AddListener(() =>
    //     {
    //         AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
    //         _ = RunPreviewUpdateAsync(() => ChangeMaterialCountAsync(-1));
    //     });

    //     decreaseTenButton.onClick.AddListener(() =>
    //     {
    //         AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
    //         _ = RunPreviewUpdateAsync(() => ChangeMaterialCountAsync(-10));
    //     });

    //     decreaseMaxButton.onClick.AddListener(() =>
    //     {
    //         AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
    //         currentMaterialCount = 0;
    //         _ = RunPreviewUpdateAsync(RecalculateAndRefreshAsync);
    //     });

    //     closeButton.onClick.AddListener(() =>
    //     {
    //         AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
    //         Destroy(CurrentPanel);
    //     });

    //     confirmButton.onClick.AddListener((UnityEngine.Events.UnityAction)(async () =>
    //     {
    //         if (currentLevel >= maxLevel)
    //         {
    //             notificationText.text = MessageConstants.UPGRADE_ALREADY_MAX;
    //             notificationText.color = Color.red;
    //             AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
    //             return;
    //         }

    //         AudioManager.Instance.PlaySFX(AudioConstants.SFX.LEVEL_UP_SOUND);
    //         if (currentMaterialCount > availableMaterialCount)
    //         {
    //             SetNotification(MessageConstants.NOT_ENOUGH_MATERIALS, Color.red);
    //             return;
    //         }

    //         if (currentMaterialCount <= 0)
    //         {
    //             SetNotification(MessageConstants.PLEASE_SELECT_QUANTITY, Color.red);
    //             return;
    //         }

    //         confirmButton.interactable = false;
    //         closeButton.interactable = false;

    //         SetNotification(MessageConstants.PROCESSING_UPGRADE, Color.cyan);

    //         try
    //         {
    //             // Chỉ gán Level/Exp thực sự khi đã BẤM CONFIRM THÀNH CÔNG
    //             stat.Level = targetLevel;
    //             stat.Experience = targetExp;

    //             // Nếu BaseStats tồn tại trên stat gốc thì cập nhật Level cho nó luôn
    //             var baseStatsProp = typeof(T).GetProperty("BaseStats");
    //             if (baseStatsProp != null)
    //             {
    //                 var baseStatsObj = baseStatsProp.GetValue(stat);
    //                 if (baseStatsObj != null)
    //                 {
    //                     var baseLevelProp = baseStatsObj.GetType().GetProperty("Level");
    //                     if (baseLevelProp != null && baseLevelProp.CanWrite)
    //                     {
    //                         baseLevelProp.SetValue(baseStatsObj, targetLevel);
    //                     }
    //                 }
    //             }

    //             // Cập nhật lại chỉ số thực tế sau khi tăng Level thành công
    //             QualityEvaluatorHelper.GetQualityPower(stat);
    //             LevelEvaluatorHelper.GetLevelPower(stat);
    //             StarEvaluatorHelper.GetStarPower(stat);

    //             if (!(stat is Achievements))
    //             {
    //                 Items materialItem = new Items
    //                 {
    //                     Id = experienceItemForDeduction.ItemId
    //                 };

    //                 double remainingQuantity = Math.Max(0, experienceItemForDeduction.UserQuantity - currentMaterialCount);

    //                 await UserItemsService.Create().InsertOrUpdateUserItemAsync(
    //                     User.CurrentUserId,
    //                     materialItem,
    //                     remainingQuantity);
    //             }

    //             // Update level vào database & Refresh UI ngoài Controller
    //             await ExecuteServiceInsertAsync();

    //             SetNotification(MessageConstants.UPGRADE_SUCCESS, Color.green);

    //             await Task.Delay(500);

    //             Destroy(CurrentPanel);
    //         }
    //         catch (Exception ex)
    //         {
    //             confirmButton.interactable = true;
    //             closeButton.interactable = true;

    //             SetNotification(
    //                 MessageConstants.UPGRADE_FAILED,
    //                 Color.red,
    //                 ex.Message);
    //         }
    //     }));
    //     #endregion
    // }
}