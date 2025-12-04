using System.Collections.Generic;
using System.Threading.Tasks;
public interface IScienceFictionRepository
{
    Task<ScienceFiction> GetScienceFictionAsync(string type);
    Task InsertOrUpdateScienceFictionAsync(string user_id, ScienceFiction scienceFiction, string type);
    Task<ScienceFiction> GetSumScienceFictionAsync(string user_id);
}