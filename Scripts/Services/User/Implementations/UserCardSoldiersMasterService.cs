using System.Threading.Tasks;

public class UserCardSoldiersMasterService : IUserCardSoldiersMasterService
{
     private static UserCardSoldiersMasterService _instance;
    private readonly IUserCardSoldiersMasterRepository _userCardSoldiersMasterRepository;

    public UserCardSoldiersMasterService(IUserCardSoldiersMasterRepository userCardSoldiersMasterRepository)
    {
        _userCardSoldiersMasterRepository = userCardSoldiersMasterRepository;
    }

    public static UserCardSoldiersMasterService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardSoldiersMasterService(new UserCardSoldiersMasterRepository());
        }
        return _instance;
    }

    public async Task<Master> GetCardSoldierMasterAsync(string id, string card_id)
    {
        return await _userCardSoldiersMasterRepository.GetCardSoldierMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardSoldierMasterAsync(Master master, string card_id)
    {
        await _userCardSoldiersMasterRepository.InsertOrUpdateCardSoldierMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumCardSoldiersMasterAsync(string user_id, string card_id)
    {
        return await _userCardSoldiersMasterRepository.GetSumCardSoldiersMasterAsync(user_id, card_id);
    }
}
