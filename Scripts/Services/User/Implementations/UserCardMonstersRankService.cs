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

    public async Task<Rank> GetUserCardMonsterRankAsync(string userId, string id, string cardId)
    {
        return await _userCardMonstersRankRepository.GetUserCardMonsterRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardMonsterRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardMonstersRankRepository.InsertOrUpdateUserCardMonsterRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumUserCardMonstersRankAsync(string userId, string cardId)
    {
        return await _userCardMonstersRankRepository.GetSumUserCardMonstersRankAsync(userId, cardId);
    }
}
