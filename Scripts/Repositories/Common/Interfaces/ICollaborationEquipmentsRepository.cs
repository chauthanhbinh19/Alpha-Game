using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationEquipmentsRepository
{
    Task<List<string>> GetUniqueCollaborationEquipmentsTypesAsync();
    Task<List<string>> GetUniqueCollaborationEquipmentsIdAsync();
    Task<List<CollaborationEquipments>> GetCollaborationEquipmentsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare);
    Task<List<CollaborationEquipments>> GetCollaborationEquipmentsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCollaborationEquipmentsWithPriceCountAsync(string type);
    Task<CollaborationEquipments> GetCollaborationEquipmentByIdAsync(string id);
}
