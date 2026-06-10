using System.Threading.Tasks;

public interface IRanksService
{
    Task<Ranks> GetRankByIdAsync(string id);
}