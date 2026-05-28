using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHITNsService
{ 
    Task<UserHITNs> GetUserHITNsAsync(string id);
    Task InsertOrUpdateUserHITNsAsync(string user_id, UserHITNs HITNs, string id);
    Task<UserHITNs> GetSumUserHITNsAsync(string user_id);
}