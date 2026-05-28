using System.Threading.Tasks;

public interface IUserCardGeneralsRankRepository
{
    Task<Rank> GetCardGeneralRankAsync(string id, string card_id);
    Task InsertOrUpdateCardGeneralRankAsync(Rank Rank, string card_id);
    Task<Rank> GetSumCardGeneralsRankAsync(string user_id, string card_id);
}
