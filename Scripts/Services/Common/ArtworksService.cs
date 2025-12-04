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

    public async Task<List<Artworks>> GetArtworksAsync(string type, int pageSize, int offset, string rare)
    {
        List<Artworks> list = await _ArtworkRepository.GetArtworksAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtworksCountAsync(string type, string rare)
    {
        return await _ArtworkRepository.GetArtworksCountAsync(type, rare);
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