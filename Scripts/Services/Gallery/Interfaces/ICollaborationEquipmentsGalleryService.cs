using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationEquipmentsGalleryService
{
    Task<List<CollaborationEquipments>> GetCollaborationEquipmentsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare);
    Task<bool> InsertCollaborationEquipmentGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCollaborationEquipmentGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCollaborationEquipmentsGalleryAsync(string userId);
    Task<bool> UpdateStarCollaborationEquipmentGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarCollaborationEquipmentGalleryAsync(string userId, string collaborationEquipmentId);
    Task<bool> UpdateBatchCurrentStarCollaborationEquipmentsGalleryAsync(string userId);
    Task<bool> InsertBatchCollaborationEquipmentsGalleryAsync(string userId, List<CollaborationEquipments> collaborationEquipments);
    Task<CollaborationEquipments> GetCollaborationEquipmentCollectionByIdAsync(string userId, string objectId);
    Task UpdateCollaborationEquipmentGalleryPowerAsync(string userId, string Id);
    Task<CollaborationEquipments> SumPowerCollaborationEquipmentsGalleryAsync(string userId);
}