using System.Collections.Generic;
using System.Threading.Tasks;
public interface IResearchsRepository
{
    Task<Researchs> GetResearchsAsync(string type);
    Task InsertOrUpdateResearchsAsync(string user_id, Researchs Researchs, string id);
    Task<Researchs> GetSumResearchsAsync(string user_id);
}