using System.Collections.Generic;
using System.Threading.Tasks;
public class UserSSWNsService : IUserSSWNsService
{
    private readonly IUserSSWNsRepository _userSSWNsRepository;

    public UserSSWNsService(IUserSSWNsRepository userSSWNsRepository)
    {
        _userSSWNsRepository = userSSWNsRepository;
    }

    public static IUserSSWNsService Create() => ServiceContainer.GetService<IUserSSWNsService>();

    public async Task<UserSSWNs> GetUserSSWNsAsync(string userId, string id)
    {
        return await _userSSWNsRepository.GetUserSSWNsAsync(userId, id);
    }

    public async Task<UserSSWNs> GetSumUserSSWNsAsync(string userId)
    {
        return await _userSSWNsRepository.GetSumUserSSWNsAsync(userId);
    }

    public async Task InsertOrUpdateUserSSWNsAsync(string userId, UserSSWNs SSWNs, string id)
    {
        await _userSSWNsRepository.InsertOrUpdateUserSSWNsAsync(userId, SSWNs, id);
    }
}