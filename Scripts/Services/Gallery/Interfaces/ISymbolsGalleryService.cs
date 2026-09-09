using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISymbolsGalleryService
{
    Task<List<Symbols>> GetSymbolsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSymbolsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertSymbolGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusSymbolGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusSymbolsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarSymbolGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarSymbolGalleryAsync(string userId, string symbolId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarSymbolsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchSymbolsGalleryAsync(string userId, List<Symbols> symbols);
    Task<Symbols> GetSymbolCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateSymbolGalleryPowerAsync(string userId, string Id);
    Task<Symbols> SumPowerSymbolsGalleryAsync(string userId);
}