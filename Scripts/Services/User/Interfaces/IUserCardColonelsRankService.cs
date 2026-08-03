using System.Threading.Tasks;

public interface IUserCardColonelsRankService
{
    Task<UserRanks> GetUserCardColonelRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardColonelRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumUserCardColonelsRankAsync(string userId, string cardId);
}
