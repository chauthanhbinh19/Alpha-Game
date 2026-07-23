using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBeveragesService
{
    Task<List<Beverages>> GetUserBeveragesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserBeveragesCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserBeverageAsync(Beverages beverage, string userId);
    Task<bool> InsertOrUpdateUserBeveragesBatchAsync(string userId, List<Beverages> beverages);
    Task<bool> UpdateUserBeverageLevelAsync(string userId, Beverages beverage);
    Task<bool> UpdateUserBeverageStarAsync(string userId, Beverages beverage);
    Task<bool> UpdateUserBeverageBreakthroughAsync(string userId, Beverages beverage, int star, double quantity);
    Task<Beverages> GetUserBeverageByIdAsync(string userId, string Id);
    Task<Beverages> SumPowerUserBeveragesAsync(string userId);
}