using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMagicFormationCirclesGalleryRepository
{
    Task<List<MagicFormationCircles>> GetMagicFormationCirclesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetMagicFormationCirclesCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<MagicFormationCircles>> InsertMagicFormationCircleGalleryAsync(string userId, string Id, MagicFormationCircles MagicFormationCircleFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusMagicFormationCircleGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusMagicFormationCirclesGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarMagicFormationCircleGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarMagicFormationCircleGalleryAsync(string userId, string magicFormationCircleId);
    Task<InsertOrUpdateResult<List<(string MagicFormationCircleId, double CurrentStar)>>> UpdateBatchCurrentStarMagicFormationCirclesGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<MagicFormationCircles>>> InsertBatchMagicFormationCirclesGalleryAsync(string userId, List<MagicFormationCircles> magicFormationCircles);
    Task<MagicFormationCircles> GetMagicFormationCircleCollectionByIdAsync(string userId, string objectId);
    Task UpdateMagicFormationCircleGalleryPowerAsync(string userId, string Id, MagicFormationCircles MagicFormationCircleFromDB);
    Task<MagicFormationCircles> SumPowerMagicFormationCirclesGalleryAsync(string userId);
}