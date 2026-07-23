using System.Threading.Tasks;

public interface IUserCardCaptainsRankService
{
    Task<Rank> GetUserCardCaptainRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardCaptainRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumUserCardCaptainsRankAsync(string userId, string cardId);
}
