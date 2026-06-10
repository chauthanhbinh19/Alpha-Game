using System.Threading.Tasks;

public interface IUserCardColonelsRankService
{
    Task<Rank> GetCardColonelRankAsync(string id, string card_id);
    Task InsertOrUpdateCardColonelRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumCardColonelsRankAsync(string user_id, string card_id);
}
