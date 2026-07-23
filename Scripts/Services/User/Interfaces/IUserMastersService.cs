using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserMastersService
{ 
    Task<UserMasters> GetUserMastersAsync(string userId, string id);
    Task InsertOrUpdateUserMastersAsync(string userId, UserMasters Masters, string id, IStats stat);
    Task<UserMasters> GetSumUserMastersAsync(string userId);
}