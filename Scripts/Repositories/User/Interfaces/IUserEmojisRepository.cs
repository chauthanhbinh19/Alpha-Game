using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserEmojisRepository
{
    Task<List<Emojis>> GetUserEmojisAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserEmojisCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserEmojiAsync(Emojis emoji, string userId);
    Task<bool> InsertOrUpdateUserEmojisBatchAsync(string userId, List<Emojis> emojis);
    Task<bool> UpdateUserEmojiLevelAsync(string userId, Emojis emoji);
    Task<bool> UpdateUserEmojiStarAsync(string userId, Emojis emoji);
    Task<bool> UpdateUserEmojiBreakthroughAsync(string userId, Emojis emoji, int star, double quantity);
    Task<Emojis> GetUserEmojiByIdAsync(string userId, string Id);
    Task<Emojis> SumPowerUserEmojisAsync(string userId);
}