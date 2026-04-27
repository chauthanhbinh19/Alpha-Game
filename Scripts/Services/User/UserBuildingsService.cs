using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBuildingsService : IUserBuildingsService
{
     private static UserBuildingsService _instance;
    private readonly IUserBuildingsRepository _userBuildingsRepository;

    public UserBuildingsService(IUserBuildingsRepository userBuildingsRepository)
    {
        _userBuildingsRepository = userBuildingsRepository;
    }

    public static UserBuildingsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserBuildingsService(new UserBuildingsRepository());
        }
        return _instance;
    }

    public async Task<List<Buildings>> GetUserBuildingsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Buildings> list = await _userBuildingsRepository.GetUserBuildingsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBuildingsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userBuildingsRepository.GetUserBuildingsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserBuildingAsync(Buildings Building, string userId)
    {
        return await _userBuildingsRepository.InsertUserBuildingAsync(Building, userId);
    }

    public async Task<bool> UpdateBuildingLevelAsync(Buildings Building, int cardLevel)
    {
        return await _userBuildingsRepository.UpdateBuildingLevelAsync(Building, cardLevel);
    }

    public async Task<bool> UpdateBuildingBreakthroughAsync(Buildings Building, int star, double quantity)
    {
        return await _userBuildingsRepository.UpdateBuildingBreakthroughAsync(Building, star, quantity);
    }

    public async Task<Buildings> GetUserBuildingByIdAsync(string user_id, string Id)
    {
        return await _userBuildingsRepository.GetUserBuildingByIdAsync(user_id, Id);
    }

    public async Task<Buildings> SumPowerUserBuildingsAsync()
    {
        return await _userBuildingsRepository.SumPowerUserBuildingsAsync();
    }
}
