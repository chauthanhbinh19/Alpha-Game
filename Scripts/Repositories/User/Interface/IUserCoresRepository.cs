using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCoresRepository
{
    Task<List<Cores>> GetUserCoresAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserCoresCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserCoreAsync(Cores Cores, string userId);
    Task<bool> UpdateCoreLevelAsync(Cores Cores, int cardLevel);
    Task<bool> UpdateCoreBreakthroughAsync(Cores Cores, int star, double quantity);
    Task<Cores> GetUserCoreByIdAsync(string user_id, string Id);
    Task<Cores> SumPowerUserCoresAsync();
}