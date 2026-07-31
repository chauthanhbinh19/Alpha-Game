using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmojisGalleryService
{
    Task<List<Emojis>> GetEmojisCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetEmojisCountAsync(string search, string rare);
    Task<bool> InsertEmojiGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusEmojiGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusEmojisGalleryAsync(string userId);
    Task<bool> UpdateTempStarEmojiGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarEmojiGalleryAsync(string userId, string emojiId);
    Task<bool> UpdateBatchCurrentStarEmojisGalleryAsync(string userId);
    Task<bool> InsertBatchEmojisGalleryAsync(string userId, List<Emojis> emojis);
    Task<Emojis> GetEmojiCollectionByIdAsync(string userId, string objectId);
    Task UpdateEmojiGalleryPowerAsync(string userId, string id);
    Task<Emojis> SumPowerEmojisGalleryAsync(string userId);
}