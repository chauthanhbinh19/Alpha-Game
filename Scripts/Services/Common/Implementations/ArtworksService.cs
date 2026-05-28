using System.Collections.Generic;
using System.Threading.Tasks;

public class ArtworksService : IArtworksService
{
    private static ArtworksService _instance;
    private readonly IArtworksRepository _artworksRepository;

    public ArtworksService(IArtworksRepository artworksRepository)
    {
        _artworksRepository = artworksRepository;
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
        return await _artworksRepository.GetUniqueArtworksTypesAsync();
    }

    public async Task<List<Artworks>> GetArtworksAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Artworks> list = await _artworksRepository.GetArtworksAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtworksCountAsync(string search, string type, string rare)
    {
        return await _artworksRepository.GetArtworksCountAsync(search, type, rare);
    }

    public async Task<List<Artworks>> GetArtworksWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Artworks> list = await _artworksRepository.GetArtworksWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtworksWithPriceCountAsync(string type)
    {
        return await _artworksRepository.GetArtworksWithPriceCountAsync(type);
    }

    public async Task<Artworks> GetArtworkByIdAsync(string Id)
    {
        return await _artworksRepository.GetArtworkByIdAsync(Id);
    }

    public async Task<Artworks> SumPowerArtworksPercentAsync()
    {
        return await _artworksRepository.SumPowerArtworksPercentAsync();
    }

    public async Task<List<string>> GetUniqueArtworksIdAsync()
    {
        return await _artworksRepository.GetUniqueArtworksIdAsync();
    }

    public async Task<List<Artworks>> GetArtworksWithoutLimitAsync()
    {
        return await _artworksRepository.GetArtworksWithoutLimitAsync();
    }
}