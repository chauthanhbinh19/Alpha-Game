public class UserCardMonstersRankService : IUserCardMonstersRankService
{
    private readonly IUserCardMonstersRankRepository _cardMonstersRankRepository;

    public UserCardMonstersRankService(IUserCardMonstersRankRepository cardMonstersRankRepository)
    {
        _cardMonstersRankRepository = cardMonstersRankRepository;
    }

    public static UserCardMonstersRankService Create()
    {
        return new UserCardMonstersRankService(new UserCardMonstersRankRepository());
    }

    public Rank GetCardMonstersRank(string type, string card_id)
    {
        return _cardMonstersRankRepository.GetCardMonstersRank(type, card_id);
    }

    public void InsertOrUpdateCardMonstersRank(Rank rank, string type, string card_id)
    {
        _cardMonstersRankRepository.InsertOrUpdateCardMonstersRank(rank, type, card_id);
    }

    public Rank GetSumCardMonstersRank(string user_id, string card_id)
    {
        return _cardMonstersRankRepository.GetSumCardMonstersRank(user_id, card_id);
    }
}
