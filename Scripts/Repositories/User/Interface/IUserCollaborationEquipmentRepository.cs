using System.Collections.Generic;

public interface IUserCollaborationEquipmentRepository
{
    List<CollaborationEquipment> GetUserCollaborationEquipments(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserCollaborationEquipmentCount(string user_id, string type, string rare);
    bool InsertUserCollaborationEquipments(CollaborationEquipment collaborationEquipment);
    bool UpdateCollaborationEquipmentsLevel(CollaborationEquipment collaborationEquipment, int cardLevel);
    bool UpdateCollaborationEquipmentsBreakthrough(CollaborationEquipment collaborationEquipment, int star, int quantity);
    CollaborationEquipment GetUserCollaborationEquipmentsById(string user_id, string Id);
    CollaborationEquipment SumPowerUserCollaborationEquipments();
}