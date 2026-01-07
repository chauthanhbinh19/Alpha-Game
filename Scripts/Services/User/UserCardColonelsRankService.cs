using System.Threading.Tasks;

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

    public async Task<Rank> GetCardColonelRankAsync(string id, string card_id)
    {
        return await _cardColonelsRankRepository.GetCardColonelRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardColonelRankAsync(Rank rank, string card_id)
    {
        await _cardColonelsRankRepository.InsertOrUpdateCardColonelRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumCardColonelsRankAsync(string user_id, string card_id)
    {
        return await _cardColonelsRankRepository.GetSumCardColonelsRankAsync(user_id, card_id);
    }
}
