using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITitlesGalleryService
{
    Task<List<Titles>> GetTitlesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetTitlesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertTitleGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusTitleGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusTitlesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarTitleGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarTitleGalleryAsync(string userId, string titleId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarTitlesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchTitlesGalleryAsync(string userId, List<Titles> titles);
    Task<Titles> GetTitleCollectionByIdAsync(string userId, string titleId);
    Task<InsertOrUpdateResult<bool>> UpdateTitleGalleryPowerAsync(string userId, string id);
    Task<Titles> SumPowerTitlesGalleryAsync(string userId);
}