using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMechaBeastsService : IUserMechaBeastsService
{
    private static UserMechaBeastsService _instance;
    private readonly IUserMechaBeastsRepository _userMechaBeastsRepository;

    public UserMechaBeastsService(IUserMechaBeastsRepository userMechaBeastsRepository)
    {
        _userMechaBeastsRepository = userMechaBeastsRepository;
    }

    public static UserMechaBeastsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserMechaBeastsService(new UserMechaBeastsRepository());
        }
        return _instance;
    }

    public async Task<List<MechaBeasts>> GetUserMechaBeastsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = await _userMechaBeastsRepository.GetUserMechaBeastsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMechaBeastsCountAsync(string userId, string search, string rare)
    {
        return await _userMechaBeastsRepository.GetUserMechaBeastsCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserMechaBeastAsync(MechaBeasts mechaBeast, string userId)
    {
        return await _userMechaBeastsRepository.InsertUserMechaBeastAsync(mechaBeast, userId);
    }

    public async Task<bool> UpdateUserMechaBeastLevelAsync(string userId, MechaBeasts mechaBeast)
    {
        return await _userMechaBeastsRepository.UpdateUserMechaBeastLevelAsync(userId, mechaBeast);
    }

    public async Task<bool> UpdateUserMechaBeastStarAsync(string userId, MechaBeasts mechaBeast)
    {
        return await _userMechaBeastsRepository.UpdateUserMechaBeastStarAsync(userId, mechaBeast);
    }

    public async Task<bool> UpdateUserMechaBeastBreakthroughAsync(string userId, MechaBeasts mechaBeast, int star, double quantity)
    {
        return await _userMechaBeastsRepository.UpdateUserMechaBeastBreakthroughAsync(userId, mechaBeast, star, quantity);
    }

    public async Task<MechaBeasts> GetUserMechaBeastByIdAsync(string userId, string Id)
    {
        return await _userMechaBeastsRepository.GetUserMechaBeastByIdAsync(userId, Id);
    }

    public async Task<MechaBeasts> SumPowerUserMechaBeastsAsync(string userId)
    {
        return await _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserMechaBeastsBatchAsync(string userId, List<MechaBeasts> mechaBeasts)
    {
        return await _userMechaBeastsRepository.InsertOrUpdateUserMechaBeastsBatchAsync(userId, mechaBeasts);
    }
}
