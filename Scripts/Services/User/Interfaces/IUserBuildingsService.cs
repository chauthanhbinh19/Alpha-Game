using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBuildingsService
{
    Task<List<Buildings>> GetUserBuildingsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserBuildingsCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserBuildingAsync(Buildings building, string userId);
    Task<bool> InsertOrUpdateUserBuildingsBatchAsync(string userId, List<Buildings> buildings);
    Task<bool> UpdateUserBuildingLevelAsync(string userId, Buildings building);
    Task<bool> UpdateUserBuildingStarAsync(string userId, Buildings building);
    Task<bool> UpdateUserBuildingBreakthroughAsync(string userId, Buildings building, int star, double quantity);
    Task<Buildings> GetUserBuildingByIdAsync(string userId, string Id);
    Task<Buildings> SumPowerUserBuildingsAsync(string userId);
}