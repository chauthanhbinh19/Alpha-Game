using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFoodsRepository
{
    Task<List<Foods>> GetUserFoodsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserFoodsCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserFoodAsync(Foods food, string userId);
    Task<bool> InsertOrUpdateUserFoodsBatchAsync(List<Foods> foods);
    Task<bool> UpdateUserFoodLevelAsync(Foods food);
    Task<bool> UpdateUserFoodStarAsync(Foods food);
    Task<bool> UpdateUserFoodBreakthroughAsync(Foods food, int star, double quantity);
    Task<Foods> GetUserFoodByIdAsync(string userId, string Id);
    Task<Foods> SumPowerUserFoodsAsync();
}