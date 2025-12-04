using System.Threading.Tasks;

public class UserCardHeroesRankService : IUserCardHeroesRankService
{
    private readonly IUserCardHeroesRankRepository _cardHeroesRankRepository;

    public UserCardHeroesRankService(IUserCardHeroesRankRepository cardHeroesRankRepository)
    {
        _cardHeroesRankRepository = cardHeroesRankRepository;
    }

    public static UserCardHeroesRankService Create()
    {
        return new UserCardHeroesRankService(new UserCardHeroesRankRepository());
    }

    public async Task<Rank> GetCardHeroRankAsync(string type, string card_id)
    {
        return await _cardHeroesRankRepository.GetCardHeroRankAsync(type, card_id);
    }

    public async Task InsertOrUpdateCardHeroRankAsync(Rank rank, string type, string card_id)
    {
        await _cardHeroesRankRepository.InsertOrUpdateCardHeroRankAsync(rank, type, card_id);
    }

    public async Task<Rank> GetSumCardHeroesRankAsync(string user_id, string card_id)
    {
        return await _cardHeroesRankRepository.GetSumCardHeroesRankAsync(user_id, card_id);
    }
}
