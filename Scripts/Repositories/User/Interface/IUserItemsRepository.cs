using System.Collections.Generic;

public interface IUserItemsRepository
{
    Items GetUserItemByName(string itemName);
    Items InsertUserItems(Items items, int quantity);
    Items UpdateUserItemsQuantity(Items items);
    Items UpdateUserItemsQuantity(Items items, int quantity);
}