using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHIRNsService
{ 
    Task<HIRNs> GetHIRNsAsync(string id);
    Task InsertOrUpdateHIRNsAsync(string user_id, HIRNs HIRNs, string id);
    Task<HIRNs> GetSumHIRNsAsync(string user_id);
    HIRNs EnhanceHIRNs(HIRNs research, int level, int multiplier = 1);
}