using System.Threading.Tasks;

public interface IUserBooksRankService
{
    Task<Rank> GetBookRankAsync(string type, string card_id);
    Task InsertOrUpdateBookRankAsync(Rank rank, string type, string card_id);
    Task<Rank> GetSumBooksRankAsync(string user_id, string card_id);
}
