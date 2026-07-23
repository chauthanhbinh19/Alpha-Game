using System.Threading.Tasks;

public interface IUserCardHeroesRankService
{
    Task<Rank> GetUserCardHeroRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardHeroRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumUserCardHeroesRankAsync(string userId, string cardId);
}
