using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    private Transform MainPanel;
    public GameObject LevelPanelPrefab;
    private GameObject currentPanel;
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        LevelPanelPrefab = UIManager.Instance.Get("LevelPanelPrefab");
    }
    public void CreateLevelPanel<T>(T stat, ItemExperienceDTO itemExp, int maxLevel, Func<int, double> expRule, Predicate<T> statFilter = null) where T : IStats
    {
        if (statFilter != null && !statFilter(stat))
        {
            Debug.LogWarning("Đối tượng stat không thỏa mãn điều kiện lọc!");
            return;
        }
        currentPanel = Instantiate(LevelPanelPrefab, MainPanel);
        Transform panelTransform = currentPanel.transform;

        // --- Khởi tạo và tìm UI Components ---
        TextMeshProUGUI currentLevelText = panelTransform.Find("CurrentLevel").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nextLevelText = panelTransform.Find("NextLevel").GetComponent<TextMeshProUGUI>();
        Slider progressionSlider = panelTransform.Find("ProgressionSlider").GetComponent<Slider>();
        Slider quantitySlider = panelTransform.Find("QuantitySlider").GetComponent<Slider>();
        TextMeshProUGUI experienceText = panelTransform.Find("ExperienceText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI userItemQuantityText = panelTransform.Find("UserItemQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemUsedQuantityText = panelTransform.Find("ItemUsedQuantityText").GetComponent<TextMeshProUGUI>();
        RawImage userItemImage = panelTransform.Find("UserItemImage").GetComponent<RawImage>();
        RawImage itemUsedImage = panelTransform.Find("ItemUsedImage").GetComponent<RawImage>();

        // TÌM COMPONENT THÔNG BÁO TRÊN UI
        TextMeshProUGUI notificationText = panelTransform.Find("Notification/ContentText").GetComponent<TextMeshProUGUI>();

        Button increaseOne = panelTransform.Find("IncreaseOneButton").GetComponent<Button>();
        Button increaseTen = panelTransform.Find("IncreaseTenButton").GetComponent<Button>();
        Button increaseMax = panelTransform.Find("IncreaseMaxButton").GetComponent<Button>();
        Button decreaseOne = panelTransform.Find("DecreaseOneButton").GetComponent<Button>();
        Button decreaseTen = panelTransform.Find("DecreaseTenButton").GetComponent<Button>();
        Button decreaseMax = panelTransform.Find("DecreaseMaxButton").GetComponent<Button>();
        Button confirm = panelTransform.Find("ConfirmButton").GetComponent<Button>();
        Button close = panelTransform.Find("CloseButton").GetComponent<Button>();

        int currentLevel = stat.Level;
        double currentExp = stat.Experience;

        int targetLevel = currentLevel;
        double targetExp = currentExp;

        long currentMaterialCount = 0;

        double expPerItem =
            itemExp.ExperienceValue > 0
                ? itemExp.ExperienceValue
                : 100;

        string texturePath =
            ImageHelper.RemoveImageExtension(itemExp.Image);

        userItemImage.texture =
            TextureHelper.LoadTexture2DCached(texturePath);

        itemUsedImage.texture =
            TextureHelper.LoadTexture2DCached(texturePath);

        quantitySlider.minValue = 0;
        quantitySlider.maxValue = Mathf.Max((float)itemExp.Quantity, 0);

        #region Local Functions

        void SetNotification(string translationKey, Color color, params object[] args)
        {
            if (notificationText == null)
                return;

            string translatedValue =
                LocalizationManager.Get(translationKey);

            if (args != null && args.Length > 0)
            {
                translatedValue =
                    string.Format(
                        translatedValue,
                        args);
            }

            notificationText.text = translatedValue;
            notificationText.color = color;
        }

        void CalculateLevelFromItems(long itemsToUse)
        {
            double totalExp =
                currentExp +
                ((double)itemsToUse * expPerItem);

            int tempLevel = currentLevel;

            while (tempLevel < maxLevel)
            {
                double requiredExp =
                    expRule(tempLevel);

                if (requiredExp <= 0)
                    break;

                if (totalExp < requiredExp)
                    break;

                totalExp -= requiredExp;
                tempLevel++;
            }

            targetLevel = tempLevel;

            if (targetLevel >= maxLevel)
            {
                targetLevel = maxLevel;
                targetExp = 0;
            }
            else
            {
                targetExp = totalExp;
            }
        }

        long CalculateMaxMaterialsNeeded()
        {
            double totalExpNeeded = 0;

            for (
                int level = currentLevel;
                level < maxLevel;
                level++)
            {
                totalExpNeeded += expRule(level);
            }

            totalExpNeeded -= currentExp;

            if (totalExpNeeded <= 0)
                return 0;

            return (long)Math.Ceiling(
                totalExpNeeded / expPerItem);
        }

        void RefreshUI()
        {
            currentLevelText.text =
                currentLevel.ToString();

            nextLevelText.text =
                targetLevel >= maxLevel
                    ? "MAX"
                    : targetLevel.ToString();

            userItemQuantityText.text =
                NumberFormatterHelper.FormatNumber(
                    itemExp.Quantity,
                    true);

            itemUsedQuantityText.text =
                NumberFormatterHelper.FormatNumber(
                    currentMaterialCount,
                    true);

            quantitySlider.SetValueWithoutNotify(
                currentMaterialCount);

            if (targetLevel >= maxLevel)
            {
                progressionSlider.value = 1f;
                experienceText.text = "MAX";
            }
            else
            {
                double requiredExp =
                    expRule(targetLevel);

                if (requiredExp <= 0)
                    requiredExp = 1;

                progressionSlider.value =
                    (float)(targetExp / requiredExp);

                experienceText.text =
                    $"{targetExp:N0}/{requiredExp:N0}";
            }

            if (currentMaterialCount <= 0)
            {
                itemUsedQuantityText.color =
                    Color.white;

                SetNotification(
                    MessageConstants.PleaseSelectQuantity,
                    Color.yellow);
            }
            else
            {
                itemUsedQuantityText.color =
                    Color.white;

                if (targetLevel >= maxLevel)
                {
                    SetNotification(
                        MessageConstants.MaxLevelReached,
                        Color.green,
                        maxLevel);
                }
                else
                {
                    SetNotification(
                        MessageConstants.ReadyToUpgrade,
                        Color.green,
                        targetLevel);
                }
            }
        }

        void ChangeMaterialCount(long amount)
        {
            currentMaterialCount += amount;

            currentMaterialCount =
                Math.Max(
                    0,
                    Math.Min(
                        currentMaterialCount,
                        (long)itemExp.Quantity));

            CalculateLevelFromItems(
                currentMaterialCount);

            RefreshUI();
        }

        void SetMaxMaterialCount()
        {
            long maxNeed =
                CalculateMaxMaterialsNeeded();

            currentMaterialCount =
                Math.Min(
                    maxNeed,
                    (long)itemExp.Quantity);

            CalculateLevelFromItems(
                currentMaterialCount);

            RefreshUI();
        }

        #endregion

        quantitySlider.onValueChanged.AddListener(value =>
        {
            currentMaterialCount =
                (long)Math.Round(value);

            CalculateLevelFromItems(
                currentMaterialCount);

            RefreshUI();
        });

        CalculateLevelFromItems(0);
        RefreshUI();


        async Task ExecuteServiceInsertAsync()
        {
            try
            {
                if (stat is Achievements achievement) await UserAchievementsService.Create().UpdateAchievementLevelAsync(achievement);
                else if (stat is Alchemies alchemy) await UserAlchemiesService.Create().UpdateAlchemyLevelAsync(alchemy);
                else if (stat is Architectures architecture) await UserArchitecturesService.Create().UpdateArchitectureLevelAsync(architecture);
                else if (stat is Artifacts artifact) await UserArtifactsService.Create().UpdateArtifactLevelAsync(artifact);
                else if (stat is Artworks artwork) await UserArtworksService.Create().UpdateArtworkLevelAsync(artwork);
                else if (stat is Avatars avatar) await UserAvatarsService.Create().UpdateAvatarLevelAsync(avatar);
                else if (stat is Badges badge) await UserBadgesService.Create().UpdateBadgeLevelAsync(badge);
                else if (stat is Beverages beverage) await UserBeveragesService.Create().UpdateBeverageLevelAsync(beverage);
                else if (stat is Books book) await UserBooksService.Create().UpdateBookLevelAsync(book);
                else if (stat is Borders border) await UserBordersService.Create().UpdateBorderLevelAsync(border);
                else if (stat is Buildings building) await UserBuildingsService.Create().UpdateBuildingLevelAsync(building);
                else if (stat is CardAdmirals admiral) await UserCardAdmiralsService.Create().UpdateCardAdmiralLevelAsync(admiral);
                else if (stat is CardCaptains captain) await UserCardCaptainsService.Create().UpdateCardCaptainLevelAsync(captain);
                else if (stat is CardColonels colonel) await UserCardColonelsService.Create().UpdateCardColonelLevelAsync(colonel);
                else if (stat is CardGenerals general) await UserCardGeneralsService.Create().UpdateCardGeneralLevelAsync(general);
                else if (stat is CardHeroes hero) await UserCardHeroesService.Create().UpdateCardHeroLevelAsync(hero);
                else if (stat is CardLives cardLife) await UserCardLivesService.Create().UpdateCardLifeLevelAsync(cardLife);
                else if (stat is CardMilitaries military) await UserCardMilitariesService.Create().UpdateCardMilitaryLevelAsync(military);
                else if (stat is CardMonsters monster) await UserCardMonstersService.Create().UpdateCardMonsterLevelAsync(monster);
                else if (stat is CardSoldiers soldier) await UserCardSoldiersService.Create().UpdateCardSoldierLevelAsync(soldier);
                else if (stat is CardSpells spell) await UserCardSpellsService.Create().UpdateCardSpellLevelAsync(spell);
                else if (stat is CollaborationEquipments collabEquip) await UserCollaborationEquipmentsService.Create().UpdateCollaborationEquipmentLevelAsync(collabEquip);
                else if (stat is Collaborations collab) await UserCollaborationsService.Create().UpdateCollaborationLevelAsync(collab);
                else if (stat is Cores core) await UserCoresService.Create().UpdateCoreLevelAsync(core);
                else if (stat is Emojis emoji) await UserEmojisService.Create().UpdateEmojiLevelAsync(emoji);
                else if (stat is Equipments equipment) await UserEquipmentsService.Create().UpdateEquipmentsLevelAsync(equipment);
                else if (stat is Fashions fashion) await UserFashionsService.Create().UpdateFashionLevelAsync(fashion);
                else if (stat is Foods food) await UserFoodsService.Create().UpdateFoodLevelAsync(food);
                else if (stat is Forges forge) await UserForgesService.Create().UpdateForgeLevelAsync(forge);
                else if (stat is Furnitures furniture) await UserFurnituresService.Create().UpdateFurnitureLevelAsync(furniture);
                else if (stat is MagicFormationCircles circle) await UserMagicFormationCirclesService.Create().UpdateMagicFormationCircleLevelAsync(circle);
                else if (stat is MechaBeasts mechaBeast) await UserMechaBeastsService.Create().UpdateMechaBeastLevelAsync(mechaBeast);
                else if (stat is Medals medal) await UserMedalsService.Create().UpdateMedalLevelAsync(medal);
                else if (stat is Pets pet) await UserPetsService.Create().UpdatePetLevelAsync(pet);
                else if (stat is Plants plant) await UserPlantsService.Create().UpdatePlantLevelAsync(plant);
                else if (stat is Puppets puppet) await UserPuppetsService.Create().UpdatePuppetLevelAsync(puppet);
                else if (stat is Relics relic) await UserRelicsService.Create().UpdateRelicLevelAsync(relic);
                else if (stat is Robots robot) await UserRobotsService.Create().UpdateRobotLevelAsync(robot);
                else if (stat is Runes rune) await UserRunesService.Create().UpdateRuneLevelAsync(rune);
                else if (stat is Skills skill) await UserSkillsService.Create().UpdateSkillsLevelAsync(skill);
                else if (stat is SpiritBeasts spiritBeast) await UserSpiritBeastsService.Create().UpdateSpiritBeastLevelAsync(spiritBeast);
                else if (stat is SpiritCards spiritCard) await UserSpiritCardsService.Create().UpdateSpiritCardLevelAsync(spiritCard);
                else if (stat is Symbols symbol) await UserSymbolsService.Create().UpdateSymbolLevelAsync(symbol);
                else if (stat is Talismans talisman) await UserTalismansService.Create().UpdateTalismanLevelAsync(talisman);
                else if (stat is Technologies technology) await UserTechnologiesService.Create().UpdateTechnologyLevelAsync(technology);
                else if (stat is Titles title) await UserTitlesService.Create().UpdateTitleLevelAsync(title);
                else if (stat is Vehicles vehicle) await UserVehiclesService.Create().UpdateVehicleLevelAsync(vehicle);
                else if (stat is Weapons weapon) await UserWeaponsService.Create().UpdateWeaponLevelAsync(weapon);
                else if (stat is Outfits outfit) await UserOutfitsService.Create().UpdateOutfitLevelAsync(outfit);
            }
            catch (Exception ex)
            {
                SetNotification(MessageConstants.ServerError, Color.red, ex.Message);
                throw;
            }
        }

        #region Button Events listeners
        increaseOne.onClick.AddListener(() =>
        {
            ChangeMaterialCount(1);
        });

        increaseTen.onClick.AddListener(() =>
        {
            ChangeMaterialCount(10);
        });

        increaseMax.onClick.AddListener(() =>
        {
            SetMaxMaterialCount();
        });

        decreaseOne.onClick.AddListener(() =>
        {
            ChangeMaterialCount(-1);
        });

        decreaseTen.onClick.AddListener(() =>
        {
            ChangeMaterialCount(-10);
        });

        decreaseMax.onClick.AddListener(() =>
        {
            currentMaterialCount = 0;

            CalculateLevelFromItems(0);

            RefreshUI();
        });

        close.onClick.AddListener(() => Destroy(currentPanel));
        confirm.onClick.AddListener(async () =>
        {
            if (currentMaterialCount > itemExp.Quantity)
            {
                SetNotification(MessageConstants.NotEnoughMaterials, Color.red);
                return;
            }
            if (currentMaterialCount <= 0)
            {
                SetNotification(MessageConstants.PleaseSelectQuantity, Color.red);
                return;
            }

            confirm.interactable = false;
            SetNotification(MessageConstants.ProcessingUpgrade, Color.cyan);

            // --- BƯỚC 1: Tính toán chuẩn xác dữ liệu mới cho stat ---
            double totalExpGained = (double)currentMaterialCount * expPerItem;
            double remainingExpInTargetLevel = currentExp + totalExpGained;

            for (int i = currentLevel; i < targetLevel; i++)
            {
                remainingExpInTargetLevel -= expRule(i);
            }

            if (targetLevel >= maxLevel)
            {
                remainingExpInTargetLevel = 0;
            }

            stat.Level = targetLevel;
            stat.Experience = remainingExpInTargetLevel;

            // --- BƯỚC 2: API trừ nguyên liệu và gửi dữ liệu stat lên Server ---
            try
            {
                Items materialItem = new Items { Id = itemExp.Id };
                double usedQuantity = -(double)currentMaterialCount;

                await UserItemsService.Create().InsertOrUpdateUserItemQuantityAsync(User.CurrentUserId, materialItem, usedQuantity);
                await ExecuteServiceInsertAsync();

                SetNotification(MessageConstants.UpgradeSuccess, Color.green);
                await Task.Delay(500);
                Destroy(currentPanel);
            }
            catch (Exception ex)
            {
                confirm.interactable = true;
                SetNotification(MessageConstants.UpgradeFailed, Color.red, ex.Message);
            }
        });
        #endregion
    }
}