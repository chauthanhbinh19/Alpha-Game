using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserForgesService
{
    Task<List<Forges>> GetUserForgesAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserForgesCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserForgeAsync(Forges forge, string userId);
    Task<bool> InsertOrUpdateUserForgesBatchAsync(List<Forges> forges);
    Task<bool> UpdateForgeLevelAsync(Forges forge, int level);
    Task<bool> UpdateForgeBreakthroughAsync(Forges forge, int star, double quantity);
    Task<Forges> GetUserForgeByIdAsync(string user_id, string Id);
    Task<Forges> SumPowerUserForgesAsync();
}