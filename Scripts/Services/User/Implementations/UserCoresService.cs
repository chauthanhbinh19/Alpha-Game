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

    public async Task<List<Cores>> GetUserCoresAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Cores> list = await _userCoresRepository.GetUserCoresAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCoresCountAsync(string user_id, string search, string rare)
    {
        return await _userCoresRepository.GetUserCoresCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserCoreAsync(Cores core, string userId)
    {
        return await _userCoresRepository.InsertUserCoreAsync(core, userId);
    }

    public async Task<bool> UpdateCoreLevelAsync(Cores core, int level)
    {
        return await _userCoresRepository.UpdateCoreLevelAsync(core, level);
    }

    public async Task<bool> UpdateCoreBreakthroughAsync(Cores core, int star, double quantity)
    {
        return await _userCoresRepository.UpdateCoreBreakthroughAsync(core, star, quantity);
    }

    public async Task<Cores> GetUserCoreByIdAsync(string user_id, string Id)
    {
        return await _userCoresRepository.GetUserCoreByIdAsync(user_id, Id);
    }

    public async Task<Cores> SumPowerUserCoresAsync()
    {
        return await _userCoresRepository.SumPowerUserCoresAsync();
    }

    public async Task<bool> InsertOrUpdateUserCoresBatchAsync(List<Cores> cores)
    {
        return await _userCoresRepository.InsertOrUpdateUserCoresBatchAsync(cores);
    }
}
