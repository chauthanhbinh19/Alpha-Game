using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIINsService
{ 
    Task<UserHIINs> GetUserHIINsAsync(string id);
    Task InsertOrUpdateUserHIINsAsync(string user_id, UserHIINs HIINs, string id);
    Task<UserHIINs> GetSumUserHIINsAsync(string user_id);
}