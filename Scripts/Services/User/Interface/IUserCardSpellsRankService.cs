using System.Threading.Tasks;

public interface IUserCardSpellsRankService
{
    Task<Rank> GetCardSpellRankAsync(string type, string card_id);
    Task InsertOrUpdateCardSpellRankAsync(Rank Rank, string type, string card_id);
    Task<Rank> GetSumCardSpellsRankAsync(string user_id, string card_id);
}
