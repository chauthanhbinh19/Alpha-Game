using System.Threading.Tasks;

public class UserCardMilitariesMasterService : IUserCardMilitariesMasterService
{
    private readonly IUserCardMilitariesMasterRepository _userCardMilitariesMasterRepository;

    public UserCardMilitariesMasterService(IUserCardMilitariesMasterRepository userCardMilitariesMasterRepository)
    {
        _userCardMilitariesMasterRepository = userCardMilitariesMasterRepository;
    }

    public static IUserCardMilitariesMasterService Create() => ServiceContainer.GetService<IUserCardMilitariesMasterService>();

    public async Task<UserMasters> GetUserCardMilitaryMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardMilitariesMasterRepository.GetUserCardMilitaryMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardMilitaryMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardMilitariesMasterRepository.InsertOrUpdateUserCardMilitaryMasterAsync(userId, userMaster, cardId);
    }

    public async Task<UserMasters> GetSumUserCardMilitariesMasterAsync(string userId, string cardId)
    {
        return await _userCardMilitariesMasterRepository.GetSumUserCardMilitariesMasterAsync(userId, cardId);
    }
}
