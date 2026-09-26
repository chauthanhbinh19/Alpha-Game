using System;
using System.Collections.Generic;
using System.Threading.Tasks;


public class ShopsService : IShopsService
{
    private readonly IShopsRepository _shopsRepository;
    private static readonly Dictionary<string, (string Table, string Column, string CodeName)> ModuleMappings = new()
    {
        { AppConstants.ObjectType.ACHIEVEMENTS, (DataBaseConstants.Table.ACHIEVEMENTS, DataBaseConstants.Column.USER_ACHIEVEMENT_ID, "achievements") },
        { AppConstants.ObjectType.ALCHEMIES, (DataBaseConstants.Table.ALCHEMIES, DataBaseConstants.Column.USER_ALCHEMY_ID, "alchemies") },
        { AppConstants.ObjectType.ARCHITECTURES, (DataBaseConstants.Table.ARCHITECTURES, DataBaseConstants.Column.USER_ARCHITECTURE_ID, "architectures") },
        { AppConstants.ObjectType.ARTIFACTS, (DataBaseConstants.Table.ARTIFACTS, DataBaseConstants.Column.USER_ARTIFACT_ID, "artifacts") },
        { AppConstants.ObjectType.ARTWORKS, (DataBaseConstants.Table.ARTWORKS, DataBaseConstants.Column.USER_ARTWORK_ID, "artworks") },
        { AppConstants.ObjectType.AVATARS, (DataBaseConstants.Table.AVATARS, DataBaseConstants.Column.USER_AVATAR_ID, "avatars") },
        { AppConstants.ObjectType.BADGES, (DataBaseConstants.Table.BADGES, DataBaseConstants.Column.USER_BADGE_ID, "badges") },
        { AppConstants.ObjectType.BEVERAGES, (DataBaseConstants.Table.BEVERAGES, DataBaseConstants.Column.USER_BEVERAGE_ID, "beverages") },
        { AppConstants.ObjectType.BOOKS, (DataBaseConstants.Table.BOOKS, DataBaseConstants.Column.USER_BOOK_ID, "books") },
        { AppConstants.ObjectType.BORDERS, (DataBaseConstants.Table.BORDERS, DataBaseConstants.Column.USER_BORDER_ID, "borders") },
        { AppConstants.ObjectType.BUILDINGS, (DataBaseConstants.Table.BUILDINGS, DataBaseConstants.Column.USER_BUILDING_ID, "buildings") },
        { AppConstants.ObjectType.CARD_ADMIRALS, (DataBaseConstants.Table.CARD_ADMIRALS, DataBaseConstants.Column.USER_CARD_ADMIRAL_ID, "card_admirals") },
        { AppConstants.ObjectType.CARD_CAPTAINS, (DataBaseConstants.Table.CARD_CAPTAINS, DataBaseConstants.Column.USER_CARD_CAPTAIN_ID, "card_captains") },
        { AppConstants.ObjectType.CARD_COLONELS, (DataBaseConstants.Table.CARD_COLONELS, DataBaseConstants.Column.USER_CARD_COLONEL_ID, "card_colonels") },
        { AppConstants.ObjectType.CARD_GENERALS, (DataBaseConstants.Table.CARD_GENERALS, DataBaseConstants.Column.USER_CARD_GENERAL_ID, "card_generals") },
        { AppConstants.ObjectType.CARD_HEROES, (DataBaseConstants.Table.CARD_HEROES, DataBaseConstants.Column.USER_CARD_HERO_ID, "card_heroes") },
        { AppConstants.ObjectType.CARD_LIVES, (DataBaseConstants.Table.CARD_LIVES, DataBaseConstants.Column.USER_CARD_LIFE_ID, "card_lives") },
        { AppConstants.ObjectType.CARD_MILITARIES, (DataBaseConstants.Table.CARD_MILITARIES, DataBaseConstants.Column.USER_CARD_MILITARY_ID, "card_militaries") },
        { AppConstants.ObjectType.CARD_MONSTERS, (DataBaseConstants.Table.CARD_MONSTERS, DataBaseConstants.Column.USER_CARD_MONSTER_ID, "card_monsters") },
        { AppConstants.ObjectType.CARD_SOLDIERS, (DataBaseConstants.Table.CARD_SOLDIERS, DataBaseConstants.Column.USER_CARD_SOLDIER_ID, "card_soldiers") },
        { AppConstants.ObjectType.CARD_SPELLS, (DataBaseConstants.Table.CARD_SPELLS, DataBaseConstants.Column.USER_CARD_SPELL_ID, "card_spells") },
        { AppConstants.ObjectType.COLLABORATION_EQUIPMENTS, (DataBaseConstants.Table.COLLABORATION_EQUIPMENTS, DataBaseConstants.Column.USER_COLLABORATION_EQUIPMENT_ID, "collaboration_equipments") },
        { AppConstants.ObjectType.COLLABORATIONS, (DataBaseConstants.Table.COLLABORATIONS, DataBaseConstants.Column.USER_COLLABORATION_ID, "collaborations") },
        { AppConstants.ObjectType.CORES, (DataBaseConstants.Table.CORES, DataBaseConstants.Column.USER_CORE_ID, "cores") },
        { AppConstants.ObjectType.EMOJIS, (DataBaseConstants.Table.EMOJIS, DataBaseConstants.Column.USER_EMOJI_ID, "emojis") },
        { AppConstants.ObjectType.EQUIPMENTS, (DataBaseConstants.Table.EQUIPMENTS, DataBaseConstants.Column.USER_EQUIPMENT_ID, "equipments") },
        { AppConstants.ObjectType.FASHIONS, (DataBaseConstants.Table.FASHIONS, DataBaseConstants.Column.USER_FASHION_ID, "fashions") },
        { AppConstants.ObjectType.FOODS, (DataBaseConstants.Table.FOODS, DataBaseConstants.Column.USER_FOOD_ID, "foods") },
        { AppConstants.ObjectType.FORGES, (DataBaseConstants.Table.FORGES, DataBaseConstants.Column.USER_FORGE_ID, "forges") },
        { AppConstants.ObjectType.FURNITURES, (DataBaseConstants.Table.FURNITURES, DataBaseConstants.Column.USER_FURNITURE_ID, "furnitures") },
        { AppConstants.ObjectType.MAGIC_FORMATION_CIRCLES, (DataBaseConstants.Table.MAGIC_FORMATION_CIRCLES, DataBaseConstants.Column.USER_MFC_ID, "magic_formation_circles") },
        { AppConstants.ObjectType.MECHA_BEASTS, (DataBaseConstants.Table.MECHA_BEASTS, DataBaseConstants.Column.USER_MECHA_BEAST_ID, "mecha_beasts") },
        { AppConstants.ObjectType.MEDALS, (DataBaseConstants.Table.MEDALS, DataBaseConstants.Column.USER_MEDAL_ID, "medals") },
        { AppConstants.ObjectType.OUTFITS, (DataBaseConstants.Table.OUTFITS, DataBaseConstants.Column.USER_OUTFIT_ID, "outfits") },
        { AppConstants.ObjectType.PETS, (DataBaseConstants.Table.PETS, DataBaseConstants.Column.USER_PET_ID, "pets") },
        { AppConstants.ObjectType.PLANTS, (DataBaseConstants.Table.PLANTS, DataBaseConstants.Column.USER_PLANT_ID, "plants") },
        { AppConstants.ObjectType.PUPPETS, (DataBaseConstants.Table.PUPPETS, DataBaseConstants.Column.USER_PUPPET_ID, "puppets") },
        { AppConstants.ObjectType.RELICS, (DataBaseConstants.Table.RELICS, DataBaseConstants.Column.USER_RELIC_ID, "relics") },
        { AppConstants.ObjectType.ROBOTS, (DataBaseConstants.Table.ROBOTS, DataBaseConstants.Column.USER_ROBOT_ID, "robots") },
        { AppConstants.ObjectType.RUNES, (DataBaseConstants.Table.RUNES, DataBaseConstants.Column.USER_RUNE_ID, "runes") },
        { AppConstants.ObjectType.SKILLS, (DataBaseConstants.Table.SKILLS, DataBaseConstants.Column.USER_SKILL_ID, "skills") },
        { AppConstants.ObjectType.SPIRIT_BEASTS, (DataBaseConstants.Table.SPIRIT_BEASTS, DataBaseConstants.Column.USER_SPIRIT_BEAST_ID, "spirit_beasts") },
        { AppConstants.ObjectType.SPIRIT_CARDS, (DataBaseConstants.Table.SPIRIT_CARDS, DataBaseConstants.Column.USER_SPIRIT_CARD_ID, "spirit_cards") },
        { AppConstants.ObjectType.SYMBOLS, (DataBaseConstants.Table.SYMBOLS, DataBaseConstants.Column.USER_SYMBOL_ID, "symbols") },
        { AppConstants.ObjectType.TALISMANS, (DataBaseConstants.Table.TALISMANS, DataBaseConstants.Column.USER_TALISMAN_ID, "talismans") },
        { AppConstants.ObjectType.TECHNOLOGIES, (DataBaseConstants.Table.TECHNOLOGIES, DataBaseConstants.Column.USER_TECHNOLOGY_ID, "technologies") },
        { AppConstants.ObjectType.TITLES, (DataBaseConstants.Table.TITLES, DataBaseConstants.Column.USER_TITLE_ID, "titles") },
        { AppConstants.ObjectType.VEHICLES, (DataBaseConstants.Table.VEHICLES, DataBaseConstants.Column.USER_VEHICLE_ID, "vehicles") },
        { AppConstants.ObjectType.WEAPONS, (DataBaseConstants.Table.WEAPONS, DataBaseConstants.Column.USER_WEAPON_ID, "weapons") }
    };

