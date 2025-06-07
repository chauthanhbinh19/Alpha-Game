using System.Collections.Generic;

public interface IUserItemsService
{
    Items GetUserItemByName(string itemName);
    Items InsertUserItems(Items items, int quantity);
    Items UpdateUserItemsQuantity(Items items);
    Items UpdateUserItemsQuantity(Items items, int quantity);
    List<Items> GetItemForLevel(string type);
    List<Items> GetItemForBreakthourgh(string type);
    List<Items> GetItemForRank(string type);
}
