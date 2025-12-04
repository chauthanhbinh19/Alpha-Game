using System.Threading.Tasks;

public interface IUserCardAdmiralsRankService
{
    Task<Rank> GetCardAdmiralRankAsync(string type, string card_id);
    Task InsertOrUpdateCardAdmiralRankAsync(Rank Rank, string type, string card_id);
    Task<Rank> GetSumCardAdmiralsRankAsync(string user_id, string card_id);
}
