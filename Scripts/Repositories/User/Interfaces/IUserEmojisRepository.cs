using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserEmojisRepository
{
    Task<List<Emojis>> GetUserEmojisAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserEmojisCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Emojis>> InsertOrUpdateUserEmojiAsync(string userId, Emojis emoji);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Emojis>>> InsertOrUpdateUserEmojisBatchAsync(string userId, List<Emojis> emojis);
    Task<InsertOrUpdateResult<bool>> UpdateUserEmojiLevelAsync(string userId, Emojis emoji);
    Task<InsertOrUpdateResult<bool>> UpdateUserEmojiStarAsync(string userId, Emojis emoji);
    Task<Emojis> GetUserEmojiByIdAsync(string userId, string Id);
    Task<Emojis> SumPowerUserEmojisAsync(string userId);
}