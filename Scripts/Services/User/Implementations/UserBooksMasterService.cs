using System.Threading.Tasks;

public class UserBooksMasterService : IUserBooksMasterService
{
    private static UserBooksMasterService _instance;
    private readonly IUserBooksMasterRepository _userBooksMasterRepository;

    public UserBooksMasterService(IUserBooksMasterRepository userBooksMasterRepository)
    {
        _userBooksMasterRepository = userBooksMasterRepository;
    }

    public static UserBooksMasterService Create()
    {
        if (_instance == null)
        {
            _instance = new UserBooksMasterService(new UserBooksMasterRepository());
        }
        return _instance;
    }

    public async Task<Master> GetUserBookMasterAsync(string userId, string id, string card_id)
    {
        return await _userBooksMasterRepository.GetUserBookMasterAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserBookMasterAsync(string userId, UserMasters userMaster, string card_id)
    {
        await _userBooksMasterRepository.InsertOrUpdateUserBookMasterAsync(userId, userMaster, card_id);
    }

    public async Task<Master> GetSumUserBooksMasterAsync(string userId, string card_id)
    {
        return await _userBooksMasterRepository.GetSumUserBooksMasterAsync(userId, card_id);
    }
}
