using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWeaponsService
{
    Task<List<string>> GetUniqueWeaponsTypesAsync();
    Task<List<string>> GetUniqueWeaponsIdAsync();
    Task<List<Weapons>> GetWeaponsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<Weapons>> GetWeaponsWithoutLimitAsync();
    Task<int> GetWeaponsCountAsync(string search, string type, string rare);
    Task<List<Weapons>> GetWeaponsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetWeaponsWithPriceCountAsync(string type);
    Task<Weapons> GetWeaponByIdAsync(string id);
    Task<Weapons> SumPowerWeaponsPercentAsync();
}
