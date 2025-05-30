using System.Collections.Generic;

public interface IUserItemsRepository
{ 
    Items GetUserItemByName(string itemName);
    Items UpdateUserItemsQuantity(Items items);
}