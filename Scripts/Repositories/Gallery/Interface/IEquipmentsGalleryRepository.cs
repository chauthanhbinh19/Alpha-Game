using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEquipmentsGalleryRepository
{
    Task<List<Equipments>> GetEquipmentsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetEquipmentsCountAsync(string search, string type, string rare);
    Task InsertEquipmentGalleryAsync(string Id, Equipments EquipmentFromDB);
    Task UpdateStatusEquipmentGalleryAsync(string Id);
    Task UpdateStarEquipmentGalleryAsync(string Id, double star);
    Task UpdateEquipmentGalleryPowerAsync(string Id, Equipments EquipmentFromDB);
    Task<Equipments> SumPowerEquipmentsGalleryAsync();
}