using System.Collections.Generic;

public interface ICollaborationEquipmentService
{
    List<string> GetUniqueCollaborationEquipmentTypes();
    List<string> GetUniqueCollaborationEquipmentId();
    List<CollaborationEquipment> GetCollaborationEquipments(string type, int pageSize, int offset, string rare);
    int GetCollaborationEquipmentCount(string type, string rare);
    List<CollaborationEquipment> GetCollaborationEquipmentsWithPrice(string type, int pageSize, int offset);
    int GetCollaborationEquipmentsWithPriceCount(string type);
    CollaborationEquipment GetCollaborationEquipmentsById(string Id);
}
