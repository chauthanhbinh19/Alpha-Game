using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserUpgradesRepository
{
    Task<UserUpgrades> GetUserUpgradesAsync(string userId, string upgradeId, string objectId, string userTable, string objectColumn);
    Task<InsertOrUpdateResult<UserUpgrades>> InsertOrUpdateUserUpgradesAsync(string userId, UserUpgrades upgrade, string objectId, string userTable, string objectColumn, List<RecipeItemDetailDTO> totalDeductions = null);
    Task<UserUpgrades> GetSumUserUpgradesAsync(string userId, string objectId, string userTable, string objectColumn);
}