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

    public async Task<Rank> GetUserBookRankAsync(string userId, string id, string card_id)
    {
        return await _userBooksRankRepository.GetUserBookRankAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserBookRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userBooksRankRepository.InsertOrUpdateUserBookRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumUserBooksRankAsync(string userId, string card_id)
    {
        return await _userBooksRankRepository.GetSumUserBooksRankAsync(userId, card_id);
    }
}
