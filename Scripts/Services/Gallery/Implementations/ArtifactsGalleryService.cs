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

    public async Task<List<Artifacts>> GetArtifactsCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Artifacts> list = await _artifactsGalleryRepository.GetArtifactsCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtifactsCountAsync(string search, string rare)
    {
        return await _artifactsGalleryRepository.GetArtifactsCountAsync(search, rare);
    }

    public async Task InsertArtifactGalleryAsync(string Id)
    {
        IArtifactsRepository _repository = new ArtifactsRepository();
        ArtifactsService _service = new ArtifactsService(_repository);
        await _artifactsGalleryRepository.InsertArtifactGalleryAsync(Id, await _service.GetArtifactByIdAsync(Id));
    }

    public async Task UpdateStatusArtifactGalleryAsync(string Id)
    {
        await _artifactsGalleryRepository.UpdateStatusArtifactGalleryAsync(Id);
    }

    public async Task<Artifacts> SumPowerArtifactsGalleryAsync()
    {
        return await _artifactsGalleryRepository.SumPowerArtifactsGalleryAsync();
    }

    public async Task UpdateStarArtifactGalleryAsync(string Id, double star)
    {
        await _artifactsGalleryRepository.UpdateStarArtifactGalleryAsync(Id, star);
    }

    public async Task UpdateArtifactGalleryPowerAsync(string Id)
    {
        IArtifactsRepository _repository = new ArtifactsRepository();
        ArtifactsService _service = new ArtifactsService(_repository);
        await _artifactsGalleryRepository.UpdateArtifactGalleryPowerAsync(Id, await _service.GetArtifactByIdAsync(Id));
    }
}
