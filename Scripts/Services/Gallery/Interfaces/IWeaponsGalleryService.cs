using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWeaponsGalleryService
{
    Task<List<Weapons>> GetWeaponsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetWeaponsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertWeaponGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusWeaponGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusWeaponsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarWeaponGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarWeaponGalleryAsync(string userId, string weaponId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarWeaponsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchWeaponsGalleryAsync(string userId, List<Weapons> weapons);
    Task<Weapons> GetWeaponCollectionByIdAsync(string userId, string weaponId);
    Task<InsertOrUpdateResult<bool>> UpdateWeaponGalleryPowerAsync(string userId, string id);
    Task<Weapons> SumPowerWeaponsGalleryAsync(string userId);
}