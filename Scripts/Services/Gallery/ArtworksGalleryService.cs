using System.Collections.Generic;
using System.Threading.Tasks;

public class ArtworksGalleryService : IArtworksGalleryService
{
    private static ArtworksGalleryService _instance;
    private IArtworksGalleryRepository _artworksGalleryRepository;

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

    public async Task<List<Artworks>> GetArtworksCollectionAsync(string search, string type, int pageSize, int offset, string rare)
    {
        List<Artworks> list = await _artworksGalleryRepository.GetArtworksCollectionAsync(search, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtworksCountAsync(string search, string type, string rare)
    {
        return await _artworksGalleryRepository.GetArtworksCountAsync(search, type, rare);
    }

    public async Task InsertArtworkGalleryAsync(string Id)
    {
        IArtworksRepository _repository = new ArtworksRepository();
        ArtworksService _service = new ArtworksService(_repository);
        await _artworksGalleryRepository.InsertArtworkGalleryAsync(Id, await _service.GetArtworkByIdAsync(Id));
    }

    public async Task UpdateStatusArtworkGalleryAsync(string Id)
    {
        await _artworksGalleryRepository.UpdateStatusArtworkGalleryAsync(Id);
    }

    public async Task<Artworks> SumPowerArtworksGalleryAsync()
    {
        return await _artworksGalleryRepository.SumPowerArtworksGalleryAsync();
    }

    public async Task UpdateStarArtworkGalleryAsync(string Id, double star)
    {
        await _artworksGalleryRepository.UpdateStarArtworkGalleryAsync(Id, star);
    }

    public async Task UpdateArtworkGalleryPowerAsync(string Id)
    {
        IArtworksRepository _repository = new ArtworksRepository();
        ArtworksService _service = new ArtworksService(_repository);
        await _artworksGalleryRepository.UpdateArtworkGalleryPowerAsync(Id, await _service.GetArtworkByIdAsync(Id));
    }
}
