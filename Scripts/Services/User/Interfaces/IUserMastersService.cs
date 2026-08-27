using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserMastersService
{ 
    Task<UserMasters> GetUserMastersAsync(string userId, string masterId, IStats stat);
    Task InsertOrUpdateUserMastersAsync(string userId, UserMasters Masters, IStats stat);
    Task<UserMasters> GetSumUserMastersAsync(string userId, IStats stat);
}