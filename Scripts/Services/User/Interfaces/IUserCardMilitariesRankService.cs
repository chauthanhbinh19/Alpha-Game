using System.Threading.Tasks;

public interface IUserCardMilitariesRankService
{
    Task<UserRanks> GetUserCardMilitaryRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardMilitaryRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumUserCardMilitariesRankAsync(string userId, string cardId);
}
