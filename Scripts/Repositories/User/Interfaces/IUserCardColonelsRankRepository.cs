using System.Threading.Tasks;

public interface IUserCardColonelsRankRepository
{
    Task<Rank> GetUserCardColonelRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardColonelRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumUserCardColonelsRankAsync(string userId, string cardId);
}
