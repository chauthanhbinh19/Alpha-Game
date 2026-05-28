using System.Collections.Generic;
using System.Threading.Tasks;

public class UserItemsService : IUserItemsService
{
     private static UserItemsService _instance;
    private readonly IUserItemsRepository _userItemsRepository;

    public UserItemsService(IUserItemsRepository userItemsRepository)
    {
        _userItemsRepository = userItemsRepository;
    }

    public static UserItemsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserItemsService(new UserItemsRepository());
        }
        return _instance;
    }

    public async Task<List<Items>> GetUserItemsAsync(string user_id, string search, string type, int pageSize, int offset)
    {
        return await _userItemsRepository.GetUserItemsAsync(user_id, search, type, pageSize, offset);
    }

    public async Task<int> GetUserItemsCountAsync(string user_id, string search, string type)
    {
        return await _userItemsRepository.GetUserItemsCountAsync(user_id, search, type);
    }
    public async Task<Items> GetUserItemByNameAsync(string itemName)
    {
        return await _userItemsRepository.GetUserItemByNameAsync(itemName);
    }

    public async Task<bool> InsertUserItemAsync(Items item, double quantity)
    {
        return await _userItemsRepository.InsertUserItemAsync(item, quantity);
    }

    public async Task<Items> UpdateUserItemQuantityAsync(Items item)
    {
        return await _userItemsRepository.UpdateUserItemQuantityAsync(item);
    }
    public async Task<Items> UpdateUserItemQuantityAsync(Items item, double quantity)
    {
        return await _userItemsRepository.UpdateUserItemQuantityAsync(item, quantity);
    }
    public async Task<List<Items>> GetItemForLevelAsync(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case AppConstants.MainType.CARD_HERO:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.BOOK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOOKS));
                break;
            case AppConstants.MainType.AVATAR:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_AVATARS));
                break;
            case AppConstants.MainType.BORDER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BORDERS));
                break;
            case AppConstants.MainType.CARD_CAPTAIN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.PET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_PETS));
                break;
            case AppConstants.MainType.COLLABORATION_EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_COLLABORATION_EQUIPMENTS));
                break;
            case AppConstants.MainType.CARD_MILITARY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_CARD_MILITARY));
                break;
            case AppConstants.MainType.CARD_SPELL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_CARD_SPELLS));
                break;
            case AppConstants.MainType.COLLABORATION:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_COLLABORATIONS));
                break;
            case AppConstants.MainType.CARD_MONSTER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_CARD_MONSTERS));
                break;
            case AppConstants.MainType.EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_EQUIPMENTS));
                break;
            case AppConstants.MainType.MEDAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_MEDALS));
                break;
            case AppConstants.MainType.SKILL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_SKILLS));
                break;
            case AppConstants.MainType.SYMBOL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_SYMBOLS));
                break;
            case AppConstants.MainType.TITLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_TITLES));
                break;
            case AppConstants.MainType.MAGIC_FORMATION_CIRCLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_MAGIC_FORMATION_CIRCLES));
                break;
            case AppConstants.MainType.RELIC:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_RELICS));
                break;
            case AppConstants.MainType.CARD_COLONEL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.CARD_GENERAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.CARD_ADMIRAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.CARD_LIFE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_CARD_LIVES));
                break;
            case AppConstants.MainType.SPIRIT_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_SPIRIT_BEASTS));
                break;
            case AppConstants.MainType.ACHIEVEMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_ACHIEVEMENTS));
                break;
            case AppConstants.MainType.ALCHEMY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_ALCHEMIES));
                break;
            case AppConstants.MainType.PUPPET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_PUPPETS));
                break;
            case AppConstants.MainType.TALISMAN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_TALISMANS));
                break;
            case AppConstants.MainType.FORGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_FORGES));
                break;
            case AppConstants.MainType.ARCHITECTURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_ARCHITECTURES));
                break;
            case AppConstants.MainType.ARTWORK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_ARTWORKS));
                break;
            case AppConstants.MainType.BADGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BADGES));
                break;
            case AppConstants.MainType.FOOD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_FOODS));
                break;
            case AppConstants.MainType.BEVERAGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BEVERAGES));
                break;
            case AppConstants.MainType.BUILDING:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BUILDINGS));
                break;
            case AppConstants.MainType.ARTIFACT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_ARTIFACTS));
                break;
            case AppConstants.MainType.CORE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_CORES));
                break;
            case AppConstants.MainType.FURNITURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_FURNITURES));
                break;
            case AppConstants.MainType.MECHA_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_MECHA_BEASTS));
                break;
            case AppConstants.MainType.PLANT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_PLANTS));
                break;
            case AppConstants.MainType.ROBOT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_ROBOTS));
                break;
            case AppConstants.MainType.RUNE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_RUNES));
                break;
            case AppConstants.MainType.SPIRIT_CARD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_SPIRIT_CARDS));
                break;
            case AppConstants.MainType.TECHNOLOGY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_TECHNOLOGIES));
                break;
            case AppConstants.MainType.VEHICLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_VEHICLES));
                break;
            case AppConstants.MainType.WEAPON:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_WEAPONS));
                break;
            case AppConstants.MainType.EMOJI:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_EMOJIS));
                break;
            case AppConstants.MainType.CARD_SOLDIER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_CARD_SOLDIERS));
                break;
            default:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
        }
        return items;
    }
    public async Task<List<Items>> GetItemForBreakthourghAsync(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case AppConstants.MainType.CARD_HERO:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_CARD_HEROES));
                break;
            case AppConstants.MainType.BOOK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_BOOKS));
                break;
            case AppConstants.MainType.CARD_CAPTAIN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_CARD_CAPTAINS));
                break;
            case AppConstants.MainType.PET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_PETS));
                break;
            case AppConstants.MainType.COLLABORATION_EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_COLLABORATION_EQUIPMENTS));
                break;
            case AppConstants.MainType.CARD_MILITARY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_CARD_MILITARIES));
                break;
            case AppConstants.MainType.CARD_SPELL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_CARD_SPELLS));
                break;
            case AppConstants.MainType.COLLABORATION:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_COLLABORATIONS));
                break;
            case AppConstants.MainType.CARD_MONSTER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_CARD_MONSTERS));
                break;
            case AppConstants.MainType.EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_EQUIPMENTS));
                break;
            case AppConstants.MainType.MEDAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_MEDALS));
                break;
            case AppConstants.MainType.SKILL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_SKILLS));
                break;
            case AppConstants.MainType.SYMBOL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_SYMBOLS));
                break;
            case AppConstants.MainType.TITLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_TITLES));
                break;
            case AppConstants.MainType.MAGIC_FORMATION_CIRCLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_MAGIC_FORMATION_CIRCLES));
                break;
            case AppConstants.MainType.RELIC:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_RELICS));
                break;
            case AppConstants.MainType.CARD_COLONEL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_CARD_COLONELS));
                break;
            case AppConstants.MainType.CARD_GENERAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_CARD_GENERALS));
                break;
            case AppConstants.MainType.CARD_ADMIRAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_CARD_ADMIRALS));
                break;
            case AppConstants.MainType.CARD_LIFE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_CARD_LIVES));
                break;
            case AppConstants.MainType.SPIRIT_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_SPIRIT_BEASTS));
                break;
            case AppConstants.MainType.SPIRIT_CARD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_SPIRIT_CARDS));
                break;
            case AppConstants.MainType.ACHIEVEMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_ACHIEVEMENTS));
                break;
            case AppConstants.MainType.AVATAR:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_AVATARS));
                break;
            case AppConstants.MainType.BORDER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_BORDERS));
                break;
            case AppConstants.MainType.ALCHEMY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_ALCHEMIES));
                break;
            case AppConstants.MainType.PUPPET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_PUPPETS));
                break;
            case AppConstants.MainType.TALISMAN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_TALISMANS));
                break;
            case AppConstants.MainType.FORGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_FORGES));
                break;
            case AppConstants.MainType.ARCHITECTURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_ARCHITECTURES));
                break;
            case AppConstants.MainType.ARTWORK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_ARTWORKS));
                break;
            case AppConstants.MainType.BADGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_BADGES));
                break;
            case AppConstants.MainType.BEVERAGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_BEVERAGES));
                break;
            case AppConstants.MainType.FOOD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_FOODS));
                break;
            case AppConstants.MainType.BUILDING:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_BUILDINGS));
                break;
            case AppConstants.MainType.ARTIFACT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_ARTIFACTS));
                break;
            case AppConstants.MainType.CORE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_CORES));
                break;
            case AppConstants.MainType.FURNITURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_FURNITURES));
                break;
            case AppConstants.MainType.MECHA_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_MECHA_BEASTS));
                break;
            case AppConstants.MainType.PLANT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_PLANTS));
                break;
            case AppConstants.MainType.ROBOT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_ROBOTS));
                break;
            case AppConstants.MainType.RUNE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_RUNES));
                break;
            case AppConstants.MainType.TECHNOLOGY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_TECHNOLOGIES));
                break;
            case AppConstants.MainType.VEHICLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_VEHICLES));
                break;
            case AppConstants.MainType.WEAPON:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_WEAPONS));
                break;
            case AppConstants.MainType.EMOJI:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_EMOJIS));
                break;
            case AppConstants.MainType.CARD_SOLDIER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_CARD_SOLDIERS));
                break;
            default:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
        }
        return items;
    }
    public async Task<List<Items>> GetItemForAwakeningAsync(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case AppConstants.MainType.CARD_HERO:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_CARD_HEROES));
                break;
            case AppConstants.MainType.BOOK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_BOOKS));
                break;
            case AppConstants.MainType.CARD_CAPTAIN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_CARD_CAPTAINS));
                break;
            case AppConstants.MainType.PET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_PETS));
                break;
            case AppConstants.MainType.COLLABORATION_EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_COLLABORATION_EQUIPMENTS));
                break;
            case AppConstants.MainType.CARD_MILITARY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_CARD_MILITARIES));
                break;
            case AppConstants.MainType.CARD_SPELL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_CARD_SPELLS));
                break;
            case AppConstants.MainType.COLLABORATION:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_COLLABORATIONS));
                break;
            case AppConstants.MainType.CARD_MONSTER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_CARD_MONSTERS));
                break;
            case AppConstants.MainType.EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_EQUIPMENTS));
                break;
            case AppConstants.MainType.MEDAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_MEDALS));
                break;
            case AppConstants.MainType.SKILL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_SKILLS));
                break;
            case AppConstants.MainType.SYMBOL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_SYMBOLS));
                break;
            case AppConstants.MainType.TITLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_TITLES));
                break;
            case AppConstants.MainType.MAGIC_FORMATION_CIRCLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_MAGIC_FORMATION_CIRCLES));
                break;
            case AppConstants.MainType.RELIC:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_RELICS));
                break;
            case AppConstants.MainType.CARD_COLONEL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_CARD_COLONELS));
                break;
            case AppConstants.MainType.CARD_GENERAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_CARD_GENERALS));
                break;
            case AppConstants.MainType.CARD_ADMIRAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_CARD_ADMIRALS));
                break;
            case AppConstants.MainType.CARD_LIFE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_CARD_LIVES));
                break;
            case AppConstants.MainType.SPIRIT_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_SPIRIT_BEASTS));
                break;
            case AppConstants.MainType.SPIRIT_CARD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_SPIRIT_CARDS));
                break;
            case AppConstants.MainType.ACHIEVEMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_ACHIEVEMENTS));
                break;
            case AppConstants.MainType.AVATAR:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_AVATARS));
                break;
            case AppConstants.MainType.BORDER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_BORDERS));
                break;
            case AppConstants.MainType.ALCHEMY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_ALCHEMIES));
                break;
            case AppConstants.MainType.PUPPET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_PUPPETS));
                break;
            case AppConstants.MainType.TALISMAN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_TALISMANS));
                break;
            case AppConstants.MainType.FORGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_FORGES));
                break;
            case AppConstants.MainType.ARCHITECTURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_ARCHITECTURES));
                break;
            case AppConstants.MainType.ARTWORK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_ARTWORKS));
                break;
            case AppConstants.MainType.BADGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_BADGES));
                break;
            case AppConstants.MainType.BEVERAGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_BEVERAGES));
                break;
            case AppConstants.MainType.FOOD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_FOODS));
                break;
            case AppConstants.MainType.BUILDING:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_BUILDINGS));
                break;
            case AppConstants.MainType.ARTIFACT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_ARTIFACTS));
                break;
            case AppConstants.MainType.CORE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_CORES));
                break;
            case AppConstants.MainType.FURNITURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_FURNITURES));
                break;
            case AppConstants.MainType.MECHA_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_MECHA_BEASTS));
                break;
            case AppConstants.MainType.PLANT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_PLANTS));
                break;
            case AppConstants.MainType.ROBOT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_ROBOTS));
                break;
            case AppConstants.MainType.RUNE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_RUNES));
                break;
            case AppConstants.MainType.TECHNOLOGY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_TECHNOLOGIES));
                break;
            case AppConstants.MainType.VEHICLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_VEHICLES));
                break;
            case AppConstants.MainType.WEAPON:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_WEAPONS));
                break;
            case AppConstants.MainType.EMOJI:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_EMOJIS));
                break;
            case AppConstants.MainType.CARD_SOLDIER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_CARD_SOLDIERS));
                break;
            default:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Awakening.AWAKENING_CARD_HEROES));
                break;
        }
        return items;
    }
    public async Task<List<Items>> GetItemForAscensionAsync(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case AppConstants.MainType.CARD_HERO:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_CARD_HEROES));
                break;
            case AppConstants.MainType.BOOK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_BOOKS));
                break;
            case AppConstants.MainType.CARD_CAPTAIN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_CARD_CAPTAINS));
                break;
            case AppConstants.MainType.PET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_PETS));
                break;
            case AppConstants.MainType.COLLABORATION_EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_COLLABORATION_EQUIPMENTS));
                break;
            case AppConstants.MainType.CARD_MILITARY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_CARD_MILITARIES));
                break;
            case AppConstants.MainType.CARD_SPELL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_CARD_SPELLS));
                break;
            case AppConstants.MainType.COLLABORATION:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_COLLABORATIONS));
                break;
            case AppConstants.MainType.CARD_MONSTER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_CARD_MONSTERS));
                break;
            case AppConstants.MainType.EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_EQUIPMENTS));
                break;
            case AppConstants.MainType.MEDAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_MEDALS));
                break;
            case AppConstants.MainType.SKILL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_SKILLS));
                break;
            case AppConstants.MainType.SYMBOL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_SYMBOLS));
                break;
            case AppConstants.MainType.TITLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_TITLES));
                break;
            case AppConstants.MainType.MAGIC_FORMATION_CIRCLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_MAGIC_FORMATION_CIRCLES));
                break;
            case AppConstants.MainType.RELIC:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_RELICS));
                break;
            case AppConstants.MainType.CARD_COLONEL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_CARD_COLONELS));
                break;
            case AppConstants.MainType.CARD_GENERAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_CARD_GENERALS));
                break;
            case AppConstants.MainType.CARD_ADMIRAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_CARD_ADMIRALS));
                break;
            case AppConstants.MainType.CARD_LIFE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_CARD_LIVES));
                break;
            case AppConstants.MainType.SPIRIT_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_SPIRIT_BEASTS));
                break;
            case AppConstants.MainType.SPIRIT_CARD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_SPIRIT_CARDS));
                break;
            case AppConstants.MainType.ACHIEVEMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_ACHIEVEMENTS));
                break;
            case AppConstants.MainType.AVATAR:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_AVATARS));
                break;
            case AppConstants.MainType.BORDER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_BORDERS));
                break;
            case AppConstants.MainType.ALCHEMY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_ALCHEMIES));
                break;
            case AppConstants.MainType.PUPPET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_PUPPETS));
                break;
            case AppConstants.MainType.TALISMAN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_TALISMANS));
                break;
            case AppConstants.MainType.FORGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_FORGES));
                break;
            case AppConstants.MainType.ARCHITECTURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_ARCHITECTURES));
                break;
            case AppConstants.MainType.ARTWORK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_ARTWORKS));
                break;
            case AppConstants.MainType.BADGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_BADGES));
                break;
            case AppConstants.MainType.BEVERAGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_BEVERAGES));
                break;
            case AppConstants.MainType.FOOD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_FOODS));
                break;
            case AppConstants.MainType.BUILDING:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_BUILDINGS));
                break;
            case AppConstants.MainType.ARTIFACT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_ARTIFACTS));
                break;
            case AppConstants.MainType.CORE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_CORES));
                break;
            case AppConstants.MainType.FURNITURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_FURNITURES));
                break;
            case AppConstants.MainType.MECHA_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_MECHA_BEASTS));
                break;
            case AppConstants.MainType.PLANT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_PLANTS));
                break;
            case AppConstants.MainType.ROBOT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_ROBOTS));
                break;
            case AppConstants.MainType.RUNE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_RUNES));
                break;
            case AppConstants.MainType.TECHNOLOGY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_TECHNOLOGIES));
                break;
            case AppConstants.MainType.VEHICLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_VEHICLES));
                break;
            case AppConstants.MainType.WEAPON:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_WEAPONS));
                break;
            case AppConstants.MainType.EMOJI:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_EMOJIS));
                break;
            case AppConstants.MainType.CARD_SOLDIER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_CARD_SOLDIERS));
                break;
            default:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Ascension.ASCENSION_CARD_HEROES));
                break;
        }
        return items;
    }
    public async Task<List<Items>> GetItemForResonanceAsync(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case AppConstants.MainType.CARD_HERO:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_CARD_HEROES));
                break;
            case AppConstants.MainType.BOOK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_BOOKS));
                break;
            case AppConstants.MainType.CARD_CAPTAIN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_CARD_CAPTAINS));
                break;
            case AppConstants.MainType.PET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_PETS));
                break;
            case AppConstants.MainType.COLLABORATION_EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_COLLABORATION_EQUIPMENTS));
                break;
            case AppConstants.MainType.CARD_MILITARY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_CARD_MILITARIES));
                break;
            case AppConstants.MainType.CARD_SPELL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_CARD_SPELLS));
                break;
            case AppConstants.MainType.COLLABORATION:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_COLLABORATIONS));
                break;
            case AppConstants.MainType.CARD_MONSTER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_CARD_MONSTERS));
                break;
            case AppConstants.MainType.EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_EQUIPMENTS));
                break;
            case AppConstants.MainType.MEDAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_MEDALS));
                break;
            case AppConstants.MainType.SKILL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_SKILLS));
                break;
            case AppConstants.MainType.SYMBOL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_SYMBOLS));
                break;
            case AppConstants.MainType.TITLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_TITLES));
                break;
            case AppConstants.MainType.MAGIC_FORMATION_CIRCLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_MAGIC_FORMATION_CIRCLES));
                break;
            case AppConstants.MainType.RELIC:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_RELICS));
                break;
            case AppConstants.MainType.CARD_COLONEL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_CARD_COLONELS));
                break;
            case AppConstants.MainType.CARD_GENERAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_CARD_GENERALS));
                break;
            case AppConstants.MainType.CARD_ADMIRAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_CARD_ADMIRALS));
                break;
            case AppConstants.MainType.CARD_LIFE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_CARD_LIVES));
                break;
            case AppConstants.MainType.SPIRIT_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_SPIRIT_BEASTS));
                break;
            case AppConstants.MainType.SPIRIT_CARD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_SPIRIT_CARDS));
                break;
            case AppConstants.MainType.ACHIEVEMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_ACHIEVEMENTS));
                break;
            case AppConstants.MainType.AVATAR:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_AVATARS));
                break;
            case AppConstants.MainType.BORDER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_BORDERS));
                break;
            case AppConstants.MainType.ALCHEMY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_ALCHEMIES));
                break;
            case AppConstants.MainType.PUPPET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_PUPPETS));
                break;
            case AppConstants.MainType.TALISMAN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_TALISMANS));
                break;
            case AppConstants.MainType.FORGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_FORGES));
                break;
            case AppConstants.MainType.ARCHITECTURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_ARCHITECTURES));
                break;
            case AppConstants.MainType.ARTWORK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_ARTWORKS));
                break;
            case AppConstants.MainType.BADGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_BADGES));
                break;
            case AppConstants.MainType.BEVERAGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_BEVERAGES));
                break;
            case AppConstants.MainType.FOOD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_FOODS));
                break;
            case AppConstants.MainType.BUILDING:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_BUILDINGS));
                break;
            case AppConstants.MainType.ARTIFACT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_ARTIFACTS));
                break;
            case AppConstants.MainType.CORE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_CORES));
                break;
            case AppConstants.MainType.FURNITURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_FURNITURES));
                break;
            case AppConstants.MainType.MECHA_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_MECHA_BEASTS));
                break;
            case AppConstants.MainType.PLANT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_PLANTS));
                break;
            case AppConstants.MainType.ROBOT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_ROBOTS));
                break;
            case AppConstants.MainType.RUNE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_RUNES));
                break;
            case AppConstants.MainType.TECHNOLOGY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_TECHNOLOGIES));
                break;
            case AppConstants.MainType.VEHICLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_VEHICLES));
                break;
            case AppConstants.MainType.WEAPON:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_WEAPONS));
                break;
            case AppConstants.MainType.EMOJI:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_EMOJIS));
                break;
            case AppConstants.MainType.CARD_SOLDIER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_CARD_SOLDIERS));
                break;
            default:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Resonance.RESONANCE_CARD_HEROES));
                break;
        }
        return items;
    }
    public async Task<List<Items>> GetItemForEnhancementAsync(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case AppConstants.MainType.CARD_HERO:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_CARD_HEROES));
                break;
            case AppConstants.MainType.BOOK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_BOOKS));
                break;
            case AppConstants.MainType.CARD_CAPTAIN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_CARD_CAPTAINS));
                break;
            case AppConstants.MainType.PET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_PETS));
                break;
            case AppConstants.MainType.COLLABORATION_EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_COLLABORATION_EQUIPMENTS));
                break;
            case AppConstants.MainType.CARD_MILITARY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_CARD_MILITARIES));
                break;
            case AppConstants.MainType.CARD_SPELL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_CARD_SPELLS));
                break;
            case AppConstants.MainType.COLLABORATION:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_COLLABORATIONS));
                break;
            case AppConstants.MainType.CARD_MONSTER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_CARD_MONSTERS));
                break;
            case AppConstants.MainType.EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_EQUIPMENTS));
                break;
            case AppConstants.MainType.MEDAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_MEDALS));
                break;
            case AppConstants.MainType.SKILL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_SKILLS));
                break;
            case AppConstants.MainType.SYMBOL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_SYMBOLS));
                break;
            case AppConstants.MainType.TITLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_TITLES));
                break;
            case AppConstants.MainType.MAGIC_FORMATION_CIRCLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_MAGIC_FORMATION_CIRCLES));
                break;
            case AppConstants.MainType.RELIC:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_RELICS));
                break;
            case AppConstants.MainType.CARD_COLONEL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_CARD_COLONELS));
                break;
            case AppConstants.MainType.CARD_GENERAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_CARD_GENERALS));
                break;
            case AppConstants.MainType.CARD_ADMIRAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_CARD_ADMIRALS));
                break;
            case AppConstants.MainType.CARD_LIFE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_CARD_LIVES));
                break;
            case AppConstants.MainType.SPIRIT_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_SPIRIT_BEASTS));
                break;
            case AppConstants.MainType.SPIRIT_CARD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_SPIRIT_CARDS));
                break;
            case AppConstants.MainType.ACHIEVEMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_ACHIEVEMENTS));
                break;
            case AppConstants.MainType.AVATAR:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_AVATARS));
                break;
            case AppConstants.MainType.BORDER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_BORDERS));
                break;
            case AppConstants.MainType.ALCHEMY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_ALCHEMIES));
                break;
            case AppConstants.MainType.PUPPET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_PUPPETS));
                break;
            case AppConstants.MainType.TALISMAN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_TALISMANS));
                break;
            case AppConstants.MainType.FORGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_FORGES));
                break;
            case AppConstants.MainType.ARCHITECTURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_ARCHITECTURES));
                break;
            case AppConstants.MainType.ARTWORK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_ARTWORKS));
                break;
            case AppConstants.MainType.BADGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_BADGES));
                break;
            case AppConstants.MainType.BEVERAGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_BEVERAGES));
                break;
            case AppConstants.MainType.FOOD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_FOODS));
                break;
            case AppConstants.MainType.BUILDING:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_BUILDINGS));
                break;
            case AppConstants.MainType.ARTIFACT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_ARTIFACTS));
                break;
            case AppConstants.MainType.CORE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_CORES));
                break;
            case AppConstants.MainType.FURNITURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_FURNITURES));
                break;
            case AppConstants.MainType.MECHA_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_MECHA_BEASTS));
                break;
            case AppConstants.MainType.PLANT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_PLANTS));
                break;
            case AppConstants.MainType.ROBOT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_ROBOTS));
                break;
            case AppConstants.MainType.RUNE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_RUNES));
                break;
            case AppConstants.MainType.TECHNOLOGY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_TECHNOLOGIES));
                break;
            case AppConstants.MainType.VEHICLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_VEHICLES));
                break;
            case AppConstants.MainType.WEAPON:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_WEAPONS));
                break;
            case AppConstants.MainType.EMOJI:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_EMOJIS));
                break;
            case AppConstants.MainType.CARD_SOLDIER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_CARD_SOLDIERS));
                break;
            default:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Enhancement.ENHANCEMENT_CARD_HEROES));
                break;
        }
        return items;
    }
    public async Task<List<Items>> GetItemForRankAsync(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case "Affinity":
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_1));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_2));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_3));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_4));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_5));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_6));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_7));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_8));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_9));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_10));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_11));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_12));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_14));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_15));
                break;
            default:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Affinity.AFFINITY_NUMBER_1));
                break;
        }
        return items;
    }

    public async Task<bool> InsertOrUpdateUserItemsBatchAsync(List<(Items item, double quantity)> items)
    {
        return await _userItemsRepository.InsertOrUpdateUserItemsBatchAsync(items);
    }

    public async Task<bool> InsertOrUpdateUserItemQuantityAsync(string userId, Items item, double quantity)
    {
        return await _userItemsRepository.InsertOrUpdateUserItemQuantityAsync(userId, item, quantity);
    }
}
