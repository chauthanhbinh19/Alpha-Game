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

    public async Task<Master> GetCardGeneralMasterAsync(string id, string card_id)
    {
        return await _userCardGeneralsMasterRepository.GetCardGeneralMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardGeneralMasterAsync(Master master, string card_id)
    {
        await _userCardGeneralsMasterRepository.InsertOrUpdateCardGeneralMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumCardGeneralsMasterAsync(string user_id, string card_id)
    {
        return await _userCardGeneralsMasterRepository.GetSumCardGeneralsMasterAsync(user_id, card_id);
    }
}
