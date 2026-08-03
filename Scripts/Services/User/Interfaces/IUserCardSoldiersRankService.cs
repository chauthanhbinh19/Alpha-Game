using System.Threading.Tasks;

public interface IUserCardSoldiersRankService
{
    Task<UserRanks> GetUserCardSoldierRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardSoldierRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumUserCardSoldiersRankAsync(string userId, string cardId);
}
