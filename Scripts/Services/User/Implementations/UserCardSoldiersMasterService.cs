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

    public async Task<Master> GetUserCardSoldierMasterAsync(string userId, string id, string card_id)
    {
        return await _userCardSoldiersMasterRepository.GetUserCardSoldierMasterAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserCardSoldierMasterAsync(string userId, UserMasters userMaster, string card_id)
    {
        await _userCardSoldiersMasterRepository.InsertOrUpdateUserCardSoldierMasterAsync(userId, userMaster, card_id);
    }

    public async Task<Master> GetSumUserCardSoldiersMasterAsync(string userId, string card_id)
    {
        return await _userCardSoldiersMasterRepository.GetSumUserCardSoldiersMasterAsync(userId, card_id);
    }
}
