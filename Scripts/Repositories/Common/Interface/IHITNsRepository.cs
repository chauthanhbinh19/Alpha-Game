using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHITNsRepository
{
    Task<HITNs> GetHITNsAsync(string type);
    Task InsertOrUpdateHITNsAsync(string user_id, HITNs HITNs, string id);
    Task<HITNs> GetSumHITNsAsync(string user_id);
}