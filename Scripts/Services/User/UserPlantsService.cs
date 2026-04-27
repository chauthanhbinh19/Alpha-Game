using System.Collections.Generic;
using System.Threading.Tasks;

public class UserPlantsService : IUserPlantsService
{
     private static UserPlantsService _instance;
    private readonly IUserPlantsRepository _userPlantsRepository;

    public UserPlantsService(IUserPlantsRepository userPlantsRepository)
    {
        _userPlantsRepository = userPlantsRepository;
    }

    public static UserPlantsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserPlantsService(new UserPlantsRepository());
        }
        return _instance;
    }

    
    

    public async Task<List<Plants>> GetUserPlantsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Plants> list = await _userPlantsRepository.GetUserPlantsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserPlantsCountAsync(string user_id, string search, string rare)
    {
        return await _userPlantsRepository.GetUserPlantsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserPlantAsync(Plants Plants, string userId)
    {
        return await _userPlantsRepository.InsertUserPlantAsync(Plants, userId);
    }

    public async Task<bool> UpdatePlantLevelAsync(Plants Plants, int cardLevel)
    {
        return await _userPlantsRepository.UpdatePlantLevelAsync(Plants, cardLevel);
    }

    public async Task<bool> UpdatePlantBreakthroughAsync(Plants Plants, int star, double quantity)
    {
        return await _userPlantsRepository.UpdatePlantBreakthroughAsync(Plants, star, quantity);
    }

    public async Task<Plants> GetUserPlantByIdAsync(string user_id, string Id)
    {
        return await _userPlantsRepository.GetUserPlantByIdAsync(user_id, Id);
    }

    public async Task<Plants> SumPowerUserPlantsAsync()
    {
        return await _userPlantsRepository.SumPowerUserPlantsAsync();
    }
}
