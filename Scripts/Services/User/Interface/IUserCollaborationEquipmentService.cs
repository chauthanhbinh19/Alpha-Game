using System.Collections.Generic;

public interface IUserCollaborationEquipmentService
{
    CollaborationEquipments GetNewLevelPower(CollaborationEquipments c, double coefficient);
    CollaborationEquipments GetNewBreakthroughPower(CollaborationEquipments c, double coefficient);
    List<CollaborationEquipments> GetUserCollaborationEquipments(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserCollaborationEquipmentCount(string user_id, string type, string rare);
    bool InsertUserCollaborationEquipments(CollaborationEquipments collaborationEquipment, string userId);
    bool UpdateCollaborationEquipmentsLevel(CollaborationEquipments collaborationEquipment, int cardLevel);
    bool UpdateCollaborationEquipmentsBreakthrough(CollaborationEquipments collaborationEquipment, int star, double quantity);
    CollaborationEquipments GetUserCollaborationEquipmentsById(string user_id, string Id);
    CollaborationEquipments SumPowerUserCollaborationEquipments();
}