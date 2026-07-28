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

    public async Task<List<Buildings>> GetUserBuildingsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Buildings> list = await _userBuildingsRepository.GetUserBuildingsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBuildingsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userBuildingsRepository.GetUserBuildingsCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserBuildingAsync(Buildings building, string userId)
    {
        var result = await _userBuildingsRepository.InsertUserBuildingAsync(building, userId);
        if (result)
        {
            await BuildingsGalleryService.Create().InsertBuildingGalleryAsync(userId, building.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserBuildingLevelAsync(string userId, Buildings building)
    {
        return await _userBuildingsRepository.UpdateUserBuildingLevelAsync(userId, building);
    }

    public async Task<bool> UpdateUserBuildingStarAsync(string userId, Buildings building)
    {
        var result = await _userBuildingsRepository.UpdateUserBuildingStarAsync(userId, building);
        if (result)
        {
            await BuildingsGalleryService.Create().UpdateStarBuildingGalleryAsync(userId, building.Id, building.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserBuildingBreakthroughAsync(string userId, Buildings building, int star, double quantity)
    {
        return await _userBuildingsRepository.UpdateUserBuildingBreakthroughAsync(userId, building, star, quantity);
    }

    public async Task<Buildings> GetUserBuildingByIdAsync(string userId, string Id)
    {
        return await _userBuildingsRepository.GetUserBuildingByIdAsync(userId, Id);
    }

    public async Task<Buildings> SumPowerUserBuildingsAsync(string userId)
    {
        return await _userBuildingsRepository.SumPowerUserBuildingsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserBuildingsBatchAsync(string userId, List<Buildings> buildings)
    {
        return await _userBuildingsRepository.InsertOrUpdateUserBuildingsBatchAsync(userId, buildings);
    }
}
