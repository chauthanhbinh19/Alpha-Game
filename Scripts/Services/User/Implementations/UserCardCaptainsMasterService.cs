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

    public async Task<Master> GetCardCaptainMasterAsync(string id, string card_id)
    {
        return await _userCardCaptainsMasterRepository.GetCardCaptainMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardCaptainMasterAsync(Master master, string card_id)
    {
        await _userCardCaptainsMasterRepository.InsertOrUpdateCardCaptainMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumCardCaptainsMasterAsync(string user_id, string card_id)
    {
        return await _userCardCaptainsMasterRepository.GetSumCardCaptainsMasterAsync(user_id, card_id);
    }
}
