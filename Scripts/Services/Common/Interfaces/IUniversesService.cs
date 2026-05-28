using System.Threading.Tasks;

public interface IUniversesService
{
    Task<Universes> GetUniverseByIdAsync(string id);
}