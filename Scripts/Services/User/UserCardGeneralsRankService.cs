public class UserCardGeneralsRankService : IUserCardGeneralsRankService
{
    private readonly IUserCardGeneralsRankRepository _cardGeneralsRankRepository;

    public UserCardGeneralsRankService(IUserCardGeneralsRankRepository cardGeneralsRankRepository)
    {
        _cardGeneralsRankRepository = cardGeneralsRankRepository;
    }

    public static UserCardGeneralsRankService Create()
    {
        return new UserCardGeneralsRankService(new UserCardGeneralsRankRepository());
    }

    public Rank GetCardGeneralsRank(string type, string card_id)
    {
        return _cardGeneralsRankRepository.GetCardGeneralsRank(type, card_id);
    }

    public void InsertOrUpdateCardGeneralsRank(Rank rank, string type, string card_id)
    {
        _cardGeneralsRankRepository.InsertOrUpdateCardGeneralsRank(rank, type, card_id);
    }

    public Rank GetSumCardGeneralsRank(string user_id, string card_id)
    {
        return _cardGeneralsRankRepository.GetSumCardGeneralsRank(user_id, card_id);
    }
}
