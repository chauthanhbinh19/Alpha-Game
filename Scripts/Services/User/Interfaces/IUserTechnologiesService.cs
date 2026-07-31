using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTechnologiesService
{
    Task<List<Technologies>> GetUserTechnologiesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserTechnologiesCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTechnologyAsync(string userId, Technologies technology);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserTechnologiesBatchAsync(string userId, List<Technologies> technologies);
    Task<bool> UpdateUserTechnologyLevelAsync(string userId, Technologies technology);
    Task<bool> UpdateUserTechnologyStarAsync(string userId, Technologies technology);
    Task<Technologies> GetUserTechnologyByIdAsync(string userId, string Id);
    Task<Technologies> SumPowerUserTechnologiesAsync(string userId);
}