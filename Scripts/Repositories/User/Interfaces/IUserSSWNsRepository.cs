using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserSSWNsRepository
{
    Task<UserSSWNs> GetUserSSWNsAsync(string type);
    Task InsertOrUpdateUserSSWNsAsync(string user_id, UserSSWNs SSWNs, string id);
    Task<UserSSWNs> GetSumUserSSWNsAsync(string user_id);
}