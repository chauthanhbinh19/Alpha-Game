using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardAdmiralsGalleryService
{
    Task<List<CardAdmirals>> GetCardAdmiralsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardAdmiralsCountAsync(string search, string type, string rare);
    Task<bool> InsertCardAdmiralGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCardAdmiralGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCardAdmiralsGalleryAsync(string userId);
    Task<bool> UpdateTempStarCardAdmiralGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarCardAdmiralGalleryAsync(string userId, string cardAdmiralId);
    Task<bool> UpdateBatchCurrentStarCardAdmiralsGalleryAsync(string userId);
    Task<bool> InsertBatchCardAdmiralsGalleryAsync(string userId, List<CardAdmirals> cardAdmirals);
    Task<CardAdmirals> GetCardAdmiralCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardAdmiralGalleryPowerAsync(string userId, string Id);
    Task<CardAdmirals> SumPowerCardAdmiralsGalleryAsync(string userId);
}