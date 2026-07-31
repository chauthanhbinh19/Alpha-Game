using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAlchemiesGalleryRepository
{
    Task<List<Alchemies>> GetAlchemiesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetAlchemyCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Alchemies>> InsertAlchemyGalleryAsync(string userId, string Id, Alchemies AlchemyFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusAlchemyGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusAlchemiesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarAlchemyGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarAlchemyGalleryAsync(string userId, string alchemyId);
    Task<InsertOrUpdateResult<List<(string AlchemyId, double CurrentStar)>>> UpdateBatchCurrentStarAlchemiesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Alchemies>>> InsertBatchAlchemiesGalleryAsync(string userId, List<Alchemies> alchemies);
    Task<Alchemies> GetAlchemyCollectionByIdAsync(string userId, string objectId);
    Task UpdateAlchemyGalleryPowerAsync(string userId, string Id, Alchemies AlchemyFromDB);
    Task<Alchemies> SumPowerAlchemyGalleryAsync(string userId);
}
