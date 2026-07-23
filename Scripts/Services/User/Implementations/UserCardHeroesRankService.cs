using System.Threading.Tasks;

public class UserCardHeroesRankService : IUserCardHeroesRankService
{
    private static UserCardHeroesRankService _instance;
    private readonly IUserCardHeroesRankRepository _userCardHeroesRankRepository;

    public UserCardHeroesRankService(IUserCardHeroesRankRepository userCardHeroesRankRepository)
    {
        _userCardHeroesRankRepository = userCardHeroesRankRepository;
    }

    public static UserCardHeroesRankService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardHeroesRankService(new UserCardHeroesRankRepository());
        }
        return _instance;
    }

    public async Task<Rank> GetUserCardHeroRankAsync(string userId, string id, string cardId)
    {
        return await _userCardHeroesRankRepository.GetUserCardHeroRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardHeroRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardHeroesRankRepository.InsertOrUpdateUserCardHeroRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumUserCardHeroesRankAsync(string userId, string cardId)
    {
        return await _userCardHeroesRankRepository.GetSumUserCardHeroesRankAsync(userId, cardId);
    }
}
