using System.Collections.Generic;
using System.Threading.Tasks;

public class UserEmojisService : IUserEmojisService
{
     private static UserEmojisService _instance;
    private readonly IUserEmojisRepository _userEmojisRepository;

    public UserEmojisService(IUserEmojisRepository userEmojisRepository)
    {
        _userEmojisRepository = userEmojisRepository;
    }

    public static UserEmojisService Create()
    {
        if (_instance == null)
        {
            _instance = new UserEmojisService(new UserEmojisRepository());
        }
        return _instance;
    }

    public async Task<List<Emojis>> GetUserEmojisAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Emojis> list = await _userEmojisRepository.GetUserEmojisAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserEmojisCountAsync(string user_id, string search, string rare)
    {
        return await _userEmojisRepository.GetUserEmojisCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserEmojiAsync(Emojis emoji, string userId)
    {
        return await _userEmojisRepository.InsertUserEmojiAsync(emoji, userId);
    }

    public async Task<bool> UpdateEmojiLevelAsync(Emojis emoji, int level)
    {
        return await _userEmojisRepository.UpdateEmojiLevelAsync(emoji, level);
    }

    public async Task<bool> UpdateEmojiBreakthroughAsync(Emojis emoji, int star, double quantity)
    {
        return await _userEmojisRepository.UpdateEmojiBreakthroughAsync(emoji, star, quantity);
    }

    public async Task<Emojis> GetUserEmojiByIdAsync(string user_id, string Id)
    {
        return await _userEmojisRepository.GetUserEmojiByIdAsync(user_id, Id);
    }

    public async Task<Emojis> SumPowerUserEmojisAsync()
    {
        return await _userEmojisRepository.SumPowerUserEmojisAsync();
    }

    public async Task<bool> InsertOrUpdateUserEmojisBatchAsync(List<Emojis> emojis)
    {
        return await _userEmojisRepository.InsertOrUpdateUserEmojisBatchAsync(emojis);
    }
}
