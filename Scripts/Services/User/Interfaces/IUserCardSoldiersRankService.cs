using System.Threading.Tasks;

public interface IUserCardSoldiersRankService
{
    Task<Rank> GetCardSoldierRankAsync(string id, string card_id);
    Task InsertOrUpdateCardSoldierRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumCardSoldiersRankAsync(string user_id, string card_id);
}
