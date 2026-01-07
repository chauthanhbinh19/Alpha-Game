using System.Threading.Tasks;

public interface IUserCardAdmiralsRankRepository
{
    Task<Rank> GetCardAdmiralRankAsync(string id, string card_id);
    Task InsertOrUpdateCardAdmiralRankAsync(Rank Rank, string card_id);
    Task<Rank> GetSumCardAdmiralsRankAsync(string user_id, string card_id);
}
