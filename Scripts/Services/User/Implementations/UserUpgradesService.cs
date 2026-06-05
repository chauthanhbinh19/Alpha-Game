using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public class UserUpgradesService : IUserUpgradesService
{
    private static UserUpgradesService _instance;
    private readonly IUserUpgradesRepository _userUpgradesRepository;
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

    public UserUpgradesService(IUserUpgradesRepository userUpgradesRepository)
    {
        _userUpgradesRepository = userUpgradesRepository;
    }

    public static UserUpgradesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserUpgradesService(new UserUpgradesRepository());
        }
        return _instance;
    }

    public async Task<UserUpgrades> GetUserUpgradesAsync(string upgradeId, IStats stat)
    {
        if (!UpgradeMappings.TryGetValue(stat.GetType(), out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported stat type: {stat.GetType().Name}");
        }
        return await _userUpgradesRepository.GetUserUpgradesAsync(upgradeId, mapping.Table, mapping.Column);
    }

    public async Task<UserUpgrades> GetSumUserUpgradesAsync(string userId, IStats stat)
    {
        if (!UpgradeMappings.TryGetValue(stat.GetType(), out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported stat type: {stat.GetType().Name}");
        }
        return await _userUpgradesRepository.GetSumUserUpgradesAsync(userId, stat.Id, mapping.Table, mapping.Column);
    }

    public async Task InsertOrUpdateUserUpgradesAsync(string userId, UserUpgrades upgrade, IStats stat)
    {
        if (!UpgradeMappings.TryGetValue(stat.GetType(), out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported stat type: {stat.GetType().Name}");
        }
        await _userUpgradesRepository.InsertOrUpdateUserUpgradesAsync(userId, upgrade, stat.Id, mapping.Table, mapping.Column);
    }

}