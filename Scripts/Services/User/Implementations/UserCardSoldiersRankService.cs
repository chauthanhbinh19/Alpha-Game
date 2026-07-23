using System.Threading.Tasks;

public class UserCardSoldiersRankService : IUserCardSoldiersRankService
{
    private static UserCardSoldiersRankService _instance;
    private readonly IUserCardSoldiersRankRepository _userCardSoldiersRankRepository;

    public UserCardSoldiersRankService(IUserCardSoldiersRankRepository userCardSoldiersRankRepository)
    {
        _userCardSoldiersRankRepository = userCardSoldiersRankRepository;
    }

    public static UserCardSoldiersRankService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardSoldiersRankService(new UserCardSoldiersRankRepository());
        }
        return _instance;
    }

    public async Task<Rank> GetUserCardSoldierRankAsync(string userId, string id, string cardId)
    {
        return await _userCardSoldiersRankRepository.GetUserCardSoldierRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardSoldierRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardSoldiersRankRepository.InsertOrUpdateUserCardSoldierRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumUserCardSoldiersRankAsync(string userId, string cardId)
    {
        return await _userCardSoldiersRankRepository.GetSumUserCardSoldiersRankAsync(userId, cardId);
    }
}
