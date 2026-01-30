using System.Collections.Generic;
using System.Threading.Tasks;

public class UserBordersService : IUserBordersService
{
    private IUserBordersRepository _userBordersRepository;

    public UserBordersService(IUserBordersRepository userBordersRepository)
    {
        _userBordersRepository = userBordersRepository;
    }

    public static UserBordersService Create()
    {
        return new UserBordersService(new UserBordersRepository());
    }

    public async Task<List<Borders>> GetUserBordersAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Borders> list = await _userBordersRepository.GetUserBordersAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserBordersCountAsync(string user_id, string search, string rare)
    {
        return await _userBordersRepository.GetUserBordersCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserBorderAsync(Borders borders, string userId)
    {
        return await _userBordersRepository.InsertUserBorderAsync(borders, userId);
    }

    public async Task<bool> InsertUserBorderByIdAsync(string borderId, string userId)
    {
        IBordersRepository _repository = new BordersRepository();
        BordersService _service = new BordersService(_repository);
        return await _userBordersRepository.InsertUserBorderByIdAsync(await _service.GetBorderByIdAsync(borderId), userId);
    }

    public async Task<Borders> GetBorderByUsedAsync(string user_id)
    {
        return await _userBordersRepository.GetBorderByUsedAsync(user_id);
    }

    public async Task UpdateIsUsedBorderAsync(string borderId, string userId, bool is_used)
    {
        await _userBordersRepository.UpdateIsUsedBorderAsync(borderId, userId, is_used);
    }

    public async Task<Borders> SumPowerUserBordersAsync()
    {
        return await _userBordersRepository.SumPowerUserBordersAsync();
    }
}
