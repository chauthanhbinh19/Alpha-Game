using System.Threading.Tasks;

public class UserCardSpellsMasterService : IUserCardSpellsMasterService
{
     private static UserCardSpellsMasterService _instance;
    private readonly IUserCardSpellsMasterRepository _userCardSpellsMasterRepository;

    public UserCardSpellsMasterService(IUserCardSpellsMasterRepository userCardSpellsMasterRepository)
    {
        _userCardSpellsMasterRepository = userCardSpellsMasterRepository;
    }

    public static UserCardSpellsMasterService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardSpellsMasterService(new UserCardSpellsMasterRepository());
        }
        return _instance;
    }

    public async Task<Master> GetCardSpellMasterAsync(string id, string card_id)
    {
        return await _userCardSpellsMasterRepository.GetCardSpellMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardSpellMasterAsync(Master master, string card_id)
    {
        await _userCardSpellsMasterRepository.InsertOrUpdateCardSpellMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumCardSpellsMasterAsync(string user_id, string card_id)
    {
        return await _userCardSpellsMasterRepository.GetSumCardSpellsMasterAsync(user_id, card_id);
    }
}
