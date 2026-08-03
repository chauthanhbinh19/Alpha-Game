using System.Threading.Tasks;

public class UserCardSoldiersMasterService : IUserCardSoldiersMasterService
{
    private readonly IUserCardSoldiersMasterRepository _userCardSoldiersMasterRepository;

    public UserCardSoldiersMasterService(IUserCardSoldiersMasterRepository userCardSoldiersMasterRepository)
    {
        _userCardSoldiersMasterRepository = userCardSoldiersMasterRepository;
    }

    public static IUserCardSoldiersMasterService Create() => ServiceContainer.GetService<IUserCardSoldiersMasterService>();

    public async Task<UserMasters> GetUserCardSoldierMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardSoldiersMasterRepository.GetUserCardSoldierMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardSoldierMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardSoldiersMasterRepository.InsertOrUpdateUserCardSoldierMasterAsync(userId, userMaster, cardId);
    }

    public async Task<UserMasters> GetSumUserCardSoldiersMasterAsync(string userId, string cardId)
    {
        return await _userCardSoldiersMasterRepository.GetSumUserCardSoldiersMasterAsync(userId, cardId);
    }
}
