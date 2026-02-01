using System.Threading.Tasks;

public class UserCardMonstersRankService : IUserCardMonstersRankService
{
     private static UserCardMonstersRankService _instance;
    private readonly IUserCardMonstersRankRepository _userCardMonstersRankRepository;

    public UserCardMonstersRankService(IUserCardMonstersRankRepository userCardMonstersRankRepository)
    {
        _userCardMonstersRankRepository = userCardMonstersRankRepository;
    }

    public static UserCardMonstersRankService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardMonstersRankService(new UserCardMonstersRankRepository());
        }
        return _instance;
    }

    public async Task<Rank> GetCardMonsterRankAsync(string id, string card_id)
    {
        return await _userCardMonstersRankRepository.GetCardMonsterRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardMonsterRankAsync(Rank rank, string card_id)
    {
        await _userCardMonstersRankRepository.InsertOrUpdateCardMonsterRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumCardMonstersRankAsync(string user_id, string card_id)
    {
        return await _userCardMonstersRankRepository.GetSumCardMonstersRankAsync(user_id, card_id);
    }
}
