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

    public async Task<Rank> GetCardGeneralRankAsync(string id, string card_id)
    {
        return await _userCardGeneralsRankRepository.GetCardGeneralRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardGeneralRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardGeneralsRankRepository.InsertOrUpdateCardGeneralRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumCardGeneralsRankAsync(string user_id, string card_id)
    {
        return await _userCardGeneralsRankRepository.GetSumCardGeneralsRankAsync(user_id, card_id);
    }
}
