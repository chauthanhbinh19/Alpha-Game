using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTechnologiesService
{
    Task<List<Technologies>> GetUserTechnologiesAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserTechnologiesCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserTechnologyAsync(Technologies technology, string userId);
    Task<bool> InsertOrUpdateUserTechnologiesBatchAsync(List<Technologies> technologies);
    Task<bool> UpdateTechnologyLevelAsync(Technologies technology);
    Task<bool> UpdateTechnologyBreakthroughAsync(Technologies technology, int star, double quantity);
    Task<bool> UpdateTechnologyStarAsync(Technologies technology);
    Task<Technologies> GetUserTechnologyByIdAsync(string user_id, string Id);
    Task<Technologies> SumPowerUserTechnologiesAsync();
}