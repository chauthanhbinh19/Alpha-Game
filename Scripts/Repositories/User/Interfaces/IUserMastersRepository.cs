using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserMastersRepository
{
    Task<UserMasters> GetUserMastersAsync(string userId, string masterId, string objectId, string userTable, string objectColumn);
    Task InsertOrUpdateUserMastersAsync(string userId, UserMasters Masters, string objectId, string userTable, string objectColumn);
    Task<UserMasters> GetSumUserMastersAsync(string userId, string objectId, string userTable, string objectColumn);
}