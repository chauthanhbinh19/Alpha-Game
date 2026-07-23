using System.Collections.Generic;
using System.Threading.Tasks;

public class UserArchitecturesService : IUserArchitecturesService
{
    private static UserArchitecturesService _instance;
    private readonly IUserArchitecturesRepository _userArchitecturesRepository;

    public UserArchitecturesService(IUserArchitecturesRepository userArchitecturesRepository)
    {
        _userArchitecturesRepository = userArchitecturesRepository;
    }

    public static UserArchitecturesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserArchitecturesService(new UserArchitecturesRepository());
        }
        return _instance;
    }

    public async Task<List<Architectures>> GetUserArchitecturesAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Architectures> list = await _userArchitecturesRepository.GetUserArchitecturesAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserArchitecturesCountAsync(string user_id, string search, string rare)
    {
        return await _userArchitecturesRepository.GetUserArchitecturesCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserArchitectureAsync(Architectures architecture, string userId)
    {
        return await _userArchitecturesRepository.InsertUserArchitectureAsync(architecture, userId);
    }

    public async Task<bool> UpdateUserArchitectureLevelAsync(string userId, Architectures architecture)
    {
        return await _userArchitecturesRepository.UpdateUserArchitectureLevelAsync(userId, architecture);
    }

    public async Task<bool> UpdateUserArchitectureStarAsync(string userId, Architectures architecture)
    {
        return await _userArchitecturesRepository.UpdateUserArchitectureStarAsync(userId, architecture);
    }

    public async Task<bool> UpdateUserArchitectureBreakthroughAsync(string userId, Architectures architecture, int star, double quantity)
    {
        return await _userArchitecturesRepository.UpdateUserArchitectureBreakthroughAsync(userId, architecture, star, quantity);
    }

    public async Task<Architectures> GetUserArchitectureByIdAsync(string user_id, string Id)
    {
        return await _userArchitecturesRepository.GetUserArchitectureByIdAsync(user_id, Id);
    }

    public async Task<Architectures> SumPowerUserArchitecturesAsync(string userId)
    {
        return await _userArchitecturesRepository.SumPowerUserArchitecturesAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserArchitecturesBatchAsync(string userId, List<Architectures> architectures)
    {
        return await _userArchitecturesRepository.InsertOrUpdateUserArchitecturesBatchAsync(userId, architectures);
    }
}
