using System.Threading.Tasks;

public class UserCardSpellsRankService : IUserCardSpellsRankService
{
    private readonly IUserCardSpellsRankRepository _cardSpellRankRepository;

    public UserCardSpellsRankService(IUserCardSpellsRankRepository cardSpellRankRepository)
    {
        _cardSpellRankRepository = cardSpellRankRepository;
    }

    public static UserCardSpellsRankService Create()
    {
        return new UserCardSpellsRankService(new UserCardSpellsRankRepository());
    }

    public async Task<Rank> GetCardSpellRankAsync(string type, string card_id)
    {
        return await _cardSpellRankRepository.GetCardSpellRankAsync(type, card_id);
    }

    public async Task InsertOrUpdateCardSpellRankAsync(Rank rank, string type, string card_id)
    {
        await _cardSpellRankRepository.InsertOrUpdateCardSpellRankAsync(rank, type, card_id);
    }

    public async Task<Rank> GetSumCardSpellsRankAsync(string user_id, string card_id)
    {
        return await _cardSpellRankRepository.GetSumCardSpellsRankAsync(user_id, card_id);
    }
}
