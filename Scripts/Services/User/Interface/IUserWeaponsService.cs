using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserWeaponsService
{
    Task<Weapons> GetNewLevelPowerAsync(Weapons c, double coefficient);
    Task<Weapons> GetNewBreakthroughPowerAsync(Weapons c, double coefficient);
    Task<List<Weapons>> GetUserWeaponsAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserWeaponsCountAsync(string user_id, string rare);
    Task<bool> InsertUserWeaponAsync(Weapons Weapons, string userId);
    Task<bool> UpdateWeaponLevelAsync(Weapons Weapons, int WeaponLevel);
    Task<bool> UpdateWeaponBreakthroughAsync(Weapons Weapons, int star, double quantity);
    Task<Weapons> GetUserWeaponByIdAsync(string user_id, string Id);
    Task<Weapons> SumPowerUserWeaponsAsync();
}