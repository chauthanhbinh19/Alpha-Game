using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    public string search = "";
    public string rare = AppConstants.Rare.ALL;
    public string type = AppConstants.Type.ALL;
    public const int PAGE_SIZE = 100000;
    public int offset = 0;
    void Start()
    {

    }
    [ContextMenu("Run Initiate Async")]
    public async Task InitiateAsync()
    {
        User.CurrentUserId = "";
        List<Achievements> achievements = await AchievementsService.Create()
            .GetAchievementsAsync(search, rare, PAGE_SIZE, offset);
        await UserAchievementsService.Create()
            .InsertOrUpdateUserAchievementsBatchAsync(achievements);

        List<Alchemies> alchemies = await AlchemiesService.Create()
            .GetAlchemiesAsync(search, type, rare, PAGE_SIZE, offset);
        await UserAlchemiesService.Create()
            .InsertOrUpdateUserAlchemiesBatchAsync(alchemies);

        List<Architectures> architectures = await ArchitecturesService.Create()
            .GetArchitecturesAsync(search, rare, PAGE_SIZE, offset);
        await UserArchitecturesService.Create()
            .InsertOrUpdateUserArchitecturesBatchAsync(architectures);

        List<Artifacts> artifacts = await ArtifactsService.Create()
            .GetArtifactsAsync(search, rare, PAGE_SIZE, offset);
        await UserArtifactsService.Create()
            .InsertOrUpdateUserArtifactsBatchAsync(artifacts);

        List<Artworks> artworks = await ArtworksService.Create()
            .GetArtworksAsync(search, type, rare, PAGE_SIZE, offset);
        await UserArtworksService.Create()
            .InsertOrUpdateUserArtworksBatchAsync(artworks);

        List<Avatars> avatars = await AvatarsService.Create()
            .GetAvatarsAsync(search, rare, PAGE_SIZE, offset);
        await UserAvatarsService.Create()
            .InsertOrUpdateUserAvatarsBatchAsync(avatars);

        List<Badges> badges = await BadgesService.Create()
            .GetBadgesAsync(search, rare, PAGE_SIZE, offset);
        await UserBadgesService.Create()
            .InsertOrUpdateUserBadgesBatchAsync(badges);

        List<Beverages> beverages = await BeveragesService.Create()
            .GetBeveragesAsync(search, rare, PAGE_SIZE, offset);
        await UserBeveragesService.Create()
            .InsertOrUpdateUserBeveragesBatchAsync(beverages);

        List<Books> books = await BooksService.Create()
            .GetBooksAsync(search, type, rare, PAGE_SIZE, offset);
        await UserBooksService.Create().InsertOrUpdateUserBooksBatchAsync(books);

        List<Borders> borders = await BordersService.Create()
            .GetBordersAsync(search, rare, PAGE_SIZE, offset);
        await UserBordersService.Create()
            .InsertOrUpdateUserBordersBatchAsync(borders);

        List<Buildings> buildings = await BuildingsService.Create()
            .GetBuildingsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserBuildingsService.Create()
            .InsertOrUpdateUserBuildingsBatchAsync(buildings);

        List<CardAdmirals> cardAdmirals = await CardAdmiralsService.Create()
            .GetCardAdmiralsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserCardAdmiralsService.Create()
            .InsertOrUpdateUserCardAdmiralsBatchAsync(cardAdmirals);

        List<CardCaptains> cardCaptains = await CardCaptainsService.Create()
            .GetCardCaptainsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserCardCaptainsService.Create()
            .InsertOrUpdateUserCardCaptainsBatchAsync(cardCaptains);

        List<CardColonels> cardColonels = await CardColonelsService.Create()
            .GetCardColonelsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserCardColonelsService.Create()
            .InsertOrUpdateUserCardColonelsBatchAsync(cardColonels);

        List<CardGenerals> cardGenerals = await CardGeneralsService.Create()
            .GetCardGeneralsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserCardGeneralsService.Create()
            .InsertOrUpdateUserCardGeneralsBatchAsync(cardGenerals);

        List<CardHeroes> cardHeroes = await CardHeroesService.Create()
            .GetCardHeroesAsync(search, type, rare, PAGE_SIZE, offset);
        await UserCardHeroesService.Create()
            .InsertOrUpdateUserCardHeroesBatchAsync(cardHeroes);

        List<CardMilitaries> cardMilitaries = await CardMilitariesService.Create()
            .GetCardMilitariesAsync(search, type, rare, PAGE_SIZE, offset);
        await UserCardMilitariesService.Create()
            .InsertOrUpdateUserCardMilitariesBatchAsync(cardMilitaries);

        List<CardMonsters> cardMonsters = await CardMonstersService.Create()
            .GetCardMonstersAsync(search, type, rare, PAGE_SIZE, offset);
        await UserCardMonstersService.Create()
            .InsertOrUpdateUserCardMonstersBatchAsync(cardMonsters);

        List<CardSpells> cardSpells = await CardSpellsService.Create()
            .GetCardSpellsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserCardSpellsService.Create()
            .InsertOrUpdateUserCardSpellsBatchAsync(cardSpells);

        List<CardLives> cardLives = await CardLivesService.Create()
            .GetCardLivesAsync(search, type, rare, PAGE_SIZE, offset);
        await UserCardLivesService.Create()
            .InsertOrUpdateUserCardLivesBatchAsync(cardLives);

        List<CollaborationEquipments> collaborationEquipments = await CollaborationEquipmentsService.Create()
            .GetCollaborationEquipmentsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserCollaborationEquipmentsService.Create()
            .InsertOrUpdateUserCollaborationEquipmentsBatchAsync(collaborationEquipments);

        List<Collaborations> collaborations = await CollaborationsService.Create()
            .GetCollaborationsAsync(search, rare, PAGE_SIZE, offset);
        await UserCollaborationsService.Create()
            .InsertOrUpdateUserCollaborationsBatchAsync(collaborations);

        List<Cores> cores = await CoresService.Create()
            .GetCoresAsync(search, rare, PAGE_SIZE, offset);
        await UserCoresService.Create()
            .InsertOrUpdateUserCoresBatchAsync(cores);

        List<Emojis> emojis = await EmojisService.Create()
            .GetEmojisAsync(search, rare, PAGE_SIZE, offset);
        await UserEmojisService.Create()
            .InsertOrUpdateUserEmojisBatchAsync(emojis);

        // List<Equipments> equipments = await EquipmentsService.Create().GetEquipmentsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserEquipmentsService.Create().InsertOrUpdateUserEquipmentsBatchAsync(equipments);

        List<Fashions> fashions = await FashionsService.Create()
            .GetFashionsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserFashionsService.Create()
            .InsertOrUpdateUserFashionsBatchAsync(fashions);

        List<Foods> foods = await FoodsService.Create()
            .GetFoodsAsync(search, rare, PAGE_SIZE, offset);
        await UserFoodsService.Create()
            .InsertOrUpdateUserFoodsBatchAsync(foods);

        List<Forges> forges = await ForgesService.Create()
            .GetForgesAsync(search, type, rare, PAGE_SIZE, offset);
        await UserForgesService.Create()
            .InsertOrUpdateUserForgesBatchAsync(forges);

        List<Furnitures> furnitures = await FurnituresService.Create()
            .GetFurnituresAsync(search, type, rare, PAGE_SIZE, offset);
        await UserFurnituresService.Create()
            .InsertOrUpdateUserFurnituresBatchAsync(furnitures);

        List<MagicFormationCircles> magicFormationCircles = await MagicFormationCirclesService.Create()
            .GetMagicFormationCirclesAsync(search, type, rare, PAGE_SIZE, offset);
        await UserMagicFormationCirclesService.Create()
            .InsertOrUpdateUserMagicFormationCirclesBatchAsync(magicFormationCircles);

        List<MechaBeasts> mechaBeasts = await MechaBeastsService.Create()
            .GetMechaBeastsAsync(search, rare, PAGE_SIZE, offset);
        await UserMechaBeastsService.Create()
            .InsertOrUpdateUserMechaBeastsBatchAsync(mechaBeasts);

        List<Medals> medals = await MedalsService.Create()
            .GetMedalsAsync(search, rare, PAGE_SIZE, offset);
        await UserMedalsService.Create()
            .InsertOrUpdateUserMedalsBatchAsync(medals);

        List<Pets> pets = await PetsService.Create()
            .GetPetsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserPetsService.Create()
            .InsertOrUpdateUserPetsBatchAsync(pets);

        List<Plants> plants = await PlantsService.Create()
            .GetPlantsAsync(search, rare, PAGE_SIZE, offset);
        await UserPlantsService.Create()
            .InsertOrUpdateUserPlantsBatchAsync(plants);

        List<Puppets> puppets = await PuppetsService.Create()
            .GetPuppetsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserPuppetsService.Create()
            .InsertOrUpdateUserPuppetsBatchAsync(puppets);

        List<Relics> relics = await RelicsService.Create()
            .GetRelicsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserRelicsService.Create()
            .InsertOrUpdateUserRelicsBatchAsync(relics);

        List<Robots> robots = await RobotsService.Create()
            .GetRobotsAsync(search, rare, PAGE_SIZE, offset);
        await UserRobotsService.Create()
            .InsertOrUpdateUserRobotsBatchAsync(robots);

        List<Runes> runes = await RunesService.Create()
            .GetRunesAsync(search, rare, PAGE_SIZE, offset);
        await UserRunesService.Create()
            .InsertOrUpdateUserRunesBatchAsync(runes);

        List<Skills> skills = await SkillsService.Create()
            .GetSkillsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserSkillsService.Create()
            .InsertOrUpdateUserSkillsBatchAsync(skills);

        List<SpiritBeasts> spiritBeasts = await SpiritBeastsService.Create()
            .GetSpiritBeastsAsync(search, rare, PAGE_SIZE, offset);
        await UserSpiritBeastsService.Create()
            .InsertOrUpdateUserSpiritBeastsBatchAsync(spiritBeasts);

        List<SpiritCards> spiritCards = await SpiritCardsService.Create()
            .GetSpiritCardsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserSpiritCardsService.Create()
            .InsertOrUpdateUserSpiritCardsBatchAsync(spiritCards);

        List<Symbols> symbols = await SymbolsService.Create()
            .GetSymbolsAsync(search, type, rare, PAGE_SIZE, offset);
        await UserSymbolsService.Create()
            .InsertOrUpdateUserSymbolsBatchAsync(symbols);

        List<Talismans> talismans = await TalismansService.Create()
            .GetTalismansAsync(search, type, rare, PAGE_SIZE, offset);
        await UserTalismansService.Create()
            .InsertOrUpdateUserTalismansBatchAsync(talismans);

        List<Technologies> technologies = await TechnologiesService.Create()
            .GetTechnologiesAsync(search, rare, PAGE_SIZE, offset);
        await UserTechnologiesService.Create()
            .InsertOrUpdateUserTechnologiesBatchAsync(technologies);

        List<Titles> titles = await TitlesService.Create()
            .GetTitlesAsync(search, rare, PAGE_SIZE, offset);
        await UserTitlesService.Create()
            .InsertOrUpdateUserTitlesBatchAsync(titles);

        List<Vehicles> vehicles = await VehiclesService.Create()
            .GetVehiclesAsync(search, type, rare, PAGE_SIZE, offset);
        await UserVehiclesService.Create()
            .InsertOrUpdateUserVehiclesBatchAsync(vehicles);

        List<Weapons> weapons = await WeaponsService.Create()
            .GetWeaponsAsync(search, rare, PAGE_SIZE, offset);
        await UserWeaponsService.Create()
            .InsertOrUpdateUserWeaponsBatchAsync(weapons);

        // List<Items> items = await ItemsService.Create().GetItemsAsync(search, rare, PAGE_SIZE, offset);
        // await UserItemsService.Create().InsertOrUpdateUserItemsBatchAsync(items);
    }
}
