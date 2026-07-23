using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class Test : MonoBehaviour
{
    public string Search = "";
    public string Rare = AppConstants.Rare.ALL;
    public string Type = AppConstants.Type.ALL;
    public const int PAGE_SIZE = 10000;
    public int Offset = 0;
    public string userId;
    void Start()
    {
        
    }
    [ContextMenu("Run Initiate Async")]
    public async Task InitiateAsync()
    {
        User.CurrentUserId = userId;

        Debug.Log("<color=yellow>Start</color>");
        List<Achievements> achievements = await AchievementsService.Create()
            .GetAchievementsAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserAchievementsService.Create()
            .InsertOrUpdateUserAchievementsBatchAsync(User.CurrentUserId, achievements);
        Debug.Log("<color=cyan>Achievements initiate successfully</color>");

        List<Alchemies> alchemies = await AlchemiesService.Create()
            .GetAlchemiesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserAlchemiesService.Create()
            .InsertOrUpdateUserAlchemiesBatchAsync(User.CurrentUserId, alchemies);
        Debug.Log("<color=cyan>Alchemies initiate successfully</color>");

        List<Architectures> architectures = await ArchitecturesService.Create()
            .GetArchitecturesAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserArchitecturesService.Create()
            .InsertOrUpdateUserArchitecturesBatchAsync(User.CurrentUserId, architectures);
        Debug.Log("<color=cyan>Architectures initiate successfully</color>");

        List<Artifacts> artifacts = await ArtifactsService.Create()
            .GetArtifactsAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserArtifactsService.Create()
            .InsertOrUpdateUserArtifactsBatchAsync(User.CurrentUserId, artifacts);
        Debug.Log("<color=cyan>Artifacts initiate successfully</color>");

        List<Artworks> artworks = await ArtworksService.Create()
            .GetArtworksAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserArtworksService.Create()
            .InsertOrUpdateUserArtworksBatchAsync(User.CurrentUserId, artworks);
        Debug.Log("<color=cyan>Artworks initiate successfully</color>");

        List<Avatars> avatars = await AvatarsService.Create()
            .GetAvatarsAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserAvatarsService.Create()
            .InsertOrUpdateUserAvatarsBatchAsync(User.CurrentUserId, avatars);
        Debug.Log("<color=cyan>Avatars initiate successfully</color>");

        List<Badges> badges = await BadgesService.Create()
            .GetBadgesAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserBadgesService.Create()
            .InsertOrUpdateUserBadgesBatchAsync(User.CurrentUserId, badges);
        Debug.Log("<color=cyan>Badges initiate successfully</color>");

        List<Beverages> beverages = await BeveragesService.Create()
            .GetBeveragesAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserBeveragesService.Create()
            .InsertOrUpdateUserBeveragesBatchAsync(User.CurrentUserId, beverages);
        Debug.Log("<color=cyan>Beverages initiate successfully</color>");

        List<Books> books = await BooksService.Create()
            .GetBooksAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserBooksService.Create().InsertOrUpdateUserBooksBatchAsync(User.CurrentUserId, books);
        Debug.Log("<color=cyan>Books initiate successfully</color>");

        List<Borders> borders = await BordersService.Create()
            .GetBordersAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserBordersService.Create()
            .InsertOrUpdateUserBordersBatchAsync(User.CurrentUserId, borders);
        Debug.Log("<color=cyan>Borders initiate successfully</color>");

        List<Buildings> buildings = await BuildingsService.Create()
            .GetBuildingsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserBuildingsService.Create()
            .InsertOrUpdateUserBuildingsBatchAsync(User.CurrentUserId, buildings);
        Debug.Log("<color=cyan>Buildings initiate successfully</color>");

        List<CardAdmirals> cardAdmirals = await CardAdmiralsService.Create()
            .GetCardAdmiralsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserCardAdmiralsService.Create()
            .InsertOrUpdateUserCardAdmiralsBatchAsync(User.CurrentUserId, cardAdmirals);
        Debug.Log("<color=cyan>Card Admirals initiate successfully</color>");

        List<CardCaptains> cardCaptains = await CardCaptainsService.Create()
            .GetCardCaptainsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserCardCaptainsService.Create()
            .InsertOrUpdateUserCardCaptainsBatchAsync(User.CurrentUserId, cardCaptains);
        Debug.Log("<color=cyan>Card Captains initiate successfully</color>");

        List<CardColonels> cardColonels = await CardColonelsService.Create()
            .GetCardColonelsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserCardColonelsService.Create()
            .InsertOrUpdateUserCardColonelsBatchAsync(User.CurrentUserId, cardColonels);
        Debug.Log("<color=cyan>Card Colonels initiate successfully</color>");

        List<CardGenerals> cardGenerals = await CardGeneralsService.Create()
            .GetCardGeneralsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserCardGeneralsService.Create()
            .InsertOrUpdateUserCardGeneralsBatchAsync(User.CurrentUserId, cardGenerals);
        Debug.Log("<color=cyan>Card Generals initiate successfully</color>");

        List<CardHeroes> cardHeroes = await CardHeroesService.Create()
            .GetCardHeroesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserCardHeroesService.Create()
            .InsertOrUpdateUserCardHeroesBatchAsync(User.CurrentUserId, cardHeroes);
        Debug.Log("<color=cyan>Card Heroes initiate successfully</color>");

        List<CardMilitaries> cardMilitaries = await CardMilitariesService.Create()
            .GetCardMilitariesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserCardMilitariesService.Create()
            .InsertOrUpdateUserCardMilitariesBatchAsync(User.CurrentUserId, cardMilitaries);
        Debug.Log("<color=cyan>Card Militaries initiate successfully</color>");

        List<CardMonsters> cardMonsters = await CardMonstersService.Create()
            .GetCardMonstersAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserCardMonstersService.Create()
            .InsertOrUpdateUserCardMonstersBatchAsync(User.CurrentUserId, cardMonsters);
        Debug.Log("<color=cyan>Card Monsters initiate successfully</color>");

        List<CardSpells> cardSpells = await CardSpellsService.Create()
            .GetCardSpellsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserCardSpellsService.Create()
            .InsertOrUpdateUserCardSpellsBatchAsync(User.CurrentUserId, cardSpells);
        Debug.Log("<color=cyan>Card Spells initiate successfully</color>");

        List<CardSoldiers> cardSoldiers = await CardSoldiersService.Create()
            .GetCardSoldiersAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserCardSoldiersService.Create()
            .InsertOrUpdateUserCardSoldiersBatchAsync(User.CurrentUserId, cardSoldiers);
        Debug.Log("<color=cyan>Card Soldiers initiate successfully</color>");

        List<CardLives> cardLives = await CardLivesService.Create()
            .GetCardLivesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserCardLivesService.Create()
            .InsertOrUpdateUserCardLivesBatchAsync(User.CurrentUserId, cardLives);
        Debug.Log("<color=cyan>Card Lives initiate successfully</color>");

        List<CollaborationEquipments> collaborationEquipments = await CollaborationEquipmentsService.Create()
            .GetCollaborationEquipmentsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserCollaborationEquipmentsService.Create()
            .InsertOrUpdateUserCollaborationEquipmentsBatchAsync(User.CurrentUserId, collaborationEquipments);
        Debug.Log("<color=cyan>Collaboration Equipments initiate successfully</color>");

        List<Collaborations> collaborations = await CollaborationsService.Create()
            .GetCollaborationsAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserCollaborationsService.Create()
            .InsertOrUpdateUserCollaborationsBatchAsync(User.CurrentUserId, collaborations);
        Debug.Log("<color=cyan>Collaborations initiate successfully</color>");

        List<Cores> cores = await CoresService.Create()
            .GetCoresAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserCoresService.Create()
            .InsertOrUpdateUserCoresBatchAsync(User.CurrentUserId, cores);
        Debug.Log("<color=cyan>Cores initiate successfully</color>");

        List<Emojis> emojis = await EmojisService.Create()
            .GetEmojisAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserEmojisService.Create()
            .InsertOrUpdateUserEmojisBatchAsync(User.CurrentUserId, emojis);
        Debug.Log("<color=cyan>Emojis initiate successfully</color>");

        List<Equipments> equipments = await EquipmentsService.Create()
            .GetEquipmentsAsync(Search, Type, Rare, 23000, Offset);
        var equipmentsWithQuantity = equipments
            .Select(x => (data: x, quantity: 1000000d))
            .ToList();
        await UserEquipmentsService.Create().InsertOrUpdateUserEquipmentsBatchAsync(User.CurrentUserId, equipmentsWithQuantity);
        Debug.Log("<color=cyan>Equipments initiate successfully</color>");

        List<Fashions> fashions = await FashionsService.Create()
            .GetFashionsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserFashionsService.Create()
            .InsertOrUpdateUserFashionsBatchAsync(User.CurrentUserId, fashions);
        Debug.Log("<color=cyan>Fashions initiate successfully</color>");

        List<Foods> foods = await FoodsService.Create()
            .GetFoodsAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserFoodsService.Create()
            .InsertOrUpdateUserFoodsBatchAsync(User.CurrentUserId, foods);
        Debug.Log("<color=cyan>Foods initiate successfully</color>");

        List<Forges> forges = await ForgesService.Create()
            .GetForgesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserForgesService.Create()
            .InsertOrUpdateUserForgesBatchAsync(User.CurrentUserId, forges);
        Debug.Log("<color=cyan>Forges initiate successfully</color>");

        List<Furnitures> furnitures = await FurnituresService.Create()
            .GetFurnituresAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserFurnituresService.Create()
            .InsertOrUpdateUserFurnituresBatchAsync(User.CurrentUserId, furnitures);
        Debug.Log("<color=cyan>Furnitures initiate successfully</color>");

        List<MagicFormationCircles> magicFormationCircles = await MagicFormationCirclesService.Create()
            .GetMagicFormationCirclesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserMagicFormationCirclesService.Create()
            .InsertOrUpdateUserMagicFormationCirclesBatchAsync(User.CurrentUserId, magicFormationCircles);
        Debug.Log("<color=cyan>Magic Formation Circles initiate successfully</color>");

        List<MechaBeasts> mechaBeasts = await MechaBeastsService.Create()
            .GetMechaBeastsAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserMechaBeastsService.Create()
            .InsertOrUpdateUserMechaBeastsBatchAsync(User.CurrentUserId, mechaBeasts);
        Debug.Log("<color=cyan>Mecha Beasts initiate successfully</color>");

        List<Medals> medals = await MedalsService.Create()
            .GetMedalsAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserMedalsService.Create()
            .InsertOrUpdateUserMedalsBatchAsync(User.CurrentUserId, medals);
        Debug.Log("<color=cyan>Medals initiate successfully</color>");

        List<Pets> pets = await PetsService.Create()
            .GetPetsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserPetsService.Create()
            .InsertOrUpdateUserPetsBatchAsync(User.CurrentUserId, pets);
        Debug.Log("<color=cyan>Pets initiate successfully</color>");

        List<Plants> plants = await PlantsService.Create()
            .GetPlantsAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserPlantsService.Create()
            .InsertOrUpdateUserPlantsBatchAsync(User.CurrentUserId, plants);
        Debug.Log("<color=cyan>Plants initiate successfully</color>");

        List<Puppets> puppets = await PuppetsService.Create()
            .GetPuppetsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserPuppetsService.Create()
            .InsertOrUpdateUserPuppetsBatchAsync(User.CurrentUserId, puppets);
        Debug.Log("<color=cyan>Puppets initiate successfully</color>");

        List<Relics> relics = await RelicsService.Create()
            .GetRelicsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserRelicsService.Create()
            .InsertOrUpdateUserRelicsBatchAsync(User.CurrentUserId, relics);
        Debug.Log("<color=cyan>Relics initiate successfully</color>");

        List<Robots> robots = await RobotsService.Create()
            .GetRobotsAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserRobotsService.Create()
            .InsertOrUpdateUserRobotsBatchAsync(User.CurrentUserId, robots);
        Debug.Log("<color=cyan>Robots initiate successfully</color>");

        List<Runes> runes = await RunesService.Create()
            .GetRunesAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserRunesService.Create()
            .InsertOrUpdateUserRunesBatchAsync(User.CurrentUserId, runes);
        Debug.Log("<color=cyan>Runes initiate successfully</color>");

        List<Skills> skills = await SkillsService.Create()
            .GetSkillsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserSkillsService.Create()
            .InsertOrUpdateUserSkillsBatchAsync(User.CurrentUserId, skills);
        Debug.Log("<color=cyan>Skills initiate successfully</color>");

        List<SpiritBeasts> spiritBeasts = await SpiritBeastsService.Create()
            .GetSpiritBeastsAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserSpiritBeastsService.Create()
            .InsertOrUpdateUserSpiritBeastsBatchAsync(User.CurrentUserId, spiritBeasts);
        Debug.Log("<color=cyan>Spirit Beasts initiate successfully</color>");

        List<SpiritCards> spiritCards = await SpiritCardsService.Create()
            .GetSpiritCardsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserSpiritCardsService.Create()
            .InsertOrUpdateUserSpiritCardsBatchAsync(User.CurrentUserId, spiritCards);
        Debug.Log("<color=cyan>Spirit Cards initiate successfully</color>");

        List<Symbols> symbols = await SymbolsService.Create()
            .GetSymbolsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserSymbolsService.Create()
            .InsertOrUpdateUserSymbolsBatchAsync(User.CurrentUserId, symbols);
        Debug.Log("<color=cyan>Symbols initiate successfully</color>");

        List<Talismans> talismans = await TalismansService.Create()
            .GetTalismansAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserTalismansService.Create()
            .InsertOrUpdateUserTalismansBatchAsync(User.CurrentUserId, talismans);
        Debug.Log("<color=cyan>Talismans initiate successfully</color>");

        List<Technologies> technologies = await TechnologiesService.Create()
            .GetTechnologiesAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserTechnologiesService.Create()
            .InsertOrUpdateUserTechnologiesBatchAsync(User.CurrentUserId, technologies);
        Debug.Log("<color=cyan>Technologies initiate successfully</color>");

        List<Titles> titles = await TitlesService.Create()
            .GetTitlesAsync(Search, Rare, PAGE_SIZE, Offset);
        await UserTitlesService.Create()
            .InsertOrUpdateUserTitlesBatchAsync(User.CurrentUserId, titles);
        Debug.Log("<color=cyan>Titles initiate successfully</color>");

        List<Vehicles> vehicles = await VehiclesService.Create()
            .GetVehiclesAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserVehiclesService.Create()
            .InsertOrUpdateUserVehiclesBatchAsync(User.CurrentUserId, vehicles);
        Debug.Log("<color=cyan>Vehicles initiate successfully</color>");

        List<Weapons> weapons = await WeaponsService.Create()
            .GetWeaponsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserWeaponsService.Create()
            .InsertOrUpdateUserWeaponsBatchAsync(User.CurrentUserId, weapons);
        Debug.Log("<color=cyan>Weapons initiate successfully</color>");

        List<Outfits> outfits = await OutfitsService.Create()
            .GetOutfitsAsync(Search, Type, Rare, PAGE_SIZE, Offset);
        await UserOutfitsService.Create()
            .InsertOrUpdateUserOutfitsBatchAsync(User.CurrentUserId, outfits);
        Debug.Log("<color=cyan>Outfits initiate successfully</color>");

        List<Items> items = await ItemsService.Create()
            .GetItemsAsync();
        var itemsWithQuantity = items
            .Select(x => (data: x, quantity: 10000000000d))
            .ToList();
        await UserItemsService.Create().InsertOrUpdateUserItemsBatchAsync(User.CurrentUserId, itemsWithQuantity);
        Debug.Log("<color=cyan>Items initiate successfully</color>");
        Debug.Log("<color=yellow>End</color>");
    }
    [ContextMenu("Run Initiate Team Async")]
    public async Task InitiateTeamAsync()
    {
        User.CurrentUserId = userId;

        Debug.Log("<color=yellow>Start</color>");
        await TeamsService.Create().UpdateUserCardHeroesTeamPositionsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Card Heroes team and position initiate successfully</color>");
        await TeamsService.Create().UpdateUserCardCaptainsTeamPositionsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Card Captains team and position initiate successfully</color>");
        await TeamsService.Create().UpdateUserCardColonelsTeamPositionsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Card Colonels team and position initiate successfully</color>");
        await TeamsService.Create().UpdateUserCardGeneralsTeamPositionsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Card Generals team and position initiate successfully</color>");
        await TeamsService.Create().UpdateUserCardAdmiralsTeamPositionsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Card Admirals team and position initiate successfully</color>");
        await TeamsService.Create().UpdateUserCardMonstersTeamPositionsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Card Monsters team and position initiate successfully</color>");
        await TeamsService.Create().UpdateUserCardMilitariesTeamPositionsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Card Militaries team and position initiate successfully</color>");
        await TeamsService.Create().UpdateUserCardSoldiersTeamPositionsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Card Soldiers team and position initiate successfully</color>");
        await TeamsService.Create().UpdateUserCardSpellsTeamPositionsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Card Spells team and position initiate successfully</color>");

        Debug.Log("<color=yellow>End</color>");
    }
    [ContextMenu("Run Initiate Skill Async")]
    public async Task InitiateSkillAsync()
    {
        User.CurrentUserId = userId;

        Debug.Log("<color=yellow>Start</color>");
        await UserSkillsService.Create().AssignRandomSkillsToUserCardHeroesAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Skills for user Card Heroes initiate successfully</color>");
        await UserSkillsService.Create().AssignRandomSkillsToUserCardCaptainsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Skills for user Card Captains initiate successfully</color>");
        await UserSkillsService.Create().AssignRandomSkillsToUserCardColonelsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Skills for user Card Colonels initiate successfully</color>");
        await UserSkillsService.Create().AssignRandomSkillsToUserCardGeneralsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Skills for user Card Generals initiate successfully</color>");
        await UserSkillsService.Create().AssignRandomSkillsToUserCardAdmiralsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Skills for user Card Admirals initiate successfully</color>");
        await UserSkillsService.Create().AssignRandomSkillsToUserCardMonstersAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Skills for user Card Monsters initiate successfully</color>");
        await UserSkillsService.Create().AssignRandomSkillsToUserCardMilitariesAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Skills for user Card Militaries initiate successfully</color>");
        await UserSkillsService.Create().AssignRandomSkillsToUserCardSoldiersAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Skills for user Card Soldiers initiate successfully</color>");
        await UserSkillsService.Create().AssignRandomSkillsToUserCardSpellsAsync(User.CurrentUserId);
        Debug.Log("<color=cyan>Skills for user Card Spells initiate successfully</color>");

        Debug.Log("<color=yellow>End</color>");
    }
    public async Task GetUserSkillsAsync()
    {
        User.CurrentUserId = "639167826246347876";
        await UserSkillsService.Create().GetUserSkillsAsync(User.CurrentUserId, Search, Type, PAGE_SIZE, Offset, Rare);
    }
}

