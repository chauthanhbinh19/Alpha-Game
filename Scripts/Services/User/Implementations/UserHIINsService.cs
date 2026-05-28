using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIINsService : IUserHIINsService
{
    private static UserHIINsService _instance;
    private readonly IUserHIINsRepository _userHIINsRepository;

    public UserHIINsService(IUserHIINsRepository userHIINsRepository)
    {
        _userHIINsRepository = userHIINsRepository;
    }

    public static UserHIINsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserHIINsService(new UserHIINsRepository());
        }
        return _instance;
    }

    public async Task<UserHIINs> GetUserHIINsAsync(string id)
    {
        return await _userHIINsRepository.GetUserHIINsAsync(id);
    }

    public async Task<UserHIINs> GetSumUserHIINsAsync(string user_id)
    {
        return await _userHIINsRepository.GetSumUserHIINsAsync(user_id);
    }

    public async Task InsertOrUpdateUserHIINsAsync(string userId, UserHIINs HIINs, string id)
    {
        await _userHIINsRepository.InsertOrUpdateUserHIINsAsync(userId, HIINs, id);
    }

    
}