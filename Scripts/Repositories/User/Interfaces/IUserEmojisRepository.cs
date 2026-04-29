using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserEmojisRepository
{
    Task<List<Emojis>> GetUserEmojisAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserEmojisCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserEmojiAsync(Emojis emoji, string userId);
    Task<bool> InsertOrUpdateUserEmojisBatchAsync(List<Emojis> emojis);
    Task<bool> UpdateEmojiLevelAsync(Emojis emoji, int cardLevel);
    Task<bool> UpdateEmojiBreakthroughAsync(Emojis emoji, int star, double quantity);
    Task<Emojis> GetUserEmojiByIdAsync(string user_id, string Id);
    Task<Emojis> SumPowerUserEmojisAsync();
}