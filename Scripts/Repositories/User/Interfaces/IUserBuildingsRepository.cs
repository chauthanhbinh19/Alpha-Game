using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBuildingsRepository
{
    Task<List<Buildings>> GetUserBuildingsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserBuildingsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserBuildingAsync(Buildings building, string userId);
    Task<bool> InsertOrUpdateUserBuildingsBatchAsync(List<Buildings> buildings);
    Task<bool> UpdateBuildingLevelAsync(Buildings building, int cardLevel);
    Task<bool> UpdateBuildingBreakthroughAsync(Buildings building, int star, double quantity);
    Task<Buildings> GetUserBuildingByIdAsync(string user_id, string Id);
    Task<Buildings> SumPowerUserBuildingsAsync();
}