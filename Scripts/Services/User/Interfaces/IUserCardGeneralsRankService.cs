using System.Threading.Tasks;

public interface IUserCardGeneralsRankService
{
    Task<Rank> GetUserCardGeneralRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardGeneralRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumUserCardGeneralsRankAsync(string userId, string cardId);
}
