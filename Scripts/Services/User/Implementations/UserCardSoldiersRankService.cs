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

    public async Task<Rank> GetCardSoldierRankAsync(string id, string card_id)
    {
        return await _userCardSoldiersRankRepository.GetCardSoldierRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardSoldierRankAsync(Rank rank, string card_id)
    {
        await _userCardSoldiersRankRepository.InsertOrUpdateCardSoldierRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumCardSoldiersRankAsync(string user_id, string card_id)
    {
        return await _userCardSoldiersRankRepository.GetSumCardSoldiersRankAsync(user_id, card_id);
    }
}
