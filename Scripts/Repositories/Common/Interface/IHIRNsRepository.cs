using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHIRNsRepository
{
    Task<HIRNs> GetHIRNsAsync(string type);
    Task InsertOrUpdateHIRNsAsync(string user_id, HIRNs HIRNs, string id);
    Task<HIRNs> GetSumHIRNsAsync(string user_id);
}