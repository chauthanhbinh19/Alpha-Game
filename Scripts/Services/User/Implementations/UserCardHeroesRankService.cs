using System.Threading.Tasks;

public class UserCardHeroesRankService : IUserCardHeroesRankService
{
    private readonly IUserCardHeroesRankRepository _userCardHeroesRankRepository;

    public UserCardHeroesRankService(IUserCardHeroesRankRepository userCardHeroesRankRepository)
    {
        _userCardHeroesRankRepository = userCardHeroesRankRepository;
    }

    public static IUserCardHeroesRankService Create() => ServiceContainer.GetService<IUserCardHeroesRankService>();

    public async Task<UserRanks> GetUserCardHeroRankAsync(string userId, string id, string cardId)
    {
        return await _userCardHeroesRankRepository.GetUserCardHeroRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardHeroRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardHeroesRankRepository.InsertOrUpdateUserCardHeroRankAsync(userId, userRank, cardId);
    }

    public async Task<UserRanks> GetSumUserCardHeroesRankAsync(string userId, string cardId)
    {
        return await _userCardHeroesRankRepository.GetSumUserCardHeroesRankAsync(userId, cardId);
    }
}
