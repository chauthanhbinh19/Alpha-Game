using System.Threading.Tasks;

public class UserCardGeneralsRankService : IUserCardGeneralsRankService
{
    private static UserCardGeneralsRankService _instance;
    private readonly IUserCardGeneralsRankRepository _userCardGeneralsRankRepository;

    public UserCardGeneralsRankService(IUserCardGeneralsRankRepository userCardGeneralsRankRepository)
    {
        _userCardGeneralsRankRepository = userCardGeneralsRankRepository;
    }

    public static UserCardGeneralsRankService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardGeneralsRankService(new UserCardGeneralsRankRepository());
        }
        return _instance;
    }

    public async Task<Rank> GetUserCardGeneralRankAsync(string userId, string id, string card_id)
    {
        return await _userCardGeneralsRankRepository.GetUserCardGeneralRankAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserCardGeneralRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardGeneralsRankRepository.InsertOrUpdateUserCardGeneralRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumUserCardGeneralsRankAsync(string userId, string card_id)
    {
        return await _userCardGeneralsRankRepository.GetSumUserCardGeneralsRankAsync(userId, card_id);
    }
}
