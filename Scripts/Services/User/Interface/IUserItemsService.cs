using System.Collections.Generic;

public interface IUserItemsService
{
    List<Items> GetUserItems(string user_id, string type, int pageSize, int offset);
    int GetUserItemCount(string user_id, string type);
    Items GetUserItemByName(string itemName);
    bool InsertUserItems(Items items, int quantity);
    Items UpdateUserItemsQuantity(Items items);
    Items UpdateUserItemsQuantity(Items items, int quantity);
    List<Items> GetItemForLevel(string type);
    List<Items> GetItemForBreakthourgh(string type);
    List<Items> GetItemForRank(string type);
}
