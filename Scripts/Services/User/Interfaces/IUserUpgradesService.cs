using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserUpgradesService
{ 
    Task<UserUpgrades> GetUserUpgradesAsync(string upgradeId, IStats stat);
    Task InsertOrUpdateUserUpgradesAsync(string userId, UserUpgrades upgrade, IStats stat);
    Task<UserUpgrades> GetSumUserUpgradesAsync(string userId, IStats stat);
}