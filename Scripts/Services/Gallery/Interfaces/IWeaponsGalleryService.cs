using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWeaponsGalleryService
{
    Task<List<Weapons>> GetWeaponsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetWeaponsCountAsync(string search, string type, string rare);
    Task<bool> InsertWeaponGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusWeaponGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusWeaponsGalleryAsync(string userId);
    Task<bool> UpdateTempStarWeaponGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarWeaponGalleryAsync(string userId, string weaponId);
    Task<bool> UpdateBatchCurrentStarWeaponsGalleryAsync(string userId);
    Task<bool> InsertBatchWeaponsGalleryAsync(string userId, List<Weapons> weapons);
    Task<Weapons> GetWeaponCollectionByIdAsync(string userId, string weaponId);
    Task UpdateWeaponGalleryPowerAsync(string userId, string id);
    Task<Weapons> SumPowerWeaponsGalleryAsync(string userId);
}