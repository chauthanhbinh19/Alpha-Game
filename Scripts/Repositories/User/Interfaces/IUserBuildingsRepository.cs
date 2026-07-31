using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBuildingsRepository
{
    Task<List<Buildings>> GetUserBuildingsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserBuildingsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<Buildings>> InsertOrUpdateUserBuildingAsync(string userId, Buildings building);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Buildings>>> InsertOrUpdateUserBuildingsBatchAsync(string userId, List<Buildings> buildings);
    Task<InsertOrUpdateResult<bool>> UpdateUserBuildingLevelAsync(string userId, Buildings building);
    Task<InsertOrUpdateResult<bool>> UpdateUserBuildingStarAsync(string userId, Buildings building);
    Task<Buildings> GetUserBuildingByIdAsync(string userId, string Id);
    Task<Buildings> SumPowerUserBuildingsAsync(string userId);
}