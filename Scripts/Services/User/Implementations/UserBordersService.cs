using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBordersService : IUserBordersService
{
    private static UserBordersService _instance;
    private readonly IUserBordersRepository _userBordersRepository;

    public UserBordersService(IUserBordersRepository userBordersRepository)
    {
        _userBordersRepository = userBordersRepository;
    }

    public static UserBordersService Create()
    {
        if (_instance == null)
        {
            _instance = new UserBordersService(new UserBordersRepository());
        }
        return _instance;
    }

    public async Task<List<Borders>> GetUserBordersAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Borders> list = await _userBordersRepository.GetUserBordersAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserBordersCountAsync(string userId, string search, string rare)
    {
        return await _userBordersRepository.GetUserBordersCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserBorderAsync(Borders border, string userId)
    {
        var result = await _userBordersRepository.InsertUserBorderAsync(border, userId);
        if (result)
        {
            await BordersGalleryService.Create().InsertBorderGalleryAsync(userId, border.Id);
        }
        return result;
    }

    public async Task<bool> InsertUserBorderByIdAsync(string borderId, string userId)
    {
        IBordersRepository _repository = new BordersRepository();
        BordersService _service = new BordersService(_repository);
        return await _userBordersRepository.InsertUserBorderByIdAsync(await _service.GetBorderByIdAsync(borderId), userId);
    }

    public async Task<bool> UpdateUserBorderLevelAsync(string userId, Borders border)
    {
        return await _userBordersRepository.UpdateUserBorderLevelAsync(userId, border);
    }

    public async Task<bool> UpdateUserBorderStarAsync(string userId, Borders border)
    {
        var result = await _userBordersRepository.UpdateUserBorderStarAsync(userId, border);
        if (result)
        {
            await BordersGalleryService.Create().UpdateStarBorderGalleryAsync(userId, border.Id, border.Star);
        }
        return result;
    }

    public async Task<Borders> GetUserBorderByUsedAsync(string userId)
    {
        return await _userBordersRepository.GetUserBorderByUsedAsync(userId);
    }

    public async Task UpdateIsUsedUserBorderAsync(string borderId, string userId, bool is_used)
    {
        await _userBordersRepository.UpdateIsUsedUserBorderAsync(borderId, userId, is_used);
    }

    public async Task<Borders> SumPowerUserBordersAsync(string userId)
    {
        return await _userBordersRepository.SumPowerUserBordersAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserBordersBatchAsync(string userId, List<Borders> borders)
    {
        return await _userBordersRepository.InsertOrUpdateUserBordersBatchAsync(userId, borders);
    }
}
