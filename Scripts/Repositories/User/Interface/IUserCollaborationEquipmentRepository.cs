using System.Collections.Generic;

public interface IUserCollaborationEquipmentRepository
{
    List<CollaborationEquipments> GetUserCollaborationEquipments(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserCollaborationEquipmentCount(string user_id, string type, string rare);
    bool InsertUserCollaborationEquipments(CollaborationEquipments collaborationEquipment);
    bool UpdateCollaborationEquipmentsLevel(CollaborationEquipments collaborationEquipment, int cardLevel);
    bool UpdateCollaborationEquipmentsBreakthrough(CollaborationEquipments collaborationEquipment, int star, int quantity);
    CollaborationEquipments GetUserCollaborationEquipmentsById(string user_id, string Id);
    CollaborationEquipments SumPowerUserCollaborationEquipments();
}