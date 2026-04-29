using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCollaborationEquipmentsRepository
{
    Task<List<CollaborationEquipments>> GetUserCollaborationEquipmentsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserCollaborationEquipmentsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserCollaborationEquipmentAsync(CollaborationEquipments collaborationEquipment, string userId);
    Task<bool> InsertOrUpdateUserCollaborationEquipmentsBatchAsync(List<CollaborationEquipments> collaborationEquipments);
    Task<bool> UpdateCollaborationEquipmentLevelAsync(CollaborationEquipments collaborationEquipment, int level);
    Task<bool> UpdateCollaborationEquipmentBreakthroughAsync(CollaborationEquipments collaborationEquipment, int star, double quantity);
    Task<CollaborationEquipments> GetUserCollaborationEquipmentByIdAsync(string user_id, string Id);
    Task<CollaborationEquipments> SumPowerUserCollaborationEquipmentsAsync();
}