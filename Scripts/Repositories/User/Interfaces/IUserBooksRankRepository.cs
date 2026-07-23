using System.Threading.Tasks;

public interface IUserBooksRankRepository
{
    Task<Rank> GetUserBookRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserBookRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumUserBooksRankAsync(string userId, string cardId);
}
