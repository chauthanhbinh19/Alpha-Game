using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserForgesRepository
{
    Task<List<Forges>> GetUserForgesAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<int> GetUserForgesCountAsync(string user_id, string type, string rare);
    Task<bool> InsertUserForgeAsync(Forges Forge, string userId);
    Task<bool> UpdateForgeLevelAsync(Forges Forge, int cardLevel);
    Task<bool> UpdateForgeBreakthroughAsync(Forges Forge, int star, double quantity);
    Task<Forges> GetUserForgeByIdAsync(string user_id, string Id);
    Task<Forges> SumPowerUserForgesAsync();
}