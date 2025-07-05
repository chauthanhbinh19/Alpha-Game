using System.Collections.Generic;

public interface ICollaborationEquipmentRepository
{
    List<string> GetUniqueCollaborationEquipmentTypes();
    List<string> GetUniqueCollaborationEquipmentId();
    List<CollaborationEquipment> GetCollaborationEquipments(string type, int pageSize, int offset);
    int GetCollaborationEquipmentCount(string type);
    List<CollaborationEquipment> GetCollaborationEquipmentsWithPrice(string type, int pageSize, int offset);
    int GetCollaborationEquipmentsWithPriceCount(string type);
    CollaborationEquipment GetCollaborationEquipmentsById(string Id);
}
