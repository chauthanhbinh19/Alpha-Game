using System.Threading.Tasks;

public class UserCardAdmiralsMasterService : IUserCardAdmiralsMasterService
{
    private static UserCardAdmiralsMasterService _instance;
    private readonly IUserCardAdmiralsMasterRepository _userCardAdmiralsMasterRepository;

    public UserCardAdmiralsMasterService(IUserCardAdmiralsMasterRepository userCardAdmiralsMasterRepository)
    {
        _userCardAdmiralsMasterRepository = userCardAdmiralsMasterRepository;
    }

    public static UserCardAdmiralsMasterService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardAdmiralsMasterService(new UserCardAdmiralsMasterRepository());
        }
        return _instance;
    }

    public async Task<Master> GetUserCardAdmiralMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardAdmiralsMasterRepository.GetUserCardAdmiralMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardAdmiralMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardAdmiralsMasterRepository.InsertOrUpdateUserCardAdmiralMasterAsync(userId, userMaster, cardId);
    }

    public async Task<Master> GetSumUserCardAdmiralsMasterAsync(string userId, string cardId)
    {
        return await _userCardAdmiralsMasterRepository.GetSumUserCardAdmiralsMasterAsync(userId, cardId);
    }
}
