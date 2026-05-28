using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHITNsRepository
{
    Task<UserHITNs> GetUserHITNsAsync(string type);
    Task InsertOrUpdateUserHITNsAsync(string user_id, UserHITNs HITNs, string id);
    Task<UserHITNs> GetSumUserHITNsAsync(string user_id);
}