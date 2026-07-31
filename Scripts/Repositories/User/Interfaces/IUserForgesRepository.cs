using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserForgesRepository
{
    Task<List<Forges>> GetUserForgesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserForgesCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<Forges>> InsertOrUpdateUserForgeAsync(string userId, Forges forge);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Forges>>> InsertOrUpdateUserForgesBatchAsync(string userId, List<Forges> forges);
    Task<InsertOrUpdateResult<bool>> UpdateUserForgeLevelAsync(string userId, Forges forge);
    Task<InsertOrUpdateResult<bool>> UpdateUserForgeStarAsync(string userId, Forges forge);
    Task<Forges> GetUserForgeByIdAsync(string userId, string Id);
    Task<Forges> SumPowerUserForgesAsync(string userId);
}