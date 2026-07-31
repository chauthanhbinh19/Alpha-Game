using System.Threading.Tasks;

public class UserCardColonelsMasterService : IUserCardColonelsMasterService
{
    private readonly IUserCardColonelsMasterRepository _userCardColonelsMasterRepository;

    public UserCardColonelsMasterService(IUserCardColonelsMasterRepository userCardColonelsMasterRepository)
    {
        _userCardColonelsMasterRepository = userCardColonelsMasterRepository;
    }

    public static IUserCardColonelsMasterService Create() => ServiceContainer.GetService<IUserCardColonelsMasterService>();

    public async Task<Master> GetUserCardColonelMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardColonelsMasterRepository.GetUserCardColonelMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardColonelMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardColonelsMasterRepository.InsertOrUpdateUserCardColonelMasterAsync(userId, userMaster, cardId);
    }

    public async Task<Master> GetSumUserCardColonelsMasterAsync(string userId, string cardId)
    {
        return await _userCardColonelsMasterRepository.GetSumUserCardColonelsMasterAsync(userId, cardId);
    }
}
