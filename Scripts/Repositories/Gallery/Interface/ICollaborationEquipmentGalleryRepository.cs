using System.Collections.Generic;

public interface ICollaborationEquipmentGalleryRepository
{
    List<CollaborationEquipment> GetCollaborationEquipmentsCollection(string type, int pageSize, int offset, string rare);
    int GetCollaborationEquipmentCount(string type, string rare);
    void InsertCollaborationEquipmentsGallery(string Id, CollaborationEquipment collaborationEquipmentFromDB);
    void UpdateStatusCollaborationEquipmentsGallery(string Id);
    void UpdateStarCollaborationEquipmentsGallery(string Id, double star);
    void UpdateCollaborationEquipmentsGalleryPower(string Id, CollaborationEquipment CollaborationEquipmentFromDB);
    CollaborationEquipment SumPowerCollaborationEquipmentsGallery();
}