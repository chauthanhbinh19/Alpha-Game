using System.Threading.Tasks;

public interface IUserCardGeneralsRankService
{
    Task<Rank> GetCardGeneralRankAsync(string id, string card_id);
    Task InsertOrUpdateCardGeneralRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumCardGeneralsRankAsync(string user_id, string card_id);
}
