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

    public async Task<Master> GetUserCardSpellMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardSpellsMasterRepository.GetUserCardSpellMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardSpellMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardSpellsMasterRepository.InsertOrUpdateUserCardSpellMasterAsync(userId, userMaster, cardId);
    }

    public async Task<Master> GetSumUserCardSpellsMasterAsync(string userId, string cardId)
    {
        return await _userCardSpellsMasterRepository.GetSumUserCardSpellsMasterAsync(userId, cardId);
    }
}
