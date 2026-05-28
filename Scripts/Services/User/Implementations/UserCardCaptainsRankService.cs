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

    public async Task<Rank> GetCardCaptainRankAsync(string id, string card_id)
    {
        return await _userCardCaptainsRankRepository.GetCardCaptainRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardCaptainRankAsync(Rank rank, string card_id)
    {
        await _userCardCaptainsRankRepository.InsertOrUpdateCardCaptainRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumCardCaptainsRankAsync(string user_id, string card_id)
    {
        return await _userCardCaptainsRankRepository.GetSumCardCaptainsRankAsync(user_id, card_id);
    }
}
