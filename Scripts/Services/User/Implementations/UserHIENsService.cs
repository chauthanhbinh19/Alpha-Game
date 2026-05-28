using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIENsService : IUserHIENsService
{
    private static UserHIENsService _instance;
    private readonly IUserHIENsRepository _userHIENsRepository;

    public UserHIENsService(IUserHIENsRepository userHIENsRepository)
    {
        _userHIENsRepository = userHIENsRepository;
    }

    public static UserHIENsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserHIENsService(new UserHIENsRepository());
        }
        return _instance;
    }

    public async Task<UserHIENs> GetUserHIENsAsync(string id)
    {
        return await _userHIENsRepository.GetUserHIENsAsync(id);
    }

    public async Task<UserHIENs> GetSumUserHIENsAsync(string user_id)
    {
        return await _userHIENsRepository.GetSumUserHIENsAsync(user_id);
    }

    public async Task InsertOrUpdateUserHIENsAsync(string userId, UserHIENs HIENs, string id)
    {
        await _userHIENsRepository.InsertOrUpdateUserHIENsAsync(userId, HIENs, id);
    }

    
}