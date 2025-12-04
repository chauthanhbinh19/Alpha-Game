using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCollaborationEquipmentsService
{
    Task<CollaborationEquipments> GetNewLevelPowerAsync(CollaborationEquipments c, double coefficient);
    Task<CollaborationEquipments> GetNewBreakthroughPowerAsync(CollaborationEquipments c, double coefficient);
    Task<List<CollaborationEquipments>> GetUserCollaborationEquipmentsAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<int> GetUserCollaborationEquipmentsCountAsync(string user_id, string type, string rare);
    Task<bool> InsertUserCollaborationEquipmentAsync(CollaborationEquipments CollaborationEquipment, string userId);
    Task<bool> UpdateCollaborationEquipmentLevelAsync(CollaborationEquipments CollaborationEquipment, int cardLevel);
    Task<bool> UpdateCollaborationEquipmentBreakthroughAsync(CollaborationEquipments CollaborationEquipment, int star, double quantity);
    Task<CollaborationEquipments> GetUserCollaborationEquipmentByIdAsync(string user_id, string Id);
    Task<CollaborationEquipments> SumPowerUserCollaborationEquipmentsAsync();
}