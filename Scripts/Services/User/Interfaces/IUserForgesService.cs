using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserForgesService
{
    Task<List<Forges>> GetUserForgesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserForgesCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserForgeAsync(Forges forge, string userId);
    Task<bool> InsertOrUpdateUserForgesBatchAsync(string userId, List<Forges> forges);
    Task<bool> UpdateUserForgeLevelAsync(string userId, Forges forge);
    Task<bool> UpdateUserForgeStarAsync(string userId, Forges forge);
    Task<bool> UpdateUserForgeBreakthroughAsync(string userId, Forges forge, int star, double quantity);
    Task<Forges> GetUserForgeByIdAsync(string userId, string Id);
    Task<Forges> SumPowerUserForgesAsync(string userId);
}