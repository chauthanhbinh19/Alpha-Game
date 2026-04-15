using System.Collections.Generic;
using System.Threading.Tasks;

public class UserAvatarsService : IUserAvatarsService
{
     private static UserAvatarsService _instance;
    private readonly IUserAvatarsRepository _userAvatarsRepository;

    public UserAvatarsService(IUserAvatarsRepository userAvatarsRepository)
    {
        _userAvatarsRepository = userAvatarsRepository;
    }

    public static UserAvatarsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserAvatarsService(new UserAvatarsRepository());
        }
        return _instance;
    }

    public async Task<List<Avatars>> GetUserAvatarsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<Avatars> list = await _userAvatarsRepository.GetUserAvatarsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserAvatarsCountAsync(string user_id, string search, string rare)
    {
        return await _userAvatarsRepository.GetUserAvatarsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserAvatarAsync(Avatars avatars, string userId)
    {
        return await _userAvatarsRepository.InsertUserAvatarAsync(avatars, userId);
    }

    public async Task<bool> InsertUserAvatarByIdAsync(string avatarId, string userId)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        return await _userAvatarsRepository.InsertUserAvatarByIdAsync(await _service.GetAvatarByIdAsync(avatarId), userId);
    }

    public async Task<Avatars> GetAvatarByUsedAsync(string user_id)
    {
        return await _userAvatarsRepository.GetAvatarByUsedAsync(user_id);
    }

    public async Task UpdateIsUsedAvatarAsync(string avatarId, string userId, bool is_used)
    {
        await _userAvatarsRepository.UpdateIsUsedAvatarAsync(avatarId, userId, is_used);
    }

    public async Task<Avatars> SumPowerUserAvatarsAsync()
    {
        return await _userAvatarsRepository.SumPowerUserAvatarsAsync();
    }
}