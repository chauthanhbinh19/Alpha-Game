using System.Collections.Generic;

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

    public List<Items> GetUserItems(string user_id, string type, int pageSize, int offset)
    {
        return _userItemsRepository.GetUserItems(user_id, type, pageSize, offset);
    }

    public int GetUserItemCount(string user_id, string type)
    {
        return _userItemsRepository.GetUserItemCount(user_id, type);
    }
    public Items GetUserItemByName(string itemName)
    {
        return _userItemsRepository.GetUserItemByName(itemName);
    }

    public bool InsertUserItems(Items items, int quantity)
    {
        return _userItemsRepository.InsertUserItems(items, quantity);
    }

    public Items UpdateUserItemsQuantity(Items items)
    {
        return _userItemsRepository.UpdateUserItemsQuantity(items);
    }
    public Items UpdateUserItemsQuantity(Items items, int quantity)
    {
        return _userItemsRepository.UpdateUserItemsQuantity(items, quantity);
    }
    public List<Items> GetItemForLevel(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case AppConstants.MainType.CARD_HERO:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.BOOK:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOOK));
                break;
            case AppConstants.MainType.CARD_CAPTAIN:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.PET:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_PET));
                break;
            case AppConstants.MainType.COLLABORATION_EQUIPMENT:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_COLLABORATION_EQUIPMENT));
                break;
            case AppConstants.MainType.CARD_MILITARY:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.CARD_SPELL:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.COLLABORATION:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_COLLABORATION));
                break;
            case AppConstants.MainType.CARD_MONSTER:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.EQUIPMENT:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_EQUIPMENT));
                break;
            case AppConstants.MainType.MEDAL:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_MEDAL));
                break;
            case AppConstants.MainType.SKILL:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_SKILL));
                break;
            case AppConstants.MainType.SYMBOL:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_SYMBOL));
                break;
            case AppConstants.MainType.TITLE:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_TITLE));
                break;
            case AppConstants.MainType.MAGIC_FORMATION_CIRCLE:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_MAGIC_FORMATION_CIRCLE));
                break;
            case AppConstants.MainType.RELIC:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_RELIC));
                break;
            case AppConstants.MainType.CARD_COLONEL:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.CARD_GENERAL:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.CARD_ADMIRAL:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.CARD_LIFE:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.SPIRIT_BEAST:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
            case AppConstants.MainType.ACHIEVEMENT:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_ACHIEVEMENT));
                break;
            default:
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.EXP_BOTTOLE_LV6));
                break;
        }
        return items;
    }
    public List<Items> GetItemForBreakthourgh(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case AppConstants.MainType.CARD_HERO:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.BOOK:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_CAPTAIN:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.PET:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.COLLABORATION_EQUIPMENT:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_MILITARY:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_SPELL:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.COLLABORATION:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_MONSTER:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.EQUIPMENT:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.MEDAL:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.SKILL:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.SYMBOL:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.TITLE:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.MAGIC_FORMATION_CIRCLE:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.RELIC:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_COLONEL:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_GENERAL:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_ADMIRAL:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.CARD_LIFE:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.SPIRIT_BEAST:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            case AppConstants.MainType.ACHIEVEMENT:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
            default:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BREAK_THROUGH_TOKEN));
                break;
        }
        return items;
    }
    public List<Items> GetItemForRank(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case "Affinity":
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_1));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_2));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_3));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_4));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_5));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_6));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_7));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_8));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_9));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_10));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_11));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_12));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_14));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_15));
                break;
            default:
                items.Add(GetUserItemByName(ItemConstants.Affinity.AFFINITY_NUMBER_1));
                break;
        }
        return items;
    }
}
