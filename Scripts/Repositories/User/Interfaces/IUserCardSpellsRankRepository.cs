using System.Threading.Tasks;

public interface IUserCardSpellsRankRepository
{
    Task<Rank> GetUserCardSpellRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardSpellRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumUserCardSpellsRankAsync(string userId, string cardId);
}
