using System.Threading.Tasks;

public interface IUserCardGeneralsRankService
{
    Task<Rank> GetCardGeneralRankAsync(string id, string card_id);
    Task InsertOrUpdateCardGeneralRankAsync(Rank rank, string card_id);
    Task<Rank> GetSumCardGeneralsRankAsync(string user_id, string card_id);
}
