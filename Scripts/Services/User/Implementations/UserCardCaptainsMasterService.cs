using System.Threading.Tasks;

public class UserCardCaptainsMasterService : IUserCardCaptainsMasterService
{
    private static UserCardCaptainsMasterService _instance;
    private readonly IUserCardCaptainsMasterRepository _userCardCaptainsMasterRepository;

    public UserCardCaptainsMasterService(IUserCardCaptainsMasterRepository userCardCaptainsMasterRepository)
    {
        _userCardCaptainsMasterRepository = userCardCaptainsMasterRepository;
    }

    public static UserCardCaptainsMasterService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardCaptainsMasterService(new UserCardCaptainsMasterRepository());
        }
        return _instance;
    }

    public async Task<Master> GetUserCardCaptainMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardCaptainsMasterRepository.GetUserCardCaptainMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardCaptainMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardCaptainsMasterRepository.InsertOrUpdateUserCardCaptainMasterAsync(userId, userMaster, cardId);
    }

    public async Task<Master> GetSumUserCardCaptainsMasterAsync(string userId, string cardId)
    {
        return await _userCardCaptainsMasterRepository.GetSumUserCardCaptainsMasterAsync(userId, cardId);
    }
}
