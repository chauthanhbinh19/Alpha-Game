using System.Collections.Generic;
using System.Threading.Tasks;
public interface IResearchsService
{ 
    Task<Researchs> GetResearchsAsync(string id);
    Task InsertOrUpdateResearchsAsync(string user_id, Researchs Researchs, string id);
    Task<Researchs> GetSumResearchsAsync(string user_id);
    Researchs EnhanceResearchs(Researchs research, int level, int multiplier = 1);
}