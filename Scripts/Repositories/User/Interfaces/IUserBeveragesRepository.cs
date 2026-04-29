using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBeveragesRepository
{
    Task<List<Beverages>> GetUserBeveragesAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserBeveragesCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserBeverageAsync(Beverages beverage, string userId);
    Task<bool> InsertOrUpdateUserBeveragesBatchAsync(List<Beverages> beverages);
    Task<bool> UpdateBeverageLevelAsync(Beverages beverage, int beverageLevel);
    Task<bool> UpdateBeverageBreakthroughAsync(Beverages beverage, int star, double quantity);
    Task<Beverages> GetUserBeverageByIdAsync(string user_id, string Id);
    Task<Beverages> SumPowerUserBeveragesAsync();
}