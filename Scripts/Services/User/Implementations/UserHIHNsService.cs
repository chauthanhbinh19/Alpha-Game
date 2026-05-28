using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIHNsService : IUserHIHNsService
{
    private static UserHIHNsService _instance;
    private readonly IUserHIHNsRepository _userHIHNsRepository;

    public UserHIHNsService(IUserHIHNsRepository userHIHNsRepository)
    {
        _userHIHNsRepository = userHIHNsRepository;
    }

    public static UserHIHNsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserHIHNsService(new UserHIHNsRepository());
        }
        return _instance;
    }

    public async Task<UserHIHNs> GetUserHIHNsAsync(string id)
    {
        return await _userHIHNsRepository.GetUserHIHNsAsync(id);
    }

    public async Task<UserHIHNs> GetSumUserHIHNsAsync(string user_id)
    {
        return await _userHIHNsRepository.GetSumUserHIHNsAsync(user_id);
    }

    public async Task InsertOrUpdateUserHIHNsAsync(string userId, UserHIHNs HIHNs, string id)
    {
        await _userHIHNsRepository.InsertOrUpdateUserHIHNsAsync(userId, HIHNs, id);
    }

    
}