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

    public async Task<Master> GetCardColonelMasterAsync(string id, string card_id)
    {
        return await _userCardColonelsMasterRepository.GetCardColonelMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardColonelMasterAsync(Master master, string card_id)
    {
        await _userCardColonelsMasterRepository.InsertOrUpdateCardColonelMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumCardColonelsMasterAsync(string user_id, string card_id)
    {
        return await _userCardColonelsMasterRepository.GetSumCardColonelsMasterAsync(user_id, card_id);
    }
}
