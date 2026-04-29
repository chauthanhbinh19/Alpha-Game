using System.Threading.Tasks;

public interface IUserCardCaptainsRankService
{
    Task<Rank> GetCardCaptainRankAsync(string id, string card_id);
    Task InsertOrUpdateCardCaptainRankAsync(Rank rank, string card_id);
    Task<Rank> GetSumCardCaptainsRankAsync(string user_id, string card_id);
}
