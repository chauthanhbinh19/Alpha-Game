using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIDCsService
{ 
    Task<UserHIDCs> GetUserHIDCsAsync(string id);
    Task InsertOrUpdateUserHIDCsAsync(string user_id, UserHIDCs HIDCs, string id);
    Task<UserHIDCs> GetSumUserHIDCsAsync(string user_id);
}