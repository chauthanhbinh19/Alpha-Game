using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIDCsService : IUserHIDCsService
{
    private readonly IUserHIDCsRepository _userHIDCsRepository;

    public UserHIDCsService(IUserHIDCsRepository userHIDCsRepository)
    {
        _userHIDCsRepository = userHIDCsRepository;
    }

    public static IUserHIDCsService Create() => ServiceContainer.GetService<IUserHIDCsService>();

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