using System.Threading.Tasks;

public class UserCardCaptainsRankService : IUserCardCaptainsRankService
{
    private static UserCardCaptainsRankService _instance;
    private readonly IUserCardCaptainsRankRepository _userCardCaptainsRankRepository;

    public UserCardCaptainsRankService(IUserCardCaptainsRankRepository userCardCaptainsRankRepository)
    {
        _userCardCaptainsRankRepository = userCardCaptainsRankRepository;
    }

    public static UserCardCaptainsRankService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardCaptainsRankService(new UserCardCaptainsRankRepository());
        }
        return _instance;
    }

    public async Task<Rank> GetUserCardCaptainRankAsync(string userId, string id, string card_id)
    {
        return await _userCardCaptainsRankRepository.GetUserCardCaptainRankAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserCardCaptainRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardCaptainsRankRepository.InsertOrUpdateUserCardCaptainRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumUserCardCaptainsRankAsync(string userId, string card_id)
    {
        return await _userCardCaptainsRankRepository.GetSumUserCardCaptainsRankAsync(userId, card_id);
    }
}
