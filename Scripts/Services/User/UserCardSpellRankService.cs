public class UserCardSpellRankService : IUserCardSpellRankService
{
    private readonly IUserCardSpellRankRepository _cardSpellRankRepository;

    public UserCardSpellRankService(IUserCardSpellRankRepository cardSpellRankRepository)
    {
        _cardSpellRankRepository = cardSpellRankRepository;
    }

    public static UserCardSpellRankService Create()
    {
        return new UserCardSpellRankService(new UserCardSpellRankRepository());
    }

    public Rank GetCardSpellRank(string type, string card_id)
    {
        return _cardSpellRankRepository.GetCardSpellRank(type, card_id);
    }

    public void InsertOrUpdateCardSpellRank(Rank rank, string type, string card_id)
    {
        _cardSpellRankRepository.InsertOrUpdateCardSpellRank(rank, type, card_id);
    }

    public Rank GetSumCardSpellRank(string user_id, string card_id)
    {
        return _cardSpellRankRepository.GetSumCardSpellRank(user_id, card_id);
    }
}