    public ShopsService(IShopsRepository shopsRepository)
    {
        _shopsRepository = shopsRepository;
    }

    public static IShopsService Create() => ServiceContainer.GetService<IShopsService>();

    public async Task<List<string>> GetShopCodeNamesAsync(string shopType = null)
    {
        return await _shopsRepository.GetShopCodeNamesAsync(shopType);
    }

    public async Task<List<Currencies>> GetCurrenciesByShopAsync(string userId, ShopRequestDTO shopRequestDTO)
    {
        return await _shopsRepository.GetCurrenciesByShopAsync(userId, shopRequestDTO);
        // return new List<Currencies>();
    }

    public async Task<ShopDTO> GetShopsAsync(ShopRequestDTO shopRequestDTO)
    {
        if (!ModuleMappings.TryGetValue(shopRequestDTO.ObjectType, out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported object type: {shopRequestDTO.ObjectType}");
        }

        shopRequestDTO.ObjectTable = mapping.Table;

        return await _shopsRepository.GetShopsAsync(shopRequestDTO);
    }

    public async Task<ShopDTO> GetUserShopsAsync(string userId, ShopRequestDTO shopRequestDTO)
    {
        if (!ModuleMappings.TryGetValue(shopRequestDTO.ObjectType, out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported object type: {shopRequestDTO.ObjectType}");
        }

        shopRequestDTO.ObjectTable = mapping.Table;

        return await _shopsRepository.GetUserShopsAsync(userId, shopRequestDTO);
    }

    public async Task<int> GetShopItemCountAsync(ShopRequestDTO shopRequestDTO)
    {
        return await _shopsRepository.GetShopItemCountAsync(shopRequestDTO);
    }
}