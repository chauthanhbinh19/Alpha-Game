using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationEquipmentsGalleryRepository
{
    Task<List<CollaborationEquipments>> GetCollaborationEquipmentsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<CollaborationEquipments>> InsertCollaborationEquipmentGalleryAsync(string userId, string Id, CollaborationEquipments CollaborationEquipmentFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCollaborationEquipmentGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCollaborationEquipmentsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarCollaborationEquipmentGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCollaborationEquipmentGalleryAsync(string userId, string collaborationEquipmentId);
    Task<InsertOrUpdateResult<List<(string CollaborationEquipmentId, double CurrentStar)>>> UpdateBatchCurrentStarCollaborationEquipmentsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<CollaborationEquipments>>> InsertBatchCollaborationEquipmentsGalleryAsync(string userId, List<CollaborationEquipments> collaborationEquipments);
    Task<CollaborationEquipments> GetCollaborationEquipmentCollectionByIdAsync(string userId, string objectId);
    Task UpdateCollaborationEquipmentGalleryPowerAsync(string userId, string Id, CollaborationEquipments CollaborationEquipmentFromDB);
    Task<CollaborationEquipments> SumPowerCollaborationEquipmentsGalleryAsync(string userId);
}