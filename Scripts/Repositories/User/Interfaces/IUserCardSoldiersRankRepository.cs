using System.Threading.Tasks;

public interface IUserCardSoldiersRankRepository
{
    Task<Rank> GetCardSoldierRankAsync(string id, string card_id);
    Task InsertOrUpdateCardSoldierRankAsync(Rank Rank, string card_id);
    Task<Rank> GetSumCardSoldiersRankAsync(string user_id, string card_id);
}
