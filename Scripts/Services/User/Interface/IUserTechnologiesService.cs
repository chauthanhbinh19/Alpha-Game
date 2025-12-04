using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTechnologiesService
{
    Task<Technologies> GetNewLevelPowerAsync(Technologies c, double coefficient);
    Task<Technologies> GetNewBreakthroughPowerAsync(Technologies c, double coefficient);
    Task<List<Technologies>> GetUserTechnologiesAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserTechnologiesCountAsync(string user_id, string rare);
    Task<bool> InsertUserTechnologyAsync(Technologies Technologies, string userId);
    Task<bool> UpdateTechnologyLevelAsync(Technologies Technologies, int TechnologyLevel);
    Task<bool> UpdateTechnologyBreakthroughAsync(Technologies Technologies, int star, double quantity);
    Task<Technologies> GetUserTechnologyByIdAsync(string user_id, string Id);
    Task<Technologies> SumPowerUserTechnologiesAsync();
}