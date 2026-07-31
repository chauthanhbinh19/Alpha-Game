using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCollaborationEquipmentsRepository
{
    Task<List<CollaborationEquipments>> GetUserCollaborationEquipmentsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserCollaborationEquipmentsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<CollaborationEquipments>> InsertOrUpdateUserCollaborationEquipmentAsync(string userId, CollaborationEquipments collaborationEquipment);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<CollaborationEquipments>>> InsertOrUpdateUserCollaborationEquipmentsBatchAsync(string userId, List<CollaborationEquipments> collaborationEquipments);
    Task<InsertOrUpdateResult<bool>> UpdateUserCollaborationEquipmentLevelAsync(string userId, CollaborationEquipments collaborationEquipment);
    Task<InsertOrUpdateResult<bool>> UpdateUserCollaborationEquipmentStarAsync(string userId, CollaborationEquipments collaborationEquipment);
    Task<CollaborationEquipments> GetUserCollaborationEquipmentByIdAsync(string userId, string Id);
    Task<CollaborationEquipments> SumPowerUserCollaborationEquipmentsAsync(string userId);
}