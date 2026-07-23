using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIHNsRepository
{
    Task<UserHIHNs> GetUserHIHNsAsync(string userId, string id);
    Task InsertOrUpdateUserHIHNsAsync(string userId, UserHIHNs HIHNs, string id);
    Task<UserHIHNs> GetSumUserHIHNsAsync(string userId);
}