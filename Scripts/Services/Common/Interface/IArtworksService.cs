using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArtworksService
{
    Task<List<string>> GetUniqueArtworksTypesAsync();
    Task<List<string>> GetUniqueArtworksIdAsync();
    Task<List<Artworks>> GetArtworksAsync(string search, string type, string rare, int pageSize, int offset);
    Task<int> GetArtworksCountAsync(string search, string type, string rare);
    Task<List<Artworks>> GetArtworksWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetArtworksWithPriceCountAsync(string type);
    Task<Artworks> GetArtworkByIdAsync(string id);
    Task<Artworks> SumPowerArtworksPercentAsync();
}