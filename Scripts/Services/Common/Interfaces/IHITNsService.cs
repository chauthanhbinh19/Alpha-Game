using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHITNsService
{ 
    Task<HITNs> GetHITNsAsync(string id);
    Task InsertOrUpdateHITNsAsync(string user_id, HITNs HITNs, string id);
    Task<HITNs> GetSumHITNsAsync(string user_id);
}