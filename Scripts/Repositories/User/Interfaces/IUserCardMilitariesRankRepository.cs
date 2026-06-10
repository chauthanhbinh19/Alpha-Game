using System.Threading.Tasks;

public interface IUserCardMilitariesRankRepository
{
    Task<Rank> GetCardMilitaryRankAsync(string id, string card_id);
    Task InsertOrUpdateCardMilitaryRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumCardMilitariesRankAsync(string user_id, string card_id);
}
