using System.Threading.Tasks;

public interface IUserCardHeroesRankRepository
{
    Task<Rank> GetCardHeroRankAsync(string id, string card_id);
    Task InsertOrUpdateCardHeroRankAsync(Rank Rank, string card_id);
    Task<Rank> GetSumCardHeroesRankAsync(string user_id, string card_id);
}
