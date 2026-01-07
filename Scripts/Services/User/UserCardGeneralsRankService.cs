using System.Threading.Tasks;

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

    public async Task<Rank> GetCardGeneralRankAsync(string id, string card_id)
    {
        return await _cardGeneralsRankRepository.GetCardGeneralRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardGeneralRankAsync(Rank rank, string card_id)
    {
        await _cardGeneralsRankRepository.InsertOrUpdateCardGeneralRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumCardGeneralsRankAsync(string user_id, string card_id)
    {
        return await _cardGeneralsRankRepository.GetSumCardGeneralsRankAsync(user_id, card_id);
    }
}
