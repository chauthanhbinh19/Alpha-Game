using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMechaBeastsService
{
    Task<MechaBeasts> GetNewLevelPowerAsync(MechaBeasts c, double coefficient);
    Task<MechaBeasts> GetNewBreakthroughPowerAsync(MechaBeasts c, double coefficient);
    Task<List<MechaBeasts>> GetUserMechaBeastsAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserMechaBeastsCountAsync(string user_id, string rare);
    Task<bool> InsertUserMechaBeastAsync(MechaBeasts MechaBeasts, string userId);
    Task<bool> UpdateMechaBeastLevelAsync(MechaBeasts MechaBeasts, int TitleLevel);
    Task<bool> UpdateMechaBeastBreakthroughAsync(MechaBeasts MechaBeasts, int star, double quantity);
    Task<MechaBeasts> GetUserMechaBeastByIdAsync(string user_id, string Id);
    Task<MechaBeasts> SumPowerUserMechaBeastsAsync();
}