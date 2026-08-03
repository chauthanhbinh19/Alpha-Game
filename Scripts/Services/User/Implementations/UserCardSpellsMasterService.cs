using System.Threading.Tasks;

public class UserCardSpellsMasterService : IUserCardSpellsMasterService
{
    private readonly IUserCardSpellsMasterRepository _userCardSpellsMasterRepository;

    public UserCardSpellsMasterService(IUserCardSpellsMasterRepository userCardSpellsMasterRepository)
    {
        _userCardSpellsMasterRepository = userCardSpellsMasterRepository;
    }

    public static IUserCardSpellsMasterService Create() => ServiceContainer.GetService<IUserCardSpellsMasterService>();

    public async Task<UserMasters> GetUserCardSpellMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardSpellsMasterRepository.GetUserCardSpellMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardSpellMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardSpellsMasterRepository.InsertOrUpdateUserCardSpellMasterAsync(userId, userMaster, cardId);
    }

    public async Task<UserMasters> GetSumUserCardSpellsMasterAsync(string userId, string cardId)
    {
        return await _userCardSpellsMasterRepository.GetSumUserCardSpellsMasterAsync(userId, cardId);
    }
}
