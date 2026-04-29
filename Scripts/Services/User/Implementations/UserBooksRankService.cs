using System.Threading.Tasks;

public class UserBooksRankService : IUserBooksRankService
{
     private static UserBooksRankService _instance;
    private readonly IUserBooksRankRepository _userBooksRankRepository;

    public UserBooksRankService(IUserBooksRankRepository userBooksRankRepository)
    {
        _userBooksRankRepository = userBooksRankRepository;
    }

    public static UserBooksRankService Create()
    {
        if (_instance == null)
        {
            _instance = new UserBooksRankService(new UserBooksRankRepository());
        }
        return _instance;
    }

    public async Task<Rank> GetBookRankAsync(string id, string card_id)
    {
        return await _userBooksRankRepository.GetBookRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateBookRankAsync(Rank rank, string card_id)
    {
        await _userBooksRankRepository.InsertOrUpdateBookRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumBooksRankAsync(string user_id, string card_id)
    {
        return await _userBooksRankRepository.GetSumBooksRankAsync(user_id, card_id);
    }
}
