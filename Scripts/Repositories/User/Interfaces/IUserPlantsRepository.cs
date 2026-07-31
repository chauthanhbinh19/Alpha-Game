using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserPlantsRepository
{
    Task<List<Plants>> GetUserPlantsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserPlantsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Plants>> InsertOrUpdateUserPlantAsync(string userId, Plants plant);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Plants>>> InsertOrUpdateUserPlantsBatchAsync(string userId, List<Plants> plants);
    Task<InsertOrUpdateResult<bool>> UpdateUserPlantLevelAsync(string userId, Plants plant);
    Task<InsertOrUpdateResult<bool>> UpdateUserPlantStarAsync(string userId, Plants plant);
    Task<Plants> GetUserPlantByIdAsync(string userId, string Id);
    Task<Plants> SumPowerUserPlantsAsync(string userId);
}