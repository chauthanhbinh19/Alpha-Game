using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEquipmentsGalleryService
{
    Task<List<Equipments>> GetEquipmentsCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetEquipmentsCountAsync(string type, string rare);
    Task InsertEquipmentGalleryAsync(string Id);
    Task UpdateStatusEquipmentGalleryAsync(string Id);
    Task UpdateStarEquipmentGalleryAsync(string Id, double star);
    Task UpdateEquipmentGalleryPowerAsync(string Id);
    Task<Equipments> SumPowerEquipmentsGalleryAsync();
}