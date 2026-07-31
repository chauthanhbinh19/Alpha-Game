using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCoresService
{
    Task<List<Cores>> GetUserCoresAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserCoresCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCoreAsync(string userId, Cores core);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCoresBatchAsync(string userId, List<Cores> cores);
    Task<bool> UpdateUserCoreLevelAsync(string userId, Cores core);
    Task<bool> UpdateUserCoreStarAsync(string userId, Cores core);
    Task<Cores> GetUserCoreByIdAsync(string userId, string Id);
    Task<Cores> SumPowerUserCoresAsync(string userId);
}