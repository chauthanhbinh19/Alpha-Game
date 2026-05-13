using System.Collections.Generic;
using System.Threading.Tasks;
public interface IHIDCsService
{ 
    Task<HIDCs> GetHIDCsAsync(string id);
    Task InsertOrUpdateHIDCsAsync(string user_id, HIDCs HIDCs, string id);
    Task<HIDCs> GetSumHIDCsAsync(string user_id);
}