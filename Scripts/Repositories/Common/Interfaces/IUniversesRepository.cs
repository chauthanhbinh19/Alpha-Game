using System.Threading.Tasks;

public interface IUniversesRepository
{
    Task<Universes> GetUniverseByIdAsync(string id);
}