public class UserCardColonelsRankService : IUserCardColonelsRankService
{
    private readonly IUserCardColonelsRankRepository _cardColonelsRankRepository;

    public UserCardColonelsRankService(IUserCardColonelsRankRepository cardColonelsRankRepository)
    {
        _cardColonelsRankRepository = cardColonelsRankRepository;
    }

    public static UserCardColonelsRankService Create()
    {
        return new UserCardColonelsRankService(new UserCardColonelsRankRepository());
    }

    public Rank GetCardColonelsRank(string type, string card_id)
    {
        return _cardColonelsRankRepository.GetCardColonelsRank(type, card_id);
    }

    public void InsertOrUpdateCardColonelsRank(Rank rank, string type, string card_id)
    {
        _cardColonelsRankRepository.InsertOrUpdateCardColonelsRank(rank, type, card_id);
    }

    public Rank GetSumCardColonelsRank(string user_id, string card_id)
    {
        return _cardColonelsRankRepository.GetSumCardColonelsRank(user_id, card_id);
    }
}
