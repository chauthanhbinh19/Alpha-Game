using System.Threading.Tasks;

public interface IUserCardGeneralsRankService
{
    Task<Rank> GetCardGeneralRankAsync(string type, string card_id);
    Task InsertOrUpdateCardGeneralRankAsync(Rank Rank, string type, string card_id);
    Task<Rank> GetSumCardGeneralsRankAsync(string user_id, string card_id);
}
