using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMagicFormationCirclesGalleryService
{
    Task<List<MagicFormationCircles>> GetMagicFormationCirclesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetMagicFormationCirclesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertMagicFormationCircleGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusMagicFormationCircleGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusMagicFormationCirclesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarMagicFormationCircleGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarMagicFormationCircleGalleryAsync(string userId, string magicFormationCircleId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarMagicFormationCirclesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchMagicFormationCirclesGalleryAsync(string userId, List<MagicFormationCircles> magicFormationCircles);
    Task<MagicFormationCircles> GetMagicFormationCircleCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateMagicFormationCircleGalleryPowerAsync(string userId, string Id);
    Task<MagicFormationCircles> SumPowerMagicFormationCirclesGalleryAsync(string userId);
}