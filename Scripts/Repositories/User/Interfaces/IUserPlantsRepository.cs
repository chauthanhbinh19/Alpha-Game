using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserPlantsRepository
{
    Task<List<Plants>> GetUserPlantsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserPlantsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserPlantAsync(Plants plant, string userId);
    Task<bool> InsertOrUpdateUserPlantsBatchAsync(List<Plants> plants);
    Task<bool> UpdatePlantLevelAsync(Plants plant, int level);
    Task<bool> UpdatePlantBreakthroughAsync(Plants plant, int star, double quantity);
    Task<Plants> GetUserPlantByIdAsync(string user_id, string Id);
    Task<Plants> SumPowerUserPlantsAsync();
}