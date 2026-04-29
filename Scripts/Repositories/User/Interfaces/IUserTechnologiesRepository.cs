using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTechnologiesRepository
{
    Task<List<Technologies>> GetUserTechnologiesAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserTechnologiesCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserTechnologyAsync(Technologies technology, string userId);
    Task<bool> InsertOrUpdateUserTechnologiesBatchAsync(List<Technologies> technologies);
    Task<bool> UpdateTechnologyLevelAsync(Technologies technology, int level);
    Task<bool> UpdateTechnologyBreakthroughAsync(Technologies technology, int star, double quantity);
    Task<Technologies> GetUserTechnologyByIdAsync(string user_id, string Id);
    Task<Technologies> SumPowerUserTechnologiesAsync();
}