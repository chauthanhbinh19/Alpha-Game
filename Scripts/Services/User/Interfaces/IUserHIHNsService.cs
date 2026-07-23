using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIHNsService
{ 
    Task<UserHIHNs> GetUserHIHNsAsync(string userId, string id);
    Task InsertOrUpdateUserHIHNsAsync(string userId, UserHIHNs HIHNs, string id);
    Task<UserHIHNs> GetSumUserHIHNsAsync(string userId);
}