using System.Threading.Tasks;

public class UserBooksMasterService : IUserBooksMasterService
{
    private readonly IUserBooksMasterRepository _userBooksMasterRepository;

    public UserBooksMasterService(IUserBooksMasterRepository userBooksMasterRepository)
    {
        _userBooksMasterRepository = userBooksMasterRepository;
    }

    public static IUserBooksMasterService Create() => ServiceContainer.GetService<IUserBooksMasterService>();

    public async Task<UserMasters> GetUserBookMasterAsync(string userId, string id, string cardId)
    {
        return await _userBooksMasterRepository.GetUserBookMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserBookMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userBooksMasterRepository.InsertOrUpdateUserBookMasterAsync(userId, userMaster, cardId);
    }

    public async Task<UserMasters> GetSumUserBooksMasterAsync(string userId, string cardId)
    {
        return await _userBooksMasterRepository.GetSumUserBooksMasterAsync(userId, cardId);
    }
}
