using System.Collections.Generic;
using System.Threading.Tasks;

public class CollaborationEquipmentsService : ICollaborationEquipmentsService
{
    private readonly ICollaborationEquipmentsRepository _collaborationEquipmentRepository;

    public CollaborationEquipmentsService(ICollaborationEquipmentsRepository collaborationEquipmentRepository)
    {
        _collaborationEquipmentRepository = collaborationEquipmentRepository;
    }

    public static CollaborationEquipmentsService Create()
    {
        return new CollaborationEquipmentsService(new CollaborationEquipmentsRepository());
    }


    public async Task<List<string>> GetUniqueCollaborationEquipmentsTypesAsync()
    {
        return await _collaborationEquipmentRepository.GetUniqueCollaborationEquipmentsTypesAsync();
    }

    public async Task<List<CollaborationEquipments>> GetCollaborationEquipmentsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CollaborationEquipments> list = await _collaborationEquipmentRepository.GetCollaborationEquipmentsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare)
    {
        return await _collaborationEquipmentRepository.GetCollaborationEquipmentsCountAsync(search, type, rare);
    }

    public async Task<List<CollaborationEquipments>> GetCollaborationEquipmentsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CollaborationEquipments> list = await _collaborationEquipmentRepository.GetCollaborationEquipmentsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationEquipmentsWithPriceCountAsync(string type)
    {
        return await _collaborationEquipmentRepository.GetCollaborationEquipmentsWithPriceCountAsync(type);
    }

    public async Task<CollaborationEquipments> GetCollaborationEquipmentByIdAsync(string Id)
    {
        return await _collaborationEquipmentRepository.GetCollaborationEquipmentByIdAsync(Id);
    }

    public async Task<List<string>> GetUniqueCollaborationEquipmentsIdAsync()
    {
        return await _collaborationEquipmentRepository.GetUniqueCollaborationEquipmentsIdAsync();
    }
}
