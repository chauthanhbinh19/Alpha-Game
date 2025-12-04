using System.Threading.Tasks;

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

    public async Task<Rank> GetCardMonsterRankAsync(string type, string card_id)
    {
        return await _cardMonstersRankRepository.GetCardMonsterRankAsync(type, card_id);
    }

    public async Task InsertOrUpdateCardMonsterRankAsync(Rank rank, string type, string card_id)
    {
        await _cardMonstersRankRepository.InsertOrUpdateCardMonsterRankAsync(rank, type, card_id);
    }

    public async Task<Rank> GetSumCardMonstersRankAsync(string user_id, string card_id)
    {
        return await _cardMonstersRankRepository.GetSumCardMonstersRankAsync(user_id, card_id);
    }
}
