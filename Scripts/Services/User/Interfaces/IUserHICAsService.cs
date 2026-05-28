using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHICAsService
{ 
    Task<UserHICAs> GetUserHICAsAsync(string id);
    Task InsertOrUpdateUserHICAsAsync(string user_id, UserHICAs HICAs, string id);
    Task<UserHICAs> GetSumUserHICAsAsync(string user_id);
}