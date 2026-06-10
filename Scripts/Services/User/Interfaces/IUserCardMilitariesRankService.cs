using System.Threading.Tasks;

public interface IUserCardMilitariesRankService
{
    Task<Rank> GetCardMilitaryRankAsync(string id, string card_id);
    Task InsertOrUpdateCardMilitaryRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumCardMilitariesRankAsync(string user_id, string card_id);
}
