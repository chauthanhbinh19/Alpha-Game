using System.Collections.Generic;
using System.Threading.Tasks;
public class UserResearchsService : IUserResearchsService
{
    private static UserResearchsService _instance;
    private readonly IUserResearchsRepository _userResearchsRepository;

    public UserResearchsService(IUserResearchsRepository userResearchsRepository)
    {
        _userResearchsRepository = userResearchsRepository;
    }

    public static UserResearchsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserResearchsService(new UserResearchsRepository());
        }
        return _instance;
    }

    public async Task<UserResearchs> GetUserResearchsAsync(string id)
    {
        return await _userResearchsRepository.GetUserResearchsAsync(id);
    }

    public async Task<UserResearchs> GetSumUserResearchsAsync(string user_id)
    {
        return await _userResearchsRepository.GetSumUserResearchsAsync(user_id);
    }

    public async Task InsertOrUpdateUserResearchsAsync(string userId, UserResearchs Researchs, string id)
    {
        await _userResearchsRepository.InsertOrUpdateUserResearchsAsync(userId, Researchs, id);
    }
}