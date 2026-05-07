using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    public string search = "";
    public string rare = AppConstants.Rare.ALL;
    public string type = AppConstants.Type.ALL;
    public const int PAGE_SIZE = 10000;
    public int offset = 0;
    void Start()
    {

    }
    [ContextMenu("Run Initiate Async")]
    public async Task InitiateAsync()
    {
        User.CurrentUserId = "639126150923291730";
        List<Achievements> achievements = await AchievementsService.Create()
            .GetAchievementsAsync(search, rare, PAGE_SIZE, offset);
        await UserAchievementsService.Create()
            .InsertOrUpdateUserAchievementsBatchAsync(achievements);
        Debug.Log("<color=cyan>Achievements initiate successfully</color>");

        // List<Alchemies> alchemies = await AlchemiesService.Create()
        //     .GetAlchemiesAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserAlchemiesService.Create()
        //     .InsertOrUpdateUserAlchemiesBatchAsync(alchemies);
        // Debug.Log("<color=cyan>Alchemies initiate successfully</color>");

        // List<Architectures> architectures = await ArchitecturesService.Create()
        //     .GetArchitecturesAsync(search, rare, PAGE_SIZE, offset);
        // await UserArchitecturesService.Create()
        //     .InsertOrUpdateUserArchitecturesBatchAsync(architectures);
        // Debug.Log("<color=cyan>Architectures initiate successfully</color>");

        // List<Artifacts> artifacts = await ArtifactsService.Create()
        //     .GetArtifactsAsync(search, rare, PAGE_SIZE, offset);
        // await UserArtifactsService.Create()
        //     .InsertOrUpdateUserArtifactsBatchAsync(artifacts);
        // Debug.Log("<color=cyan>Artifacts initiate successfully</color>");

        // List<Artworks> artworks = await ArtworksService.Create()
        //     .GetArtworksAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserArtworksService.Create()
        //     .InsertOrUpdateUserArtworksBatchAsync(artworks);
        // Debug.Log("<color=cyan>Artworks initiate successfully</color>");

        // List<Avatars> avatars = await AvatarsService.Create()
        //     .GetAvatarsAsync(search, rare, PAGE_SIZE, offset);
        // await UserAvatarsService.Create()
        //     .InsertOrUpdateUserAvatarsBatchAsync(avatars);
        // Debug.Log("<color=cyan>Avatars initiate successfully</color>");

        // List<Badges> badges = await BadgesService.Create()
        //     .GetBadgesAsync(search, rare, PAGE_SIZE, offset);
        // await UserBadgesService.Create()
        //     .InsertOrUpdateUserBadgesBatchAsync(badges);
        // Debug.Log("<color=cyan>Badges initiate successfully</color>");

        // List<Beverages> beverages = await BeveragesService.Create()
        //     .GetBeveragesAsync(search, rare, PAGE_SIZE, offset);
        // await UserBeveragesService.Create()
        //     .InsertOrUpdateUserBeveragesBatchAsync(beverages);
        // Debug.Log("<color=cyan>Beverages initiate successfully</color>");

        // List<Books> books = await BooksService.Create()
        //     .GetBooksAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserBooksService.Create().InsertOrUpdateUserBooksBatchAsync(books);
        // Debug.Log("<color=cyan>Books initiate successfully</color>");

        // List<Borders> borders = await BordersService.Create()
        //     .GetBordersAsync(search, rare, PAGE_SIZE, offset);
        // await UserBordersService.Create()
        //     .InsertOrUpdateUserBordersBatchAsync(borders);
        // Debug.Log("<color=cyan>Borders initiate successfully</color>");

        // List<Buildings> buildings = await BuildingsService.Create()
        //     .GetBuildingsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserBuildingsService.Create()
        //     .InsertOrUpdateUserBuildingsBatchAsync(buildings);
        // Debug.Log("<color=cyan>Buildings initiate successfully</color>");

        // List<CardAdmirals> cardAdmirals = await CardAdmiralsService.Create()
        //     .GetCardAdmiralsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserCardAdmiralsService.Create()
        //     .InsertOrUpdateUserCardAdmiralsBatchAsync(cardAdmirals);
        // Debug.Log("<color=cyan>Card Admirals initiate successfully</color>");

        // List<CardCaptains> cardCaptains = await CardCaptainsService.Create()
        //     .GetCardCaptainsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserCardCaptainsService.Create()
        //     .InsertOrUpdateUserCardCaptainsBatchAsync(cardCaptains);
        // Debug.Log("<color=cyan>Card Captains initiate successfully</color>");

        // List<CardColonels> cardColonels = await CardColonelsService.Create()
        //     .GetCardColonelsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserCardColonelsService.Create()
        //     .InsertOrUpdateUserCardColonelsBatchAsync(cardColonels);
        // Debug.Log("<color=cyan>Card Colonels initiate successfully</color>");

        // List<CardGenerals> cardGenerals = await CardGeneralsService.Create()
        //     .GetCardGeneralsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserCardGeneralsService.Create()
        //     .InsertOrUpdateUserCardGeneralsBatchAsync(cardGenerals);
        // Debug.Log("<color=cyan>Card Generals initiate successfully</color>");

        // List<CardHeroes> cardHeroes = await CardHeroesService.Create()
        //     .GetCardHeroesAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserCardHeroesService.Create()
        //     .InsertOrUpdateUserCardHeroesBatchAsync(cardHeroes);
        // Debug.Log("<color=cyan>Card Heroes initiate successfully</color>");

        // List<CardMilitaries> cardMilitaries = await CardMilitariesService.Create()
        //     .GetCardMilitariesAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserCardMilitariesService.Create()
        //     .InsertOrUpdateUserCardMilitariesBatchAsync(cardMilitaries);
        // Debug.Log("<color=cyan>Card Militaries initiate successfully</color>");

        // List<CardMonsters> cardMonsters = await CardMonstersService.Create()
        //     .GetCardMonstersAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserCardMonstersService.Create()
        //     .InsertOrUpdateUserCardMonstersBatchAsync(cardMonsters);
        // Debug.Log("<color=cyan>Card Monsters initiate successfully</color>");

        // List<CardSpells> cardSpells = await CardSpellsService.Create()
        //     .GetCardSpellsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserCardSpellsService.Create()
        //     .InsertOrUpdateUserCardSpellsBatchAsync(cardSpells);
        // Debug.Log("<color=cyan>Card Spells initiate successfully</color>");

        // List<CardLives> cardLives = await CardLivesService.Create()
        //     .GetCardLivesAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserCardLivesService.Create()
        //     .InsertOrUpdateUserCardLivesBatchAsync(cardLives);
        // Debug.Log("<color=cyan>Card Lives initiate successfully</color>");

        // List<CollaborationEquipments> collaborationEquipments = await CollaborationEquipmentsService.Create()
        //     .GetCollaborationEquipmentsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserCollaborationEquipmentsService.Create()
        //     .InsertOrUpdateUserCollaborationEquipmentsBatchAsync(collaborationEquipments);
        // Debug.Log("<color=cyan>Collaboration Equipments initiate successfully</color>");

        // List<Collaborations> collaborations = await CollaborationsService.Create()
        //     .GetCollaborationsAsync(search, rare, PAGE_SIZE, offset);
        // await UserCollaborationsService.Create()
        //     .InsertOrUpdateUserCollaborationsBatchAsync(collaborations);
        // Debug.Log("<color=cyan>Collaborations initiate successfully</color>");

        // List<Cores> cores = await CoresService.Create()
        //     .GetCoresAsync(search, rare, PAGE_SIZE, offset);
        // await UserCoresService.Create()
        //     .InsertOrUpdateUserCoresBatchAsync(cores);
        // Debug.Log("<color=cyan>Cores initiate successfully</color>");

        // List<Emojis> emojis = await EmojisService.Create()
        //     .GetEmojisAsync(search, rare, PAGE_SIZE, offset);
        // await UserEmojisService.Create()
        //     .InsertOrUpdateUserEmojisBatchAsync(emojis);
        // Debug.Log("<color=cyan>Emojis initiate successfully</color>");

        // // List<Equipments> equipments = await EquipmentsService.Create().GetEquipmentsAsync(search, type, rare, PAGE_SIZE, offset);
        // // await UserEquipmentsService.Create().InsertOrUpdateUserEquipmentsBatchAsync(equipments);

        // List<Fashions> fashions = await FashionsService.Create()
        //     .GetFashionsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserFashionsService.Create()
        //     .InsertOrUpdateUserFashionsBatchAsync(fashions);
        // Debug.Log("<color=cyan>Fashions initiate successfully</color>");

        // List<Foods> foods = await FoodsService.Create()
        //     .GetFoodsAsync(search, rare, PAGE_SIZE, offset);
        // await UserFoodsService.Create()
        //     .InsertOrUpdateUserFoodsBatchAsync(foods);
        // Debug.Log("<color=cyan>Foods initiate successfully</color>");

        // List<Forges> forges = await ForgesService.Create()
        //     .GetForgesAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserForgesService.Create()
        //     .InsertOrUpdateUserForgesBatchAsync(forges);
        // Debug.Log("<color=cyan>Forges initiate successfully</color>");

        // List<Furnitures> furnitures = await FurnituresService.Create()
        //     .GetFurnituresAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserFurnituresService.Create()
        //     .InsertOrUpdateUserFurnituresBatchAsync(furnitures);
        // Debug.Log("<color=cyan>Furnitures initiate successfully</color>");

        // List<MagicFormationCircles> magicFormationCircles = await MagicFormationCirclesService.Create()
        //     .GetMagicFormationCirclesAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserMagicFormationCirclesService.Create()
        //     .InsertOrUpdateUserMagicFormationCirclesBatchAsync(magicFormationCircles);
        // Debug.Log("<color=cyan>Magic Formation Circles initiate successfully</color>");

        // List<MechaBeasts> mechaBeasts = await MechaBeastsService.Create()
        //     .GetMechaBeastsAsync(search, rare, PAGE_SIZE, offset);
        // await UserMechaBeastsService.Create()
        //     .InsertOrUpdateUserMechaBeastsBatchAsync(mechaBeasts);
        // Debug.Log("<color=cyan>Mecha Beasts initiate successfully</color>");

        // List<Medals> medals = await MedalsService.Create()
        //     .GetMedalsAsync(search, rare, PAGE_SIZE, offset);
        // await UserMedalsService.Create()
        //     .InsertOrUpdateUserMedalsBatchAsync(medals);
        // Debug.Log("<color=cyan>Medals initiate successfully</color>");

        // List<Pets> pets = await PetsService.Create()
        //     .GetPetsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserPetsService.Create()
        //     .InsertOrUpdateUserPetsBatchAsync(pets);
        // Debug.Log("<color=cyan>Pets initiate successfully</color>");

        // List<Plants> plants = await PlantsService.Create()
        //     .GetPlantsAsync(search, rare, PAGE_SIZE, offset);
        // await UserPlantsService.Create()
        //     .InsertOrUpdateUserPlantsBatchAsync(plants);
        // Debug.Log("<color=cyan>Plants initiate successfully</color>");

        // List<Puppets> puppets = await PuppetsService.Create()
        //     .GetPuppetsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserPuppetsService.Create()
        //     .InsertOrUpdateUserPuppetsBatchAsync(puppets);
        // Debug.Log("<color=cyan>Puppets initiate successfully</color>");

        // List<Relics> relics = await RelicsService.Create()
        //     .GetRelicsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserRelicsService.Create()
        //     .InsertOrUpdateUserRelicsBatchAsync(relics);
        // Debug.Log("<color=cyan>Relics initiate successfully</color>");

        // List<Robots> robots = await RobotsService.Create()
        //     .GetRobotsAsync(search, rare, PAGE_SIZE, offset);
        // await UserRobotsService.Create()
        //     .InsertOrUpdateUserRobotsBatchAsync(robots);
        // Debug.Log("<color=cyan>Robots initiate successfully</color>");

        // List<Runes> runes = await RunesService.Create()
        //     .GetRunesAsync(search, rare, PAGE_SIZE, offset);
        // await UserRunesService.Create()
        //     .InsertOrUpdateUserRunesBatchAsync(runes);
        // Debug.Log("<color=cyan>Runes initiate successfully</color>");

        // List<Skills> skills = await SkillsService.Create()
        //     .GetSkillsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserSkillsService.Create()
        //     .InsertOrUpdateUserSkillsBatchAsync(skills);
        // Debug.Log("<color=cyan>Skills initiate successfully</color>");

        // List<SpiritBeasts> spiritBeasts = await SpiritBeastsService.Create()
        //     .GetSpiritBeastsAsync(search, rare, PAGE_SIZE, offset);
        // await UserSpiritBeastsService.Create()
        //     .InsertOrUpdateUserSpiritBeastsBatchAsync(spiritBeasts);
        // Debug.Log("<color=cyan>Spirit Beasts initiate successfully</color>");

        // List<SpiritCards> spiritCards = await SpiritCardsService.Create()
        //     .GetSpiritCardsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserSpiritCardsService.Create()
        //     .InsertOrUpdateUserSpiritCardsBatchAsync(spiritCards);
        // Debug.Log("<color=cyan>Spirit Cards initiate successfully</color>");

        // List<Symbols> symbols = await SymbolsService.Create()
        //     .GetSymbolsAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserSymbolsService.Create()
        //     .InsertOrUpdateUserSymbolsBatchAsync(symbols);
        // Debug.Log("<color=cyan>Symbols initiate successfully</color>");

        // List<Talismans> talismans = await TalismansService.Create()
        //     .GetTalismansAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserTalismansService.Create()
        //     .InsertOrUpdateUserTalismansBatchAsync(talismans);
        // Debug.Log("<color=cyan>Talismans initiate successfully</color>");

        // List<Technologies> technologies = await TechnologiesService.Create()
        //     .GetTechnologiesAsync(search, rare, PAGE_SIZE, offset);
        // await UserTechnologiesService.Create()
        //     .InsertOrUpdateUserTechnologiesBatchAsync(technologies);
        // Debug.Log("<color=cyan>Technologies initiate successfully</color>");

        // List<Titles> titles = await TitlesService.Create()
        //     .GetTitlesAsync(search, rare, PAGE_SIZE, offset);
        // await UserTitlesService.Create()
        //     .InsertOrUpdateUserTitlesBatchAsync(titles);
        // Debug.Log("<color=cyan>Titles initiate successfully</color>");

        // List<Vehicles> vehicles = await VehiclesService.Create()
        //     .GetVehiclesAsync(search, type, rare, PAGE_SIZE, offset);
        // await UserVehiclesService.Create()
        //     .InsertOrUpdateUserVehiclesBatchAsync(vehicles);
        // Debug.Log("<color=cyan>Vehicles initiate successfully</color>");

        // List<Weapons> weapons = await WeaponsService.Create()
        //     .GetWeaponsAsync(search, rare, PAGE_SIZE, offset);
        // await UserWeaponsService.Create()
        //     .InsertOrUpdateUserWeaponsBatchAsync(weapons);
        // Debug.Log("<color=cyan>Weapons initiate successfully</color>");

        // List<Items> items = await ItemsService.Create().GetItemsAsync(search, rare, PAGE_SIZE, offset);
        // await UserItemsService.Create().InsertOrUpdateUserItemsBatchAsync(items);
    }
}
