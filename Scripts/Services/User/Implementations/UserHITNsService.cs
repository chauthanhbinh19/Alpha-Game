using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHITNsService : IUserHITNsService
{
    private static UserHITNsService _instance;
    private readonly IUserHITNsRepository _userHITNsRepository;

    public UserHITNsService(IUserHITNsRepository userHITNsRepository)
    {
        _userHITNsRepository = userHITNsRepository;
    }

    public static UserHITNsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserHITNsService(new UserHITNsRepository());
        }
        return _instance;
    }

    public async Task<UserHITNs> GetUserHITNsAsync(string id)
    {
        return await _userHITNsRepository.GetUserHITNsAsync(id);
    }

    public async Task<UserHITNs> GetSumUserHITNsAsync(string user_id)
    {
        return await _userHITNsRepository.GetSumUserHITNsAsync(user_id);
    }

    public async Task InsertOrUpdateUserHITNsAsync(string userId, UserHITNs HITNs, string id)
    {
        await _userHITNsRepository.InsertOrUpdateUserHITNsAsync(userId, HITNs, id);
    }
}