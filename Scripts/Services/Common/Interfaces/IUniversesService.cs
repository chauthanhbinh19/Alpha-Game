using System.Threading.Tasks;

public interface IUniversesService
{
    Task<Universes> GetUniverseByIdAsync(string id);
    Task<InsertOrUpdateResult<Universes>> InsertUniverseAsync(Universes universe);
    Task<InsertOrUpdateResult<Universes>> UpdateUniverseAsync(Universes universe);
}