using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StarController : MonoBehaviour
{
    public static StarController Instance { get; private set; }
    private Transform MainPanel;
    public GameObject StarPanelPrefab;
    private GameObject CurrentPanel;
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

        int currentStar = stat.Star;
        int targetStar = currentStar;

        int currentMaterialCount = 0;

        string texturePath =
            ImageHelper.RemoveImageExtension("UI/Icon/storage");

        userItemImage.texture =
            TextureHelper.LoadTexture2DCached(texturePath);

        usedItemImage.texture =
            TextureHelper.LoadTexture2DCached(texturePath);

        quantitySlider.minValue = 0;
        quantitySlider.maxValue = (float)stat.Quantity;

        #region Local Functions
        void SetNotification(string translationKey, Color color, params object[] args)
        {
            string translatedValue =
                LocalizationManager.Get(translationKey);

            if (args != null && args.Length > 0)
            {
                translatedValue =
                    string.Format(translatedValue, args);
            }

            notificationText.text = translatedValue;
            notificationText.color = color;
        }

        void SetConfirmButtonState(bool interactable, bool isMax = false)
        {
            confirmButton.interactable = interactable;
            var backgroundImage = confirmButton.transform.Find("Background2")?.GetComponent<RawImage>();
            if (backgroundImage != null)
            {
                backgroundImage.color = isMax && !interactable
                    ? Color.gray
                    : Color.white;
            }
        }

        void CalculateStarFromMaterials(long materials)
        {
            double remain = materials;

            int tempStar = currentStar;

            while (tempStar < maxStar)
            {
                double required =
                    starRule(tempStar);

                if (required <= 0)
                    break;

                if (remain < required)
                    break;

                remain -= required;
                tempStar++;
            }

            targetStar = tempStar;
        }

        int CalculateMaxMaterialsNeeded()
        {
            double total = 0;

            for (int star = currentStar; star < maxStar; star++)
            {
                total += starRule(star);
            }

            return (int)Math.Ceiling(total);
        }

        void RefreshUI()
        {
            TextureHelper.SetupStars(currentStarTransform, currentStar);
            TextureHelper.SetupStars(nextStarTransform, targetStar);
            userQuantityText.text = NumberFormatterHelper.FormatNumber(stat.Quantity, true);
            usedQuantityText.text = NumberFormatterHelper.FormatNumber(currentMaterialCount, true);
            quantitySlider.SetValueWithoutNotify(
                currentMaterialCount);

            if (currentStar >= maxStar)
            {
                progressionSlider.minValue = 0;
                progressionSlider.maxValue = 1;
                progressionSlider.SetValueWithoutNotify(1);

                experienceText.text = "MAX";
                SetConfirmButtonState(false, true);
                return;
            }

            double required = starRule(currentStar);

            double progress = Math.Min(currentMaterialCount, required);

            progressionSlider.minValue = 0;
            progressionSlider.maxValue = (float)required;
            progressionSlider.SetValueWithoutNotify((float)progress);

            experienceText.text =
                $"{NumberFormatterHelper.FormatNumber(progress, true)} / {NumberFormatterHelper.FormatNumber(required, true)}";

            if (currentMaterialCount <= 0)
            {
                SetNotification(MessageConstants.PLEASE_SELECT_QUANTITY, Color.yellow);
                SetConfirmButtonState(false);
            }
            else if (targetStar >= maxStar)
            {
                SetNotification(MessageConstants.MAX_LEVEL_REACHED, Color.green, maxStar);
                SetConfirmButtonState(false, true);
            }
            else
            {
                SetNotification(MessageConstants.READY_TO_UPGRADE, Color.green, targetStar);
                SetConfirmButtonState(true);
            }
        }

        void ChangeMaterialCount(int amount)
        {
            currentMaterialCount += amount;

            currentMaterialCount =
                Math.Max(
                    0,
                    Math.Min(
                        currentMaterialCount,
                        stat.Quantity));

            CalculateStarFromMaterials(
                currentMaterialCount);

            RefreshUI();
        }

        void SetMaxMaterialCount()
        {
            int maxNeed =
                CalculateMaxMaterialsNeeded();

            currentMaterialCount =
                Math.Min(
                    maxNeed,
                    stat.Quantity);

            CalculateStarFromMaterials(
                currentMaterialCount);

            RefreshUI();
        }
        #endregion

        quantitySlider.onValueChanged.AddListener(value =>
        {
            currentMaterialCount =
                (int)Math.Round(value);

            CalculateStarFromMaterials(
                currentMaterialCount);

            RefreshUI();
        });

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
            ChangeMaterialCount(1);
        });

        increaseTenButton.onClick.AddListener(() =>
        {
            ChangeMaterialCount(10);
        });

        increaseMaxButton.onClick.AddListener(() =>
        {
            SetMaxMaterialCount();
        });

        decreaseOneButton.onClick.AddListener(() =>
        {
            ChangeMaterialCount(-1);
        });

        decreaseTenButton.onClick.AddListener(() =>
        {
            ChangeMaterialCount(-10);
        });

        decreaseMaxButton.onClick.AddListener(() =>
        {
            currentMaterialCount = 0;
            CalculateStarFromMaterials(0);
            RefreshUI();
        });

        closeButton.onClick.AddListener(() =>
        {
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
                SetNotification(
                    MessageConstants.PLEASE_SELECT_QUANTITY,
                    Color.red);

                return;
            }

            if (currentMaterialCount > stat.Quantity)
            {
                SetNotification(
                    MessageConstants.NOT_ENOUGH_MATERIALS,
                    Color.red);

                return;
            }

            confirmButton.interactable = false;
            closeButton.interactable = false;

            try
            {
                stat.Star = targetStar;
                stat.Quantity = Math.Max(0, stat.Quantity - currentMaterialCount);

                await ExecuteServiceInsertAsync();

                SetNotification(
                    MessageConstants.UPGRADE_SUCCESS,
                    Color.green);

                await Task.Delay(500);

                Destroy(CurrentPanel);
            }
            catch (Exception ex)
            {
                confirmButton.interactable = true;
                closeButton.interactable = true;

                SetNotification(
                    MessageConstants.UPGRADE_FAILED,
                    Color.red,
                    ex.Message);
            }
        }));
        #endregion
    }
}