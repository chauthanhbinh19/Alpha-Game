using System.Collections.Generic;
using System.Threading.Tasks;

public class UserAvatarsService : IUserAvatarsService
{
    private readonly IUserAvatarsRepository _userAvatarsRepository;

    public UserAvatarsService(IUserAvatarsRepository userAvatarsRepository)
    {
        _userAvatarsRepository = userAvatarsRepository;
    }

    public static UserAvatarsService Create()
    {
        return new UserAvatarsService(new UserAvatarsRepository());
    }

    public async Task<List<Avatars>> GetUserAvatarsAsync(string user_id, int pageSize, int offset, string rare)
    {
        List<Avatars> list = await _userAvatarsRepository.GetUserAvatarsAsync(user_id, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetUserAvatarsCountAsync(string user_id, string rare)
    {
        return await _userAvatarsRepository.GetUserAvatarsCountAsync(user_id, rare);
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