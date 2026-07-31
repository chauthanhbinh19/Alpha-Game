using System.Threading.Tasks;

public class UserCardMonstersMasterService : IUserCardMonstersMasterService
{
    private readonly IUserCardMonstersMasterRepository _userCardMonstersMasterRepository;

    public UserCardMonstersMasterService(IUserCardMonstersMasterRepository userCardMonstersMasterRepository)
    {
        _userCardMonstersMasterRepository = userCardMonstersMasterRepository;
    }

    public static IUserCardMonstersMasterService Create() => ServiceContainer.GetService<IUserCardMonstersMasterService>();

    public async Task<Master> GetUserCardMonsterMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardMonstersMasterRepository.GetUserCardMonsterMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardMonsterMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardMonstersMasterRepository.InsertOrUpdateUserCardMonsterMasterAsync(userId, userMaster, cardId);
    }

    public async Task<Master> GetSumUserCardMonstersMasterAsync(string userId, string cardId)
    {
        return await _userCardMonstersMasterRepository.GetSumUserCardMonstersMasterAsync(userId, cardId);
    }
}
