using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmojisGalleryRepository
{
    Task<List<Emojis>> GetEmojisCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetEmojisCountAsync(string search, string rare);
    Task InsertEmojiGalleryAsync(string Id, Emojis EmojiFromDB);
    Task UpdateStatusEmojiGalleryAsync(string Id);
    Task UpdateStarEmojiGalleryAsync(string id, double star);
    Task UpdateEmojiGalleryPowerAsync(string id, Emojis EmojiFromDB);
    Task<Emojis> SumPowerEmojisGalleryAsync();
}