using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAlchemiesGalleryService
{
    Task<List<Alchemies>> GetAlchemiesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetAlchemyCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertAlchemyGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusAlchemyGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusAlchemiesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarAlchemyGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarAlchemyGalleryAsync(string userId, string alchemyId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarAlchemiesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchAlchemiesGalleryAsync(string userId, List<Alchemies> alchemies);
    Task<Alchemies> GetAlchemyCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateAlchemyGalleryPowerAsync(string userId, string Id);
    Task<Alchemies> SumPowerAlchemiesGalleryAsync(string userId);
}
