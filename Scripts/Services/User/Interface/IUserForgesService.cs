using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserForgesService
{
    Task<Forges> GetNewLevelPowerAsync(Forges c, double coefficient);
    Task<Forges> GetNewBreakthroughPowerAsync(Forges c, double coefficient);
    Task<List<Forges>> GetUserForgesAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<int> GetUserForgesCountAsync(string user_id, string type, string rare);
    Task<bool> InsertUserForgeAsync(Forges Forge, string userId);
    Task<bool> UpdateForgeLevelAsync(Forges Forge, int cardLevel);
    Task<bool> UpdateForgeBreakthroughAsync(Forges Forge, int star, double quantity);
    Task<Forges> GetUserForgeByIdAsync(string user_id, string Id);
    Task<Forges> SumPowerUserForgesAsync();
}