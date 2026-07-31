using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserForgesService
{
    Task<List<Forges>> GetUserForgesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserForgesCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserForgeAsync(string userId, Forges forge);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserForgesBatchAsync(string userId, List<Forges> forges);
    Task<bool> UpdateUserForgeLevelAsync(string userId, Forges forge);
    Task<bool> UpdateUserForgeStarAsync(string userId, Forges forge);
    Task<Forges> GetUserForgeByIdAsync(string userId, string Id);
    Task<Forges> SumPowerUserForgesAsync(string userId);
}