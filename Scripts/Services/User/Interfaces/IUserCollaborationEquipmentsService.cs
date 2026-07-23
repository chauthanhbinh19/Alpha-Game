using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCollaborationEquipmentsService
{
    Task<List<CollaborationEquipments>> GetUserCollaborationEquipmentsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserCollaborationEquipmentsCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserCollaborationEquipmentAsync(CollaborationEquipments collaborationEquipment, string userId);
    Task<bool> InsertOrUpdateUserCollaborationEquipmentsBatchAsync(string userId, List<CollaborationEquipments> collaborationEquipments);
    Task<bool> UpdateUserCollaborationEquipmentLevelAsync(string userId, CollaborationEquipments collaborationEquipment);
    Task<bool> UpdateUserCollaborationEquipmentStarAsync(string userId, CollaborationEquipments collaborationEquipment);
    Task<bool> UpdateUserCollaborationEquipmentBreakthroughAsync(string userId, CollaborationEquipments collaborationEquipment, int star, double quantity);
    Task<CollaborationEquipments> GetUserCollaborationEquipmentByIdAsync(string userId, string Id);
    Task<CollaborationEquipments> SumPowerUserCollaborationEquipmentsAsync(string userId);
}