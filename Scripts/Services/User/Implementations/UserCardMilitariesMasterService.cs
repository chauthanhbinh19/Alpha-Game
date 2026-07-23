using System.Threading.Tasks;

public class UserCardMilitariesMasterService : IUserCardMilitariesMasterService
{
    private static UserCardMilitariesMasterService _instance;
    private readonly IUserCardMilitariesMasterRepository _userCardMilitariesMasterRepository;

    public UserCardMilitariesMasterService(IUserCardMilitariesMasterRepository userCardMilitariesMasterRepository)
    {
        _userCardMilitariesMasterRepository = userCardMilitariesMasterRepository;
    }

    public static UserCardMilitariesMasterService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardMilitariesMasterService(new UserCardMilitariesMasterRepository());
        }
        return _instance;
    }

    public async Task<Master> GetUserCardMilitaryMasterAsync(string userId, string id, string card_id)
    {
        return await _userCardMilitariesMasterRepository.GetUserCardMilitaryMasterAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserCardMilitaryMasterAsync(string userId, UserMasters userMaster, string card_id)
    {
        await _userCardMilitariesMasterRepository.InsertOrUpdateUserCardMilitaryMasterAsync(userId, userMaster, card_id);
    }

    public async Task<Master> GetSumUserCardMilitariesMasterAsync(string userId, string card_id)
    {
        return await _userCardMilitariesMasterRepository.GetSumUserCardMilitariesMasterAsync(userId, card_id);
    }
}
