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

    public async Task<List<CollaborationEquipments>> GetCollaborationEquipmentsAsync(string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> list = await _collaborationEquipmentRepository.GetCollaborationEquipmentsAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationEquipmentsCountAsync(string type, string rare)
    {
        return await _collaborationEquipmentRepository.GetCollaborationEquipmentsCountAsync(type, rare);
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
