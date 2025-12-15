using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserPlantsService
{
    Task<Plants> GetNewLevelPowerAsync(Plants c, double coefficient);
    Task<Plants> GetNewBreakthroughPowerAsync(Plants c, double coefficient);
    Task<List<Plants>> GetUserPlantsAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserPlantsCountAsync(string user_id, string rare);
    Task<bool> InsertUserPlantAsync(Plants Plants, string userId);
    Task<bool> UpdatePlantLevelAsync(Plants Plants, int PlantLevel);
    Task<bool> UpdatePlantBreakthroughAsync(Plants Plants, int star, double quantity);
    Task<Plants> GetUserPlantByIdAsync(string user_id, string Id);
    Task<Plants> SumPowerUserPlantsAsync();
}