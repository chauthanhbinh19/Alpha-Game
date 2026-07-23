using System.Collections.Generic;
using System.Threading.Tasks;

public class ArtworksGalleryService : IArtworksGalleryService
{
    private static ArtworksGalleryService _instance;
    private readonly IArtworksGalleryRepository _artworksGalleryRepository;

    public ArtworksGalleryService(IArtworksGalleryRepository artworksGalleryRepository)
    {
        _artworksGalleryRepository = artworksGalleryRepository;
    }

    public static ArtworksGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new ArtworksGalleryService(new ArtworksGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Artworks>> GetArtworksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Artworks> list = await _artworksGalleryRepository.GetArtworksCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtworksCountAsync(string search, string type, string rare)
    {
        return await _artworksGalleryRepository.GetArtworksCountAsync(search, type, rare);
    }

    public async Task InsertArtworkGalleryAsync(string userId, string Id)
    {
        IArtworksRepository _repository = new ArtworksRepository();
        ArtworksService _service = new ArtworksService(_repository);
        await _artworksGalleryRepository.InsertArtworkGalleryAsync(userId, Id, await _service.GetArtworkByIdAsync(Id));
    }

    public async Task UpdateStatusArtworkGalleryAsync(string userId, string Id)
    {
        await _artworksGalleryRepository.UpdateStatusArtworkGalleryAsync(userId, Id);
    }

    public async Task<Artworks> SumPowerArtworksGalleryAsync(string userId)
    {
        return await _artworksGalleryRepository.SumPowerArtworksGalleryAsync(userId);
    }

    public async Task UpdateStarArtworkGalleryAsync(string userId, string Id, double star)
    {
        await _artworksGalleryRepository.UpdateStarArtworkGalleryAsync(userId, Id, star);
    }

    public async Task UpdateArtworkGalleryPowerAsync(string userId, string Id)
    {
        IArtworksRepository _repository = new ArtworksRepository();
        ArtworksService _service = new ArtworksService(_repository);
        await _artworksGalleryRepository.UpdateArtworkGalleryPowerAsync(userId, Id, await _service.GetArtworkByIdAsync(Id));
    }
}
