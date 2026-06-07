using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserMastersRepository
{
    Task<UserMasters> GetUserMastersAsync(string type);
    Task InsertOrUpdateUserMastersAsync(string user_id, UserMasters Masters, string id);
    Task<UserMasters> GetSumUserMastersAsync(string user_id);
}