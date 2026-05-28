using System.Threading.Tasks;

public class UserCardColonelsRankService : IUserCardColonelsRankService
{
     private static UserCardColonelsRankService _instance;
    private readonly IUserCardColonelsRankRepository _userCardColonelsRankRepository;

    public UserCardColonelsRankService(IUserCardColonelsRankRepository userCardColonelsRankRepository)
    {
        _userCardColonelsRankRepository = userCardColonelsRankRepository;
    }

    public static UserCardColonelsRankService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardColonelsRankService(new UserCardColonelsRankRepository());
        }
        return _instance;
    }

    public async Task<Rank> GetCardColonelRankAsync(string id, string card_id)
    {
        return await _userCardColonelsRankRepository.GetCardColonelRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardColonelRankAsync(Rank rank, string card_id)
    {
        await _userCardColonelsRankRepository.InsertOrUpdateCardColonelRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumCardColonelsRankAsync(string user_id, string card_id)
    {
        return await _userCardColonelsRankRepository.GetSumCardColonelsRankAsync(user_id, card_id);
    }
}
