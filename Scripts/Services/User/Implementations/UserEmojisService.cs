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

    public async Task<List<Emojis>> GetUserEmojisAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Emojis> list = await _userEmojisRepository.GetUserEmojisAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserEmojisCountAsync(string userId, string search, string rare)
    {
        return await _userEmojisRepository.GetUserEmojisCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserEmojiAsync(Emojis emoji, string userId)
    {
        var result = await _userEmojisRepository.InsertUserEmojiAsync(emoji, userId);
        if (result)
        {
            await EmojisGalleryService.Create().InsertEmojiGalleryAsync(userId, emoji.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserEmojiLevelAsync(string userId, Emojis emoji)
    {
        return await _userEmojisRepository.UpdateUserEmojiLevelAsync(userId, emoji);
    }

    public async Task<bool> UpdateUserEmojiStarAsync(string userId, Emojis emoji)
    {
        var result = await _userEmojisRepository.UpdateUserEmojiStarAsync(userId, emoji);
        if (result)
        {
            await EmojisGalleryService.Create().UpdateStarEmojiGalleryAsync(userId, emoji.Id, emoji.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserEmojiBreakthroughAsync(string userId, Emojis emoji, int star, double quantity)
    {
        return await _userEmojisRepository.UpdateUserEmojiBreakthroughAsync(userId, emoji, star, quantity);
    }

    public async Task<Emojis> GetUserEmojiByIdAsync(string userId, string Id)
    {
        return await _userEmojisRepository.GetUserEmojiByIdAsync(userId, Id);
    }

    public async Task<Emojis> SumPowerUserEmojisAsync(string userId)
    {
        return await _userEmojisRepository.SumPowerUserEmojisAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserEmojisBatchAsync(string userId, List<Emojis> emojis)
    {
        return await _userEmojisRepository.InsertOrUpdateUserEmojisBatchAsync(userId, emojis);
    }
}
