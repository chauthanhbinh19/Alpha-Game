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

    public Rank GetBooksRank(string type, string card_id)
    {
        return _booksRankRepository.GetBooksRank(type, card_id);
    }

    public void InsertOrUpdateBooksRank(Rank rank, string type, string card_id)
    {
        _booksRankRepository.InsertOrUpdateBooksRank(rank, type, card_id);
    }

    public Rank GetSumBooksRank(string user_id, string card_id)
    {
        return _booksRankRepository.GetSumBooksRank(user_id, card_id);
    }
}
