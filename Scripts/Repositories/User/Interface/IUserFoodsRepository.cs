using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFoodsRepository
{
    Task<List<Foods>> GetUserFoodsAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserFoodsCountAsync(string user_id, string rare);
    Task<bool> InsertUserFoodAsync(Foods Foods, string userId);
    Task<bool> UpdateFoodLevelAsync(Foods Foods, int FoodLevel);
    Task<bool> UpdateFoodBreakthroughAsync(Foods Foods, int star, double quantity);
    Task<Foods> GetUserFoodByIdAsync(string user_id, string Id);
    Task<Foods> SumPowerUserFoodsAsync();
}