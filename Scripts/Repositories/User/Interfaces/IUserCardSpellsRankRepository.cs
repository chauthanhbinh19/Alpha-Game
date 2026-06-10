using System.Threading.Tasks;

public interface IUserCardSpellsRankRepository
{
    Task<Rank> GetCardSpellRankAsync(string id, string card_id);
    Task InsertOrUpdateCardSpellRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumCardSpellsRankAsync(string user_id, string card_id);
}
