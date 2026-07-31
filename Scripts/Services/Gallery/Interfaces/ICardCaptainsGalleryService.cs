using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardCaptainsGalleryService
{
    Task<List<CardCaptains>> GetCardCaptainsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardCaptainsCountAsync(string search, string type, string rare);
    Task<bool> InsertCardCaptainGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCardCaptainGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCardCaptainsGalleryAsync(string userId);
    Task<bool> UpdateTempStarCardCaptainGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarCardCaptainGalleryAsync(string userId, string cardCaptainId);
    Task<bool> UpdateBatchCurrentStarCardCaptainsGalleryAsync(string userId);
    Task<bool> InsertBatchCardCaptainsGalleryAsync(string userId, List<CardCaptains> cardCaptains);
    Task<CardCaptains> GetCardCaptainCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardCaptainGalleryPowerAsync(string userId, string Id);
    Task<CardCaptains> SumPowerCardCaptainsGalleryAsync(string userId);
}