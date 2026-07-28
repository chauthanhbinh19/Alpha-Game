
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserAlchemiesService : IUserAlchemiesService
{
    private static UserAlchemiesService _instance;
    private readonly IUserAlchemiesRepository _userAlchemiesRepository;

    public UserAlchemiesService(IUserAlchemiesRepository userAlchemiesRepository)
    {
        _userAlchemiesRepository = userAlchemiesRepository;
    }

    public static UserAlchemiesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserAlchemiesService(new UserAlchemiesRepository());
        }
        return _instance;
    }

    public async Task<List<Alchemies>> GetUserAlchemiesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Alchemies> list = await _userAlchemiesRepository.GetUserAlchemiesAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserAlchemiesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userAlchemiesRepository.GetUserAlchemiesCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserAlchemyAsync(Alchemies alchemy, string userId)
    {
        var result = await _userAlchemiesRepository.InsertUserAlchemyAsync(alchemy, userId);
        if (result)
        {
            await AlchemiesGalleryService.Create().InsertAlchemyGalleryAsync(userId, alchemy.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserAlchemyLevelAsync(string userId, Alchemies alchemy)
    {
        return await _userAlchemiesRepository.UpdateUserAlchemyLevelAsync(userId, alchemy);
    }

    public async Task<bool> UpdateUserAlchemyStarAsync(string userId, Alchemies alchemy)
    {
        var result = await _userAlchemiesRepository.UpdateUserAlchemyStarAsync(userId, alchemy);
        if (result)
        {
            await AlchemiesGalleryService.Create().UpdateStarAlchemyGalleryAsync(userId, alchemy.Id, alchemy.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserAlchemyBreakthroughAsync(string userId, Alchemies alchemy, int star, double quantity)
    {
        return await _userAlchemiesRepository.UpdateUserAlchemyBreakthroughAsync(userId, alchemy, star, quantity);
    }

    public async Task<Alchemies> GetUserAlchemyByIdAsync(string userId, string Id)
    {
        return await _userAlchemiesRepository.GetUserAlchemyByIdAsync(userId, Id);
    }

    public async Task<Alchemies> SumPowerUserAlchemiesAsync(string userId)
    {
        return await _userAlchemiesRepository.SumPowerUserAlchemiesAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserAlchemiesBatchAsync(string userId, List<Alchemies> alchemies)
    {
        return await _userAlchemiesRepository.InsertOrUpdateUserAlchemiesBatchAsync(userId, alchemies);
    }
}
