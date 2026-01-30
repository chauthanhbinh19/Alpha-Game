using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISymbolsGalleryService
{
    Task<List<Symbols>> GetSymbolsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSymbolsCountAsync(string search, string type, string rare);
    Task InsertSymbolGalleryAsync(string Id);
    Task UpdateStatusSymbolGalleryAsync(string Id);
    Task UpdateStarSymbolGalleryAsync(string Id, double star);
    Task UpdateSymbolGalleryPowerAsync(string Id);
    Task<Symbols> SumPowerSymbolsGalleryAsync();
}