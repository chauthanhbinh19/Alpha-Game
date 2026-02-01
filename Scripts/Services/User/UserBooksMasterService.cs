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

    public async Task<Master> GetBookMasterAsync(string id, string card_id)
    {
        return await _userBooksMasterRepository.GetBookMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateBookMasterAsync(Master master, string card_id)
    {
        await _userBooksMasterRepository.InsertOrUpdateBookMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumBooksMasterAsync(string user_id, string card_id)
    {
        return await _userBooksMasterRepository.GetSumBooksMasterAsync(user_id, card_id);
    }
}
