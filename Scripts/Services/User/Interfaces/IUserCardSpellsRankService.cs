using System.Threading.Tasks;

public interface IUserCardSpellsRankService
{
    Task<UserRanks> GetUserCardSpellRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardSpellRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumUserCardSpellsRankAsync(string userId, string cardId);
}
