using System.Threading.Tasks;

public class UserBooksRankService : IUserBooksRankService
{
    private readonly IUserBooksRankRepository _booksRankRepository;

    public UserBooksRankService(IUserBooksRankRepository booksRankRepository)
    {
        _booksRankRepository = booksRankRepository;
    }

    public static UserBooksRankService Create()
    {
        return new UserBooksRankService(new UserBooksRankRepository());
    }

    public async Task<Rank> GetBookRankAsync(string id, string card_id)
    {
        return await _booksRankRepository.GetBookRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateBookRankAsync(Rank rank, string card_id)
    {
        await _booksRankRepository.InsertOrUpdateBookRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumBooksRankAsync(string user_id, string card_id)
    {
        return await _booksRankRepository.GetSumBooksRankAsync(user_id, card_id);
    }
}
