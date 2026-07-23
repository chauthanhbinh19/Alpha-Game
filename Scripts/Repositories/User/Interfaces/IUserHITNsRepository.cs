using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHITNsRepository
{
    Task<UserHITNs> GetUserHITNsAsync(string userId, string id);
    Task InsertOrUpdateUserHITNsAsync(string userId, UserHITNs HITNs, string id);
    Task<UserHITNs> GetSumUserHITNsAsync(string userId);
}