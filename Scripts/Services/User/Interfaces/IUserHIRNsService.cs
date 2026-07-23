using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIRNsService
{ 
    Task<UserHIRNs> GetUserHIRNsAsync(string userId, string id);
    Task InsertOrUpdateUserHIRNsAsync(string userId, UserHIRNs HIRNs, string id);
    Task<UserHIRNs> GetSumUserHIRNsAsync(string userId);
}