using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationEquipmentsGalleryRepository
{
    Task<List<CollaborationEquipments>> GetCollaborationEquipmentsCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetCollaborationEquipmentsCountAsync(string type, string rare);
    Task InsertCollaborationEquipmentGalleryAsync(string Id, CollaborationEquipments CollaborationEquipmentFromDB);
    Task UpdateStatusCollaborationEquipmentGalleryAsync(string Id);
    Task UpdateStarCollaborationEquipmentGalleryAsync(string Id, double star);
    Task UpdateCollaborationEquipmentGalleryPowerAsync(string Id, CollaborationEquipments CollaborationEquipmentFromDB);
    Task<CollaborationEquipments> SumPowerCollaborationEquipmentsGalleryAsync();
}