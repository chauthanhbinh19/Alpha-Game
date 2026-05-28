using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHISNsService : IUserHISNsService
{
    private static UserHISNsService _instance;
    private readonly IUserHISNsRepository _userHISNsRepository;

    public UserHISNsService(IUserHISNsRepository userHISNsRepository)
    {
        _userHISNsRepository = userHISNsRepository;
    }

    public static UserHISNsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserHISNsService(new UserHISNsRepository());
        }
        return _instance;
    }

    public async Task<UserHISNs> GetUserHISNsAsync(string id)
    {
        return await _userHISNsRepository.GetUserHISNsAsync(id);
    }

    public async Task<UserHISNs> GetSumUserHISNsAsync(string user_id)
    {
        return await _userHISNsRepository.GetSumUserHISNsAsync(user_id);
    }

    public async Task InsertOrUpdateUserHISNsAsync(string userId, UserHISNs HISNs, string id)
    {
        await _userHISNsRepository.InsertOrUpdateUserHISNsAsync(userId, HISNs, id);
    }
}