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

    public async Task<Master> GetUserCardCaptainMasterAsync(string userId, string id, string card_id)
    {
        return await _userCardCaptainsMasterRepository.GetUserCardCaptainMasterAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserCardCaptainMasterAsync(string userId, UserMasters userMaster, string card_id)
    {
        await _userCardCaptainsMasterRepository.InsertOrUpdateUserCardCaptainMasterAsync(userId, userMaster, card_id);
    }

    public async Task<Master> GetSumUserCardCaptainsMasterAsync(string userId, string card_id)
    {
        return await _userCardCaptainsMasterRepository.GetSumUserCardCaptainsMasterAsync(userId, card_id);
    }
}
