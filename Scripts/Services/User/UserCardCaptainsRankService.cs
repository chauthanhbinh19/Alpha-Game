using System.Threading.Tasks;

public class UserCardCaptainsRankService : IUserCardCaptainsRankService
{
    private readonly IUserCardCaptainsRankRepository _cardCaptainsRankRepository;

    public UserCardCaptainsRankService(IUserCardCaptainsRankRepository cardCaptainsRankRepository)
    {
        _cardCaptainsRankRepository = cardCaptainsRankRepository;
    }

    public static UserCardCaptainsRankService Create()
    {
        return new UserCardCaptainsRankService(new UserCardCaptainsRankRepository());
    }

    public async Task<Rank> GetCardCaptainRankAsync(string id, string card_id)
    {
        return await _cardCaptainsRankRepository.GetCardCaptainRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardCaptainRankAsync(Rank rank, string card_id)
    {
        await _cardCaptainsRankRepository.InsertOrUpdateCardCaptainRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumCardCaptainsRankAsync(string user_id, string card_id)
    {
        return await _cardCaptainsRankRepository.GetSumCardCaptainsRankAsync(user_id, card_id);
    }
}
