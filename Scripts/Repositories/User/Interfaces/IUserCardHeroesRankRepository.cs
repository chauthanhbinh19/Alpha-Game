using System.Threading.Tasks;

public interface IUserCardHeroesRankRepository
{
    Task<Rank> GetCardHeroRankAsync(string id, string card_id);
    Task InsertOrUpdateCardHeroRankAsync(string userId, UserRanks userRank, string cardId);
    Task<Rank> GetSumCardHeroesRankAsync(string user_id, string card_id);
}
