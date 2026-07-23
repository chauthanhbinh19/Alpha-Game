using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserMastersRepository
{
    Task<UserMasters> GetUserMastersAsync(string userId, string type);
    Task InsertOrUpdateUserMastersAsync(string userId, UserMasters Masters, string id);
    Task<UserMasters> GetSumUserMastersAsync(string userId);
}