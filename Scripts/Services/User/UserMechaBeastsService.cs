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

    public async Task<List<MechaBeasts>> GetUserMechaBeastsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = await _userMechaBeastsRepository.GetUserMechaBeastsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMechaBeastsCountAsync(string user_id, string search, string rare)
    {
        return await _userMechaBeastsRepository.GetUserMechaBeastsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserMechaBeastAsync(MechaBeasts MechaBeasts, string userId)
    {
        return await _userMechaBeastsRepository.InsertUserMechaBeastAsync(MechaBeasts, userId);
    }

    public async Task<bool> UpdateMechaBeastLevelAsync(MechaBeasts MechaBeasts, int cardLevel)
    {
        return await _userMechaBeastsRepository.UpdateMechaBeastLevelAsync(MechaBeasts, cardLevel);
    }

    public async Task<bool> UpdateMechaBeastBreakthroughAsync(MechaBeasts MechaBeasts, int star, double quantity)
    {
        return await _userMechaBeastsRepository.UpdateMechaBeastBreakthroughAsync(MechaBeasts, star, quantity);
    }

    public async Task<MechaBeasts> GetUserMechaBeastByIdAsync(string user_id, string Id)
    {
        return await _userMechaBeastsRepository.GetUserMechaBeastByIdAsync(user_id, Id);
    }

    public async Task<MechaBeasts> SumPowerUserMechaBeastsAsync()
    {
        return await _userMechaBeastsRepository.SumPowerUserMechaBeastsAsync();
    }
}
