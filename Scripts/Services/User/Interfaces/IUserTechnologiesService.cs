using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTechnologiesService
{
    Task<List<Technologies>> GetUserTechnologiesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserTechnologiesCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserTechnologyAsync(Technologies technology, string userId);
    Task<bool> InsertOrUpdateUserTechnologiesBatchAsync(string userId, List<Technologies> technologies);
    Task<bool> UpdateUserTechnologyLevelAsync(string userId, Technologies technology);
    Task<bool> UpdateUserTechnologyBreakthroughAsync(string userId, Technologies technology, int star, double quantity);
    Task<bool> UpdateUserTechnologyStarAsync(string userId, Technologies technology);
    Task<Technologies> GetUserTechnologyByIdAsync(string userId, string Id);
    Task<Technologies> SumPowerUserTechnologiesAsync(string userId);
}