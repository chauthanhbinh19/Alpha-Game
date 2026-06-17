using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GameDataCacheConfig
{
    private static GameDataCacheConfig _instance;
    public static GameDataCacheConfig Instance => _instance ??= new GameDataCacheConfig();

    public List<Achievements> AllAchievements { get; private set; }
    public List<Alchemies> AllAlchemies { get; private set; }
    public List<Architectures> AllArchitectures { get; private set; }
    public List<Artifacts> AllArtifacts { get; private set; }
    public List<Artworks> AllArtworks { get; private set; }
    public List<Avatars> AllAvatars { get; private set; }
    public List<Badges> AllBadges { get; private set; }
    public List<Beverages> AllBeverages { get; private set; }
    public List<Books> AllBooks { get; private set; }
    public List<Borders> AllBorders { get; private set; }
    public List<Buildings> AllBuildings { get; private set; }
    public List<CardAdmirals> AllCardAdmirals { get; private set; }
    public List<CardCaptains> AllCardCaptains { get; private set; }
    public List<CardColonels> AllCardColonels { get; private set; }
    public List<CardGenerals> AllCardGenerals { get; private set; }
    public List<CardHeroes> AllCardHeroes { get; private set; }
    public List<CardLives> AllCardLives { get; private set; }
    public List<CardMilitaries> AllCardMilitaries { get; private set; }
    public List<CardMonsters> AllCardMonsters { get; private set; }
    public List<CardSoldiers> AllCardSoldiers { get; private set; }
    public List<CardSpells> AllCardSpells { get; private set; }
    public List<CollaborationEquipments> AllCollaborationEquipments { get; private set; }
    public List<Collaborations> AllCollaborations { get; private set; }
    public List<Cores> AllCores { get; private set; }
    public List<Emojis> AllEmojis { get; private set; }
    public List<Equipments> AllEquipments { get; private set; }
    public List<Fashions> AllFashions { get; private set; }
    public List<Foods> AllFoods { get; private set; }
    public List<Forges> AllForges { get; private set; }
    public List<Furnitures> AllFurnitures { get; private set; }
    public List<Items> AllItems { get; private set; }
    public List<MagicFormationCircles> AllMagicFormationCircles { get; private set; }
    public List<MechaBeasts> AllMechaBeasts { get; private set; }
    public List<Medals> AllMedals { get; private set; }
    public List<Pets> AllPets { get; private set; }
    public List<Plants> AllPlants { get; private set; }
    public List<Puppets> AllPuppets { get; private set; }
    public List<Relics> AllRelics { get; private set; }
    public List<Robots> AllRobots { get; private set; }
    public List<Runes> AllRunes { get; private set; }
    public List<Skills> AllSkills { get; private set; }
    public List<SpiritBeasts> AllSpiritBeasts { get; private set; }
    public List<SpiritCards> AllSpiritCards { get; private set; }
    public List<Symbols> AllSymbols { get; private set; }
    public List<Talismans> AllTalismans { get; private set; }
    public List<Technologies> AllTechnologies { get; private set; }
    public List<Titles> AllTitles { get; private set; }
    public List<Vehicles> AllVehicles { get; private set; }
    public List<Weapons> AllWeapons { get; private set; }
    public List<Outfits> AllOutfits { get; private set; }

    public Dictionary<string, List<Alchemies>> AlchemiesByType { get; private set; }
    public Dictionary<string, List<Artworks>> ArtworksByType { get; private set; }
    public Dictionary<string, List<Books>> BooksByType { get; private set; }
    public Dictionary<string, List<Buildings>> BuildingsByType { get; private set; }
    public Dictionary<string, List<CardAdmirals>> CardAdmiralsByType { get; private set; }
    public Dictionary<string, List<CardCaptains>> CardCaptainsByType { get; private set; }
    public Dictionary<string, List<CardColonels>> CardColonelsByType { get; private set; }
    public Dictionary<string, List<CardGenerals>> CardGeneralsByType { get; private set; }
    public Dictionary<string, List<CardHeroes>> CardHeroesByType { get; private set; }
    public Dictionary<string, List<CardLives>> CardLivesByType { get; private set; }
    public Dictionary<string, List<CardMilitaries>> CardMilitariesByType { get; private set; }
    public Dictionary<string, List<CardMonsters>> CardMonstersByType { get; private set; }
    public Dictionary<string, List<CardSoldiers>> CardSoldiersByType { get; private set; }
    public Dictionary<string, List<CardSpells>> CardSpellsByType { get; private set; }
    public Dictionary<string, List<CollaborationEquipments>> CollaborationEquipmentsByType { get; private set; }
    public Dictionary<string, List<Equipments>> EquipmentsByType { get; private set; }
    public Dictionary<string, List<Fashions>> FashionsByType { get; private set; }
    public Dictionary<string, List<Forges>> ForgesByType { get; private set; }
    public Dictionary<string, List<Furnitures>> FurnituresByType { get; private set; }
    public Dictionary<string, List<Items>> ItemsByType { get; private set; }
    public Dictionary<string, List<MagicFormationCircles>> MagicFormationCirclesByType { get; private set; }
    public Dictionary<string, List<Pets>> PetsByType { get; private set; }
    public Dictionary<string, List<Puppets>> PuppetsByType { get; private set; }
    public Dictionary<string, List<Relics>> RelicsByType { get; private set; }
    public Dictionary<string, List<Skills>> SkillsByType { get; private set; }
    public Dictionary<string, List<SpiritCards>> SpiritCardsByType { get; private set; }
    public Dictionary<string, List<Symbols>> SymbolsByType { get; private set; }
    public Dictionary<string, List<Talismans>> TalismansByType { get; private set; }
    public Dictionary<string, List<Vehicles>> VehiclesByType { get; private set; }
    public Dictionary<string, List<Weapons>> WeaponsByType { get; private set; }
    public Dictionary<string, List<Outfits>> OutfitsByType { get; private set; }

    public GameDataCacheConfig() { }

    public async Task LoadDataAsync()
    {
        LoadingManager.Instance.ShowLoading();

        var loadingTasks = new List<(string name, Func<Task> task)>
        {
            (AppDisplayConstants.MainType.ACHIEVEMENTS, async () => AllAchievements = await AchievementsService.Create().GetAchievementsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.ALCHEMIES, async () => AllAlchemies = await AlchemiesService.Create().GetAlchemiesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.ARCHITECTURES, async () => AllArchitectures = await ArchitecturesService.Create().GetArchitecturesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.ARTIFACTS, async () => AllArtifacts = await ArtifactsService.Create().GetArtifactsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.ARTWORKS, async () => AllArtworks = await ArtworksService.Create().GetArtworksWithoutLimitAsync()),
            (AppDisplayConstants.MainType.AVATARS, async () => AllAvatars = await AvatarsService.Create().GetAvatarsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.BADGES, async () => AllBadges = await BadgesService.Create().GetBadgesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.BEVERAGES, async () => AllBeverages = await BeveragesService.Create().GetBeveragesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.BOOKS, async () => AllBooks = await BooksService.Create().GetBooksWithoutLimitAsync()),
            (AppDisplayConstants.MainType.BORDERS, async () => AllBorders = await BordersService.Create().GetBordersWithoutLimitAsync()),
            (AppDisplayConstants.MainType.BUILDINGS, async () => AllBuildings = await BuildingsService.Create().GetBuildingsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.CARD_ADMIRALS, async () => AllCardAdmirals = await CardAdmiralsService.Create().GetCardAdmiralsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.CARD_CAPTAINS, async () => AllCardCaptains = await CardCaptainsService.Create().GetCardCaptainsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.CARD_COLONELS, async () => AllCardColonels = await CardColonelsService.Create().GetCardColonelsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.CARD_GENERALS, async () => AllCardGenerals = await CardGeneralsService.Create().GetCardGeneralsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.CARD_HEROES, async () => AllCardHeroes = await CardHeroesService.Create().GetCardHeroesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.CARD_LIVES, async () => AllCardLives = await CardLivesService.Create().GetCardLivesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.CARD_MILITARIES, async () => AllCardMilitaries = await CardMilitariesService.Create().GetCardMilitariesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.CARD_MONSTERS, async () => AllCardMonsters = await CardMonstersService.Create().GetCardMonstersWithoutLimitAsync()),
            (AppDisplayConstants.MainType.CARD_SOLDIERS, async () => AllCardSoldiers = await CardSoldiersService.Create().GetCardSoldiersWithoutLimitAsync()),
            (AppDisplayConstants.MainType.CARD_SPELLS, async () => AllCardSpells = await CardSpellsService.Create().GetCardSpellsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.COLLABORATION_EQUIPMENTS, async () => AllCollaborationEquipments = await CollaborationEquipmentsService.Create().GetCollaborationEquipmentsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.COLLABORATIONS, async () => AllCollaborations = await CollaborationsService.Create().GetCollaborationsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.CORES, async () => AllCores = await CoresService.Create().GetCoresWithoutLimitAsync()),
            (AppDisplayConstants.MainType.EMOJIS, async () => AllEmojis = await EmojisService.Create().GetEmojisWithoutLimitAsync()),
            (AppDisplayConstants.MainType.EQUIPMENTS, async () => AllEquipments = await EquipmentsService.Create().GetEquipmentsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.FASHIONS, async () => AllFashions = await FashionsService.Create().GetFashionsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.FOODS, async () => AllFoods = await FoodsService.Create().GetFoodsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.FORGES, async () => AllForges = await ForgesService.Create().GetForgesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.FURNITURES, async () => AllFurnitures = await FurnituresService.Create().GetFurnituresWithoutLimitAsync()),
            (AppDisplayConstants.MainType.ITEMS, async () => AllItems = await ItemsService.Create().GetItemsAsync()),
            (AppDisplayConstants.MainType.MAGIC_FORMATION_CIRCLES, async () => AllMagicFormationCircles = await MagicFormationCirclesService.Create().GetMagicFormationCirclesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.MECHA_BEASTS, async () => AllMechaBeasts = await MechaBeastsService.Create().GetMechaBeastsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.MEDALS, async () => AllMedals = await MedalsService.Create().GetMedalsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.PETS, async () => AllPets = await PetsService.Create().GetPetsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.PLANTS, async () => AllPlants = await PlantsService.Create().GetPlantsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.PUPPETS, async () => AllPuppets = await PuppetsService.Create().GetPuppetsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.RELICS, async () => AllRelics = await RelicsService.Create().GetRelicsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.ROBOTS, async () => AllRobots = await RobotsService.Create().GetRobotsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.RUNES, async () => AllRunes = await RunesService.Create().GetRunesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.SKILLS, async () => AllSkills = await SkillsService.Create().GetSkillsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.SPIRIT_BEASTS, async () => AllSpiritBeasts = await SpiritBeastsService.Create().GetSpiritBeastsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.SPIRIT_CARDS, async () => AllSpiritCards = await SpiritCardsService.Create().GetSpiritCardsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.SYMBOLS, async () => AllSymbols = await SymbolsService.Create().GetSymbolsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.TALISMANS, async () => AllTalismans = await TalismansService.Create().GetTalismansWithoutLimitAsync()),
            (AppDisplayConstants.MainType.TECHNOLOGIES, async () => AllTechnologies = await TechnologiesService.Create().GetTechnologiesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.TITLES, async () => AllTitles = await TitlesService.Create().GetTitlesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.VEHICLES, async () => AllVehicles = await VehiclesService.Create().GetVehiclesWithoutLimitAsync()),
            (AppDisplayConstants.MainType.WEAPONS, async () => AllWeapons = await WeaponsService.Create().GetWeaponsWithoutLimitAsync()),
            (AppDisplayConstants.MainType.OUTFITS, async () => AllOutfits = await OutfitsService.Create().GetOutfitsWithoutLimitAsync()),
        };

        int total = loadingTasks.Count;
        int completedCount = 0;

        // Sử dụng SemaphoreSlim để tránh spam quá nhiều request cùng 1 mili-giây nếu cần (ở đây chạy thẳng luôn)
        var tasks = loadingTasks.Select(async current =>
        {
            // Chạy task nạp dữ liệu
            await current.task();

            // Tăng số lượng task đã xong một cách an toàn (Thread-safe)
            int done = Interlocked.Increment(ref completedCount);
            float progress = (float)done / total;

            // Cập nhật giao diện (Unity yêu cầu chạy trên Main Thread, dùng Task.Run nếu cần, nhưng await đảm bảo điều này)
            LoadingManager.Instance.SetProgress(
                progress,
                $"{Mathf.RoundToInt(progress * 100)}%",
                LocalizationManager.Get(current.name) // Hiển thị tên cái vừa tải xong
            );
        });

        // Kích hoạt tất cả các tác vụ chạy song song và đợi toàn bộ hoàn thành
        await Task.WhenAll(tasks);

        CacheDictionaryType();

        LoadingManager.Instance.SetProgress(1f, $"{1f:P0}", "Completed");
        await Task.Delay(300);
        LoadingManager.Instance.HideLoading();
    }
    public async Task LoadDataFirst()
    {
        await PatternsService.Create().InitializeMasterDataAsync();
    }
    public void CacheDictionaryType()
    {
        // Cache dictionary để query nhanh
        AlchemiesByType = AllAlchemies
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        ArtworksByType = AllArtworks
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        BooksByType = AllBooks
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        BuildingsByType = AllBuildings
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        CardAdmiralsByType = AllCardAdmirals
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        CardCaptainsByType = AllCardCaptains
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        CardColonelsByType = AllCardColonels
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        CardGeneralsByType = AllCardGenerals
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        CardHeroesByType = AllCardHeroes
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        CardLivesByType = AllCardLives
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        CardMilitariesByType = AllCardMilitaries
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        CardMonstersByType = AllCardMonsters
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        CardSoldiersByType = AllCardSoldiers
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        CardSpellsByType = AllCardSpells
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        CollaborationEquipmentsByType = AllCollaborationEquipments
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        EquipmentsByType = AllEquipments
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        FashionsByType = AllFashions
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        ForgesByType = AllForges
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        FurnituresByType = AllFurnitures
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        ItemsByType = AllItems
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        MagicFormationCirclesByType = AllMagicFormationCircles
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        PetsByType = AllPets
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        PuppetsByType = AllPuppets
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        RelicsByType = AllRelics
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        SkillsByType = AllSkills
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        SpiritCardsByType = AllSpiritCards
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        SymbolsByType = AllSymbols
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        TalismansByType = AllTalismans
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        VehiclesByType = AllVehicles
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        WeaponsByType = AllWeapons
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());

        OutfitsByType = AllOutfits
            .Where(x => !string.IsNullOrEmpty(x.Type))
            .GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.ToList());
    }
}