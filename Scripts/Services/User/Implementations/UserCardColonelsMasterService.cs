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

    public async Task<Master> GetUserCardColonelMasterAsync(string userId, string id, string card_id)
    {
        return await _userCardColonelsMasterRepository.GetUserCardColonelMasterAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserCardColonelMasterAsync(string userId, UserMasters userMaster, string card_id)
    {
        await _userCardColonelsMasterRepository.InsertOrUpdateUserCardColonelMasterAsync(userId, userMaster, card_id);
    }

    public async Task<Master> GetSumUserCardColonelsMasterAsync(string userId, string card_id)
    {
        return await _userCardColonelsMasterRepository.GetSumUserCardColonelsMasterAsync(userId, card_id);
    }
}
