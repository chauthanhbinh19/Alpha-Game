using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHICAsService : IUserHICAsService
{
    private static UserHICAsService _instance;
    private readonly IUserHICAsRepository _userHICAsRepository;

    public UserHICAsService(IUserHICAsRepository userHICAsRepository)
    {
        _userHICAsRepository = userHICAsRepository;
    }

    public static UserHICAsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserHICAsService(new UserHICAsRepository());
        }
        return _instance;
    }

    public async Task<UserHICAs> GetUserHICAsAsync(string id)
    {
        return await _userHICAsRepository.GetUserHICAsAsync(id);
    }

    public async Task<UserHICAs> GetSumUserHICAsAsync(string user_id)
    {
        return await _userHICAsRepository.GetSumUserHICAsAsync(user_id);
    }

    public async Task InsertOrUpdateUserHICAsAsync(string userId, UserHICAs HICAs, string id)
    {
        await _userHICAsRepository.InsertOrUpdateUserHICAsAsync(userId, HICAs, id);
    }
}