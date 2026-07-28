using System.Collections.Generic;
using System.Threading.Tasks;

public class UserForgesService : IUserForgesService
{
    private static UserForgesService _instance;
    private readonly IUserForgesRepository _userForgesRepository;

    public UserForgesService(IUserForgesRepository userForgesRepository)
    {
        _userForgesRepository = userForgesRepository;
    }

    public static UserForgesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserForgesService(new UserForgesRepository());
        }
        return _instance;
    }

    public async Task<List<Forges>> GetUserForgesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Forges> list = await _userForgesRepository.GetUserForgesAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserForgesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userForgesRepository.GetUserForgesCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserForgeAsync(Forges forge, string userId)
    {
        var result = await _userForgesRepository.InsertUserForgeAsync(forge, userId);
        if (result)
        {
            await ForgesGalleryService.Create().InsertForgeGalleryAsync(userId, forge.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserForgeLevelAsync(string userId, Forges forge)
    {
        return await _userForgesRepository.UpdateUserForgeLevelAsync(userId, forge);
    }

    public async Task<bool> UpdateUserForgeStarAsync(string userId, Forges forge)
    {
        var result = await _userForgesRepository.UpdateUserForgeStarAsync(userId, forge);
        if (result)
        {
            await ForgesGalleryService.Create().UpdateStarForgeGalleryAsync(userId, forge.Id, forge.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserForgeBreakthroughAsync(string userId, Forges forge, int star, double quantity)
    {
        return await _userForgesRepository.UpdateUserForgeBreakthroughAsync(userId, forge, star, quantity);
    }

    public async Task<Forges> GetUserForgeByIdAsync(string userId, string Id)
    {
        return await _userForgesRepository.GetUserForgeByIdAsync(userId, Id);
    }

    public async Task<Forges> SumPowerUserForgesAsync(string userId)
    {
        return await _userForgesRepository.SumPowerUserForgesAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserForgesBatchAsync(string userId, List<Forges> forges)
    {
        return await _userForgesRepository.InsertOrUpdateUserForgesBatchAsync(userId, forges);
    }
}
