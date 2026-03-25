using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using System.Reflection;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
public class HomeManager : MonoBehaviour
{
    public static HomeManager Instance { get; private set; }
    private GameObject HomePanelPrefab;
    private Transform MainPanel;
    private string search;
    private string type;
    private string rare;
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
        search ="";
        type = AppConstants.Type.ALL;
        rare = AppConstants.Rare.ALL;
        HomePanelPrefab = UIManager.Instance.Get("HomePanelPrefab");
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
    }
    public async Task CreateHomePanelAsync()
    {
        GameObject currentObject = Instantiate(HomePanelPrefab, MainPanel);

        Transform achivementTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/AchievementPanel");
        TextMeshProUGUI achievementTitleText = achivementTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI achievementQuantityText = achivementTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userAchievementCount = await UserAchievementsService.Create().GetUserAchievementsCountAsync(User.CurrentUserId, search, rare);
        achievementTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ACHIEVEMENT);
        achievementQuantityText.text = userAchievementCount.ToString();
        
        Transform alchemyTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/AlchemyPanel");
        TextMeshProUGUI alchemyTitleText = alchemyTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI alchemyQuantityText = alchemyTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userAlchemyCount = await UserAlchemiesService.Create().GetUserAlchemiesCountAsync(User.CurrentUserId, search, type, rare);
        alchemyTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ALCHEMY);
        alchemyQuantityText.text = userAlchemyCount.ToString();

        Transform architectureTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/ArchitecturePanel");
        TextMeshProUGUI architectureTitleText = architectureTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI architectureQuantityText = architectureTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userArchitectureCount = await UserArchitecturesService.Create().GetUserArchitecturesCountAsync(User.CurrentUserId, search, rare);
        architectureTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ARCHITECTURE);
        architectureQuantityText.text = userArchitectureCount.ToString();

        Transform artworkTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/ArtworkPanel");
        TextMeshProUGUI artworkTitleText = artworkTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI artworkQuantityText = artworkTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userArtworkCount = await UserArtworksService.Create().GetUserArtworksCountAsync(User.CurrentUserId, search, type, rare);
        artworkTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ARTWORK);
        artworkQuantityText.text = userArtworkCount.ToString();

        Transform avatarTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/AvatarPanel");
        TextMeshProUGUI avatarTitleText = avatarTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI avatarQuantityText = avatarTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userAvatarCount = await UserAvatarsService.Create().GetUserAvatarsCountAsync(User.CurrentUserId, search, rare);
        avatarTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.AVATAR);
        avatarQuantityText.text = userAvatarCount.ToString();

        Transform badgeTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/BadgePanel");
        TextMeshProUGUI badgeTitleText = badgeTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI badgeQuantityText = badgeTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userBadgeCount = await UserBadgesService.Create().GetUserBadgesCountAsync(User.CurrentUserId, search, rare);
        badgeTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BADGE);
        badgeQuantityText.text = userBadgeCount.ToString();

        Transform beverageTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/BeveragePanel");
        TextMeshProUGUI beverageTitleText = beverageTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI beverageQuantityText = beverageTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userBeverageCount = await UserBeveragesService.Create().GetUserBeveragesCountAsync(User.CurrentUserId, search, rare);
        beverageTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BEVERAGE);
        beverageQuantityText.text = userBeverageCount.ToString();

        Transform bookTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/BookPanel");
        TextMeshProUGUI bookTitleText = bookTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI bookQuantityText = bookTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userBookCount = await UserBooksService.Create().GetUserBooksCountAsync(User.CurrentUserId, search, type, rare);
        bookTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BOOK);
        bookQuantityText.text = userBookCount.ToString();

        Transform borderTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/BorderPanel");
        TextMeshProUGUI borderTitleText = borderTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI borderQuantityText = borderTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userBorderCount = await UserBordersService.Create().GetUserBordersCountAsync(User.CurrentUserId, search, rare);
        borderTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BORDER);
        borderQuantityText.text = userBorderCount.ToString();

        Transform buildingTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/BuildingPanel");
        TextMeshProUGUI buildingTitleText = buildingTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI buildingQuantityText = buildingTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userBuildingCount = await UserBuildingsService.Create().GetUserBuildingsCountAsync(User.CurrentUserId, search, type, rare);
        buildingTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.BUILDING);
        buildingQuantityText.text = userBuildingCount.ToString();

        Transform cardAdmiralTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CardAdmiralPanel");
        TextMeshProUGUI cardAdmiralTitleText = cardAdmiralTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cardAdmiralQuantityText = cardAdmiralTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCardAdmiralCount = await UserCardAdmiralsService.Create().GetUserCardAdmiralsCountAsync(User.CurrentUserId, search, type, rare);
        cardAdmiralTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_ADMIRAL);
        cardAdmiralQuantityText.text = userCardAdmiralCount.ToString();

        Transform cardCaptainTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CardCaptainPanel");
        TextMeshProUGUI cardCaptainTitleText = cardCaptainTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cardCaptainQuantityText = cardCaptainTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCardCaptainCount = await UserCardCaptainsService.Create().GetUserCardCaptainsCountAsync(User.CurrentUserId, search, type, rare);
        cardCaptainTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_CAPTAIN);
        cardCaptainQuantityText.text = userCardCaptainCount.ToString();

        Transform cardColonelTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CardColonelPanel");
        TextMeshProUGUI cardColonelTitleText = cardColonelTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cardColonelQuantityText = cardColonelTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCardColonelCount = await UserCardColonelsService.Create().GetUserCardColonelsCountAsync(User.CurrentUserId, search, type, rare);
        cardColonelTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_COLONEL);
        cardColonelQuantityText.text = userCardColonelCount.ToString();

        Transform cardGeneralTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CardGeneralPanel");
        TextMeshProUGUI cardGeneralTitleText = cardGeneralTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cardGeneralQuantityText = cardGeneralTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCardGeneralCount = await UserCardGeneralsService.Create().GetUserCardGeneralsCountAsync(User.CurrentUserId, search, type, rare);
        cardGeneralTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_GENERAL);
        cardGeneralQuantityText.text = userCardGeneralCount.ToString();

        Transform cardHeroTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CardHeroPanel");
        TextMeshProUGUI cardHeroTitleText = cardHeroTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cardHeroQuantityText = cardHeroTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCardHeroCount = await UserCardHeroesService.Create().GetUserCardHeroesCountAsync(User.CurrentUserId, search, type, rare);
        cardHeroTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_HERO);
        cardHeroQuantityText.text = userCardHeroCount.ToString();
        
        Transform cardLifeTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CardLifePanel");
        TextMeshProUGUI cardLifeTitleText = cardLifeTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cardLifeQuantityText = cardLifeTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCardLifeCount = await UserCardLivesService.Create().GetUserCardLivesCountAsync(User.CurrentUserId, search, type, rare);
        cardLifeTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_LIFE);
        cardLifeQuantityText.text = userCardLifeCount.ToString();

        Transform cardMilitaryTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CardMilitaryPanel");
        TextMeshProUGUI cardMilitaryTitleText = cardMilitaryTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cardMilitaryQuantityText = cardMilitaryTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCardMilitaryCount = await UserCardMilitariesService.Create().GetUserCardMilitariesCountAsync(User.CurrentUserId, search, type, rare);
        cardMilitaryTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MILITARY);
        cardMilitaryQuantityText.text = userCardMilitaryCount.ToString();

        Transform cardMonsterTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CardMonsterPanel");
        TextMeshProUGUI cardMonsterTitleText = cardMonsterTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cardMonsterQuantityText = cardMonsterTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCardMonsterCount = await UserCardMonstersService.Create().GetUserCardMonstersCountAsync(User.CurrentUserId, search, type, rare);
        cardMonsterTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_MONSTER);
        cardMonsterQuantityText.text = userCardMonsterCount.ToString();

        Transform cardSpellTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CardSpellPanel");
        TextMeshProUGUI cardSpellTitleText = cardSpellTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cardSpellQuantityText = cardSpellTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCardSpellCount = await UserCardSpellsService.Create().GetUserCardSpellsCountAsync(User.CurrentUserId, search, type, rare);
        cardSpellTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD_SPELL);
        cardSpellQuantityText.text = userCardSpellCount.ToString();

        Transform cardTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CardPanel");
        TextMeshProUGUI cardTitleText = cardTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI cardQuantityText = cardTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCardCount = await UserCardsService.Create().GetUserCardsCountAsync(User.CurrentUserId, search, rare);
        cardTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CARD);
        cardQuantityText.text = userCardCount.ToString();

        Transform collaborationEquipmentTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CollaborationEquipmentPanel");
        TextMeshProUGUI collaborationEquipmentTitleText = collaborationEquipmentTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI collaborationEquipmentQuantityText = collaborationEquipmentTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCollaborationEquipmentCount = await UserCollaborationEquipmentsService.Create().GetUserCollaborationEquipmentsCountAsync(User.CurrentUserId, search, type, rare);
        collaborationEquipmentTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.COLLABORATION_EQUIPMENT);
        collaborationEquipmentQuantityText.text = userCollaborationEquipmentCount.ToString();

        Transform collaborationTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CollaborationPanel");
        TextMeshProUGUI collaborationTitleText = collaborationTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI collaborationQuantityText = collaborationTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCollaborationCount = await UserCollaborationsService.Create().GetUserCollaborationsCountAsync(User.CurrentUserId, search, rare);
        collaborationTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.COLLABORATION);
        collaborationQuantityText.text = userCollaborationCount.ToString();

        Transform coreTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/CorePanel");
        TextMeshProUGUI coreTitleText = coreTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI coreQuantityText = coreTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userCoreCount = await UserCoresService.Create().GetUserCoresCountAsync(User.CurrentUserId, search, rare);
        coreTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.CORE);
        coreQuantityText.text = userCoreCount.ToString();

        Transform equipmentTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/EquipmentPanel");
        TextMeshProUGUI equipmentTitleText = equipmentTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI equipmentQuantityText = equipmentTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userEquipmentCount = await UserEquipmentsService.Create().GetUserEquipmentsCountAsync(User.CurrentUserId, search, type, rare);
        equipmentTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.EQUIPMENT);
        equipmentQuantityText.text = userEquipmentCount.ToString();

        Transform fashionTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/FashionPanel");
        TextMeshProUGUI fashionTitleText = fashionTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI fashionQuantityText = fashionTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userFashionCount = await UserFashionsService.Create().GetUserFashionsCountAsync(User.CurrentUserId, search, type, rare);
        fashionTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.FASHION);
        fashionQuantityText.text = userFashionCount.ToString();

        Transform foodTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/FoodPanel");
        TextMeshProUGUI foodTitleText = foodTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI foodQuantityText = foodTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userFoodCount = await UserFoodsService.Create().GetUserFoodsCountAsync(User.CurrentUserId, search, rare);
        foodTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.FOOD);
        foodQuantityText.text = userFoodCount.ToString();

        Transform forgeTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/ForgePanel");
        TextMeshProUGUI forgeTitleText = forgeTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI forgeQuantityText = forgeTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userForgeCount = await UserForgesService.Create().GetUserForgesCountAsync(User.CurrentUserId, search, type, rare);
        forgeTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.FORGE);
        forgeQuantityText.text = userForgeCount.ToString();

        Transform furnitureTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/FurniturePanel");
        TextMeshProUGUI furnitureTitleText = furnitureTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI furnitureQuantityText = furnitureTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userFurnitureCount = await UserFurnituresService.Create().GetUserFurnituresCountAsync(User.CurrentUserId, search, type, rare);
        furnitureTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.FURNITURE);
        furnitureQuantityText.text = userFurnitureCount.ToString();

        Transform itemTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/ItemPanel");
        TextMeshProUGUI itemTitleText = itemTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemQuantityText = itemTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userItemCount = await UserItemsService.Create().GetUserItemsCountAsync(User.CurrentUserId, search, type);
        itemTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ITEM);
        itemQuantityText.text = userItemCount.ToString();

        Transform magicFormationCircleTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/MagicFormationCirclePanel");
        TextMeshProUGUI magicFormationCircleTitleText = magicFormationCircleTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI magicFormationCircleQuantityText = magicFormationCircleTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userMagicFormationCircleCount = await UserMagicFormationCirclesService.Create().GetUserMagicFormationCirclesCountAsync(User.CurrentUserId, search, type, rare);
        magicFormationCircleTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.MAGIC_FORMATION_CIRCLE);
        magicFormationCircleQuantityText.text = userMagicFormationCircleCount.ToString();

        Transform mechaBeastTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/MechaBeastPanel");
        TextMeshProUGUI mechaBeastTitleText = mechaBeastTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI mechaBeastQuantityText = mechaBeastTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userMechaBeastCount = await UserMagicFormationCirclesService.Create().GetUserMagicFormationCirclesCountAsync(User.CurrentUserId, search, type, rare);
        mechaBeastTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.MECHA_BEAST);
        mechaBeastQuantityText.text = userMechaBeastCount.ToString();

        Transform medalTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/MedalPanel");
        TextMeshProUGUI medalTitleText = medalTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI medalQuantityText = medalTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userMedalCount = await UserMedalsService.Create().GetUserMedalsCountAsync(User.CurrentUserId, search, rare);
        medalTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.MEDAL);
        medalQuantityText.text = userMedalCount.ToString();

        Transform petTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/PetPanel");
        TextMeshProUGUI petTitleText = petTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI petQuantityText = petTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userPetCount = await UserPetsService.Create().GetUserPetsCountAsync(User.CurrentUserId, search, type, rare);
        petTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.PET);
        petQuantityText.text = userPetCount.ToString();

        Transform plantTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/PlantPanel");
        TextMeshProUGUI plantTitleText = plantTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI plantQuantityText = plantTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userPlantCount = await UserPlantsService.Create().GetUserPlantsCountAsync(User.CurrentUserId, search, rare);
        plantTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.PLANT);
        plantQuantityText.text = userPlantCount.ToString();

        Transform puppetTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/PuppetPanel");
        TextMeshProUGUI puppetTitleText = puppetTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI puppetQuantityText = puppetTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userPuppetCount = await UserPuppetsService.Create().GetUserPuppetsCountAsync(User.CurrentUserId, search, type, rare);
        puppetTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.PUPPET);
        puppetQuantityText.text = userPuppetCount.ToString();

        Transform relicTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/RelicPanel");
        TextMeshProUGUI relicTitleText = relicTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI relicQuantityText = relicTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userRelicCount = await UserRelicsService.Create().GetUserRelicsCountAsync(User.CurrentUserId, search, type, rare);
        relicTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RELIC);
        relicQuantityText.text = userRelicCount.ToString();

        Transform robotTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/RobotPanel");
        TextMeshProUGUI robotTitleText = robotTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI robotQuantityText = robotTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userRobotCount = await UserRobotsService.Create().GetUserRobotsCountAsync(User.CurrentUserId, search, rare);
        robotTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.ROBOT);
        robotQuantityText.text = userRobotCount.ToString();

        Transform runeTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/RunePanel");
        TextMeshProUGUI runeTitleText = runeTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI runeQuantityText = runeTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userRuneCount = await UserRunesService.Create().GetUserRunesCountAsync(User.CurrentUserId, search, rare);
        runeTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.RUNE);
        runeQuantityText.text = userRuneCount.ToString();

        Transform skillTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/SkillPanel");
        TextMeshProUGUI skillTitleText = skillTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI skillQuantityText = skillTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userSkillCount = await UserSkillsService.Create().GetUserSkillsCountAsync(User.CurrentUserId, search, type, rare);
        skillTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SKILL);
        skillQuantityText.text = userSkillCount.ToString();

        Transform spiritBeastTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/SpiritBeastPanel");
        TextMeshProUGUI spiritBeastTitleText = spiritBeastTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI spiritBeastQuantityText = spiritBeastTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userSpiritBeastCount = await UserSpiritBeastsService.Create().GetUserSpiritBeastsCountAsync(User.CurrentUserId, search, rare);
        spiritBeastTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SPIRIT_BEAST);
        spiritBeastQuantityText.text = userSpiritBeastCount.ToString();

        Transform spiritCardTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/SpiritCardPanel");
        TextMeshProUGUI spiritCardTitleText = spiritCardTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI spiritCardQuantityText = spiritCardTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userSpiritCardCount = await UserSpiritCardsService.Create().GetUserSpiritCardCountAsync(User.CurrentUserId, search, type, rare);
        spiritCardTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SPIRIT_CARD);
        spiritCardQuantityText.text = userSpiritCardCount.ToString();

        Transform symbolTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/SymbolPanel");
        TextMeshProUGUI symbolTitleText = symbolTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI symbolQuantityText = symbolTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userSymbolCount = await UserSymbolsService.Create().GetUserSymbolsCountAsync(User.CurrentUserId, search, type, rare);
        symbolTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.SYMBOL);
        symbolQuantityText.text = userSymbolCount.ToString();

        Transform talismanTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/TalismanPanel");
        TextMeshProUGUI talismanTitleText = talismanTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI talismanQuantityText = talismanTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userTalismanCount = await UserTalismansService.Create().GetUserTalismansCountAsync(User.CurrentUserId, search, type, rare);
        talismanTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TALISMAN);
        talismanQuantityText.text = userTalismanCount.ToString();

        Transform technologyTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/TechnologyPanel");
        TextMeshProUGUI technologyTitleText = technologyTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI technologyQuantityText = technologyTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userTechnologyCount = await UserTechnologiesService.Create().GetUserTechnologiesCountAsync(User.CurrentUserId, search, rare);
        technologyTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TECHNOLOGY);
        technologyQuantityText.text = userTechnologyCount.ToString();

        Transform titleTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/TitlePanel");
        TextMeshProUGUI titleTitleText = titleTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI titleQuantityText = titleTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userTitleCount = await UserTitlesService.Create().GetUserTitlesCountAsync(User.CurrentUserId, search, rare);
        titleTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.TITLE);
        titleQuantityText.text = userTitleCount.ToString();

        Transform vehicleTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/VehiclePanel");
        TextMeshProUGUI vehicleTitleText = vehicleTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI vehicleQuantityText = vehicleTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userVehicleCount = await UserVehiclesService.Create().GetUserVehiclesCountAsync(User.CurrentUserId, search, type, rare);
        vehicleTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.VEHICLE);
        vehicleQuantityText.text = userVehicleCount.ToString();

        Transform weaponTransform = currentObject.transform.Find("Scroll View/Viewport/Content/DataPanel/WeaponPanel");
        TextMeshProUGUI weaponTitleText = weaponTransform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI weaponQuantityText = weaponTransform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        int userWeaponCount = await UserWeaponsService.Create().GetUserWeaponsCountAsync(User.CurrentUserId, search, rare);
        weaponTitleText.text = LocalizationManager.Get(AppDisplayConstants.MainType.WEAPON);
        weaponQuantityText.text = userWeaponCount.ToString();
    }
}