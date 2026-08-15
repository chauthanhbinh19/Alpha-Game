using System.Threading.Tasks;

public interface IUpgradesRepository
{
    Task<Upgrades> GetUpgradeByIdAsync(string id);
    Task<InsertOrUpdateResult<Upgrades>> InsertUpgradeAsync(Upgrades upgrade);
    Task<InsertOrUpdateResult<Upgrades>> UpdateUpgradeAsync(Upgrades upgrade);
}