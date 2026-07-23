using System.Threading.Tasks;

public interface IUserCardMilitariesRankService
{
    Task<Rank> GetUserCardMilitaryRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardMilitaryRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumUserCardMilitariesRankAsync(string userId, string cardId);
}
