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

    public async Task<UserHIDCs> GetUserHIDCsAsync(string userId, string id)
    {
        return await _userHIDCsRepository.GetUserHIDCsAsync(userId, id);
    }

    public async Task<UserHIDCs> GetSumUserHIDCsAsync(string userId)
    {
        return await _userHIDCsRepository.GetSumUserHIDCsAsync(userId);
    }

    public async Task InsertOrUpdateUserHIDCsAsync(string userId, UserHIDCs HIDCs, string id)
    {
        await _userHIDCsRepository.InsertOrUpdateUserHIDCsAsync(userId, HIDCs, id);
    }

    
}