using System;
using System.Threading.Tasks;
using UnityEngine;

public class UserShopPurchaseService : IUserShopPurchaseService
{
    private readonly IUserShopPurchaseRepository _userShopPurchaseRepository;
    private readonly IAchievementsService _achievementsService;
    private readonly IAlchemiesService _alchemiesService;
    private readonly IArchitecturesService _architecturesService;
    private readonly IArtifactsService _artifactsService;
    private readonly IArtworksService _artworksService;
    private readonly IAvatarsService _avatarsService;
    private readonly IBadgesService _badgesService;
    private readonly IBeveragesService _beveragesService;
    private readonly IBooksService _booksService;
    private readonly IBordersService _bordersService;
    private readonly IBuildingsService _buildingsService;
    private readonly ICardAdmiralsService _cardAdmiralsService;
    private readonly ICardCaptainsService _cardCaptainsService;
    private readonly ICardColonelsService _cardColonelsService;
    private readonly ICardGeneralsService _cardGeneralsService;
    private readonly ICardHeroesService _cardHeroesService;
    private readonly ICardLivesService _cardLivesService;
    private readonly ICardMilitariesService _cardMilitariesService;
    private readonly ICardMonstersService _cardMonstersService;
    private readonly ICardSoldiersService _cardSoldiersService;
    private readonly ICardSpellsService _cardSpellsService;
    private readonly ICollaborationEquipmentsService _collaborationEquipmentsService;
    private readonly ICollaborationsService _collaborationsService;
    private readonly ICoresService _coresService;
    private readonly IEmojisService _emojisService;
    private readonly IEquipmentsService _equipmentsService;
    private readonly IFashionsService _fashionsService;
    private readonly IFoodsService _foodsService;
    private readonly IForgesService _forgesService;
    private readonly IFurnituresService _furnituresService;
    private readonly IMagicFormationCirclesService _magicFormationCirclesService;
    private readonly IMechaBeastsService _mechaBeastsService;
    private readonly IMedalsService _medalsService;
    private readonly IOutfitsService _outfitsService;
    private readonly IPetsService _petsService;
    private readonly IPlantsService _plantsService;
    private readonly IPuppetsService _puppetsService;
    private readonly IRelicsService _relicsService;
    private readonly IRobotsService _robotsService;
    private readonly IRunesService _runesService;
    private readonly ISkillsService _skillsService;
    private readonly ISpiritBeastsService _spiritBeastsService;
    private readonly ISpiritCardsService _spiritCardsService;
    private readonly ISymbolsService _symbolsService;
    private readonly ITalismansService _talismansService;
    private readonly ITechnologiesService _technologiesService;
    private readonly ITitlesService _titlesService;
    private readonly IVehiclesService _vehiclesService;
    private readonly IWeaponsService _weaponsService;

    private readonly IAchievementsGalleryService _achievementGalleryService;
    private readonly IAlchemiesGalleryService _alchemiesGalleryService;
    private readonly IArchitecturesGalleryService _architecturesGalleryService;
    private readonly IArtifactsGalleryService _artifactsGalleryService;
    private readonly IArtworksGalleryService _artworksGalleryService;
    private readonly IAvatarsGalleryService _avatarsGalleryService;
    private readonly IBadgesGalleryService _badgesGalleryService;
    private readonly IBeveragesGalleryService _beveragesGalleryService;
    private readonly IBooksGalleryService _booksGalleryService;
    private readonly IBordersGalleryService _bordersGalleryService;
    private readonly IBuildingsGalleryService _buildingsGalleryService;
    private readonly ICardAdmiralsGalleryService _cardAdmiralsGalleryService;
    private readonly ICardCaptainsGalleryService _cardCaptainsGalleryService;
    private readonly ICardColonelsGalleryService _cardColonelsGalleryService;
    private readonly ICardGeneralsGalleryService _cardGeneralsGalleryService;
    private readonly ICardHeroesGalleryService _cardHeroesGalleryService;
    private readonly ICardLivesGalleryService _cardLivesGalleryService;
    private readonly ICardMilitariesGalleryService _cardMilitariesGalleryService;
    private readonly ICardMonstersGalleryService _cardMonstersGalleryService;
    private readonly ICardSoldiersGalleryService _cardSoldiersGalleryService;
    private readonly ICardSpellsGalleryService _cardSpellsGalleryService;
    private readonly ICollaborationEquipmentsGalleryService _collaborationEquipmentsGalleryService;
    private readonly ICollaborationsGalleryService _collaborationsGalleryService;
    private readonly ICoresGalleryService _coresGalleryService;
    private readonly IEmojisGalleryService _emojisGalleryService;
    private readonly IEquipmentsGalleryService _equipmentsGalleryService;
    private readonly IFashionsGalleryService _fashionsGalleryService;
    private readonly IFoodsGalleryService _foodsGalleryService;
    private readonly IForgesGalleryService _forgesGalleryService;
    private readonly IFurnituresGalleryService _furnituresGalleryService;
    private readonly IMagicFormationCirclesGalleryService _magicFormationCirclesGalleryService;
    private readonly IMechaBeastsGalleryService _mechaBeastsGalleryService;
    private readonly IMedalsGalleryService _medalsGalleryService;
    private readonly IOutfitsGalleryService _outfitsGalleryService;
    private readonly IPetsGalleryService _petsGalleryService;
    private readonly IPlantsGalleryService _plantsGalleryService;
    private readonly IPuppetsGalleryService _puppetsGalleryService;
    private readonly IRelicsGalleryService _relicsGalleryService;
    private readonly IRobotsGalleryService _robotsGalleryService;
    private readonly IRunesGalleryService _runesGalleryService;
    private readonly ISkillsGalleryService _skillsGalleryService;
    private readonly ISpiritBeastsGalleryService _spiritBeastsGalleryService;
    private readonly ISpiritCardsGalleryService _spiritCardsGalleryService;
    private readonly ISymbolsGalleryService _symbolsGalleryService;
    private readonly ITalismansGalleryService _talismansGalleryService;
    private readonly ITechnologiesGalleryService _technologiesGalleryService;
    private readonly ITitlesGalleryService _titlesGalleryService;
    private readonly IVehiclesGalleryService _vehiclesGalleryService;
    private readonly IWeaponsGalleryService _weaponsGalleryService;

