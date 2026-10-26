using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserUpgradesService
{ 
    Task<UserUpgrades> GetUserUpgradesAsync(string userId, string upgradeId, IStats stat);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserUpgradesAsync(string userId, UserUpgrades upgrade, IStats stat, List<RecipeItemDetailDTO> totalDeductions = null);
    Task<UserUpgrades> GetSumUserUpgradesAsync(string userId, IStats stat);
}