using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHISNsService
{ 
    Task<HISNs> GetHISNsAsync(string id);
    Task InsertOrUpdateHISNsAsync(string user_id, HISNs HISNs, string id);
    Task<HISNs> GetSumHISNsAsync(string user_id);
}