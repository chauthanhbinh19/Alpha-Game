using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHISNsService : IUserHISNsService
{
    private readonly IUserHISNsRepository _userHISNsRepository;

    public UserHISNsService(IUserHISNsRepository userHISNsRepository)
    {
        _userHISNsRepository = userHISNsRepository;
    }

    public static IUserHISNsService Create() => ServiceContainer.GetService<IUserHISNsService>();

    public async Task<UserHISNs> GetUserHISNsAsync(string userId, string id)
    {
        return await _userHISNsRepository.GetUserHISNsAsync(userId, id);
    }

    public async Task<UserHISNs> GetSumUserHISNsAsync(string userId)
    {
        return await _userHISNsRepository.GetSumUserHISNsAsync(userId);
    }

    public async Task InsertOrUpdateUserHISNsAsync(string userId, UserHISNs HISNs, string id)
    {
        await _userHISNsRepository.InsertOrUpdateUserHISNsAsync(userId, HISNs, id);
    }
}