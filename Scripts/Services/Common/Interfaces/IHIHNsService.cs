using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHIHNsService
{ 
    Task<HIHNs> GetHIHNsAsync(string id);
    Task InsertOrUpdateHIHNsAsync(string user_id, HIHNs HIHNs, string id);
    Task<HIHNs> GetSumHIHNsAsync(string user_id);
    HIHNs EnhanceHIHNs(HIHNs research, int level, int multiplier = 1);
}