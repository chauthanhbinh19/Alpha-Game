using System.Collections.Generic;
using System.Threading.Tasks;
public class UserHIRNsService : IUserHIRNsService
{
    private static UserHIRNsService _instance;
    private readonly IUserHIRNsRepository _userHIRNsRepository;

    public UserHIRNsService(IUserHIRNsRepository userHIRNsRepository)
    {
        _userHIRNsRepository = userHIRNsRepository;
    }

    public static UserHIRNsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserHIRNsService(new UserHIRNsRepository());
        }
        return _instance;
    }

    public async Task<UserHIRNs> GetUserHIRNsAsync(string id)
    {
        return await _userHIRNsRepository.GetUserHIRNsAsync(id);
    }

    public async Task<UserHIRNs> GetSumUserHIRNsAsync(string user_id)
    {
        return await _userHIRNsRepository.GetSumUserHIRNsAsync(user_id);
    }

    public async Task InsertOrUpdateUserHIRNsAsync(string userId, UserHIRNs HIRNs, string id)
    {
        await _userHIRNsRepository.InsertOrUpdateUserHIRNsAsync(userId, HIRNs, id);
    }
}