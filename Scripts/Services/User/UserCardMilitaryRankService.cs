public class UserCardMilitaryRankService : IUserCardMilitaryRankService
{
    private readonly IUserCardMilitaryRankRepository _cardMilitaryRankRepository;

    public UserCardMilitaryRankService(IUserCardMilitaryRankRepository cardMilitaryRankRepository)
    {
        _cardMilitaryRankRepository = cardMilitaryRankRepository;
    }

    public static UserCardMilitaryRankService Create()
    {
        return new UserCardMilitaryRankService(new UserCardMilitaryRankRepository());
    }

    public Rank GetCardMilitaryRank(string type, string card_id)
    {
        return _cardMilitaryRankRepository.GetCardMilitaryRank(type, card_id);
    }

    public void InsertOrUpdateCardMilitaryRank(Rank rank, string type, string card_id)
    {
        _cardMilitaryRankRepository.InsertOrUpdateCardMilitaryRank(rank, type, card_id);
    }

    public Rank GetSumCardMilitaryRank(string user_id, string card_id)
    {
        return _cardMilitaryRankRepository.GetSumCardMilitaryRank(user_id, card_id);
    }
}
