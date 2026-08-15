using System.Threading.Tasks;

public interface IRanksRepository
{
    Task<Ranks> GetRankByIdAsync(string id);
    Task<InsertOrUpdateResult<Ranks>> InsertRankAsync(Ranks rank);
    Task<InsertOrUpdateResult<Ranks>> UpdateRankAsync(Ranks rank);
}