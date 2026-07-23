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

    public async Task<Master> GetUserCardSpellMasterAsync(string userId, string id, string card_id)
    {
        return await _userCardSpellsMasterRepository.GetUserCardSpellMasterAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserCardSpellMasterAsync(string userId, UserMasters userMaster, string card_id)
    {
        await _userCardSpellsMasterRepository.InsertOrUpdateUserCardSpellMasterAsync(userId, userMaster, card_id);
    }

    public async Task<Master> GetSumUserCardSpellsMasterAsync(string userId, string card_id)
    {
        return await _userCardSpellsMasterRepository.GetSumUserCardSpellsMasterAsync(userId, card_id);
    }
}
