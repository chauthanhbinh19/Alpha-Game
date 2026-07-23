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

    public async Task<Master> GetUserCardSoldierMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardSoldiersMasterRepository.GetUserCardSoldierMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardSoldierMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardSoldiersMasterRepository.InsertOrUpdateUserCardSoldierMasterAsync(userId, userMaster, cardId);
    }

    public async Task<Master> GetSumUserCardSoldiersMasterAsync(string userId, string cardId)
    {
        return await _userCardSoldiersMasterRepository.GetSumUserCardSoldiersMasterAsync(userId, cardId);
    }
}
