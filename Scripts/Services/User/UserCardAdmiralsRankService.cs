using System.Threading.Tasks;

public class UserCardAdmiralsRankService : IUserCardAdmiralsRankService
{
    private readonly IUserCardAdmiralsRankRepository _cardAdmiralsRankRepository;

    public UserCardAdmiralsRankService(IUserCardAdmiralsRankRepository cardAdmiralsRankRepository)
    {
        _cardAdmiralsRankRepository = cardAdmiralsRankRepository;
    }

    public static UserCardAdmiralsRankService Create()
    {
        return new UserCardAdmiralsRankService(new UserCardAdmiralsRankRepository());
    }

    public async Task<Rank> GetCardAdmiralRankAsync(string type, string card_id)
    {
        return await _cardAdmiralsRankRepository.GetCardAdmiralRankAsync(type, card_id);
    }

    public async Task InsertOrUpdateCardAdmiralRankAsync(Rank rank, string type, string card_id)
    {
        await _cardAdmiralsRankRepository.InsertOrUpdateCardAdmiralRankAsync(rank, type, card_id);
    }

    public async Task<Rank> GetSumCardAdmiralsRankAsync(string user_id, string card_id)
    {
        return await _cardAdmiralsRankRepository.GetSumCardAdmiralsRankAsync(user_id, card_id);
    }
}
