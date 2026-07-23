using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmojisGalleryService
{
    Task<List<Emojis>> GetEmojisCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetEmojisCountAsync(string search, string rare);
    Task InsertEmojiGalleryAsync(string userId, string Id);
    Task UpdateStatusEmojiGalleryAsync(string userId, string Id);
    Task UpdateStarEmojiGalleryAsync(string userId, string id, double star);
    Task UpdateEmojiGalleryPowerAsync(string userId, string id);
    Task<Emojis> SumPowerEmojisGalleryAsync(string userId);
}