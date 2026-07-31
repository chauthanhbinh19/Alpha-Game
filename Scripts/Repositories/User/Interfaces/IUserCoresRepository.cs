using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCoresRepository
{
    Task<List<Cores>> GetUserCoresAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserCoresCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Cores>> InsertOrUpdateUserCoreAsync(string userId, Cores core);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Cores>>> InsertOrUpdateUserCoresBatchAsync(string userId, List<Cores> cores);
    Task<InsertOrUpdateResult<bool>> UpdateUserCoreLevelAsync(string userId, Cores core);
    Task<InsertOrUpdateResult<bool>> UpdateUserCoreStarAsync(string userId, Cores core);
    Task<Cores> GetUserCoreByIdAsync(string userId, string Id);
    Task<Cores> SumPowerUserCoresAsync(string userId);
}