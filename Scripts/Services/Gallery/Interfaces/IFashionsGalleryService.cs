using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFashionsGalleryService
{
    Task<List<Fashions>> GetFashionsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetFashionsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertFashionGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusFashionGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusFashionsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarFashionGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarFashionGalleryAsync(string userId, string fashionId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarFashionsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchFashionsGalleryAsync(string userId, List<Fashions> fashions);
    Task<Fashions> GetFashionCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateFashionGalleryPowerAsync(string userId, string Id);
    Task<Fashions> SumPowerFashionsGalleryAsync(string userId);
}