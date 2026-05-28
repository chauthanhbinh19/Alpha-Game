using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserSSWNsService
{ 
    Task<UserSSWNs> GetUserSSWNsAsync(string id);
    Task InsertOrUpdateUserSSWNsAsync(string user_id, UserSSWNs SSWNs, string id);
    Task<UserSSWNs> GetSumUserSSWNsAsync(string user_id);
}