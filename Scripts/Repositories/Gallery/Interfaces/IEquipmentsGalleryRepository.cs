using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEquipmentsGalleryRepository
{
    Task<List<Equipments>> GetEquipmentsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetEquipmentsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Equipments>> InsertEquipmentGalleryAsync(string userId, string Id, Equipments EquipmentFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusEquipmentGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusEquipmentsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarEquipmentGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarEquipmentGalleryAsync(string userId, string equipmentId);
    Task<InsertOrUpdateResult<List<(string EquipmentId, double CurrentStar)>>> UpdateBatchCurrentStarEquipmentsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Equipments>>> InsertBatchEquipmentsGalleryAsync(string userId, List<Equipments> equipments);
    Task<Equipments> GetEquipmentCollectionByIdAsync(string userId, string objectId);
    Task UpdateEquipmentGalleryPowerAsync(string userId, string Id, Equipments EquipmentFromDB);
    Task<Equipments> SumPowerEquipmentsGalleryAsync(string userId);
}