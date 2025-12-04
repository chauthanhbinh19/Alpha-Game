using System.Threading.Tasks;

public class UserCardMilitariesRankService : IUserCardMilitariesRankService
{
    private readonly IUserCardMilitariesRankRepository _cardMilitaryRankRepository;

    public UserCardMilitariesRankService(IUserCardMilitariesRankRepository cardMilitaryRankRepository)
    {
        _cardMilitaryRankRepository = cardMilitaryRankRepository;
    }

    public static UserCardMilitariesRankService Create()
    {
        return new UserCardMilitariesRankService(new UserCardMilitariesRankRepository());
    }

    public async Task<Rank> GetCardMilitaryRankAsync(string type, string card_id)
    {
        return await _cardMilitaryRankRepository.GetCardMilitaryRankAsync(type, card_id);
    }

    public async Task InsertOrUpdateCardMilitaryRankAsync(Rank rank, string type, string card_id)
    {
        await _cardMilitaryRankRepository.InsertOrUpdateCardMilitaryRankAsync(rank, type, card_id);
    }

    public async Task<Rank> GetSumCardMilitariesRankAsync(string user_id, string card_id)
    {
        return await _cardMilitaryRankRepository.GetSumCardMilitariesRankAsync(user_id, card_id);
    }
}
