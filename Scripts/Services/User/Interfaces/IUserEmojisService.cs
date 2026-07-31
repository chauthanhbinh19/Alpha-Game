using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserEmojisService
{
    Task<List<Emojis>> GetUserEmojisAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserEmojisCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEmojiAsync(string userId, Emojis emoji);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEmojisBatchAsync(string userId, List<Emojis> emojis);
    Task<bool> UpdateUserEmojiLevelAsync(string userId, Emojis emoji);
    Task<bool> UpdateUserEmojiStarAsync(string userId, Emojis emoji);
    Task<Emojis> GetUserEmojiByIdAsync(string userId, string Id);
    Task<Emojis> SumPowerUserEmojisAsync(string userId);
}