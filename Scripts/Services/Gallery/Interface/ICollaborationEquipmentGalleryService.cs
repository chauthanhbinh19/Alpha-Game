using System.Collections.Generic;

public interface ICollaborationEquipmentGalleryService
{
    List<CollaborationEquipment> GetCollaborationEquipmentsCollection(string type, int pageSize, int offset);
    int GetCollaborationEquipmentCount(string type);
    void InsertCollaborationEquipmentsGallery(string Id);
    void UpdateStatusCollaborationEquipmentsGallery(string Id);
    CollaborationEquipment SumPowerCollaborationEquipmentsGallery();
}