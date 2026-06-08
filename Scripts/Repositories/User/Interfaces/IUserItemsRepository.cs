using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserItemsRepository
{
    Task<List<Items>> GetUserItemsAsync(string userId, string search, string type, int pageSize, int offset);
    Task<int> GetUserItemsCountAsync(string userId, string search, string type);
    Task<Items> GetUserItemByNameAsync(string itemName);
    Task<Items> GetUserItemByCodeNameAsync(string codeName);
    Task<ItemExperienceDTO> GetUserItemExperienceByCodeNameAsync(string codeName);
    Task<bool> InsertUserItemAsync(Items items, double quantity);
    Task<Items> UpdateUserItemQuantityAsync(Items items);
    Task<Items> UpdateUserItemQuantityAsync(Items items, double quantity);
    Task<bool> InsertOrUpdateUserItemQuantityAsync(string userId, Items item, double quantity);
    Task<bool> InsertOrUpdateUserItemsBatchAsync(List<(Items item, double quantity)> items);
}