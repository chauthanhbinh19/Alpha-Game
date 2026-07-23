using System.Threading.Tasks;

public class UserCardGeneralsMasterService : IUserCardGeneralsMasterService
{
    private static UserCardGeneralsMasterService _instance;
    private readonly IUserCardGeneralsMasterRepository _userCardGeneralsMasterRepository;

    public UserCardGeneralsMasterService(IUserCardGeneralsMasterRepository userCardGeneralsMasterRepository)
    {
        _userCardGeneralsMasterRepository = userCardGeneralsMasterRepository;
    }

    public static UserCardGeneralsMasterService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardGeneralsMasterService(new UserCardGeneralsMasterRepository());
        }
        return _instance;
    }

    public async Task<Master> GetUserCardGeneralMasterAsync(string userId, string id, string card_id)
    {
        return await _userCardGeneralsMasterRepository.GetUserCardGeneralMasterAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserCardGeneralMasterAsync(string userId, UserMasters userMaster, string card_id)
    {
        await _userCardGeneralsMasterRepository.InsertOrUpdateUserCardGeneralMasterAsync(userId, userMaster, card_id);
    }

    public async Task<Master> GetSumUserCardGeneralsMasterAsync(string userId, string card_id)
    {
        return await _userCardGeneralsMasterRepository.GetSumUserCardGeneralsMasterAsync(userId, card_id);
    }
}
