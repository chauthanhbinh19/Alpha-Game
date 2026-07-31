using System.Threading.Tasks;

public class UserBooksRankService : IUserBooksRankService
{
    private readonly IUserBooksRankRepository _userBooksRankRepository;

    public UserBooksRankService(IUserBooksRankRepository userBooksRankRepository)
    {
        _userBooksRankRepository = userBooksRankRepository;
    }

    public static IUserBooksRankService Create() => ServiceContainer.GetService<IUserBooksRankService>();

    public async Task<Rank> GetUserBookRankAsync(string userId, string id, string cardId)
    {
        return await _userBooksRankRepository.GetUserBookRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserBookRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userBooksRankRepository.InsertOrUpdateUserBookRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumUserBooksRankAsync(string userId, string cardId)
    {
        return await _userBooksRankRepository.GetSumUserBooksRankAsync(userId, cardId);
    }
}
