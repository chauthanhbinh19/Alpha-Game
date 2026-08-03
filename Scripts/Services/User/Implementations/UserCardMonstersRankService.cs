using System.Threading.Tasks;

public class UserCardMonstersRankService : IUserCardMonstersRankService
{
    private readonly IUserCardMonstersRankRepository _userCardMonstersRankRepository;

    public UserCardMonstersRankService(IUserCardMonstersRankRepository userCardMonstersRankRepository)
    {
        _userCardMonstersRankRepository = userCardMonstersRankRepository;
    }

    public static IUserCardMonstersRankService Create() => ServiceContainer.GetService<IUserCardMonstersRankService>();

    public async Task<UserRanks> GetUserCardMonsterRankAsync(string userId, string id, string cardId)
    {
        return await _userCardMonstersRankRepository.GetUserCardMonsterRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardMonsterRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardMonstersRankRepository.InsertOrUpdateUserCardMonsterRankAsync(userId, userRank, cardId);
    }

    public async Task<UserRanks> GetSumUserCardMonstersRankAsync(string userId, string cardId)
    {
        return await _userCardMonstersRankRepository.GetSumUserCardMonstersRankAsync(userId, cardId);
    }
}
