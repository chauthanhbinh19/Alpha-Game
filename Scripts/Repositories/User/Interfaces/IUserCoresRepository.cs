using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCoresRepository
{
    Task<List<Cores>> GetUserCoresAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserCoresCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserCoreAsync(Cores core, string userId);
    Task<bool> InsertOrUpdateUserCoresBatchAsync(List<Cores> cores);
    Task<bool> UpdateCoreLevelAsync(Cores core, int level);
    Task<bool> UpdateCoreBreakthroughAsync(Cores core, int star, double quantity);
    Task<Cores> GetUserCoreByIdAsync(string user_id, string Id);
    Task<Cores> SumPowerUserCoresAsync();
}