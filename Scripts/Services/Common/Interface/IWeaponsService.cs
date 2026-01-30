using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWeaponsService
{
    Task<List<string>> GetUniqueWeaponsIdAsync();
    Task<List<Weapons>> GetWeaponsAsync(string search, string rare, int pageSize, int offset);
    Task<int> GetWeaponsCountAsync(string search, string rare);
    Task<List<Weapons>> GetWeaponsWithPriceAsync(int pageSize, int offset);
    Task<int> GetWeaponsWithPriceCountAsync();
    Task<Weapons> GetWeaponByIdAsync(string id);
    Task<Weapons> SumPowerWeaponsPercentAsync();
}
