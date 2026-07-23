using System.Threading.Tasks;

public class UserCardColonelsMasterService : IUserCardColonelsMasterService
{
    private static UserCardColonelsMasterService _instance;
    private readonly IUserCardColonelsMasterRepository _userCardColonelsMasterRepository;

    public UserCardColonelsMasterService(IUserCardColonelsMasterRepository userCardColonelsMasterRepository)
    {
        _userCardColonelsMasterRepository = userCardColonelsMasterRepository;
    }

    public static UserCardColonelsMasterService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardColonelsMasterService(new UserCardColonelsMasterRepository());
        }
        return _instance;
    }

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
