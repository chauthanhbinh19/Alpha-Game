using System.Threading.Tasks;

public class UserCardColonelsRankService : IUserCardColonelsRankService
{
    private readonly IUserCardColonelsRankRepository _userCardColonelsRankRepository;

    public UserCardColonelsRankService(IUserCardColonelsRankRepository userCardColonelsRankRepository)
    {
        _userCardColonelsRankRepository = userCardColonelsRankRepository;
    }

    public static IUserCardColonelsRankService Create() => ServiceContainer.GetService<IUserCardColonelsRankService>();

    public async Task<Rank> GetUserCardColonelRankAsync(string userId, string id, string cardId)
    {
        return await _userCardColonelsRankRepository.GetUserCardColonelRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardColonelRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardColonelsRankRepository.InsertOrUpdateUserCardColonelRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumUserCardColonelsRankAsync(string userId, string cardId)
    {
        return await _userCardColonelsRankRepository.GetSumUserCardColonelsRankAsync(userId, cardId);
    }
}
