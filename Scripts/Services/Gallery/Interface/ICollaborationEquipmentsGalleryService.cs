using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationEquipmentsGalleryService
{
    Task<List<CollaborationEquipments>> GetCollaborationEquipmentsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare);
    Task InsertCollaborationEquipmentGalleryAsync(string Id);
    Task UpdateStatusCollaborationEquipmentGalleryAsync(string Id);
    Task UpdateStarCollaborationEquipmentGalleryAsync(string Id, double star);
    Task UpdateCollaborationEquipmentGalleryPowerAsync(string Id);
    Task<CollaborationEquipments> SumPowerCollaborationEquipmentsGalleryAsync();
}