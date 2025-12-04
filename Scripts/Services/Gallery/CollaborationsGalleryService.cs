using System.Collections.Generic;
using System.Threading.Tasks;

public class CollaborationsGalleryService : ICollaborationsGalleryService
{
    private readonly ICollaborationsGalleryRepository _collaborationGalleryRepository;

    public CollaborationsGalleryService(ICollaborationsGalleryRepository collaborationGalleryRepository)
    {
        _collaborationGalleryRepository = collaborationGalleryRepository;
    }

    public static CollaborationsGalleryService Create()
    {
        return new CollaborationsGalleryService(new CollaborationsGalleryRepository());
    }

    public async Task<List<Collaborations>> GetCollaborationsCollectionAsync(int pageSize, int offset, string rare)
    {
        List<Collaborations> list = await _collaborationGalleryRepository.GetCollaborationsCollectionAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationsCountAsync(string rare)
    {
        return await _collaborationGalleryRepository.GetCollaborationsCountAsync(rare);
    }

    public async Task InsertCollaborationGalleryAsync(string Id)
    {
        ICollaborationsRepository _repository = new CollaborationsRepository();
        CollaborationsService _service = new CollaborationsService(_repository);
        await _collaborationGalleryRepository.InsertCollaborationGalleryAsync(Id, await _service.GetCollaborationByIdAsync(Id));
    }

    public async Task UpdateStatusCollaborationGalleryAsync(string Id)
    {
        await _collaborationGalleryRepository.UpdateStatusCollaborationGalleryAsync(Id);
    }

    public async Task<Collaborations> SumPowerCollaborationsGalleryAsync()
    {
        return await _collaborationGalleryRepository.SumPowerCollaborationsGalleryAsync();
    }

    public async Task UpdateStarCollaborationGalleryAsync(string Id, double star)
    {
        await _collaborationGalleryRepository.UpdateStarCollaborationGalleryAsync(Id, star);
    }

    public async Task UpdateCollaborationGalleryPowerAsync(string Id)
    {
        ICollaborationsRepository _repository = new CollaborationsRepository();
        CollaborationsService _service = new CollaborationsService(_repository);
        await _collaborationGalleryRepository.UpdateCollaborationGalleryPowerAsync(Id, await _service.GetCollaborationByIdAsync(Id));
    }
}
