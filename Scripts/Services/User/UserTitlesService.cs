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

    public async Task<List<Titles>> GetUserTitlesAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Titles> list = await _userTitlesRepository.GetUserTitlesAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserTitlesCountAsync(string user_id, string search, string rare)
    {
        return await _userTitlesRepository.GetUserTitlesCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserTitleAsync(Titles titles, string userId)
    {
        return await _userTitlesRepository.InsertUserTitleAsync(titles, userId);
    }

    public async Task<bool> UpdateTitleLevelAsync(Titles titles, int cardLevel)
    {
        return await _userTitlesRepository.UpdateTitleLevelAsync(titles, cardLevel);
    }

    public async Task<bool> UpdateTitleBreakthroughAsync(Titles titles, int star, double quantity)
    {
        return await _userTitlesRepository.UpdateTitleBreakthroughAsync(titles, star, quantity);
    }

    public async Task<Titles> GetUserTitleByIdAsync(string user_id, string Id)
    {
        return await _userTitlesRepository.GetUserTitleByIdAsync(user_id, Id);
    }

    public async Task<Titles> SumPowerUserTitlesAsync()
    {
        return await _userTitlesRepository.SumPowerUserTitlesAsync();
    }
}
