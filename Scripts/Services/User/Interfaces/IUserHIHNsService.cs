using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIHNsService
{ 
    Task<UserHIHNs> GetUserHIHNsAsync(string id);
    Task InsertOrUpdateUserHIHNsAsync(string user_id, UserHIHNs HIHNs, string id);
    Task<UserHIHNs> GetSumUserHIHNsAsync(string user_id);
}