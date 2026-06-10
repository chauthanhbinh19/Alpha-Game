using System.Threading.Tasks;

public interface IUserCardMonstersRankRepository
{
    Task<Rank> GetCardMonsterRankAsync(string id, string card_id);
    Task InsertOrUpdateCardMonsterRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumCardMonstersRankAsync(string user_id, string card_id);

}
