public class UserCardAdmiralsRankService : IUserCardAdmiralsRankService
{
    private readonly IUserCardAdmiralsRankRepository _cardAdmiralsRankRepository;

    public UserCardAdmiralsRankService(IUserCardAdmiralsRankRepository cardAdmiralsRankRepository)
    {
        _cardAdmiralsRankRepository = cardAdmiralsRankRepository;
    }

    public static UserCardAdmiralsRankService Create()
    {
        return new UserCardAdmiralsRankService(new UserCardAdmiralsRankRepository());
    }

    public Rank GetCardAdmiralsRank(string type, string card_id)
    {
        return _cardAdmiralsRankRepository.GetCardAdmiralsRank(type, card_id);
    }

    public void InsertOrUpdateCardAdmiralsRank(Rank rank, string type, string card_id)
    {
        _cardAdmiralsRankRepository.InsertOrUpdateCardAdmiralsRank(rank, type, card_id);
    }

    public Rank GetSumCardAdmiralsRank(string user_id, string card_id)
    {
        return _cardAdmiralsRankRepository.GetSumCardAdmiralsRank(user_id, card_id);
    }
}
