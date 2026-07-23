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

    public async Task<List<CollaborationEquipments>> GetCollaborationEquipmentsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> list = await _collaborationEquipmentsGalleryRepository.GetCollaborationEquipmentsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare)
    {
        return await _collaborationEquipmentsGalleryRepository.GetCollaborationEquipmentsCountAsync(search, type, rare);
    }

    public async Task InsertCollaborationEquipmentGalleryAsync(string userId, string Id)
    {
        ICollaborationEquipmentsRepository _repository = new CollaborationEquipmentsRepository();
        CollaborationEquipmentsService _service = new CollaborationEquipmentsService(_repository);
        await _collaborationEquipmentsGalleryRepository.InsertCollaborationEquipmentGalleryAsync(userId, Id, await _service.GetCollaborationEquipmentByIdAsync(Id));
    }

    public async Task UpdateStatusCollaborationEquipmentGalleryAsync(string userId, string Id)
    {
        await _collaborationEquipmentsGalleryRepository.UpdateStatusCollaborationEquipmentGalleryAsync(userId, Id);
    }

    public async Task<CollaborationEquipments> SumPowerCollaborationEquipmentsGalleryAsync(string userId)
    {
        return await _collaborationEquipmentsGalleryRepository.SumPowerCollaborationEquipmentsGalleryAsync(userId);
    }

    public async Task UpdateStarCollaborationEquipmentGalleryAsync(string userId, string Id, double star)
    {
        await _collaborationEquipmentsGalleryRepository.UpdateStarCollaborationEquipmentGalleryAsync(userId, Id, star);
    }

    public async Task UpdateCollaborationEquipmentGalleryPowerAsync(string userId, string Id)
    {
        ICollaborationEquipmentsRepository _repository = new CollaborationEquipmentsRepository();
        CollaborationEquipmentsService _service = new CollaborationEquipmentsService(_repository);
        await _collaborationEquipmentsGalleryRepository.UpdateCollaborationEquipmentGalleryPowerAsync(userId, Id, await _service.GetCollaborationEquipmentByIdAsync(Id));
    }
}
