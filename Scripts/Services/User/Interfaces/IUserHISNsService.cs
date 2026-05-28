using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHISNsService
{ 
    Task<UserHISNs> GetUserHISNsAsync(string id);
    Task InsertOrUpdateUserHISNsAsync(string user_id, UserHISNs HISNs, string id);
    Task<UserHISNs> GetSumUserHISNsAsync(string user_id);
}