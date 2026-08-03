using System.Threading.Tasks;

public interface IUserCardGeneralsRankService
{
    Task<UserRanks> GetUserCardGeneralRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardGeneralRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumUserCardGeneralsRankAsync(string userId, string cardId);
}
