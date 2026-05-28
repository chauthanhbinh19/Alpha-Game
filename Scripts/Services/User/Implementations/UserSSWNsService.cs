using System.Collections.Generic;
using System.Threading.Tasks;
public class UserSSWNsService : IUserSSWNsService
{
    private static UserSSWNsService _instance;
    private readonly IUserSSWNsRepository _userSSWNsRepository;

    public UserSSWNsService(IUserSSWNsRepository userSSWNsRepository)
    {
        _userSSWNsRepository = userSSWNsRepository;
    }

    public static UserSSWNsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserSSWNsService(new UserSSWNsRepository());
        }
        return _instance;
    }

    public async Task<UserSSWNs> GetUserSSWNsAsync(string id)
    {
        return await _userSSWNsRepository.GetUserSSWNsAsync(id);
    }

    public async Task<UserSSWNs> GetSumUserSSWNsAsync(string user_id)
    {
        return await _userSSWNsRepository.GetSumUserSSWNsAsync(user_id);
    }

    public async Task InsertOrUpdateUserSSWNsAsync(string userId, UserSSWNs SSWNs, string id)
    {
        await _userSSWNsRepository.InsertOrUpdateUserSSWNsAsync(userId, SSWNs, id);
    }
}