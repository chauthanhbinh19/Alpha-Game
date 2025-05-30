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

    public Items GetUserItemByName(string itemName)
    {
        return _userItemsRepository.GetUserItemByName(itemName);
    }

    public Items UpdateUserItemsQuantity(Items items)
    {
        return _userItemsRepository.UpdateUserItemsQuantity(items);
    }
    public List<Items> GetItemForLevel(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case "CardHeroes":
                items.Add(GetUserItemByName("Exp Bottle lv1"));
                items.Add(GetUserItemByName("Exp Bottle lv2"));
                items.Add(GetUserItemByName("Exp Bottle lv3"));
                items.Add(GetUserItemByName("Exp Bottle lv4"));
                items.Add(GetUserItemByName("Exp Bottle lv5"));
                items.Add(GetUserItemByName("Exp Bottle lv6"));
                break;
            case "Books":
                items.Add(GetUserItemByName("Exp Books"));
                break;
            case "CardCaptains":
                items.Add(GetUserItemByName("Exp Bottle lv1"));
                items.Add(GetUserItemByName("Exp Bottle lv2"));
                items.Add(GetUserItemByName("Exp Bottle lv3"));
                items.Add(GetUserItemByName("Exp Bottle lv4"));
                items.Add(GetUserItemByName("Exp Bottle lv5"));
                items.Add(GetUserItemByName("Exp Bottle lv6"));
                break;
            case "Pets":
                items.Add(GetUserItemByName("Exp Pets"));
                break;
            case "CollaborationEquipment":
                items.Add(GetUserItemByName("Exp Collaboration Equipments"));
                break;
            case "CardMilitary":
                items.Add(GetUserItemByName("Exp Card Military"));
                break;
            case "CardSpell":
                items.Add(GetUserItemByName("Exp Spell"));
                break;
            case "Collaboration":
                items.Add(GetUserItemByName("Exp Collaborations"));
                break;
            case "CardMonsters":
                items.Add(GetUserItemByName("Exp Card Monsters"));
                break;
            case "Equipments":
                items.Add(GetUserItemByName("Exp Equipments"));
                break;
            case "Medals":
                items.Add(GetUserItemByName("Exp Medals"));
                break;
            case "Skills":
                items.Add(GetUserItemByName("Exp Skills"));
                break;
            case "Symbols":
                items.Add(GetUserItemByName("Exp Symbols"));
                break;
            case "Titles":
                items.Add(GetUserItemByName("Exp Titles"));
                break;
            case "MagicFormationCircle":
                items.Add(GetUserItemByName("Exp Magic Formation Circle"));
                break;
            case "Relics":
                items.Add(GetUserItemByName("Exp Relics"));
                break;
            case "CardColonels":
                items.Add(GetUserItemByName("Exp Bottle lv1"));
                items.Add(GetUserItemByName("Exp Bottle lv2"));
                items.Add(GetUserItemByName("Exp Bottle lv3"));
                items.Add(GetUserItemByName("Exp Bottle lv4"));
                items.Add(GetUserItemByName("Exp Bottle lv5"));
                items.Add(GetUserItemByName("Exp Bottle lv6"));
                break;
            case "CardGenerals":
                items.Add(GetUserItemByName("Exp Bottle lv1"));
                items.Add(GetUserItemByName("Exp Bottle lv2"));
                items.Add(GetUserItemByName("Exp Bottle lv3"));
                items.Add(GetUserItemByName("Exp Bottle lv4"));
                items.Add(GetUserItemByName("Exp Bottle lv5"));
                items.Add(GetUserItemByName("Exp Bottle lv6"));
                break;
            case "CardAdmirals":
                items.Add(GetUserItemByName("Exp Bottle lv1"));
                items.Add(GetUserItemByName("Exp Bottle lv2"));
                items.Add(GetUserItemByName("Exp Bottle lv3"));
                items.Add(GetUserItemByName("Exp Bottle lv4"));
                items.Add(GetUserItemByName("Exp Bottle lv5"));
                items.Add(GetUserItemByName("Exp Bottle lv6"));
                break;
            case "Achievements":
                items.Add(GetUserItemByName("Exp Achievements"));
                break;
            default:
                items.Add(GetUserItemByName("Exp Bottle lv1"));
                items.Add(GetUserItemByName("Exp Bottle lv2"));
                items.Add(GetUserItemByName("Exp Bottle lv3"));
                items.Add(GetUserItemByName("Exp Bottle lv4"));
                items.Add(GetUserItemByName("Exp Bottle lv5"));
                items.Add(GetUserItemByName("Exp Bottle lv6"));
                break;
        }
        return items;
    }
    public List<Items> GetItemForBreakthourgh(string type)
    {
        List<Items> items = new List<Items>();
        switch (type)
        {
            case "CardHeroes":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "Books":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "CardCaptains":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "Pets":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "CollaborationEquipment":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "CardMilitary":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "CardSpell":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "Collaboration":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "CardMonsters":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "Equipments":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "Medals":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "Skills":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "Symbols":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "Titles":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "MagicFormationCircle":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "Relics":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "CardColonels":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "CardGenerals":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "CardAdmirals":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            case "Achievements":
                items.Add(GetUserItemByName("Breakthrough Token"));
                break;
            default:
                items.Add(GetUserItemByName("Breakthrough Token"));
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
                items.Add(GetUserItemByName("Affinity 1"));
                items.Add(GetUserItemByName("Affinity 2"));
                items.Add(GetUserItemByName("Affinity 3"));
                items.Add(GetUserItemByName("Affinity 4"));
                items.Add(GetUserItemByName("Affinity 5"));
                items.Add(GetUserItemByName("Affinity 6"));
                items.Add(GetUserItemByName("Affinity 7"));
                items.Add(GetUserItemByName("Affinity 8"));
                items.Add(GetUserItemByName("Affinity 9"));
                items.Add(GetUserItemByName("Affinity 10"));
                items.Add(GetUserItemByName("Affinity 11"));
                items.Add(GetUserItemByName("Affinity 12"));
                items.Add(GetUserItemByName("Affinity 13"));
                items.Add(GetUserItemByName("Affinity 14"));
                break;
            default:
                items.Add(GetUserItemByName("Affinity 1"));
                break;
        }
        return items;
    }
}
