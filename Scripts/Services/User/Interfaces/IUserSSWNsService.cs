using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserSSWNsService
{ 
    Task<UserSSWNs> GetUserSSWNsAsync(string userId, string id);
    Task InsertOrUpdateUserSSWNsAsync(string userId, UserSSWNs SSWNs, string id);
    Task<UserSSWNs> GetSumUserSSWNsAsync(string userId);
}