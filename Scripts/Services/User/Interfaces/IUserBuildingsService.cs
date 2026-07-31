using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBuildingsService
{
    Task<List<Buildings>> GetUserBuildingsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserBuildingsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBuildingAsync(string userId, Buildings building);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserBuildingsBatchAsync(string userId, List<Buildings> buildings);
    Task<bool> UpdateUserBuildingLevelAsync(string userId, Buildings building);
    Task<bool> UpdateUserBuildingStarAsync(string userId, Buildings building);
    Task<Buildings> GetUserBuildingByIdAsync(string userId, string Id);
    Task<Buildings> SumPowerUserBuildingsAsync(string userId);
}