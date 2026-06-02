using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FeaturesService : IFeaturesService
{
    private static FeaturesService _instance;
    private readonly IFeaturesRepository _featuresRepository;

    private static readonly Dictionary<Type, (string Table, string Column, string CodeName)> ModuleMappings = new()
    {
        { typeof(Achievements), ("user_achievements_module", "user_achievement_id", "achievements") },
        { typeof(Alchemies), ("user_alchemies_module", "user_alchemy_id", "alchemies") },
        { typeof(Architectures), ("user_architectures_module", "user_architecture_id", "architectures") },
        { typeof(Artifacts), ("user_artifacts_module", "user_artifact_id", "artifacts") },
        { typeof(Artworks), ("user_artworks_module", "user_artwork_id", "artworks") },
        { typeof(Avatars), ("user_avatars_module", "user_avatar_id", "avatars") },
        { typeof(Badges), ("user_badges_module", "user_badge_id", "badges") },
        { typeof(Beverages), ("user_beverages_module", "user_beverage_id", "beverages") },
        { typeof(Books), ("user_books_module", "user_book_id", "books") },
        { typeof(Borders), ("user_borders_module", "user_border_id", "borders") },
        { typeof(Buildings), ("user_buildings_module", "user_building_id", "buildings") },
        { typeof(CardAdmirals), ("user_card_admirals_module", "user_card_admiral_id", "card_admirals") },
        { typeof(CardCaptains), ("user_card_captains_module", "user_card_captain_id", "card_captains") },
        { typeof(CardColonels), ("user_card_colonels_module", "user_card_colonel_id", "card_colonels") },
        { typeof(CardGenerals), ("user_card_generals_module", "user_card_general_id", "card_generals") },
        { typeof(CardHeroes), ("user_card_heroes_module", "user_card_hero_id", "card_heroes") },
        { typeof(CardLives), ("user_card_lives_module", "user_card_life_id", "card_lives") },
        { typeof(CardMilitaries), ("user_card_militaries_module", "user_card_military_id", "card_militaries") },
        { typeof(CardMonsters), ("user_card_monsters_module", "user_card_monster_id", "card_monsters") },
        { typeof(CardSoldiers), ("user_card_soldiers_module", "user_card_soldier_id", "card_soldiers") },
        { typeof(CardSpells), ("user_card_spells_module", "user_card_spell_id", "card_spells") },
        { typeof(CollaborationEquipments), ("user_collaboration_equipments_module", "user_collaboration_equipment_id", "collaboration_equipments") },
        { typeof(Collaborations), ("user_collaborations_module", "user_collaboration_id", "collaborations") },
        { typeof(Cores), ("user_cores_module", "user_core_id", "cores") },
        { typeof(Emojis), ("user_emojis_module", "user_emoji_id", "emojis") },
        { typeof(Equipments), ("user_equipments_module", "user_equipment_id", "equipments") },
        { typeof(Fashions), ("user_fashions_module", "user_fashion_id", "fashions") },
        { typeof(Foods), ("user_foods_module", "user_food_id", "foods") },
        { typeof(Forges), ("user_forges_module", "user_forge_id", "forges") },
        { typeof(Furnitures), ("user_furnitures_module", "user_furniture_id", "furnitures") },
        { typeof(MagicFormationCircles), ("user_magic_formation_circles_module", "user_mfc_id", "magic_formation_circles") },
        { typeof(MechaBeasts), ("user_mecha_beasts_module", "user_mecha_beast_id", "mecha_beasts") },
        { typeof(Medals), ("user_medals_module", "user_medal_id", "medals") },
        { typeof(Pets), ("user_pets_module", "user_pet_id", "pets") },
        { typeof(Plants), ("user_plants_module", "user_plant_id", "plants") },
        { typeof(Puppets), ("user_puppets_module", "user_puppet_id", "puppets") },
        { typeof(Relics), ("user_relics_module", "user_relic_id", "relics") },
        { typeof(Robots), ("user_robots_module", "user_robot_id", "robots") },
        { typeof(Runes), ("user_runes_module", "user_rune_id", "runes") },
        { typeof(Skills), ("user_skills_module", "user_skill_id", "skills") },
        { typeof(SpiritBeasts), ("user_spirit_beasts_module", "user_spirit_beast_id", "spirit_beasts") },
        { typeof(SpiritCards), ("user_spirit_cards_module", "user_spirit_card_id", "spirit_cards") },
        { typeof(Symbols), ("user_symbols_module", "user_symbol_id", "symbols") },
        { typeof(Talismans), ("user_talismans_module", "user_talisman_id", "talismans") },
        { typeof(Technologies), ("user_technologies_module", "user_technology_id", "technologies") },
        { typeof(Titles), ("user_titles_module", "user_title_id", "titles") },
        { typeof(Vehicles), ("user_vehicles_module", "user_vehicle_id", "vehicles") },
        { typeof(Weapons), ("user_weapons_module", "user_weapon_id", "weapons") },
        { typeof(Outfits), ("user_outfits_module", "user_outfit_id", "outfits") }
    };

    private static readonly Dictionary<Type, (string Table, string Column, string CodeName)> UpgradeMappings = new()
    {
        { typeof(Achievements), ("user_achievements_upgrade", "user_achievement_id", "achievements") },
        { typeof(Alchemies), ("user_alchemies_upgrade", "user_alchemy_id", "alchemies") },
        { typeof(Architectures), ("user_architectures_upgrade", "user_architecture_id", "architectures") },
        { typeof(Artifacts), ("user_artifacts_upgrade", "user_artifact_id", "artifacts") },
        { typeof(Artworks), ("user_artworks_upgrade", "user_artwork_id", "artworks") },
        { typeof(Avatars), ("user_avatars_upgrade", "user_avatar_id", "avatars") },
        { typeof(Badges), ("user_badges_upgrade", "user_badge_id", "badges") },
        { typeof(Beverages), ("user_beverages_upgrade", "user_beverage_id", "beverages") },
        { typeof(Books), ("user_books_upgrade", "user_book_id", "books") },
        { typeof(Borders), ("user_borders_upgrade", "user_border_id", "borders") },
        { typeof(Buildings), ("user_buildings_upgrade", "user_building_id", "buildings") },
        { typeof(CardAdmirals), ("user_card_admirals_upgrade", "user_card_admiral_id", "card_admirals") },
        { typeof(CardCaptains), ("user_card_captains_upgrade", "user_card_captain_id", "card_captains") },
        { typeof(CardColonels), ("user_card_colonels_upgrade", "user_card_colonel_id", "card_colonels") },
        { typeof(CardGenerals), ("user_card_generals_upgrade", "user_card_general_id", "card_generals") },
        { typeof(CardHeroes), ("user_card_heroes_upgrade", "user_card_hero_id", "card_heroes") },
        { typeof(CardLives), ("user_card_lives_upgrade", "user_card_life_id", "card_lives") },
        { typeof(CardMilitaries), ("user_card_militaries_upgrade", "user_card_military_id", "card_militaries") },
        { typeof(CardMonsters), ("user_card_monsters_upgrade", "user_card_monster_id", "card_monsters") },
        { typeof(CardSoldiers), ("user_card_soldiers_upgrade", "user_card_soldier_id", "card_soldiers") },
        { typeof(CardSpells), ("user_card_spells_upgrade", "user_card_spell_id", "card_spells") },
        { typeof(CollaborationEquipments), ("user_collaboration_equipments_upgrade", "user_collaboration_equipment_id", "collaboration_equipments") },
        { typeof(Collaborations), ("user_collaborations_upgrade", "user_collaboration_id", "collaborations") },
        { typeof(Cores), ("user_cores_upgrade", "user_core_id", "cores") },
        { typeof(Emojis), ("user_emojis_upgrade", "user_emoji_id", "emojis") },
        { typeof(Equipments), ("user_equipments_upgrade", "user_equipment_id", "equipments") },
        { typeof(Fashions), ("user_fashions_upgrade", "user_fashion_id", "fashions") },
        { typeof(Foods), ("user_foods_upgrade", "user_food_id", "foods") },
        { typeof(Forges), ("user_forges_upgrade", "user_forge_id", "forges") },
        { typeof(Furnitures), ("user_furnitures_upgrade", "user_furniture_id", "furnitures") },
        { typeof(MagicFormationCircles), ("user_magic_formation_circles_upgrade", "user_mfc_id", "magic_formation_circles") },
        { typeof(MechaBeasts), ("user_mecha_beasts_upgrade", "user_mecha_beast_id", "mecha_beasts") },
        { typeof(Medals), ("user_medals_upgrade", "user_medal_id", "medals") },
        { typeof(Pets), ("user_pets_upgrade", "user_pet_id", "pets") },
        { typeof(Plants), ("user_plants_upgrade", "user_plant_id", "plants") },
        { typeof(Puppets), ("user_puppets_upgrade", "user_puppet_id", "puppets") },
        { typeof(Relics), ("user_relics_upgrade", "user_relic_id", "relics") },
        { typeof(Robots), ("user_robots_upgrade", "user_robot_id", "robots") },
        { typeof(Runes), ("user_runes_upgrade", "user_rune_id", "runes") },
        { typeof(Skills), ("user_skills_upgrade", "user_skill_id", "skills") },
        { typeof(SpiritBeasts), ("user_spirit_beasts_upgrade", "user_spirit_beast_id", "spirit_beasts") },
        { typeof(SpiritCards), ("user_spirit_cards_upgrade", "user_spirit_card_id", "spirit_cards") },
        { typeof(Symbols), ("user_symbols_upgrade", "user_symbol_id", "symbols") },
        { typeof(Talismans), ("user_talismans_upgrade", "user_talisman_id", "talismans") },
        { typeof(Technologies), ("user_technologies_upgrade", "user_technology_id", "technologies") },
        { typeof(Titles), ("user_titles_upgrade", "user_title_id", "titles") },
        { typeof(Vehicles), ("user_vehicles_upgrade", "user_vehicle_id", "vehicles") },
        { typeof(Weapons), ("user_weapons_upgrade", "user_weapon_id", "weapons") },
        { typeof(Outfits), ("user_outfits_upgrade", "user_outfit_id", "outfits") }
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
