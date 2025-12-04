using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWeaponsGalleryService
{
    Task<List<Weapons>> GetWeaponsCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetWeaponsCountAsync(string rare);
    Task InsertWeaponGalleryAsync(string Id);
    Task UpdateStatusWeaponGalleryAsync(string Id);
    Task UpdateStarWeaponGalleryAsync(string id, double star);
    Task UpdateWeaponGalleryPowerAsync(string id);
    Task<Weapons> SumPowerWeaponsGalleryAsync();
}