using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmojisGalleryService
{
    Task<List<Emojis>> GetEmojisCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetEmojisCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertEmojiGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusEmojiGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusEmojisGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarEmojiGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarEmojiGalleryAsync(string userId, string emojiId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarEmojisGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchEmojisGalleryAsync(string userId, List<Emojis> emojis);
    Task<Emojis> GetEmojiCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateEmojiGalleryPowerAsync(string userId, string id);
    Task<Emojis> SumPowerEmojisGalleryAsync(string userId);
}