using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIENsService : IUserHIENsService
{
    private readonly IUserHIENsRepository _userHIENsRepository;

    public UserHIENsService(IUserHIENsRepository userHIENsRepository)
    {
        _userHIENsRepository = userHIENsRepository;
    }

    public static IUserHIENsService Create() => ServiceContainer.GetService<IUserHIENsService>();

    public async Task<UserHIENs> GetUserHIENsAsync(string userId, string id)
    {
        return await _userHIENsRepository.GetUserHIENsAsync(userId, id);
    }

    public async Task<UserHIENs> GetSumUserHIENsAsync(string userId)
    {
        return await _userHIENsRepository.GetSumUserHIENsAsync(userId);
    }

    public async Task InsertOrUpdateUserHIENsAsync(string userId, UserHIENs HIENs, string id)
    {
        await _userHIENsRepository.InsertOrUpdateUserHIENsAsync(userId, HIENs, id);
    }

    
}