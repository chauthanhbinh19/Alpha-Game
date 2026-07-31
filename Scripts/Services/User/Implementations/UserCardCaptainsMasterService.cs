using System.Threading.Tasks;

public class UserCardCaptainsMasterService : IUserCardCaptainsMasterService
{
    private readonly IUserCardCaptainsMasterRepository _userCardCaptainsMasterRepository;

    public UserCardCaptainsMasterService(IUserCardCaptainsMasterRepository userCardCaptainsMasterRepository)
    {
        _userCardCaptainsMasterRepository = userCardCaptainsMasterRepository;
    }

    public static IUserCardCaptainsMasterService Create() => ServiceContainer.GetService<IUserCardCaptainsMasterService>();

    public async Task<Master> GetUserCardCaptainMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardCaptainsMasterRepository.GetUserCardCaptainMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardCaptainMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardCaptainsMasterRepository.InsertOrUpdateUserCardCaptainMasterAsync(userId, userMaster, cardId);
    }

    public async Task<Master> GetSumUserCardCaptainsMasterAsync(string userId, string cardId)
    {
        return await _userCardCaptainsMasterRepository.GetSumUserCardCaptainsMasterAsync(userId, cardId);
    }
}
