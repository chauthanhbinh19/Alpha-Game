using System.Threading.Tasks;

public interface IUserCardCaptainsRankService
{
    Task<UserRanks> GetUserCardCaptainRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardCaptainRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumUserCardCaptainsRankAsync(string userId, string cardId);
}
