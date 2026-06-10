using System.Threading.Tasks;

public interface IUserCardCaptainsRankRepository
{
    Task<Rank> GetCardCaptainRankAsync(string id, string card_id);
    Task InsertOrUpdateCardCaptainRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumCardCaptainsRankAsync(string user_id, string card_id);
}
