using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIDCsRepository
{
    Task<UserHIDCs> GetUserHIDCsAsync(string userId, string id);
    Task InsertOrUpdateUserHIDCsAsync(string userId, UserHIDCs HIDCs, string id);
    Task<UserHIDCs> GetSumUserHIDCsAsync(string userId);
}