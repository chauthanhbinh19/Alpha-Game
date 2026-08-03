using System.Threading.Tasks;

public interface IUserCardHeroesRankService
{
    Task<UserRanks> GetUserCardHeroRankAsync(string userId, string id, string cardId);
    Task InsertOrUpdateUserCardHeroRankAsync(string userId, UserRanks userRank, string cardId);
    Task<UserRanks> GetSumUserCardHeroesRankAsync(string userId, string cardId);
}
