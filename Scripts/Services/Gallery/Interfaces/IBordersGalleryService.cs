using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBordersGalleryService
{
    Task<List<Borders>> GetBordersCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBordersCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertBorderGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusBorderGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBordersGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarBorderGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarBorderGalleryAsync(string userId, string borderId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarBordersGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchBordersGalleryAsync(string userId, List<Borders> borders);
    Task<Borders> GetBorderCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateBorderGalleryPowerAsync(string userId, string id);
    Task<Borders> SumPowerBordersGalleryAsync(string userId);
}