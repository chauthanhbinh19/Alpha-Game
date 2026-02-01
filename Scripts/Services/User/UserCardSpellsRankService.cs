using System.Threading.Tasks;

public class UserCardSpellsRankService : IUserCardSpellsRankService
{
     private static UserCardSpellsRankService _instance;
    private readonly IUserCardSpellsRankRepository _userCardSpellsRankRepository;

    public UserCardSpellsRankService(IUserCardSpellsRankRepository userCardSpellsRankRepository)
    {
        _userCardSpellsRankRepository = userCardSpellsRankRepository;
    }

    public static UserCardSpellsRankService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardSpellsRankService(new UserCardSpellsRankRepository());
        }
        return _instance;
    }

    public async Task<Rank> GetCardSpellRankAsync(string id, string card_id)
    {
        return await _userCardSpellsRankRepository.GetCardSpellRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardSpellRankAsync(Rank rank, string card_id)
    {
        await _userCardSpellsRankRepository.InsertOrUpdateCardSpellRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumCardSpellsRankAsync(string user_id, string card_id)
    {
        return await _userCardSpellsRankRepository.GetSumCardSpellsRankAsync(user_id, card_id);
    }
}