    private readonly IUserAchievementsService _userAchievementsService;
    private readonly IUserAlchemiesService _userAlchemiesService;
    private readonly IUserArchitecturesService _userArchitecturesService;
    private readonly IUserArtifactsService _userArtifactsService;
    private readonly IUserArtworksService _userArtworksService;
    private readonly IUserAvatarsService _userAvatarsService;
    private readonly IUserBadgesService _userBadgesService;
    private readonly IUserBeveragesService _userBeveragesService;
    private readonly IUserBooksService _userBooksService;
    private readonly IUserBordersService _userBordersService;
    private readonly IUserBuildingsService _userBuildingsService;
    private readonly IUserCardAdmiralsService _userCardAdmiralsService;
    private readonly IUserCardCaptainsService _userCardCaptainsService;
    private readonly IUserCardColonelsService _userCardColonelsService;
    private readonly IUserCardGeneralsService _userCardGeneralsService;
    private readonly IUserCardHeroesService _userCardHeroesService;
    private readonly IUserCardLivesService _userCardLivesService;
    private readonly IUserCardMilitariesService _userCardMilitariesService;
    private readonly IUserCardMonstersService _userCardMonstersService;
    private readonly IUserCardSoldiersService _userCardSoldiersService;
    private readonly IUserCardSpellsService _userCardSpellsService;
    private readonly IUserCollaborationEquipmentsService _userCollaborationEquipmentsService;
    private readonly IUserCollaborationsService _userCollaborationsService;
    private readonly IUserCoresService _userCoresService;
    private readonly IUserEmojisService _userEmojisService;
    private readonly IUserEquipmentsService _userEquipmentsService;
    private readonly IUserFashionsService _userFashionsService;
    private readonly IUserFoodsService _userFoodsService;
    private readonly IUserForgesService _userForgesService;
    private readonly IUserFurnituresService _userFurnituresService;
    private readonly IUserMagicFormationCirclesService _userMagicFormationCirclesService;
    private readonly IUserMechaBeastsService _userMechaBeastsService;
    private readonly IUserMedalsService _userMedalsService;
    private readonly IUserOutfitsService _userOutfitsService;
    private readonly IUserPetsService _userPetsService;
    private readonly IUserPlantsService _userPlantsService;
    private readonly IUserPuppetsService _userPuppetsService;
    private readonly IUserRelicsService _userRelicsService;
    private readonly IUserRobotsService _userRobotsService;
    private readonly IUserRunesService _userRunesService;
    private readonly IUserSkillsService _userSkillsService;
    private readonly IUserSpiritBeastsService _userSpiritBeastsService;
    private readonly IUserSpiritCardsService _userSpiritCardsService;
    private readonly IUserSymbolsService _userSymbolsService;
    private readonly IUserTalismansService _userTalismansService;
    private readonly IUserTechnologiesService _userTechnologiesService;
    private readonly IUserTitlesService _userTitlesService;
    private readonly IUserVehiclesService _userVehiclesService;
    private readonly IUserWeaponsService _userWeaponsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserShopPurchaseService(
        IUserShopPurchaseRepository userShopPurchaseRepository,
        IPowerManagerService powerManagerService,

        // Core Services
        IAchievementsService achievementsService,
        IAlchemiesService alchemiesService,
        IArchitecturesService architecturesService,
        IArtifactsService artifactsService,
        IArtworksService artworksService,
        IAvatarsService avatarsService,
        IBadgesService badgesService,
        IBeveragesService beveragesService,
        IBooksService booksService,
        IBordersService bordersService,
        IBuildingsService buildingsService,
        ICardAdmiralsService cardAdmiralsService,
        ICardCaptainsService cardCaptainsService,
        ICardColonelsService cardColonelsService,
        ICardGeneralsService cardGeneralsService,
        ICardHeroesService cardHeroesService,
        ICardLivesService cardLivesService,
        ICardMilitariesService cardMilitariesService,
        ICardMonstersService cardMonstersService,
        ICardSoldiersService cardSoldiersService,
        ICardSpellsService cardSpellsService,
        ICollaborationEquipmentsService collaborationEquipmentsService,
        ICollaborationsService collaborationsService,
        ICoresService coresService,
        IEmojisService emojisService,
        IEquipmentsService equipmentsService,
        IFashionsService fashionsService,
        IFoodsService foodsService,
        IForgesService forgesService,
        IFurnituresService furnituresService,
        IMagicFormationCirclesService magicFormationCirclesService,
        IMechaBeastsService mechaBeastsService,
        IMedalsService medalsService,
        IOutfitsService outfitsService,
        IPetsService petsService,
        IPlantsService plantsService,
        IPuppetsService puppetsService,
        IRelicsService relicsService,
        IRobotsService robotsService,
        IRunesService runesService,
        ISkillsService skillsService,
        ISpiritBeastsService spiritBeastsService,
        ISpiritCardsService spiritCardsService,
        ISymbolsService symbolsService,
        ITalismansService talismansService,
        ITechnologiesService technologiesService,
        ITitlesService titlesService,
        IVehiclesService vehiclesService,
        IWeaponsService weaponsService,

        // Gallery Services
        IAchievementsGalleryService achievementGalleryService,
        IAlchemiesGalleryService alchemiesGalleryService,
        IArchitecturesGalleryService architecturesGalleryService,
        IArtifactsGalleryService artifactsGalleryService,
        IArtworksGalleryService artworksGalleryService,
        IAvatarsGalleryService avatarsGalleryService,
        IBadgesGalleryService badgesGalleryService,
        IBeveragesGalleryService beveragesGalleryService,
        IBooksGalleryService booksGalleryService,
        IBordersGalleryService bordersGalleryService,
        IBuildingsGalleryService buildingsGalleryService,
        ICardAdmiralsGalleryService cardAdmiralsGalleryService,
        ICardCaptainsGalleryService cardCaptainsGalleryService,
        ICardColonelsGalleryService cardColonelsGalleryService,
        ICardGeneralsGalleryService cardGeneralsGalleryService,
        ICardHeroesGalleryService cardHeroesGalleryService,
        ICardLivesGalleryService cardLivesGalleryService,
        ICardMilitariesGalleryService cardMilitariesGalleryService,
        ICardMonstersGalleryService cardMonstersGalleryService,
        ICardSoldiersGalleryService cardSoldiersGalleryService,
        ICardSpellsGalleryService cardSpellsGalleryService,
        ICollaborationEquipmentsGalleryService collaborationEquipmentsGalleryService,
        ICollaborationsGalleryService collaborationsGalleryService,
        ICoresGalleryService coresGalleryService,
        IEmojisGalleryService emojisGalleryService,
        IEquipmentsGalleryService equipmentsGalleryService,
        IFashionsGalleryService fashionsGalleryService,
        IFoodsGalleryService foodsGalleryService,
        IForgesGalleryService forgesGalleryService,
        IFurnituresGalleryService furnituresGalleryService,
        IMagicFormationCirclesGalleryService magicFormationCirclesGalleryService,
        IMechaBeastsGalleryService mechaBeastsGalleryService,
        IMedalsGalleryService medalsGalleryService,
        IOutfitsGalleryService outfitsGalleryService,
        IPetsGalleryService petsGalleryService,
        IPlantsGalleryService plantsGalleryService,
        IPuppetsGalleryService puppetsGalleryService,
        IRelicsGalleryService relicsGalleryService,
        IRobotsGalleryService robotsGalleryService,
        IRunesGalleryService runesGalleryService,
        ISkillsGalleryService skillsGalleryService,
        ISpiritBeastsGalleryService spiritBeastsGalleryService,
        ISpiritCardsGalleryService spiritCardsGalleryService,
        ISymbolsGalleryService symbolsGalleryService,
        ITalismansGalleryService talismansGalleryService,
        ITechnologiesGalleryService technologiesGalleryService,
        ITitlesGalleryService titlesGalleryService,
        IVehiclesGalleryService vehiclesGalleryService,
        IWeaponsGalleryService weaponsGalleryService,

        // User Services
        IUserAchievementsService userAchievementsService,
        IUserAlchemiesService userAlchemiesService,
        IUserArchitecturesService userArchitecturesService,
        IUserArtifactsService userArtifactsService,
        IUserArtworksService userArtworksService,
        IUserAvatarsService userAvatarsService,
        IUserBadgesService userBadgesService,
        IUserBeveragesService userBeveragesService,
        IUserBooksService userBooksService,
        IUserBordersService userBordersService,
        IUserBuildingsService userBuildingsService,
        IUserCardAdmiralsService userCardAdmiralsService,
        IUserCardCaptainsService userCardCaptainsService,
        IUserCardColonelsService userCardColonelsService,
        IUserCardGeneralsService userCardGeneralsService,
        IUserCardHeroesService userCardHeroesService,
        IUserCardLivesService userCardLivesService,
        IUserCardMilitariesService userCardMilitariesService,
        IUserCardMonstersService userCardMonstersService,
        IUserCardSoldiersService userCardSoldiersService,
        IUserCardSpellsService userCardSpellsService,
        IUserCollaborationEquipmentsService userCollaborationEquipmentsService,
        IUserCollaborationsService userCollaborationsService,
        IUserCoresService userCoresService,
        IUserEmojisService userEmojisService,
        IUserEquipmentsService userEquipmentsService,
        IUserFashionsService userFashionsService,
        IUserFoodsService userFoodsService,
        IUserForgesService userForgesService,
        IUserFurnituresService userFurnituresService,
        IUserMagicFormationCirclesService userMagicFormationCirclesService,
        IUserMechaBeastsService userMechaBeastsService,
        IUserMedalsService userMedalsService,
        IUserOutfitsService userOutfitsService,
        IUserPetsService userPetsService,
        IUserPlantsService userPlantsService,
        IUserPuppetsService userPuppetsService,
        IUserRelicsService userRelicsService,
        IUserRobotsService userRobotsService,
        IUserRunesService userRunesService,
        IUserSkillsService userSkillsService,
        IUserSpiritBeastsService userSpiritBeastsService,
        IUserSpiritCardsService userSpiritCardsService,
        IUserSymbolsService userSymbolsService,
        IUserTalismansService userTalismansService,
        IUserTechnologiesService userTechnologiesService,
        IUserTitlesService userTitlesService,
        IUserVehiclesService userVehiclesService,
        IUserWeaponsService userWeaponsService)
    {
        _userShopPurchaseRepository = userShopPurchaseRepository;
        _powerManagerService = powerManagerService;

        // Core Services Assign
        _achievementsService = achievementsService;
        _alchemiesService = alchemiesService;
        _architecturesService = architecturesService;
        _artifactsService = artifactsService;
        _artworksService = artworksService;
        _avatarsService = avatarsService;
        _badgesService = badgesService;
        _beveragesService = beveragesService;
        _booksService = booksService;
        _bordersService = bordersService;
        _buildingsService = buildingsService;
        _cardAdmiralsService = cardAdmiralsService;
        _cardCaptainsService = cardCaptainsService;
        _cardColonelsService = cardColonelsService;
        _cardGeneralsService = cardGeneralsService;
        _cardHeroesService = cardHeroesService;
        _cardLivesService = cardLivesService;
        _cardMilitariesService = cardMilitariesService;
        _cardMonstersService = cardMonstersService;
        _cardSoldiersService = cardSoldiersService;
        _cardSpellsService = cardSpellsService;
        _collaborationEquipmentsService = collaborationEquipmentsService;
        _collaborationsService = collaborationsService;
        _coresService = coresService;
        _emojisService = emojisService;
        _equipmentsService = equipmentsService;
        _fashionsService = fashionsService;
        _foodsService = foodsService;
        _forgesService = forgesService;
        _furnituresService = furnituresService;
        _magicFormationCirclesService = magicFormationCirclesService;
        _mechaBeastsService = mechaBeastsService;
        _medalsService = medalsService;
        _outfitsService = outfitsService;
        _petsService = petsService;
        _plantsService = plantsService;
        _puppetsService = puppetsService;
        _relicsService = relicsService;
        _robotsService = robotsService;
        _runesService = runesService;
        _skillsService = skillsService;
        _spiritBeastsService = spiritBeastsService;
        _spiritCardsService = spiritCardsService;
        _symbolsService = symbolsService;
        _talismansService = talismansService;
        _technologiesService = technologiesService;
        _titlesService = titlesService;
        _vehiclesService = vehiclesService;
        _weaponsService = weaponsService;

        // Gallery Services Assign
        _achievementGalleryService = achievementGalleryService;
        _alchemiesGalleryService = alchemiesGalleryService;
        _architecturesGalleryService = architecturesGalleryService;
        _artifactsGalleryService = artifactsGalleryService;
        _artworksGalleryService = artworksGalleryService;
        _avatarsGalleryService = avatarsGalleryService;
        _badgesGalleryService = badgesGalleryService;
        _beveragesGalleryService = beveragesGalleryService;
        _booksGalleryService = booksGalleryService;
        _bordersGalleryService = bordersGalleryService;
        _buildingsGalleryService = buildingsGalleryService;
        _cardAdmiralsGalleryService = cardAdmiralsGalleryService;
        _cardCaptainsGalleryService = cardCaptainsGalleryService;
        _cardColonelsGalleryService = cardColonelsGalleryService;
        _cardGeneralsGalleryService = cardGeneralsGalleryService;
        _cardHeroesGalleryService = cardHeroesGalleryService;
        _cardLivesGalleryService = cardLivesGalleryService;
        _cardMilitariesGalleryService = cardMilitariesGalleryService;
        _cardMonstersGalleryService = cardMonstersGalleryService;
        _cardSoldiersGalleryService = cardSoldiersGalleryService;
        _cardSpellsGalleryService = cardSpellsGalleryService;
        _collaborationEquipmentsGalleryService = collaborationEquipmentsGalleryService;
        _collaborationsGalleryService = collaborationsGalleryService;
        _coresGalleryService = coresGalleryService;
        _emojisGalleryService = emojisGalleryService;
        _equipmentsGalleryService = equipmentsGalleryService;
        _fashionsGalleryService = fashionsGalleryService;
        _foodsGalleryService = foodsGalleryService;
        _forgesGalleryService = forgesGalleryService;
        _furnituresGalleryService = furnituresGalleryService;
        _magicFormationCirclesGalleryService = magicFormationCirclesGalleryService;
        _mechaBeastsGalleryService = mechaBeastsGalleryService;
        _medalsGalleryService = medalsGalleryService;
        _outfitsGalleryService = outfitsGalleryService;
        _petsGalleryService = petsGalleryService;
        _plantsGalleryService = plantsGalleryService;
        _puppetsGalleryService = puppetsGalleryService;
        _relicsGalleryService = relicsGalleryService;
        _robotsGalleryService = robotsGalleryService;
        _runesGalleryService = runesGalleryService;
        _skillsGalleryService = skillsGalleryService;
        _spiritBeastsGalleryService = spiritBeastsGalleryService;
        _spiritCardsGalleryService = spiritCardsGalleryService;
        _symbolsGalleryService = symbolsGalleryService;
        _talismansGalleryService = talismansGalleryService;
        _technologiesGalleryService = technologiesGalleryService;
        _titlesGalleryService = titlesGalleryService;
        _vehiclesGalleryService = vehiclesGalleryService;
        _weaponsGalleryService = weaponsGalleryService;

        // User Services Assign
        _userAchievementsService = userAchievementsService;
        _userAlchemiesService = userAlchemiesService;
        _userArchitecturesService = userArchitecturesService;
        _userArtifactsService = userArtifactsService;
        _userArtworksService = userArtworksService;
        _userAvatarsService = userAvatarsService;
        _userBadgesService = userBadgesService;
        _userBeveragesService = userBeveragesService;
        _userBooksService = userBooksService;
        _userBordersService = userBordersService;
        _userBuildingsService = userBuildingsService;
        _userCardAdmiralsService = userCardAdmiralsService;
        _userCardCaptainsService = userCardCaptainsService;
        _userCardColonelsService = userCardColonelsService;
        _userCardGeneralsService = userCardGeneralsService;
        _userCardHeroesService = userCardHeroesService;
        _userCardLivesService = userCardLivesService;
        _userCardMilitariesService = userCardMilitariesService;
        _userCardMonstersService = userCardMonstersService;
        _userCardSoldiersService = userCardSoldiersService;
        _userCardSpellsService = userCardSpellsService;
        _userCollaborationEquipmentsService = userCollaborationEquipmentsService;
        _userCollaborationsService = userCollaborationsService;
        _userCoresService = userCoresService;
        _userEmojisService = userEmojisService;
        _userEquipmentsService = userEquipmentsService;
        _userFashionsService = userFashionsService;
        _userFoodsService = userFoodsService;
        _userForgesService = userForgesService;
        _userFurnituresService = userFurnituresService;
        _userMagicFormationCirclesService = userMagicFormationCirclesService;
        _userMechaBeastsService = userMechaBeastsService;
        _userMedalsService = userMedalsService;
        _userOutfitsService = userOutfitsService;
        _userPetsService = userPetsService;
        _userPlantsService = userPlantsService;
        _userPuppetsService = userPuppetsService;
        _userRelicsService = userRelicsService;
        _userRobotsService = userRobotsService;
        _userRunesService = userRunesService;
        _userSkillsService = userSkillsService;
        _userSpiritBeastsService = userSpiritBeastsService;
        _userSpiritCardsService = userSpiritCardsService;
        _userSymbolsService = userSymbolsService;
        _userTalismansService = userTalismansService;
        _userTechnologiesService = userTechnologiesService;
        _userTitlesService = userTitlesService;
        _userVehiclesService = userVehiclesService;
        _userWeaponsService = userWeaponsService;
    }

