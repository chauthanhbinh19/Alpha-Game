using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCoresService
{
    Task<List<Cores>> GetUserCoresAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserCoresCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserCoreAsync(Cores core, string userId);
    Task<bool> InsertOrUpdateUserCoresBatchAsync(string userId, List<Cores> cores);
    Task<bool> UpdateUserCoreLevelAsync(string userId, Cores core);
    Task<bool> UpdateUserCoreStarAsync(string userId, Cores core);
    Task<bool> UpdateUserCoreBreakthroughAsync(string userId, Cores core, int star, double quantity);
    Task<Cores> GetUserCoreByIdAsync(string userId, string Id);
    Task<Cores> SumPowerUserCoresAsync(string userId);
}