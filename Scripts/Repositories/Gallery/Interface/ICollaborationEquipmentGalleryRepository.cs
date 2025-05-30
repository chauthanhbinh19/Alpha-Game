using System.Collections.Generic;

public interface ICollaborationEquipmentGalleryRepository
{
    List<CollaborationEquipment> GetCollaborationEquipmentsCollection(string type, int pageSize, int offset);
    int GetCollaborationEquipmentCount(string type);
    void InsertCollaborationEquipmentsGallery(string Id, CollaborationEquipment collaborationEquipmentFromDB);
    void UpdateStatusCollaborationEquipmentsGallery(string Id);
    CollaborationEquipment SumPowerCollaborationEquipmentsGallery();
}