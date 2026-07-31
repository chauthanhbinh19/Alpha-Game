using System.Threading.Tasks;

public class UserCardGeneralsMasterService : IUserCardGeneralsMasterService
{
    private readonly IUserCardGeneralsMasterRepository _userCardGeneralsMasterRepository;

    public UserCardGeneralsMasterService(IUserCardGeneralsMasterRepository userCardGeneralsMasterRepository)
    {
        _userCardGeneralsMasterRepository = userCardGeneralsMasterRepository;
    }

    public static IUserCardGeneralsMasterService Create() => ServiceContainer.GetService<IUserCardGeneralsMasterService>();

    public async Task<Master> GetUserCardGeneralMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardGeneralsMasterRepository.GetUserCardGeneralMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardGeneralMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardGeneralsMasterRepository.InsertOrUpdateUserCardGeneralMasterAsync(userId, userMaster, cardId);
    }

    public async Task<Master> GetSumUserCardGeneralsMasterAsync(string userId, string cardId)
    {
        return await _userCardGeneralsMasterRepository.GetSumUserCardGeneralsMasterAsync(userId, cardId);
    }
}
