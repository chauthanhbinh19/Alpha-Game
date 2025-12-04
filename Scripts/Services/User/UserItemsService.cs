using System.Collections.Generic;
using System.Threading.Tasks;

public class UserItemsService : IUserItemsService
{
    private readonly IUserItemsRepository _userItemsRepository;

    public UserItemsService(IUserItemsRepository userItemsRepository)
    {
        _userItemsRepository = userItemsRepository;
    }

    public static UserItemsService Create()
    {
        return new UserItemsService(new UserItemsRepository());
    }

    public async Task<List<Items>> GetUserItemsAsync(string user_id, string type, int pageSize, int offset)
    {
        return await _userItemsRepository.GetUserItemsAsync(user_id, type, pageSize, offset);
    }

    public async Task<int> GetUserItemsCountAsync(string user_id, string type)
    {
        return await _userItemsRepository.GetUserItemsCountAsync(user_id, type);
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
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.CARD_SPELL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.COLLABORATION:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_COLLABORATION));
                break;
            case AppConstants.MainType.CARD_MONSTER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
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
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.SPIRIT_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.ACHIEVEMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Experiment.EXP_ACHIEVEMENT));
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
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.BOOK:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_CAPTAIN:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.PET:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.COLLABORATION_EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_MILITARY:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_SPELL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.COLLABORATION:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_MONSTER:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.EQUIPMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.MEDAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.SKILL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.SYMBOL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.TITLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.MAGIC_FORMATION_CIRCLE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.RELIC:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_COLONEL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_GENERAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_ADMIRAL:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_LIFE:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.SPIRIT_BEAST:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.ACHIEVEMENT:
                items.Add(await GetUserItemByNameAsync(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
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
