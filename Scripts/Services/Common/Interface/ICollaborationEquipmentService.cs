using System.Collections.Generic;

public interface ICollaborationEquipmentService
{
    List<string> GetUniqueCollaborationEquipmentTypes();
    List<CollaborationEquipment> GetCollaborationEquipments(string type, int pageSize, int offset);
    int GetCollaborationEquipmentCount(string type);
    List<CollaborationEquipment> GetCollaborationEquipmentsWithPrice(string type, int pageSize, int offset);
    int GetCollaborationEquipmentsWithPriceCount(string type);
    CollaborationEquipment GetCollaborationEquipmentsById(string Id);
}
