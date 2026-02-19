using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHIHNsRepository
{
    Task<HIHNs> GetHIHNsAsync(string type);
    Task InsertOrUpdateHIHNsAsync(string user_id, HIHNs HIHNs, string id);
    Task<HIHNs> GetSumHIHNsAsync(string user_id);
}