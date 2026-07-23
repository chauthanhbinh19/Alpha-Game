using System.Threading.Tasks;

public interface IUserCardMonstersRankRepository
{
    Task<Rank> GetUserCardMonsterRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardMonsterRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumUserCardMonstersRankAsync(string userId, string cardId);

}
