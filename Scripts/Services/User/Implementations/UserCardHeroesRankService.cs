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

    public async Task<Rank> GetCardHeroRankAsync(string id, string card_id)
    {
        return await _userCardHeroesRankRepository.GetCardHeroRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardHeroRankAsync(Rank rank, string card_id)
    {
        await _userCardHeroesRankRepository.InsertOrUpdateCardHeroRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumCardHeroesRankAsync(string user_id, string card_id)
    {
        return await _userCardHeroesRankRepository.GetSumCardHeroesRankAsync(user_id, card_id);
    }
}
