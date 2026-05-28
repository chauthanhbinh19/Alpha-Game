using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMedalsService : IUserMedalsService
{
     private static UserMedalsService _instance;
    private IUserMedalsRepository _userMedalsRepository;

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

    public async Task<List<Medals>> GetUserMedalsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Medals> list = await _userMedalsRepository.GetUserMedalsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMedalsCountAsync(string user_id, string search, string rare)
    {
        return await _userMedalsRepository.GetUserMedalsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserMedalAsync(Medals medal, string userId)
    {
        return await _userMedalsRepository.InsertUserMedalAsync(medal, userId);
    }

    public async Task<bool> UpdateMedalLevelAsync(Medals medal, int level)
    {
        return await _userMedalsRepository.UpdateMedalLevelAsync(medal, level);
    }

    public async Task<bool> UpdateMedalBreakthroughAsync(Medals medal, int star, double quantity)
    {
        return await _userMedalsRepository.UpdateMedalBreakthroughAsync(medal, star, quantity);
    }

    public async Task<Medals> GetUserMedalByIdAsync(string user_id, string Id)
    {
        return await _userMedalsRepository.GetUserMedalByIdAsync(user_id, Id);
    }

    public async Task<Medals> SumPowerUserMedalsAsync()
    {
        return await _userMedalsRepository.SumPowerUserMedalsAsync();
    }

    public async Task<bool> InsertOrUpdateUserMedalsBatchAsync(List<Medals> medals)
    {
        return await _userMedalsRepository.InsertOrUpdateUserMedalsBatchAsync(medals);
    }
}
