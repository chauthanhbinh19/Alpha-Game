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

    public async Task<Master> GetCardMilitaryMasterAsync(string id, string card_id)
    {
        return await _userCardMilitariesMasterRepository.GetCardMilitaryMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardMilitaryMasterAsync(Master master, string card_id)
    {
        await _userCardMilitariesMasterRepository.InsertOrUpdateCardMilitaryMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumCardMilitariesMasterAsync(string user_id, string card_id)
    {
        return await _userCardMilitariesMasterRepository.GetSumCardMilitariesMasterAsync(user_id, card_id);
    }
}
