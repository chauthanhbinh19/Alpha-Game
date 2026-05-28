using System.Collections.Generic;
using System.Threading.Tasks;

public class CollaborationEquipmentsGalleryService : ICollaborationEquipmentsGalleryService
{
    private static CollaborationEquipmentsGalleryService _instance;
    private readonly ICollaborationEquipmentsGalleryRepository _collaborationEquipmentsGalleryRepository;

    public CollaborationEquipmentsGalleryService(ICollaborationEquipmentsGalleryRepository collaborationEquipmentsGalleryRepository)
    {
        _collaborationEquipmentsGalleryRepository = collaborationEquipmentsGalleryRepository;
    }

    public static CollaborationEquipmentsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CollaborationEquipmentsGalleryService(new CollaborationEquipmentsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<CollaborationEquipments>> GetCollaborationEquipmentsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> list = await _collaborationEquipmentsGalleryRepository.GetCollaborationEquipmentsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare)
    {
        return await _collaborationEquipmentsGalleryRepository.GetCollaborationEquipmentsCountAsync(search, type, rare);
    }

    public async Task InsertCollaborationEquipmentGalleryAsync(string Id)
    {
        ICollaborationEquipmentsRepository _repository = new CollaborationEquipmentsRepository();
        CollaborationEquipmentsService _service = new CollaborationEquipmentsService(_repository);
        await _collaborationEquipmentsGalleryRepository.InsertCollaborationEquipmentGalleryAsync(Id, await _service.GetCollaborationEquipmentByIdAsync(Id));
    }

    public async Task UpdateStatusCollaborationEquipmentGalleryAsync(string Id)
    {
        await _collaborationEquipmentsGalleryRepository.UpdateStatusCollaborationEquipmentGalleryAsync(Id);
    }

    public async Task<CollaborationEquipments> SumPowerCollaborationEquipmentsGalleryAsync()
    {
        return await _collaborationEquipmentsGalleryRepository.SumPowerCollaborationEquipmentsGalleryAsync();
    }

    public async Task UpdateStarCollaborationEquipmentGalleryAsync(string Id, double star)
    {
        await _collaborationEquipmentsGalleryRepository.UpdateStarCollaborationEquipmentGalleryAsync(Id, star);
    }

    public async Task UpdateCollaborationEquipmentGalleryPowerAsync(string Id)
    {
        ICollaborationEquipmentsRepository _repository = new CollaborationEquipmentsRepository();
        CollaborationEquipmentsService _service = new CollaborationEquipmentsService(_repository);
        await _collaborationEquipmentsGalleryRepository.UpdateCollaborationEquipmentGalleryPowerAsync(Id, await _service.GetCollaborationEquipmentByIdAsync(Id));
    }
}
