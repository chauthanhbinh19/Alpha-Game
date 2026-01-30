using System.Collections.Generic;
using System.Threading.Tasks;

public class CollaborationEquipmentsGalleryService : ICollaborationEquipmentsGalleryService
{
    private readonly ICollaborationEquipmentsGalleryRepository _collabEquipmentGalleryRepo;

    public CollaborationEquipmentsGalleryService(ICollaborationEquipmentsGalleryRepository collabEquipmentGalleryRepo)
    {
        _collabEquipmentGalleryRepo = collabEquipmentGalleryRepo;
    }

    public static CollaborationEquipmentsGalleryService Create()
    {
        return new CollaborationEquipmentsGalleryService(new CollaborationEquipmentsGalleryRepository());
    }

    public async Task<List<CollaborationEquipments>> GetCollaborationEquipmentsCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> list = await _collabEquipmentGalleryRepo.GetCollaborationEquipmentsCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationEquipmentsCountAsync(string search, string type, string rare)
    {
        return await _collabEquipmentGalleryRepo.GetCollaborationEquipmentsCountAsync(search, type, rare);
    }

    public async Task InsertCollaborationEquipmentGalleryAsync(string Id)
    {
        ICollaborationEquipmentsRepository _repository = new CollaborationEquipmentsRepository();
        CollaborationEquipmentsService _service = new CollaborationEquipmentsService(_repository);
        await _collabEquipmentGalleryRepo.InsertCollaborationEquipmentGalleryAsync(Id, await _service.GetCollaborationEquipmentByIdAsync(Id));
    }

    public async Task UpdateStatusCollaborationEquipmentGalleryAsync(string Id)
    {
        await _collabEquipmentGalleryRepo.UpdateStatusCollaborationEquipmentGalleryAsync(Id);
    }

    public async Task<CollaborationEquipments> SumPowerCollaborationEquipmentsGalleryAsync()
    {
        return await _collabEquipmentGalleryRepo.SumPowerCollaborationEquipmentsGalleryAsync();
    }

    public async Task UpdateStarCollaborationEquipmentGalleryAsync(string Id, double star)
    {
        await _collabEquipmentGalleryRepo.UpdateStarCollaborationEquipmentGalleryAsync(Id, star);
    }

    public async Task UpdateCollaborationEquipmentGalleryPowerAsync(string Id)
    {
        ICollaborationEquipmentsRepository _repository = new CollaborationEquipmentsRepository();
        CollaborationEquipmentsService _service = new CollaborationEquipmentsService(_repository);
        await _collabEquipmentGalleryRepo.UpdateCollaborationEquipmentGalleryPowerAsync(Id, await _service.GetCollaborationEquipmentByIdAsync(Id));
    }
}
