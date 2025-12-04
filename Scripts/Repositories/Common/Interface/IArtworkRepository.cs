using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtworksRepository
{
    Task<List<string>> GetUniqueArtworksTypesAsync();
    Task<List<string>> GetUniqueArtworksIdAsync();
    Task<List<Artworks>> GetArtworksAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetArtworksCountAsync(string type, string rare);
    Task<List<Artworks>> GetArtworksWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetArtworksWithPriceCountAsync(string type);
    Task<Artworks> GetArtworkByIdAsync(string id);
    Task<Artworks> SumPowerArtworksPercentAsync();
}