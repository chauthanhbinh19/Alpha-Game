using System.Threading.Tasks;

public interface IUserCardAdmiralsRankService
{
    Task<UserRanks> GetUserCardAdmiralRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardAdmiralRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumUserCardAdmiralsRankAsync(string userId, string cardId);
}
