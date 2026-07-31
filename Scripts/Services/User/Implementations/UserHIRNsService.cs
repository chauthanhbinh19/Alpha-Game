using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIRNsService : IUserHIRNsService
{
    private readonly IUserHIRNsRepository _userHIRNsRepository;

    public UserHIRNsService(IUserHIRNsRepository userHIRNsRepository)
    {
        _userHIRNsRepository = userHIRNsRepository;
    }

    public static IUserHIRNsService Create() => ServiceContainer.GetService<IUserHIRNsService>();

    public async Task<UserHIRNs> GetUserHIRNsAsync(string userId, string id)
    {
        return await _userHIRNsRepository.GetUserHIRNsAsync(userId, id);
    }

    public async Task<UserHIRNs> GetSumUserHIRNsAsync(string userId)
    {
        return await _userHIRNsRepository.GetSumUserHIRNsAsync(userId);
    }

    public async Task InsertOrUpdateUserHIRNsAsync(string userId, UserHIRNs HIRNs, string id)
    {
        await _userHIRNsRepository.InsertOrUpdateUserHIRNsAsync(userId, HIRNs, id);
    }
}