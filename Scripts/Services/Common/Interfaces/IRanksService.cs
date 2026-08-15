using System.Threading.Tasks;

public interface IRanksService
{
    Task<Ranks> GetRankByIdAsync(string id);
    Task<InsertOrUpdateResult<Ranks>> InsertRankAsync(Ranks rank);
    Task<InsertOrUpdateResult<Ranks>> UpdateRankAsync(Ranks rank);
}