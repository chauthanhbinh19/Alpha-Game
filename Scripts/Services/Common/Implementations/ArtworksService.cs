using System.Collections.Generic;
using System.Threading.Tasks;

public class ArtworksService : IArtworksService
{
    private static ArtworksService _instance;
    private readonly IArtworksRepository _artworkRepository;

    public ArtworksService(IArtworksRepository ArtworkRepository)
    {
        _artworkRepository = ArtworkRepository;
    }

    public static ArtworksService Create()
    {
        if (_instance == null)
        {
            _instance = new ArtworksService(new ArtworksRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueArtworksTypesAsync()
    {
        return await _artworkRepository.GetUniqueArtworksTypesAsync();
    }

    public async Task<List<Artworks>> GetArtworksAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Artworks> list = await _artworkRepository.GetArtworksAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtworksCountAsync(string search, string type, string rare)
    {
        return await _artworkRepository.GetArtworksCountAsync(search, type, rare);
    }

    public async Task<List<Artworks>> GetArtworksWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Artworks> list = await _artworkRepository.GetArtworksWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtworksWithPriceCountAsync(string type)
    {
        return await _artworkRepository.GetArtworksWithPriceCountAsync(type);
    }

    public async Task<Artworks> GetArtworkByIdAsync(string Id)
    {
        return await _artworkRepository.GetArtworkByIdAsync(Id);
    }

    public async Task<Artworks> SumPowerArtworksPercentAsync()
    {
        return await _artworkRepository.SumPowerArtworksPercentAsync();
    }

    public async Task<List<string>> GetUniqueArtworksIdAsync()
    {
        return await _artworkRepository.GetUniqueArtworksIdAsync();
    }

    public async Task<List<Artworks>> GetArtworksWithoutLimitAsync()
    {
        return await _artworkRepository.GetArtworksWithoutLimitAsync();
    }
}