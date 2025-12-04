using System.Threading.Tasks;

public interface IUserCardCaptainsRankRepository
{
    Task<Rank> GetCardCaptainRankAsync(string type, string card_id);
    Task InsertOrUpdateCardCaptainRankAsync(Rank Rank, string type, string card_id);
    Task<Rank> GetSumCardCaptainsRankAsync(string user_id, string card_id);
}
