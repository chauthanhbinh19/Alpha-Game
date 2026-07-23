using System.Collections.Generic;
using System.Threading.Tasks;

public class CollaborationsGalleryService : ICollaborationsGalleryService
{
    private static CollaborationsGalleryService _instance;
    private readonly ICollaborationsGalleryRepository _collaborationsGalleryRepository;

    public CollaborationsGalleryService(ICollaborationsGalleryRepository collaborationsGalleryRepository)
    {
        _collaborationsGalleryRepository = collaborationsGalleryRepository;
    }

    public static CollaborationsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new CollaborationsGalleryService(new CollaborationsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Collaborations>> GetCollaborationsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Collaborations> list = await _collaborationsGalleryRepository.GetCollaborationsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetCollaborationsCountAsync(string search, string rare)
    {
        return await _collaborationsGalleryRepository.GetCollaborationsCountAsync(search, rare);
    }

    public async Task InsertCollaborationGalleryAsync(string userId, string Id)
    {
        ICollaborationsRepository _repository = new CollaborationsRepository();
        CollaborationsService _service = new CollaborationsService(_repository);
        await _collaborationsGalleryRepository.InsertCollaborationGalleryAsync(userId, Id, await _service.GetCollaborationByIdAsync(Id));
    }

    public async Task UpdateStatusCollaborationGalleryAsync(string userId, string Id)
    {
        await _collaborationsGalleryRepository.UpdateStatusCollaborationGalleryAsync(userId, Id);
    }

    public async Task<Collaborations> SumPowerCollaborationsGalleryAsync(string userId)
    {
        return await _collaborationsGalleryRepository.SumPowerCollaborationsGalleryAsync(userId);
    }

    public async Task UpdateStarCollaborationGalleryAsync(string userId, string Id, double star)
    {
        await _collaborationsGalleryRepository.UpdateStarCollaborationGalleryAsync(userId, Id, star);
    }

    public async Task UpdateCollaborationGalleryPowerAsync(string userId, string Id)
    {
        ICollaborationsRepository _repository = new CollaborationsRepository();
        CollaborationsService _service = new CollaborationsService(_repository);
        await _collaborationsGalleryRepository.UpdateCollaborationGalleryPowerAsync(userId, Id, await _service.GetCollaborationByIdAsync(Id));
    }
}
