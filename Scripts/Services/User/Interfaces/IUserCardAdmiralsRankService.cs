using System.Threading.Tasks;

public interface IUserCardAdmiralsRankService
{
    Task<Rank> GetCardAdmiralRankAsync(string id, string card_id);
    Task InsertOrUpdateCardAdmiralRankAsync(Rank rank, string card_id);
    Task<Rank> GetSumCardAdmiralsRankAsync(string user_id, string card_id);
}
