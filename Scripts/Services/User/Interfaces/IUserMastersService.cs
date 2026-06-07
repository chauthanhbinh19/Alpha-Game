using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserMastersService
{ 
    Task<UserMasters> GetUserMastersAsync(string id);
    Task InsertOrUpdateUserMastersAsync(string user_id, UserMasters Masters, string id);
    Task<UserMasters> GetSumUserMastersAsync(string user_id);
}