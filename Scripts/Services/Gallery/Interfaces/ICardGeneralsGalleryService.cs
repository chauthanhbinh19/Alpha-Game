using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardGeneralsGalleryService
{
    Task<List<CardGenerals>> GetCardGeneralsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardGeneralsCountAsync(string search, string type, string rare);
    Task<bool> InsertCardGeneralGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCardGeneralGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCardGeneralsGalleryAsync(string userId);
    Task<bool> UpdateStarCardGeneralGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarCardGeneralGalleryAsync(string userId, string cardGeneralId);
    Task<bool> UpdateBatchCurrentStarCardGeneralsGalleryAsync(string userId);
    Task<bool> InsertBatchCardGeneralsGalleryAsync(string userId, List<CardGenerals> cardGenerals);
    Task<CardGenerals> GetCardGeneralCollectionByIdAsync(string userId, string objectId);
    Task UpdateCardGeneralGalleryPowerAsync(string userId, string Id);
    Task<CardGenerals> SumPowerCardGeneralsGalleryAsync(string userId);
}