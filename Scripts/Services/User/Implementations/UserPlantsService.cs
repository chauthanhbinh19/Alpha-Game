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




    public async Task<List<Plants>> GetUserPlantsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Plants> list = await _userPlantsRepository.GetUserPlantsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserPlantsCountAsync(string userId, string search, string rare)
    {
        return await _userPlantsRepository.GetUserPlantsCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserPlantAsync(Plants plant, string userId)
    {
        return await _userPlantsRepository.InsertUserPlantAsync(plant, userId);
    }

    public async Task<bool> UpdateUserPlantLevelAsync(string userId, Plants plant)
    {
        return await _userPlantsRepository.UpdateUserPlantLevelAsync(userId, plant);
    }

    public async Task<bool> UpdateUserPlantStarAsync(string userId, Plants plant)
    {
        return await _userPlantsRepository.UpdateUserPlantStarAsync(userId, plant);
    }

    public async Task<bool> UpdateUserPlantBreakthroughAsync(string userId, Plants plant, int star, double quantity)
    {
        return await _userPlantsRepository.UpdateUserPlantBreakthroughAsync(userId, plant, star, quantity);
    }

    public async Task<Plants> GetUserPlantByIdAsync(string userId, string Id)
    {
        return await _userPlantsRepository.GetUserPlantByIdAsync(userId, Id);
    }

    public async Task<Plants> SumPowerUserPlantsAsync(string userId)
    {
        return await _userPlantsRepository.SumPowerUserPlantsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserPlantsBatchAsync(string userId, List<Plants> plants)
    {
        return await _userPlantsRepository.InsertOrUpdateUserPlantsBatchAsync(userId, plants);
    }
}
