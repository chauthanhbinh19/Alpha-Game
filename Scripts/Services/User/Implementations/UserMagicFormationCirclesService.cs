using System.Collections.Generic;
using System.Threading.Tasks;

public class UserMagicFormationCirclesService : IUserMagicFormationCirclesService
{
     private static UserMagicFormationCirclesService _instance;
    private IUserMagicFormationCirclesRepository _userMagicFormationCirclesRepository;

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

    public async Task<List<MagicFormationCircles>> GetUserMagicFormationCirclesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> list = await _userMagicFormationCirclesRepository.GetUserMagicFormationCirclesAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserMagicFormationCirclesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userMagicFormationCirclesRepository.GetUserMagicFormationCirclesCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserMagicFormationCircleAsync(MagicFormationCircles magicFormationCircle, string userId)
    {
        return await _userMagicFormationCirclesRepository.InsertUserMagicFormationCircleAsync(magicFormationCircle, userId);
    }

    public async Task<bool> UpdateMagicFormationCircleLevelAsync(MagicFormationCircles magicFormationCircle, int level)
    {
        return await _userMagicFormationCirclesRepository.UpdateMagicFormationCircleLevelAsync(magicFormationCircle, level);
    }

    public async Task<bool> UpdateMagicFormationCircleBreakthroughAsync(MagicFormationCircles magicFormationCircle, int star, double quantity)
    {
        return await _userMagicFormationCirclesRepository.UpdateMagicFormationCircleBreakthroughAsync(magicFormationCircle, star, quantity);
    }

    public async Task<MagicFormationCircles> GetUserMagicFormationCircleByIdAsync(string user_id, string Id)
    {
        return await _userMagicFormationCirclesRepository.GetUserMagicFormationCircleByIdAsync(user_id, Id);
    }

    public async Task<MagicFormationCircles> SumPowerUserMagicFormationCirclesAsync()
    {
        return await _userMagicFormationCirclesRepository.SumPowerUserMagicFormationCirclesAsync();
    }

    public async Task<bool> InsertOrUpdateUserMagicFormationCirclesBatchAsync(List<MagicFormationCircles> magicFormationCircles)
    {
        return await _userMagicFormationCirclesRepository.InsertOrUpdateUserMagicFormationCirclesBatchAsync(magicFormationCircles);
    }
}
