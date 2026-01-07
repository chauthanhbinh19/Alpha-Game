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

    public async Task<Rank> GetCardAdmiralRankAsync(string id, string card_id)
    {
        return await _cardAdmiralsRankRepository.GetCardAdmiralRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardAdmiralRankAsync(Rank rank, string card_id)
    {
        await _cardAdmiralsRankRepository.InsertOrUpdateCardAdmiralRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumCardAdmiralsRankAsync(string user_id, string card_id)
    {
        return await _cardAdmiralsRankRepository.GetSumCardAdmiralsRankAsync(user_id, card_id);
    }
}
