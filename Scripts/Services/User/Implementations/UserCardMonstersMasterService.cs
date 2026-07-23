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

    public async Task<Master> GetUserCardMonsterMasterAsync(string userId, string id, string card_id)
    {
        return await _userCardMonstersMasterRepository.GetUserCardMonsterMasterAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserCardMonsterMasterAsync(string userId, UserMasters userMaster, string card_id)
    {
        await _userCardMonstersMasterRepository.InsertOrUpdateUserCardMonsterMasterAsync(userId, userMaster, card_id);
    }

    public async Task<Master> GetSumUserCardMonstersMasterAsync(string userId, string card_id)
    {
        return await _userCardMonstersMasterRepository.GetSumUserCardMonstersMasterAsync(userId, card_id);
    }
}
