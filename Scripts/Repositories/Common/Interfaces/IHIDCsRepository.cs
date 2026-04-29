using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHIDCsRepository
{
    Task<HIDCs> GetHIDCsAsync(string type);
    Task InsertOrUpdateHIDCsAsync(string user_id, HIDCs HIDCs, string id);
    Task<HIDCs> GetSumHIDCsAsync(string user_id);
}