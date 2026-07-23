using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserWeaponsRepository
{
    Task<List<Weapons>> GetUserWeaponsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserWeaponsCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserWeaponAsync(Weapons weapon, string userId);
    Task<bool> InsertOrUpdateUserWeaponsBatchAsync(string userId, List<Weapons> weapons);
    Task<bool> UpdateUserWeaponLevelAsync(string userId, Weapons weapon);
    Task<bool> UpdateUserWeaponStarAsync(string userId, Weapons weapon);
    Task<bool> UpdateUserWeaponBreakthroughAsync(string userId, Weapons weapon, int star, double quantity);
    Task<Weapons> GetUserWeaponByIdAsync(string userId, string Id);
    Task<Weapons> SumPowerUserWeaponsAsync(string userId);
}