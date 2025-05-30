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

    public Rank GetCardHeroesRank(string type, string card_id)
    {
        return _cardHeroesRankRepository.GetCardHeroesRank(type, card_id);
    }

    public void InsertOrUpdateCardHeroesRank(Rank rank, string type, string card_id)
    {
        _cardHeroesRankRepository.InsertOrUpdateCardHeroesRank(rank, type, card_id);
    }

    public Rank GetSumCardHeroesRank(string user_id, string card_id)
    {
        return _cardHeroesRankRepository.GetSumCardHeroesRank(user_id, card_id);
    }
}
