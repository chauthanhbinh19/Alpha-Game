using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHITNsService
{ 
    Task<UserHITNs> GetUserHITNsAsync(string userId, string id);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserHITNsAsync(string userId, UserHITNs HITNs, string id);
    Task<UserHITNs> GetSumUserHITNsAsync(string userId);
}