using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHICBsService : IUserHICBsService
{
    private readonly IUserHICBsRepository _userHICBsRepository;

    public UserHICBsService(IUserHICBsRepository userHICBsRepository)
    {
        _userHICBsRepository = userHICBsRepository;
    }

    public static IUserHICBsService Create() => ServiceContainer.GetService<IUserHICBsService>();

    public async Task<UserHICBs> GetUserHICBsAsync(string userId, string id)
    {
        return await _userHICBsRepository.GetUserHICBsAsync(userId, id);
    }

    public async Task<UserHICBs> GetSumUserHICBsAsync(string userId)
    {
        return await _userHICBsRepository.GetSumUserHICBsAsync(userId);
    }

    public async Task InsertOrUpdateUserHICBsAsync(string userId, UserHICBs HICBs, string id)
    {
        await _userHICBsRepository.InsertOrUpdateUserHICBsAsync(userId, HICBs, id);
    }

    
}