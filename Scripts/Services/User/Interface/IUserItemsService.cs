using System.Collections.Generic;

public interface IUserItemsService
{
    Items GetUserItemByName(string itemName);
    Items UpdateUserItemsQuantity(Items items);
    List<Items> GetItemForLevel(string type);
    List<Items> GetItemForBreakthourgh(string type);
    List<Items> GetItemForRank(string type);
}
