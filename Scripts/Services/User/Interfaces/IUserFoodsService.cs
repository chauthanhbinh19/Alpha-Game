using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFoodsService
{
    Task<List<Foods>> GetUserFoodsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserFoodsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserFoodAsync(Foods food, string userId);
    Task<bool> InsertOrUpdateUserFoodsBatchAsync(List<Foods> foods);
    Task<bool> UpdateFoodLevelAsync(Foods food, int level);
    Task<bool> UpdateFoodBreakthroughAsync(Foods food, int star, double quantity);
    Task<Foods> GetUserFoodByIdAsync(string user_id, string Id);
    Task<Foods> SumPowerUserFoodsAsync();
}