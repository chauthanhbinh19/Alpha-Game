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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        StarPanelPrefab = UIManager.Instance.Get("StarPanelPrefab");
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

        long currentMaterialCount = 0;

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

        long CalculateMaxMaterialsNeeded()
        {
            double total = 0;

            for (int star = currentStar; star < maxStar; star++)
            {
                total += starRule(star);
            }

            return (long)Math.Ceiling(total);
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

        void ChangeMaterialCount(long amount)
        {
            currentMaterialCount += amount;

            currentMaterialCount =
                Math.Max(
                    0,
                    Math.Min(
                        currentMaterialCount,
                        (long)stat.Quantity));

            CalculateStarFromMaterials(
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
                    (long)stat.Quantity);

            CalculateStarFromMaterials(
                currentMaterialCount);

            RefreshUI();
        }
        #endregion

        quantitySlider.onValueChanged.AddListener(value =>
        {
            currentMaterialCount =
                (long)Math.Round(value);

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
                    await UserAchievementsService.Create().UpdateAchievementStarAsync(achievement);
                    UserAchievementsController.Instance.RefreshCurrentDetailsUI(achievement);
                }
                else if (stat is Alchemies alchemy)
                {
                    await UserAlchemiesService.Create().UpdateAlchemyStarAsync(alchemy);
                    UserAlchemiesController.Instance.RefreshCurrentDetailsUI(alchemy);
                }
                else if (stat is Architectures architecture)
                {
                    await UserArchitecturesService.Create().UpdateArchitectureStarAsync(architecture);
                    UserArchitecturesController.Instance.RefreshCurrentDetailsUI(architecture);
                }
                else if (stat is Artifacts artifact)
                {
                    await UserArtifactsService.Create().UpdateArtifactStarAsync(artifact);
                    UserArtifactsController.Instance.RefreshCurrentDetailsUI(artifact);
                }
                else if (stat is Artworks artwork)
                {
                    await UserArtworksService.Create().UpdateArtworkStarAsync(artwork);
                    UserArtworksController.Instance.RefreshCurrentDetailsUI(artwork);
                }
                else if (stat is Avatars avatar)
                {
                    await UserAvatarsService.Create().UpdateAvatarStarAsync(avatar);
                    UserAvatarsController.Instance.RefreshCurrentDetailsUI(avatar);
                }
                else if (stat is Badges badge)
                {
                    await UserBadgesService.Create().UpdateBadgeStarAsync(badge);
                    UserBadgesController.Instance.RefreshCurrentDetailsUI(badge);
                }
                else if (stat is Beverages beverage)
                {
                    await UserBeveragesService.Create().UpdateBeverageStarAsync(beverage);
                    UserBeveragesController.Instance.RefreshCurrentDetailsUI(beverage);
                }
                else if (stat is Books book)
                {
                    await UserBooksService.Create().UpdateBookStarAsync(book);
                    UserBooksController.Instance.RefreshCurrentDetailsUI(book);
                }
                else if (stat is Borders border)
                {
                    await UserBordersService.Create().UpdateBorderStarAsync(border);
                    UserBordersController.Instance.RefreshCurrentDetailsUI(border);
                }
                else if (stat is Buildings building)
                {
                    await UserBuildingsService.Create().UpdateBuildingStarAsync(building);
                    UserBuildingsController.Instance.RefreshCurrentDetailsUI(building);
                }
                else if (stat is CardAdmirals admiral)
                {
                    await UserCardAdmiralsService.Create().UpdateCardAdmiralStarAsync(admiral);
                    UserCardAdmiralsController.Instance.RefreshCurrentDetailsUI(admiral);
                }
                else if (stat is CardCaptains captain)
                {
                    await UserCardCaptainsService.Create().UpdateCardCaptainStarAsync(captain);
                    UserCardCaptainsController.Instance.RefreshCurrentDetailsUI(captain);
                }
                else if (stat is CardColonels colonel)
                {
                    await UserCardColonelsService.Create().UpdateCardColonelStarAsync(colonel);
                    UserCardColonelsController.Instance.RefreshCurrentDetailsUI(colonel);
                }
                else if (stat is CardGenerals general)
                {
                    await UserCardGeneralsService.Create().UpdateCardGeneralStarAsync(general);
                    UserCardGeneralsController.Instance.RefreshCurrentDetailsUI(general);
                }
                else if (stat is CardHeroes hero)
                {
                    await UserCardHeroesService.Create().UpdateCardHeroStarAsync(hero);
                    UserCardHeroesController.Instance.RefreshCurrentDetailsUI(hero);
                }
                else if (stat is CardLives cardLife)
                {
                    await UserCardLivesService.Create().UpdateCardLifeStarAsync(cardLife);
                    UserCardLivesController.Instance.RefreshCurrentDetailsUI(cardLife);
                }
                else if (stat is CardMilitaries military)
                {
                    await UserCardMilitariesService.Create().UpdateCardMilitaryStarAsync(military);
                    UserCardMilitariesController.Instance.RefreshCurrentDetailsUI(military);
                }
                else if (stat is CardMonsters monster)
                {
                    await UserCardMonstersService.Create().UpdateCardMonsterStarAsync(monster);
                    UserCardMonstersController.Instance.RefreshCurrentDetailsUI(monster);
                }
                else if (stat is CardSoldiers soldier)
                {
                    await UserCardSoldiersService.Create().UpdateCardSoldierStarAsync(soldier);
                    UserCardSoldiersController.Instance.RefreshCurrentDetailsUI(soldier);
                }
                else if (stat is CardSpells spell)
                {
                    await UserCardSpellsService.Create().UpdateCardSpellStarAsync(spell);
                    UserCardSpellsController.Instance.RefreshCurrentDetailsUI(spell);
                }
                else if (stat is CollaborationEquipments collabEquip)
                {
                    await UserCollaborationEquipmentsService.Create().UpdateCollaborationEquipmentStarAsync(collabEquip);
                    UserCollaborationEquipmentsController.Instance.RefreshCurrentDetailsUI(collabEquip);
                }
                else if (stat is Collaborations collab)
                {
                    await UserCollaborationsService.Create().UpdateCollaborationStarAsync(collab);
                    UserCollaborationsController.Instance.RefreshCurrentDetailsUI(collab);
                }
                else if (stat is Cores core)
                {
                    await UserCoresService.Create().UpdateCoreStarAsync(core);
                    UserCoresController.Instance.RefreshCurrentDetailsUI(core);
                }
                else if (stat is Emojis emoji)
                {
                    await UserEmojisService.Create().UpdateEmojiStarAsync(emoji);
                    UserEmojisController.Instance.RefreshCurrentDetailsUI(emoji);
                }
                else if (stat is Equipments equipment)
                {
                    await UserEquipmentsService.Create().UpdateEquipmentStarAsync(equipment);
                    UserEquipmentsController.Instance.RefreshCurrentDetailsUI(equipment);
                }
                else if (stat is Fashions fashion)
                {
                    await UserFashionsService.Create().UpdateFashionStarAsync(fashion);
                    UserFashionsController.Instance.RefreshCurrentDetailsUI(fashion);
                }
                else if (stat is Foods food)
                {
                    await UserFoodsService.Create().UpdateFoodStarAsync(food);
                    UserFoodsController.Instance.RefreshCurrentDetailsUI(food);
                }
                else if (stat is Forges forge)
                {
                    await UserForgesService.Create().UpdateForgeStarAsync(forge);
                    UserForgesController.Instance.RefreshCurrentDetailsUI(forge);
                }
                else if (stat is Furnitures furniture)
                {
                    await UserFurnituresService.Create().UpdateFurnitureStarAsync(furniture);
                    UserFurnituresController.Instance.RefreshCurrentDetailsUI(furniture);
                }
                else if (stat is MagicFormationCircles circle)
                {
                    await UserMagicFormationCirclesService.Create().UpdateMagicFormationCircleStarAsync(circle);
                    UserMagicFormationCirclesController.Instance.RefreshCurrentDetailsUI(circle);
                }
                else if (stat is MechaBeasts mechaBeast)
                {
                    await UserMechaBeastsService.Create().UpdateMechaBeastStarAsync(mechaBeast);
                    UserMechaBeastsController.Instance.RefreshCurrentDetailsUI(mechaBeast);
                }
                else if (stat is Medals medal)
                {
                    await UserMedalsService.Create().UpdateMedalStarAsync(medal);
                    UserMedalsController.Instance.RefreshCurrentDetailsUI(medal);
                }
                else if (stat is Pets pet)
                {
                    await UserPetsService.Create().UpdatePetStarAsync(pet);
                    UserPetsController.Instance.RefreshCurrentDetailsUI(pet);
                }
                else if (stat is Plants plant)
                {
                    await UserPlantsService.Create().UpdatePlantStarAsync(plant);
                    UserPlantsController.Instance.RefreshCurrentDetailsUI(plant);
                }
                else if (stat is Puppets puppet)
                {
                    await UserPuppetsService.Create().UpdatePuppetStarAsync(puppet);
                    UserPuppetsController.Instance.RefreshCurrentDetailsUI(puppet);
                }
                else if (stat is Relics relic)
                {
                    await UserRelicsService.Create().UpdateRelicStarAsync(relic);
                    UserRelicsController.Instance.RefreshCurrentDetailsUI(relic);
                }
                else if (stat is Robots robot)
                {
                    await UserRobotsService.Create().UpdateRobotStarAsync(robot);
                    UserRobotsController.Instance.RefreshCurrentDetailsUI(robot);
                }
                else if (stat is Runes rune)
                {
                    await UserRunesService.Create().UpdateRuneStarAsync(rune);
                    UserRunesController.Instance.RefreshCurrentDetailsUI(rune);
                }
                else if (stat is Skills skill)
                {
                    await UserSkillsService.Create().UpdateSkillStarAsync(skill);
                    UserSkillsController.Instance.RefreshCurrentDetailsUI(skill);
                }
                else if (stat is SpiritBeasts spiritBeast)
                {
                    await UserSpiritBeastsService.Create().UpdateSpiritBeastStarAsync(spiritBeast);
                    UserSpiritBeastsController.Instance.RefreshCurrentDetailsUI(spiritBeast);
                }
                else if (stat is SpiritCards spiritCard)
                {
                    await UserSpiritCardsService.Create().UpdateSpiritCardStarAsync(spiritCard);
                    UserSpiritCardsController.Instance.RefreshCurrentDetailsUI(spiritCard);
                }
                else if (stat is Symbols symbol)
                {
                    await UserSymbolsService.Create().UpdateSymbolStarAsync(symbol);
                    UserSymbolsController.Instance.RefreshCurrentDetailsUI(symbol);
                }
                else if (stat is Talismans talisman)
                {
                    await UserTalismansService.Create().UpdateTalismanStarAsync(talisman);
                    UserTalismansController.Instance.RefreshCurrentDetailsUI(talisman);
                }
                else if (stat is Technologies technology)
                {
                    await UserTechnologiesService.Create().UpdateTechnologyStarAsync(technology);
                    UserTechnologiesController.Instance.RefreshCurrentDetailsUI(technology);
                }
                else if (stat is Titles title)
                {
                    await UserTitlesService.Create().UpdateTitleStarAsync(title);
                    UserTitlesController.Instance.RefreshCurrentDetailsUI(title);
                }
                else if (stat is Vehicles vehicle)
                {
                    await UserVehiclesService.Create().UpdateVehicleStarAsync(vehicle);
                    UserVehiclesController.Instance.RefreshCurrentDetailsUI(vehicle);
                }
                else if (stat is Weapons weapon)
                {
                    await UserWeaponsService.Create().UpdateWeaponStarAsync(weapon);
                    UserWeaponsController.Instance.RefreshCurrentDetailsUI(weapon);
                }
                else if (stat is Outfits outfit)
                {
                    await UserOutfitsService.Create().UpdateOutfitStarAsync(outfit);
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