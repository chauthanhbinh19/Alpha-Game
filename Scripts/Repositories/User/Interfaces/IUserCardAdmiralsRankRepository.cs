using System.Threading.Tasks;

public interface IUserCardAdmiralsRankRepository
{
    Task<Rank> GetCardAdmiralRankAsync(string id, string card_id);
    Task InsertOrUpdateCardAdmiralRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumCardAdmiralsRankAsync(string user_id, string card_id);
}
