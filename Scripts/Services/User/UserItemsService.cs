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
            case AppConstants.MainType.CardHero:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv6));
                break;
            case AppConstants.MainType.Book:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBook));
                break;
            case AppConstants.MainType.CardCaptain:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv6));
                break;
            case AppConstants.MainType.Pet:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpPet));
                break;
            case AppConstants.MainType.CollaborationEquipment:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpCollaborationEquipment));
                break;
            case AppConstants.MainType.CardMilitary:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv6));
                break;
            case AppConstants.MainType.CardSpell:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv6));
                break;
            case AppConstants.MainType.Collaboration:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpCollaboration));
                break;
            case AppConstants.MainType.CardMonster:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv6));
                break;
            case AppConstants.MainType.Equipment:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpEquipment));
                break;
            case AppConstants.MainType.Medal:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpMedal));
                break;
            case AppConstants.MainType.Skill:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpSkill));
                break;
            case AppConstants.MainType.Symbol:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpSymbol));
                break;
            case AppConstants.MainType.Title:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpTitle));
                break;
            case AppConstants.MainType.MagicFormationCircle:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpMagicFormationCircle));
                break;
            case AppConstants.MainType.Relic:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpRelic));
                break;
            case AppConstants.MainType.CardColonel:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv6));
                break;
            case AppConstants.MainType.CardGeneral:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv6));
                break;
            case AppConstants.MainType.CardAdmiral:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv6));
                break;
            case AppConstants.MainType.CardLife:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv6));
                break;
            case AppConstants.MainType.SpiritBeast:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv6));
                break;
            case AppConstants.MainType.Alchievement:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpAchievement));
                break;
            default:
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv1));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv2));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv3));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv4));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv5));
                items.Add(GetUserItemByName(ItemConstants.Experiment.ExpBottleLv6));
                break;
        }
        return items;
    }
    public List<Items> GetItemForBreakthourgh(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case AppConstants.MainType.CardHero:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.Book:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.CardCaptain:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.Pet:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.CollaborationEquipment:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.CardMilitary:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.CardSpell:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.Collaboration:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.CardMonster:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.Equipment:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.Medal:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.Skill:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.Symbol:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.Title:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.MagicFormationCircle:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.Relic:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.CardColonel:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.CardGeneral:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.CardAdmiral:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.CardLife:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.SpiritBeast:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            case AppConstants.MainType.Alchievement:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
                break;
            default:
                items.Add(GetUserItemByName(ItemConstants.Breakthrough.BreakthroughToken));
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
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber1));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber2));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber3));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber4));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber5));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber6));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber7));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber8));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber9));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber10));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber11));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber12));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber13));
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber14));
                break;
            default:
                items.Add(GetUserItemByName(ItemConstants.Affinity.AffinityNumber1));
                break;
        }
        return items;
    }
}
