using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIHNsRepository
{
    Task<UserHIHNs> GetUserHIHNsAsync(string type);
    Task InsertOrUpdateUserHIHNsAsync(string user_id, UserHIHNs HIHNs, string id);
    Task<UserHIHNs> GetSumUserHIHNsAsync(string user_id);
}