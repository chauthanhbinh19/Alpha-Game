using System.Threading.Tasks;

public class UserCardMilitariesRankService : IUserCardMilitariesRankService
{
     private static UserCardMilitariesRankService _instance;
    private readonly IUserCardMilitariesRankRepository _userCardMilitariesRankRepository;

    public UserCardMilitariesRankService(IUserCardMilitariesRankRepository userCardMilitariesRankRepository)
    {
        _userCardMilitariesRankRepository = userCardMilitariesRankRepository;
    }

    public static UserCardMilitariesRankService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardMilitariesRankService(new UserCardMilitariesRankRepository());
        }
        return _instance;
    }

    public async Task<Rank> GetCardMilitaryRankAsync(string id, string card_id)
    {
        return await _userCardMilitariesRankRepository.GetCardMilitaryRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardMilitaryRankAsync(Rank rank, string card_id)
    {
        await _userCardMilitariesRankRepository.InsertOrUpdateCardMilitaryRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumCardMilitariesRankAsync(string user_id, string card_id)
    {
        return await _userCardMilitariesRankRepository.GetSumCardMilitariesRankAsync(user_id, card_id);
    }
}
