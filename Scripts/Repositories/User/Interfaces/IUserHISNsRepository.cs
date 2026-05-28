using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHISNsRepository
{
    Task<UserHISNs> GetUserHISNsAsync(string type);
    Task InsertOrUpdateUserHISNsAsync(string user_id, UserHISNs HISNs, string id);
    Task<UserHISNs> GetSumUserHISNsAsync(string user_id);
}