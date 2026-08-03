using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class StarController : MonoBehaviour
{
    public static StarController Instance { get; private set; }
    private Transform MainPanel;
    public GameObject StarPanelPrefab;
    private GameObject CurrentPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        StarPanelPrefab = UIManager.Instance.Get(AppConstants.Prefab.Progression.STAR_PANEL_PREFAB);
    }

    public void CreateStarPanel<T>(T stat, int maxStar, Func<int, double> starRule, Predicate<T> statFilter = null) where T : IStats
    {
        if (statFilter != null && !statFilter(stat))
        {
            Debug.LogWarning("Đối tượng stat không thỏa mãn điều kiện lọc!");
            return;
        }

        CurrentPanel = Instantiate(StarPanelPrefab, MainPanel);
        Transform panelTransform = CurrentPanel.transform;

        // --- Khởi tạo và tìm UI Components ---
        Transform currentStarTransform = panelTransform.Find("CurrentStarGridLayout");
        Transform nextStarTransform = panelTransform.Find("NextStarGridLayout");
        Slider progressionSlider = panelTransform.Find("ProgressionSlider").GetComponent<Slider>();
        Slider quantitySlider = panelTransform.Find("QuantitySlider").GetComponent<Slider>();
        TextMeshProUGUI experienceText = panelTransform.Find("ExperienceText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI userQuantityText = panelTransform.Find("UserItemQuantityText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI usedQuantityText = panelTransform.Find("ItemUsedQuantityText").GetComponent<TextMeshProUGUI>();
        RawImage userItemImage = panelTransform.Find("UserItemImage").GetComponent<RawImage>();
        RawImage usedItemImage = panelTransform.Find("ItemUsedImage").GetComponent<RawImage>();

        TextMeshProUGUI notificationText = panelTransform.Find("Notification/ContentText").GetComponent<TextMeshProUGUI>();

        Button increaseOneButton = panelTransform.Find("IncreaseOneButton").GetComponent<Button>();
        Button increaseTenButton = panelTransform.Find("IncreaseTenButton").GetComponent<Button>();
        Button increaseMaxButton = panelTransform.Find("IncreaseMaxButton").GetComponent<Button>();
        Button decreaseOneButton = panelTransform.Find("DecreaseOneButton").GetComponent<Button>();
        Button decreaseTenButton = panelTransform.Find("DecreaseTenButton").GetComponent<Button>();
        Button decreaseMaxButton = panelTransform.Find("DecreaseMaxButton").GetComponent<Button>();
        Button confirmButton = panelTransform.Find("ConfirmButton").GetComponent<Button>();
        Button closeButton = panelTransform.Find("CloseButton").GetComponent<Button>();
        Transform currentStatsContent = panelTransform.Find("Scroll View/Viewport/Content/CurrentStats");
        Transform nextStatsContent = panelTransform.Find("Scroll View/Viewport/Content/NextStats");

        int currentStar = stat.Star;
        int targetStar = currentStar;

        int currentMaterialCount = 0;
        int lastTargetStar = -1;

        string texturePath = ImageHelper.RemoveImageExtension("UI/Icon/storage");

        userItemImage.texture = TextureHelper.LoadTexture2DCached(texturePath);
        usedItemImage.texture = TextureHelper.LoadTexture2DCached(texturePath);

        quantitySlider.minValue = 0;
        quantitySlider.maxValue = (float)stat.Quantity;

        #region Local Functions

        void SetNotification(string translationKey, Color color, params object[] args)
        {
            if (notificationText == null) return;

            string translatedValue = LocalizationManager.Get(translationKey);

            if (args != null && args.Length > 0)
            {
                translatedValue = string.Format(translatedValue, args);
            }

            notificationText.text = translatedValue;
            notificationText.color = color;
        }

        void SetConfirmButtonState(bool interactable, bool isMax = false)
        {
            confirmButton.interactable = interactable;
        }

        void CalculateStarFromMaterials(long materials)
        {
            double remain = materials;
            int tempStar = currentStar;

            while (tempStar < maxStar)
            {
                double required = starRule(tempStar);

                if (required <= 0) break;
                if (remain < required) break;

                remain -= required;
                tempStar++;
            }

            targetStar = tempStar;
        }

        int CalculateMaxMaterialsNeeded()
        {
            // Nếu nguyên liệu hiện có bằng 0 hoặc đã Max Star thì không cần tính
            if (stat.Quantity <= 0 || currentStar >= maxStar) return 0;

            double total = 0;

            for (int star = currentStar; star < maxStar; star++)
            {
                total += starRule(star);
                // Tránh cộng dồn quá lớn gây tràn số double/int
                if (total >= int.MaxValue)
                {
                    return int.MaxValue;
                }
            }

            // Ép kiểu an toàn tránh overflow sang số âm
            long ceilingTotal = (long)Math.Ceiling(total);
            return (int)Math.Min(ceilingTotal, int.MaxValue);
        }

        // Tạo bản Clone Preview BaseStats theo targetStar
        IStats CreatePreviewBaseStats(int newStar)
        {
            try
            {
                var baseStatsProp = typeof(T).GetProperty("BaseStats");
                object originalBaseStats = baseStatsProp?.GetValue(stat);

                object sourceToClone = originalBaseStats ?? stat;
                if (sourceToClone == null) return null;

                string json = JsonConvert.SerializeObject(sourceToClone);
                Type typeToDeserialize = sourceToClone.GetType();
                IStats previewBaseStats = (IStats)JsonConvert.DeserializeObject(json, typeToDeserialize);

                if (previewBaseStats != null)
                {
                    previewBaseStats.Star = newStar;

                    QualityEvaluatorHelper.GetQualityPower(previewBaseStats);
                    LevelEvaluatorHelper.GetLevelPower(previewBaseStats);
                    StarEvaluatorHelper.GetStarPower(previewBaseStats);
                }

                return previewBaseStats;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Lỗi khi clone Preview BaseStats: {ex.Message}");
                return null;
            }
        }

        // 1. Render CurrentStats (Chỉ gọi 1 lần khi tạo)
        void RenderCurrentStats()
        {
            if (currentStatsContent == null) return;

            var baseStatsProp = typeof(T).GetProperty("BaseStats");
            object currentBaseStatsObj = baseStatsProp?.GetValue(stat);

            IStats currentDisplayStats = stat;
            if (currentBaseStatsObj != null)
            {
                string json = JsonConvert.SerializeObject(currentBaseStatsObj);
                currentDisplayStats = (IStats)JsonConvert.DeserializeObject(json, currentBaseStatsObj.GetType());
            }

            if (currentDisplayStats != null)
            {
                currentDisplayStats.Star = currentStar;
                QualityEvaluatorHelper.GetQualityPower(currentDisplayStats);
                LevelEvaluatorHelper.GetLevelPower(currentDisplayStats);
                StarEvaluatorHelper.GetStarPower(currentDisplayStats);

                StatsManager.Instance.CreateStatsManager(currentDisplayStats, currentStatsContent);
            }
        }

        // 2. Render NextStats (Chỉ gọi khi Target Star thực sự thay đổi)
        void RenderNextStats()
        {
            if (nextStatsContent == null) return;

            if (targetStar == currentStar)
            {
                var baseStatsProp = typeof(T).GetProperty("BaseStats");
                object currentBaseStatsObj = baseStatsProp?.GetValue(stat);

                IStats currentDisplayStats = stat;
                if (currentBaseStatsObj != null)
                {
                    string json = JsonConvert.SerializeObject(currentBaseStatsObj);
                    currentDisplayStats = (IStats)JsonConvert.DeserializeObject(json, currentBaseStatsObj.GetType());
                }

                if (currentDisplayStats != null)
                {
                    currentDisplayStats.Star = currentStar;
                    QualityEvaluatorHelper.GetQualityPower(currentDisplayStats);
                    LevelEvaluatorHelper.GetLevelPower(currentDisplayStats);
                    StarEvaluatorHelper.GetStarPower(currentDisplayStats);

                    StatsManager.Instance.CreateStatsManager(currentDisplayStats, nextStatsContent);
                }
            }
            else
            {
                IStats previewBaseStats = CreatePreviewBaseStats(targetStar);
                if (previewBaseStats != null)
                {
                    StatsManager.Instance.CreateStatsManager(previewBaseStats, nextStatsContent);
                }
            }
        }

        // 3. Refresh UI chính
        void RefreshUI()
        {
            TextureHelper.SetupStars(currentStarTransform, currentStar);
            TextureHelper.SetupStars(nextStarTransform, targetStar);

            userQuantityText.text = NumberFormatterHelper.FormatNumber(stat.Quantity, true);
            usedQuantityText.text = NumberFormatterHelper.FormatNumber(currentMaterialCount, true);
            quantitySlider.SetValueWithoutNotify(currentMaterialCount);

            // B. TÍNH TIẾN ĐỘ VÀ EXP TEXT
            if (currentStar >= maxStar)
            {
                progressionSlider.minValue = 0;
                progressionSlider.maxValue = 1;
                progressionSlider.SetValueWithoutNotify(1);
                experienceText.text = "MAX";
            }
            else
            {
                double required = starRule(currentStar);
                if (required <= 0) required = 1;

                double progress = Math.Min(currentMaterialCount, required);

                progressionSlider.minValue = 0;
                progressionSlider.maxValue = (float)required;
                progressionSlider.SetValueWithoutNotify((float)progress);

                experienceText.text = $"{NumberFormatterHelper.FormatNumber(progress, true)} / {NumberFormatterHelper.FormatNumber(required, true)}";
            }

            // C. CHỈ RE-RENDER NEXT STATS KHI TARGET STAR THAY ĐỔI
            if (targetStar != lastTargetStar)
            {
                RenderNextStats();
                lastTargetStar = targetStar;
            }

            // D. THÔNG BÁO & TRẠNG THÁI NÚT
            if (currentStar >= maxStar)
            {
                SetNotification(MessageConstants.UPGRADE_ALREADY_MAX, Color.green);
                SetConfirmButtonState(false, true);
            }
            else if (currentMaterialCount <= 0)
            {
                SetNotification(MessageConstants.PLEASE_SELECT_QUANTITY, Color.yellow);
                SetConfirmButtonState(false);
            }
            else if (currentMaterialCount < starRule(currentStar))
            {
                SetNotification(MessageConstants.NOT_ENOUGH_MATERIALS, Color.red);
                SetConfirmButtonState(false);
            }
            else if (targetStar >= maxStar)
            {
                SetNotification(MessageConstants.MAX_LEVEL_REACHED, Color.green, maxStar);
                SetConfirmButtonState(true);
            }
            else
            {
                SetNotification(MessageConstants.READY_TO_UPGRADE, Color.green, targetStar);
                SetConfirmButtonState(true);
            }
        }

        void ChangeMaterialCount(int amount)
        {
            // Nếu người chơi không có nguyên liệu, luôn giữ là 0
            if (stat.Quantity <= 0)
            {
                currentMaterialCount = 0;
                CalculateStarFromMaterials(0);
                RefreshUI();
                return;
            }

            currentMaterialCount += amount;
            // Đảm bảo giá trị luôn nằm trong khoảng [0, stat.Quantity]
            currentMaterialCount = Math.Max(0, Math.Min(currentMaterialCount, stat.Quantity));

            CalculateStarFromMaterials(currentMaterialCount);
            RefreshUI();
        }

        void SetMaxMaterialCount()
        {
            // 1. Kiểm tra điều kiện chặn ngay từ đầu
            if (currentStar >= maxStar || stat.Quantity <= 0)
            {
                currentMaterialCount = 0;
                CalculateStarFromMaterials(0);
                RefreshUI();
                return;
            }

            // 2. Tính toán nguyên liệu max cần thiết
            int maxNeed = CalculateMaxMaterialsNeeded();

            // 3. Gán giá trị an toàn
            currentMaterialCount = Math.Min(maxNeed, stat.Quantity);
            currentMaterialCount = Math.Max(0, currentMaterialCount); // Bảo đảm không bao giờ âm

            CalculateStarFromMaterials(currentMaterialCount);
            RefreshUI();
        }

        #endregion

        quantitySlider.onValueChanged.AddListener(value =>
        {
            currentMaterialCount = (int)Math.Round(value);
            CalculateStarFromMaterials(currentMaterialCount);
            RefreshUI();
        });

        RenderCurrentStats();
        CalculateStarFromMaterials(0);
        RefreshUI();

        async Task ExecuteServiceInsertAsync()
        {
            try
            {
                if (stat is Achievements achievement)
                {
                    await UserAchievementsService.Create().UpdateUserAchievementStarAsync(User.CurrentUserId, achievement);
                    UserAchievementsController.Instance.RefreshCurrentDetailsUI(achievement);
                }
                else if (stat is Alchemies alchemy)
                {
                    await UserAlchemiesService.Create().UpdateUserAlchemyStarAsync(User.CurrentUserId, alchemy);
                    UserAlchemiesController.Instance.RefreshCurrentDetailsUI(alchemy);
                }
                else if (stat is Architectures architecture)
                {
                    await UserArchitecturesService.Create().UpdateUserArchitectureStarAsync(User.CurrentUserId, architecture);
                    UserArchitecturesController.Instance.RefreshCurrentDetailsUI(architecture);
                }
                else if (stat is Artifacts artifact)
                {
                    await UserArtifactsService.Create().UpdateUserArtifactStarAsync(User.CurrentUserId, artifact);
                    UserArtifactsController.Instance.RefreshCurrentDetailsUI(artifact);
                }
                else if (stat is Artworks artwork)
                {
                    await UserArtworksService.Create().UpdateUserArtworkStarAsync(User.CurrentUserId, artwork);
                    UserArtworksController.Instance.RefreshCurrentDetailsUI(artwork);
                }
                else if (stat is Avatars avatar)
                {
                    await UserAvatarsService.Create().UpdateUserAvatarStarAsync(User.CurrentUserId, avatar);
                    UserAvatarsController.Instance.RefreshCurrentDetailsUI(avatar);
                }
                else if (stat is Badges badge)
                {
                    await UserBadgesService.Create().UpdateUserBadgeStarAsync(User.CurrentUserId, badge);
                    UserBadgesController.Instance.RefreshCurrentDetailsUI(badge);
                }
                else if (stat is Beverages beverage)
                {
                    await UserBeveragesService.Create().UpdateUserBeverageStarAsync(User.CurrentUserId, beverage);
                    UserBeveragesController.Instance.RefreshCurrentDetailsUI(beverage);
                }
                else if (stat is Books book)
                {
                    await UserBooksService.Create().UpdateUserBookStarAsync(User.CurrentUserId, book);
                    UserBooksController.Instance.RefreshCurrentDetailsUI(book);
                }
                else if (stat is Borders border)
                {
                    await UserBordersService.Create().UpdateUserBorderStarAsync(User.CurrentUserId, border);
                    UserBordersController.Instance.RefreshCurrentDetailsUI(border);
                }
                else if (stat is Buildings building)
                {
                    await UserBuildingsService.Create().UpdateUserBuildingStarAsync(User.CurrentUserId, building);
                    UserBuildingsController.Instance.RefreshCurrentDetailsUI(building);
                }
                else if (stat is CardAdmirals admiral)
                {
                    await UserCardAdmiralsService.Create().UpdateUserCardAdmiralStarAsync(User.CurrentUserId, admiral);
                    UserCardAdmiralsController.Instance.RefreshCurrentDetailsUI(admiral);
                }
                else if (stat is CardCaptains captain)
                {
                    await UserCardCaptainsService.Create().UpdateUserCardCaptainStarAsync(User.CurrentUserId, captain);
                    UserCardCaptainsController.Instance.RefreshCurrentDetailsUI(captain);
                }
                else if (stat is CardColonels colonel)
                {
                    await UserCardColonelsService.Create().UpdateUserCardColonelStarAsync(User.CurrentUserId, colonel);
                    UserCardColonelsController.Instance.RefreshCurrentDetailsUI(colonel);
                }
                else if (stat is CardGenerals general)
                {
                    await UserCardGeneralsService.Create().UpdateUserCardGeneralStarAsync(User.CurrentUserId, general);
                    UserCardGeneralsController.Instance.RefreshCurrentDetailsUI(general);
                }
                else if (stat is CardHeroes hero)
                {
                    await UserCardHeroesService.Create().UpdateUserCardHeroStarAsync(User.CurrentUserId, hero);
                    UserCardHeroesController.Instance.RefreshCurrentDetailsUI(hero);
                }
                else if (stat is CardLives cardLife)
                {
                    await UserCardLivesService.Create().UpdateUserCardLifeStarAsync(User.CurrentUserId, cardLife);
                    UserCardLivesController.Instance.RefreshCurrentDetailsUI(cardLife);
                }
                else if (stat is CardMilitaries military)
                {
                    await UserCardMilitariesService.Create().UpdateUserCardMilitaryStarAsync(User.CurrentUserId, military);
                    UserCardMilitariesController.Instance.RefreshCurrentDetailsUI(military);
                }
                else if (stat is CardMonsters monster)
                {
                    await UserCardMonstersService.Create().UpdateUserCardMonsterStarAsync(User.CurrentUserId, monster);
                    UserCardMonstersController.Instance.RefreshCurrentDetailsUI(monster);
                }
                else if (stat is CardSoldiers soldier)
                {
                    await UserCardSoldiersService.Create().UpdateUserCardSoldierStarAsync(User.CurrentUserId, soldier);
                    UserCardSoldiersController.Instance.RefreshCurrentDetailsUI(soldier);
                }
                else if (stat is CardSpells spell)
                {
                    await UserCardSpellsService.Create().UpdateUserCardSpellStarAsync(User.CurrentUserId, spell);
                    UserCardSpellsController.Instance.RefreshCurrentDetailsUI(spell);
                }
                else if (stat is CollaborationEquipments collabEquip)
                {
                    await UserCollaborationEquipmentsService.Create().UpdateUserCollaborationEquipmentStarAsync(User.CurrentUserId, collabEquip);
                    UserCollaborationEquipmentsController.Instance.RefreshCurrentDetailsUI(collabEquip);
                }
                else if (stat is Collaborations collab)
                {
                    await UserCollaborationsService.Create().UpdateUserCollaborationStarAsync(User.CurrentUserId, collab);
                    UserCollaborationsController.Instance.RefreshCurrentDetailsUI(collab);
                }
                else if (stat is Cores core)
                {
                    await UserCoresService.Create().UpdateUserCoreStarAsync(User.CurrentUserId, core);
                    UserCoresController.Instance.RefreshCurrentDetailsUI(core);
                }
                else if (stat is Emojis emoji)
                {
                    await UserEmojisService.Create().UpdateUserEmojiStarAsync(User.CurrentUserId, emoji);
                    UserEmojisController.Instance.RefreshCurrentDetailsUI(emoji);
                }
                else if (stat is Equipments equipment)
                {
                    await UserEquipmentsService.Create().UpdateUserEquipmentStarAsync(User.CurrentUserId, equipment);
                    UserEquipmentsController.Instance.RefreshCurrentDetailsUI(equipment);
                }
                else if (stat is Fashions fashion)
                {
                    await UserFashionsService.Create().UpdateUserFashionStarAsync(User.CurrentUserId, fashion);
                    UserFashionsController.Instance.RefreshCurrentDetailsUI(fashion);
                }
                else if (stat is Foods food)
                {
                    await UserFoodsService.Create().UpdateUserFoodStarAsync(User.CurrentUserId, food);
                    UserFoodsController.Instance.RefreshCurrentDetailsUI(food);
                }
                else if (stat is Forges forge)
                {
                    await UserForgesService.Create().UpdateUserForgeStarAsync(User.CurrentUserId, forge);
                    UserForgesController.Instance.RefreshCurrentDetailsUI(forge);
                }
                else if (stat is Furnitures furniture)
                {
                    await UserFurnituresService.Create().UpdateUserFurnitureStarAsync(User.CurrentUserId, furniture);
                    UserFurnituresController.Instance.RefreshCurrentDetailsUI(furniture);
                }
                else if (stat is MagicFormationCircles circle)
                {
                    await UserMagicFormationCirclesService.Create().UpdateUserMagicFormationCircleStarAsync(User.CurrentUserId, circle);
                    UserMagicFormationCirclesController.Instance.RefreshCurrentDetailsUI(circle);
                }
                else if (stat is MechaBeasts mechaBeast)
                {
                    await UserMechaBeastsService.Create().UpdateUserMechaBeastStarAsync(User.CurrentUserId, mechaBeast);
                    UserMechaBeastsController.Instance.RefreshCurrentDetailsUI(mechaBeast);
                }
                else if (stat is Medals medal)
                {
                    await UserMedalsService.Create().UpdateUserMedalStarAsync(User.CurrentUserId, medal);
                    UserMedalsController.Instance.RefreshCurrentDetailsUI(medal);
                }
                else if (stat is Pets pet)
                {
                    await UserPetsService.Create().UpdateUserPetStarAsync(User.CurrentUserId, pet);
                    UserPetsController.Instance.RefreshCurrentDetailsUI(pet);
                }
                else if (stat is Plants plant)
                {
                    await UserPlantsService.Create().UpdateUserPlantStarAsync(User.CurrentUserId, plant);
                    UserPlantsController.Instance.RefreshCurrentDetailsUI(plant);
                }
                else if (stat is Puppets puppet)
                {
                    await UserPuppetsService.Create().UpdateUserPuppetStarAsync(User.CurrentUserId, puppet);
                    UserPuppetsController.Instance.RefreshCurrentDetailsUI(puppet);
                }
                else if (stat is Relics relic)
                {
                    await UserRelicsService.Create().UpdateUserRelicStarAsync(User.CurrentUserId, relic);
                    UserRelicsController.Instance.RefreshCurrentDetailsUI(relic);
                }
                else if (stat is Robots robot)
                {
                    await UserRobotsService.Create().UpdateUserRobotStarAsync(User.CurrentUserId, robot);
                    UserRobotsController.Instance.RefreshCurrentDetailsUI(robot);
                }
                else if (stat is Runes rune)
                {
                    await UserRunesService.Create().UpdateUserRuneStarAsync(User.CurrentUserId, rune);
                    UserRunesController.Instance.RefreshCurrentDetailsUI(rune);
                }
                else if (stat is Skills skill)
                {
                    await UserSkillsService.Create().UpdateUserSkillStarAsync(User.CurrentUserId, skill);
                    UserSkillsController.Instance.RefreshCurrentDetailsUI(skill);
                }
                else if (stat is SpiritBeasts spiritBeast)
                {
                    await UserSpiritBeastsService.Create().UpdateUserSpiritBeastStarAsync(User.CurrentUserId, spiritBeast);
                    UserSpiritBeastsController.Instance.RefreshCurrentDetailsUI(spiritBeast);
                }
                else if (stat is SpiritCards spiritCard)
                {
                    await UserSpiritCardsService.Create().UpdateUserSpiritCardStarAsync(User.CurrentUserId, spiritCard);
                    UserSpiritCardsController.Instance.RefreshCurrentDetailsUI(spiritCard);
                }
                else if (stat is Symbols symbol)
                {
                    await UserSymbolsService.Create().UpdateUserSymbolStarAsync(User.CurrentUserId, symbol);
                    UserSymbolsController.Instance.RefreshCurrentDetailsUI(symbol);
                }
                else if (stat is Talismans talisman)
                {
                    await UserTalismansService.Create().UpdateUserTalismanStarAsync(User.CurrentUserId, talisman);
                    UserTalismansController.Instance.RefreshCurrentDetailsUI(talisman);
                }
                else if (stat is Technologies technology)
                {
                    await UserTechnologiesService.Create().UpdateUserTechnologyStarAsync(User.CurrentUserId, technology);
                    UserTechnologiesController.Instance.RefreshCurrentDetailsUI(technology);
                }
                else if (stat is Titles title)
                {
                    await UserTitlesService.Create().UpdateUserTitleStarAsync(User.CurrentUserId, title);
                    UserTitlesController.Instance.RefreshCurrentDetailsUI(title);
                }
                else if (stat is Vehicles vehicle)
                {
                    await UserVehiclesService.Create().UpdateUserVehicleStarAsync(User.CurrentUserId, vehicle);
                    UserVehiclesController.Instance.RefreshCurrentDetailsUI(vehicle);
                }
                else if (stat is Weapons weapon)
                {
                    await UserWeaponsService.Create().UpdateUserWeaponStarAsync(User.CurrentUserId, weapon);
                    UserWeaponsController.Instance.RefreshCurrentDetailsUI(weapon);
                }
                else if (stat is Outfits outfit)
                {
                    await UserOutfitsService.Create().UpdateUserOutfitStarAsync(User.CurrentUserId, outfit);
                    UserOutfitsController.Instance.RefreshCurrentDetailsUI(outfit);
                }
            }
            catch (Exception ex)
            {
                SetNotification(MessageConstants.SERVER_ERROR, Color.red, ex.Message);
                throw;
            }
        }

        #region Button Events listeners
        increaseOneButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ChangeMaterialCount(1);
        });

        increaseTenButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ChangeMaterialCount(10);
        });

        increaseMaxButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            SetMaxMaterialCount();
        });

        decreaseOneButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ChangeMaterialCount(-1);
        });

        decreaseTenButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ChangeMaterialCount(-10);
        });

        decreaseMaxButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            currentMaterialCount = 0;
            CalculateStarFromMaterials(0);
            RefreshUI();
        });

        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(CurrentPanel);
        });

        confirmButton.onClick.AddListener((UnityEngine.Events.UnityAction)(async () =>
        {
            if (currentStar >= maxStar)
            {
                notificationText.text = MessageConstants.UPGRADE_ALREADY_MAX;
                notificationText.color = Color.red;
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                return;
            }

            AudioManager.Instance.PlaySFX(AudioConstants.SFX.LEVEL_UP_SOUND);

            if (currentMaterialCount <= 0)
            {
                SetNotification(MessageConstants.PLEASE_SELECT_QUANTITY, Color.red);
                return;
            }

            if (currentMaterialCount > stat.Quantity)
            {
                SetNotification(MessageConstants.NOT_ENOUGH_MATERIALS, Color.red);
                return;
            }

            confirmButton.interactable = false;
            closeButton.interactable = false;

            SetNotification(MessageConstants.PROCESSING_UPGRADE, Color.cyan);

            try
            {
                // Cập nhật giá trị Star mới cho stat gốc
                stat.Star = targetStar;
                stat.Quantity = Math.Max(0, stat.Quantity - currentMaterialCount);

                // Cập nhật BaseStats (nếu có)
                var baseStatsProp = typeof(T).GetProperty("BaseStats");
                if (baseStatsProp != null)
                {
                    var baseStatsObj = baseStatsProp.GetValue(stat);
                    if (baseStatsObj != null)
                    {
                        var baseStarProp = baseStatsObj.GetType().GetProperty("Star");
                        if (baseStarProp != null && baseStarProp.CanWrite)
                        {
                            baseStarProp.SetValue(baseStatsObj, targetStar);
                        }
                    }
                }

                // Cập nhật lại sức mạnh
                QualityEvaluatorHelper.GetQualityPower(stat);
                LevelEvaluatorHelper.GetLevelPower(stat);
                StarEvaluatorHelper.GetStarPower(stat);

                // Gọi Service lưu vào DB
                await ExecuteServiceInsertAsync();

                SetNotification(MessageConstants.UPGRADE_SUCCESS, Color.green);

                await Task.Delay(500);

                Destroy(CurrentPanel);
            }
            catch (Exception ex)
            {
                confirmButton.interactable = true;
                closeButton.interactable = true;

                SetNotification(MessageConstants.UPGRADE_FAILED, Color.red, ex.Message);
            }
        }));
        #endregion
    }
}