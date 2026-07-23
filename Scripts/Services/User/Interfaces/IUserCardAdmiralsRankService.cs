using System.Threading.Tasks;

public interface IUserCardAdmiralsRankService
{
    Task<Rank> GetUserCardAdmiralRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardAdmiralRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumUserCardAdmiralsRankAsync(string userId, string cardId);
}
