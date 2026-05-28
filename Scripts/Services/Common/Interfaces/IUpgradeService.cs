using System.Threading.Tasks;

public interface IUpgradeService
{
    Task<UpgradeResultDTO> UpgradeOneLevelAsync(string featureName, int currentLevel, int maxLevel, string userId);
    Task<UpgradeResultDTO> UpgradeMaxLevelAsync(string featureName,int currentLevel,int maxLevel,string userId);
}