using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserItemsRepository
{
    Task<List<Items>> GetUserItemsAsync(string user_id, string search, string type, int pageSize, int offset);
    Task<int> GetUserItemsCountAsync(string user_id, string search, string type);
    Task<Items> GetUserItemByNameAsync(string itemName);
    Task<bool> InsertUserItemAsync(Items items, double quantity);
    Task<Items> UpdateUserItemQuantityAsync(Items items);
    Task<Items> UpdateUserItemQuantityAsync(Items items, double quantity);
    Task<bool> InsertOrUpdateUserItemsBatchAsync(List<(Items item, double quantity)> items);
}