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

    public async Task<List<Avatars>> GetUserAvatarsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Avatars> list = await _userAvatarsRepository.GetUserAvatarsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserAvatarsCountAsync(string userId, string search, string rare)
    {
        return await _userAvatarsRepository.GetUserAvatarsCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserAvatarAsync(Avatars avatar, string userId)
    {
        return await _userAvatarsRepository.InsertUserAvatarAsync(avatar, userId);
    }

    public async Task<bool> InsertUserAvatarByIdAsync(string avatarId, string userId)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        return await _userAvatarsRepository.InsertUserAvatarByIdAsync(await _service.GetAvatarByIdAsync(avatarId), userId);
    }

    public async Task<bool> UpdateUserAvatarLevelAsync(string userId, Avatars avatar)
    {
        return await _userAvatarsRepository.UpdateUserAvatarLevelAsync(userId, avatar);
    }

    public async Task<bool> UpdateUserAvatarStarAsync(string userId, Avatars avatar)
    {
        return await _userAvatarsRepository.UpdateUserAvatarStarAsync(userId, avatar);
    }

    public async Task<Avatars> GetUserAvatarByUsedAsync(string userId)
    {
        return await _userAvatarsRepository.GetUserAvatarByUsedAsync(userId);
    }

    public async Task UpdateIsUsedUserAvatarAsync(string avatarId, string userId, bool is_used)
    {
        await _userAvatarsRepository.UpdateIsUsedUserAvatarAsync(avatarId, userId, is_used);
    }

    public async Task<Avatars> SumPowerUserAvatarsAsync(string userId)
    {
        return await _userAvatarsRepository.SumPowerUserAvatarsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserAvatarsBatchAsync(string userId, List<Avatars> avatars)
    {
        return await _userAvatarsRepository.InsertOrUpdateUserAvatarsBatchAsync(userId, avatars);
    }
}