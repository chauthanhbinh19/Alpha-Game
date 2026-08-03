using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmojisRepository
{
    Task<List<string>> GetUniqueEmojisIdAsync();
    Task<List<Emojis>> GetEmojisAsync(string search, string rare, int pageSize, int offset);
    Task<List<Emojis>> GetEmojisWithoutLimitAsync();
    Task<int> GetEmojisCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Emojis>> InsertEmojiAsync(Emojis entity);
    Task<InsertOrUpdateResult<Emojis>> UpdateEmojiAsync(Emojis entity);
    Task<List<Emojis>> GetEmojisWithPriceAsync(int pageSize, int offset);
    Task<int> GetEmojisWithPriceCountAsync();
    Task<Emojis> GetEmojiByIdAsync(string Id);
    Task<Emojis> SumPowerEmojisPercentAsync(string userId);
}
