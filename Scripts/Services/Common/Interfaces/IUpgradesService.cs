using System.Threading.Tasks;

public interface IUpgradesService
{
    Task<Upgrades> GetUpgradeByIdAsync(string id);
    Task<InsertOrUpdateResult<Upgrades>> InsertUpgradeAsync(Upgrades upgrade);
    Task<InsertOrUpdateResult<Upgrades>> UpdateUpgradeAsync(Upgrades upgrade);
    // Task<UpgradeResultDTO> UpgradeOneLevelAsync(string featureName, int currentLevel, int maxLevel, string userId);
    // Task<UpgradeResultDTO> UpgradeMaxLevelAsync(string featureName,int currentLevel,int maxLevel,string userId);
}