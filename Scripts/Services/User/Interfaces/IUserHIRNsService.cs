using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIRNsService
{ 
    Task<UserHIRNs> GetUserHIRNsAsync(string id);
    Task InsertOrUpdateUserHIRNsAsync(string user_id, UserHIRNs HIRNs, string id);
    Task<UserHIRNs> GetSumUserHIRNsAsync(string user_id);
}