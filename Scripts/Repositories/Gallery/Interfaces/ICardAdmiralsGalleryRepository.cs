using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardAdmiralsGalleryRepository
{
    Task<List<CardAdmirals>> GetCardAdmiralsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardAdmiralsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<CardAdmirals>> InsertCardAdmiralGalleryAsync(string userId, string Id, CardAdmirals CardAdmiralFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardAdmiralGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardAdmiralsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarCardAdmiralGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCardAdmiralGalleryAsync(string userId, string cardAdmiralId);
    Task<InsertOrUpdateResult<List<(string CardAdmiralId, double CurrentStar)>>> UpdateBatchCurrentStarCardAdmiralsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<CardAdmirals>>> InsertBatchCardAdmiralsGalleryAsync(string userId, List<CardAdmirals> cardAdmirals);
    Task<CardAdmirals> GetCardAdmiralCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardAdmiralGalleryPowerAsync(string userId, string Id, CardAdmirals CardAdmiralFromDB);
    Task<CardAdmirals> SumPowerCardAdmiralsGalleryAsync(string userId);
}