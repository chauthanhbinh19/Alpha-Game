using System.Threading.Tasks;

public interface IUserCardMonstersRankRepository
{
    Task<UserRanks> GetUserCardMonsterRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardMonsterRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumUserCardMonstersRankAsync(string userId, string cardId);
}
