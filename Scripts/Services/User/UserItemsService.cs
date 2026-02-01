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

    public async Task<bool> InsertUserItemAsync(Items items, double quantity)
    {
        return await _userItemsRepository.InsertUserItemAsync(items, quantity);
    }

    public async Task<Items> UpdateUserItemQuantityAsync(Items items)
    {
        return await _userItemsRepository.UpdateUserItemQuantityAsync(items);
    }
    public async Task<Items> UpdateUserItemQuantityAsync(Items items, double quantity)
    {
        return await _userItemsRepository.UpdateUserItemQuantityAsync(items, quantity);
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
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOOK));
                break;
            case AppConstants.MainType.AVATAR:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_AVATAR));
                break;
            case AppConstants.MainType.BORDER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BORDER));
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
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_PET));
                break;
            case AppConstants.MainType.COLLABORATION_EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_COLLABORATION_EQUIPMENT));
                break;
            case AppConstants.MainType.CARD_MILITARY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_CARD_MILITARY));
                break;
            case AppConstants.MainType.CARD_SPELL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_CARD_SPELL));
                break;
            case AppConstants.MainType.COLLABORATION:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_COLLABORATION));
                break;
            case AppConstants.MainType.CARD_MONSTER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_CARD_MONSTER));
                break;
            case AppConstants.MainType.EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_EQUIPMENT));
                break;
            case AppConstants.MainType.MEDAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_MEDAL));
                break;
            case AppConstants.MainType.SKILL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_SKILL));
                break;
            case AppConstants.MainType.SYMBOL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_SYMBOL));
                break;
            case AppConstants.MainType.TITLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_TITLE));
                break;
            case AppConstants.MainType.MAGIC_FORMATION_CIRCLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_MAGIC_FORMATION_CIRCLE));
                break;
            case AppConstants.MainType.RELIC:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_RELIC));
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
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_CARD_LIFE));
                break;
            case AppConstants.MainType.SPIRIT_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_SPIRIT_BEAST));
                break;
            case AppConstants.MainType.ACHIEVEMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_ACHIEVEMENT));
                break;
            case AppConstants.MainType.ALCHEMY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_ALCHEMY));
                break;
            case AppConstants.MainType.PUPPET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_PUPPET));
                break;
            case AppConstants.MainType.TALISMAN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_TALISMAN));
                break;
            case AppConstants.MainType.FORGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_FORGE));
                break;
            case AppConstants.MainType.ARCHITECTURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_ARCHITECTURE));
                break;
            case AppConstants.MainType.ARTWORK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_ARTWORK));
                break;
            case AppConstants.MainType.BADGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BADGE));
                break;
            case AppConstants.MainType.FOOD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_FOOD));
                break;
            case AppConstants.MainType.BEVERAGE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BEVERAGE));
                break;
            case AppConstants.MainType.BUILDING:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BUILDING));
                break;
            case AppConstants.MainType.CARD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_CARD));
                break;
            case AppConstants.MainType.CORE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_CORE));
                break;
            case AppConstants.MainType.FURNITURE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_FURNITURE));
                break;
            case AppConstants.MainType.MECHA_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_MECHA_BEAST));
                break;
            case AppConstants.MainType.PLANT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_PLANT));
                break;
            case AppConstants.MainType.ROBOT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_ROBOT));
                break;
            case AppConstants.MainType.RUNE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_RUNE));
                break;
            case AppConstants.MainType.SPIRIT_CARD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_SPIRIT_CARD));
                break;
            case AppConstants.MainType.TECHNOLOGY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_TECHNOLOGY));
                break;
            case AppConstants.MainType.VEHICLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_VEHICLE));
                break;
            case AppConstants.MainType.WEAPON:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_WEAPON));
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
            case AppConstants.MainType.CARD:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.LIMIT_BREAK_CARDS));
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
            default:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
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
}
