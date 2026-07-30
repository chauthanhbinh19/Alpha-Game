using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMagicFormationCirclesGalleryService
{
    Task<List<MagicFormationCircles>> GetMagicFormationCirclesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetMagicFormationCirclesCountAsync(string search, string type, string rare);
    Task<bool> InsertMagicFormationCircleGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusMagicFormationCircleGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusMagicFormationCirclesGalleryAsync(string userId);
    Task<bool> UpdateStarMagicFormationCircleGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarMagicFormationCircleGalleryAsync(string userId, string magicFormationCircleId);
    Task<bool> UpdateBatchCurrentStarMagicFormationCirclesGalleryAsync(string userId);
    Task<bool> InsertBatchMagicFormationCirclesGalleryAsync(string userId, List<MagicFormationCircles> magicFormationCircles);
    Task<MagicFormationCircles> GetMagicFormationCircleCollectionByIdAsync(string userId, string objectId);
    Task UpdateMagicFormationCircleGalleryPowerAsync(string userId, string Id);
    Task<MagicFormationCircles> SumPowerMagicFormationCirclesGalleryAsync(string userId);
}