    public static IUserShopPurchaseService Create() => ServiceContainer.GetService<IUserShopPurchaseService>();

    public async Task<InsertOrUpdateResult<bool>> PurchaseObjectFromShop(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        // InsertOrUpdateResult<bool> result = new InsertOrUpdateResult<bool>();.
       Debug.Log(shopDTO.ShopDetail.ObjectType);
        switch (shopDTO.ShopDetail.ObjectType)
        {
            case AppConstants.ObjectType.ACHIEVEMENTS:
                // TODO: Xử lý mua Achievement
                return await InsertOrUpdateUserAchievementAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.CARD_HEROES:
                // TODO: Xử lý mua CardHero
                return await InsertOrUpdateUserCardHeroAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.BOOKS:
                // TODO: Xử lý mua Book
                return await InsertOrUpdateUserBookAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.PETS:
                // TODO: Xử lý mua Pet
                return await InsertOrUpdateUserPetAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.CARD_CAPTAINS:
                // TODO: Xử lý mua CardCaptain
                return await InsertOrUpdateUserCardCaptainAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.COLLABORATION_EQUIPMENTS:
                // TODO: Xử lý mua CollaborationEquipment
                return await InsertOrUpdateUserCollaborationEquipmentAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.CARD_MILITARIES:
                // TODO: Xử lý mua CardMilitary
                return await InsertOrUpdateUserCardMilitaryAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.CARD_SPELLS:
                // TODO: Xử lý mua CardSpell
                return await InsertOrUpdateUserCardSpellAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.COLLABORATIONS:
                // TODO: Xử lý mua Collaboration
                return await InsertOrUpdateUserCollaborationAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.CARD_MONSTERS:
                // TODO: Xử lý mua CardMonster
                return await InsertOrUpdateUserCardMonsterAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.EQUIPMENTS:
                // TODO: Xử lý mua Equipment
                return await InsertOrUpdateUserEquipmentAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.MEDALS:
                // TODO: Xử lý mua Medal
                return await InsertOrUpdateUserMedalAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.SKILLS:
                // TODO: Xử lý mua Skill
                return await InsertOrUpdateUserSkillAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.SYMBOLS:
                // TODO: Xử lý mua Symbol
                return await InsertOrUpdateUserSymbolAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.TITLES:
                // TODO: Xử lý mua Title
                return await InsertOrUpdateUserTitleAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.MAGIC_FORMATION_CIRCLES:
                // TODO: Xử lý mua MagicFormationCircle
                return await InsertOrUpdateUserMagicFormationCircleAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.RELICS:
                // TODO: Xử lý mua Relic
                return await InsertOrUpdateUserRelicAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.CARD_COLONELS:
                // TODO: Xử lý mua CardColonel
                return await InsertOrUpdateUserCardColonelAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.CARD_GENERALS:
                // TODO: Xử lý mua CardGeneral
                return await InsertOrUpdateUserCardGeneralAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.CARD_ADMIRALS:
                // TODO: Xử lý mua CardAdmiral
                return await InsertOrUpdateUserCardAdmiralAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.CARD_SOLDIERS:
                // TODO: Xử lý mua CardSoldier
                return await InsertOrUpdateUserCardSoldierAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.BORDERS:
                // TODO: Xử lý mua Border
                return await InsertOrUpdateUserBorderAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.TALISMANS:
                // TODO: Xử lý mua Talisman
                return await InsertOrUpdateUserTalismanAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.PUPPETS:
                // TODO: Xử lý mua Puppet
                return await InsertOrUpdateUserPuppetAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.ALCHEMIES:
                // TODO: Xử lý mua Alchemy
                return await InsertOrUpdateUserAlchemyAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.FORGES:
                // TODO: Xử lý mua Forge
                return await InsertOrUpdateUserForgeAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.CARD_LIVES:
                // TODO: Xử lý mua CardLife
                return await InsertOrUpdateUserCardLifeAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.ARTWORKS:
                // TODO: Xử lý mua Artwork
                return await InsertOrUpdateUserArtworkAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.SPIRIT_BEASTS:
                // TODO: Xử lý mua SpiritBeast
                return await InsertOrUpdateUserSpiritBeastAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.AVATARS:
                // TODO: Xử lý mua Avatar
                return await InsertOrUpdateUserAvatarAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.SPIRIT_CARDS:
                // TODO: Xử lý mua SpiritCard
                return await InsertOrUpdateUserSpiritCardAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.ARTIFACTS:
                // TODO: Xử lý mua Artifact
                return await InsertOrUpdateUserArtifactAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.ARCHITECTURES:
                // TODO: Xử lý mua Architecture
                return await InsertOrUpdateUserArchitectureAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.TECHNOLOGIES:
                // TODO: Xử lý mua Technology
                return await InsertOrUpdateUserTechnologyAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.VEHICLES:
                // TODO: Xử lý mua Vehicle
                return await InsertOrUpdateUserVehicleAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.CORES:
                // TODO: Xử lý mua Core
                return await InsertOrUpdateUserCoreAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.WEAPONS:
                // TODO: Xử lý mua Weapon
                return await InsertOrUpdateUserWeaponAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.ROBOTS:
                // TODO: Xử lý mua Robot
                return await InsertOrUpdateUserRobotAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.BADGES:
                // TODO: Xử lý mua Badge
                return await InsertOrUpdateUserBadgeAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.MECHA_BEASTS:
                // TODO: Xử lý mua MechaBeast
                return await InsertOrUpdateUserMechaBeastAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.RUNES:
                // TODO: Xử lý mua Rune
                return await InsertOrUpdateUserRuneAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.FURNITURES:
                // TODO: Xử lý mua Furniture
                return await InsertOrUpdateUserFurnitureAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.FOODS:
                // TODO: Xử lý mua Food
                return await InsertOrUpdateUserFoodAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.BEVERAGES:
                // TODO: Xử lý mua Beverage
                return await InsertOrUpdateUserBeverageAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.BUILDINGS:
                // TODO: Xử lý mua Building
                return await InsertOrUpdateUserBuildingAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.PLANTS:
                // TODO: Xử lý mua Plant
                return await InsertOrUpdateUserPlantAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.FASHIONS:
                // TODO: Xử lý mua Fashion
                return await InsertOrUpdateUserFashionAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.EMOJIS:
                // TODO: Xử lý mua Emoji
                return await InsertOrUpdateUserEmojiAsync(userId, shopDTO, purchaseCount);

            case AppConstants.ObjectType.OUTFITS:
                // TODO: Xử lý mua Outfit
                return await InsertOrUpdateUserOutfitAsync(userId, shopDTO, purchaseCount);

            default:
                return InsertOrUpdateResult<bool>.Inserted(false);
        }
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardHeroAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {

        // var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        // Robots oldRobot = oldRobotTask.Result;
        // Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCardHeroAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _cardHeroesGalleryService.InsertCardHeroGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        // var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(newRobotTask, newUserRobotTask);

        // PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        // PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        // PowerManager totalDelta = new PowerManager();
        // if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        // if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        // if (totalDelta.HasAnyPositiveStat())
        // {
        //     PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        //     await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        // }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAchievementAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldAchievementTask = _achievementsService.SumPowerAchievementsPercentAsync(userId);
        var oldUserAchievementTask = _userAchievementsService.SumPowerUserAchievementsAsync(userId);

        await Task.WhenAll(oldAchievementTask, oldUserAchievementTask);

        Achievements oldAchievement = oldAchievementTask.Result;
        Achievements oldUserAchievement = oldUserAchievementTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserAchievementAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _achievementGalleryService.InsertAchievementGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newAchievementTask = _achievementsService.SumPowerAchievementsPercentAsync(userId);
        var newUserAchievementTask = _userAchievementsService.SumPowerUserAchievementsAsync(userId);

        await Task.WhenAll(newAchievementTask, newUserAchievementTask);

        PowerManager deltaPower = (PowerManager)newAchievementTask.Result - (PowerManager)oldAchievement;
        PowerManager deltaUserPower = (PowerManager)newUserAchievementTask.Result - (PowerManager)oldUserAchievement;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBookAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        // var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        // Robots oldRobot = oldRobotTask.Result;
        // Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserBookAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _booksGalleryService.InsertBookGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        // var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(newRobotTask, newUserRobotTask);

        // PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        // PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        // PowerManager totalDelta = new PowerManager();
        // if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        // if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        // if (totalDelta.HasAnyPositiveStat())
        // {
        //     PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        //     await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        // }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPetAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        // var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        // Robots oldRobot = oldRobotTask.Result;
        // Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserPetAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _petsGalleryService.InsertPetGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        // var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(newRobotTask, newUserRobotTask);

        // PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        // PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        // PowerManager totalDelta = new PowerManager();
        // if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        // if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        // if (totalDelta.HasAnyPositiveStat())
        // {
        //     PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        //     await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        // }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardCaptainAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        // var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        // Robots oldRobot = oldRobotTask.Result;
        // Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCardCaptainAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _cardCaptainsGalleryService.InsertCardCaptainGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        // var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(newRobotTask, newUserRobotTask);

        // PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        // PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        // PowerManager totalDelta = new PowerManager();
        // if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        // if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        // if (totalDelta.HasAnyPositiveStat())
        // {
        //     PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        //     await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        // }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCollaborationEquipmentAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldCollaborationEquipmentTask = _collaborationEquipmentsService.SumPowerCollaborationEquipmentsPercentAsync(userId);
        var oldUserCollaborationEquipmentTask = _userCollaborationEquipmentsService.SumPowerUserCollaborationEquipmentsAsync(userId);

        await Task.WhenAll(oldCollaborationEquipmentTask, oldUserCollaborationEquipmentTask);

        CollaborationEquipments oldCollaborationEquipment = oldCollaborationEquipmentTask.Result;
        CollaborationEquipments oldUserCollaborationEquipment = oldUserCollaborationEquipmentTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCollaborationEquipmentAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _collaborationEquipmentsGalleryService.InsertCollaborationEquipmentGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newCollaborationEquipmentTask = _collaborationEquipmentsService.SumPowerCollaborationEquipmentsPercentAsync(userId);
        var newUserCollaborationEquipmentTask = _userCollaborationEquipmentsService.SumPowerUserCollaborationEquipmentsAsync(userId);

        await Task.WhenAll(newCollaborationEquipmentTask, newUserCollaborationEquipmentTask);

        PowerManager deltaPower = (PowerManager)newCollaborationEquipmentTask.Result - (PowerManager)oldCollaborationEquipment;
        PowerManager deltaUserPower = (PowerManager)newUserCollaborationEquipmentTask.Result - (PowerManager)oldUserCollaborationEquipment;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardMilitaryAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        // var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        // Robots oldRobot = oldRobotTask.Result;
        // Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCardMilitaryAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _cardMilitariesGalleryService.InsertCardMilitaryGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        // var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(newRobotTask, newUserRobotTask);

        // PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        // PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        // PowerManager totalDelta = new PowerManager();
        // if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        // if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        // if (totalDelta.HasAnyPositiveStat())
        // {
        //     PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        //     await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        // }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardSpellAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        // var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        // Robots oldRobot = oldRobotTask.Result;
        // Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCardSpellAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _cardSpellsGalleryService.InsertCardSpellGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        // var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(newRobotTask, newUserRobotTask);

        // PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        // PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        // PowerManager totalDelta = new PowerManager();
        // if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        // if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        // if (totalDelta.HasAnyPositiveStat())
        // {
        //     PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        //     await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        // }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCollaborationAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldCollaborationTask = _collaborationsService.SumPowerCollaborationsPercentAsync(userId);
        var oldUserCollaborationTask = _userCollaborationsService.SumPowerUserCollaborationsAsync(userId);

        await Task.WhenAll(oldCollaborationTask, oldUserCollaborationTask);

        Collaborations oldCollaboration = oldCollaborationTask.Result;
        Collaborations oldUserCollaboration = oldUserCollaborationTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCollaborationAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _collaborationsGalleryService.InsertCollaborationGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newCollaborationTask = _collaborationsService.SumPowerCollaborationsPercentAsync(userId);
        var newUserCollaborationTask = _userCollaborationsService.SumPowerUserCollaborationsAsync(userId);

        await Task.WhenAll(newCollaborationTask, newUserCollaborationTask);

        PowerManager deltaPower = (PowerManager)newCollaborationTask.Result - (PowerManager)oldCollaboration;
        PowerManager deltaUserPower = (PowerManager)newUserCollaborationTask.Result - (PowerManager)oldUserCollaboration;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardMonsterAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        // var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        // Robots oldRobot = oldRobotTask.Result;
        // Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCardMonsterAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _cardMonstersGalleryService.InsertCardMonsterGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        // var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(newRobotTask, newUserRobotTask);

        // PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        // PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        // PowerManager totalDelta = new PowerManager();
        // if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        // if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        // if (totalDelta.HasAnyPositiveStat())
        // {
        //     PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        //     await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        // }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEquipmentAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        // var oldEquipmentTask = _equipmentsService.SumPowerEquipmentsPercentAsync(userId);
        // var oldUserEquipmentTask = _userEquipmentsService.SumPowerUserEquipmentsAsync(userId);

        // await Task.WhenAll(oldEquipmentTask, oldUserEquipmentTask);

        // Equipments oldEquipment = oldEquipmentTask.Result;
        // Equipments oldUserEquipment = oldUserEquipmentTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserEquipmentAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _equipmentsGalleryService.InsertEquipmentGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        // var newEquipmentTask = _achievementsService.SumPowerEquipmentsPercentAsync(userId);
        // var newUserEquipmentTask = _userEquipmentsService.SumPowerUserEquipmentsAsync(userId);

        // await Task.WhenAll(newEquipmentTask, newUserEquipmentTask);

        // PowerManager deltaPower = (PowerManager)newEquipmentTask.Result - (PowerManager)oldEquipment;
        // PowerManager deltaUserPower = (PowerManager)newUserEquipmentTask.Result - (PowerManager)oldUserEquipment;

        // PowerManager totalDelta = new PowerManager();
        // if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        // if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        // if (totalDelta.HasAnyPositiveStat())
        // {
        //     PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        //     await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        // }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMedalAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldMedalTask = _medalsService.SumPowerMedalsPercentAsync(userId);
        var oldUserMedalTask = _userMedalsService.SumPowerUserMedalsAsync(userId);

        await Task.WhenAll(oldMedalTask, oldUserMedalTask);

        Medals oldMedal = oldMedalTask.Result;
        Medals oldUserMedal = oldUserMedalTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserMedalAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _medalsGalleryService.InsertMedalGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newMedalTask = _medalsService.SumPowerMedalsPercentAsync(userId);
        var newUserMedalTask = _userMedalsService.SumPowerUserMedalsAsync(userId);

        await Task.WhenAll(newMedalTask, newUserMedalTask);

        PowerManager deltaPower = (PowerManager)newMedalTask.Result - (PowerManager)oldMedal;
        PowerManager deltaUserPower = (PowerManager)newUserMedalTask.Result - (PowerManager)oldUserMedal;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSkillAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldSkillTask = _skillsService.SumPowerSkillsPercentAsync(userId);
        var oldUserSkillTask = _userSkillsService.SumPowerUserSkillsAsync(userId);

        await Task.WhenAll(oldSkillTask, oldUserSkillTask);

        Skills oldSkill = oldSkillTask.Result;
        Skills oldUserSkill = oldUserSkillTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserSkillAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _skillsGalleryService.InsertSkillGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newSkillTask = _skillsService.SumPowerSkillsPercentAsync(userId);
        var newUserSkillTask = _userSkillsService.SumPowerUserSkillsAsync(userId);

        await Task.WhenAll(newSkillTask, newUserSkillTask);

        PowerManager deltaPower = (PowerManager)newSkillTask.Result - (PowerManager)oldSkill;
        PowerManager deltaUserPower = (PowerManager)newUserSkillTask.Result - (PowerManager)oldUserSkill;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSymbolAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldSymbolTask = _symbolsService.SumPowerSymbolsPercentAsync(userId);
        var oldUserSymbolTask = _userSymbolsService.SumPowerUserSymbolsAsync(userId);

        await Task.WhenAll(oldSymbolTask, oldUserSymbolTask);

        Symbols oldSymbol = oldSymbolTask.Result;
        Symbols oldUserSymbol = oldUserSymbolTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserSymbolAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _symbolsGalleryService.InsertSymbolGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newSymbolTask = _symbolsService.SumPowerSymbolsPercentAsync(userId);
        var newUserSymbolTask = _userSymbolsService.SumPowerUserSymbolsAsync(userId);

        await Task.WhenAll(newSymbolTask, newUserSymbolTask);

        PowerManager deltaPower = (PowerManager)newSymbolTask.Result - (PowerManager)oldSymbol;
        PowerManager deltaUserPower = (PowerManager)newUserSymbolTask.Result - (PowerManager)oldUserSymbol;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTitleAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldTitleTask = _titlesService.SumPowerTitlesPercentAsync(userId);
        var oldUserTitleTask = _userTitlesService.SumPowerUserTitlesAsync(userId);

        await Task.WhenAll(oldTitleTask, oldUserTitleTask);

        Titles oldTitle = oldTitleTask.Result;
        Titles oldUserTitle = oldUserTitleTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserTitleAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _titlesGalleryService.InsertTitleGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newTitleTask = _titlesService.SumPowerTitlesPercentAsync(userId);
        var newUserTitleTask = _userTitlesService.SumPowerUserTitlesAsync(userId);

        await Task.WhenAll(newTitleTask, newUserTitleTask);

        PowerManager deltaPower = (PowerManager)newTitleTask.Result - (PowerManager)oldTitle;
        PowerManager deltaUserPower = (PowerManager)newUserTitleTask.Result - (PowerManager)oldUserTitle;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMagicFormationCircleAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldMagicFormationCircleTask = _magicFormationCirclesService.SumPowerMagicFormationCirclesPercentAsync(userId);
        var oldUserMagicFormationCircleTask = _userMagicFormationCirclesService.SumPowerUserMagicFormationCirclesAsync(userId);

        await Task.WhenAll(oldMagicFormationCircleTask, oldUserMagicFormationCircleTask);

        MagicFormationCircles oldMagicFormationCircle = oldMagicFormationCircleTask.Result;
        MagicFormationCircles oldUserMagicFormationCircle = oldUserMagicFormationCircleTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserMagicFormationCircleAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _magicFormationCirclesGalleryService.InsertMagicFormationCircleGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newMagicFormationCircleTask = _magicFormationCirclesService.SumPowerMagicFormationCirclesPercentAsync(userId);
        var newUserMagicFormationCircleTask = _userMagicFormationCirclesService.SumPowerUserMagicFormationCirclesAsync(userId);

        await Task.WhenAll(newMagicFormationCircleTask, newUserMagicFormationCircleTask);

        PowerManager deltaPower = (PowerManager)newMagicFormationCircleTask.Result - (PowerManager)oldMagicFormationCircle;
        PowerManager deltaUserPower = (PowerManager)newUserMagicFormationCircleTask.Result - (PowerManager)oldUserMagicFormationCircle;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRelicAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldRelicTask = _relicsService.SumPowerRelicsPercentAsync(userId);
        var oldUserRelicTask = _userRelicsService.SumPowerUserRelicsAsync(userId);

        await Task.WhenAll(oldRelicTask, oldUserRelicTask);

        Relics oldRelic = oldRelicTask.Result;
        Relics oldUserRelic = oldUserRelicTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserRelicAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _relicsGalleryService.InsertRelicGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newRelicTask = _relicsService.SumPowerRelicsPercentAsync(userId);
        var newUserRelicTask = _userRelicsService.SumPowerUserRelicsAsync(userId);

        await Task.WhenAll(newRelicTask, newUserRelicTask);

        PowerManager deltaPower = (PowerManager)newRelicTask.Result - (PowerManager)oldRelic;
        PowerManager deltaUserPower = (PowerManager)newUserRelicTask.Result - (PowerManager)oldUserRelic;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardColonelAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        // var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        // Robots oldRobot = oldRobotTask.Result;
        // Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCardColonelAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _cardColonelsGalleryService.InsertCardColonelGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        // var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(newRobotTask, newUserRobotTask);

        // PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        // PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        // PowerManager totalDelta = new PowerManager();
        // if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        // if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        // if (totalDelta.HasAnyPositiveStat())
        // {
        //     PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        //     await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        // }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardGeneralAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        // var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        // Robots oldRobot = oldRobotTask.Result;
        // Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCardGeneralAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _cardGeneralsGalleryService.InsertCardGeneralGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        // var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(newRobotTask, newUserRobotTask);

        // PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        // PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        // PowerManager totalDelta = new PowerManager();
        // if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        // if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        // if (totalDelta.HasAnyPositiveStat())
        // {
        //     PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        //     await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        // }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardAdmiralAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        // var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        // Robots oldRobot = oldRobotTask.Result;
        // Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCardAdmiralAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _cardAdmiralsGalleryService.InsertCardAdmiralGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        // var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(newRobotTask, newUserRobotTask);

        // PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        // PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        // PowerManager totalDelta = new PowerManager();
        // if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        // if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        // if (totalDelta.HasAnyPositiveStat())
        // {
        //     PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        //     await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        // }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardSoldierAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        // var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var oldUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        // Robots oldRobot = oldRobotTask.Result;
        // Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCardSoldierAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _cardSoldiersGalleryService.InsertCardSoldierGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        // var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        // var newUserRobotTask = _userRobotsRepository.SumPowerUserRobotsAsync(userId);

        // await Task.WhenAll(newRobotTask, newUserRobotTask);

        // PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        // PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        // PowerManager totalDelta = new PowerManager();
        // if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        // if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        // if (totalDelta.HasAnyPositiveStat())
        // {
        //     PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        //     await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        // }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBorderAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldBorderTask = _bordersService.SumPowerBordersPercentAsync(userId);
        var oldUserBorderTask = _userBordersService.SumPowerUserBordersAsync(userId);

        await Task.WhenAll(oldBorderTask, oldUserBorderTask);

        Borders oldBorder = oldBorderTask.Result;
        Borders oldUserBorder = oldUserBorderTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserBorderAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _bordersGalleryService.InsertBorderGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newBorderTask = _bordersService.SumPowerBordersPercentAsync(userId);
        var newUserBorderTask = _userBordersService.SumPowerUserBordersAsync(userId);

        await Task.WhenAll(newBorderTask, newUserBorderTask);

        PowerManager deltaPower = (PowerManager)newBorderTask.Result - (PowerManager)oldBorder;
        PowerManager deltaUserPower = (PowerManager)newUserBorderTask.Result - (PowerManager)oldUserBorder;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTalismanAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldTalismanTask = _talismansService.SumPowerTalismansPercentAsync(userId);
        var oldUserTalismanTask = _userTalismansService.SumPowerUserTalismansAsync(userId);

        await Task.WhenAll(oldTalismanTask, oldUserTalismanTask);

        Talismans oldTalisman = oldTalismanTask.Result;
        Talismans oldUserTalisman = oldUserTalismanTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserTalismanAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _talismansGalleryService.InsertTalismanGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newTalismanTask = _talismansService.SumPowerTalismansPercentAsync(userId);
        var newUserTalismanTask = _userTalismansService.SumPowerUserTalismansAsync(userId);

        await Task.WhenAll(newTalismanTask, newUserTalismanTask);

        PowerManager deltaPower = (PowerManager)newTalismanTask.Result - (PowerManager)oldTalisman;
        PowerManager deltaUserPower = (PowerManager)newUserTalismanTask.Result - (PowerManager)oldUserTalisman;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPuppetAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldPuppetTask = _puppetsService.SumPowerPuppetsPercentAsync(userId);
        var oldUserPuppetTask = _userPuppetsService.SumPowerUserPuppetsAsync(userId);

        await Task.WhenAll(oldPuppetTask, oldUserPuppetTask);

        Puppets oldPuppet = oldPuppetTask.Result;
        Puppets oldUserPuppet = oldUserPuppetTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserPuppetAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _puppetsGalleryService.InsertPuppetGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newPuppetTask = _puppetsService.SumPowerPuppetsPercentAsync(userId);
        var newUserPuppetTask = _userPuppetsService.SumPowerUserPuppetsAsync(userId);

        await Task.WhenAll(newPuppetTask, newUserPuppetTask);

        PowerManager deltaPower = (PowerManager)newPuppetTask.Result - (PowerManager)oldPuppet;
        PowerManager deltaUserPower = (PowerManager)newUserPuppetTask.Result - (PowerManager)oldUserPuppet;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAlchemyAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldAlchemyTask = _alchemiesService.SumPowerAlchemiesPercentAsync(userId);
        var oldUserAlchemyTask = _userAlchemiesService.SumPowerUserAlchemiesAsync(userId);

        await Task.WhenAll(oldAlchemyTask, oldUserAlchemyTask);

        Alchemies oldAlchemy = oldAlchemyTask.Result;
        Alchemies oldUserAlchemy = oldUserAlchemyTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserAlchemyAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _alchemiesGalleryService.InsertAlchemyGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newAlchemyTask = _alchemiesService.SumPowerAlchemiesPercentAsync(userId);
        var newUserAlchemyTask = _userAlchemiesService.SumPowerUserAlchemiesAsync(userId);

        await Task.WhenAll(newAlchemyTask, newUserAlchemyTask);

        PowerManager deltaPower = (PowerManager)newAlchemyTask.Result - (PowerManager)oldAlchemy;
        PowerManager deltaUserPower = (PowerManager)newUserAlchemyTask.Result - (PowerManager)oldUserAlchemy;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserForgeAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldForgeTask = _forgesService.SumPowerForgesPercentAsync(userId);
        var oldUserForgeTask = _userForgesService.SumPowerUserForgesAsync(userId);

        await Task.WhenAll(oldForgeTask, oldUserForgeTask);

        Forges oldForge = oldForgeTask.Result;
        Forges oldUserForge = oldUserForgeTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserForgeAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _forgesGalleryService.InsertForgeGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newForgeTask = _forgesService.SumPowerForgesPercentAsync(userId);
        var newUserForgeTask = _userForgesService.SumPowerUserForgesAsync(userId);

        await Task.WhenAll(newForgeTask, newUserForgeTask);

        PowerManager deltaPower = (PowerManager)newForgeTask.Result - (PowerManager)oldForge;
        PowerManager deltaUserPower = (PowerManager)newUserForgeTask.Result - (PowerManager)oldUserForge;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardLifeAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldCardLifeTask = _cardLivesService.SumPowerCardLivesPercentAsync(userId);
        var oldUserCardLifeTask = _userCardLivesService.SumPowerUserCardLivesAsync(userId);

        await Task.WhenAll(oldCardLifeTask, oldUserCardLifeTask);

        CardLives oldCardLife = oldCardLifeTask.Result;
        CardLives oldUserCardLife = oldUserCardLifeTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCardLifeAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _cardLivesGalleryService.InsertCardLifeGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newCardLifeTask = _cardLivesService.SumPowerCardLivesPercentAsync(userId);
        var newUserCardLifeTask = _userCardLivesService.SumPowerUserCardLivesAsync(userId);

        await Task.WhenAll(newCardLifeTask, newUserCardLifeTask);

        PowerManager deltaPower = (PowerManager)newCardLifeTask.Result - (PowerManager)oldCardLife;
        PowerManager deltaUserPower = (PowerManager)newUserCardLifeTask.Result - (PowerManager)oldUserCardLife;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtworkAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldArtworkTask = _artworksService.SumPowerArtworksPercentAsync(userId);
        var oldUserArtworkTask = _userArtworksService.SumPowerUserArtworksAsync(userId);

        await Task.WhenAll(oldArtworkTask, oldUserArtworkTask);

        Artworks oldArtwork = oldArtworkTask.Result;
        Artworks oldUserArtwork = oldUserArtworkTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserArtworkAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _artworksGalleryService.InsertArtworkGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newArtworkTask = _artworksService.SumPowerArtworksPercentAsync(userId);
        var newUserArtworkTask = _userArtworksService.SumPowerUserArtworksAsync(userId);

        await Task.WhenAll(newArtworkTask, newUserArtworkTask);

        PowerManager deltaPower = (PowerManager)newArtworkTask.Result - (PowerManager)oldArtwork;
        PowerManager deltaUserPower = (PowerManager)newUserArtworkTask.Result - (PowerManager)oldUserArtwork;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritBeastAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldSpiritBeastTask = _spiritBeastsService.SumPowerSpiritBeastsPercentAsync(userId);
        var oldUserSpiritBeastTask = _userSpiritBeastsService.SumPowerUserSpiritBeastsAsync(userId);

        await Task.WhenAll(oldSpiritBeastTask, oldUserSpiritBeastTask);

        SpiritBeasts oldSpiritBeast = oldSpiritBeastTask.Result;
        SpiritBeasts oldUserSpiritBeast = oldUserSpiritBeastTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserSpiritBeastAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _spiritBeastsGalleryService.InsertSpiritBeastGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newSpiritBeastTask = _spiritBeastsService.SumPowerSpiritBeastsPercentAsync(userId);
        var newUserSpiritBeastTask = _userSpiritBeastsService.SumPowerUserSpiritBeastsAsync(userId);

        await Task.WhenAll(newSpiritBeastTask, newUserSpiritBeastTask);

        PowerManager deltaPower = (PowerManager)newSpiritBeastTask.Result - (PowerManager)oldSpiritBeast;
        PowerManager deltaUserPower = (PowerManager)newUserSpiritBeastTask.Result - (PowerManager)oldUserSpiritBeast;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserAvatarAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldAvatarTask = _avatarsService.SumPowerAvatarsPercentAsync(userId);
        var oldUserAvatarTask = _userAvatarsService.SumPowerUserAvatarsAsync(userId);

        await Task.WhenAll(oldAvatarTask, oldUserAvatarTask);

        Avatars oldAvatar = oldAvatarTask.Result;
        Avatars oldUserAvatar = oldUserAvatarTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserAvatarAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _avatarsGalleryService.InsertAvatarGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newAvatarTask = _avatarsService.SumPowerAvatarsPercentAsync(userId);
        var newUserAvatarTask = _userAvatarsService.SumPowerUserAvatarsAsync(userId);

        await Task.WhenAll(newAvatarTask, newUserAvatarTask);

        PowerManager deltaPower = (PowerManager)newAvatarTask.Result - (PowerManager)oldAvatar;
        PowerManager deltaUserPower = (PowerManager)newUserAvatarTask.Result - (PowerManager)oldUserAvatar;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritCardAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldSpiritCardTask = _spiritCardsService.SumPowerSpiritCardsPercentAsync(userId);
        var oldUserSpiritCardTask = _userSpiritCardsService.SumPowerUserSpiritCardsAsync(userId);

        await Task.WhenAll(oldSpiritCardTask, oldUserSpiritCardTask);

        SpiritCards oldSpiritCard = oldSpiritCardTask.Result;
        SpiritCards oldUserSpiritCard = oldUserSpiritCardTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserSpiritCardAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _spiritCardsGalleryService.InsertSpiritCardGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newSpiritCardTask = _spiritCardsService.SumPowerSpiritCardsPercentAsync(userId);
        var newUserSpiritCardTask = _userSpiritCardsService.SumPowerUserSpiritCardsAsync(userId);

        await Task.WhenAll(newSpiritCardTask, newUserSpiritCardTask);

        PowerManager deltaPower = (PowerManager)newSpiritCardTask.Result - (PowerManager)oldSpiritCard;
        PowerManager deltaUserPower = (PowerManager)newUserSpiritCardTask.Result - (PowerManager)oldUserSpiritCard;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArtifactAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldArtifactTask = _artifactsService.SumPowerArtifactsPercentAsync(userId);
        var oldUserArtifactTask = _userArtifactsService.SumPowerUserArtifactsAsync(userId);

        await Task.WhenAll(oldArtifactTask, oldUserArtifactTask);

        Artifacts oldArtifact = oldArtifactTask.Result;
        Artifacts oldUserArtifact = oldUserArtifactTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserArtifactAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _artifactsGalleryService.InsertArtifactGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newArtifactTask = _artifactsService.SumPowerArtifactsPercentAsync(userId);
        var newUserArtifactTask = _userArtifactsService.SumPowerUserArtifactsAsync(userId);

        await Task.WhenAll(newArtifactTask, newUserArtifactTask);

        PowerManager deltaPower = (PowerManager)newArtifactTask.Result - (PowerManager)oldArtifact;
        PowerManager deltaUserPower = (PowerManager)newUserArtifactTask.Result - (PowerManager)oldUserArtifact;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserArchitectureAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldArchitectureTask = _architecturesService.SumPowerArchitecturesPercentAsync(userId);
        var oldUserArchitectureTask = _userArchitecturesService.SumPowerUserArchitecturesAsync(userId);

        await Task.WhenAll(oldArchitectureTask, oldUserArchitectureTask);

        Architectures oldArchitecture = oldArchitectureTask.Result;
        Architectures oldUserArchitecture = oldUserArchitectureTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserArchitectureAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _architecturesGalleryService.InsertArchitectureGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newArchitectureTask = _architecturesService.SumPowerArchitecturesPercentAsync(userId);
        var newUserArchitectureTask = _userArchitecturesService.SumPowerUserArchitecturesAsync(userId);

        await Task.WhenAll(newArchitectureTask, newUserArchitectureTask);

        PowerManager deltaPower = (PowerManager)newArchitectureTask.Result - (PowerManager)oldArchitecture;
        PowerManager deltaUserPower = (PowerManager)newUserArchitectureTask.Result - (PowerManager)oldUserArchitecture;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTechnologyAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldTechnologyTask = _technologiesService.SumPowerTechnologiesPercentAsync(userId);
        var oldUserTechnologyTask = _userTechnologiesService.SumPowerUserTechnologiesAsync(userId);

        await Task.WhenAll(oldTechnologyTask, oldUserTechnologyTask);

        Technologies oldTechnology = oldTechnologyTask.Result;
        Technologies oldUserTechnology = oldUserTechnologyTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserTechnologyAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _technologiesGalleryService.InsertTechnologyGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newTechnologyTask = _technologiesService.SumPowerTechnologiesPercentAsync(userId);
        var newUserTechnologyTask = _userTechnologiesService.SumPowerUserTechnologiesAsync(userId);

        await Task.WhenAll(newTechnologyTask, newUserTechnologyTask);

        PowerManager deltaPower = (PowerManager)newTechnologyTask.Result - (PowerManager)oldTechnology;
        PowerManager deltaUserPower = (PowerManager)newUserTechnologyTask.Result - (PowerManager)oldUserTechnology;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserVehicleAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldVehicleTask = _vehiclesService.SumPowerVehiclesPercentAsync(userId);
        var oldUserVehicleTask = _userVehiclesService.SumPowerUserVehiclesAsync(userId);

        await Task.WhenAll(oldVehicleTask, oldUserVehicleTask);

        Vehicles oldVehicle = oldVehicleTask.Result;
        Vehicles oldUserVehicle = oldUserVehicleTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserVehicleAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _vehiclesGalleryService.InsertVehicleGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newVehicleTask = _vehiclesService.SumPowerVehiclesPercentAsync(userId);
        var newUserVehicleTask = _userVehiclesService.SumPowerUserVehiclesAsync(userId);

        await Task.WhenAll(newVehicleTask, newUserVehicleTask);

        PowerManager deltaPower = (PowerManager)newVehicleTask.Result - (PowerManager)oldVehicle;
        PowerManager deltaUserPower = (PowerManager)newUserVehicleTask.Result - (PowerManager)oldUserVehicle;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCoreAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldCoreTask = _coresService.SumPowerCoresPercentAsync(userId);
        var oldUserCoreTask = _userCoresService.SumPowerUserCoresAsync(userId);

        await Task.WhenAll(oldCoreTask, oldUserCoreTask);

        Cores oldCore = oldCoreTask.Result;
        Cores oldUserCore = oldUserCoreTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserCoreAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _coresGalleryService.InsertCoreGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newCoreTask = _coresService.SumPowerCoresPercentAsync(userId);
        var newUserCoreTask = _userCoresService.SumPowerUserCoresAsync(userId);

        await Task.WhenAll(newCoreTask, newUserCoreTask);

        PowerManager deltaPower = (PowerManager)newCoreTask.Result - (PowerManager)oldCore;
        PowerManager deltaUserPower = (PowerManager)newUserCoreTask.Result - (PowerManager)oldUserCore;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserWeaponAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldWeaponTask = _weaponsService.SumPowerWeaponsPercentAsync(userId);
        var oldUserWeaponTask = _userWeaponsService.SumPowerUserWeaponsAsync(userId);

        await Task.WhenAll(oldWeaponTask, oldUserWeaponTask);

        Weapons oldWeapon = oldWeaponTask.Result;
        Weapons oldUserWeapon = oldUserWeaponTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserWeaponAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _weaponsGalleryService.InsertWeaponGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newWeaponTask = _weaponsService.SumPowerWeaponsPercentAsync(userId);
        var newUserWeaponTask = _userWeaponsService.SumPowerUserWeaponsAsync(userId);

        await Task.WhenAll(newWeaponTask, newUserWeaponTask);

        PowerManager deltaPower = (PowerManager)newWeaponTask.Result - (PowerManager)oldWeapon;
        PowerManager deltaUserPower = (PowerManager)newUserWeaponTask.Result - (PowerManager)oldUserWeapon;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRobotAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        var oldUserRobotTask = _userRobotsService.SumPowerUserRobotsAsync(userId);

        await Task.WhenAll(oldRobotTask, oldUserRobotTask);

        Robots oldRobot = oldRobotTask.Result;
        Robots oldUserRobot = oldUserRobotTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserRobotAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _robotsGalleryService.InsertRobotGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newRobotTask = _robotsService.SumPowerRobotsPercentAsync(userId);
        var newUserRobotTask = _userRobotsService.SumPowerUserRobotsAsync(userId);

        await Task.WhenAll(newRobotTask, newUserRobotTask);

        PowerManager deltaPower = (PowerManager)newRobotTask.Result - (PowerManager)oldRobot;
        PowerManager deltaUserPower = (PowerManager)newUserRobotTask.Result - (PowerManager)oldUserRobot;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBadgeAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldBadgeTask = _badgesService.SumPowerBadgesPercentAsync(userId);
        var oldUserBadgeTask = _userBadgesService.SumPowerUserBadgesAsync(userId);

        await Task.WhenAll(oldBadgeTask, oldUserBadgeTask);

        Badges oldBadge = oldBadgeTask.Result;
        Badges oldUserBadge = oldUserBadgeTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserBadgeAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _badgesGalleryService.InsertBadgeGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newBadgeTask = _badgesService.SumPowerBadgesPercentAsync(userId);
        var newUserBadgeTask = _userBadgesService.SumPowerUserBadgesAsync(userId);

        await Task.WhenAll(newBadgeTask, newUserBadgeTask);

        PowerManager deltaPower = (PowerManager)newBadgeTask.Result - (PowerManager)oldBadge;
        PowerManager deltaUserPower = (PowerManager)newUserBadgeTask.Result - (PowerManager)oldUserBadge;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMechaBeastAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldMechaBeastTask = _mechaBeastsService.SumPowerMechaBeastsPercentAsync(userId);
        var oldUserMechaBeastTask = _userMechaBeastsService.SumPowerUserMechaBeastsAsync(userId);

        await Task.WhenAll(oldMechaBeastTask, oldUserMechaBeastTask);

        MechaBeasts oldMechaBeast = oldMechaBeastTask.Result;
        MechaBeasts oldUserMechaBeast = oldUserMechaBeastTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserMechaBeastAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _mechaBeastsGalleryService.InsertMechaBeastGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newMechaBeastTask = _mechaBeastsService.SumPowerMechaBeastsPercentAsync(userId);
        var newUserMechaBeastTask = _userMechaBeastsService.SumPowerUserMechaBeastsAsync(userId);

        await Task.WhenAll(newMechaBeastTask, newUserMechaBeastTask);

        PowerManager deltaPower = (PowerManager)newMechaBeastTask.Result - (PowerManager)oldMechaBeast;
        PowerManager deltaUserPower = (PowerManager)newUserMechaBeastTask.Result - (PowerManager)oldUserMechaBeast;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRuneAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldRuneTask = _runesService.SumPowerRunesPercentAsync(userId);
        var oldUserRuneTask = _userRunesService.SumPowerUserRunesAsync(userId);

        await Task.WhenAll(oldRuneTask, oldUserRuneTask);

        Runes oldRune = oldRuneTask.Result;
        Runes oldUserRune = oldUserRuneTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserRuneAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _runesGalleryService.InsertRuneGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newRuneTask = _runesService.SumPowerRunesPercentAsync(userId);
        var newUserRuneTask = _userRunesService.SumPowerUserRunesAsync(userId);

        await Task.WhenAll(newRuneTask, newUserRuneTask);

        PowerManager deltaPower = (PowerManager)newRuneTask.Result - (PowerManager)oldRune;
        PowerManager deltaUserPower = (PowerManager)newUserRuneTask.Result - (PowerManager)oldUserRune;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFurnitureAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldFurnitureTask = _furnituresService.SumPowerFurnituresPercentAsync(userId);
        var oldUserFurnitureTask = _userFurnituresService.SumPowerUserFurnituresAsync(userId);

        await Task.WhenAll(oldFurnitureTask, oldUserFurnitureTask);

        Furnitures oldFurniture = oldFurnitureTask.Result;
        Furnitures oldUserFurniture = oldUserFurnitureTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserFurnitureAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _furnituresGalleryService.InsertFurnitureGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newFurnitureTask = _furnituresService.SumPowerFurnituresPercentAsync(userId);
        var newUserFurnitureTask = _userFurnituresService.SumPowerUserFurnituresAsync(userId);

        await Task.WhenAll(newFurnitureTask, newUserFurnitureTask);

        PowerManager deltaPower = (PowerManager)newFurnitureTask.Result - (PowerManager)oldFurniture;
        PowerManager deltaUserPower = (PowerManager)newUserFurnitureTask.Result - (PowerManager)oldUserFurniture;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFoodAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldFoodTask = _foodsService.SumPowerFoodsPercentAsync(userId);
        var oldUserFoodTask = _userFoodsService.SumPowerUserFoodsAsync(userId);

        await Task.WhenAll(oldFoodTask, oldUserFoodTask);

        Foods oldFood = oldFoodTask.Result;
        Foods oldUserFood = oldUserFoodTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserFoodAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _foodsGalleryService.InsertFoodGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newFoodTask = _foodsService.SumPowerFoodsPercentAsync(userId);
        var newUserFoodTask = _userFoodsService.SumPowerUserFoodsAsync(userId);

        await Task.WhenAll(newFoodTask, newUserFoodTask);

        PowerManager deltaPower = (PowerManager)newFoodTask.Result - (PowerManager)oldFood;
        PowerManager deltaUserPower = (PowerManager)newUserFoodTask.Result - (PowerManager)oldUserFood;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBeverageAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldBeverageTask = _beveragesService.SumPowerBeveragesPercentAsync(userId);
        var oldUserBeverageTask = _userBeveragesService.SumPowerUserBeveragesAsync(userId);

        await Task.WhenAll(oldBeverageTask, oldUserBeverageTask);

        Beverages oldBeverage = oldBeverageTask.Result;
        Beverages oldUserBeverage = oldUserBeverageTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserBeverageAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _beveragesGalleryService.InsertBeverageGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newBeverageTask = _beveragesService.SumPowerBeveragesPercentAsync(userId);
        var newUserBeverageTask = _userBeveragesService.SumPowerUserBeveragesAsync(userId);

        await Task.WhenAll(newBeverageTask, newUserBeverageTask);

        PowerManager deltaPower = (PowerManager)newBeverageTask.Result - (PowerManager)oldBeverage;
        PowerManager deltaUserPower = (PowerManager)newUserBeverageTask.Result - (PowerManager)oldUserBeverage;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBuildingAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldBuildingTask = _buildingsService.SumPowerBuildingsPercentAsync(userId);
        var oldUserBuildingTask = _userBuildingsService.SumPowerUserBuildingsAsync(userId);

        await Task.WhenAll(oldBuildingTask, oldUserBuildingTask);

        Buildings oldBuilding = oldBuildingTask.Result;
        Buildings oldUserBuilding = oldUserBuildingTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserBuildingAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _buildingsGalleryService.InsertBuildingGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newBuildingTask = _buildingsService.SumPowerBuildingsPercentAsync(userId);
        var newUserBuildingTask = _userBuildingsService.SumPowerUserBuildingsAsync(userId);

        await Task.WhenAll(newBuildingTask, newUserBuildingTask);

        PowerManager deltaPower = (PowerManager)newBuildingTask.Result - (PowerManager)oldBuilding;
        PowerManager deltaUserPower = (PowerManager)newUserBuildingTask.Result - (PowerManager)oldUserBuilding;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPlantAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldPlantTask = _plantsService.SumPowerPlantsPercentAsync(userId);
        var oldUserPlantTask = _userPlantsService.SumPowerUserPlantsAsync(userId);

        await Task.WhenAll(oldPlantTask, oldUserPlantTask);

        Plants oldPlant = oldPlantTask.Result;
        Plants oldUserPlant = oldUserPlantTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserPlantAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _plantsGalleryService.InsertPlantGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newPlantTask = _plantsService.SumPowerPlantsPercentAsync(userId);
        var newUserPlantTask = _userPlantsService.SumPowerUserPlantsAsync(userId);

        await Task.WhenAll(newPlantTask, newUserPlantTask);

        PowerManager deltaPower = (PowerManager)newPlantTask.Result - (PowerManager)oldPlant;
        PowerManager deltaUserPower = (PowerManager)newUserPlantTask.Result - (PowerManager)oldUserPlant;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFashionAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldFashionTask = _fashionsService.SumPowerFashionsPercentAsync(userId);
        var oldUserFashionTask = _userFashionsService.SumPowerUserFashionsAsync(userId);

        await Task.WhenAll(oldFashionTask, oldUserFashionTask);

        Fashions oldFashion = oldFashionTask.Result;
        Fashions oldUserFashion = oldUserFashionTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserFashionAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _fashionsGalleryService.InsertFashionGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newFashionTask = _fashionsService.SumPowerFashionsPercentAsync(userId);
        var newUserFashionTask = _userFashionsService.SumPowerUserFashionsAsync(userId);

        await Task.WhenAll(newFashionTask, newUserFashionTask);

        PowerManager deltaPower = (PowerManager)newFashionTask.Result - (PowerManager)oldFashion;
        PowerManager deltaUserPower = (PowerManager)newUserFashionTask.Result - (PowerManager)oldUserFashion;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEmojiAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldEmojiTask = _emojisService.SumPowerEmojisPercentAsync(userId);
        var oldUserEmojiTask = _userEmojisService.SumPowerUserEmojisAsync(userId);

        await Task.WhenAll(oldEmojiTask, oldUserEmojiTask);

        Emojis oldEmoji = oldEmojiTask.Result;
        Emojis oldUserEmoji = oldUserEmojiTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserEmojiAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _emojisGalleryService.InsertEmojiGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newEmojiTask = _emojisService.SumPowerEmojisPercentAsync(userId);
        var newUserEmojiTask = _userEmojisService.SumPowerUserEmojisAsync(userId);

        await Task.WhenAll(newEmojiTask, newUserEmojiTask);

        PowerManager deltaPower = (PowerManager)newEmojiTask.Result - (PowerManager)oldEmoji;
        PowerManager deltaUserPower = (PowerManager)newUserEmojiTask.Result - (PowerManager)oldUserEmoji;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserOutfitAsync(string userId, ShopDTO shopDTO, int purchaseCount)
    {
        var oldOutfitTask = _outfitsService.SumPowerOutfitsPercentAsync(userId);
        var oldUserOutfitTask = _userOutfitsService.SumPowerUserOutfitsAsync(userId);

        await Task.WhenAll(oldOutfitTask, oldUserOutfitTask);

        Outfits oldOutfit = oldOutfitTask.Result;
        Outfits oldUserOutfit = oldUserOutfitTask.Result;

        var insertOrUpdateResult = await _userShopPurchaseRepository.InsertOrUpdateUserOutfitAsync(userId, shopDTO, purchaseCount);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.Failed)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.Failed,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _outfitsGalleryService.InsertOutfitGalleryAsync(userId, shopDTO.ShopDetail.ObjectId);

        var newOutfitTask = _outfitsService.SumPowerOutfitsPercentAsync(userId);
        var newUserOutfitTask = _userOutfitsService.SumPowerUserOutfitsAsync(userId);

        await Task.WhenAll(newOutfitTask, newUserOutfitTask);

        PowerManager deltaPower = (PowerManager)newOutfitTask.Result - (PowerManager)oldOutfit;
        PowerManager deltaUserPower = (PowerManager)newUserOutfitTask.Result - (PowerManager)oldUserOutfit;

        PowerManager totalDelta = new PowerManager();
        if (deltaPower.HasAnyPositiveStat()) totalDelta += deltaPower;
        if (deltaUserPower.HasAnyPositiveStat()) totalDelta += deltaUserPower;

        if (totalDelta.HasAnyPositiveStat())
        {
            PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
            await _powerManagerService.UpdateUserStatsAsync(userId, currentPower + totalDelta);
        }

        return InsertOrUpdateResult<bool>.Inserted(true);
    }
}