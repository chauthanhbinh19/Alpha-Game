using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMechaBeastsRepository
{
    Task<List<MechaBeasts>> GetUserMechaBeastsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserMechaBeastsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserMechaBeastAsync(MechaBeasts mechaBeast, string userId);
    Task<bool> InsertOrUpdateUserMechaBeastsBatchAsync(List<MechaBeasts> mechaBeasts);
    Task<bool> UpdateMechaBeastLevelAsync(MechaBeasts mechaBeast, int level);
    Task<bool> UpdateMechaBeastBreakthroughAsync(MechaBeasts mechaBeast, int star, double quantity);
    Task<MechaBeasts> GetUserMechaBeastByIdAsync(string user_id, string Id);
    Task<MechaBeasts> SumPowerUserMechaBeastsAsync();
}