using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardAdmiralsGalleryService
{
    Task<List<CardAdmirals>> GetCardAdmiralsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardAdmiralsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCardAdmiralGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardAdmiralGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardAdmiralsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCardAdmiralGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardAdmiralGalleryAsync(string userId, string cardAdmiralId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardAdmiralsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCardAdmiralsGalleryAsync(string userId, List<CardAdmirals> cardAdmirals);
    Task<CardAdmirals> GetCardAdmiralCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCardAdmiralGalleryPowerAsync(string userId, string Id);
    Task<CardAdmirals> SumPowerCardAdmiralsGalleryAsync(string userId);
}