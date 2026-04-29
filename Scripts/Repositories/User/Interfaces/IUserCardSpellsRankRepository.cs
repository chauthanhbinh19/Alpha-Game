using System.Threading.Tasks;

public interface IUserCardSpellsRankRepository
{
    Task<Rank> GetCardSpellRankAsync(string id, string card_id);
    Task InsertOrUpdateCardSpellRankAsync(Rank Rank, string card_id);
    Task<Rank> GetSumCardSpellsRankAsync(string user_id, string card_id);
}
