using System.Collections.Generic;

public interface ICollaborationEquipmentGalleryService
{
    List<CollaborationEquipment> GetCollaborationEquipmentsCollection(string type, int pageSize, int offset, string rare);
    int GetCollaborationEquipmentCount(string type, string rare);
    void InsertCollaborationEquipmentsGallery(string Id);
    void UpdateStatusCollaborationEquipmentsGallery(string Id);
    void UpdateStarCollaborationEquipmentsGallery(string Id, double star);
    void UpdateCollaborationEquipmentsGalleryPower(string Id);
    CollaborationEquipment SumPowerCollaborationEquipmentsGallery();
}