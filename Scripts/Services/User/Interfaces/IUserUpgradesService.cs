using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserUpgradesService
{ 
    Task<UserUpgrades> GetUserUpgradesAsync(string id);
    Task InsertOrUpdateUserUpgradesAsync(string user_id, UserUpgrades Upgrades, string id);
    Task<UserUpgrades> GetSumUserUpgradesAsync(string user_id);
}