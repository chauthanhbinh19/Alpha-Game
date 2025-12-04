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

    public async Task<Rank> GetCardGeneralRankAsync(string type, string card_id)
    {
        return await _cardGeneralsRankRepository.GetCardGeneralRankAsync(type, card_id);
    }

    public async Task InsertOrUpdateCardGeneralRankAsync(Rank rank, string type, string card_id)
    {
        await _cardGeneralsRankRepository.InsertOrUpdateCardGeneralRankAsync(rank, type, card_id);
    }

    public async Task<Rank> GetSumCardGeneralsRankAsync(string user_id, string card_id)
    {
        return await _cardGeneralsRankRepository.GetSumCardGeneralsRankAsync(user_id, card_id);
    }
}
