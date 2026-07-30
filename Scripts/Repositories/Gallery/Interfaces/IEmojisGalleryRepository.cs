using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmojisGalleryRepository
{
    Task<List<Emojis>> GetEmojisCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetEmojisCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Emojis>> InsertEmojiGalleryAsync(string userId, string Id, Emojis EmojiFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusEmojiGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusEmojisGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarEmojiGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarEmojiGalleryAsync(string userId, string emojiId);
    Task<InsertOrUpdateResult<List<(string EmojiId, double CurrentStar)>>> UpdateBatchCurrentStarEmojisGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Emojis>>> InsertBatchEmojisGalleryAsync(string userId, List<Emojis> emojis);
    Task<Emojis> GetEmojiCollectionByIdAsync(string userId, string objectId);
    Task UpdateEmojiGalleryPowerAsync(string userId, string id, Emojis EmojiFromDB);
    Task<Emojis> SumPowerEmojisGalleryAsync(string userId);
}