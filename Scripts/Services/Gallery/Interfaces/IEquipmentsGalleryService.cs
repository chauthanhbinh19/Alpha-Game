using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEquipmentsGalleryService
{
    Task<List<Equipments>> GetEquipmentsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetEquipmentsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertEquipmentGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusEquipmentGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusEquipmentsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarEquipmentGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarEquipmentGalleryAsync(string userId, string equipmentId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarEquipmentsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchEquipmentsGalleryAsync(string userId, List<Equipments> equipments);
    Task<Equipments> GetEquipmentCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateEquipmentGalleryPowerAsync(string userId, string Id);
    Task<Equipments> SumPowerEquipmentsGalleryAsync(string userId);
}