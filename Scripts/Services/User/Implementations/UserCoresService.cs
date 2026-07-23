using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCoresService : IUserCoresService
{
    private static UserCoresService _instance;
    private readonly IUserCoresRepository _userCoresRepository;

    public UserCoresService(IUserCoresRepository userCoresRepository)
    {
        _userCoresRepository = userCoresRepository;
    }

    public static UserCoresService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCoresService(new UserCoresRepository());
        }
        return _instance;
    }

    public async Task<List<Cores>> GetUserCoresAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Cores> list = await _userCoresRepository.GetUserCoresAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCoresCountAsync(string userId, string search, string rare)
    {
        return await _userCoresRepository.GetUserCoresCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserCoreAsync(Cores core, string userId)
    {
        return await _userCoresRepository.InsertUserCoreAsync(core, userId);
    }

    public async Task<bool> UpdateUserCoreLevelAsync(string userId, Cores core)
    {
        return await _userCoresRepository.UpdateUserCoreLevelAsync(userId, core);
    }

    public async Task<bool> UpdateUserCoreStarAsync(string userId, Cores core)
    {
        return await _userCoresRepository.UpdateUserCoreStarAsync(userId, core);
    }

    public async Task<bool> UpdateUserCoreBreakthroughAsync(string userId, Cores core, int star, double quantity)
    {
        return await _userCoresRepository.UpdateUserCoreBreakthroughAsync(userId, core, star, quantity);
    }

    public async Task<Cores> GetUserCoreByIdAsync(string userId, string Id)
    {
        return await _userCoresRepository.GetUserCoreByIdAsync(userId, Id);
    }

    public async Task<Cores> SumPowerUserCoresAsync(string userId)
    {
        return await _userCoresRepository.SumPowerUserCoresAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserCoresBatchAsync(string userId, List<Cores> cores)
    {
        return await _userCoresRepository.InsertOrUpdateUserCoresBatchAsync(userId, cores);
    }
}
