using System.Threading.Tasks;

public interface IRanksRepository
{
    Task<Ranks> GetRankByIdAsync(string id);
}