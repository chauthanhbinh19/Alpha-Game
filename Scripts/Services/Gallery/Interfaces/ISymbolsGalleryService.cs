using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISymbolsGalleryService
{
    Task<List<Symbols>> GetSymbolsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSymbolsCountAsync(string search, string type, string rare);
    Task<bool> InsertSymbolGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusSymbolGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusSymbolsGalleryAsync(string userId);
    Task<bool> UpdateStarSymbolGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarSymbolGalleryAsync(string userId, string symbolId);
    Task<bool> UpdateBatchCurrentStarSymbolsGalleryAsync(string userId);
    Task<bool> InsertBatchSymbolsGalleryAsync(string userId, List<Symbols> symbols);
    Task<Symbols> GetSymbolCollectionByIdAsync(string userId, string objectId);
    Task UpdateSymbolGalleryPowerAsync(string userId, string Id);
    Task<Symbols> SumPowerSymbolsGalleryAsync(string userId);
}