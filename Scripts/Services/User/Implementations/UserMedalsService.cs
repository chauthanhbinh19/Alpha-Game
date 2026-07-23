using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMedalsService : IUserMedalsService
{
    private static UserMedalsService _instance;
    private readonly IUserMedalsRepository _userMedalsRepository;

    public UserMedalsService(IUserMedalsRepository userMedalsRepository)
    {
        _userMedalsRepository = userMedalsRepository;
    }

    public static UserMedalsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserMedalsService(new UserMedalsRepository());
        }
        return _instance;
    }

    public async Task<List<Medals>> GetUserMedalsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Medals> list = await _userMedalsRepository.GetUserMedalsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMedalsCountAsync(string userId, string search, string rare)
    {
        return await _userMedalsRepository.GetUserMedalsCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserMedalAsync(Medals medal, string userId)
    {
        return await _userMedalsRepository.InsertUserMedalAsync(medal, userId);
    }

    public async Task<bool> UpdateUserMedalLevelAsync(string userId, Medals medal)
    {
        return await _userMedalsRepository.UpdateUserMedalLevelAsync(userId, medal);
    }

    public async Task<bool> UpdateUserMedalStarAsync(string userId, Medals medal)
    {
        return await _userMedalsRepository.UpdateUserMedalStarAsync(userId, medal);
    }

    public async Task<bool> UpdateUserMedalBreakthroughAsync(string userId, Medals medal, int star, double quantity)
    {
        return await _userMedalsRepository.UpdateUserMedalBreakthroughAsync(userId, medal, star, quantity);
    }

    public async Task<Medals> GetUserMedalByIdAsync(string userId, string Id)
    {
        return await _userMedalsRepository.GetUserMedalByIdAsync(userId, Id);
    }

    public async Task<Medals> SumPowerUserMedalsAsync(string userId)
    {
        return await _userMedalsRepository.SumPowerUserMedalsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserMedalsBatchAsync(string userId, List<Medals> medals)
    {
        return await _userMedalsRepository.InsertOrUpdateUserMedalsBatchAsync(userId, medals);
    }
}
