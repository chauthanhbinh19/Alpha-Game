using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFoodsRepository
{
    Task<List<Foods>> GetUserFoodsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserFoodsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Foods>> InsertOrUpdateUserFoodAsync(string userId, Foods food);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Foods>>> InsertOrUpdateUserFoodsBatchAsync(string userId, List<Foods> foods);
    Task<InsertOrUpdateResult<bool>> UpdateUserFoodLevelAsync(string userId, Foods food);
    Task<InsertOrUpdateResult<bool>> UpdateUserFoodStarAsync(string userId, Foods food);
    Task<Foods> GetUserFoodByIdAsync(string userId, string Id);
    Task<Foods> SumPowerUserFoodsAsync(string userId);
}