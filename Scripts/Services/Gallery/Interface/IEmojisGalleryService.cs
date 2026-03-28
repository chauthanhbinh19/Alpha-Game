using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmojisGalleryService
{
    Task<List<Emojis>> GetEmojisCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetEmojisCountAsync(string search, string rare);
    Task InsertEmojiGalleryAsync(string Id);
    Task UpdateStatusEmojiGalleryAsync(string Id);
    Task UpdateStarEmojiGalleryAsync(string id, double star);
    Task UpdateEmojiGalleryPowerAsync(string id);
    Task<Emojis> SumPowerEmojisGalleryAsync();
}