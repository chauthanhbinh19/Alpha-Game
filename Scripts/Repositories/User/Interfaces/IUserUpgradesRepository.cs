using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserUpgradesRepository
{
    Task<UserUpgrades> GetUserUpgradesAsync(string userId, string upgradeId, string objectId, string userTable, string objectColumn);
    Task InsertOrUpdateUserUpgradesAsync(string userId, UserUpgrades upgrade, string objectId, string userTable, string objectColumn);
    Task<UserUpgrades> GetSumUserUpgradesAsync(string userId, string objectId, string userTable, string objectColumn);
}