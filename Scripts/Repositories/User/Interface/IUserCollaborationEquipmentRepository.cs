using System.Collections.Generic;

public interface IUserCollaborationEquipmentRepository
{
    List<CollaborationEquipment> GetUserCollaborationEquipments(string user_id, string type, int pageSize, int offset);
    int GetUserCollaborationEquipmentCount(string user_id, string type);
    bool InsertUserCollaborationEquipments(CollaborationEquipment collaborationEquipment);
    bool UpdateCollaborationEquipmentsLevel(CollaborationEquipment collaborationEquipment, int cardLevel);
    bool UpdateCollaborationEquipmentsBreakthrough(CollaborationEquipment collaborationEquipment, int star, int quantity);
    CollaborationEquipment GetUserCollaborationEquipmentsById(string user_id, string Id);
    CollaborationEquipment SumPowerUserCollaborationEquipments();
}