using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTechnologiesService
{
    Task<List<Technologies>> GetUserTechnologiesAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserTechnologiesCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserTechnologyAsync(Technologies Technologies, string userId);
    Task<bool> UpdateTechnologyLevelAsync(Technologies Technologies, int TechnologyLevel);
    Task<bool> UpdateTechnologyBreakthroughAsync(Technologies Technologies, int star, double quantity);
    Task<Technologies> GetUserTechnologyByIdAsync(string user_id, string Id);
    Task<Technologies> SumPowerUserTechnologiesAsync();
}