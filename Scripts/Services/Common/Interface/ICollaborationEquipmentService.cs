using System.Collections.Generic;

public interface ICollaborationEquipmentService
{
    List<string> GetUniqueCollaborationEquipmentTypes();
    List<string> GetUniqueCollaborationEquipmentId();
    List<CollaborationEquipments> GetCollaborationEquipments(string type, int pageSize, int offset, string rare);
    int GetCollaborationEquipmentCount(string type, string rare);
    List<CollaborationEquipments> GetCollaborationEquipmentsWithPrice(string type, int pageSize, int offset);
    int GetCollaborationEquipmentsWithPriceCount(string type);
    CollaborationEquipments GetCollaborationEquipmentsById(string Id);
}
