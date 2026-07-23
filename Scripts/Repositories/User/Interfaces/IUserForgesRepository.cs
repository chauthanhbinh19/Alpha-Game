using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserForgesRepository
{
    Task<List<Forges>> GetUserForgesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserForgesCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserForgeAsync(Forges forge, string userId);
    Task<bool> InsertOrUpdateUserForgesBatchAsync(List<Forges> forges);
    Task<bool> UpdateUserForgeLevelAsync(Forges forge);
    Task<bool> UpdateUserForgeStarAsync(Forges forge);
    Task<bool> UpdateUserForgeBreakthroughAsync(Forges forge, int star, double quantity);
    Task<Forges> GetUserForgeByIdAsync(string userId, string Id);
    Task<Forges> SumPowerUserForgesAsync();
}