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

    public async Task<Master> GetCardAdmiralMasterAsync(string id, string card_id)
    {
        return await _userCardAdmiralsMasterRepository.GetCardAdmiralMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardAdmiralMasterAsync(Master master, string card_id)
    {
        await _userCardAdmiralsMasterRepository.InsertOrUpdateCardAdmiralMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumCardAdmiralsMasterAsync(string user_id, string card_id)
    {
        return await _userCardAdmiralsMasterRepository.GetSumCardAdmiralsMasterAsync(user_id, card_id);
    }
}
