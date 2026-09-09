using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMedalsGalleryService
{
    Task<List<Medals>> GetMedalsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetMedalsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertMedalGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusMedalGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusMedalsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarMedalGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarMedalGalleryAsync(string userId, string medalId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarMedalsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchMedalsGalleryAsync(string userId, List<Medals> medals);
    Task<Medals> GetMedalCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateMedalGalleryPowerAsync(string userId, string id);
    Task<Medals> SumPowerMedalsGalleryAsync(string userId);
}