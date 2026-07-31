using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMechaBeastsService
{
    Task<List<MechaBeasts>> GetUserMechaBeastsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserMechaBeastsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMechaBeastAsync(string userId, MechaBeasts mechaBeast);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMechaBeastsBatchAsync(string userId, List<MechaBeasts> mechaBeasts);
    Task<bool> UpdateUserMechaBeastLevelAsync(string userId, MechaBeasts mechaBeast);
    Task<bool> UpdateUserMechaBeastStarAsync(string userId, MechaBeasts mechaBeast);
    Task<MechaBeasts> GetUserMechaBeastByIdAsync(string userId, string Id);
    Task<MechaBeasts> SumPowerUserMechaBeastsAsync(string userId);
}