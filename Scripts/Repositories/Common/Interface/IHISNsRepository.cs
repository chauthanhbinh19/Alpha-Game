using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHISNsRepository
{
    Task<HISNs> GetHISNsAsync(string type);
    Task InsertOrUpdateHISNsAsync(string user_id, HISNs HISNs, string id);
    Task<HISNs> GetSumHISNsAsync(string user_id);
}