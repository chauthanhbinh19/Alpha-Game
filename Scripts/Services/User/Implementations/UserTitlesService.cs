using System.Collections.Generic;
using System.Threading.Tasks;

public class UserTitlesService : IUserTitlesService
{
    private static UserTitlesService _instance;
    private readonly IUserTitlesRepository _userTitlesRepository;

    public UserTitlesService(IUserTitlesRepository userTitlesRepository)
    {
        _userTitlesRepository = userTitlesRepository;
    }

    public static UserTitlesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserTitlesService(new UserTitlesRepository());
        }
        return _instance;
    }

    public async Task<List<Titles>> GetUserTitlesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Titles> list = await _userTitlesRepository.GetUserTitlesAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserTitlesCountAsync(string userId, string search, string rare)
    {
        return await _userTitlesRepository.GetUserTitlesCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserTitleAsync(Titles title, string userId)
    {
        return await _userTitlesRepository.InsertUserTitleAsync(title, userId);
    }

    public async Task<bool> UpdateUserTitleLevelAsync(string userId, Titles title)
    {
        return await _userTitlesRepository.UpdateUserTitleLevelAsync(userId, title);
    }

    public async Task<bool> UpdateUserTitleStarAsync(string userId, Titles title)
    {
        return await _userTitlesRepository.UpdateUserTitleStarAsync(userId, title);
    }

    public async Task<bool> UpdateUserTitleBreakthroughAsync(string userId, Titles title, int star, double quantity)
    {
        return await _userTitlesRepository.UpdateUserTitleBreakthroughAsync(userId, title, star, quantity);
    }

    public async Task<Titles> GetUserTitleByIdAsync(string userId, string Id)
    {
        return await _userTitlesRepository.GetUserTitleByIdAsync(userId, Id);
    }

    public async Task<Titles> SumPowerUserTitlesAsync(string userId)
    {
        return await _userTitlesRepository.SumPowerUserTitlesAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserTitlesBatchAsync(string userId, List<Titles> titles)
    {
        return await _userTitlesRepository.InsertOrUpdateUserTitlesBatchAsync(userId, titles);
    }
}
