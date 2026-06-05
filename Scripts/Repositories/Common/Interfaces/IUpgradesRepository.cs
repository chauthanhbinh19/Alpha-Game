using System.Threading.Tasks;

public interface IUpgradesRepository
{
    Task<Upgrades> GetUpgradeByIdAsync(string id);
}