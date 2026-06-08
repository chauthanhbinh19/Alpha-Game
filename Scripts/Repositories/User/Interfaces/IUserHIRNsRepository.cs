using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIRNsRepository
{
    Task<UserHIRNs> GetUserHIRNsAsync(string type);
    Task InsertOrUpdateUserHIRNsAsync(string userId, UserHIRNs HIRNs, string id);
    Task<UserHIRNs> GetSumUserHIRNsAsync(string userId);
}