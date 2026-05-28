using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIDCsService : IUserHIDCsService
{
    private static UserHIDCsService _instance;
    private readonly IUserHIDCsRepository _userHIDCsRepository;

    public UserHIDCsService(IUserHIDCsRepository userHIDCsRepository)
    {
        _userHIDCsRepository = userHIDCsRepository;
    }

    public static UserHIDCsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserHIDCsService(new UserHIDCsRepository());
        }
        return _instance;
    }

    public async Task<UserHIDCs> GetUserHIDCsAsync(string id)
    {
        return await _userHIDCsRepository.GetUserHIDCsAsync(id);
    }

    public async Task<UserHIDCs> GetSumUserHIDCsAsync(string user_id)
    {
        return await _userHIDCsRepository.GetSumUserHIDCsAsync(user_id);
    }

    public async Task InsertOrUpdateUserHIDCsAsync(string userId, UserHIDCs HIDCs, string id)
    {
        await _userHIDCsRepository.InsertOrUpdateUserHIDCsAsync(userId, HIDCs, id);
    }

    
}