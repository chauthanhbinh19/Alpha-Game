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

    public async Task<Master> GetUserCardGeneralMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardGeneralsMasterRepository.GetUserCardGeneralMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardGeneralMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardGeneralsMasterRepository.InsertOrUpdateUserCardGeneralMasterAsync(userId, userMaster, cardId);
    }

    public async Task<Master> GetSumUserCardGeneralsMasterAsync(string userId, string cardId)
    {
        return await _userCardGeneralsMasterRepository.GetSumUserCardGeneralsMasterAsync(userId, cardId);
    }
}
