using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserSSWNsRepository
{
    Task<UserSSWNs> GetUserSSWNsAsync(string userId, string id);
    Task InsertOrUpdateUserSSWNsAsync(string userId, UserSSWNs SSWNs, string id);
    Task<UserSSWNs> GetSumUserSSWNsAsync(string userId);
}