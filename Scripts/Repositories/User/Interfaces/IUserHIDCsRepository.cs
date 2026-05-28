using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIDCsRepository
{
    Task<UserHIDCs> GetUserHIDCsAsync(string type);
    Task InsertOrUpdateUserHIDCsAsync(string user_id, UserHIDCs HIDCs, string id);
    Task<UserHIDCs> GetSumUserHIDCsAsync(string user_id);
}