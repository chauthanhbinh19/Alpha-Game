using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTechnologiesRepository
{
    Task<List<Technologies>> GetUserTechnologiesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserTechnologiesCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Technologies>> InsertOrUpdateUserTechnologyAsync(string userId, Technologies technology);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Technologies>>> InsertOrUpdateUserTechnologiesBatchAsync(string userId, List<Technologies> technologies);
    Task<InsertOrUpdateResult<bool>> UpdateUserTechnologyLevelAsync(string userId, Technologies technology);
    Task<InsertOrUpdateResult<bool>> UpdateUserTechnologyStarAsync(string userId, Technologies technology);
    Task<Technologies> GetUserTechnologyByIdAsync(string userId, string Id);
    Task<Technologies> SumPowerUserTechnologiesAsync(string userId);
}