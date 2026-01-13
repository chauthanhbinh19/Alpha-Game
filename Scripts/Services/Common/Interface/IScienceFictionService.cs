using System.Collections.Generic;
using System.Threading.Tasks;
public interface IScienceFictionService
{ 
    Task<ScienceFiction> GetScienceFictionAsync(string id);
    Task InsertOrUpdateScienceFictionAsync(string user_id, ScienceFiction scienceFiction, string id);
    Task<ScienceFiction> GetSumScienceFictionAsync(string user_id);
}