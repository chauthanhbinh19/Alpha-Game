using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardCaptainsGalleryService
{
    Task<List<CardCaptains>> GetCardCaptainsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardCaptainsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCardCaptainGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCardCaptainGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCardCaptainsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCardCaptainGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCardCaptainGalleryAsync(string userId, string cardCaptainId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCardCaptainsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCardCaptainsGalleryAsync(string userId, List<CardCaptains> cardCaptains);
    Task<CardCaptains> GetCardCaptainCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCardCaptainGalleryPowerAsync(string userId, string Id);
    Task<CardCaptains> SumPowerCardCaptainsGalleryAsync(string userId);
}