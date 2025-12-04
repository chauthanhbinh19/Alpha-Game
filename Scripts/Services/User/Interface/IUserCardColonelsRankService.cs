using System.Threading.Tasks;

public interface IUserCardColonelsRankService
{
    Task<Rank> GetCardColonelRankAsync(string type, string card_id);
    Task InsertOrUpdateCardColonelRankAsync(Rank Rank, string type, string card_id);
    Task<Rank> GetSumCardColonelsRankAsync(string user_id, string card_id);
}
