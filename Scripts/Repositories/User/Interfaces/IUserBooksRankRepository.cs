using System.Threading.Tasks;

public interface IUserBooksRankRepository
{
    Task<Rank> GetBookRankAsync(string id, string card_id);
    Task InsertOrUpdateBookRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumBooksRankAsync(string user_id, string card_id);
}
