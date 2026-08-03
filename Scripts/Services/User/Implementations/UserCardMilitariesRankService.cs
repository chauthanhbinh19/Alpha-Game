using System.Threading.Tasks;

public class UserCardMilitariesRankService : IUserCardMilitariesRankService
{
    private readonly IUserCardMilitariesRankRepository _userCardMilitariesRankRepository;

    public UserCardMilitariesRankService(IUserCardMilitariesRankRepository userCardMilitariesRankRepository)
    {
        _userCardMilitariesRankRepository = userCardMilitariesRankRepository;
    }

    public static IUserCardMilitariesRankService Create() => ServiceContainer.GetService<IUserCardMilitariesRankService>();

    public async Task<UserRanks> GetUserCardMilitaryRankAsync(string userId, string id, string cardId)
    {
        return await _userCardMilitariesRankRepository.GetUserCardMilitaryRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardMilitaryRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardMilitariesRankRepository.InsertOrUpdateUserCardMilitaryRankAsync(userId, userRank, cardId);
    }

    public async Task<UserRanks> GetSumUserCardMilitariesRankAsync(string userId, string cardId)
    {
        return await _userCardMilitariesRankRepository.GetSumUserCardMilitariesRankAsync(userId, cardId);
    }
}
