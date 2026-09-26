using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHISNsRepository
{
    Task<UserHISNs> GetUserHISNsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserHISNs>> InsertOrUpdateUserHISNsAsync(string userId, UserHISNs HISNs, string id);
    Task<UserHISNs> GetSumUserHISNsAsync(string userId);
}