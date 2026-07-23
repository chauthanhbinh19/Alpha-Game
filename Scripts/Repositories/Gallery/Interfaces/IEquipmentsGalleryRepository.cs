using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEquipmentsGalleryRepository
{
    Task<List<Equipments>> GetEquipmentsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetEquipmentsCountAsync(string search, string type, string rare);
    Task InsertEquipmentGalleryAsync(string userId, string Id, Equipments EquipmentFromDB);
    Task UpdateStatusEquipmentGalleryAsync(string userId, string Id);
    Task UpdateStarEquipmentGalleryAsync(string userId, string Id, double star);
    Task UpdateEquipmentGalleryPowerAsync(string userId, string Id, Equipments EquipmentFromDB);
    Task<Equipments> SumPowerEquipmentsGalleryAsync(string userId);
}