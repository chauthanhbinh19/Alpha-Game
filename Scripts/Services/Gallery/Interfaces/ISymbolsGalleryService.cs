using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISymbolsGalleryService
{
    Task<List<Symbols>> GetSymbolsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSymbolsCountAsync(string search, string type, string rare);
    Task InsertSymbolGalleryAsync(string userId, string Id);
    Task UpdateStatusSymbolGalleryAsync(string userId, string Id);
    Task UpdateStarSymbolGalleryAsync(string userId, string Id, double star);
    Task UpdateSymbolGalleryPowerAsync(string userId, string Id);
    Task<Symbols> SumPowerSymbolsGalleryAsync(string userId);
}