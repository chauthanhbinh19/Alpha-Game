using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationEquipmentsGalleryService
{
    Task<List<CollaborationEquipments>> GetCollaborationEquipmentsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCollaborationEquipmentGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCollaborationEquipmentGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCollaborationEquipmentsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCollaborationEquipmentGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCollaborationEquipmentGalleryAsync(string userId, string collaborationEquipmentId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCollaborationEquipmentsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCollaborationEquipmentsGalleryAsync(string userId, List<CollaborationEquipments> collaborationEquipments);
    Task<CollaborationEquipments> GetCollaborationEquipmentCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCollaborationEquipmentGalleryPowerAsync(string userId, string Id);
    Task<CollaborationEquipments> SumPowerCollaborationEquipmentsGalleryAsync(string userId);
}