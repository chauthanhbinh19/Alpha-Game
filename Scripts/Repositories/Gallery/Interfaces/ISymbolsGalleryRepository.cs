using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISymbolsGalleryRepository
{
    Task<List<Symbols>> GetSymbolsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSymbolsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Symbols>> InsertSymbolGalleryAsync(string userId, string Id, Symbols SymbolFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusSymbolGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusSymbolsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarSymbolGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarSymbolGalleryAsync(string userId, string symbolId);
    Task<InsertOrUpdateResult<List<(string SymbolId, double CurrentStar)>>> UpdateBatchCurrentStarSymbolsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Symbols>>> InsertBatchSymbolsGalleryAsync(string userId, List<Symbols> symbols);
    Task<Symbols> GetSymbolCollectionByIdAsync(string userId, string objectId);
    Task UpdateSymbolGalleryPowerAsync(string userId, string Id, Symbols SymbolFromDB);
    Task<Symbols> SumPowerSymbolsGalleryAsync(string userId);
}