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

    public async Task<Master> GetCardMonsterMasterAsync(string id, string card_id)
    {
        return await _userCardMonstersMasterRepository.GetCardMonsterMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardMonsterMasterAsync(string userId, UserMasters userMaster, string card_id)
    {
        await _userCardMonstersMasterRepository.InsertOrUpdateCardMonsterMasterAsync(userId, userMaster, card_id);
    }

    public async Task<Master> GetSumCardMonstersMasterAsync(string user_id, string card_id)
    {
        return await _userCardMonstersMasterRepository.GetSumCardMonstersMasterAsync(user_id, card_id);
    }
}
