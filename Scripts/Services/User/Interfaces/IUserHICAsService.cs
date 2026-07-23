using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHICAsService
{ 
    Task<UserHICAs> GetUserHICAsAsync(string userId, string id);
    Task InsertOrUpdateUserHICAsAsync(string userId, UserHICAs HICAs, string id);
    Task<UserHICAs> GetSumUserHICAsAsync(string userId);
}