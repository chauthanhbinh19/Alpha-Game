using System.Threading.Tasks;

public interface IUserCardHeroesRankService
{
    Task<Rank> GetCardHeroRankAsync(string id, string card_id);
    Task InsertOrUpdateCardHeroRankAsync(Rank rank, string card_id);
    Task<Rank> GetSumCardHeroesRankAsync(string user_id, string card_id);
}
