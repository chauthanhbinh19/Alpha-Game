using System.Threading.Tasks;

public interface IUserCardAdmiralsRankRepository
{
    Task<UserRanks> GetUserCardAdmiralRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardAdmiralRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumUserCardAdmiralsRankAsync(string userId, string cardId);
}
