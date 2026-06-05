using System.Threading.Tasks;

public interface IUpgradesService
{
    Task<Upgrades> GetUpgradeByIdAsync(string id);
    // Task<UpgradeResultDTO> UpgradeOneLevelAsync(string featureName, int currentLevel, int maxLevel, string userId);
    // Task<UpgradeResultDTO> UpgradeMaxLevelAsync(string featureName,int currentLevel,int maxLevel,string userId);
}