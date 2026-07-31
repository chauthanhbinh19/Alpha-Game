using System.Threading.Tasks;

public class UserCardSpellsRankService : IUserCardSpellsRankService
{
    private readonly IUserCardSpellsRankRepository _userCardSpellsRankRepository;

    public UserCardSpellsRankService(IUserCardSpellsRankRepository userCardSpellsRankRepository)
    {
        _userCardSpellsRankRepository = userCardSpellsRankRepository;
    }

    public static IUserCardSpellsRankService Create() => ServiceContainer.GetService<IUserCardSpellsRankService>();

    public async Task<Rank> GetUserCardSpellRankAsync(string userId, string id, string cardId)
    {
        return await _userCardSpellsRankRepository.GetUserCardSpellRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardSpellRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardSpellsRankRepository.InsertOrUpdateUserCardSpellRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumUserCardSpellsRankAsync(string userId, string cardId)
    {
        return await _userCardSpellsRankRepository.GetSumUserCardSpellsRankAsync(userId, cardId);
    }
}
