using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWeaponsGalleryService
{
    Task<List<Weapons>> GetWeaponsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetWeaponsCountAsync(string search, string type, string rare);
    Task InsertWeaponGalleryAsync(string userId, string Id);
    Task UpdateStatusWeaponGalleryAsync(string userId, string Id);
    Task UpdateStarWeaponGalleryAsync(string userId, string id, double star);
    Task UpdateWeaponGalleryPowerAsync(string userId, string id);
    Task<Weapons> SumPowerWeaponsGalleryAsync(string userId);
}