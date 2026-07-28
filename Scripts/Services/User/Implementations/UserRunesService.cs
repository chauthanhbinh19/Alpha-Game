using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRunesService : IUserRunesService
{
    private static UserRunesService _instance;
    private readonly IUserRunesRepository _userRunesRepository;

    public UserRunesService(IUserRunesRepository userRunesRepository)
    {
        _userRunesRepository = userRunesRepository;
    }

    public static UserRunesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserRunesService(new UserRunesRepository());
        }
        return _instance;
    }

    public async Task<List<Runes>> GetUserRunesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Runes> list = await _userRunesRepository.GetUserRunesAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserRunesCountAsync(string userId, string search, string rare)
    {
        return await _userRunesRepository.GetUserRunesCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserRuneAsync(Runes rune, string userId)
    {
        var result = await _userRunesRepository.InsertUserRuneAsync(rune, userId);
        if (result)
        {
            await RunesGalleryService.Create().InsertRuneGalleryAsync(userId, rune.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserRuneLevelAsync(string userId, Runes rune)
    {
        return await _userRunesRepository.UpdateUserRuneLevelAsync(userId, rune);
    }

    public async Task<bool> UpdateUserRuneStarAsync(string userId, Runes rune)
    {
        var result = await _userRunesRepository.UpdateUserRuneStarAsync(userId, rune);
        if (result)
        {
            await RunesGalleryService.Create().UpdateStarRuneGalleryAsync(userId, rune.Id, rune.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserRuneBreakthroughAsync(string userId, Runes rune, int star, double quantity)
    {
        return await _userRunesRepository.UpdateUserRuneBreakthroughAsync(userId, rune, star, quantity);
    }

    public async Task<Runes> GetUserRuneByIdAsync(string userId, string Id)
    {
        return await _userRunesRepository.GetUserRuneByIdAsync(userId, Id);
    }

    public async Task<Runes> SumPowerUserRunesAsync(string userId)
    {
        return await _userRunesRepository.SumPowerUserRunesAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserRunesBatchAsync(string userId, List<Runes> runes)
    {
        return await _userRunesRepository.InsertOrUpdateUserRunesBatchAsync(userId, runes);
    }
}
