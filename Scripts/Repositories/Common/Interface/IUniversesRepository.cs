using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUniversesRepository
{
    Task<Universes> GetUniversesAsync(string type);
    Task InsertOrUpdateUniversesAsync(string user_id, Universes Universes, string id);
    Task<Universes> GetSumUniversesAsync(string user_id);
}