using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserWeaponsRepository
{
    Task<List<Weapons>> GetUserWeaponsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserWeaponsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserWeaponAsync(Weapons weapon, string userId);
    Task<bool> InsertOrUpdateUserWeaponsBatchAsync(List<Weapons> weapons);
    Task<bool> UpdateWeaponLevelAsync(Weapons weapon, int level);
    Task<bool> UpdateWeaponBreakthroughAsync(Weapons weapon, int star, double quantity);
    Task<Weapons> GetUserWeaponByIdAsync(string user_id, string Id);
    Task<Weapons> SumPowerUserWeaponsAsync();
}