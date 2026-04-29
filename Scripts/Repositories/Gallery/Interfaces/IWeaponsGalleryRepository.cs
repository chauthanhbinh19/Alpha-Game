using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWeaponsGalleryRepository
{
    Task<List<Weapons>> GetWeaponsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetWeaponsCountAsync(string search, string rare);
    Task InsertWeaponGalleryAsync(string Id, Weapons WeaponFromDB);
    Task UpdateStatusWeaponGalleryAsync(string Id);
    Task UpdateStarWeaponGalleryAsync(string id, double star);
    Task UpdateWeaponGalleryPowerAsync(string id, Weapons WeaponFromDB);
    Task<Weapons> SumPowerWeaponsGalleryAsync();
}