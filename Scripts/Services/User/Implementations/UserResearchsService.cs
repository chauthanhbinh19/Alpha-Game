using System.Collections.Generic;
using System.Threading.Tasks;
public class UserResearchsService : IUserResearchsService
{
    private readonly IUserResearchsRepository _userResearchsRepository;

    public UserResearchsService(IUserResearchsRepository userResearchsRepository)
    {
        _userResearchsRepository = userResearchsRepository;
    }

    public static IUserResearchsService Create() => ServiceContainer.GetService<IUserResearchsService>();

    public async Task<UserResearchs> GetUserResearchsAsync(string userId, string id)
    {
        return await _userResearchsRepository.GetUserResearchsAsync(userId, id);
    }

    public async Task<UserResearchs> GetSumUserResearchsAsync(string userId)
    {
        return await _userResearchsRepository.GetSumUserResearchsAsync(userId);
    }

    public async Task InsertOrUpdateUserResearchsAsync(string userId, UserResearchs Researchs, string id)
    {
        await _userResearchsRepository.InsertOrUpdateUserResearchsAsync(userId, Researchs, id);
    }
}