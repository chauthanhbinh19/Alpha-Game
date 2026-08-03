using System.Threading.Tasks;

public interface IUserBooksRankService
{
    Task<UserRanks> GetUserBookRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserBookRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumUserBooksRankAsync(string userId, string cardId);
}
