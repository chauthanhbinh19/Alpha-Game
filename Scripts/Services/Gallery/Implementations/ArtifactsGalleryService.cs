using System.Collections.Generic;
using System.Threading.Tasks;

public class ArtifactsGalleryService : IArtifactsGalleryService
{
    private static ArtifactsGalleryService _instance;
    private readonly IArtifactsGalleryRepository _ArtifactsGalleryRepository;

    public ArtifactsGalleryService(IArtifactsGalleryRepository ArtifactsGalleryRepository)
    {
        _ArtifactsGalleryRepository = ArtifactsGalleryRepository;
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
        List<Artifacts> list = await _ArtifactsGalleryRepository.GetArtifactsCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtifactsCountAsync(string search, string rare)
    {
        return await _ArtifactsGalleryRepository.GetArtifactsCountAsync(search, rare);
    }

    public async Task InsertArtifactGalleryAsync(string Id)
    {
        IArtifactsRepository _repository = new ArtifactsRepository();
        ArtifactsService _service = new ArtifactsService(_repository);
        await _ArtifactsGalleryRepository.InsertArtifactGalleryAsync(Id, await _service.GetArtifactByIdAsync(Id));
    }

    public async Task UpdateStatusArtifactGalleryAsync(string Id)
    {
        await _ArtifactsGalleryRepository.UpdateStatusArtifactGalleryAsync(Id);
    }

    public async Task<Artifacts> SumPowerArtifactsGalleryAsync()
    {
        return await _ArtifactsGalleryRepository.SumPowerArtifactsGalleryAsync();
    }

    public async Task UpdateStarArtifactGalleryAsync(string Id, double star)
    {
        await _ArtifactsGalleryRepository.UpdateStarArtifactGalleryAsync(Id, star);
    }

    public async Task UpdateArtifactGalleryPowerAsync(string Id)
    {
        IArtifactsRepository _repository = new ArtifactsRepository();
        ArtifactsService _service = new ArtifactsService(_repository);
        await _ArtifactsGalleryRepository.UpdateArtifactGalleryPowerAsync(Id, await _service.GetArtifactByIdAsync(Id));
    }
}
