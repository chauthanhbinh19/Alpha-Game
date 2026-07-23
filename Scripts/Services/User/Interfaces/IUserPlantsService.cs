using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserPlantsService
{
    Task<List<Plants>> GetUserPlantsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserPlantsCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserPlantAsync(Plants plant, string userId);
    Task<bool> InsertOrUpdateUserPlantsBatchAsync(string userId, List<Plants> plants);
    Task<bool> UpdateUserPlantLevelAsync(string userId, Plants plant);
    Task<bool> UpdateUserPlantStarAsync(string userId, Plants plant);
    Task<bool> UpdateUserPlantBreakthroughAsync(string userId, Plants plant, int star, double quantity);
    Task<Plants> GetUserPlantByIdAsync(string userId, string Id);
    Task<Plants> SumPowerUserPlantsAsync(string userId);
}