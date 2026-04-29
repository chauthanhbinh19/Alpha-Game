using System.Collections.Generic;
using System.Threading.Tasks;

public class CollaborationEquipmentsService : ICollaborationEquipmentsService
{
    private static CollaborationEquipmentsService _instance;
    private readonly ICollaborationEquipmentsRepository _collaborationEquipmentsRepository;

    public CollaborationEquipmentsService(ICollaborationEquipmentsRepository collaborationEquipmentsRepository)
    {
        _collaborationEquipmentsRepository = collaborationEquipmentsRepository;
    }

    public static CollaborationEquipmentsService Create()
    {
        if (_instance == null)
        {
            _instance = new CollaborationEquipmentsService(new CollaborationEquipmentsRepository());
        }
        return _instance;
    }


    public async Task<List<string>> GetUniqueCollaborationEquipmentsTypesAsync()
    {
        return await _collaborationEquipmentsRepository.GetUniqueCollaborationEquipmentsTypesAsync();
    }

    public async Task<List<CollaborationEquipments>> GetCollaborationEquipmentsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<CollaborationEquipments> list = await _collaborationEquipmentsRepository.GetCollaborationEquipmentsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare)
    {
        return await _collaborationEquipmentsRepository.GetCollaborationEquipmentsCountAsync(search, type, rare);
    }

    public async Task<List<CollaborationEquipments>> GetCollaborationEquipmentsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<CollaborationEquipments> list = await _collaborationEquipmentsRepository.GetCollaborationEquipmentsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationEquipmentsWithPriceCountAsync(string type)
    {
        return await _collaborationEquipmentsRepository.GetCollaborationEquipmentsWithPriceCountAsync(type);
    }

    public async Task<CollaborationEquipments> GetCollaborationEquipmentByIdAsync(string Id)
    {
        return await _collaborationEquipmentsRepository.GetCollaborationEquipmentByIdAsync(Id);
    }

    public async Task<List<string>> GetUniqueCollaborationEquipmentsIdAsync()
    {
        return await _collaborationEquipmentsRepository.GetUniqueCollaborationEquipmentsIdAsync();
    }
}
