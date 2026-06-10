using System.Threading.Tasks;

public class UserCardAdmiralsRankService : IUserCardAdmiralsRankService
{
     private static UserCardAdmiralsRankService _instance;
    private readonly IUserCardAdmiralsRankRepository _userCardAdmiralsRankRepository;

    public UserCardAdmiralsRankService(IUserCardAdmiralsRankRepository userCardAdmiralsRankRepository)
    {
        _userCardAdmiralsRankRepository = userCardAdmiralsRankRepository;
    }

    public static UserCardAdmiralsRankService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardAdmiralsRankService(new UserCardAdmiralsRankRepository());
        }
        return _instance;
    }

    public async Task<Rank> GetCardAdmiralRankAsync(string id, string card_id)
    {
        return await _userCardAdmiralsRankRepository.GetCardAdmiralRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardAdmiralRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardAdmiralsRankRepository.InsertOrUpdateCardAdmiralRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumCardAdmiralsRankAsync(string user_id, string card_id)
    {
        return await _userCardAdmiralsRankRepository.GetSumCardAdmiralsRankAsync(user_id, card_id);
    }
}
