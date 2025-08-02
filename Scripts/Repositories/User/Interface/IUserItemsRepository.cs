using System.Collections.Generic;

public interface IUserItemsRepository
{
    List<Items> GetUserItems(string user_id, string type, int pageSize, int offset);
    int GetUserItemCount(string user_id, string type);
    Items GetUserItemByName(string itemName);
    bool InsertUserItems(Items items, int quantity);
    Items UpdateUserItemsQuantity(Items items);
    Items UpdateUserItemsQuantity(Items items, int quantity);
}