using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUniversesService
{ 
    Task<Universes> GetUniversesAsync(string id);
    Task InsertOrUpdateUniversesAsync(string user_id, Universes Universes, string id);
    Task<Universes> GetSumUniversesAsync(string user_id);
}