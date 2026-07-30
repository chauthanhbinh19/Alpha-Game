using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITitlesGalleryRepository
{
    Task<List<Titles>> GetTitlesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetTitlesCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Titles>> InsertTitleGalleryAsync(string userId, string Id, Titles TitleFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusTitleGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusTitlesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarTitleGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarTitleGalleryAsync(string userId, string titleId);
    Task<InsertOrUpdateResult<List<(string TitleId, double CurrentStar)>>> UpdateBatchCurrentStarTitlesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Titles>>> InsertBatchTitlesGalleryAsync(string userId, List<Titles> titles);
    Task<Titles> GetTitleCollectionByIdAsync(string userId, string objectId);
    Task UpdateTitleGalleryPowerAsync(string userId, string id, Titles TitleFromDB);
    Task<Titles> SumPowerTitlesGalleryAsync(string userId);
}