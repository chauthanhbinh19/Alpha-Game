using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserEmojisService
{
    Task<Emojis> GetNewLevelPowerAsync(Emojis c, double coefficient);
    Task<Emojis> GetNewBreakthroughPowerAsync(Emojis c, double coefficient);
    Task<List<Emojis>> GetUserEmojisAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserEmojisCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserEmojiAsync(Emojis Emojis, string userId);
    Task<bool> UpdateEmojiLevelAsync(Emojis Emojis, int cardLevel);
    Task<bool> UpdateEmojiBreakthroughAsync(Emojis Emojis, int star, double quantity);
    Task<Emojis> GetUserEmojiByIdAsync(string user_id, string Id);
    Task<Emojis> SumPowerUserEmojisAsync();
}