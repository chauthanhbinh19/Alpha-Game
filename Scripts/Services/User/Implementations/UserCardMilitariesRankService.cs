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

    public async Task<Rank> GetUserCardMilitaryRankAsync(string userId, string id, string cardId)
    {
        return await _userCardMilitariesRankRepository.GetUserCardMilitaryRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardMilitaryRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardMilitariesRankRepository.InsertOrUpdateUserCardMilitaryRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumUserCardMilitariesRankAsync(string userId, string cardId)
    {
        return await _userCardMilitariesRankRepository.GetSumUserCardMilitariesRankAsync(userId, cardId);
    }
}
