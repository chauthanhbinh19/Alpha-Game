using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMagicFormationCirclesService : IUserMagicFormationCirclesService
{
    private static UserMagicFormationCirclesService _instance;
    private readonly IUserMagicFormationCirclesRepository _userMagicFormationCirclesRepository;

    public UserMagicFormationCirclesService(IUserMagicFormationCirclesRepository userMagicFormationCirclesRepository)
    {
        _userMagicFormationCirclesRepository = userMagicFormationCirclesRepository;
    }

    public static UserMagicFormationCirclesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserMagicFormationCirclesService(new UserMagicFormationCirclesRepository());
        }
        return _instance;
    }

    public async Task<List<MagicFormationCircles>> GetUserMagicFormationCirclesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> list = await _userMagicFormationCirclesRepository.GetUserMagicFormationCirclesAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMagicFormationCirclesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userMagicFormationCirclesRepository.GetUserMagicFormationCirclesCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserMagicFormationCircleAsync(MagicFormationCircles magicFormationCircle, string userId)
    {
        return await _userMagicFormationCirclesRepository.InsertUserMagicFormationCircleAsync(magicFormationCircle, userId);
    }

    public async Task<bool> UpdateUserMagicFormationCircleLevelAsync(string userId, MagicFormationCircles magicFormationCircle)
    {
        return await _userMagicFormationCirclesRepository.UpdateUserMagicFormationCircleLevelAsync(userId, magicFormationCircle);
    }

    public async Task<bool> UpdateUserMagicFormationCircleStarAsync(string userId, MagicFormationCircles magicFormationCircle)
    {
        return await _userMagicFormationCirclesRepository.UpdateUserMagicFormationCircleStarAsync(userId, magicFormationCircle);
    }

    public async Task<bool> UpdateUserMagicFormationCircleBreakthroughAsync(string userId, MagicFormationCircles magicFormationCircle, int star, double quantity)
    {
        return await _userMagicFormationCirclesRepository.UpdateUserMagicFormationCircleBreakthroughAsync(userId, magicFormationCircle, star, quantity);
    }

    public async Task<MagicFormationCircles> GetUserMagicFormationCircleByIdAsync(string userId, string Id)
    {
        return await _userMagicFormationCirclesRepository.GetUserMagicFormationCircleByIdAsync(userId, Id);
    }

    public async Task<MagicFormationCircles> SumPowerUserMagicFormationCirclesAsync(string userId)
    {
        return await _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserMagicFormationCirclesBatchAsync(string userId, List<MagicFormationCircles> magicFormationCircles)
    {
        return await _userMagicFormationCirclesRepository.InsertOrUpdateUserMagicFormationCirclesBatchAsync(userId, magicFormationCircles);
    }
}
