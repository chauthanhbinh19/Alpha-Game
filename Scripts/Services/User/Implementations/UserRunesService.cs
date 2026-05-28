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

    public async Task<List<Runes>> GetUserRunesAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Runes> list = await _userRunesRepository.GetUserRunesAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserRunesCountAsync(string user_id, string search, string rare)
    {
        return await _userRunesRepository.GetUserRunesCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserRuneAsync(Runes rune, string userId)
    {
        return await _userRunesRepository.InsertUserRuneAsync(rune, userId);
    }

    public async Task<bool> UpdateRuneLevelAsync(Runes rune, int level)
    {
        return await _userRunesRepository.UpdateRuneLevelAsync(rune, level);
    }

    public async Task<bool> UpdateRuneBreakthroughAsync(Runes rune, int star, double quantity)
    {
        return await _userRunesRepository.UpdateRuneBreakthroughAsync(rune, star, quantity);
    }

    public async Task<Runes> GetUserRuneByIdAsync(string user_id, string Id)
    {
        return await _userRunesRepository.GetUserRuneByIdAsync(user_id, Id);
    }

    public async Task<Runes> SumPowerUserRunesAsync()
    {
        return await _userRunesRepository.SumPowerUserRunesAsync();
    }

    public async Task<bool> InsertOrUpdateUserRunesBatchAsync(List<Runes> runes)
    {
        return await _userRunesRepository.InsertOrUpdateUserRunesBatchAsync(runes);
    }
}
