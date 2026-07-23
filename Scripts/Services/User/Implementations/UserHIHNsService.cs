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

    public async Task<UserHIHNs> GetUserHIHNsAsync(string userId, string id)
    {
        return await _userHIHNsRepository.GetUserHIHNsAsync(userId, id);
    }

    public async Task<UserHIHNs> GetSumUserHIHNsAsync(string userId)
    {
        return await _userHIHNsRepository.GetSumUserHIHNsAsync(userId);
    }

    public async Task InsertOrUpdateUserHIHNsAsync(string userId, UserHIHNs HIHNs, string id)
    {
        await _userHIHNsRepository.InsertOrUpdateUserHIHNsAsync(userId, HIHNs, id);
    }

    
}