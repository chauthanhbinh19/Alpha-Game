using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMechaBeastsRepository
{
    Task<List<MechaBeasts>> GetUserMechaBeastsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserMechaBeastsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<MechaBeasts>> InsertOrUpdateUserMechaBeastAsync(string userId, MechaBeasts mechaBeast);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<MechaBeasts>>> InsertOrUpdateUserMechaBeastsBatchAsync(string userId, List<MechaBeasts> mechaBeasts);
    Task<InsertOrUpdateResult<bool>> UpdateUserMechaBeastLevelAsync(string userId, MechaBeasts mechaBeast);
    Task<InsertOrUpdateResult<bool>> UpdateUserMechaBeastStarAsync(string userId, MechaBeasts mechaBeast);
    Task<MechaBeasts> GetUserMechaBeastByIdAsync(string userId, string Id);
    Task<MechaBeasts> SumPowerUserMechaBeastsAsync(string userId);
}