using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FeaturesService : IFeaturesService
{
    private static FeaturesService _instance;
    private readonly IFeaturesRepository _featuresRepository;

    private static readonly Dictionary<Type, (string Table, string Column, string CodeName)> ModuleMappings = new()
    {
        { typeof(Achievements), (DataBaseConstants.Table.USER_ACHIEVEMENTS_MODULE, DataBaseConstants.Column.USER_ACHIEVEMENT_ID, "achievements") },
        { typeof(Alchemies), (DataBaseConstants.Table.USER_ALCHEMIES_MODULE, DataBaseConstants.Column.USER_ALCHEMY_ID, "alchemies") },
        { typeof(Architectures), (DataBaseConstants.Table.USER_ARCHITECTURES_MODULE, DataBaseConstants.Column.USER_ARCHITECTURE_ID, "architectures") },
        { typeof(Artifacts), (DataBaseConstants.Table.USER_ARTIFACTS_MODULE, DataBaseConstants.Column.USER_ARTIFACT_ID, "artifacts") },
        { typeof(Artworks), (DataBaseConstants.Table.USER_ARTWORKS_MODULE, DataBaseConstants.Column.USER_ARTWORK_ID, "artworks") },
        { typeof(Avatars), (DataBaseConstants.Table.USER_AVATARS_MODULE, DataBaseConstants.Column.USER_AVATAR_ID, "avatars") },
        { typeof(Badges), (DataBaseConstants.Table.USER_BADGES_MODULE, DataBaseConstants.Column.USER_BADGE_ID, "badges") },
        { typeof(Beverages), (DataBaseConstants.Table.USER_BEVERAGES_MODULE, DataBaseConstants.Column.USER_BEVERAGE_ID, "beverages") },
        { typeof(Books), (DataBaseConstants.Table.USER_BOOKS_MODULE, DataBaseConstants.Column.USER_BOOK_ID, "books") },
        { typeof(Borders), (DataBaseConstants.Table.USER_BORDERS_MODULE, DataBaseConstants.Column.USER_BORDER_ID, "borders") },
        { typeof(Buildings), (DataBaseConstants.Table.USER_BUILDINGS_MODULE, DataBaseConstants.Column.USER_BUILDING_ID, "buildings") },
        { typeof(CardAdmirals), (DataBaseConstants.Table.USER_CARD_ADMIRALS_MODULE, DataBaseConstants.Column.USER_CARD_ADMIRAL_ID, "card_admirals") },
        { typeof(CardCaptains), (DataBaseConstants.Table.USER_CARD_CAPTAINS_MODULE, DataBaseConstants.Column.USER_CARD_CAPTAIN_ID, "card_captains") },
        { typeof(CardColonels), (DataBaseConstants.Table.USER_CARD_COLONELS_MODULE, DataBaseConstants.Column.USER_CARD_COLONEL_ID, "card_colonels") },
        { typeof(CardGenerals), (DataBaseConstants.Table.USER_CARD_GENERALS_MODULE, DataBaseConstants.Column.USER_CARD_GENERAL_ID, "card_generals") },
        { typeof(CardHeroes), (DataBaseConstants.Table.USER_CARD_HEROES_MODULE, DataBaseConstants.Column.USER_CARD_HERO_ID, "card_heroes") },
        { typeof(CardLives), (DataBaseConstants.Table.USER_CARD_LIVES_MODULE, DataBaseConstants.Column.USER_CARD_LIFE_ID, "card_lives") },
        { typeof(CardMilitaries), (DataBaseConstants.Table.USER_CARD_MILITARIES_MODULE, DataBaseConstants.Column.USER_CARD_MILITARY_ID, "card_militaries") },
        { typeof(CardMonsters), (DataBaseConstants.Table.USER_CARD_MONSTERS_MODULE, DataBaseConstants.Column.USER_CARD_MONSTER_ID, "card_monsters") },
        { typeof(CardSoldiers), (DataBaseConstants.Table.USER_CARD_SOLDIERS_MODULE, DataBaseConstants.Column.USER_CARD_SOLDIER_ID, "card_soldiers") },
        { typeof(CardSpells), (DataBaseConstants.Table.USER_CARD_SPELLS_MODULE, DataBaseConstants.Column.USER_CARD_SPELL_ID, "card_spells") },
        { typeof(CollaborationEquipments), (DataBaseConstants.Table.USER_COLLABORATION_EQUIPMENTS_MODULE, DataBaseConstants.Column.USER_COLLABORATION_EQUIPMENT_ID, "collaboration_equipments") },
        { typeof(Collaborations), (DataBaseConstants.Table.USER_COLLABORATIONS_MODULE, DataBaseConstants.Column.USER_COLLABORATION_ID, "collaborations") },
        { typeof(Cores), (DataBaseConstants.Table.USER_CORES_MODULE, DataBaseConstants.Column.USER_CORE_ID, "cores") },
        { typeof(Emojis), (DataBaseConstants.Table.USER_EMOJIS_MODULE, DataBaseConstants.Column.USER_EMOJI_ID, "emojis") },
        { typeof(Equipments), (DataBaseConstants.Table.USER_EQUIPMENTS_MODULE, DataBaseConstants.Column.USER_EQUIPMENT_ID, "equipments") },
        { typeof(Fashions), (DataBaseConstants.Table.USER_FASHIONS_MODULE, DataBaseConstants.Column.USER_FASHION_ID, "fashions") },
        { typeof(Foods), (DataBaseConstants.Table.USER_FOODS_MODULE, DataBaseConstants.Column.USER_FOOD_ID, "foods") },
        { typeof(Forges), (DataBaseConstants.Table.USER_FORGES_MODULE, DataBaseConstants.Column.USER_FORGE_ID, "forges") },
        { typeof(Furnitures), (DataBaseConstants.Table.USER_FURNITURES_MODULE, DataBaseConstants.Column.USER_FURNITURE_ID, "furnitures") },
        { typeof(MagicFormationCircles), (DataBaseConstants.Table.USER_MAGIC_FORMATION_CIRCLES_MODULE, DataBaseConstants.Column.USER_MFC_ID, "magic_formation_circles") },
        { typeof(MechaBeasts), (DataBaseConstants.Table.USER_MECHA_BEASTS_MODULE, DataBaseConstants.Column.USER_MECHA_BEAST_ID, "mecha_beasts") },
        { typeof(Medals), (DataBaseConstants.Table.USER_MEDALS_MODULE, DataBaseConstants.Column.USER_MEDAL_ID, "medals") },
        { typeof(Pets), (DataBaseConstants.Table.USER_PETS_MODULE, DataBaseConstants.Column.USER_PET_ID, "pets") },
        { typeof(Plants), (DataBaseConstants.Table.USER_PLANTS_MODULE, DataBaseConstants.Column.USER_PLANT_ID, "plants") },
        { typeof(Puppets), (DataBaseConstants.Table.USER_PUPPETS_MODULE, DataBaseConstants.Column.USER_PUPPET_ID, "puppets") },
        { typeof(Relics), (DataBaseConstants.Table.USER_RELICS_MODULE, DataBaseConstants.Column.USER_RELIC_ID, "relics") },
        { typeof(Robots), (DataBaseConstants.Table.USER_ROBOTS_MODULE, DataBaseConstants.Column.USER_ROBOT_ID, "robots") },
        { typeof(Runes), (DataBaseConstants.Table.USER_RUNES_MODULE, DataBaseConstants.Column.USER_RUNE_ID, "runes") },
        { typeof(Skills), (DataBaseConstants.Table.USER_SKILLS_MODULE, DataBaseConstants.Column.USER_SKILL_ID, "skills") },
        { typeof(SpiritBeasts), (DataBaseConstants.Table.USER_SPIRIT_BEASTS_MODULE, DataBaseConstants.Column.USER_SPIRIT_BEAST_ID, "spirit_beasts") },
        { typeof(SpiritCards), (DataBaseConstants.Table.USER_SPIRIT_CARDS_MODULE, DataBaseConstants.Column.USER_SPIRIT_CARD_ID, "spirit_cards") },
        { typeof(Symbols), (DataBaseConstants.Table.USER_SYMBOLS_MODULE, DataBaseConstants.Column.USER_SYMBOL_ID, "symbols") },
        { typeof(Talismans), (DataBaseConstants.Table.USER_TALISMANS_MODULE, DataBaseConstants.Column.USER_TALISMAN_ID, "talismans") },
        { typeof(Technologies), (DataBaseConstants.Table.USER_TECHNOLOGIES_MODULE, DataBaseConstants.Column.USER_TECHNOLOGY_ID, "technologies") },
        { typeof(Titles), (DataBaseConstants.Table.USER_TITLES_MODULE, DataBaseConstants.Column.USER_TITLE_ID, "titles") },
        { typeof(Vehicles), (DataBaseConstants.Table.USER_VEHICLES_MODULE, DataBaseConstants.Column.USER_VEHICLE_ID, "vehicles") },
        { typeof(Weapons), (DataBaseConstants.Table.USER_WEAPONS_MODULE, DataBaseConstants.Column.USER_WEAPON_ID, "weapons") },
        { typeof(Outfits), (DataBaseConstants.Table.USER_OUTFITS_MODULE, DataBaseConstants.Column.USER_OUTFIT_ID, "outfits") }
    };

    private static readonly Dictionary<Type, (string Table, string Column, string CodeName)> UpgradeMappings = new()
    {
        { typeof(Achievements), (DataBaseConstants.Table.USER_ACHIEVEMENTS_UPGRADE, DataBaseConstants.Column.USER_ACHIEVEMENT_ID, "achievements") },
        { typeof(Alchemies), (DataBaseConstants.Table.USER_ALCHEMIES_UPGRADE, DataBaseConstants.Column.USER_ALCHEMY_ID, "alchemies") },
        { typeof(Architectures), (DataBaseConstants.Table.USER_ARCHITECTURES_UPGRADE, DataBaseConstants.Column.USER_ARCHITECTURE_ID, "architectures") },
        { typeof(Artifacts), (DataBaseConstants.Table.USER_ARTIFACTS_UPGRADE, DataBaseConstants.Column.USER_ARTIFACT_ID, "artifacts") },
        { typeof(Artworks), (DataBaseConstants.Table.USER_ARTWORKS_UPGRADE, DataBaseConstants.Column.USER_ARTWORK_ID, "artworks") },
        { typeof(Avatars), (DataBaseConstants.Table.USER_AVATARS_UPGRADE, DataBaseConstants.Column.USER_AVATAR_ID, "avatars") },
        { typeof(Badges), (DataBaseConstants.Table.USER_BADGES_UPGRADE, DataBaseConstants.Column.USER_BADGE_ID, "badges") },
        { typeof(Beverages), (DataBaseConstants.Table.USER_BEVERAGES_UPGRADE, DataBaseConstants.Column.USER_BEVERAGE_ID, "beverages") },
        { typeof(Books), (DataBaseConstants.Table.USER_BOOKS_UPGRADE, DataBaseConstants.Column.USER_BOOK_ID, "books") },
        { typeof(Borders), (DataBaseConstants.Table.USER_BORDERS_UPGRADE, DataBaseConstants.Column.USER_BORDER_ID, "borders") },
        { typeof(Buildings), (DataBaseConstants.Table.USER_BUILDINGS_UPGRADE, DataBaseConstants.Column.USER_BUILDING_ID, "buildings") },
        { typeof(CardAdmirals), (DataBaseConstants.Table.USER_CARD_ADMIRALS_UPGRADE, DataBaseConstants.Column.USER_CARD_ADMIRAL_ID, "card_admirals") },
        { typeof(CardCaptains), (DataBaseConstants.Table.USER_CARD_CAPTAINS_UPGRADE, DataBaseConstants.Column.USER_CARD_CAPTAIN_ID, "card_captains") },
        { typeof(CardColonels), (DataBaseConstants.Table.USER_CARD_COLONELS_UPGRADE, DataBaseConstants.Column.USER_CARD_COLONEL_ID, "card_colonels") },
        { typeof(CardGenerals), (DataBaseConstants.Table.USER_CARD_GENERALS_UPGRADE, DataBaseConstants.Column.USER_CARD_GENERAL_ID, "card_generals") },
        { typeof(CardHeroes), (DataBaseConstants.Table.USER_CARD_HEROES_UPGRADE, DataBaseConstants.Column.USER_CARD_HERO_ID, "card_heroes") },
        { typeof(CardLives), (DataBaseConstants.Table.USER_CARD_LIVES_UPGRADE, DataBaseConstants.Column.USER_CARD_LIFE_ID, "card_lives") },
        { typeof(CardMilitaries), (DataBaseConstants.Table.USER_CARD_MILITARIES_UPGRADE, DataBaseConstants.Column.USER_CARD_MILITARY_ID, "card_militaries") },
        { typeof(CardMonsters), (DataBaseConstants.Table.USER_CARD_MONSTERS_UPGRADE, DataBaseConstants.Column.USER_CARD_MONSTER_ID, "card_monsters") },
        { typeof(CardSoldiers), (DataBaseConstants.Table.USER_CARD_SOLDIERS_UPGRADE, DataBaseConstants.Column.USER_CARD_SOLDIER_ID, "card_soldiers") },
        { typeof(CardSpells), (DataBaseConstants.Table.USER_CARD_SPELLS_UPGRADE, DataBaseConstants.Column.USER_CARD_SPELL_ID, "card_spells") },
        { typeof(CollaborationEquipments), (DataBaseConstants.Table.USER_COLLABORATION_EQUIPMENTS_UPGRADE, DataBaseConstants.Column.USER_COLLABORATION_EQUIPMENT_ID, "collaboration_equipments") },
        { typeof(Collaborations), (DataBaseConstants.Table.USER_COLLABORATIONS_UPGRADE, DataBaseConstants.Column.USER_COLLABORATION_ID, "collaborations") },
        { typeof(Cores), (DataBaseConstants.Table.USER_CORES_UPGRADE, DataBaseConstants.Column.USER_CORE_ID, "cores") },
        { typeof(Emojis), (DataBaseConstants.Table.USER_EMOJIS_UPGRADE, DataBaseConstants.Column.USER_EMOJI_ID, "emojis") },
        { typeof(Equipments), (DataBaseConstants.Table.USER_EQUIPMENTS_UPGRADE, DataBaseConstants.Column.USER_EQUIPMENT_ID, "equipments") },
        { typeof(Fashions), (DataBaseConstants.Table.USER_FASHIONS_UPGRADE, DataBaseConstants.Column.USER_FASHION_ID, "fashions") },
        { typeof(Foods), (DataBaseConstants.Table.USER_FOODS_UPGRADE, DataBaseConstants.Column.USER_FOOD_ID, "foods") },
        { typeof(Forges), (DataBaseConstants.Table.USER_FORGES_UPGRADE, DataBaseConstants.Column.USER_FORGE_ID, "forges") },
        { typeof(Furnitures), (DataBaseConstants.Table.USER_FURNITURES_UPGRADE, DataBaseConstants.Column.USER_FURNITURE_ID, "furnitures") },
        { typeof(MagicFormationCircles), (DataBaseConstants.Table.USER_MAGIC_FORMATION_CIRCLES_UPGRADE, DataBaseConstants.Column.USER_MFC_ID, "magic_formation_circles") },
        { typeof(MechaBeasts), (DataBaseConstants.Table.USER_MECHA_BEASTS_UPGRADE, DataBaseConstants.Column.USER_MECHA_BEAST_ID, "mecha_beasts") },
        { typeof(Medals), (DataBaseConstants.Table.USER_MEDALS_UPGRADE, DataBaseConstants.Column.USER_MEDAL_ID, "medals") },
        { typeof(Pets), (DataBaseConstants.Table.USER_PETS_UPGRADE, DataBaseConstants.Column.USER_PET_ID, "pets") },
        { typeof(Plants), (DataBaseConstants.Table.USER_PLANTS_UPGRADE, DataBaseConstants.Column.USER_PLANT_ID, "plants") },
        { typeof(Puppets), (DataBaseConstants.Table.USER_PUPPETS_UPGRADE, DataBaseConstants.Column.USER_PUPPET_ID, "puppets") },
        { typeof(Relics), (DataBaseConstants.Table.USER_RELICS_UPGRADE, DataBaseConstants.Column.USER_RELIC_ID, "relics") },
        { typeof(Robots), (DataBaseConstants.Table.USER_ROBOTS_UPGRADE, DataBaseConstants.Column.USER_ROBOT_ID, "robots") },
        { typeof(Runes), (DataBaseConstants.Table.USER_RUNES_UPGRADE, DataBaseConstants.Column.USER_RUNE_ID, "runes") },
        { typeof(Skills), (DataBaseConstants.Table.USER_SKILLS_UPGRADE, DataBaseConstants.Column.USER_SKILL_ID, "skills") },
        { typeof(SpiritBeasts), (DataBaseConstants.Table.USER_SPIRIT_BEASTS_UPGRADE, DataBaseConstants.Column.USER_SPIRIT_BEAST_ID, "spirit_beasts") },
        { typeof(SpiritCards), (DataBaseConstants.Table.USER_SPIRIT_CARDS_UPGRADE, DataBaseConstants.Column.USER_SPIRIT_CARD_ID, "spirit_cards") },
        { typeof(Symbols), (DataBaseConstants.Table.USER_SYMBOLS_UPGRADE, DataBaseConstants.Column.USER_SYMBOL_ID, "symbols") },
        { typeof(Talismans), (DataBaseConstants.Table.USER_TALISMANS_UPGRADE, DataBaseConstants.Column.USER_TALISMAN_ID, "talismans") },
        { typeof(Technologies), (DataBaseConstants.Table.USER_TECHNOLOGIES_UPGRADE, DataBaseConstants.Column.USER_TECHNOLOGY_ID, "technologies") },
        { typeof(Titles), (DataBaseConstants.Table.USER_TITLES_UPGRADE, DataBaseConstants.Column.USER_TITLE_ID, "titles") },
        { typeof(Vehicles), (DataBaseConstants.Table.USER_VEHICLES_UPGRADE, DataBaseConstants.Column.USER_VEHICLE_ID, "vehicles") },
        { typeof(Weapons), (DataBaseConstants.Table.USER_WEAPONS_UPGRADE, DataBaseConstants.Column.USER_WEAPON_ID, "weapons") },
        { typeof(Outfits), (DataBaseConstants.Table.USER_OUTFITS_UPGRADE, DataBaseConstants.Column.USER_OUTFIT_ID, "outfits") }
    };

    public FeaturesService(IFeaturesRepository featuresRepository)
    {
        _featuresRepository = featuresRepository;
    }

    public static FeaturesService Create()
    {
        if (_instance == null)
        {
            _instance = new FeaturesService(new FeaturesRepository());
        }
        return _instance;
    }

    public async Task<Dictionary<string, Features>> GetFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, Features>> GetAnimeFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetAnimeFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureScienceFictionDTO>> GetScienceFictionFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetScienceFictionFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureResearchDTO>> GetResearchFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetResearchFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureArchiveDTO>> GetArchiveFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetArchiveFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureUniverseDTO>> GetUniverseFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetUniverseFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHIINDTO>> GetHIINFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHIINFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureSSWNDTO>> GetSSWNFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetSSWNFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHITNDTO>> GetHITNFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHITNFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHIHNDTO>> GetHIHNFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHIHNFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHIENDTO>> GetHIENFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHIENFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHICADTO>> GetHICAFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHICAFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHIRNDTO>> GetHIRNFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHIRNFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHIDCDTO>> GetHIDCFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHIDCFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHICBDTO>> GetHICBFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHICBFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureHISNDTO>> GetHISNFeaturesByTypeAsync(string type)
    {
        return await _featuresRepository.GetHISNFeaturesByTypeAsync(type);
    }

    public async Task<Dictionary<string, FeatureModuleDTO>> GetModuleFeaturesByTypeAsync(string type, IStats stat)
    {
        if (!ModuleMappings.TryGetValue(stat.GetType(), out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported stat type: {stat.GetType().Name}");
        }

        string prefix = type switch
        {
            "Module Breakthrough" => "breakthrough",
            "Module Awakening" => "awakening",
            "Module Ascension" => "ascension",
            "Module Resonance" => "resonance",
            "Module Enhancement" => "enhancement",
            "Module Refinement" => "refinement",
            _ => throw new ArgumentException($"Unknown module type: {type}")
        };
        string featureCodeName = $"{prefix}_{mapping.CodeName}";

        return await _featuresRepository.GetModuleFeaturesByTypeAsync(stat.Id, type, featureCodeName, mapping.Table, mapping.Column);
    }

    public async Task<Dictionary<string, FeatureUpgradeDTO>> GetUpgradeFeaturesByTypeAsync(string type, IStats stat)
    {
        if (!UpgradeMappings.TryGetValue(stat.GetType(), out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported stat type: {stat.GetType().Name}");
        }

        string prefix = type switch
        {
            "Upgrade Breakthrough" => "breakthrough",
            "Upgrade Awakening" => "awakening",
            "Upgrade Ascension" => "ascension",
            "Upgrade Resonance" => "resonance",
            "Upgrade Enhancement" => "enhancement",
            "Upgrade Refinement" => "refinement",
            _ => throw new ArgumentException($"Unknown module type: {type}")
        };
        string featureCodeName = $"{prefix}_{mapping.CodeName}";

        return await _featuresRepository.GetUpgradeFeaturesByTypeAsync(stat.Id, type, featureCodeName, mapping.Table, mapping.Column);
    }

    // Implement other methods from IFeaturesService by calling the repository
}
