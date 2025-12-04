using System.Collections.Generic;
using System.Threading.Tasks;

public class ArtworksGalleryService : IArtworksGalleryService
{
    private IArtworksGalleryRepository _ArtworkGalleryRepository;

    public ArtworksGalleryService(IArtworksGalleryRepository ArtworkGalleryRepository)
    {
        _ArtworkGalleryRepository = ArtworkGalleryRepository;
    }

    public static ArtworksGalleryService Create()
    {
        return new ArtworksGalleryService(new ArtworksGalleryRepository());
    }

    public async Task<List<Artworks>> GetArtworksCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<Artworks> list = await _ArtworkGalleryRepository.GetArtworksCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtworksCountAsync(string type, string rare)
    {
        return await _ArtworkGalleryRepository.GetArtworksCountAsync(type, rare);
    }

    public async Task InsertArtworkGalleryAsync(string Id)
    {
        IArtworksRepository _repository = new ArtworksRepository();
        ArtworksService _service = new ArtworksService(_repository);
        await _ArtworkGalleryRepository.InsertArtworkGalleryAsync(Id, await _service.GetArtworkByIdAsync(Id));
    }

    public async Task UpdateStatusArtworkGalleryAsync(string Id)
    {
        await _ArtworkGalleryRepository.UpdateStatusArtworkGalleryAsync(Id);
    }

    public async Task<Artworks> SumPowerArtworksGalleryAsync()
    {
        return await _ArtworkGalleryRepository.SumPowerArtworksGalleryAsync();
    }

    public async Task UpdateStarArtworkGalleryAsync(string Id, double star)
    {
        await _ArtworkGalleryRepository.UpdateStarArtworkGalleryAsync(Id, star);
    }

    public async Task UpdateArtworkGalleryPowerAsync(string Id)
    {
        IArtworksRepository _repository = new ArtworksRepository();
        ArtworksService _service = new ArtworksService(_repository);
        await _ArtworkGalleryRepository.UpdateArtworkGalleryPowerAsync(Id, await _service.GetArtworkByIdAsync(Id));
    }
}
