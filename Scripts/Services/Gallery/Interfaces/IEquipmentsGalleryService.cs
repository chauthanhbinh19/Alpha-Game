using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEquipmentsGalleryService
{
    Task<List<Equipments>> GetEquipmentsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetEquipmentsCountAsync(string search, string type, string rare);
    Task<bool> InsertEquipmentGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusEquipmentGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusEquipmentsGalleryAsync(string userId);
    Task<bool> UpdateStarEquipmentGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarEquipmentGalleryAsync(string userId, string equipmentId);
    Task<bool> UpdateBatchCurrentStarEquipmentsGalleryAsync(string userId);
    Task<bool> InsertBatchEquipmentsGalleryAsync(string userId, List<Equipments> equipments);
    Task<Equipments> GetEquipmentCollectionByIdAsync(string userId, string objectId);
    Task UpdateEquipmentGalleryPowerAsync(string userId, string Id);
    Task<Equipments> SumPowerEquipmentsGalleryAsync(string userId);
}