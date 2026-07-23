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

    public async Task<Rank> GetUserCardAdmiralRankAsync(string userId, string id, string card_id)
    {
        return await _userCardAdmiralsRankRepository.GetUserCardAdmiralRankAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserCardAdmiralRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardAdmiralsRankRepository.InsertOrUpdateUserCardAdmiralRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumUserCardAdmiralsRankAsync(string userId, string card_id)
    {
        return await _userCardAdmiralsRankRepository.GetSumUserCardAdmiralsRankAsync(userId, card_id);
    }
}
