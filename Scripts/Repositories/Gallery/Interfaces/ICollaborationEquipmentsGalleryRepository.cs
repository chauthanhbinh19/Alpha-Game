using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationEquipmentsGalleryRepository
{
    Task<List<CollaborationEquipments>> GetCollaborationEquipmentsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare);
    Task InsertCollaborationEquipmentGalleryAsync(string userId, string Id, CollaborationEquipments CollaborationEquipmentFromDB);
    Task UpdateStatusCollaborationEquipmentGalleryAsync(string userId, string Id);
    Task UpdateStarCollaborationEquipmentGalleryAsync(string userId, string Id, double star);
    Task UpdateCollaborationEquipmentGalleryPowerAsync(string userId, string Id, CollaborationEquipments CollaborationEquipmentFromDB);
    Task<CollaborationEquipments> SumPowerCollaborationEquipmentsGalleryAsync(string userId);
}