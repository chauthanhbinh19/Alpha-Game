public class UserCardCaptainsRankService : IUserCardCaptainsRankService
{
    private readonly IUserCardCaptainsRankRepository _cardCaptainsRankRepository;

    public UserCardCaptainsRankService(IUserCardCaptainsRankRepository cardCaptainsRankRepository)
    {
        _cardCaptainsRankRepository = cardCaptainsRankRepository;
    }

    public static UserCardCaptainsRankService Create()
    {
        return new UserCardCaptainsRankService(new UserCardCaptainsRankRepository());
    }

    public Rank GetCardCaptainsRank(string type, string card_id)
    {
        return _cardCaptainsRankRepository.GetCardCaptainsRank(type, card_id);
    }

    public void InsertOrUpdateCardCaptainsRank(Rank rank, string type, string card_id)
    {
        _cardCaptainsRankRepository.InsertOrUpdateCardCaptainsRank(rank, type, card_id);
    }

    public Rank GetSumCardCaptainsRank(string user_id, string card_id)
    {
        return _cardCaptainsRankRepository.GetSumCardCaptainsRank(user_id, card_id);
    }
}
