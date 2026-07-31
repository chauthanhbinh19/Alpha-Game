using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFoodsService
{
    Task<List<Foods>> GetUserFoodsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserFoodsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFoodAsync(string userId, Foods food);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFoodsBatchAsync(string userId, List<Foods> foods);
    Task<bool> UpdateUserFoodLevelAsync(string userId, Foods food);
    Task<bool> UpdateUserFoodStarAsync(string userId, Foods food);
    Task<Foods> GetUserFoodByIdAsync(string userId, string Id);
    Task<Foods> SumPowerUserFoodsAsync(string userId);
}