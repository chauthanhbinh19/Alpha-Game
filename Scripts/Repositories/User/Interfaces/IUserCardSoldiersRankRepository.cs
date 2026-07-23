using System.Threading.Tasks;

public interface IUserCardSoldiersRankRepository
{
    Task<Rank> GetUserCardSoldierRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardSoldierRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumUserCardSoldiersRankAsync(string userId, string cardId);
}
