using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWeaponsGalleryRepository
{
    Task<List<Weapons>> GetWeaponsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetWeaponsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Weapons>> InsertWeaponGalleryAsync(string userId, string Id, Weapons WeaponFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusWeaponGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusWeaponsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarWeaponGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarWeaponGalleryAsync(string userId, string weaponId);
    Task<InsertOrUpdateResult<List<(string WeaponId, double CurrentStar)>>> UpdateBatchCurrentStarWeaponsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Weapons>>> InsertBatchWeaponsGalleryAsync(string userId, List<Weapons> weapons);
    Task<Weapons> GetWeaponCollectionByIdAsync(string userId, string objectId);
    Task UpdateWeaponGalleryPowerAsync(string userId, string id, Weapons WeaponFromDB);
    Task<Weapons> SumPowerWeaponsGalleryAsync(string userId);
}