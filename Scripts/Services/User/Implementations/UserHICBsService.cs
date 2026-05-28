using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHICBsService : IUserHICBsService
{
    private static UserHICBsService _instance;
    private readonly IUserHICBsRepository _userHICBsRepository;

    public UserHICBsService(IUserHICBsRepository userHICBsRepository)
    {
        _userHICBsRepository = userHICBsRepository;
    }

    public static UserHICBsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserHICBsService(new UserHICBsRepository());
        }
        return _instance;
    }

    public async Task<UserHICBs> GetUserHICBsAsync(string id)
    {
        return await _userHICBsRepository.GetUserHICBsAsync(id);
    }

    public async Task<UserHICBs> GetSumUserHICBsAsync(string user_id)
    {
        return await _userHICBsRepository.GetSumUserHICBsAsync(user_id);
    }

    public async Task InsertOrUpdateUserHICBsAsync(string userId, UserHICBs HICBs, string id)
    {
        await _userHICBsRepository.InsertOrUpdateUserHICBsAsync(userId, HICBs, id);
    }

    
}