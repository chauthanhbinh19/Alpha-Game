using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserUpgradesRepository
{
    Task<UserUpgrades> GetUserUpgradesAsync(string type);
    Task InsertOrUpdateUserUpgradesAsync(string user_id, UserUpgrades Upgrades, string id);
    Task<UserUpgrades> GetSumUserUpgradesAsync(string user_id);
}