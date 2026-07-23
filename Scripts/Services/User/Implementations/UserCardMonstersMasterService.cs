using System.Threading.Tasks;

public class UserCardMonstersMasterService : IUserCardMonstersMasterService
{
    private static UserCardMonstersMasterService _instance;
    private readonly IUserCardMonstersMasterRepository _userCardMonstersMasterRepository;

    public UserCardMonstersMasterService(IUserCardMonstersMasterRepository userCardMonstersMasterRepository)
    {
        _userCardMonstersMasterRepository = userCardMonstersMasterRepository;
    }

    public static UserCardMonstersMasterService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardMonstersMasterService(new UserCardMonstersMasterRepository());
        }
        return _instance;
    }

    public async Task<Master> GetUserCardMonsterMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardMonstersMasterRepository.GetUserCardMonsterMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardMonsterMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardMonstersMasterRepository.InsertOrUpdateUserCardMonsterMasterAsync(userId, userMaster, cardId);
    }

    public async Task<Master> GetSumUserCardMonstersMasterAsync(string userId, string cardId)
    {
        return await _userCardMonstersMasterRepository.GetSumUserCardMonstersMasterAsync(userId, cardId);
    }
}
