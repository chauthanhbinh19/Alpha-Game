using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserItemsService
{
    Task<List<Items>> GetUserItemsAsync(string userId, string search, string type, int pageSize, int offset);
    Task<int> GetUserItemsCountAsync(string userId, string search, string type);
    Task<Items> GetUserItemByNameAsync(string userId, string itemName);
    Task<Items> GetUserItemByCodeNameAsync(string userId, string codeName);
    Task<ItemExperienceDTO> GetUserItemExperienceByCodeNameAsync(string userId, string codeName);
    Task<bool> InsertUserItemAsync(string userId, Items item, double quantity);
    Task<Items> UpdateUserItemQuantityAsync(string userId, Items item);
    Task<Items> UpdateUserItemQuantityAsync(string userId, Items item, double quantity);
    Task<bool> InsertOrUpdateUserItemAsync(string userId, Items item, double quantity);
    Task<bool> InsertOrUpdateUserItemsBatchAsync(string userId, List<(Items item, double quantity)> items);
    List<Items> GetItemForLevelAsync(string userId, string type);
    Task<List<Items>> GetItemForBreakthourghAsync(string userId, string type);
    Task<List<Items>> GetItemForRankAsync(string userId, string type);
}
