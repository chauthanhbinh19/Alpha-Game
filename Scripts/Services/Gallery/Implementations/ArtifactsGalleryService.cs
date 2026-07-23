using System.Collections.Generic;
using System.Threading.Tasks;

public class ArtifactsGalleryService : IArtifactsGalleryService
{
    private static ArtifactsGalleryService _instance;
    private readonly IArtifactsGalleryRepository _artifactsGalleryRepository;

    public ArtifactsGalleryService(IArtifactsGalleryRepository artifactsGalleryRepository)
    {
        _artifactsGalleryRepository = artifactsGalleryRepository;
    }

    public static ArtifactsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new ArtifactsGalleryService(new ArtifactsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Artifacts>> GetArtifactsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Artifacts> list = await _artifactsGalleryRepository.GetArtifactsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtifactsCountAsync(string search, string rare)
    {
        return await _artifactsGalleryRepository.GetArtifactsCountAsync(search, rare);
    }

    public async Task InsertArtifactGalleryAsync(string userId, string Id)
    {
        IArtifactsRepository _repository = new ArtifactsRepository();
        ArtifactsService _service = new ArtifactsService(_repository);
        await _artifactsGalleryRepository.InsertArtifactGalleryAsync(userId, Id, await _service.GetArtifactByIdAsync(Id));
    }

    public async Task UpdateStatusArtifactGalleryAsync(string userId, string Id)
    {
        await _artifactsGalleryRepository.UpdateStatusArtifactGalleryAsync(userId, Id);
    }

    public async Task<Artifacts> SumPowerArtifactsGalleryAsync(string userId)
    {
        return await _artifactsGalleryRepository.SumPowerArtifactsGalleryAsync(userId);
    }

    public async Task UpdateStarArtifactGalleryAsync(string userId, string Id, double star)
    {
        await _artifactsGalleryRepository.UpdateStarArtifactGalleryAsync(userId, Id, star);
    }

    public async Task UpdateArtifactGalleryPowerAsync(string userId, string Id)
    {
        IArtifactsRepository _repository = new ArtifactsRepository();
        ArtifactsService _service = new ArtifactsService(_repository);
        await _artifactsGalleryRepository.UpdateArtifactGalleryPowerAsync(userId, Id, await _service.GetArtifactByIdAsync(Id));
    }
}
