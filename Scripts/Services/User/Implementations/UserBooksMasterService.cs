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

    public async Task<Master> GetUserBookMasterAsync(string userId, string id, string cardId)
    {
        return await _userBooksMasterRepository.GetUserBookMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserBookMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userBooksMasterRepository.InsertOrUpdateUserBookMasterAsync(userId, userMaster, cardId);
    }

    public async Task<Master> GetSumUserBooksMasterAsync(string userId, string cardId)
    {
        return await _userBooksMasterRepository.GetSumUserBooksMasterAsync(userId, cardId);
    }
}
