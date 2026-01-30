using System.Collections.Generic;
using System.Threading.Tasks;

public class ArtworksService : IArtworksService
{
    private readonly IArtworksRepository _ArtworkRepository;

    public ArtworksService(IArtworksRepository ArtworkRepository)
    {
        _ArtworkRepository = ArtworkRepository;
    }

    public static ArtworksService Create()
    {
        return new ArtworksService(new ArtworksRepository());
    }

    public async Task<List<string>> GetUniqueArtworksTypesAsync()
    {
        return await _ArtworkRepository.GetUniqueArtworksTypesAsync();
    }

    public async Task<List<Artworks>> GetArtworksAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Artworks> list = await _ArtworkRepository.GetArtworksAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtworksCountAsync(string search, string type, string rare)
    {
        return await _ArtworkRepository.GetArtworksCountAsync(search, type, rare);
    }

    public async Task<List<Artworks>> GetArtworksWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Artworks> list = await _ArtworkRepository.GetArtworksWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtworksWithPriceCountAsync(string type)
    {
        return await _ArtworkRepository.GetArtworksWithPriceCountAsync(type);
    }

    public async Task<Artworks> GetArtworkByIdAsync(string Id)
    {
        return await _ArtworkRepository.GetArtworkByIdAsync(Id);
    }

    public async Task<Artworks> SumPowerArtworksPercentAsync()
    {
        return await _ArtworkRepository.SumPowerArtworksPercentAsync();
    }

    public async Task<List<string>> GetUniqueArtworksIdAsync()
    {
        return await _ArtworkRepository.GetUniqueArtworksIdAsync();
    }
}