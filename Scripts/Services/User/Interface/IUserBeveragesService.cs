using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBeveragesService
{
    Task<Beverages> GetNewLevelPowerAsync(Beverages c, double coefficient);
    Task<Beverages> GetNewBreakthroughPowerAsync(Beverages c, double coefficient);
    Task<List<Beverages>> GetUserBeveragesAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserBeveragesCountAsync(string user_id, string rare);
    Task<bool> InsertUserBeverageAsync(Beverages Beverages, string userId);
    Task<bool> UpdateBeverageLevelAsync(Beverages Beverages, int BeverageLevel);
    Task<bool> UpdateBeverageBreakthroughAsync(Beverages Beverages, int star, double quantity);
    Task<Beverages> GetUserBeverageByIdAsync(string user_id, string Id);
    Task<Beverages> SumPowerUserBeveragesAsync();
}